using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
namespace GuiN2N{
	internal class Utils{
		private static readonly string[]sizeUnits=new string[]{
			"","K","M","G","T","P","E","Z","Y"
		};
		private static readonly Random random=new Random();
		private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		private static readonly string basedir =Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		public static string DataDir=Path.Combine(basedir,"GuiN2N");
		public static string ResDir=Path.Combine(DataDir,"Extract");
		public static string FormatSize(decimal size,int decimals,int block,char i){
			int level=0;
			StringBuilder sb=new StringBuilder();
			while(size>block&&level<sizeUnits.Length){
				size/=block;
				level++;
			}
			string unit=sizeUnits[level];
			sb.Append(Math.Round((double)size,decimals));
			sb.Append(unit);
			if(unit.Trim().Length>0)
				sb.Append(i);
			return sb.ToString();
		}
		public static string FormatSize(decimal size)=>FormatSize(size,2,1024,'i')+"B";
		public static string PhysicalAddressToString(PhysicalAddress mac){
			StringBuilder str=new StringBuilder();
			byte[]array=mac.GetAddressBytes();
			foreach(byte b in array){
				int num=(b>>4)&0xF;
				if(str.Length>0)str.Append(':');
				for(int j=0;j<2;j++){
					str.Append((char)(num+(num<10?48:55)));
					num=b&0xF;
				}
			}
			return str.ToString();
		}
		public static string GetRandomBytes(int length,string del){
			byte[]rand=new byte[length];
			rng.GetBytes(rand);
			return BitConverter.ToString(rand).Replace("-",del);
		}
		public static string GetRandomMAC()=>"06:"+GetRandomBytes(5,":");
		public static int GetFreeUdpPort(){
			int port=0;
			bool available=false;
			IPGlobalProperties prop=IPGlobalProperties.GetIPGlobalProperties();
			IPEndPoint[]ls=prop.GetActiveUdpListeners();
			for(int i=0;i<1024&&!available;i++){
				available=true;
				port=random.Next(1024,65535);
				foreach(IPEndPoint ep in ls)
					if(ep.Port==port)available=false;
			}
			if(!available)throw new IOException("找不到可用端口");
			return port;
		}
		public static bool IsAdministrator(){
			bool admin;
			using(WindowsIdentity identity=WindowsIdentity.GetCurrent()){
				WindowsPrincipal principal=new WindowsPrincipal(identity);
				admin=principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			return admin;
		}
	}
}
