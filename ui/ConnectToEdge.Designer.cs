namespace GuiN2N {
	partial class ConnectToEdge {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectToEdge));
			this.lblConnectTip = new System.Windows.Forms.Label();
			this.lblManagementAddress = new System.Windows.Forms.Label();
			this.lblManagementPassword = new System.Windows.Forms.Label();
			this.txtManagementAddress = new System.Windows.Forms.TextBox();
			this.txtManagementPort = new System.Windows.Forms.NumericUpDown();
			this.txtManagementPassword = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnManagementPasswordShow = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.txtManagementPort)).BeginInit();
			this.SuspendLayout();
			// 
			// lblConnectTip
			// 
			this.lblConnectTip.AutoSize = true;
			this.lblConnectTip.Location = new System.Drawing.Point(64, 9);
			this.lblConnectTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblConnectTip.Name = "lblConnectTip";
			this.lblConnectTip.Size = new System.Drawing.Size(146, 17);
			this.lblConnectTip.TabIndex = 9;
			this.lblConnectTip.Text = "手动连接到已启动的Edge";
			// 
			// lblManagementAddress
			// 
			this.lblManagementAddress.AutoSize = true;
			this.lblManagementAddress.Location = new System.Drawing.Point(13, 36);
			this.lblManagementAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblManagementAddress.Name = "lblManagementAddress";
			this.lblManagementAddress.Size = new System.Drawing.Size(56, 17);
			this.lblManagementAddress.TabIndex = 8;
			this.lblManagementAddress.Text = "管理地址";
			// 
			// lblManagementPassword
			// 
			this.lblManagementPassword.AutoSize = true;
			this.lblManagementPassword.Location = new System.Drawing.Point(13, 65);
			this.lblManagementPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblManagementPassword.Name = "lblManagementPassword";
			this.lblManagementPassword.Size = new System.Drawing.Size(56, 17);
			this.lblManagementPassword.TabIndex = 7;
			this.lblManagementPassword.Text = "管理密码";
			// 
			// txtManagementAddress
			// 
			this.txtManagementAddress.Location = new System.Drawing.Point(76, 33);
			this.txtManagementAddress.Name = "txtManagementAddress";
			this.txtManagementAddress.Size = new System.Drawing.Size(125, 23);
			this.txtManagementAddress.TabIndex = 1;
			this.txtManagementAddress.Text = "127.0.0.1";
			// 
			// txtManagementPort
			// 
			this.txtManagementPort.Location = new System.Drawing.Point(207, 33);
			this.txtManagementPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.txtManagementPort.Name = "txtManagementPort";
			this.txtManagementPort.Size = new System.Drawing.Size(57, 23);
			this.txtManagementPort.TabIndex = 2;
			this.txtManagementPort.Value = new decimal(new int[] {
            5644,
            0,
            0,
            0});
			// 
			// txtManagementPassword
			// 
			this.txtManagementPassword.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtManagementPassword.Location = new System.Drawing.Point(76, 62);
			this.txtManagementPassword.Name = "txtManagementPassword";
			this.txtManagementPassword.Size = new System.Drawing.Size(137, 23);
			this.txtManagementPassword.TabIndex = 3;
			this.txtManagementPassword.Text = "n2n";
			this.txtManagementPassword.TextChanged += new System.EventHandler(this.txtManagementPassword_TextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(150, 91);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(45, 91);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 30);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnManagementPasswordShow
			// 
			this.btnManagementPasswordShow.Location = new System.Drawing.Point(219, 61);
			this.btnManagementPasswordShow.Name = "btnManagementPasswordShow";
			this.btnManagementPasswordShow.Size = new System.Drawing.Size(45, 25);
			this.btnManagementPasswordShow.TabIndex = 4;
			this.btnManagementPasswordShow.Text = "隐藏";
			this.btnManagementPasswordShow.UseVisualStyleBackColor = true;
			this.btnManagementPasswordShow.Click += new System.EventHandler(this.btnManagementPasswordShow_Click);
			// 
			// ConnectToEdge
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(276, 131);
			this.Controls.Add(this.btnManagementPasswordShow);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtManagementPassword);
			this.Controls.Add(this.txtManagementPort);
			this.Controls.Add(this.txtManagementAddress);
			this.Controls.Add(this.lblManagementPassword);
			this.Controls.Add(this.lblManagementAddress);
			this.Controls.Add(this.lblConnectTip);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectToEdge";
			this.Text = "连接到Edge";
			((System.ComponentModel.ISupportInitialize)(this.txtManagementPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblConnectTip;
		private System.Windows.Forms.Label lblManagementAddress;
		public System.Windows.Forms.TextBox txtManagementAddress;
		public System.Windows.Forms.NumericUpDown txtManagementPort;
		private System.Windows.Forms.Label lblManagementPassword;
		public System.Windows.Forms.TextBox txtManagementPassword;
		private System.Windows.Forms.Button btnManagementPasswordShow;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}