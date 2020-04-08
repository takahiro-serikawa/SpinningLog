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
			this.ViewRefreshMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewClearMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpAboutMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.LiveCheck = new System.Windows.Forms.CheckBox();
			this.LiveTimer = new System.Windows.Forms.Timer(this.components);
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
			this.FileNewMenu.Click += new System.EventHandler(this.FileNewMenu_Click);
			// 
			// FileOpenMenu
			// 
			this.FileOpenMenu.Name = "FileOpenMenu";
			this.FileOpenMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.FileOpenMenu.Size = new System.Drawing.Size(180, 22);
			this.FileOpenMenu.Text = "&Open ...";
			this.FileOpenMenu.Click += new System.EventHandler(this.FileOpenMenu_Click);
			// 
			// FileCloseMenu
			// 
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
			this.FileCloseAllMenu.Click += new System.EventHandler(this.FileCloseAllMenu_Click);
			// 
			// FileExportMenu
			// 
			this.FileExportMenu.Name = "FileExportMenu";
			this.FileExportMenu.Size = new System.Drawing.Size(180, 22);
			this.FileExportMenu.Text = "Export ...";
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
            this.ViewRefreshMenu,
            this.ViewClearMenu});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// ViewRefreshMenu
			// 
			this.ViewRefreshMenu.Name = "ViewRefreshMenu";
			this.ViewRefreshMenu.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.ViewRefreshMenu.Size = new System.Drawing.Size(180, 22);
			this.ViewRefreshMenu.Text = "&Refresh";
			this.ViewRefreshMenu.Click += new System.EventHandler(this.ViewRefreshMenu_Click);
			// 
			// ViewClearMenu
			// 
			this.ViewClearMenu.Name = "ViewClearMenu";
			this.ViewClearMenu.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.ViewClearMenu.Size = new System.Drawing.Size(180, 22);
			this.ViewClearMenu.Text = "&Clear";
			this.ViewClearMenu.Click += new System.EventHandler(this.ViewClearMenu_Click);
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
			this.HelpAboutMenu.Name = "HelpAboutMenu";
			this.HelpAboutMenu.Size = new System.Drawing.Size(180, 22);
			this.HelpAboutMenu.Text = "&About";
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
			// LiveCheck
			// 
			this.LiveCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LiveCheck.AutoSize = true;
			this.LiveCheck.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.LiveCheck.Location = new System.Drawing.Point(723, 4);
			this.LiveCheck.Name = "LiveCheck";
			this.LiveCheck.Size = new System.Drawing.Size(48, 19);
			this.LiveCheck.TabIndex = 2;
			this.LiveCheck.Text = "LIVE";
			this.LiveCheck.UseVisualStyleBackColor = false;
			this.LiveCheck.CheckedChanged += new System.EventHandler(this.LiveCheck_CheckedChanged);
			// 
			// LiveTimer
			// 
			this.LiveTimer.Enabled = true;
			this.LiveTimer.Interval = 1000;
			this.LiveTimer.Tick += new System.EventHandler(this.LiveTimer_Tick);
			// 
			// SpinningMain
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(800, 424);
			this.Controls.Add(this.LiveCheck);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
			this.MainMenuStrip = this.menuStrip1;
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
		private System.Windows.Forms.ToolStripMenuItem ViewRefreshMenu;
		private System.Windows.Forms.ToolStripMenuItem ViewClearMenu;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HelpAboutMenu;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.CheckBox LiveCheck;
		private System.Windows.Forms.Timer LiveTimer;
	}
}

