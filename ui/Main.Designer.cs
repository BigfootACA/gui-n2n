namespace GuiN2N
{
	partial class Main
	{
		private System.ComponentModel.IContainer components = null;
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) components.Dispose();
			base.Dispose(disposing);
		}
		#region Windows 窗体设计器生成的代码
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.notify = new System.Windows.Forms.NotifyIcon(this.components);
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.statusEdge = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusTx = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusRx = new System.Windows.Forms.ToolStripStatusLabel();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.menuAdvance = new System.Windows.Forms.ToolStripMenuItem();
			this.menuConnectToEdge = new System.Windows.Forms.ToolStripMenuItem();
			this.menuEdgeLogs = new System.Windows.Forms.ToolStripMenuItem();
			this.menuTapManager = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStartWith = new System.Windows.Forms.ToolStripMenuItem();
			this.menuPeersList = new System.Windows.Forms.ToolStripMenuItem();
			this.panel = new System.Windows.Forms.Panel();
			this.btnTapManager = new System.Windows.Forms.Button();
			this.btnRoutes = new System.Windows.Forms.Button();
			this.chkMulticast = new System.Windows.Forms.CheckBox();
			this.chkRouting = new System.Windows.Forms.CheckBox();
			this.chkMacAddressAuto = new System.Windows.Forms.CheckBox();
			this.btnMacAddressGenerate = new System.Windows.Forms.Button();
			this.lblMacAddress = new System.Windows.Forms.Label();
			this.txtMacAddress = new System.Windows.Forms.TextBox();
			this.chkAddressDHCP = new System.Windows.Forms.CheckBox();
			this.btnAddressGenerate = new System.Windows.Forms.Button();
			this.lblAddress = new System.Windows.Forms.Label();
			this.txtAddressPrefix = new System.Windows.Forms.NumericUpDown();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.btnSuperNodeDelete = new System.Windows.Forms.Button();
			this.btnSuperNodeEdit = new System.Windows.Forms.Button();
			this.txtPort = new System.Windows.Forms.NumericUpDown();
			this.chkPortAuto = new System.Windows.Forms.CheckBox();
			this.lblPort = new System.Windows.Forms.Label();
			this.btnSuperNodeAdd = new System.Windows.Forms.Button();
			this.lblSuperNode = new System.Windows.Forms.Label();
			this.lstSuperNode = new System.Windows.Forms.ListBox();
			this.btnKeyGenerate = new System.Windows.Forms.Button();
			this.btnKeyShow = new System.Windows.Forms.Button();
			this.txtKey = new System.Windows.Forms.TextBox();
			this.lblKey = new System.Windows.Forms.Label();
			this.txtCommunity = new System.Windows.Forms.TextBox();
			this.lblCommunity = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.comboVersions = new System.Windows.Forms.ComboBox();
			this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.notifyMenuShowMain = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuShowEdgeLogs = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuShowPeers = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuTapManager = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.notifyMenuStartWith = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuConnectToEdge = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.notifyMenuStop = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.statusBar.SuspendLayout();
			this.menu.SuspendLayout();
			this.panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtAddressPrefix)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPort)).BeginInit();
			this.notifyIconMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			resources.ApplyResources(this.btnStart, "btnStart");
			this.btnStart.Name = "btnStart";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			resources.ApplyResources(this.btnStop, "btnStop");
			this.btnStop.Name = "btnStop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnSave
			// 
			resources.ApplyResources(this.btnSave, "btnSave");
			this.btnSave.Name = "btnSave";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// notify
			// 
			resources.ApplyResources(this.notify, "notify");
			this.notify.Click += new System.EventHandler(this.notify_Click);
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusEdge,
            this.statusTx,
            this.statusRx});
			resources.ApplyResources(this.statusBar, "statusBar");
			this.statusBar.Name = "statusBar";
			// 
			// statusEdge
			// 
			this.statusEdge.Name = "statusEdge";
			resources.ApplyResources(this.statusEdge, "statusEdge");
			// 
			// statusTx
			// 
			this.statusTx.Name = "statusTx";
			resources.ApplyResources(this.statusTx, "statusTx");
			// 
			// statusRx
			// 
			this.statusRx.Name = "statusRx";
			resources.ApplyResources(this.statusRx, "statusRx");
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdvance,
            this.menuPeersList});
			resources.ApplyResources(this.menu, "menu");
			this.menu.Name = "menu";
			// 
			// menuAdvance
			// 
			this.menuAdvance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConnectToEdge,
            this.menuEdgeLogs,
            this.menuTapManager,
            this.menuStartWith});
			this.menuAdvance.Name = "menuAdvance";
			resources.ApplyResources(this.menuAdvance, "menuAdvance");
			// 
			// menuConnectToEdge
			// 
			this.menuConnectToEdge.Name = "menuConnectToEdge";
			resources.ApplyResources(this.menuConnectToEdge, "menuConnectToEdge");
			this.menuConnectToEdge.Click += new System.EventHandler(this.menuConnectToEdge_Click);
			// 
			// menuEdgeLogs
			// 
			this.menuEdgeLogs.Name = "menuEdgeLogs";
			resources.ApplyResources(this.menuEdgeLogs, "menuEdgeLogs");
			this.menuEdgeLogs.Click += new System.EventHandler(this.menuEdgeLogs_Click);
			// 
			// menuTapManager
			// 
			this.menuTapManager.Name = "menuTapManager";
			resources.ApplyResources(this.menuTapManager, "menuTapManager");
			this.menuTapManager.Click += new System.EventHandler(this.menuTapManager_Click);
			// 
			// menuStartWith
			// 
			this.menuStartWith.Name = "menuStartWith";
			resources.ApplyResources(this.menuStartWith, "menuStartWith");
			this.menuStartWith.Click += new System.EventHandler(this.menuStartWith_Click);
			// 
			// menuPeersList
			// 
			this.menuPeersList.Name = "menuPeersList";
			resources.ApplyResources(this.menuPeersList, "menuPeersList");
			this.menuPeersList.Click += new System.EventHandler(this.menuPeersList_Click);
			// 
			// panel
			// 
			this.panel.Controls.Add(this.btnTapManager);
			this.panel.Controls.Add(this.btnRoutes);
			this.panel.Controls.Add(this.chkMulticast);
			this.panel.Controls.Add(this.chkRouting);
			this.panel.Controls.Add(this.chkMacAddressAuto);
			this.panel.Controls.Add(this.btnMacAddressGenerate);
			this.panel.Controls.Add(this.lblMacAddress);
			this.panel.Controls.Add(this.txtMacAddress);
			this.panel.Controls.Add(this.chkAddressDHCP);
			this.panel.Controls.Add(this.btnAddressGenerate);
			this.panel.Controls.Add(this.lblAddress);
			this.panel.Controls.Add(this.txtAddressPrefix);
			this.panel.Controls.Add(this.txtAddress);
			this.panel.Controls.Add(this.btnSuperNodeDelete);
			this.panel.Controls.Add(this.btnSuperNodeEdit);
			this.panel.Controls.Add(this.txtPort);
			this.panel.Controls.Add(this.chkPortAuto);
			this.panel.Controls.Add(this.lblPort);
			this.panel.Controls.Add(this.btnSuperNodeAdd);
			this.panel.Controls.Add(this.lblSuperNode);
			this.panel.Controls.Add(this.lstSuperNode);
			this.panel.Controls.Add(this.btnKeyGenerate);
			this.panel.Controls.Add(this.btnKeyShow);
			this.panel.Controls.Add(this.txtKey);
			this.panel.Controls.Add(this.lblKey);
			this.panel.Controls.Add(this.txtCommunity);
			this.panel.Controls.Add(this.lblCommunity);
			this.panel.Controls.Add(this.lblVersion);
			this.panel.Controls.Add(this.comboVersions);
			resources.ApplyResources(this.panel, "panel");
			this.panel.Name = "panel";
			// 
			// btnTapManager
			// 
			resources.ApplyResources(this.btnTapManager, "btnTapManager");
			this.btnTapManager.Name = "btnTapManager";
			this.btnTapManager.UseVisualStyleBackColor = true;
			this.btnTapManager.Click += new System.EventHandler(this.btnTapManager_Click);
			// 
			// btnRoutes
			// 
			resources.ApplyResources(this.btnRoutes, "btnRoutes");
			this.btnRoutes.Name = "btnRoutes";
			this.btnRoutes.UseVisualStyleBackColor = true;
			this.btnRoutes.Click += new System.EventHandler(this.btnRoutes_Click);
			// 
			// chkMulticast
			// 
			resources.ApplyResources(this.chkMulticast, "chkMulticast");
			this.chkMulticast.Name = "chkMulticast";
			this.chkMulticast.UseVisualStyleBackColor = true;
			// 
			// chkRouting
			// 
			resources.ApplyResources(this.chkRouting, "chkRouting");
			this.chkRouting.Name = "chkRouting";
			this.chkRouting.UseVisualStyleBackColor = true;
			// 
			// chkMacAddressAuto
			// 
			resources.ApplyResources(this.chkMacAddressAuto, "chkMacAddressAuto");
			this.chkMacAddressAuto.Name = "chkMacAddressAuto";
			this.chkMacAddressAuto.UseVisualStyleBackColor = true;
			this.chkMacAddressAuto.CheckedChanged += new System.EventHandler(this.chkMacAddressAuto_CheckedChanged);
			// 
			// btnMacAddressGenerate
			// 
			resources.ApplyResources(this.btnMacAddressGenerate, "btnMacAddressGenerate");
			this.btnMacAddressGenerate.Name = "btnMacAddressGenerate";
			this.btnMacAddressGenerate.UseVisualStyleBackColor = true;
			this.btnMacAddressGenerate.Click += new System.EventHandler(this.btnMacAddressGenerate_Click);
			// 
			// lblMacAddress
			// 
			resources.ApplyResources(this.lblMacAddress, "lblMacAddress");
			this.lblMacAddress.Name = "lblMacAddress";
			// 
			// txtMacAddress
			// 
			resources.ApplyResources(this.txtMacAddress, "txtMacAddress");
			this.txtMacAddress.Name = "txtMacAddress";
			// 
			// chkAddressDHCP
			// 
			resources.ApplyResources(this.chkAddressDHCP, "chkAddressDHCP");
			this.chkAddressDHCP.Name = "chkAddressDHCP";
			this.chkAddressDHCP.UseVisualStyleBackColor = true;
			this.chkAddressDHCP.CheckedChanged += new System.EventHandler(this.chkAddressDHCP_CheckedChanged);
			// 
			// btnAddressGenerate
			// 
			resources.ApplyResources(this.btnAddressGenerate, "btnAddressGenerate");
			this.btnAddressGenerate.Name = "btnAddressGenerate";
			this.btnAddressGenerate.UseVisualStyleBackColor = true;
			this.btnAddressGenerate.Click += new System.EventHandler(this.btnAddressGenerate_Click);
			// 
			// lblAddress
			// 
			resources.ApplyResources(this.lblAddress, "lblAddress");
			this.lblAddress.Name = "lblAddress";
			// 
			// txtAddressPrefix
			// 
			resources.ApplyResources(this.txtAddressPrefix, "txtAddressPrefix");
			this.txtAddressPrefix.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.txtAddressPrefix.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtAddressPrefix.Name = "txtAddressPrefix";
			this.txtAddressPrefix.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
			// 
			// txtAddress
			// 
			resources.ApplyResources(this.txtAddress, "txtAddress");
			this.txtAddress.Name = "txtAddress";
			// 
			// btnSuperNodeDelete
			// 
			resources.ApplyResources(this.btnSuperNodeDelete, "btnSuperNodeDelete");
			this.btnSuperNodeDelete.Name = "btnSuperNodeDelete";
			this.btnSuperNodeDelete.UseVisualStyleBackColor = true;
			this.btnSuperNodeDelete.Click += new System.EventHandler(this.btnSuperNodeDelete_Click);
			// 
			// btnSuperNodeEdit
			// 
			resources.ApplyResources(this.btnSuperNodeEdit, "btnSuperNodeEdit");
			this.btnSuperNodeEdit.Name = "btnSuperNodeEdit";
			this.btnSuperNodeEdit.UseVisualStyleBackColor = true;
			this.btnSuperNodeEdit.Click += new System.EventHandler(this.btnSuperNodeEdit_Click);
			// 
			// txtPort
			// 
			resources.ApplyResources(this.txtPort, "txtPort");
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
			this.txtPort.Value = new decimal(new int[] {
            50001,
            0,
            0,
            0});
			// 
			// chkPortAuto
			// 
			resources.ApplyResources(this.chkPortAuto, "chkPortAuto");
			this.chkPortAuto.Checked = true;
			this.chkPortAuto.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPortAuto.Name = "chkPortAuto";
			this.chkPortAuto.UseVisualStyleBackColor = true;
			this.chkPortAuto.CheckedChanged += new System.EventHandler(this.chkPortAuto_CheckedChanged);
			// 
			// lblPort
			// 
			resources.ApplyResources(this.lblPort, "lblPort");
			this.lblPort.Name = "lblPort";
			// 
			// btnSuperNodeAdd
			// 
			resources.ApplyResources(this.btnSuperNodeAdd, "btnSuperNodeAdd");
			this.btnSuperNodeAdd.Name = "btnSuperNodeAdd";
			this.btnSuperNodeAdd.UseVisualStyleBackColor = true;
			this.btnSuperNodeAdd.Click += new System.EventHandler(this.btnSuperNodeAdd_Click);
			// 
			// lblSuperNode
			// 
			resources.ApplyResources(this.lblSuperNode, "lblSuperNode");
			this.lblSuperNode.Name = "lblSuperNode";
			// 
			// lstSuperNode
			// 
			this.lstSuperNode.FormattingEnabled = true;
			resources.ApplyResources(this.lstSuperNode, "lstSuperNode");
			this.lstSuperNode.Name = "lstSuperNode";
			this.lstSuperNode.SelectedIndexChanged += new System.EventHandler(this.lstSuperNode_SelectedIndexChanged);
			// 
			// btnKeyGenerate
			// 
			resources.ApplyResources(this.btnKeyGenerate, "btnKeyGenerate");
			this.btnKeyGenerate.Name = "btnKeyGenerate";
			this.btnKeyGenerate.UseVisualStyleBackColor = true;
			this.btnKeyGenerate.Click += new System.EventHandler(this.btnKeyGenerate_Click);
			// 
			// btnKeyShow
			// 
			resources.ApplyResources(this.btnKeyShow, "btnKeyShow");
			this.btnKeyShow.Name = "btnKeyShow";
			this.btnKeyShow.UseVisualStyleBackColor = true;
			this.btnKeyShow.Click += new System.EventHandler(this.btnKeyShow_Click);
			// 
			// txtKey
			// 
			resources.ApplyResources(this.txtKey, "txtKey");
			this.txtKey.Name = "txtKey";
			// 
			// lblKey
			// 
			resources.ApplyResources(this.lblKey, "lblKey");
			this.lblKey.Name = "lblKey";
			// 
			// txtCommunity
			// 
			resources.ApplyResources(this.txtCommunity, "txtCommunity");
			this.txtCommunity.Name = "txtCommunity";
			// 
			// lblCommunity
			// 
			resources.ApplyResources(this.lblCommunity, "lblCommunity");
			this.lblCommunity.Name = "lblCommunity";
			// 
			// lblVersion
			// 
			resources.ApplyResources(this.lblVersion, "lblVersion");
			this.lblVersion.Name = "lblVersion";
			// 
			// comboVersions
			// 
			this.comboVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboVersions.FormattingEnabled = true;
			resources.ApplyResources(this.comboVersions, "comboVersions");
			this.comboVersions.Name = "comboVersions";
			// 
			// notifyIconMenu
			// 
			this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyMenuShowMain,
            this.notifyMenuShowEdgeLogs,
            this.notifyMenuShowPeers,
            this.notifyMenuTapManager,
            this.notifyMenuSep1,
            this.notifyMenuStartWith,
            this.notifyMenuConnectToEdge,
            this.notifyMenuSep2,
            this.notifyMenuStop,
            this.notifyMenuExit});
			this.notifyIconMenu.Name = "notifyIconMenu";
			resources.ApplyResources(this.notifyIconMenu, "notifyIconMenu");
			// 
			// notifyMenuShowMain
			// 
			this.notifyMenuShowMain.Name = "notifyMenuShowMain";
			resources.ApplyResources(this.notifyMenuShowMain, "notifyMenuShowMain");
			this.notifyMenuShowMain.Click += new System.EventHandler(this.notifyMenuShowMain_Click);
			// 
			// notifyMenuShowEdgeLogs
			// 
			this.notifyMenuShowEdgeLogs.Name = "notifyMenuShowEdgeLogs";
			resources.ApplyResources(this.notifyMenuShowEdgeLogs, "notifyMenuShowEdgeLogs");
			this.notifyMenuShowEdgeLogs.Click += new System.EventHandler(this.notifyMenuShowEdgeLogs_Click);
			// 
			// notifyMenuShowPeers
			// 
			this.notifyMenuShowPeers.Name = "notifyMenuShowPeers";
			resources.ApplyResources(this.notifyMenuShowPeers, "notifyMenuShowPeers");
			this.notifyMenuShowPeers.Click += new System.EventHandler(this.notifyMenuShowPeers_Click);
			// 
			// notifyMenuTapManager
			// 
			this.notifyMenuTapManager.Name = "notifyMenuTapManager";
			resources.ApplyResources(this.notifyMenuTapManager, "notifyMenuTapManager");
			this.notifyMenuTapManager.Click += new System.EventHandler(this.notifyMenuTapManager_Click);
			// 
			// notifyMenuSep1
			// 
			this.notifyMenuSep1.Name = "notifyMenuSep1";
			resources.ApplyResources(this.notifyMenuSep1, "notifyMenuSep1");
			// 
			// notifyMenuStartWith
			// 
			this.notifyMenuStartWith.Name = "notifyMenuStartWith";
			resources.ApplyResources(this.notifyMenuStartWith, "notifyMenuStartWith");
			this.notifyMenuStartWith.Click += new System.EventHandler(this.notifyMenuStartWith_Click);
			// 
			// notifyMenuConnectToEdge
			// 
			this.notifyMenuConnectToEdge.Name = "notifyMenuConnectToEdge";
			resources.ApplyResources(this.notifyMenuConnectToEdge, "notifyMenuConnectToEdge");
			this.notifyMenuConnectToEdge.Click += new System.EventHandler(this.notifyMenuConnectToEdge_Click);
			// 
			// notifyMenuSep2
			// 
			this.notifyMenuSep2.Name = "notifyMenuSep2";
			resources.ApplyResources(this.notifyMenuSep2, "notifyMenuSep2");
			// 
			// notifyMenuStop
			// 
			this.notifyMenuStop.Name = "notifyMenuStop";
			resources.ApplyResources(this.notifyMenuStop, "notifyMenuStop");
			this.notifyMenuStop.Click += new System.EventHandler(this.notifyMenuStop_Click);
			// 
			// notifyMenuExit
			// 
			this.notifyMenuExit.Name = "notifyMenuExit";
			resources.ApplyResources(this.notifyMenuExit, "notifyMenuExit");
			this.notifyMenuExit.Click += new System.EventHandler(this.notifyMenuExit_Click);
			// 
			// Main
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.menu);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Main";
			this.Deactivate += new System.EventHandler(this.Main_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.panel.ResumeLayout(false);
			this.panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtAddressPrefix)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPort)).EndInit();
			this.notifyIconMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		internal System.Windows.Forms.Button btnStart;
		internal System.Windows.Forms.Button btnStop;
		internal System.Windows.Forms.Button btnSave;
		internal System.Windows.Forms.NotifyIcon notify;
		internal System.Windows.Forms.StatusStrip statusBar;
		internal System.Windows.Forms.ToolStripStatusLabel statusEdge;
		internal System.Windows.Forms.MenuStrip menu;
		internal System.Windows.Forms.ToolStripMenuItem menuAdvance;
		internal System.Windows.Forms.ToolStripMenuItem menuConnectToEdge;
		internal System.Windows.Forms.ToolStripMenuItem menuPeersList;
		private System.Windows.Forms.Panel panel;
		internal System.Windows.Forms.Button btnTapManager;
		internal System.Windows.Forms.Button btnRoutes;
		internal System.Windows.Forms.CheckBox chkMulticast;
		internal System.Windows.Forms.CheckBox chkRouting;
		internal System.Windows.Forms.CheckBox chkMacAddressAuto;
		internal System.Windows.Forms.Button btnMacAddressGenerate;
		internal System.Windows.Forms.Label lblMacAddress;
		internal System.Windows.Forms.TextBox txtMacAddress;
		internal System.Windows.Forms.CheckBox chkAddressDHCP;
		internal System.Windows.Forms.Button btnAddressGenerate;
		internal System.Windows.Forms.Label lblAddress;
		internal System.Windows.Forms.NumericUpDown txtAddressPrefix;
		internal System.Windows.Forms.TextBox txtAddress;
		internal System.Windows.Forms.Button btnSuperNodeDelete;
		internal System.Windows.Forms.Button btnSuperNodeEdit;
		internal System.Windows.Forms.NumericUpDown txtPort;
		internal System.Windows.Forms.CheckBox chkPortAuto;
		internal System.Windows.Forms.Label lblPort;
		internal System.Windows.Forms.Button btnSuperNodeAdd;
		internal System.Windows.Forms.Label lblSuperNode;
		internal System.Windows.Forms.ListBox lstSuperNode;
		internal System.Windows.Forms.Button btnKeyGenerate;
		internal System.Windows.Forms.Button btnKeyShow;
		internal System.Windows.Forms.TextBox txtKey;
		internal System.Windows.Forms.Label lblKey;
		internal System.Windows.Forms.TextBox txtCommunity;
		internal System.Windows.Forms.Label lblCommunity;
		internal System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.ComboBox comboVersions;
		private System.Windows.Forms.ToolStripStatusLabel statusTx;
		private System.Windows.Forms.ToolStripStatusLabel statusRx;
		private System.Windows.Forms.ToolStripMenuItem menuEdgeLogs;
		private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuShowMain;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuShowEdgeLogs;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuShowPeers;
		private System.Windows.Forms.ToolStripSeparator notifyMenuSep1;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuStartWith;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuConnectToEdge;
		private System.Windows.Forms.ToolStripSeparator notifyMenuSep2;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuStop;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuExit;
		private System.Windows.Forms.ToolStripMenuItem menuTapManager;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuTapManager;
		private System.Windows.Forms.ToolStripMenuItem menuStartWith;
	}
}
