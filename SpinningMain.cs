
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Web; 
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

// add ref. "System.Web"
// add ref. IWshRuntimeLibrary "Windows Script Host Object Model"

namespace SpinningLog
{
	public partial class SpinningMain: Form
	{
		public SpinningMain()
		{
			InitializeComponent();
#if DEBUG
			// redirect debug log to file
			var dtl = (DefaultTraceListener)Debug.Listeners["Default"];
			dtl.LogFileName = Path.ChangeExtension(Application.ExecutablePath, "log");
			ExportHtmlMenu.Visible = true;
#else
			webBrowser1.ScriptErrorsSuppressed = true;
#endif

			var asm = System.Reflection.Assembly.GetExecutingAssembly();

			webBrowser1.ObjectForScripting = new ComOperation(this);
			webBrowser1.DocumentCompleted += (wb, e1) => {
				// show title and version
				var ver = asm.GetName().Version;
				this.Text = string.Format("{0} ver{1}.{2:D2}",
				  (webBrowser1.DocumentTitle != "") ? webBrowser1.DocumentTitle : this.Text,
				  ver.Major, ver.Minor);

				// panel takes over drag events, WebBrowser is not supported.
				webBrowser1.Document.Body.DragOver += (body, e2) => {
					DropPanel.BringToFront();
				};

#if DEBUG
				//webBrowser1.Document.Body.SetAttribute("className", "debug");
#endif

				LiveMenu.Checked = sett.live;

				webBrowser1.Document.GetElementById("merged").InnerHtml = "";
				DropPanel.BringToFront();
				RefreshMerged();
			};

			// load "empty.html" from resource
			using (var s = asm.GetManifestResourceStream("SpinningLog.empty.html"))
			using (var r = new StreamReader(s, Encoding.UTF8)) {
				webBrowser1.DocumentText = r.ReadToEnd();
			}

			ParseCommandLine(Environment.GetCommandLineArgs());
			RestoreSettings();
		}

		/// <summary>
		/// Parse command line arguments.
		/// </summary>
		/// <param name="args">Command line arguments</param>
		void ParseCommandLine(string[] args)
		{
			bool opt_new = false;
			string html_file = null;
			var files = new List<string>();
			for (int i = 1; i < args.Length; i++)
				if (args[i][0] == '-') {
					switch (args[i]) {
					case "--new":
						opt_new = true;
						break;

					case "--html":
						if (++i < args.Length)
							html_file = args[i];
						break;

					case "--file":
					case "-f":
						if (++i < args.Length)
							files.Add(args[i]);
						break;
					}
				} else
					files.Add(args[i]);

			if (opt_new)
				;
			else if (files.Count > 0)
				AddLogFiles(files);
			else if (Properties.Settings.Default.last_open_files != "") {
				string openfiles = Properties.Settings.Default.last_open_files;
				AddLogFiles(openfiles.Split('|'));
			}
			//RefreshMerged(); -> after DocumentComplete()
		}

		private void SpinningMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Debug.WriteLine("FormClosing({0})", e.CloseReason);
		}

