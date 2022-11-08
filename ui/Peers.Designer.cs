namespace GuiN2N {
	partial class Peers {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Peers));
			this.list = new System.Windows.Forms.ListView();
			this.itemIPAddresss = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemMACAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemHostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemRemoteAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemDelay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.menu = new System.Windows.Forms.MenuStrip();
			this.menuClean = new System.Windows.Forms.ToolStripMenuItem();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemIPAddresss,
            this.itemMACAddress,
            this.itemHostName,
            this.itemRemoteAddress,
            this.itemMode,
            this.itemDelay});
			this.list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.list.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.list.FullRowSelect = true;
			this.list.GridLines = true;
			this.list.HideSelection = false;
			this.list.Location = new System.Drawing.Point(0, 25);
			this.list.MultiSelect = false;
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(664, 386);
			this.list.TabIndex = 1;
			this.list.UseCompatibleStateImageBehavior = false;
			this.list.View = System.Windows.Forms.View.Details;
			// 
			// itemIPAddresss
			// 
			this.itemIPAddresss.Text = "IP地址";
			this.itemIPAddresss.Width = 120;
			// 
			// itemMACAddress
			// 
			this.itemMACAddress.Text = "MAC地址";
			this.itemMACAddress.Width = 120;
			// 
			// itemHostName
			// 
			this.itemHostName.Text = "主机名";
			this.itemHostName.Width = 140;
			// 
			// itemRemoteAddress
			// 
			this.itemRemoteAddress.Text = "远端地址";
			this.itemRemoteAddress.Width = 150;
			// 
			// itemMode
			// 
			this.itemMode.Text = "模式";
			this.itemMode.Width = 40;
			// 
			// itemDelay
			// 
			this.itemDelay.Text = "延迟";
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuClean});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(664, 25);
			this.menu.TabIndex = 2;
			this.menu.Text = "菜单";
			// 
			// menuClean
			// 
			this.menuClean.Name = "menuClean";
			this.menuClean.Size = new System.Drawing.Size(44, 21);
			this.menuClean.Text = "清空";
			this.menuClean.Click += new System.EventHandler(this.menuClean_Click);
			// 
			// Peers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(664, 411);
			this.Controls.Add(this.list);
			this.Controls.Add(this.menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menu;
			this.Name = "Peers";
			this.Text = "主机列表";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Peers_FormClosing);
			this.Load += new System.EventHandler(this.Peers_Load);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ListView list;
		private System.Windows.Forms.ColumnHeader itemIPAddresss;
		private System.Windows.Forms.ColumnHeader itemMACAddress;
		private System.Windows.Forms.ColumnHeader itemHostName;
		private System.Windows.Forms.ColumnHeader itemRemoteAddress;
		private System.Windows.Forms.ColumnHeader itemMode;
		private System.Windows.Forms.ColumnHeader itemDelay;
		private System.Windows.Forms.MenuStrip menu;
		private System.Windows.Forms.ToolStripMenuItem menuClean;
	}
}