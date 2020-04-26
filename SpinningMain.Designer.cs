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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AppNewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseAllMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileExportMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.AppExitMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ExportHtmlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewHomeMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.LiveMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewClearMenu = new System.Windows.Forms.ToolStripMenuItem();
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
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.HighlightsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FilteringMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.HighlightsText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
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
            this.AppNewMenu,
            this.FileOpenMenu,
            this.FileCloseMenu,
            this.FileCloseAllMenu,
            this.FileExportMenu,
            this.toolStripMenuItem1,
            this.AppExitMenu,
            this.ExportHtmlMenu});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// AppNewMenu
			// 
			this.AppNewMenu.Name = "AppNewMenu";
			this.AppNewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.AppNewMenu.Size = new System.Drawing.Size(180, 22);
			this.AppNewMenu.Text = "&New ...";
			this.AppNewMenu.ToolTipText = "SpinningLogをもう一つ開く";
			this.AppNewMenu.Click += new System.EventHandler(this.AppNewMenu_Click);
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
			// ExportHtmlMenu
			// 
			this.ExportHtmlMenu.ForeColor = System.Drawing.Color.Maroon;
			this.ExportHtmlMenu.Name = "ExportHtmlMenu";
			this.ExportHtmlMenu.Size = new System.Drawing.Size(180, 22);
			this.ExportHtmlMenu.Text = "debug: export html";
			this.ExportHtmlMenu.Visible = false;
			this.ExportHtmlMenu.Click += new System.EventHandler(this.ExportHtmlMenu_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findTextToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.copyToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// findTextToolStripMenuItem
			// 
			this.findTextToolStripMenuItem.Enabled = false;
			this.findTextToolStripMenuItem.Name = "findTextToolStripMenuItem";
			this.findTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.findTextToolStripMenuItem.Text = "&Find Text";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Enabled = false;
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Enabled = false;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewHomeMenu,
            this.LiveMenu,
            this.HighlightsMenu,
            this.FilteringMenu,
            this.ViewReloadMenu,
            this.ViewClearMenu,
            this.toolStripMenuItem2,
            this.TagJumpMenu,
            this.ShowInExplorerMenu});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// ViewHomeMenu
			// 
			this.ViewHomeMenu.Name = "ViewHomeMenu";
			this.ViewHomeMenu.ShortcutKeyDisplayString = "HOME";
			this.ViewHomeMenu.Size = new System.Drawing.Size(180, 22);
			this.ViewHomeMenu.Tag = "";
			this.ViewHomeMenu.Text = "Back to &Top";
			this.ViewHomeMenu.ToolTipText = "先頭行にスクロールして、 LIVE 表示を停止";
			this.ViewHomeMenu.Click += new System.EventHandler(this.ViewHomeMenu_Click);
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
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
			// 
			// TagJumpMenu
			// 
			this.TagJumpMenu.Name = "TagJumpMenu";
			this.TagJumpMenu.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.TagJumpMenu.Size = new System.Drawing.Size(180, 22);
			this.TagJumpMenu.Text = "Tag &Jump ...";
			this.TagJumpMenu.Click += new System.EventHandler(this.TagJumpMenu_Click);
			// 
			// ShowInExplorerMenu
			// 
			this.ShowInExplorerMenu.Name = "ShowInExplorerMenu";
			this.ShowInExplorerMenu.ShortcutKeys = System.Windows.Forms.Keys.F10;
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
			// HighlightsMenu
			// 
			this.HighlightsMenu.Name = "HighlightsMenu";
			this.HighlightsMenu.Size = new System.Drawing.Size(180, 22);
			this.HighlightsMenu.Text = "Highlights";
			this.HighlightsMenu.Click += new System.EventHandler(this.HighlightsMenu_Click);
			// 
			// FilteringMenu
			// 
			this.FilteringMenu.Name = "FilteringMenu";
			this.FilteringMenu.Size = new System.Drawing.Size(180, 22);
			this.FilteringMenu.Text = "Filtering";
			this.FilteringMenu.Click += new System.EventHandler(this.FilteringMenu_Click);
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
			// HighlightsText
			// 
			this.HighlightsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HighlightsText.Location = new System.Drawing.Point(333, 0);
			this.HighlightsText.Name = "HighlightsText";
			this.HighlightsText.Size = new System.Drawing.Size(400, 23);
			this.HighlightsText.TabIndex = 4;
			this.HighlightsText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HighlightsText_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(267, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 15);
			this.label1.TabIndex = 5;
			this.label1.Text = "highlights";
			// 
			// ApplyButton
			// 
			this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ApplyButton.Location = new System.Drawing.Point(739, 0);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(58, 23);
			this.ApplyButton.TabIndex = 6;
			this.ApplyButton.Text = "apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
			// 
			// SpinningMain
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(800, 424);
			this.Controls.Add(this.ApplyButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.HighlightsText);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.DropPanel);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(500, 120);
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
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AppNewMenu;
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
		private System.Windows.Forms.ToolStripMenuItem ViewHomeMenu;
		private System.Windows.Forms.ToolStripMenuItem LiveMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem TagJumpMenu;
		private System.Windows.Forms.ToolStripMenuItem ShowInExplorerMenu;
		private System.Windows.Forms.ToolStripMenuItem findTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExportHtmlMenu;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.ToolStripMenuItem HighlightsMenu;
		private System.Windows.Forms.ToolStripMenuItem FilteringMenu;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.TextBox HighlightsText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ApplyButton;
	}
}

