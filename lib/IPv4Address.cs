using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace GuiN2N{
	public class IPv4Address{
		public static class Address{
			public static readonly IPv4Address Any=new IPv4Address(0,0,0,0);
			public static readonly IPv4Address Loopback=new IPv4Address(127,0,0,1);
			public static readonly IPv4Address Broadcast=new IPv4Address(255,255,255,255);
		}
		public static class Subnet{
			public static readonly IPv4Address Any=new IPv4Address(0,0,0,0,8);
			public static readonly IPv4Address Loopback=new IPv4Address(127,0,0,0,8);
			public static readonly IPv4Address ClassA=new IPv4Address(0,0,0,0,1);
			public static readonly IPv4Address ClassB=new IPv4Address(128,0,0,0,2);
			public static readonly IPv4Address ClassC=new IPv4Address(192,0,0,0,3);
			public static readonly IPv4Address ClassD=new IPv4Address(224,0,0,0,4);
			public static readonly IPv4Address ClassE=new IPv4Address(225,0,0,0,5);
			public static readonly IPv4Address ClassAPrivate=new IPv4Address(10,0,0,0,8);
			public static readonly IPv4Address ClassBPrivate=new IPv4Address(172,18,0,0,12);
			public static readonly IPv4Address ClassCPrivate=new IPv4Address(192,168,0,0,16);
		}
		private static readonly Random random=new Random();
		private uint address=uint.MinValue;
		private uint mask=uint.MaxValue;
		public void Set(byte a0,byte a1,byte a2,byte a3,byte prefix){
			SetAddress(a0,a1,a2,a3);
			SetPrefix(prefix);
		}
		public void Set(
			byte a0,byte a1,byte a2,byte a3,
			byte m0,byte m1,byte m2,byte m3
		){
			SetAddress(a0,a1,a2,a3);
			SetMask(m0,m1,m2,m3);
		}
		public void Set(uint address,uint mask){
			this.address=address;
			this.mask=mask;
		}
		public void Set(string address,byte prefix){
			this.address=ParseAddress(address);
			mask=PrefixToMask(prefix);
		}
		public void Set(string str){
			string addr=str;
			if(addr.Contains('/')){
				SetMask(addr.Substring(addr.IndexOf('/')+1));
				addr=addr.Substring(0,addr.IndexOf('/'));
			}
			SetAddress(addr);
		}
		public void Set(UnicastIPAddressInformation info){
			if(info.Address.AddressFamily!=AddressFamily.InterNetwork)
				throw new InvalidOperationException();
			Set(info.Address,info.IPv4Mask);
		}
		public void Set(uint address)=>this.address=address;
		public void Set(byte[]address)=>Set(BytesToNumber(address));
		public void Set(IPAddress address)=>Set(address.GetAddressBytes());
		public void Set(byte[]address,byte prefix)=>Set(BytesToNumber(address),prefix);
		public void Set(byte[]address,byte[]mask)=>Set(BytesToNumber(address),BytesToNumber(mask));
		public void Set(IPAddress address,byte prefix)=>Set(address.GetAddressBytes(),prefix);
		public void Set(IPAddress address,IPAddress mask)=>Set(address.GetAddressBytes(),mask.GetAddressBytes());
		public void SetAddress(uint address)=>this.address=address;
		public void SetMask(uint mask)=>this.mask=mask;
		public void SetWildcard(uint wildcard)=>mask=WildcardToMask(wildcard);
		public void SetPrefix(byte prefix)=>mask=PrefixToMask(prefix);
		public void SetAddress(byte[]address)=>SetAddress(BytesToNumber(address));
		public void SetMask(byte[]mask)=>SetMask(BytesToNumber(mask));
		public void SetWildcard(byte[]wildcard)=>SetWildcard(BytesToNumber(wildcard));
		public void SetAddress(IPAddress address)=>SetAddress(address.GetAddressBytes());
		public void SetMask(IPAddress mask)=>SetMask(mask.GetAddressBytes());
		public void SetWildcard(IPAddress wildcard)=>SetWildcard(wildcard.GetAddressBytes());
		public void SetAddress(string address)=>SetAddress(ParseAddress(address));
		public void SetMask(string mask)=>SetMask(mask.Length<=2?PrefixToMask(byte.Parse(mask)):ParseAddress(mask));
		public void SetWildcard(string wildcard)=>SetWildcard(ParseAddress(wildcard));
		public void SetPrefix(string prefix)=>SetPrefix(byte.Parse(prefix));
		public void SetAddress(byte p0,byte p1,byte p2,byte p3)=>SetAddress(new byte[]{p0,p1,p2,p3});
		public void SetMask(byte p0,byte p1,byte p2,byte p3)=>SetMask(new byte[]{p0,p1,p2,p3});
		public void SetWildcard(byte p0,byte p1,byte p2,byte p3)=>SetWildcard(new byte[]{p0,p1,p2,p3});
		public IPv4Address(){}
		public IPv4Address(byte[]address)=>Set(address);
		public IPv4Address(IPAddress address)=>Set(address);
		public IPv4Address(uint address)=>Set(address);
		public IPv4Address(uint address,uint mask)=>Set(address,mask);
		public IPv4Address(string address,byte prefix)=>Set(address,prefix);
		public IPv4Address(string address)=>Set(address);
		public IPv4Address(byte[]address,byte[]mask)=>Set(address,mask);
		public IPv4Address(byte[]address,byte prefix)=>Set(address,prefix);
		public IPv4Address(IPAddress address,byte prefix)=>Set(address,prefix);
		public IPv4Address(IPAddress address,IPAddress mask)=>Set(address,mask);
		public IPv4Address(UnicastIPAddressInformation info)=>Set(info);
		public IPv4Address(byte p0,byte p1,byte p2,byte p3)=>SetAddress(p0,p1,p2,p3);
		public IPv4Address(byte p0,byte p1,byte p2,byte p3,byte prefix)=>Set(p0,p1,p2,p3,prefix);
		public IPv4Address(byte a0,byte a1,byte a2,byte a3,byte m0,byte m1,byte m2,byte m3)=>Set(a0,a1,a2,a3,m0,m1,m2,m3);
		public uint GetCount()=>GetWildcard()-1;
		public uint GetAddress()=>address;
		public uint GetMask()=>mask;
		public uint GetWildcard()=>MaskToWildcard(mask);
		public byte GetPrefix()=>MaskToPrefix(mask);
		public uint GetSubnet()=>GetNetworkByMask(address,mask);
		public uint GetBroadcast()=>GetPrefix()<32?GetSubnet()+GetWildcard():GetAddress();
		public uint GetPoolStart()=>GetPrefix()<31?GetSubnet()+1:GetAddress();
		public uint GetPoolEnd()=>GetPrefix()<31?GetBroadcast()-1:GetAddress();
		public byte[]GetAddressBytes()=>NumberToBytes(GetAddress());
		public byte[]GetMaskBytes()=>NumberToBytes(GetMask());
		public byte[]GetWildcardBytes()=>NumberToBytes(GetWildcard());
		public byte[]GetPrefixBytes()=>NumberToBytes(GetPrefix());
		public byte[]GetSubnetBytes()=>NumberToBytes(GetSubnet());
		public byte[]GetBroadcastBytes()=>NumberToBytes(GetBroadcast());
		public byte[]GetPoolStartBytes()=>NumberToBytes(GetPoolStart());
		public byte[]GetPoolEndBytes()=>NumberToBytes(GetPoolEnd());
		public IPAddress GetSubnetIPAddress()=>new IPAddress(GetSubnetBytes());
		public IPAddress GetAddressIPAddress()=>new IPAddress(GetAddressBytes());
		public IPAddress GetWildcardIPAddress()=>new IPAddress(GetWildcardBytes());
		public IPAddress GetMaskIPAddress()=>new IPAddress(GetMaskBytes());
		public IPAddress GetPoolStartIPAddress()=>new IPAddress(GetPoolStartBytes());
		public IPAddress GetPoolEndIPAddress()=>new IPAddress(GetPoolEnd());
		public IPv4Address GetSubnetIPv4Address()=>new IPv4Address(GetSubnet());
		public IPv4Address GetAddressIPv4Address()=>new IPv4Address(GetAddress());
		public IPv4Address GetWildcardIPv4Address()=>new IPv4Address(GetWildcard());
		public IPv4Address GetMaskIPv4Address()=>new IPv4Address(GetMask());
		public IPv4Address GetPoolStartIPv4Address()=>new IPv4Address(GetPoolStart());
		public IPv4Address GetPoolEndIPv4Address()=>new IPv4Address(GetPoolEnd());
		public string GetAddressString()=>NumberToString(GetAddress());
		public string GetWildcardString()=>NumberToString(GetWildcard());
		public string GetMaskString()=>NumberToString(GetMask());
		public string GetPrefixString()=>GetPrefix().ToString();
		public string GetNetworkString()=>string.Format("{0}/{1}",GetAddressString(),GetPrefixString());
		public string GetSubnetString()=>string.Format("{0}/{1}",GetSubnetAddressString(),GetPrefixString());
		public string GetSubnetAddressString()=>NumberToString(GetSubnet());
		public string GetBroadcastString()=>NumberToString(GetBroadcast());
		public string GetPoolStartString()=>NumberToString(GetPoolStart());
		public string GetPoolEndString()=>NumberToString(GetPoolEnd());
		public override string ToString()=>mask==uint.MaxValue?GetAddressString():GetNetworkString();
		public bool IsInPool(byte[]address)=>IsInPool(BytesToNumber(address));
		public bool IsInPool(IPv4Address address)=>IsInPool(address.GetAddress());
		public bool IsInPool(IPAddress address)=>IsInPool(address.GetAddressBytes());
		public bool IsInPool(string address)=>IsInPool(ParseAddress(address));
		public bool IsInPool(uint address){
			if(GetPrefix()>31)return address==this.address;
			if(address>GetPoolEnd())return false;
			if(address<GetPoolStart())return false;
			return true;
		}
		public bool IsValidHost(){
			if(GetPrefix()>30)return true;
			return IsInPool(this);
		}
		public IPv4Address RandomAddress(){
			if(GetCount()<=0)return null;
			return new IPv4Address(Random());
		}
		public uint Random(){
			if(GetCount()<=0)return 0;
			return (uint)random.Next((int)GetPoolStart(),(int)GetPoolEnd()+1);
		}
		public void ForEachPool(Action<IPv4Address>action){
			for(uint i=GetPoolStart();i<=GetPoolEnd();i++)
				action(new IPv4Address(i,mask));
		}
		public static IPv4Address Parse(string address)=>new IPv4Address(address);
		public static IPv4Address RandomPrivateSubnet(){
			IPv4Address[]pools=new IPv4Address[]{
				Subnet.ClassAPrivate,
				Subnet.ClassBPrivate,
				Subnet.ClassCPrivate,
			};
			return pools[random.Next(pools.Length)];
		}
		public static IPv4Address RandomPrivateAddress(bool host){
			IPv4Address pool=RandomPrivateSubnet();
			IPv4Address address=pool.RandomAddress();
			address.SetPrefix(host?(byte)32:pool.GetPrefix());
			return address;
		}
		public static byte[]ParseAddressBytes(string address){
			byte[]cont=new byte[4];
			string[]addr=address.Split('.');
			if(addr.Length>4)throw new InvalidOperationException();
			cont[3]=byte.Parse(addr[addr.Length-1]);
			if(addr.Length>1)for(int i=0;i<addr.Length-1;i++)
				cont[i]=byte.Parse(addr[i]);
			return cont;
		}
		public static uint BytesToNumber(byte[]address){
			if(BitConverter.IsLittleEndian)Array.Reverse(address);
			return BitConverter.ToUInt32(address,0);
		}
		public static byte[]NumberToBytes(uint address){
			byte[]cont=new byte[4];
			BitConverter.GetBytes(address).CopyTo(cont,0);
			if(BitConverter.IsLittleEndian)Array.Reverse(cont);
			return cont;
		}
		public static byte MaskToPrefix(uint mask){
			long m=0x80000000;
			byte p =0;
			while(m>0){
				if((mask&m)!=0)p++;
				m>>=1;
			}
			return p;
		}
		public static string BytesToString(byte[]addr)=>string.Format("{0}.{1}.{2}.{3}",addr[0],addr[1],addr[2],addr[3]);
		public static string NumberToString(uint addr)=>BytesToString(NumberToBytes(addr));
		public static uint PrefixToMask(byte prefix)=>0xFFFFFFFF<<(32-prefix)&0xFFFFFFFF;
		public static uint PrefixToWildcard(byte prefix)=>MaskToWildcard(PrefixToMask(prefix));
		public static uint MaskToWildcard(uint mask)=>(~mask)&0xFFFFFFFF;
		public static uint WildcardToMask(uint wildcard)=>(~wildcard)&0xFFFFFFFF;
		public static uint ParseAddress(string address)=>BytesToNumber(ParseAddressBytes(address));
		public static uint GetNetworkByMask(uint address,uint mask)=>address&mask;
		public static uint GetNetworkByPrefix(uint address,byte prefix)=>address&PrefixToMask(prefix);
		public static uint GetNetworkByWildcard(uint address,uint wildcard)=>address&WildcardToMask(wildcard);
		public class IPv4AddressJsonConverter:JsonConverter<IPv4Address>{
			public override IPv4Address Read(ref Utf8JsonReader r,Type t,JsonSerializerOptions o)=>Parse(r.GetString());
			public override void Write(Utf8JsonWriter w,IPv4Address v,JsonSerializerOptions o)=>w.WriteStringValue(v.ToString());
		}
	}
}
