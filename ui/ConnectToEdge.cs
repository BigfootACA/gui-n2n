using System;
using System.Windows.Forms;
namespace GuiN2N{
	public partial class ConnectToEdge:Form{
		private static bool passwordChanged=false;
		public ConnectToEdge()=>InitializeComponent();
		private void btnManagementPasswordShow_Click(object sender,EventArgs e){
			if(txtManagementPassword.PasswordChar==0){
				txtManagementPassword.PasswordChar='*';
				btnManagementPasswordShow.Text="显示";
			}else{
				txtManagementPassword.PasswordChar='\0';
				btnManagementPasswordShow.Text="隐藏";
			}
		}
		private void txtManagementPassword_TextChanged(object sender,EventArgs e){
			if(passwordChanged)return;
			passwordChanged=true;
			if(txtManagementPassword.PasswordChar==0)
				btnManagementPasswordShow_Click(sender,e);
		}
		private void btnOK_Click(object sender,EventArgs e){
			if(txtManagementAddress.Text.Trim().Length>0)return;
			DialogResult=DialogResult.None;
			MessageBox.Show(
				"无效的管理地址","错误",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}
	}
}
