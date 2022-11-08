using System.Drawing;
using System.Windows.Forms;
namespace GuiN2N{
	public partial class EdgeLogs:Form{
		public EdgeLogs()=>InitializeComponent();
		private void EdgeLogs_FormClosing(object sender,FormClosingEventArgs e){
			if(e.CloseReason==CloseReason.UserClosing){
				e.Cancel=true;
				Hide();
			}
		}
		public void LogsWrite(string content,bool newline,Color color){
			text.Select(text.TextLength,0);
			text.SelectionColor=color;
			text.AppendText(content);
			if(newline)text.AppendText("\r\n");
			text.ScrollToCaret();
		}
	}
}
