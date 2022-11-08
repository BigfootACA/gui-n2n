namespace GuiN2N {
	partial class AdapterManager {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing&&(components!=null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdapterManager));
			this.list = new System.Windows.Forms.ListView();
			this.itemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemIPv4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.menuAdapterContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.menuAddAdapter = new System.Windows.Forms.ToolStripMenuItem();
			this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.menuInstallDriver = new System.Windows.Forms.ToolStripMenuItem();
			this.menuControlPanel = new System.Windows.Forms.ToolStripMenuItem();
			this.menuAdapterContext.SuspendLayout();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemName,
            this.itemStatus,
            this.itemMAC,
            this.itemIPv4,
            this.itemGUID});
			this.list.ContextMenuStrip = this.menuAdapterContext;
			this.list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.list.FullRowSelect = true;
			this.list.GridLines = true;
			this.list.HideSelection = false;
			this.list.Location = new System.Drawing.Point(0, 25);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(709, 336);
			this.list.TabIndex = 0;
			this.list.UseCompatibleStateImageBehavior = false;
			this.list.View = System.Windows.Forms.View.Details;
			// 
			// itemName
			// 
			this.itemName.Text = "名称";
			this.itemName.Width = 150;
			// 
			// itemStatus
			// 
			this.itemStatus.Text = "状态";
			this.itemStatus.Width = 50;
			// 
			// itemMAC
			// 
			this.itemMAC.Text = "MAC地址";
			this.itemMAC.Width = 120;
			// 
			// itemIPv4
			// 
			this.itemIPv4.Text = "IPv4地址";
			this.itemIPv4.Width = 130;
			// 
			// itemGUID
			// 
			this.itemGUID.Text = "GUID";
			this.itemGUID.Width = 230;
			// 
			// menuAdapterContext
			// 
			this.menuAdapterContext.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuAdapterContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDelete});
			this.menuAdapterContext.Name = "contextMenuStrip1";
			this.menuAdapterContext.Size = new System.Drawing.Size(101, 26);
			this.menuAdapterContext.Text = "菜单";
			this.menuAdapterContext.Opening += new System.ComponentModel.CancelEventHandler(this.menuAdapterContext_Opening);
			// 
			// menuDelete
			// 
			this.menuDelete.Name = "menuDelete";
			this.menuDelete.Size = new System.Drawing.Size(100, 22);
			this.menuDelete.Text = "删除";
			this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
			// 
			// menu
			// 
			this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddAdapter,
            this.menuRefresh,
            this.menuInstallDriver,
            this.menuControlPanel});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(709, 25);
			this.menu.TabIndex = 1;
			this.menu.Text = "菜单";
			// 
			// menuAddAdapter
			// 
			this.menuAddAdapter.Name = "menuAddAdapter";
			this.menuAddAdapter.Size = new System.Drawing.Size(44, 21);
			this.menuAddAdapter.Text = "添加";
			this.menuAddAdapter.Click += new System.EventHandler(this.menuAddAdapter_Click);
			// 
			// menuRefresh
			// 
			this.menuRefresh.Name = "menuRefresh";
			this.menuRefresh.Size = new System.Drawing.Size(44, 21);
			this.menuRefresh.Text = "刷新";
			this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
			// 
			// menuInstallDriver
			// 
			this.menuInstallDriver.Name = "menuInstallDriver";
			this.menuInstallDriver.Size = new System.Drawing.Size(68, 21);
			this.menuInstallDriver.Text = "安装驱动";
			this.menuInstallDriver.Click += new System.EventHandler(this.menuInstallDriver_Click);
			// 
			// menuControlPanel
			// 
			this.menuControlPanel.Name = "menuControlPanel";
			this.menuControlPanel.Size = new System.Drawing.Size(68, 21);
			this.menuControlPanel.Text = "控制面板";
			this.menuControlPanel.Click += new System.EventHandler(this.menuControlPanel_Click);
			// 
			// AdapterManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(709, 361);
			this.Controls.Add(this.list);
			this.Controls.Add(this.menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menu;
			this.Name = "AdapterManager";
			this.Text = "TAP网卡管理";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdapterManager_FormClosing);
			this.Load += new System.EventHandler(this.AdapterManager_Load);
			this.menuAdapterContext.ResumeLayout(false);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView list;
		private System.Windows.Forms.MenuStrip menu;
		private System.Windows.Forms.ColumnHeader itemName;
		private System.Windows.Forms.ColumnHeader itemStatus;
		private System.Windows.Forms.ColumnHeader itemIPv4;
		private System.Windows.Forms.ColumnHeader itemGUID;
		private System.Windows.Forms.ToolStripMenuItem menuAddAdapter;
		private System.Windows.Forms.ToolStripMenuItem menuRefresh;
		private System.Windows.Forms.ToolStripMenuItem menuInstallDriver;
		private System.Windows.Forms.ToolStripMenuItem menuControlPanel;
		private System.Windows.Forms.ColumnHeader itemMAC;
		private System.Windows.Forms.ContextMenuStrip menuAdapterContext;
		private System.Windows.Forms.ToolStripMenuItem menuDelete;
	}
}