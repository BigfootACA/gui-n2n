using System;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace GuiN2N{
	internal static class Log{
		private static StreamWriter logs=null;
		public static string log=null;
		public static readonly string folder=Path.Combine(Utils.DataDir,"logs");
		private static readonly LogLevel level=LogLevel.LEVEL_DEBUG;
		public static bool IsDebug{
			get{
				#if DEBUG
				return true;
				#else
				return false;
				#endif
			}
		}
		public static void t(string message,params object[]args)=>Print(LogLevel.LEVEL_TRACE,message,args);
		public static void d(string message,params object[]args)=>Print(LogLevel.LEVEL_DEBUG,message,args);
		public static void i(string message,params object[]args)=>Print(LogLevel.LEVEL_INFO,message,args);
		public static void w(string message,params object[]args)=>Print(LogLevel.LEVEL_WARN,message,args);
		public static void e(string message,params object[]args)=>Print(LogLevel.LEVEL_ERROR,message,args);
		public static void f(string message,params object[]args)=>Print(LogLevel.LEVEL_FATAL,message,args);
		public static void w(Exception e)=>Print(LogLevel.LEVEL_WARN,e.ToString());
		public static void e(Exception e)=>Print(LogLevel.LEVEL_ERROR,e.ToString());
		public static void f(Exception e)=>Print(LogLevel.LEVEL_FATAL,e.ToString());
		public static void Print(LogLevel level,string message,params object[]args){
			if(level<Log.level)return;
			if(!message.EndsWith("\n"))message+="\r\n";
			Write(string.Format(
				"[{0}] {1} {2}: {3}",
				DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
				LogLevelToString(level),
				StackFrameToString(GetCallerFrame()),
				string.Format(message,args)
			));
		}
		public static void Initialize(){
			if(!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
			for(int i=1;i<1024;i++){
				string time=DateTime.Now.ToString("yyyyMMddHHmmss");
				string fn=string.Format("logs-{0}-{1}.log",time,i);
				string path=Path.Combine(folder,fn);
				if(File.Exists(path))continue;
				logs=new StreamWriter(new FileStream(
					path,
					FileMode.Create,
					FileAccess.Write,
					FileShare.ReadWrite
				),Encoding.UTF8);
				log=path;
				break;
			}
		}
		private static void Write(string message){
			if(logs!=null){
				logs.Write(message);
				logs.Flush();
			}
			Debug.Write(message);
		}
		private static StackFrame GetCallerFrame(){
			StackTrace stack=new StackTrace(IsDebug);
			StackFrame[] frames=stack.GetFrames();
			foreach(StackFrame frame in frames)
				if(frame.GetMethod().ReflectedType!=typeof(Log))
					return frame;
			return null;
		}
		private static string StackFrameToString(StackFrame frame){
			string str="";
			if(frame==null)return str;
			string file=frame.GetFileName();
			string func=frame.GetMethod().Name;
			int line=frame.GetFileLineNumber();
			if(file!=null&&file.Trim().Length<=0)file=null;
			if(func!=null&&func.Trim().Length<=0)func=null;
			if(file!=null)str+=Path.GetFileName(file);
			if(func!=null){
				if(str.Length>0)str+="@";
				str+=func;
			}
			if(line>0){
				if(str.Length>0)str+=":";
				str+=line;
			}
			return str;
		}
		public static string LogLevelToString(LogLevel level){
			string name=level.ToString();
			if(name.StartsWith("LEVEL_"))
				name=name.Substring(6);
			return name;
		}
		public enum LogLevel{
			LEVEL_TRACE,
			LEVEL_DEBUG,
			LEVEL_INFO,
			LEVEL_WARN,
			LEVEL_ERROR,
			LEVEL_FATAL,
		};
	}
}
