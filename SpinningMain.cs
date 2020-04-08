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
				//this.Time = log.GetTimeFrom(Text);
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
				} else
					Debug.WriteLine("no file: {0}", path);
			}
		}

	}
}
