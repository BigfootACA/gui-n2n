namespace GuiN2N {
	partial class AddSuperNode {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSuperNode));
			this.lblTips = new System.Windows.Forms.Label();
			this.txtHost = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtPort = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.txtPort)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTips
			// 
			this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTips.AutoSize = true;
			this.lblTips.Location = new System.Drawing.Point(76, 9);
			this.lblTips.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTips.Name = "lblTips";
			this.lblTips.Size = new System.Drawing.Size(116, 17);
			this.lblTips.TabIndex = 5;
			this.lblTips.Text = "输入超级节点的地址";
			// 
			// txtHost
			// 
			this.txtHost.Location = new System.Drawing.Point(13, 35);
			this.txtHost.Margin = new System.Windows.Forms.Padding(4);
			this.txtHost.Name = "txtHost";
			this.txtHost.Size = new System.Drawing.Size(178, 23);
			this.txtHost.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(38, 66);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 30);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(160, 66);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(199, 35);
			this.txtPort.Margin = new System.Windows.Forms.Padding(4);
			this.txtPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.txtPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(61, 23);
			this.txtPort.TabIndex = 2;
			this.txtPort.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
			// 
			// AddSuperNode
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(273, 108);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtHost);
			this.Controls.Add(this.lblTips);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddSuperNode";
			this.ShowInTaskbar = false;
			this.Text = "添加超级节点";
			((System.ComponentModel.ISupportInitialize)(this.txtPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTips;
		public System.Windows.Forms.TextBox txtHost;
		public System.Windows.Forms.NumericUpDown txtPort;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}