		private void SpinningMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			try {
				SaveSettings();
				Debug.WriteLine("FormClosed(, {0})", e.CloseReason);
			} catch (Exception ex) {
				Debug.WriteLine("FormClosed: " + ex.Message);
			}
		}

		/// <summary>
		/// Convert timespan to string by application setting
		/// </summary>
		/// <param name="timespan">Time span to convert</param>
		/// <returns>Converted string</returns>
		string Format(TimeSpan timespan)
		{
			string s = timespan.ToString(@"h\:mm\:ss\.fff");
			if (timespan.Days != 0)
				return string.Format("{0} days, ", timespan.Days) + s;
			return s;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try {
				// refresh last time
				var timespan = DateTime.Now - this.LastTime;
				HtmlElement latest = webBrowser1.Document.GetElementById("latest");
				latest.InnerHtml = Format(timespan);

			} catch (Exception ex) {
				Debug.WriteLine("timer1_Tick: {0}", ex.Message);
			}
		}

		/// <summary>
		/// SpinningLog aplication settings
		/// </summary>
		public class SpinningSett
		{
			public SpinningSett()
			{
				// default values
				def_encoding = "UTF-8";
				//def_encoding = "Shift_JIS";
				blank_msec = 3000;
				highlights = "error|failed|fail|cannot|can not|can't";
				log_filters = new string[] { "*.log", "*.txt" };
			}

			[DefaultValueAttribute(FormWindowState.Normal)]
			public FormWindowState FormWindowState;

			public Rectangle win_bounds;    // bounds of main form 
			public string def_encoding;     // log file default encoding
			public double blank_msec;       // 
			public string[] log_filters;    // wildcard recognized as log files
			public string highlights;       // highlight keywords as Regex
			public bool live;               // live refresh mode
			public string line_filter;      // 

			public string html_file = "";
		}

		SpinningSett sett = new SpinningSett();
		string sett_filename = "";
		const string SLOG_EXT = ".slog";

		/// <summary>
		/// External text editor setting
		/// </summary>
		public class EditorSett
		{
			public string exe;
			public string goto_option;

			/// <summary>
			/// Open log with text editor.
			/// </summary>
			/// <param name="macros">Expanded macro, set of name-value pairs</param>
			public void Execute(Dictionary<string, object> macros)
			{
				Cursor.Current = Cursors.WaitCursor;
				string options = this.goto_option;
				foreach (string key in macros.Keys) {
					options = options.Replace(key, macros[key].ToString());
				}
				Process.Start(this.exe, options);
			}
		}

		EditorSett editor = new EditorSett();

		/// <summary>
		/// Restore application settings.
		/// </summary>
		void RestoreSettings()
		{
			if (!Properties.Settings.Default.valid)
				Properties.Settings.Default.Upgrade();

			editor.exe = Properties.Settings.Default.editor_exe;
			editor.goto_option = Properties.Settings.Default.editor_goto_option;

			// select setting file path
			sett_filename = Path.ChangeExtension(Application.ExecutablePath, SLOG_EXT);
			if (!File.Exists(sett_filename))
				sett_filename = Path.GetDirectoryName(Application.LocalUserAppDataPath) + SLOG_EXT;
			Debug.WriteLine("sett_filename=" + sett_filename);

			try {
				var serializer = new XmlSerializer(typeof(SpinningSett));
				using (var sr = new StreamReader(sett_filename, Encoding.UTF8)) {
					sett = (SpinningSett)serializer.Deserialize(sr);
				}
			} catch (Exception ex) {
				Debug.WriteLine(ex.Message);
				sett = new SpinningSett();
			}

			try {
				LogFile.DefaultEncoding = Encoding.GetEncoding(sett.def_encoding);
			} catch (Exception ex) {
				Debug.WriteLine(ex.Message);
				LogFile.DefaultEncoding = Encoding.UTF8;
			}

			if (sett.win_bounds.Width > 0) {
				this.StartPosition = FormStartPosition.Manual;
				this.Bounds = sett.win_bounds;
				if (sett.FormWindowState != FormWindowState.Minimized)
					this.WindowState = sett.FormWindowState;
			}

			HighlightsText.Text = sett.highlights;
			//LiveMenu.Checked = sett.live; -> after DocumentCompleted()
		}

		/// <summary>
		/// Save application settings.
		/// </summary>
		void SaveSettings()
		{
			Properties.Settings.Default.last_open_files = string.Join("|", log_files.Select(l => l.Name));

			Properties.Settings.Default.editor_exe = editor.exe;
			Properties.Settings.Default.editor_goto_option = editor.goto_option;

			Properties.Settings.Default.valid = true;
			Properties.Settings.Default.Save();

			// 
			sett.FormWindowState = this.WindowState;
			if (this.WindowState == FormWindowState.Normal)
				sett.win_bounds = this.Bounds;
			else
				sett.win_bounds = this.RestoreBounds;
			sett.def_encoding = LogFile.DefaultEncoding.WebName;
			sett.live = LiveMenu.Checked;

			var serializer = new XmlSerializer(typeof(SpinningSett));
			using (var sw = new StreamWriter(sett_filename, false, Encoding.UTF8)) {
				serializer.Serialize(sw, sett);
			}
		}

		// menu handlers

		private void AppNewMenu_Click(object sender, EventArgs e)
		{
			Process.Start(Application.ExecutablePath, "--new");
		}

		private void FileOpenMenu_Click(object sender, EventArgs e)
		{
			//openFileDialog1.FileName = 
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				AddLogFiles(openFileDialog1.FileNames);
				RefreshMerged();
			}
		}

		private void FileCloseMenu_Click(object sender, EventArgs e)
		{
			var tag = GetSelectedTag();
			if (tag != null) {
				log_files.Remove(tag.Log);
				ReloadAll();
			}
		}

		private void FileCloseAllMenu_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			log_files.Clear();
			merged_lines.Clear();
			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
		}

		private void FileExportMenu_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				HtmlElement div = webBrowser1.Document.GetElementById("merged");
				File.WriteAllText(saveFileDialog1.FileName, div.InnerText);
			}
		}

		private void ExportHtmlMenu_Click(object sender, EventArgs e)
		{
			string html = webBrowser1.Document.Body.OuterHtml;
			File.WriteAllText("export.html", html);
		}

		private void ViewReloadMenu_Click(object sender, EventArgs e)
		{
			ReloadAll();
		}

		private void ViewClearMenu_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
		}

		// scroll to top, and stop live refresh
		public void ViewHomeMenu_Click(object sender, EventArgs e)
		{
			LiveMenu.Checked = false;

			webBrowser1.Document.Window.ScrollTo(0, 0);
		}

		// scroll to bottom, and toggle live refresh
		public void LiveMenu_Click(object sender, EventArgs e)
		{
			//HtmlElement div = webBrowser1.Document.GetElementById("merged");
			//webBrowser1.Document.Window.ScrollTo(0, div.ScrollRectangle.Height);
			HtmlElement latest = webBrowser1.Document.GetElementById("latest");
			webBrowser1.Document.Window.ScrollTo(0, latest.ScrollRectangle.Bottom);
		}

		private void LiveMenu_CheckedChanged(object sender, EventArgs e)
		{
			HtmlElement live = webBrowser1.Document.GetElementById("live");
			if (/*log_files.Count > 0 && */LiveMenu.Checked)
				live.SetAttribute("className", "active");
			else
				live.SetAttribute("className", "");
		}

		private void HighlightsMenu_Click(object sender, EventArgs e)
		{
			//HighlightsPanel.Visible = !HighlightsPanel.Visible;
		}

		private void FilteringMenu_Click(object sender, EventArgs e)
		{
			//FilteringPanel.Visible = !FilteringPanel.Visible;
		}

		// open another text editor
		private void TagJumpMenu_Click(object sender, EventArgs e)
		{
			DataTag tag = GetSelectedTag();
			if (tag != null) {
				Dictionary<string, object> macros = new Dictionary<string, object>()
				{
					{ "${FILENAME}", tag.Log.Name },
					{ "${LINENO}",  1+tag.LineNo },
					{ "${LINENO1}",  1+tag.LineNo },
					{ "${LINENO0}",  tag.LineNo }
				};
				editor.Execute(macros);
			}
		}

		// open Windows Explorer
		private void ShowInExplorerMenu_Click(object sender, EventArgs e)
		{
			var tag = GetSelectedTag();
			if (tag != null) {
				Cursor.Current = Cursors.WaitCursor;
				Process.Start("EXPLORER.EXE", "/select,\"" + tag.Log.Name + "\"");
			}
		}

		// show application information
		private void HelpAboutMenu_Click(object sender, EventArgs e)
		{
			//throw new Exception("no implement");
			DropPanel.BringToFront();
		}

		// quit SpinningLog application
		private void AppExitMenu_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		// drag and drop
		private void SpinningMain_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void SpinningMain_DragDrop(object sender, DragEventArgs e)
		{
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			AddLogFiles(filenames);
			RefreshMerged();
		}

		private void DropPanel_DragLeave(object sender, EventArgs e)
		{
			DropPanel.SendToBack();
		}

		/// <summary>
		/// Interface between main form and WebBrowser 
		/// </summary>
		[ComVisible(true)]
		public class ComOperation
		{
			SpinningMain main;

			public ComOperation(SpinningMain main)
			{
				this.main = main;
			}

			// call from webBrowser1's script
			public void ComCommand(string command, string option)
			{
				switch (command) {
				case "home":
					main.ViewHomeMenu_Click(null, null);
					break;
				case "end":
				case "live-toggle":
					main.LiveMenu.Checked = !main.LiveMenu.Checked;
					break;
				case "select":
					//MessageBox.Show("select:"+option);
					break;
				}
			}
		}

		class DataTag
		{
			public LogFile Log;
			public int LineNo;
			public DataTag(LogFile log, int lineno)
			{
				this.Log = log;
				this.LineNo = lineno;
			}

			public DataTag(LogLine line)
			{
				this.Log = line.File;
				this.LineNo = line.LineNo;
			}

			public new string ToString()
			{
				return Log.ID + ":" + LineNo;
			}
		}

		/// <summary>
		/// Call javascript function in WebBrowser.
		/// </summary>
		/// <param name="funcname">Function name to call</param>
		/// <param name="args">Collection of parameters</param>
		/// <returns>Return value from called function</returns>
		object CallJavaScript(string funcname, string[] args)
		{
			return webBrowser1.Document.InvokeScript(funcname, args);
		}

		DataTag GetSelectedTag()
		{
			//string tag = (string)CallJavaScript("GetLastSelected", null);
			string tag = (string)webBrowser1.Document.InvokeScript("GetLastSelected", null);
			if (tag == null)
				return null;
			var ss = tag.Split(':');
			var log = this.log_files.Find(l => l.ID == ss[0]);
			int lineno = int.Parse(ss[1]);
			return new DataTag(log, lineno);
		}

		/// <summary>
		/// Expand shortcut (*.lnk)
		/// </summary>
		/// <param name="filename">file or Windows shortcut</param>
		/// <returns>full path of file</returns>
		public static string GetTargetPath(string filename)
		{
			if (Path.GetExtension(filename) != ".lnk")
				return Path.GetFullPath(filename);

			var shell = new IWshRuntimeLibrary.WshShell();
			var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(filename);
			return shortcut.TargetPath;
		}

		// log files and lines document class

		/// <summary>
		/// Log file information.
		/// </summary>
		class LogFile
		{
			public string Name { get; set; }    /// <summary>path of log file</summary>
			public string ID { get; private set; }
			static int serial = 0;

			public Color Color { get; set; }
			public TimeSpan TimeDifference { get; set; }
			public Encoding Encoding { get; set; }
			public static Encoding DefaultEncoding { get; set; } = Encoding.UTF8;

			public long LastPosition { get; set; }
			public int LineCount;

			static Color[] auto_colors = {
				Color.Gray, 
				// 赤・橙・黃・緑・青・藍・紫
				Color.Red, Color.Orange, Color.Yellow, Color.Lime, Color.Aqua/*, Color.Blue*/, Color.Fuchsia,
			};
			static int color_index = 0;

			public LogFile(string filename)
			{
				this.Name = filename;
				this.LastPosition = 0;
				this.LineCount = 0;
				this.ID = string.Format("{0}", serial++);

				// assign default color automatically
				this.Color = auto_colors[color_index];
				if (++color_index >= auto_colors.Length)
					color_index = 0;
				this.Encoding = LogFile.DefaultEncoding;
			}

			/// <summary>
			/// read unread lines from log file
			/// </summary>
			/// <returns>collection of lines</returns>
			public Queue<LogLine> ReadIncrLinesQ()
			{
				var lines = new Queue<LogLine>();
				try {
					string filename = GetTargetPath(this.Name);
					using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					using (var reader = new StreamReader(stream, this.Encoding)) {
						stream.Position = this.LastPosition;
						while (!reader.EndOfStream) {
							string s = reader.ReadLine();
							var line = new LogLine(this, s);
							line.LineNo = this.LineCount++;
							lines.Enqueue(line);
						}
						this.LastPosition = stream.Position;
					}
				} catch (Exception ex) {
					// if error, read at next timing
					Console.WriteLine(ex.Message);
				}
				return lines;
			}

			/// <summary>
			/// Last time in this file
			/// </summary>
			DateTime LastTime;

			/// <summary>
			/// 
			/// </summary>
			Regex TimeTemplate;

			static Regex[] templates = {
				new Regex(@"[0-9]+\/[0-9]+\/[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?(\s[AaPp][Mm])?"),	// 2020/03/23 06:00:00.123 PM
				new Regex(@"[0-9]+\-[0-9]+\-[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?(\s[AaPp][Mm])?"),	// 2020-03-23 06:00:00.123 PM
				new Regex(@"[0-9]+\/[0-9]+\/[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?"),	// 2020/03/23 06:00:00.123
				new Regex(@"[0-9]+\-[0-9]+\-[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?"),	// 2020-03-23 06:00:00.123
			};

			/// <summary>
			/// 
			/// </summary>
			/// <param name="line"></param>
			/// <returns></returns>
			public DateTime GetTimeFrom(string line)
			{
				if (TimeTemplate == null) {
					foreach (var regex in templates) {
						var match = regex.Match(line);
						if (match.Success && DateTime.TryParse(match.Value, out LastTime)) {
							TimeTemplate = regex;
							break;
						}
					}
				} else {
					var match = TimeTemplate.Match(line);
					if (match.Success)
						DateTime.TryParse(match.Value, out LastTime);
				}
				return this.LastTime;
			}

			/// <summary>
			/// 
			/// </summary>
			public void Reset()
			{
				this.LastPosition = 0;
				this.LastTime = new DateTime();
				this.LineCount = 0;
			}
		}

		/// <summary>
		/// A line of log
		/// </summary>
		class LogLine
		{
			public LogFile File { get; set; }
			public int LineNo { get; set; }
			public string Text { get; set; }
			public DateTime Time { get { return raw_time + File.TimeDifference; } }
			DateTime raw_time;

			public LogLine(LogFile file, string text)
			{
				this.File = file;
				this.Text = text;
				this.raw_time = file.GetTimeFrom(text);
			}
		}

		/// <summary>
		/// List of Log Files
		/// </summary>
		List<LogFile> log_files = new List<LogFile>();

		/// <summary>
		/// List of merged log lines
		/// </summary>
		List<LogLine> merged_lines = new List<LogLine>();

		/// <summary>
		/// Last time in all log files
		/// </summary>
		DateTime LastTime = DateTime.Now;

		/// <summary>
		/// Reload all log files.
		/// </summary>
		void ReloadAll()
		{
			merged_lines.Clear();
			foreach (var log in log_files)
				log.Reset();

			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
			RefreshMerged();
		}

		/// <summary>
		/// List up files, include under directories
		/// </summary>
		/// <param name="files">colection filename and directory</param>
		void AddLogFiles(IEnumerable<string> files)
		{
			foreach (string path in files) {
				string link_to = GetTargetPath(path);
				if (Directory.Exists(link_to)) {
					foreach (string filter in sett.log_filters)
						AddLogFiles(Directory.GetFiles(link_to, filter, SearchOption.AllDirectories));
				} else {
					if (!log_files.Any(l => l.Name == path))
						log_files.Add(new LogFile(path));
				}
			}
		}

		/// <summary>
		/// Load and merge lines of listed log files
		/// </summary>
		void RefreshMerged()
		{
			DropPanel.SendToBack();
			int tc0 = Environment.TickCount;
			var sw = Stopwatch.StartNew();

			// read lines
			var queues = new List<Queue<LogLine>>();
			foreach (var log in log_files) {
				Queue<LogLine> queue = log.ReadIncrLinesQ();
				if (queue.Count > 0)
					queues.Add(queue);
			}

			long dbg_read_msec = sw.ElapsedMilliseconds;
			sw.Restart();

			if (queues.Count > 0) {
				Cursor.Current = Cursors.WaitCursor;

				// merge
				int start_index = merged_lines.Count;
				while (queues.Count > 0) {
					var top = queues[0];
					foreach (var q in queues)
						if (top.Peek().Time > q.Peek().Time)
							top = q;

					LogLine line = top.Dequeue();
					if (top.Count <= 0)
						queues.Remove(top);

					merged_lines.Add(line);
				}

				long dbg_merge1_msec = sw.ElapsedMilliseconds;
				sw.Restart();

				string html = GenerateHtml(start_index);

				// set html, scroll
				long dbg_merge2_msec = sw.ElapsedMilliseconds;
				sw.Restart();

				if (html.Length > 0/*n > 0*/) {
					HtmlElement div = webBrowser1.Document.GetElementById("merged");
					var pre = webBrowser1.Document.CreateElement("pre");
					pre.InnerHtml = html;//.ToString();
					pre.SetAttribute("className", "flash-effect");
					div.InsertAdjacentElement(HtmlElementInsertionOrientation.BeforeEnd, pre);

					long dbg_html_msec = sw.ElapsedMilliseconds;
					sw.Restart();

					// scroll to newest line
					webBrowser1.Document.Window.ScrollTo(0, div.ScrollRectangle.Height + 100);
					Console.WriteLine(div.ScrollRectangle);
					long dbg_scroll_msec = sw.ElapsedMilliseconds;

					int n = merged_lines.Count - start_index;
					Console.WriteLine("RefreshMerged: {0} lines / read {1}, merge1 {2}, merge2 {3}, html {4} msec, scroll {5}",
					 n, dbg_read_msec, dbg_merge1_msec, dbg_merge2_msec, dbg_html_msec, dbg_scroll_msec);
				}

				Cursor.Current = Cursors.Default;
				DropPanel.SendToBack();
			}
		}

		bool highlight_enabled = true;

		string GenerateHtml(int start_index)
		{
			var html = new StringBuilder(1000*1000);
			Regex filter = (sett.line_filter != null) ? new Regex(sett.line_filter) : null;
			for (int i = start_index; i < merged_lines.Count; i++) {
				var line = merged_lines[i];

				// insert time span if more than 3000 msec
				if (i > 0) {
					var timespan = line.Time - LastTime;
					if (timespan.TotalMilliseconds > sett.blank_msec)
						html.AppendLine("<span class='blank'>" + Format(timespan) + "</span>");
					else if (timespan.TotalMilliseconds < -sett.blank_msec)
						html.AppendLine("<span class='blank back'>" + Format(timespan) + "</span>");
				}
				LastTime = line.Time;

				string text = line.Text;
				text = text.Replace('\0', ' ').TrimEnd();
				text = HttpUtility.HtmlEncode(text);
				if (highlight_enabled)
					text = Regex.Replace(text, sett.highlights, "<span class=highlight>$0</span>", RegexOptions.IgnoreCase);
				if (filter != null) {
					var match = filter.Match(text);
					if (!match.Success)
						continue;
					text = Regex.Replace(text, sett.line_filter, "<span class=filter>$0</span>", RegexOptions.IgnoreCase);
				}

				var tag = new DataTag(line);
				html.Append("<label style=color:" + line.File.Color.Name
				 + " data-tag=" + tag.ToString() + ">"
				 + Path.GetFileName(line.File.Name) + "</label> "
				 + text + "\n");
			}
			return html.ToString();
		}

		private void LiveTimer_Tick(object sender, EventArgs e)
		{
			try {
				// check any file modified
				if (LiveMenu.Checked)
					RefreshMerged();

			} catch (Exception ex) {
				Debug.WriteLine("LiveTimer: " + ex.Message);
			}
		}


		private void ApplyButton_Click(object sender, EventArgs e)
		{
			sett.highlights = HighlightsText.Text;
			ReloadAll();
		}

		private void HighlightsText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				ApplyButton_Click(null, null);
		}
	}
}
