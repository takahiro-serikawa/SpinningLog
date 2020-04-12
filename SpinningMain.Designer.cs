namespace SpinningLog
{
	partial class SpinningMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileNewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseAllMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileExportMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.AppExitMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewClearMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.BackToTopMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.LiveMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.TagJumpMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ShowInExplorerMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpAboutMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.LiveTimer = new System.Windows.Forms.Timer(this.components);
			this.DropPanel = new System.Windows.Forms.Panel();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// webBrowser1
			// 
			this.webBrowser1.AllowNavigation = false;
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(0, 24);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(800, 400);
			this.webBrowser1.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNewMenu,
            this.FileOpenMenu,
            this.FileCloseMenu,
            this.FileCloseAllMenu,
            this.FileExportMenu,
            this.toolStripMenuItem1,
            this.AppExitMenu});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// FileNewMenu
			// 
			this.FileNewMenu.Name = "FileNewMenu";
			this.FileNewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.FileNewMenu.Size = new System.Drawing.Size(180, 22);
			this.FileNewMenu.Text = "&New ...";
			this.FileNewMenu.ToolTipText = "SpinningLogをもう一つ開く";
			this.FileNewMenu.Click += new System.EventHandler(this.FileNewMenu_Click);
			// 
			// FileOpenMenu
			// 
			this.FileOpenMenu.Name = "FileOpenMenu";
			this.FileOpenMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.FileOpenMenu.Size = new System.Drawing.Size(180, 22);
			this.FileOpenMenu.Text = "&Open ...";
			this.FileOpenMenu.ToolTipText = "ログファイルを追加";
			this.FileOpenMenu.Click += new System.EventHandler(this.FileOpenMenu_Click);
			// 
			// FileCloseMenu
			// 
			this.FileCloseMenu.Enabled = false;
			this.FileCloseMenu.Name = "FileCloseMenu";
			this.FileCloseMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.FileCloseMenu.Size = new System.Drawing.Size(180, 22);
			this.FileCloseMenu.Text = "&Close";
			this.FileCloseMenu.Click += new System.EventHandler(this.FileCloseMenu_Click);
			// 
			// FileCloseAllMenu
			// 
			this.FileCloseAllMenu.Name = "FileCloseAllMenu";
			this.FileCloseAllMenu.Size = new System.Drawing.Size(180, 22);
			this.FileCloseAllMenu.Text = "C&lose All";
			this.FileCloseAllMenu.ToolTipText = "すべてのログファイルを閉じる";
			this.FileCloseAllMenu.Click += new System.EventHandler(this.FileCloseAllMenu_Click);
			// 
			// FileExportMenu
			// 
			this.FileExportMenu.Name = "FileExportMenu";
			this.FileExportMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.FileExportMenu.Size = new System.Drawing.Size(180, 22);
			this.FileExportMenu.Text = "Export ...";
			this.FileExportMenu.ToolTipText = "マージしたログをテキストファイルに書き出す";
			this.FileExportMenu.Click += new System.EventHandler(this.FileExportMenu_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
			// 
			// AppExitMenu
			// 
			this.AppExitMenu.Name = "AppExitMenu";
			this.AppExitMenu.Size = new System.Drawing.Size(180, 22);
			this.AppExitMenu.Text = "e&Xit";
			this.AppExitMenu.ToolTipText = "終了";
			this.AppExitMenu.Click += new System.EventHandler(this.AppExitMenu_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewReloadMenu,
            this.ViewClearMenu,
            this.BackToTopMenu,
            this.LiveMenu,
            this.toolStripMenuItem2,
            this.TagJumpMenu,
            this.ShowInExplorerMenu});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// ViewReloadMenu
			// 
			this.ViewReloadMenu.Name = "ViewReloadMenu";
			this.ViewReloadMenu.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.ViewReloadMenu.Size = new System.Drawing.Size(180, 22);
			this.ViewReloadMenu.Text = "&Reload All";
			this.ViewReloadMenu.ToolTipText = "すべてのログファイルを再読み込み";
			this.ViewReloadMenu.Click += new System.EventHandler(this.ViewReloadMenu_Click);
			// 
			// ViewClearMenu
			// 
			this.ViewClearMenu.Name = "ViewClearMenu";
			this.ViewClearMenu.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.ViewClearMenu.Size = new System.Drawing.Size(180, 22);
			this.ViewClearMenu.Text = "&Clear Screen";
			this.ViewClearMenu.ToolTipText = "画面表示のみクリア";
			this.ViewClearMenu.Click += new System.EventHandler(this.ViewClearMenu_Click);
			// 
			// BackToTopMenu
			// 
			this.BackToTopMenu.Name = "BackToTopMenu";
			this.BackToTopMenu.ShortcutKeyDisplayString = "HOME";
			this.BackToTopMenu.Size = new System.Drawing.Size(180, 22);
			this.BackToTopMenu.Tag = "";
			this.BackToTopMenu.Text = "Back to &Top";
			this.BackToTopMenu.ToolTipText = "先頭行にスクロールして、 LIVE 表示を停止";
			this.BackToTopMenu.Click += new System.EventHandler(this.BackToTopMenu_Click);
			// 
			// LiveMenu
			// 
			this.LiveMenu.Checked = true;
			this.LiveMenu.CheckOnClick = true;
			this.LiveMenu.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LiveMenu.Name = "LiveMenu";
			this.LiveMenu.ShortcutKeyDisplayString = "END";
			this.LiveMenu.Size = new System.Drawing.Size(180, 22);
			this.LiveMenu.Text = "&Live";
			this.LiveMenu.ToolTipText = "ログファイルを監視";
			this.LiveMenu.CheckedChanged += new System.EventHandler(this.LiveMenu_CheckedChanged);
			this.LiveMenu.Click += new System.EventHandler(this.LiveMenu_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
			// 
			// TagJumpMenu
			// 
			this.TagJumpMenu.Enabled = false;
			this.TagJumpMenu.Name = "TagJumpMenu";
			this.TagJumpMenu.Size = new System.Drawing.Size(180, 22);
			this.TagJumpMenu.Text = "Tag &Jump ...";
			this.TagJumpMenu.Click += new System.EventHandler(this.TagJumpMenu_Click);
			// 
			// ShowInExplorerMenu
			// 
			this.ShowInExplorerMenu.Enabled = false;
			this.ShowInExplorerMenu.Name = "ShowInExplorerMenu";
			this.ShowInExplorerMenu.Size = new System.Drawing.Size(180, 22);
			this.ShowInExplorerMenu.Text = "&Explorer ...";
			this.ShowInExplorerMenu.Click += new System.EventHandler(this.ShowInExplorerMenu_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAboutMenu});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// HelpAboutMenu
			// 
			this.HelpAboutMenu.Enabled = false;
			this.HelpAboutMenu.Name = "HelpAboutMenu";
			this.HelpAboutMenu.Size = new System.Drawing.Size(180, 22);
			this.HelpAboutMenu.Text = "&About SpinningLog";
			this.HelpAboutMenu.ToolTipText = "バージョン情報を表示";
			this.HelpAboutMenu.Click += new System.EventHandler(this.HelpAboutMenu_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "log";
			this.openFileDialog1.FileName = "*.log";
			this.openFileDialog1.Filter = "log file(*.log)|*.log|plain text(*.txt)|*.txt|all files(*.*)|*.*";
			this.openFileDialog1.Multiselect = true;
			this.openFileDialog1.Title = "add log files";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "log";
			this.saveFileDialog1.FileName = "*.log";
			this.saveFileDialog1.Filter = "log file(*.log)|*.log|plain text(*.txt)|*.txt|all files(*.*)|*.*";
			this.saveFileDialog1.Title = "export merged log";
			// 
			// LiveTimer
			// 
			this.LiveTimer.Enabled = true;
			this.LiveTimer.Interval = 1000;
			this.LiveTimer.Tick += new System.EventHandler(this.LiveTimer_Tick);
			// 
			// DropPanel
			// 
			this.DropPanel.AllowDrop = true;
			this.DropPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.DropPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DropPanel.Location = new System.Drawing.Point(0, 24);
			this.DropPanel.Name = "DropPanel";
			this.DropPanel.Size = new System.Drawing.Size(800, 400);
			this.DropPanel.TabIndex = 3;
			this.DropPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.SpinningMain_DragDrop);
			this.DropPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.SpinningMain_DragOver);
			this.DropPanel.DragLeave += new System.EventHandler(this.DropPanel_DragLeave);
			// 
			// SpinningMain
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(800, 424);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.DropPanel);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(300, 100);
			this.Name = "SpinningMain";
			this.Text = "spinnin\' log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpinningMain_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SpinningMain_FormClosed);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SpinningMain_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.SpinningMain_DragOver);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileNewMenu;
		private System.Windows.Forms.ToolStripMenuItem FileOpenMenu;
		private System.Windows.Forms.ToolStripMenuItem FileCloseMenu;
		private System.Windows.Forms.ToolStripMenuItem FileCloseAllMenu;
		private System.Windows.Forms.ToolStripMenuItem FileExportMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem AppExitMenu;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewReloadMenu;
		private System.Windows.Forms.ToolStripMenuItem ViewClearMenu;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HelpAboutMenu;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Timer LiveTimer;
		private System.Windows.Forms.Panel DropPanel;
		private System.Windows.Forms.ToolStripMenuItem BackToTopMenu;
		private System.Windows.Forms.ToolStripMenuItem LiveMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem TagJumpMenu;
		private System.Windows.Forms.ToolStripMenuItem ShowInExplorerMenu;
	}
}

