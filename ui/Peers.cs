using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
namespace GuiN2N{
	public partial class Peers:Form{
		private readonly Main main;
		private IPv4Address addr=null;
		private System.Windows.Forms.Timer timer;
		private readonly Dictionary<PhysicalAddress,Peer>peers=new Dictionary<PhysicalAddress,Peer>();
		public Peers(Main main){
			InitializeComponent();
			this.main=main;
			timer=new System.Windows.Forms.Timer{Interval=3000};
			timer.Tick+=OnTimeEvent;
		}
		private void Peers_Load(object sender,EventArgs e)=>timer.Start();
		private void Peers_FormClosing(object sender,FormClosingEventArgs e){
			if(e.CloseReason==CloseReason.UserClosing){
				e.Cancel=true;
				Hide();
			}else{
				timer.Stop();
				timer=null;
			}
		}
		private void menuClean_Click(object sender,EventArgs e){
			lock(peers){
				list.Items.Clear();
				peers.Clear();
			}
		}
		private void LoadPeers(){
			List<JsonElement>ret;
			List<PhysicalAddress>found=new List<PhysicalAddress>();
			try{
				ret=main.mgmt.ExecuteReadCommand("edges");
				foreach(JsonElement elem in ret){
					Peer peer=new Peer(list,elem);
					lock(peers){
						if(peers.ContainsKey(peer.mac)){
							peer=peers[peer.mac];
							peer.UpdateWithJson(elem);
						}else peer.AddList(peers);
						peer.StartCheckDelay();
						if(addr==null)addr=peer.address;
					}
					found.Add(peer.mac);
				}
				lock(peers){
					PhysicalAddress[]keys=new PhysicalAddress[peers.Keys.Count];
					peers.Keys.CopyTo(keys,0);
					foreach(PhysicalAddress key in keys){
						if(found.Contains(key))continue;
						Log.d("{0} disappered",key);
						peers[key].SetMode(Peer.PeerMode.STATUS_DISCONNECTED);
						peers[key].SetRemote((IPEndPoint)null);
					}
				}
			}catch(Exception ex){
				Log.e("error while request edges list");
				Log.e(ex);
			}
			if(timer!=null)try{Invoke(new Action(()=>timer.Start()));}catch(Exception){}
		}
		private void OnTimeEvent(object source,EventArgs e){
			if(main.mgmt==null){
				if(peers.Count!=0){
					peers.Clear();
					list.Clear();
				}
				return;
			}
			timer.Stop();
			Task.Run(LoadPeers);
		}
		private class Peer{
			internal ListView view=null;
			internal ListViewItem item=null;
			internal PeerMode mode=PeerMode.STATUS_UNKNOWN;
			internal IPv4Address address=null;
			internal IPEndPoint remote=null;
			internal PhysicalAddress mac=null;
			internal string desc=null;
			private long lastCheckDelay=0;
			private readonly Ping ping=new Ping();
			private bool checkingDelay=false;
			private void CheckDelay(object state){
				try{
					Log.d("start ping peer {0}",address.GetAddressString());
					PingReply reply=ping.Send(address.GetAddressIPAddress(),20000);
					if(reply.Status!=IPStatus.Success)throw new IOException(reply.Status.ToString());
					UpdateItem(5,string.Format("{0}ms",reply.RoundtripTime));
					Log.i("ping peer {0} result: {1}",address.GetAddressString(),reply.RoundtripTime);
					lastCheckDelay=DateTimeOffset.Now.ToUnixTimeSeconds();
				}catch(Exception ex){
					Log.e("error while ping peer {0}",address.GetAddressString());
					Log.e(ex);
					UpdateItem(5,"失败");
				}
				checkingDelay=false;
			}
			public void StartCheckDelay(){
				if(checkingDelay)return;
				if(address==null||view==null||item==null)return;
				if(!address.IsValidHost())return;
				long now=DateTimeOffset.Now.ToUnixTimeSeconds();
				if(now-lastCheckDelay<10)return;
				switch(mode){
					case PeerMode.STATUS_RELAY:case PeerMode.STATUS_DIRECT:break;
					default:UpdateItem(5,"");return;
				}
				ThreadPool.QueueUserWorkItem(CheckDelay);
			}
			public Peer(ListView view){
				this.view=view;
				Update();
			}
			public Peer(ListView view,JsonElement elem){
				this.view=view;
				UpdateWithJson(elem);
			}
			public void SetAddress(IPv4Address address){
				if(this.address!=null&&address.Equals(address))return;
				this.address=address;
				UpdateAddress();
			}
			public void SetRemote(IPEndPoint remote){
				if(this.remote!=null&&this.remote.Equals(remote))return;
				this.remote=remote;
				UpdateRemote();
			}
			public void SetMACAddress(PhysicalAddress mac){
				if(this.mac!=null&&this.mac.Equals(mac))return;
				this.mac=mac;
				UpdateMACAddress();
			}
			public void SetMode(PeerMode mode){
				if(this.mode.Equals(mode))return;
				this.mode=mode;
				UpdateMode();
			}
			public void SetAddress(string address){
				if(address==null||address.Trim().Length<=0)return;
				SetAddress(IPv4Address.Parse(address));
			}
			public void SetRemote(string remote){
				if(remote==null||remote.Trim().Length<=0)return;
				if(!remote.Contains(":"))return;
				SetRemote(
					remote.Substring(0,remote.IndexOf(":")),
					remote.Substring(remote.IndexOf(":")+1)
				);
			}
			public void SetMACAddress(string mac){
				if(mac==null||mac.Trim().Length<=0)return;
				SetMACAddress(PhysicalAddress.Parse(mac.Replace(':','-')));
			}
			public void SetMode(string mode){
				if(mode==null||mode.Trim().Length<=0)return;
				PeerMode n;
				switch(mode.ToUpper()){
					case "P2P":n=PeerMode.STATUS_DIRECT;break;
					case "PSP":n=PeerMode.STATUS_RELAY;break;
					default:return;
				}
				SetMode(n);
			}
			public void SetDescription(string desc){
				if(desc==null||desc.Trim().Length<=0)return;
				if(desc.Equals(this.desc))return;
				this.desc=desc;
				UpdateDescription();
			}
			private void UpdateItem(int index,string value){
				if(item==null||view==null)return;
				view.Invoke(new Action(()=>item.SubItems[index].Text=value));
			}
			public void AddList(Dictionary<PhysicalAddress,Peer>peers){
				peers.Add(mac,this);
				view.Invoke(new Action(()=>view.Items.Add(item)));
			}
			public void Update(){
				if(item==null){
					item=new ListViewItem("...");
					item.SubItems.Add("...");
					item.SubItems.Add("...");
					item.SubItems.Add("...");
					item.SubItems.Add("...");
					item.SubItems.Add("...");
				}
			}
			public void UpdateWithJson(JsonElement elem){
				Update();
				SetMACAddress(elem.GetProperty("macaddr"));
				SetAddress(elem.GetProperty("ip4addr"));
				SetRemote(elem.GetProperty("sockaddr"));
				SetMode(elem.GetProperty("mode"));
				SetDescription(elem.GetProperty("desc"));
				if(mac==null)throw new ArgumentNullException(nameof(mac));
			}
			public void SetRemote(IPAddress address,int port)=>SetRemote(new IPEndPoint(address,port));
			public void SetRemote(IPAddress address,string port)=>SetRemote(address,int.Parse(port));
			public void SetRemote(string address,int port)=>SetRemote(IPAddress.Parse(address),port);
			public void SetRemote(string address,string port)=>SetRemote(IPAddress.Parse(address),int.Parse(port));
			public void SetAddress(JsonElement address)=>SetAddress(address.GetString());
			public void SetRemote(JsonElement remote)=>SetRemote(remote.GetString());
			public void SetMACAddress(JsonElement mac)=>SetMACAddress(mac.GetString());
			public void SetMode(JsonElement mode)=>SetMode(mode.GetString());
			public void SetDescription(JsonElement desc)=>SetDescription(desc.GetString());
			public void UpdateAddress()=>UpdateItem(0,GetAddressString("..."));
			public void UpdateMACAddress()=>UpdateItem(1,GetMACAddressString("..."));
			public void UpdateDescription()=>UpdateItem(2,GetDescriptionString("..."));
			public void UpdateRemote()=>UpdateItem(3,GetRemoteString("..."));
			public void UpdateMode()=>UpdateItem(4,GetModeString("未知"));
			public string GetMACAddressString(string def)=>mac==null?def:Utils.PhysicalAddressToString(mac);
			public string GetAddressString(string def)=>address==null?def:address.GetAddressString();
			public string GetRemoteString(string def)=>remote==null?def:remote.ToString();
			public string GetDescriptionString(string def)=>desc==null?def:desc;
			public string GetModeString(string def){
				switch(mode){
					case PeerMode.STATUS_LOCAL:return "本地";
					case PeerMode.STATUS_RELAY:return "中转";
					case PeerMode.STATUS_DIRECT:return "直连";
					case PeerMode.STATUS_DISCONNECTED:return "断开";
					default:return def;
				}
			}
			public enum PeerMode{
				STATUS_UNKNOWN,
				STATUS_LOCAL,
				STATUS_RELAY,
				STATUS_DIRECT,
				STATUS_DISCONNECTED,
			}
		}
	}
}
