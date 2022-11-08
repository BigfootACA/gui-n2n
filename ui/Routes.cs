using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Forms;
namespace GuiN2N{
	public partial class Routes:Form{
		public List<Route>routes;
		public Routes(List<Route>routes){
			this.routes=routes;
			InitializeComponent();
		}
		public void Reload(){
			list.Items.Clear();
			btnOK.Enabled=false;
			btnDelete.Enabled=false;
			foreach(Route route in routes){
				if(route.network==null||route.nexthop==null)continue;
				ListViewItem item=new ListViewItem{
					Tag=route,
					Text=route.network.GetAddressString()
				};
				item.SubItems.Add(route.network.GetPrefixString());
				item.SubItems.Add(route.nexthop.GetAddressString());
				list.Items.Add(item);
			}
		}
		private bool ApplyRoute(Route route){
			try{
				route.network=new IPv4Address(
					txtNetwork.Text,
					(byte)txtPrefix.Value
				);
			}catch(Exception){
				MessageBox.Show(
					"无效的网络地址","错误",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return false;
			}
			try{
				route.nexthop=new IPv4Address(
					txtNextHop.Text,32
				);
			}catch(Exception){
				MessageBox.Show(
					"无效的下一跳地址","错误",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return false;
			}
			return true;
		}
		private void Routes_Load(object sender,EventArgs e)=>Reload();
		private void btnOK_Click(object sender,EventArgs e){
			if(list.SelectedItems.Count!=1)return;
			Route route=(Route)list.SelectedItems[0].Tag;
			if(!ApplyRoute(route))return;
			Reload();
		}
		private void btnAdd_Click(object sender,EventArgs e){
			Route route=new Route();
			if(!ApplyRoute(route))return;
			routes.Add(route);
			txtNetwork.Text="";
			txtPrefix.Value=24;
			txtNextHop.Text="";
			Reload();
		}
		private void btnDelete_Click(object sender,EventArgs e){
			if(list.SelectedItems.Count!=1)return;
			int i=list.SelectedIndices[0];
			routes.Remove((Route)list.Items[i].Tag);
			Reload();
			int r=Math.Min(i,routes.Count-1);
			if(r>=0)list.SelectedIndices.Add(r);
			list_SelectedIndexChanged(sender,e);
		}
		private void list_SelectedIndexChanged(object sender,EventArgs e){
			bool en=list.SelectedItems.Count==1;
			btnOK.Enabled=en;
			btnDelete.Enabled=en;
			txtNetwork.Text="";
			txtPrefix.Value=24;
			txtNextHop.Text="";
			if(en){
				Route route=(Route)list.SelectedItems[0].Tag;
				if(route.network!=null){
					txtNetwork.Text=route.network.GetAddressString();
					txtPrefix.Value=route.network.GetPrefix();
				}
				if(route.nexthop!=null)
					txtNextHop.Text=route.nexthop.GetAddressString();
			}
		}
		public class Route{
			[JsonConverter(typeof(IPv4Address.IPv4AddressJsonConverter))]
			public IPv4Address network{get;set;}
			[JsonConverter(typeof(IPv4Address.IPv4AddressJsonConverter))]
			public IPv4Address nexthop{get;set;}
		}
	}
}
