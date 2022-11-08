using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using Vanara.InteropServices;
using Vanara.PInvoke;
namespace GuiN2N{
	public static class TapManager{
		private static readonly string[]HWIDS=new string[]{@"root\tap0901","tap0901","Wintun","ovpn-dco"};
		private static List<string>GetHardwareIDs(SetupAPI.SafeHDEVINFO hdi,SetupAPI.SP_DEVINFO_DATA data){
			List<string> list = new List<string>();
			if(SetupAPI.SetupDiGetDeviceRegistryProperty(
				hdi,data,SetupAPI.SPDRP.SPDRP_HARDWAREID,
				out _,default,0,out uint size
			))return list;
			Win32Error err=Win32Error.GetLastError();
			if(err!=Win32Error.ERROR_INSUFFICIENT_BUFFER)err.ThrowIfFailed();
			if(size<=0)new Win32Error(Win32Error.ERROR_NOT_FOUND).ThrowIfFailed();
			using(SafeCoTaskMemHandle mem=new SafeCoTaskMemHandle(size)){
				Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiGetDeviceRegistryProperty(
					hdi,data,SetupAPI.SPDRP.SPDRP_HARDWAREID,
					out REG_VALUE_TYPE type,mem,mem.Size,out size
				));
				string str=Encoding.Unicode.GetString(mem.GetBytes());
				if(str[0]!=0)switch(type) {
					case REG_VALUE_TYPE.REG_SZ:list.Add(str.Substring(0,str.IndexOf('\0')));break;
					case REG_VALUE_TYPE.REG_MULTI_SZ:
						do{
							int i=str.IndexOf('\0');
							list.Add(str.Substring(0,i));
							str=str.Substring(i+1);
						}while(str[0]!=0);
					break;
				}
			}
			return list;
		}
		private static Guid GetAdapterGuid(SetupAPI.SafeHDEVINFO hdi,SetupAPI.SP_DEVINFO_DATA data){
			Guid guid=Guid.Empty;
			SafeRegistryHandle reg=SetupAPI.SetupDiOpenDevRegKey(
				hdi,data,SetupAPI.DICS_FLAG.DICS_FLAG_GLOBAL,0,
				SetupAPI.DIREG.DIREG_DRV,RegistryRights.ReadKey
			);
			Win32Error.ThrowLastErrorIfFalse(reg==null);
			uint size=1024;
			using(SafeCoTaskMemHandle mem=new SafeCoTaskMemHandle(size)){
				AdvApi32.RegQueryValueEx(
					reg,"NetCfgInstanceId",default,
					out REG_VALUE_TYPE type,mem,ref size
				).ThrowIfFailed();
				if(type==REG_VALUE_TYPE.REG_SZ){
					string str=Encoding.Unicode.GetString(mem.GetBytes());
					guid=Guid.Parse(str.Substring(0,str.IndexOf('\0')));
				}
			}
			return guid;
		}
		public static List<NetworkAdapter>ListAdapters(){
			Win32Error err;
			NetworkInterface[]nics=NetworkInterface.GetAllNetworkInterfaces();
			List<NetworkAdapter> list=new List<NetworkAdapter>();
			SetupAPI.SafeHDEVINFO hdi=SetupAPI.SetupDiGetClassDevsEx(
				SetupAPI.GUID_DEVCLASS_NET,null,IntPtr.Zero,
				SetupAPI.DIGCF.DIGCF_PRESENT,IntPtr.Zero,null,IntPtr.Zero
			);
			Win32Error.ThrowLastErrorIfFalse(hdi==null);
			SetupAPI.SP_DEVINFO_LIST_DETAIL_DATA dld=StructHelper.InitWithSize<SetupAPI.SP_DEVINFO_LIST_DETAIL_DATA>();
			Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiGetDeviceInfoListDetail(hdi,ref dld));
			for(uint i=0;;i++){
				bool compatible=false;
				NetworkAdapter adapter=new NetworkAdapter{Type=TapType.TAP_UNKNOWN};
				SetupAPI.SP_DEVINFO_DATA data=StructHelper.InitWithSize<SetupAPI.SP_DEVINFO_DATA>();
				if(!SetupAPI.SetupDiEnumDeviceInfo(hdi,i,ref data)){
					err=Win32Error.GetLastError();
					if(err==Win32Error.ERROR_NO_MORE_ITEMS)break;
					err.ThrowIfFailed();
				}
				List<string>ids=GetHardwareIDs(hdi,data);
				foreach(string id in HWIDS)if(ids.Contains(id)){
					compatible=true;
					switch(id){
						case "Wintun":adapter.Type=TapType.TAP_WINTUN;break;
						case "tap0901":adapter.Type=TapType.TAP_TAP0901;break;
						case "root\\tap0901":adapter.Type=TapType.TAP_TAP0901;break;
						case "ovpn-dco":adapter.Type=TapType.TAP_OVPN_DCO;break;
					}
					break;
				}
				if(!compatible)continue;
				adapter.Guid=GetAdapterGuid(hdi,data);
				using(RegistryKey key=Registry.LocalMachine.OpenSubKey(string.Format(
					@"SYSTEM\CurrentControlSet\Control\Network\{{{0}}}\{{{1}}}\Connection",
					SetupAPI.GUID_DEVCLASS_NET.ToString(),adapter.Guid.ToString()
				))){adapter.Name=(string)key.GetValue("Name");}
				foreach(NetworkInterface nic in nics){
					Guid guid=Guid.Parse(nic.Id);
					if(!guid.Equals(adapter.Guid))continue;
					adapter.Interface=nic;
					continue;
				}
				list.Add(adapter);
			}
			return list;
		}
		public static int CountAvailableAdapters(TapType type=TapType.TAP_UNKNOWN){
			int count=0;
			foreach(NetworkAdapter adapter in ListAdapters()){
				if(adapter.Interface==null)continue;
				if(adapter.Interface.OperationalStatus!=OperationalStatus.Down)continue;
				if(type!=TapType.TAP_UNKNOWN&&adapter.Type!=type)continue;
				count++;
			}
			return count;
		}
		public static void AddAdapter(string hwid,out bool reboot,out Guid guid){
			SetupAPI.SafeHDEVINFO hdi=SetupAPI.SetupDiCreateDeviceInfoList(
				SetupAPI.GUID_DEVCLASS_NET,default
			);
			Win32Error.ThrowLastErrorIfFalse(hdi==null);
			Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiClassNameFromGuid(
				SetupAPI.GUID_DEVCLASS_NET,out string name
			));
			SetupAPI.SP_DEVINFO_DATA data=StructHelper.InitWithSize<SetupAPI.SP_DEVINFO_DATA>();
			Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiCreateDeviceInfo(
				hdi,name,SetupAPI.GUID_DEVCLASS_NET,"Virtual Ethernet",
				default,SetupAPI.DICD.DICD_GENERATE_ID,ref data
			));
			Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiSetSelectedDevice(hdi,data));
			if(hwid==null)hwid="root\\tap0901";
			if(!hwid.EndsWith("\0"))hwid+="\0";
			SafeCoTaskMemHandle mem=new SafeCoTaskMemHandle(Encoding.Unicode.GetBytes(hwid));
			Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiSetDeviceRegistryProperty(
				hdi,ref data,SetupAPI.SPDRP.SPDRP_HARDWAREID,mem,mem.Size
			));
			Win32Error.ThrowLastErrorIfFalse(SetupAPI.SetupDiCallClassInstaller(
				SetupAPI.DI_FUNCTION.DIF_REGISTERDEVICE,hdi,data
			));
			Win32Error.ThrowLastErrorIfFalse(NewDev.DiInstallDevice(
				default,hdi,data,default,0,out reboot
			));
			guid=GetAdapterGuid(hdi,data);
		}
		public static bool IsTapDriverInstalled(){
			SelectQuery query=new SelectQuery("Win32_SystemDriver"){Condition="Name='tap0901'"};
			ManagementObjectSearcher searcher=new ManagementObjectSearcher(query);
			return searcher.Get().Count>0;
		}
		public struct NetworkAdapter{
			public Guid Guid;
			public string Name;
			public TapType Type;
			public NetworkInterface Interface;
		}
		public enum TapType{
			TAP_UNKNOWN,
			TAP_WINTUN,
			TAP_TAP0901,
			TAP_OVPN_DCO,
		}
	}
}
