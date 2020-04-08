using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Web;   // add ref. "System.Web"
using System.Text.RegularExpressions;

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
#endif

			var asm = System.Reflection.Assembly.GetExecutingAssembly();
			var ver = asm.GetName().Version;
			this.Text = string.Format("{0} ver{1}.{2:D2}", this.Text, ver.Major, ver.Minor);

			// load "empty.html" from resource
			using (var s = asm.GetManifestResourceStream("SpinningLog.empty.html"))
			using (var r = new StreamReader(s, Encoding.UTF8)) {
				webBrowser1.DocumentText = r.ReadToEnd();
			}

		}

		private void SpinningMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Debug.WriteLine("FormClosing({0})", e.CloseReason);
		}

		private void SpinningMain_FormClosed(object sender, FormClosedEventArgs e)
		{

		}


		void BlinkLive(DateTime now)
		{
			if (log_files.Count > 0 && LiveCheck.Checked) {
				int v = 255 * now.Millisecond / 1000;
				LiveCheck.ForeColor = Color.FromArgb(255, 255-v, 255-v);
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			var now = DateTime.Now;
			try {
				BlinkLive(now);

			} catch (Exception ex) {
				Debug.WriteLine("timer1_Tick: {0}", ex.Message);
			}
		}


		// menu handlers

		private void FileNewMenu_Click(object sender, EventArgs e)
		{
			Process.Start(Application.ExecutablePath);
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
			// save merged log files to text file
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				var pre = webBrowser1.Document.GetElementById("merged");
				File.WriteAllText(saveFileDialog1.FileName, pre.InnerText);
			}
		}

		private void ViewRefreshMenu_Click(object sender, EventArgs e)
		{
			Refresh();
		}

		private void ViewClearMenu_Click(object sender, EventArgs e)
		{
			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
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
			Cursor.Current = Cursors.WaitCursor;
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			AddLogFiles(filenames);
			RefreshMerged();
		}


		// log files and lines document class

		class LogFile
		{
			public string Filename { get; set; }

			public long LastPosition { get; set; }

			public LogFile(string filename)
			{
				this.Filename = filename;
				this.LastPosition = 0;

			}

			// read unread lines
			public Queue<LogLine> GetIncrLinesQ()
			//public List<LogLine> GetIncrLines()
			{
				var lines = new Queue<LogLine>();
				using (var stream = new FileStream(this.Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
			public DateTime Time { get; set; }

			public LogLine(LogFile log, string text)
			{
				this.LogFile = log;
				this.Text = text;
				this.Time = log.GetTimeFrom(Text);
			}
		}

		List<LogFile> log_files = new List<LogFile>();
		List<LogLine> merged = new List<LogLine>();

		List<string> LogFilters = new List<string>() { "*.log", "*.txt", /*"*.log.*" */ };

		void Clear()
		{
			// close all files, clear screen
			log_files.Clear();
			merged.Clear();
			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
		}

		void Refresh()
		{
			// reload all files, refresh screen
			merged.Clear();
			foreach (var log in log_files)
				log.Reset();

			webBrowser1.Document.GetElementById("merged").InnerHtml = "";
			RefreshMerged();
		}

		void AddLogFiles(string[] files)
		{
			foreach (string path in files) {
				if (Directory.Exists(path)) {
					foreach (string filter in LogFilters)
						AddLogFiles(Directory.GetFiles(path, filter, SearchOption.AllDirectories));
				} else if (File.Exists(path)) {
					if (log_files.Any(l => l.Filename == path))
						continue;   // ignore already exists
					log_files.Add(new LogFile(path));
					Console.WriteLine("add file: {0}", path);
				} else
					Console.WriteLine("no file: {0}", path);
			}
		}

		void RefreshMerged()
		{
			var group = new List<Queue<LogLine>>();
			foreach (var log in log_files) {
				Queue<LogLine> lines = log.GetIncrLinesQ();
				if (lines.Count > 0)
					group.Add(lines);
			}

			var html = new StringBuilder(1000*1000);
			while (group.Count > 0) {
				var top = group[0];
				foreach (var g in group)
					if (top.Peek().Time > g.Peek().Time)
						top = g;

				LogLine line = top.Dequeue();
				if (top.Count <= 0)
					group.Remove(top);

				string text = line.Text;
				text = text.Replace('\0', ' ').TrimEnd();
				text = HttpUtility.HtmlEncode(text);

				html.Append(Path.GetFileName(line.LogFile.Filename) + ": "
				 + text + "\n");
				merged.Add(line);
			}

			if (html.Length > 0) {
				var pre = webBrowser1.Document.GetElementById("merged");
				pre.InnerHtml += html.ToString();

				// scroll to newest line
				webBrowser1.Document.Window.ScrollTo(0, pre.ScrollRectangle.Height);
			}
		}

		#region live update
		int LiveInterval { get { return LiveTimer.Interval; } set { LiveTimer.Interval = value; } }

		private void LiveTimer_Tick(object sender, EventArgs e)
		{
			try {
				if (LiveCheck.Checked)
					RefreshMerged();

			} catch (Exception ex) {
				Debug.WriteLine("LiveTimer: " + ex.Message);
			}
		}

		private void LiveCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (LiveCheck.Checked) {
				LiveCheck.Font = new Font(LiveCheck.Font, FontStyle.Bold);
			} else {
				LiveCheck.Font = new Font(LiveCheck.Font, FontStyle.Regular);
				LiveCheck.ForeColor = SystemColors.ControlText;
			}
		}
		#endregion

	}
}
