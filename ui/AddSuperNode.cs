using System;
using System.Windows.Forms;
namespace GuiN2N{
	public partial class AddSuperNode:Form{
		internal Main.SuperNode node=null;
		public AddSuperNode():this(new Main.SuperNode()){}
		public AddSuperNode(Main.SuperNode node){
			InitializeComponent();
			this.node=node;
		}
		private void btnOK_Click(object sender,EventArgs e)=>node.Set(this);
	}
}
