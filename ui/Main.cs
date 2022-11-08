using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GuiN2N{
	public partial class Main:Form{
		internal Management mgmt=null;
		private Status status=Status.STATUS_NOT_STARTED;
		private static readonly string filter="配置文件 (*.conf)|*.conf|文本文档 (*.txt)|*.txt|所有文件 (*.*)|*.*";
		private readonly System.Windows.Forms.Timer timer;
		internal List<SuperNode>supernodes=new List<SuperNode>();
		internal List<Routes.Route>routes=new List<Routes.Route>();
		private readonly Peers peers;
		private readonly EdgeLogs logs;
		private readonly AdapterManager adapters;
		private Process edge=null;
		public Main(){
			InitializeComponent();
			logs=new EdgeLogs();
			peers=new Peers(this);
			adapters=new AdapterManager();
			timer=new System.Windows.Forms.Timer();
			Log.d("Handler of Peers: 0x{0:X}",peers.Handle);
			Log.d("Handler of EdgeLogs: 0x{0:X}",logs.Handle);
			Log.d("Handler of AdapterManager: 0x{0:X}",adapters.Handle);
		}
		private void LoadVersions(){
			comboVersions.Items.Clear();
			List<string>list=new List<string>();
			try{
				string doc=File.ReadAllText(Path.Combine(Utils.ResDir,"manifest.json"));
				JsonElement manifest=JsonDocument.Parse(doc).RootElement;
				JsonElement n2n=manifest.GetProperty("versions");
				JsonElement.ArrayEnumerator iter=n2n.EnumerateArray();
				while(iter.MoveNext()){
					JsonElement item=iter.Current;
					string version=item.GetString();
					if(!list.Contains(version))list.Add(version);
				}
				Log.d("found {0} versions",list.Count);
			}catch(Exception e){
				Log.e("load versions failed");
				Log.e(e);
				MessageBox.Show(
					string.Format("读取N2N版本失败: {0}",e.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				);
			}
			list.ForEach(v=>comboVersions.Items.Add(v));
			comboVersions.SelectedIndex=comboVersions.Items.Count-1;
		}
		private void Main_Load(object sender,EventArgs e){
			timer.Interval=2000;
			timer.Tick+=Timer_Tick;
			timer.Start();
			SetStatus(Status.STATUS_NOT_STARTED);
			LoadVersions();
			notify.Text=Text;
			notify.Icon=Icon;
			notify.ContextMenuStrip=notifyIconMenu;
			notify.Visible=true;
			Config.MainFields.LoadMainFields(this);
		}
		private void Main_FormClosing(object sender,FormClosingEventArgs e){
			if(e.CloseReason==CloseReason.UserClosing){
				e.Cancel=true;
				if(notify.Visible&&Visible)notify.ShowBalloonTip(
					0,"提示",
					"已隐藏到托盘图标",
					ToolTipIcon.Info
				);
				Hide();
			}
			Config.MainFields.SaveMainFields(this);
		}
		private void Main_Deactivate(object sender,EventArgs e)=>Config.MainFields.SaveMainFields(this);
		private void SetBytes(decimal rx,decimal tx){
			statusRx.Text=string.Format("接收: {0}",Utils.FormatSize(rx));
			statusTx.Text=string.Format("发送: {0}",Utils.FormatSize(tx));
		}
		private void UpdateBytes(){
			List<JsonElement>ret;
			try{
				ret=mgmt.ExecuteReadCommand("packetstats");
				foreach(JsonElement elem in ret){
					if(elem.GetProperty("type").GetString()!="transop")continue;
					decimal rx=elem.GetProperty("rx_pkt").GetDecimal();
					decimal tx=elem.GetProperty("tx_pkt").GetDecimal();
					Invoke(new Action(()=>SetBytes(rx,tx)));
				}
			}catch(Exception ex){
				Log.e("error while request packet stats");
				Log.e(ex);
			}
			if(timer!=null)try{Invoke(new Action(()=>timer.Start()));}catch(Exception){}
		}
		private void Timer_Tick(object sender,EventArgs e){
			if(mgmt==null)return;
			timer.Stop();
			Task.Run(UpdateBytes);
		}
		private void notify_Click(object sender,EventArgs e)=>notifyMenuShowMain_Click(sender,e);
		private void Management_Exited(object sender,Management.ExceptionEventArgs e){
			if(mgmt==null)return;
			mgmt=null;
			Log.d("management exited");
			if(e.Exception!=null)Log.w(e.Exception);
			Invoke(new Action(()=>{
				string str=null;
				switch(status){
					case Status.STATUS_STARTING:str="启动失败";break;
					case Status.STATUS_CONNECTING:str="连接失败";break;
					case Status.STATUS_STOPPING:str="停止失败";break;
					default:return;
				}
				SetStatus(edge!=null&&!edge.HasExited?
					Status.STATUS_STOPPING:
					Status.STATUS_NOT_STARTED
				);
				if(e.Exception!=null)str=string.Format(
					"{0}: {1}",str,e.Exception.Message
				);
				if(str!=null)MessageBox.Show(
					str,"错误",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}));
		}
		private void Edge_Exited(object sender,EventArgs e){
			if(mgmt!=null)mgmt.Stop();
			mgmt=null;
			Log.i("process {0} exited with 0x{1:X}",edge.ProcessName,edge.ExitCode);
			Invoke(new Action(()=>{
				int code=edge.ExitCode;
				edge=null;
				SetStatus(Status.STATUS_NOT_STARTED);
				logs.LogsWrite(string.Format("[进程已退出。返回值: 0x{0:X}]",code),true,Color.Green);
				if(code!=0)MessageBox.Show(
					string.Format("进程异常退出, 返回值: 0x{0:X}",code),
					Text,MessageBoxButtons.OK,MessageBoxIcon.Exclamation
				);
			}));
		}
		private void Edge_OutputDataReceived(object sender,DataReceivedEventArgs e)=>Invoke(new Action(()=>{
			if(e.Data==null)return;
			Log.i("Edge: {0}",e.Data);
			logs.LogsWrite(e.Data,true,Color.White);
		}));
		private void Edge_ErrorDataReceived(object sender,DataReceivedEventArgs e)=>Invoke(new Action(()=>{
			if(e.Data==null)return;
			Log.w("Edge: {0}",e.Data);
			logs.LogsWrite(e.Data,true,Color.Red);
		}));
		private void chkAddressDHCP_CheckedChanged(object sender,EventArgs e){
			txtAddress.Enabled=!chkAddressDHCP.Checked;
			txtAddressPrefix.Enabled=!chkAddressDHCP.Checked;
			btnAddressGenerate.Enabled=!chkAddressDHCP.Checked;
			if(chkAddressDHCP.Checked)
				chkRouting.Checked=true;
		}
		private void chkMacAddressAuto_CheckedChanged(object sender,EventArgs e){
			txtMacAddress.Enabled=!chkMacAddressAuto.Checked;
			btnMacAddressGenerate.Enabled=!chkMacAddressAuto.Checked;
		}
		private void chkPortAuto_CheckedChanged(object sender,EventArgs e)=>txtPort.Enabled=!chkPortAuto.Checked;
		private void btnKeyShow_Click(object sender,EventArgs e){
			if(txtKey.PasswordChar==0){
				txtKey.PasswordChar='*';
				btnKeyShow.Text="显示";
			}else{
				txtKey.PasswordChar='\0';
				btnKeyShow.Text="隐藏";
			}
		}
		private void btnKeyGenerate_Click(object sender,EventArgs e){
			txtKey.Text=Utils.GetRandomBytes(16,string.Empty);
			if(txtKey.PasswordChar!=0)btnKeyShow_Click(sender,e);
		}
		private void btnMacAddressGenerate_Click(object sender,EventArgs e)=>txtMacAddress.Text=Utils.GetRandomMAC();
		private void btnAddressGenerate_Click(object sender,EventArgs e){
			if(txtAddress.Text.Length!=0)try{
				IPv4Address addr=new IPv4Address(txtAddress.Text,(byte)txtAddressPrefix.Value);
				txtAddress.Text=addr.RandomAddress().GetAddressString();
				return;
			}catch(Exception ex){Log.e(ex);}
			txtAddressPrefix.Value=24;
			txtAddress.Text=IPv4Address.RandomPrivateAddress(true).GetAddressString();
		}
		internal void ReloadSuperNodes(){
			lstSuperNode.Items.Clear();
			supernodes.ForEach(s=>lstSuperNode.Items.Add(s.ToString()));
			btnSuperNodeEdit.Enabled=false;
			btnSuperNodeDelete.Enabled=false;
		}
		private void lstSuperNode_SelectedIndexChanged(object sender,EventArgs e){
			bool selected=lstSuperNode.SelectedIndex!=-1;
			btnSuperNodeEdit.Enabled=selected;
			btnSuperNodeDelete.Enabled=selected;
		}
		private void btnSuperNodeAdd_Click(object sender,EventArgs e){
			using(AddSuperNode d=new AddSuperNode()){
				d.Text="添加超级节点";
				if(d.ShowDialog()==DialogResult.OK){
					supernodes.Add(d.node);
					ReloadSuperNodes();
				}
			}
		}
		private void btnSuperNodeEdit_Click(object sender,EventArgs e){
			int i=lstSuperNode.SelectedIndex;
			if(i==-1||lstSuperNode.Items.Count!=supernodes.Count)return;
			SuperNode node=supernodes[i];
			using(AddSuperNode d=new AddSuperNode(node)){
				d.Text="编辑超级节点";
				d.txtHost.SelectAll();
				if(d.ShowDialog()==DialogResult.OK)ReloadSuperNodes();
			}
		}
		private void btnSuperNodeDelete_Click(object sender,EventArgs e){
			int i=lstSuperNode.SelectedIndex;
			if(i==-1||lstSuperNode.Items.Count!=supernodes.Count)return;
			supernodes.RemoveAt(i);
			ReloadSuperNodes();
			lstSuperNode.SelectedIndex=Math.Min(i,supernodes.Count-1);
			lstSuperNode_SelectedIndexChanged(sender,e);
		}
		private void btnRoutes_Click(object sender,EventArgs e){
			using(Routes r=new Routes(routes))r.ShowDialog();
		}
		private void btnStart_Click(object sender,EventArgs e)=>CheckTryStart();
		private void btnStop_Click(object sender,EventArgs e)=>TryStop();
		private void btnTapManager_Click(object sender,EventArgs e)=>adapters.Show();
		private void notifyMenuShowMain_Click(object sender,EventArgs e){
			try{Show();Focus();}catch(Exception){}
		}
		private void notifyMenuExit_Click(object sender,EventArgs e){
			if(mgmt==null){
				Application.Exit();
				return;
			}
			if(status!=Status.STATUS_NOT_STARTED){
				DialogResult r=MessageBox.Show(
					"超级节点正在运行，确认要退出吗","退出",
					MessageBoxButtons.OKCancel,MessageBoxIcon.Question
				);
				if(r!=DialogResult.OK)return;
			}
			SetStatus(Status.STATUS_STOPPING);
			Task.Run(()=>{
				DoTryStop();
				Application.Exit();
			});
		}
		private void notifyMenuStop_Click(object sender,EventArgs e)=>btnStop_Click(sender,e);
		private void notifyMenuStartWith_Click(object sender,EventArgs e)=>menuStartWith_Click(sender,e);
		private void notifyMenuConnectToEdge_Click(object sender,EventArgs e)=>menuConnectToEdge_Click(sender,e);
		private void notifyMenuShowEdgeLogs_Click(object sender,EventArgs e)=>menuEdgeLogs_Click(sender,e);
		private void notifyMenuShowPeers_Click(object sender,EventArgs e)=>menuPeersList_Click(sender,e);
		private void notifyMenuTapManager_Click(object sender,EventArgs e)=>adapters.Show();
		private void menuTapManager_Click(object sender,EventArgs e)=>adapters.Show();
		private void menuPeersList_Click(object sender,EventArgs e){
			try{peers.Show();peers.Focus();}catch(Exception){}
		}
		private void menuEdgeLogs_Click(object sender,EventArgs e){
			try{logs.Show();logs.Focus();}catch(Exception){}
		}
		private void menuStartWith_Click(object sender,EventArgs e){
			string content,filename,version;
			if(!CheckVersion())return;
			version=comboVersions.SelectedItem.ToString();
			using(OpenFileDialog open=new OpenFileDialog()){
				open.Filter=filter;
				open.Title="选择配置文件";
				open.FileName="edge.conf";
				if(open.ShowDialog()!=DialogResult.OK)return;
				filename=open.FileName;
			}
			try{
				content=File.ReadAllText(filename);
				if(content==null||content.Length<=0)throw new ArgumentNullException();
				Log.d("loaded config {0} length {1}",filename,content.Length);
			}catch(Exception ex){
				Log.e("load config failed");
				Log.e(ex);
				MessageBox.Show(
					string.Format("加载配置文件失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				);
				return;
			}
			Task.Run(()=>DoCheckTryStart(version,content));
		}
		private void menuConnectToEdge_Click(object sender,EventArgs e){
			int port;
			string host,pass;
			if(mgmt!=null)return;
			using(ConnectToEdge conn=new ConnectToEdge()){
				if(conn.ShowDialog()!=DialogResult.OK)return;
				port=(int)conn.txtManagementPort.Value;
				pass=conn.txtManagementPassword.Text;
				host=conn.txtManagementAddress.Text;
			}
			if(host.Trim().Length<=0)return;
			if(pass.Length<=0)pass=null;
			mgmt=new Management(pass,host,port);
			mgmt.Exited+=Management_Exited;
			SetStatus(Status.STATUS_CONNECTING);
			Task.Run(()=>{
				try{
					mgmt.Start();
					Log.d("waiting edge up");
					mgmt.ExecuteReadCommand("stop");
					Invoke(new Action(()=>SetStatus(Status.STATUS_RUNNING)));
				}catch(Exception ex){
					Log.e("connect to edge failed");
					Log.e(ex);
					if(mgmt!=null)mgmt.Stop();
					mgmt=null;
					if(
						status!=Status.STATUS_STOPPING&&
						status!=Status.STATUS_NOT_STARTED
					)Invoke(new Action(()=>{
						MessageBox.Show(
							string.Format("连接失败: {0}",ex.Message),
							"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
						);
						SetStatus(Status.STATUS_NOT_STARTED);
					}));
				}
			});
		}
		private void DoTryStart(string version,string config=null){
			Invoke(new Action(()=>Config.MainFields.SaveMainFields(this)));
			if(mgmt!=null||edge!=null)return;
			Log.d("try start edge use version {0}",version);
			try{
				string cfg=null;
				string folder=Path.Combine(Utils.DataDir,"StartupConfig");
				if(!Directory.Exists(folder))Directory.CreateDirectory(folder);
				for(int i=0;i<256;i++){
					string name=Utils.GetRandomBytes(8,string.Empty)+".conf";
					cfg=Path.Combine(folder,name);
					if(!File.Exists(cfg))break;
				}
				if(File.Exists(cfg))throw new IOException("无法找到可用文件名");
				if(config==null)config=GenerateConfig();
				string fn=string.Format("edge-{0}.exe",version);
				string exe=Path.Combine(Utils.ResDir,"binary",fn);
				if(!File.Exists(exe))throw new FileNotFoundException(fn);
				Invoke(new Action(()=>SetStatus(Status.STATUS_STARTING)));
				int port=Utils.GetFreeUdpPort();
				if(port<=0)throw new IOException("获取空闲UDP端口失败");
				Log.d("select udp management port {0}",port);
				string key=Utils.GetRandomBytes(16,string.Empty);
				File.WriteAllText(cfg,GenerateStartupConfig(config,port,key));
				Log.d("try start with {0} argument {1}",exe,cfg);
				ProcessStartInfo info=new ProcessStartInfo{
					FileName=exe,
					Arguments=string.Format("\"{0}\"",cfg),
					CreateNoWindow=true,
					UseShellExecute=false,
					RedirectStandardOutput=true,
					RedirectStandardError=true,
				};
				edge=Process.Start(info);
				Log.d("edge process {0} pid {1}",edge.ProcessName,edge.Id);
				Invoke(new Action(()=>logs.LogsWrite(string.Format(
					"[进程已启动。PID: {0}]",edge.Id
				),true,Color.Green)));
				edge.EnableRaisingEvents=true;
				edge.Exited+=Edge_Exited;
				edge.BeginOutputReadLine();
				edge.BeginErrorReadLine();
				edge.OutputDataReceived+=Edge_OutputDataReceived;
				edge.ErrorDataReceived+=Edge_ErrorDataReceived;
				Invoke(new Action(()=>SetStatus(Status.STATUS_CONNECTING)));
				Thread.Sleep(3000);
				mgmt=new Management(key,IPAddress.Loopback,port);
				mgmt.Exited+=Management_Exited;
				mgmt.Start();
				Log.d("waiting edge up");
				mgmt.ExecuteReadCommand("stop");
				Invoke(new Action(()=>SetStatus(Status.STATUS_RUNNING)));
			}catch(Exception ex){
				Log.e("start edge failed");
				Log.e(ex);
				if(
					status!=Status.STATUS_STOPPING&&
					status!=Status.STATUS_NOT_STARTED
				)Invoke(new Action(()=>{
					MessageBox.Show(
						string.Format("启动失败: {0}",ex.Message),
						"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
					);
					SetStatus(Status.STATUS_NOT_STARTED);
				}));
				if(mgmt!=null)mgmt.Stop();
				mgmt=null;
				try{if(edge!=null&&!edge.HasExited)edge.Kill();}
				catch(Exception e2){Log.e(e2);}
				edge=null;
			}
		}
		private void DoTryStop(){
			try{
				Log.d("sending stop command");
				mgmt.ExecuteWriteCommand("stop");
				if(edge!=null){
					Log.d("waiting edge process exit");
					if(!edge.WaitForExit(20000))
						throw new IOException("等待退出超时");
					Log.d("edge process exited");
				}
				Invoke(new Action(()=>SetStatus(Status.STATUS_NOT_STARTED)));
				if(mgmt!=null)mgmt.Stop();
				mgmt=null;
				edge=null;
			}catch(Exception ex){
				Log.e("stop edge failed");
				Log.e(ex);
				if(status==Status.STATUS_STOPPING)Invoke(new Action(()=>{
					SetStatus(Status.STATUS_RUNNING);
					MessageBox.Show(
						string.Format("停止失败: {0}",ex.Message),
						"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
					);
				}));
			}
		}
		private void DoCheckTryStart(string version,string config=null){
			int count=1;
			DialogResult r=DialogResult.Ignore;
			adapters.DoCheckDriverThen(true);
			try{count=TapManager.CountAvailableAdapters(TapManager.TapType.TAP_TAP0901);}
			catch(Exception ex){
				Log.w("error while get available adapters");
				Log.w(ex);
			}
			if(count<=0)Invoke(new Action(()=>r=MessageBox.Show(
				"找不到可用的TAP网卡，是否创建一个？","启动",
				MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question
			)));
			if(r==DialogResult.Yes)adapters.DoCreateAdapter(true);
			if(r==DialogResult.Cancel)return;
			DoTryStart(version,config);
		}
		private void TryStop(){
			if(mgmt==null)return;
			SetStatus(Status.STATUS_STOPPING);
			Task.Run(DoTryStop);
		}
		private void CheckTryStart(string content=null){
			if(!CheckVersion())return;
			if(content==null&&!CheckCanStart())return;
			string version=comboVersions.SelectedItem.ToString();
			Task.Run(()=>DoCheckTryStart(version,content));
		}
		private void btnSave_Click(object sender,EventArgs e){
			string content,filename;
			if(!CheckCanStart())return;
			try{content=GenerateConfig();}
			catch(Exception ex){
				Log.e("generate config failed");
				Log.e(ex);
				MessageBox.Show(
					string.Format("生成配置文件失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				);
				return;
			}
			using(SaveFileDialog save=new SaveFileDialog()){
				save.Filter=filter;
				save.Title="保存配置文件";
				save.FileName="edge.conf";
				if(save.ShowDialog()!=DialogResult.OK)return;
				filename=save.FileName;
			}
			try{
				File.WriteAllText(filename,content);
				Log.d("saved config to {0}",filename);
			}catch(Exception ex){
				Log.e("save config failed");
				Log.e(ex);
				MessageBox.Show(
					string.Format("保存配置文件失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				);
				return;
			}
			Process.Start("explorer.exe",string.Format("/select,\"{0}\"",filename));
		}
		private string GenerateConfig(){
			StringBuilder sb=new StringBuilder();
			sb.Append("#!/usr/sbin/edge\n\n");
			sb.Append("# Super Nodes\n");
			foreach(var item in lstSuperNode.Items)
				sb.Append("-l=\"").Append(item.ToString()).Append("\"\n");
			sb.Append("\n# Community\n");
			sb.Append("-c=\"").Append(txtCommunity.Text).Append("\"\n");
			if(txtKey.Text.Length>0){
				sb.Append("\n# Encryption key\n");
				sb.Append("-k=\"").Append(txtKey.Text).Append("\"\n");
			}
			if(!chkPortAuto.Checked){
				sb.Append("\n# Local port\n");
				sb.Append("-p=").Append(txtPort.Value).Append('\n');
			}
			sb.Append("\n# IPv4 Address\n");
			sb.Append("-a=");
			if(!chkAddressDHCP.Checked){
				IPv4Address addr=new IPv4Address(
					txtAddress.Text,
					(byte)txtAddressPrefix.Value
				);
				sb.Append("static:");
				sb.Append(addr.GetNetworkString());
			}else sb.Append("dhcp:0.0.0.0");
			sb.Append("\n");
			if(!chkMacAddressAuto.Checked){
				string str=txtMacAddress.Text.Replace(":","-");
				PhysicalAddress mac=PhysicalAddress.Parse(str);
				sb.Append("\n# Fixed MAC Address\n");
				sb.Append("-m=");
				sb.Append(Utils.PhysicalAddressToString(mac));
				sb.Append('\n');
			}
			if(routes.Count>0) {
				sb.Append("\n# Static routes\n");
				foreach(Routes.Route route in routes){
					sb.Append("-n ");
					sb.Append(route.network);
					sb.Append(":");
					sb.Append(route.nexthop);
					sb.Append('\n');
				}
			}
			if(chkMulticast.Checked){
				sb.Append("\n# Allow multicast packet\n");
				sb.Append("-E\n");
			}
			if(chkRouting.Checked){
				sb.Append("\n# Allow packet forward and routing\n");
				sb.Append("-r\n");
			}
			return sb.ToString();
		}
		private string GenerateStartupConfig(string config,int port,string key){
			StringBuilder sb=new StringBuilder();
			sb.Append(config);
			sb.Append("\n# Management Port\n");
			sb.Append("-t=").Append(port).Append("\n");
			sb.Append("\n# Management Password\n");
			sb.Append("--management-password=").Append(key).Append("\n");
			return sb.ToString();
		}
		private bool CheckVersion(){
			if(comboVersions.SelectedIndex<0){
				MessageBox.Show(
					"请选择边缘节点版本","无效配置",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return false;
			}
			return true;
		}
		private bool CheckCanStart(){
			if(!CheckVersion())return false;
			string c=txtCommunity.Text.Trim();
			if(c.Length<=0){
				MessageBox.Show(
					"请设置社区名","无效配置",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return false;
			}
			if(c.Length>16){
				MessageBox.Show(
					"社区名过长，最多16个字符","无效配置",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return false;
			}
			if(!chkPortAuto.Checked&&txtPort.Value<=1024&&MessageBox.Show(
				"端口号过低，"+
				"可能会导致冲突，"+
				"确认要继续吗？",
				"无效配置",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation
			)!=DialogResult.Yes)return false;
			if(txtKey.Text.Length<=0&&MessageBox.Show(
				"未设置加密密钥，"+
				"所有的流量都不会加密，"+
				"以明文形式传输，"+
				"确认要继续吗？",
				"无效配置",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation
			)!=DialogResult.Yes)return false;
			if(lstSuperNode.Items.Count<=0){
				MessageBox.Show(
					"请添加至少一个超级节点","无效配置",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return false;
			}
			if(!chkAddressDHCP.Checked){
				IPv4Address address;
				try{
					address=new IPv4Address(
						txtAddress.Text,
						(byte)txtAddressPrefix.Value
					);
					if(address==null)throw new ArgumentException();
				}catch(Exception){
					MessageBox.Show(
						"请输入正确的IP地址","无效配置",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
					return false;
				}
				if(!address.IsValidHost()&&MessageBox.Show(
					"输入的IP地址不是一个主机地址，"+
					"可能导致无法正常通信，"+
					"确认要使用这个地址吗？",
					"无效配置",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Exclamation
				)!=DialogResult.Yes)return false;
			}
			if(!chkMacAddressAuto.Checked){
				PhysicalAddress address;
				try{
					address=PhysicalAddress.Parse(
						txtMacAddress.Text.Replace(":","-")
					);
					if(address==null)throw new ArgumentException();
				}catch(Exception){
					MessageBox.Show(
						"请输入正确的MAC地址","无效配置",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
					return false;
				}
			}
			Log.d("startup check done");
			return true;
		}
		private void SetDisabled(bool disabled){
			panel.Enabled=!disabled;
			btnStop.Enabled=disabled;
			btnStart.Enabled=!disabled;
			menuStartWith.Enabled=!disabled;
			menuConnectToEdge.Enabled=!disabled;
			notifyMenuConnectToEdge.Enabled=!disabled;
			notifyMenuStartWith.Enabled=!disabled;
			notifyMenuStop.Enabled=disabled;
		}
		private void SetStatus(Status status){
			SetBytes(0,0);
			this.status=status;
			string str=StatusToString(status);
			Log.d("status changed to {0}",status.ToString());
			adapters.LoadAdapters(true);
			statusEdge.Text=str;
			notify.Text=string.Format("{0} - {1}",Text,str);
			if(notify.Visible)
				notify.ShowBalloonTip(0,"状态变更",str,ToolTipIcon.Info);
			switch(status){
				case Status.STATUS_NOT_STARTED:
					SetDisabled(false);
				break;
				case Status.STATUS_STARTING:
				case Status.STATUS_CONNECTING:
				case Status.STATUS_STOPPING:
					SetDisabled(true);
					btnStop.Enabled=false;
				break;
				case Status.STATUS_RUNNING:
					SetDisabled(true);
				break;
			}
		}
		private string StatusToString(Status status){
			switch(status){
				case Status.STATUS_NOT_STARTED:return "未启动";
				case Status.STATUS_STARTING:return "正在启动...";
				case Status.STATUS_CONNECTING:return "正在连接...";
				case Status.STATUS_RUNNING:return "运行中";
				case Status.STATUS_STOPPING:return "正在停止...";
				default:return "未知";
			}
		}
		public class SuperNode{
			public string hostname{get;set;}
			public int port{get;set;}=7777;
			public SuperNode(){}
			public SuperNode(string node)=>Set(node);
			public SuperNode(AddSuperNode add)=>Set(add);
			public SuperNode(string hostname,int port)=>Set(hostname,port);
			public void Set(string hostname,int port){
				this.hostname=hostname;
				this.port=port;
			}
			public void Set(string node){
				if(node.Contains(":")){
					hostname=node.Substring(0,node.IndexOf(":"));
					port=int.Parse(node.Substring(node.IndexOf(":")+1));
				}else hostname=node;
			}
			public void Set(AddSuperNode add)=>Set(add.txtHost.Text,(int)add.txtPort.Value);
			public override string ToString()=>string.Format("{0}:{1}",hostname,port);
			public static void AddTo(List<SuperNode>supernodes,AddSuperNode node){
				supernodes.Add(new SuperNode(node));
			}
			public static void AddTo(List<SuperNode>supernodes,string hostname,int port){
				supernodes.Add(new SuperNode(hostname,port));
			}
			public static void AddTo(List<SuperNode>supernodes,string node){
				try{supernodes.Add(new SuperNode(node));}
				catch(Exception ex){
					Log.w("add {0} failed",node);
					Log.w(ex);
				}
			}
		}
		private enum Status{
			STATUS_NOT_STARTED,
			STATUS_STARTING,
			STATUS_CONNECTING,
			STATUS_RUNNING,
			STATUS_STOPPING,
		}
	}
}
