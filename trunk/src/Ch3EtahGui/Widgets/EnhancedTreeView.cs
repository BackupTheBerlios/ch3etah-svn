using System;
using System.Windows.Forms;
using Reflector.UserInterface;

namespace Ch3Etah.Gui.Widgets {
	public class EnhancedTreeView : TreeView {
		
		#region Member variables and constructors

		#region Member variables

		protected readonly CommandBarContextMenu treeviewContextMenu = new CommandBarContextMenu();

		#endregion

		public EnhancedTreeView() {
			treeviewContextMenu.Popup += new EventHandler(treeviewContextMenu_Popup);
		}

		#endregion

		#region Overrides

		protected override void OnContextMenuChanged(EventArgs e) {
			base.OnContextMenuChanged(e);
		}

		public override ContextMenu ContextMenu {
			get { return treeviewContextMenu; }
			set {  }
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				SelectedNode = GetNodeAt(e.X, e.Y);
			}
			base.OnMouseDown(e);
		}

		#endregion

		#region Events

		private void treeviewContextMenu_Popup(object sender, EventArgs e) {
			
			// Try to get the verbs from the component associated to the selected node, if any
		}

		#endregion
	}
}