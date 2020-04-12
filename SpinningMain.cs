
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
			// redirect Debug log to file
			var dtl = (DefaultTraceListener)Debug.Listeners["Default"];
			dtl.LogFileName = Path.ChangeExtension(Application.ExecutablePath, "log");
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

				webBrowser1.Visible = false;
				RefreshMerged();
				webBrowser1.Visible = true;
			};

			// load "empty.html" from resource
			using (var s = asm.GetManifestResourceStream("SpinningLog.empty.html"))
			using (var r = new StreamReader(s, Encoding.UTF8)) {
				webBrowser1.DocumentText = r.ReadToEnd();
			}

			ParseCommandLine(Environment.GetCommandLineArgs());
			RestoreSettings();
		}

		void ParseCommandLine(string[] args)
		{
			bool opt_new = false;
			var files = new List<string>();
			for (int i = 1; i < args.Length; i++)
				if (args[i][0] == '-') {
					switch (args[i]) {
					case "--new":
						opt_new = true;
						break;

					case "--file":
					case "-f":
						if (++i < args.Length) files.Add(args[i]);
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
			Debug.WriteLine("FormClosing({0})", e.CloseReason);
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


		/*void BlinkLive(DateTime now)
		{
			if (log_files.Count > 0 && LiveMenu.Checked) {
				int v = 255 * now.Millisecond / 1000;
				LiveMenu.ForeColor = Color.FromArgb(255, 255-v, 255-v);
			} else
				LiveMenu.ForeColor = SystemColors.ControlText;
		}*/

		private void timer1_Tick(object sender, EventArgs e)
		{
			var now = DateTime.Now;
			try {
				//BlinkLive(now);

			} catch (Exception ex) {
				Debug.WriteLine("timer1_Tick: {0}", ex.Message);
			}
		}


		void RestoreSettings()
		{
			if (!Properties.Settings.Default.valid)
				Properties.Settings.Default.Upgrade();

			if (Properties.Settings.Default.win_width > 0) {
				this.StartPosition = FormStartPosition.Manual;
				this.Left = Properties.Settings.Default.win_left;
				this.Top = Properties.Settings.Default.win_top;
				this.Width = Properties.Settings.Default.win_width;
				this.Height = Properties.Settings.Default.win_height;

				var win_state = (FormWindowState)Properties.Settings.Default.win_state;
				if (win_state != FormWindowState.Minimized)
					this.WindowState = win_state;
			}
		}

		void SaveSettings()
		{
			Properties.Settings.Default.last_open_files = string.Join("|", log_files.Select(x => x.Filename));

			Properties.Settings.Default.win_state = (int)this.WindowState;
			if (this.WindowState == FormWindowState.Normal) {
				Properties.Settings.Default.win_left = this.Left;
				Properties.Settings.Default.win_top = this.Top;
				Properties.Settings.Default.win_width = this.Width;
				Properties.Settings.Default.win_height = this.Height;
			} else {
				Properties.Settings.Default.win_left = this.RestoreBounds.Left;
				Properties.Settings.Default.win_top = this.RestoreBounds.Top;
				Properties.Settings.Default.win_width = this.RestoreBounds.Width;
				Properties.Settings.Default.win_height = this.RestoreBounds.Height;
			}

			Properties.Settings.Default.valid = true;
			Properties.Settings.Default.Save();
		}

		// menu handlers

		private void FileNewMenu_Click(object sender, EventArgs e)
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
			throw new Exception("no implement");
		}

		private void FileCloseAllMenu_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void FileExportMenu_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				var div = webBrowser1.Document.GetElementById("merged");
				File.WriteAllText(saveFileDialog1.FileName, div.InnerText);
			}
		}

		private void ViewRefreshMenu_Click(object sender, EventArgs e)
		{
			Reload();
		}

		private void ViewClearMenu_Click(object sender, EventArgs e)
		{
			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
		}

		public void BackToTopMenu_Click(object sender, EventArgs e)
		{
			LiveMenu.Checked = false;

			webBrowser1.Document.Window.ScrollTo(0, 0);
		}

		public void LiveMenu_Click(object sender, EventArgs e)
		{
			LiveMenu.Checked = !LiveMenu.Checked;

			var div = webBrowser1.Document.GetElementById("merged");
			webBrowser1.Document.Window.ScrollTo(0, div.ScrollRectangle.Height);
		}

		private void LiveMenu_CheckedChanged(object sender, EventArgs e)
		{
			Console.WriteLine("LiveMenu.CHecked = {0}", LiveMenu.Checked);
		}

		private void HelpAboutMenu_Click(object sender, EventArgs e)
		{
			throw new Exception("no implement");
		}

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

		// interface between main form and WebBrowser
		[ComVisible(true)]
		public class ComOperation
		{
			SpinningMain main;

			public ComOperation(SpinningMain main)
			{
				this.main = main;
			}

			// call from webBrowser1's script
			public void ComCommand(string command)
			{
				switch (command) {
				case "home":
					main.BackToTopMenu_Click(null, null); 
					break;
				case "end":
					main.LiveMenu_Click(null, null); 
					break;
				}
			}
		}

		void CallJavaScript(string funcname, string[] args)
		{
			webBrowser1.Document.InvokeScript(funcname, args);
		}

		public static string GetTargetPath(string filename)
		{
			if (Path.GetExtension(filename) != ".lnk")
				return filename;

			var shell = new IWshRuntimeLibrary.WshShell();
			var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(filename);
			return shortcut.TargetPath;
		}


		// log files and lines document class

		class LogFile
		{
			public string Filename { get; set; }
			public Color Color { get; set; }

			public long LastPosition { get; set; }

			static Color[] auto_colors = {
				Color.Gray, 
				// 赤・橙・黃・緑・青・藍・紫
				Color.Red, Color.Orange, Color.Yellow, Color.Lime, Color.Aqua/*, Color.Blue*/, Color.Fuchsia,
			};
			static int color_index = 0;

			public LogFile(string filename)
			{
				this.Filename = filename;
				this.LastPosition = 0;

				// assign default color automatically
				this.Color = auto_colors[color_index];
				if (++color_index >= auto_colors.Length)
					color_index = 0;
			}

			// read unread lines
			public Queue<LogLine> ReadIncrLinesQ()
			//public List<LogLine> GetIncrLines()
			{
				var lines = new Queue<LogLine>();
				string filename = GetTargetPath(this.Filename);
				using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var reader = new StreamReader(stream, Encoding.UTF8)) {
					stream.Position = this.LastPosition;
					while (!reader.EndOfStream) {
						string s = reader.ReadLine();
						var line = new LogLine(this, s);
						lines.Enqueue(line);
					}
					this.LastPosition = stream.Position;
				}
				return lines;
			}

			DateTime LastTime;
			Regex TimeTemplate;

			static Regex[] templates = {
				new Regex(@"[0-9]+\/[0-9]+\/[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?(\s[AaPp][Mm])?"),	// 2020/03/23 06:00:00.123 PM
				new Regex(@"[0-9]+\-[0-9]+\-[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?(\s[AaPp][Mm])?"),	// 2020-03-23 06:00:00.123 PM
				new Regex(@"[0-9]+\/[0-9]+\/[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?"),	// 2020/03/23 06:00:00.123
				new Regex(@"[0-9]+\-[0-9]+\-[0-9]+\s+[0-9]+\:[0-9]+\:[0-9]+(\.[0-9]+)?"),	// 2020-03-23 06:00:00.123
			};

			public DateTime GetTimeFrom(string text)
			{
				if (TimeTemplate == null) {
					foreach (var regex in templates) {
						var match = regex.Match(text);
						if (match.Success && DateTime.TryParse(match.Value, out LastTime)) {
							TimeTemplate = regex;
							break;
						}
					}
				} else {
					var match = TimeTemplate.Match(text);
					if (match.Success)
						DateTime.TryParse(match.Value, out LastTime);
				}
				return this.LastTime;
			}

			public void Reset()
			{
				this.LastPosition = 0;
				this.LastTime = new DateTime();
			}
		}

		class LogLine
		{
			public LogFile LogFile { get; set; }
			public string Text { get; set; }
			public DateTime Time { get { return raw_time + TimeDifference; } }
			DateTime raw_time;
			TimeSpan TimeDifference;
			public Encoding Encoding;

			public static Encoding DefaultEncoding = Encoding.UTF8;

			public LogLine(LogFile log, string text)
			{
				this.LogFile = log;
				this.Text = text;
				this.raw_time = log.GetTimeFrom(Text);
				this.Encoding = LogLine.DefaultEncoding;
			}
		}

		List<LogFile> log_files = new List<LogFile>();
		List<LogLine> merged_lines = new List<LogLine>();
		DateTime LastTime = DateTime.MinValue;

		string TimeSpanString(TimeSpan timespan)
		{
			string s = timespan.ToString(@"h\:mm\:ss\.fff");
			if (timespan.Days != 0)
				return string.Format("{0} days and\n", timespan.Days) + s;
			return s;
		}

		void Clear()
		{
			// close all files, clear screen
			log_files.Clear();
			merged_lines.Clear();
			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
		}

		// reload all files
		void Reload()
		{
			merged_lines.Clear();
			foreach (var log in log_files)
				log.Reset();

			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
			RefreshMerged();
		}

		List<string> LogFilters = new List<string>() { "*.log", "*.txt", /*"*.log.*" */ };

		void AddLogFiles(IEnumerable<string> files)
		{
			foreach (string path in files) {
				string link_to = GetTargetPath(path);
				if (Directory.Exists(path) || Directory.Exists(link_to)) {
					foreach (string filter in LogFilters)
						AddLogFiles(Directory.GetFiles(link_to, filter, SearchOption.AllDirectories));
				} else if (File.Exists(path)) {
					if (log_files.Any(l => l.Filename == path))
						continue;   // ignore already exists
					log_files.Add(new LogFile(path));
					Console.WriteLine("add file: {0}", path);
				} else
					Console.WriteLine("no file: {0}", path);
			}
		}

		List<string> HighlightWords = new List<string>(){
			"error",
			"failed", "fail",
			"cannot", "can not", "can't",
		};

		static string HightlightHtml(string text, List<string> words)
		{
			return Regex.Replace(text, "(" + string.Join("|", words) + ")",
			  "<span class=highlight>$0</span>", RegexOptions.IgnoreCase);
		}

		void RefreshMerged()
		{
			DropPanel.SendToBack();
			int tc0 = Environment.TickCount;

			var queues = new List<Queue<LogLine>>();
			foreach (var log in log_files) {
				Queue<LogLine> queue = log.ReadIncrLinesQ();
				if (queue.Count > 0)
					queues.Add(queue);
			}

			var html = new StringBuilder(1000*1000);
			while (queues.Count > 0) {
				if (Environment.TickCount - tc0 > 100)
					Cursor.Current = Cursors.WaitCursor;

				var top = queues[0];
				foreach (var q in queues)
					if (top.Peek().Time > q.Peek().Time)
						top = q;

				LogLine line = top.Dequeue();
				if (top.Count <= 0)
					queues.Remove(top);

				// insert time span if more than 3000 msec
				var timespan = line.Time - LastTime;
				if (timespan.TotalMilliseconds >= 3000) {
					html.AppendLine("<span class='timespan'>" + TimeSpanString(timespan) + "</span>");
				} else if (timespan.TotalMilliseconds < 0) {
					html.AppendLine("<span class='timespan back'>" + TimeSpanString(timespan) + "</span>");
				}
				LastTime = line.Time;

				string text = line.Text;
				text = text.Replace('\0', ' ').TrimEnd();
				text = HttpUtility.HtmlEncode(text);

				text = HightlightHtml(text, HighlightWords);

				html.Append("<label style=color:" + line.LogFile.Color.Name + ">"
				 + Path.GetFileName(line.LogFile.Filename) + "</label> "
				 + text + "\n");

				merged_lines.Add(line);
			}

			if (html.Length > 0) {
				Cursor.Current = Cursors.WaitCursor;

				var div = webBrowser1.Document.GetElementById("merged");
				// 多分ここが遅い
				//pre.InnerHtml += html.ToString();
				var pre = webBrowser1.Document.CreateElement("pre");
				pre.InnerHtml = html.ToString();
				pre.SetAttribute("className", "flash-effect");
				div.InsertAdjacentElement(HtmlElementInsertionOrientation.BeforeEnd, pre);

				// scroll to newest line
				webBrowser1.Document.Window.ScrollTo(0, div.ScrollRectangle.Height);
			}

			Cursor.Current = Cursors.Default;
			DropPanel.SendToBack();
		}

		#region live update
		private void LiveTimer_Tick(object sender, EventArgs e)
		{
			try {
				if (LiveMenu.Checked)
					RefreshMerged();

			} catch (Exception ex) {
				Debug.WriteLine("LiveTimer: " + ex.Message);
			}
		}

		private void LiveCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (LiveMenu.Checked) {
				//LiveMenu.Font = new Font(LiveMenu.Font, FontStyle.Bold);
			} else {
				//LiveMenu.Font = new Font(LiveMenu.Font, FontStyle.Regular);
				LiveMenu.ForeColor = SystemColors.ControlText;
			}
		}
		#endregion

	}
}
