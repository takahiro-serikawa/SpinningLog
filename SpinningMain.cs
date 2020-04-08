using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinningLog
{
	public partial class SpinningMain: Form
	{
		public SpinningMain()
		{
			InitializeComponent();
		}


		// menu handlers

		private void FileNewMenu_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.ExecutablePath);
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

		}

		private void ViewClearMenu_Click(object sender, EventArgs e)
		{

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

		private void SpinningMain_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void SpinningMain_DragOver(object sender, DragEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			AddLogFiles(filenames);
		}


		void AddLogFiles(string[] files)
		{

		}
	}
}
