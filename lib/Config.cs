using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace GuiN2N{
	internal static class Config{
		public class MainFields{
			private static readonly string file=Path.Combine(Utils.DataDir,"main.json");
			public string version{get;set;}
			public string community{get;set;}
			public int port{get;set;}
			public bool port_auto{get;set;}
			public string key{get;set;}
			public List<Main.SuperNode>supernodes{get;set;}
			public List<Routes.Route>routes{get;set;}
			public bool address_auto{get;set;}
			public string address{get;set;}
			public int prefix{get;set;}
			public bool mac_auto{get;set;}
			public string mac{get;set;}
			public bool multicast{get;set;}
			public bool routing{get;set;}
			public void GetFields(Main main){
				if(main.comboVersions.SelectedIndex>=0)
					version=main.comboVersions.SelectedItem.ToString();
				community=main.txtCommunity.Text;
				port=(int)main.txtPort.Value;
				port_auto=main.chkPortAuto.Checked;
				key=main.txtKey.Text;
				supernodes=main.supernodes;
				routes=main.routes;
				address_auto=main.chkAddressDHCP.Checked;
				address=main.txtAddress.Text;
				prefix=(int)main.txtAddressPrefix.Value;
				mac_auto=main.chkMacAddressAuto.Checked;
				mac=main.txtMacAddress.Text;
				multicast=main.chkMulticast.Checked;
				routing=main.chkRouting.Checked;
			}
			public void SetFields(Main main){
				for(int i=0;i<main.comboVersions.Items.Count;i++){
					string item=main.comboVersions.Items[i].ToString();
					if(item==version)main.comboVersions.SelectedIndex=i;
				}
				if(community!=null)main.txtCommunity.Text=community;
				if(port>0)main.txtPort.Value=port;
				main.chkPortAuto.Checked=port_auto;
				if(key!=null)main.txtKey.Text=key;
				if(supernodes!=null){
					main.supernodes=supernodes;
					main.ReloadSuperNodes();
				}
				main.routes=routes;
				main.chkAddressDHCP.Checked=address_auto;
				if(address!=null)main.txtAddress.Text=address;
				if(prefix>0)main.txtAddressPrefix.Value=prefix;
				main.chkMacAddressAuto.Checked=mac_auto;
				if(mac!=null)main.txtMacAddress.Text=mac;
				main.chkMulticast.Checked=multicast;
				main.chkRouting.Checked=routing;
			}
			public static void LoadMainFields(Main main){
				MainFields fields;
				string content;
				try{
					if(!File.Exists(file))return;
					content=File.ReadAllText(file);
					fields=JsonSerializer.Deserialize<MainFields>(content);
					fields.SetFields(main);
					Log.d("loaded MainFields from {0}",file);
				}catch(Exception e){
					Log.e("error while load MainFields");
					Log.e(e.ToString());
				}
			}
			public static void SaveMainFields(Main main){
				MainFields fields;
				string content;
				try{
					fields=new MainFields();
					fields.GetFields(main);
					content=JsonSerializer.Serialize(fields);
					File.WriteAllText(file,content);
					Log.d("saved MainFields to {0}",file);
				}catch(Exception e){
					Log.e("error while save MainFields");
					Log.e(e.ToString());
				}
			}
		}
	}
}
