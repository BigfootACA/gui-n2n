using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace GuiN2N{
	internal static class EntryPoint{
		private static readonly string depends=Path.Combine(Utils.ResDir,"depends");
		private static Assembly LoadFromFolder(object sender,ResolveEventArgs args){
			string name=string.Format("{0}.dll",new AssemblyName(args.Name).Name);
			string path=Path.Combine(depends,name);
			if(!File.Exists(path))return null;
			Log.d("Loading {0}",path);
			return Assembly.LoadFrom(path);
		}
		private static void ExtractResources(){
			bool found=false;
			Log.d("start extract resoucrces");
			try{
				if(Directory.Exists(Utils.ResDir))
					Directory.Delete(Utils.ResDir,true);
			}catch(Exception e){
				Log.w("remove extract folder failed");
				Log.w(e);
			}
			Directory.CreateDirectory(Utils.ResDir);
			string arch=RuntimeInformation.OSArchitecture.ToString().ToLower();
			Log.i("CPU Architecture: {0}",arch);
			Assembly asm=Assembly.GetExecutingAssembly();
			Stream stream=asm.GetManifestResourceStream("GuiN2N.res.zip");
			if(stream==null)throw new FileNotFoundException("res.zip");
			Log.i("Resource size: {0}",Utils.FormatSize(stream.Length));
			using(ZipArchive zip=new ZipArchive(stream)){
				foreach(ZipArchiveEntry entry in zip.Entries){
					string path=entry.FullName;
					if(path.Length<=0)continue;
					if(path.StartsWith("cpu_")){
						string cpu=string.Format("cpu_{0}/",arch);
						if(!path.StartsWith(cpu))continue;
						path=path.Substring(cpu.Length);
						found=true;
					}
					try{
						string realpath=Path.Combine(
							Utils.ResDir,
							path.Replace('/',Path.DirectorySeparatorChar)
						);
						if(!entry.FullName.EndsWith("/")){
							Log.d(
								"extract file {0} to {1} ({2})",
								entry.FullName,realpath,
								Utils.FormatSize(entry.Length)
							);
							using(Stream file=entry.Open()){
								using(Stream dest=File.Create(realpath)){
									file.CopyTo(dest);
									dest.Flush();
								}
							}
						}else{
							Log.d(
								"create folder {0} from {1}",
								realpath,entry.FullName
							);
							Directory.CreateDirectory(realpath);
						}
					}catch(Exception e){
						Log.w("extract file {0} failed",entry.FullName);
						Log.w(e);
					}
				}
			}
			Log.d("extract done");
			if(!found)MessageBox.Show(
				string.Format("处理器架构{0}不支持",arch),
				"错误",MessageBoxButtons.OK,MessageBoxIcon.Error
			);
			AppDomain.CurrentDomain.AssemblyResolve+=new ResolveEventHandler(LoadFromFolder);
		}
		[STAThread]
		static void Main(){
			Log.i("starting");
			if(!Directory.Exists(Utils.DataDir))
				Directory.CreateDirectory(Utils.DataDir);
			Log.Initialize();
			ExtractResources();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}
	}
}
