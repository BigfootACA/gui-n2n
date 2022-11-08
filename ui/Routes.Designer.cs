namespace GuiN2N {
	partial class Routes {
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "232.254.244.220",
            "32",
            "222.222.222.222"}, -1);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Routes));
			this.list = new System.Windows.Forms.ListView();
			this.itemNetwork = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemNextHop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.txtNetwork = new System.Windows.Forms.TextBox();
			this.txtPrefix = new System.Windows.Forms.NumericUpDown();
			this.txtNextHop = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.txtPrefix)).BeginInit();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemNetwork,
            this.itemPrefix,
            this.itemNextHop});
			this.list.Dock = System.Windows.Forms.DockStyle.Top;
			this.list.FullRowSelect = true;
			this.list.GridLines = true;
			this.list.HideSelection = false;
			this.list.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.list.Location = new System.Drawing.Point(0, 0);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(304, 169);
			this.list.TabIndex = 0;
			this.list.UseCompatibleStateImageBehavior = false;
			this.list.View = System.Windows.Forms.View.Details;
			this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
			// 
			// itemNetwork
			// 
			this.itemNetwork.Text = "目标网络/主机";
			this.itemNetwork.Width = 120;
			// 
			// itemPrefix
			// 
			this.itemPrefix.Text = "掩码";
			this.itemPrefix.Width = 40;
			// 
			// itemNextHop
			// 
			this.itemNextHop.Text = "下一条/网关";
			this.itemNextHop.Width = 120;
			// 
			// txtNetwork
			// 
			this.txtNetwork.Location = new System.Drawing.Point(0, 175);
			this.txtNetwork.Name = "txtNetwork";
			this.txtNetwork.Size = new System.Drawing.Size(110, 23);
			this.txtNetwork.TabIndex = 1;
			// 
			// txtPrefix
			// 
			this.txtPrefix.Location = new System.Drawing.Point(116, 175);
			this.txtPrefix.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.txtPrefix.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.Size = new System.Drawing.Size(50, 23);
			this.txtPrefix.TabIndex = 2;
			this.txtPrefix.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
			// 
			// txtNextHop
			// 
			this.txtNextHop.Location = new System.Drawing.Point(172, 174);
			this.txtNextHop.Name = "txtNextHop";
			this.txtNextHop.Size = new System.Drawing.Size(132, 23);
			this.txtNextHop.TabIndex = 3;
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(12, 204);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 30);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(114, 204);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 30);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "添加";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(217, 204);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 30);
			this.btnDelete.TabIndex = 6;
			this.btnDelete.Text = "删除";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// Routes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 241);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtNextHop);
			this.Controls.Add(this.txtPrefix);
			this.Controls.Add(this.txtNetwork);
			this.Controls.Add(this.list);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Routes";
			this.Text = "静态路由表";
			this.Load += new System.EventHandler(this.Routes_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtPrefix)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView list;
		private System.Windows.Forms.ColumnHeader itemNetwork;
		private System.Windows.Forms.ColumnHeader itemPrefix;
		private System.Windows.Forms.ColumnHeader itemNextHop;
		private System.Windows.Forms.TextBox txtNetwork;
		private System.Windows.Forms.NumericUpDown txtPrefix;
		private System.Windows.Forms.TextBox txtNextHop;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
	}
}