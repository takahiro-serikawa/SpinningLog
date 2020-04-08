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

			webBrowser1.DocumentText = @"<html>
<head>
 <meta http-equiv='X-UA-Compatible' content='IE=edge'>
 <title>spinnin' log</title>
<style></style>
</head>
<body>
 <pre id='merged'></pre>
</body>
</html>";

		}

		private void SpinningMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Debug.WriteLine("FormClosing({0})", e.CloseReason);
		}

		private void SpinningMain_FormClosed(object sender, FormClosedEventArgs e)
		{

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
			throw new Exception("no implement");
		}

		private void FileExportMenu_Click(object sender, EventArgs e)
		{
			throw new Exception("no implement");
		}

		private void ViewRefreshMenu_Click(object sender, EventArgs e)
		{
			throw new Exception("no implement");
		}

		private void ViewClearMenu_Click(object sender, EventArgs e)
		{
			throw new Exception("no implement");
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
			public List<LogLine> GetIncrLines()
			{
				var lines = new List<LogLine>();
				using (var stream = new FileStream(this.Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var reader = new StreamReader(stream, Encoding.UTF8)) {
					stream.Position = this.LastPosition;
					while (!reader.EndOfStream) {
						string s = reader.ReadLine();
						var line = new LogLine(this, s);
						lines.Add(line);
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
		string LogFilter = "*.log";

		void AddLogFiles(string[] files)
		{
			foreach (string path in files) {
				if (Directory.Exists(path)) {
					AddLogFiles(Directory.GetFiles(path, LogFilter, SearchOption.AllDirectories));
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
			var group = new List<List<LogLine>>();
			foreach (var log in log_files) {
				List<LogLine> lines = log.GetIncrLines();
				if (lines.Count > 0)
					group.Add(lines);
			}

			var html = new StringBuilder(1000*1000);
			while (group.Count > 0) {
				var top = group[0];
				foreach (var g in group)
					if (top[0].Time > g[0].Time)
						top = g;

				LogLine line = top[0];
				top.RemoveAt(0);
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
			}
		}

	}
}
