using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GuiN2N{
	public partial class AdapterManager:Form{
		private bool loading=false;
		private List<TapManager.NetworkAdapter>interfaces=new List<TapManager.NetworkAdapter>();
		private static readonly string tapctl=Path.Combine(Utils.ResDir,"binary","tapctl.exe");
		public AdapterManager()=>InitializeComponent();
		public void DoLoadAdapters(bool set_loading){
			if(set_loading){
				if(loading)return;
				loading=true;
			}
			try{
				interfaces=TapManager.ListAdapters();
				Log.d("found {0} interfaces",interfaces.Count);
				Invoke(new Action(()=>{
					list.Items.Clear();
					foreach(TapManager.NetworkAdapter nic in interfaces){
						string mac="";
						string address="";
						string status="已禁用";
						Log.d("interface {{{0}}} {1}",nic.Guid,nic.Name);
						NetworkInterface i=nic.Interface;
						if(i!=null){
							PhysicalAddress phy=i.GetPhysicalAddress();
							if(phy!=null)mac=Utils.PhysicalAddressToString(phy);
							status=OperationalStatusToString(i.OperationalStatus);
							IPInterfaceProperties prop=i.GetIPProperties();
							if(prop!=null)foreach(var addr in prop.UnicastAddresses)try{
								address=new IPv4Address(addr).ToString();
								break;
							}catch(Exception){}
							Log.d("status: {0}",i.OperationalStatus);
							if(mac.Length>0)Log.d("MAC address: {0}",mac);
							if(address.Length>0) Log.d("IPv4 address: {0}",address);
						}else Log.d("status: DISABLED");
						ListViewItem item=new ListViewItem{
							Tag=nic,
							Text=nic.Name
						};
						item.SubItems.Add(status);
						item.SubItems.Add(mac);
						item.SubItems.Add(address);
						item.SubItems.Add(nic.Guid.ToString());
						list.Items.Add(item);
					}
				}));
			}catch(Exception e){
				Log.e("error while load adapters");
				Log.e(e);
				Invoke(new Action(()=>MessageBox.Show(
					string.Format("加载失败: {0}",e.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				)));
			}
			if(set_loading)loading=false;
		}
		public void DoCreateAdapter(bool set_loading){
			if(set_loading){
				if(loading)return;
				loading=true;
			}
			try{
				ProcessStartInfo info=new ProcessStartInfo(){
					FileName=tapctl,
					Arguments="create",
					CreateNoWindow=true,
					UseShellExecute=false,
					RedirectStandardOutput=false,
					RedirectStandardError=false,
				};
				Log.d("call tapctl create new adapter");
				Process p=Process.Start(info);
				p.WaitForExit();
				if(p.ExitCode!=0)throw new IOException(
					string.Format("0x{0:X}",p.ExitCode)
				);
				Log.d("new adapter created");
			}catch(Exception ex){
				Log.e("error while add new adapter");
				Log.e(ex);
				Invoke(new Action(()=>MessageBox.Show(
					string.Format("添加失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				)));
			}
			DoLoadAdapters(false);
			if(set_loading)loading=false;
		}
		public void DoDeleteAdapter(bool set_loading,TapManager.NetworkAdapter adapter){
			if(set_loading){
				if(loading)return;
				loading=true;
			}
			try{
				ProcessStartInfo info=new ProcessStartInfo(){
					FileName=tapctl,
					Arguments=string.Format("delete {{{0}}}",adapter.Guid),
					CreateNoWindow=true,
					UseShellExecute=false,
					RedirectStandardOutput=false,
					RedirectStandardError=false,
				};
				Log.d("call tapctl delete adapter {{{0}}}",adapter.Guid);
				Process p=Process.Start(info);
				p.WaitForExit();
				if(p.ExitCode!=0)throw new IOException(
					string.Format("0x{0:X}",p.ExitCode)
				);
				Log.d("adapter deleted");
			}catch(Exception ex){
				Log.e("error while delete adapter");
				Log.e(ex);
				Invoke(new Action(()=>MessageBox.Show(
					string.Format("删除失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				)));
			}
			DoLoadAdapters(false);
			if(set_loading)loading=false;
		}
		public void DoInstallDriver(bool set_loading){
			if(TapManager.IsTapDriverInstalled())return;
			if(set_loading){
				if(loading)return;
				loading=true;
			}
			try{
				string install=Path.Combine(Utils.ResDir,"binary","tapinstall.exe");
				string driver=Path.Combine(Utils.ResDir,"driver","tap0901.inf");
				if(!File.Exists(driver))throw new FileNotFoundException("driver not fond",driver);
				ProcessStartInfo info=new ProcessStartInfo(){
					FileName=install,
					Arguments=string.Format("install \"{0}\" tap0901",driver),
					CreateNoWindow=true,
					UseShellExecute=false,
					RedirectStandardOutput=false,
					RedirectStandardError=false,
				};
				Log.d("call install driver");
				Process p=Process.Start(info);
				p.WaitForExit();
				if(p.ExitCode!=0) throw new IOException(
					string.Format("0x{0:X}",p.ExitCode)
				);
				Log.d("driver installed");
			}catch(Exception ex){
				Log.e("error while install driver");
				Log.e(ex);
				Invoke(new Action(()=>MessageBox.Show(
					string.Format("安装失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				)));
			}
			DoLoadAdapters(false);
			if(set_loading)loading=false;
		}
		public void DoCheckDriverThen(bool set_loading){
			DialogResult r=DialogResult.Ignore;
			try{
				if(!TapManager.IsTapDriverInstalled())Invoke(new Action(()=>r=MessageBox.Show(
					"TAP驱动未安装，是否安装?","安装驱动",
					MessageBoxButtons.YesNo,MessageBoxIcon.Question
				)));
				if(r==DialogResult.Yes)DoInstallDriver(set_loading);
			}catch(Exception ex){
				Log.e("error while create adapter");
				Log.e(ex);
				Invoke(new Action(()=>MessageBox.Show(
					string.Format("创建失败: {0}",ex.Message),
					"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
				)));
			}
		}
		public void DoCheckCreateAdapter(bool set_loading){
			DoCheckDriverThen(set_loading);
			DoCreateAdapter(set_loading);
		}
		public void LoadAdapters(bool set_loading)=>Task.Run(()=>DoLoadAdapters(set_loading));
		public void CreateAdapter(bool set_loading)=>Task.Run(()=>DoCreateAdapter(set_loading));
		public void InstallDriver(bool set_loading)=>Task.Run(()=>DoInstallDriver(set_loading));
		public void CheckCreateAdapter(bool set_loading)=>Task.Run(()=>DoCheckCreateAdapter(set_loading));
		public void CheckDriverThen(bool set_loading)=>Task.Run(()=>DoCheckDriverThen(set_loading));
		public void DeleteAdapter(bool set_loading,TapManager.NetworkAdapter adapter)=>Task.Run(()=>DoDeleteAdapter(set_loading,adapter));
		private void AdapterManager_Load(object sender,EventArgs e)=>LoadAdapters(true);
		private void menuRefresh_Click(object sender,EventArgs e)=>LoadAdapters(true);
		private void menuAddAdapter_Click(object sender,EventArgs e)=>CheckCreateAdapter(true);
		private void menuInstallDriver_Click(object sender,EventArgs e)=>InstallDriver(true);
		private void menuControlPanel_Click(object sender,EventArgs e)=>Process.Start("ncpa.cpl");
		private void menuDelete_Click(object sender,EventArgs e){
			if(list.SelectedItems.Count!=1)return;
			DeleteAdapter(true,(TapManager.NetworkAdapter)list.SelectedItems[0].Tag);
		}
		private void menuAdapterContext_Opening(object sender,CancelEventArgs e){
			if(list.SelectedItems.Count<=0)e.Cancel=true;
		}
		private void AdapterManager_FormClosing(object sender,FormClosingEventArgs e){
			if(e.CloseReason==CloseReason.UserClosing){
				e.Cancel=true;
				Hide();
			}
		}
		private static string OperationalStatusToString(OperationalStatus status){
			switch(status){
				case OperationalStatus.Up:return "已连接";
				case OperationalStatus.Down:return "已断开";
				case OperationalStatus.Testing:return "测试中";
				case OperationalStatus.Dormant:return "已休眠";
				case OperationalStatus.NotPresent:return "不存在";
				case OperationalStatus.LowerLayerDown:return "未连接";
				default:return "未知";
			}
		}
	}
}
