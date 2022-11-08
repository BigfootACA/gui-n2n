namespace GuiN2N {
	partial class EdgeLogs {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdgeLogs));
			this.text = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// text
			// 
			this.text.BackColor = System.Drawing.Color.Black;
			this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.text.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.text.ForeColor = System.Drawing.Color.White;
			this.text.Location = new System.Drawing.Point(0, 0);
			this.text.Margin = new System.Windows.Forms.Padding(4);
			this.text.Name = "text";
			this.text.ReadOnly = true;
			this.text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.text.Size = new System.Drawing.Size(884, 511);
			this.text.TabIndex = 0;
			this.text.Text = "";
			// 
			// EdgeLogs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 511);
			this.Controls.Add(this.text);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "EdgeLogs";
			this.Text = "边缘节点日志";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EdgeLogs_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.RichTextBox text;
	}
}