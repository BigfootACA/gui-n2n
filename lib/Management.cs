using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
namespace GuiN2N{
	internal class Management{
		private readonly UdpClient udp;
		private readonly IPEndPoint remote;
		private readonly string password=null;
		private readonly Thread receiver=null;
		public event EventHandler<ExceptionEventArgs>Exited;
		private readonly Dictionary<string,ManagementResponse>responses=new Dictionary<string,ManagementResponse>();
		private static readonly Random random =new Random();
		public class ExceptionEventArgs:EventArgs{
			public Exception Exception{get;}
			public ExceptionEventArgs(Exception exception)=>Exception=exception;
		}
		public Management(string password,IPEndPoint remote):this(password,new UdpClient()){
			this.remote=remote;
			Log.i("Connect to {0}",remote.ToString());
			udp.Connect(remote);
		}
		public Management(string password,IPAddress host,int port):this(password,new IPEndPoint(host,port)){}
		public Management(string password,string host,int port):this(password,IPAddress.Parse(host),port){}
		public Management(string password,UdpClient udp){
			this.password=password;
			this.udp=udp;
			receiver=new Thread(new ThreadStart(ReceiveThread));
		}
		public void Start()=>receiver.Start();
		public void Stop(){
			try{
			if(
				receiver!=null&&
				receiver.ThreadState==ThreadState.Running
			)receiver.Abort();
			if(udp!=null)udp.Close();
			}catch(Exception e){Log.w(e);}
		}
		~Management()=>Stop();
		private void DataProcess(string data){
			string tag,type;
			JsonElement json;
			ManagementResponse res;
			json=JsonDocument.Parse(data).RootElement;
			tag=json.GetProperty("_tag").GetString();
			type=json.GetProperty("_type").GetString();
			Log.t("got response {0} with tag {1}",type,tag);
			lock(responses){
				if(!responses.ContainsKey(tag))return;
				res=responses[tag];
			}
			lock(res){
				if(res.tag!=tag)return;
				if(type=="begin")res.multi=true;
				if(type!="begin"&&type!="end")
					res.response.Add(json);
				if(res.multi&&type!="end")return;
			}
			if(res.wait!=null)res.wait.Set();
		}
		private void ReceiveThread(){
			Exception err=null;
			try{
				byte[]reads=null;
				IPEndPoint ep=new IPEndPoint(IPAddress.Any,0);
				Log.i("Receive thread started");
				while(true){
					try{
						reads=udp.Receive(ref ep);
						if(reads.Length<=0)continue;
						if(!ep.Equals(remote))continue;
					}catch(Exception e){
						Log.e("error while receive response");
						Log.e(e);
						err=e;
						break;
					}
					try{DataProcess(Encoding.UTF8.GetString(reads));}
					catch(Exception e){
						Log.e("error while progress response");
						Log.e(e);
						err=e;
					}
				}
			}catch(ThreadAbortException){}
			Log.i("Receive thread stopped");
			Exited?.Invoke(this,new ExceptionEventArgs(err));
		}
		private void SendCommand(string tag,CommandType type,string[]cmd){
			int ret;
			char op;
			string content;
			if(cmd.Length<=0)throw new ArgumentNullException();
			switch(type){
				case CommandType.CMD_READ:op='r';break;
				case CommandType.CMD_WRITE:op='w';break;
				default:throw new InvalidOperationException();
			}
			content=string.Format("{0} {1}:{2}",op,tag,cmd.Length);
			if(password!=null)content+=":"+password;
			foreach(string part in cmd)content+=" "+part;
			Log.d(
				"send {0} cmd {1} as {2}",
				type.ToString().Substring(4),cmd[0],tag
			);
			byte[]data=Encoding.UTF8.GetBytes(content);
			lock(udp){ret=udp.Send(data,data.Length);}
			if(ret!=data.Length)throw new IOException();
		}
		public List<JsonElement>ExecuteCommand(CommandType type,params string[]cmd){
			string tag;
			ManagementResponse res=new ManagementResponse();
			lock(responses){
				do{tag=random.Next(100000,999999).ToString();}
				while(responses.ContainsKey(tag));
				responses.Add(tag,res);
			}
			lock(res){
				res.tag=tag;
				res.wait=new AutoResetEvent(false);
			}
			SendCommand(tag,type,cmd);
			if(!res.wait.WaitOne(5000)){
				Log.e("wait {0} response timeout",tag);
				throw new TimeoutException();
			}
			lock(responses){responses.Remove(tag);}
			Log.d("cmd {0} got {1} responses",tag,res.response.Count);
			return res.response;
		}
		public List<JsonElement> ExecuteReadCommand(params string[]cmd)=>ExecuteCommand(CommandType.CMD_READ,cmd);
		public List<JsonElement> ExecuteWriteCommand(params string[]cmd)=>ExecuteCommand(CommandType.CMD_WRITE,cmd);
		private class ManagementResponse{
			public string tag;
			public bool multi=false;
			public AutoResetEvent wait;
			public List<JsonElement>response=new List<JsonElement>();
		}
		public enum CommandType{CMD_READ,CMD_WRITE};
	}
}
