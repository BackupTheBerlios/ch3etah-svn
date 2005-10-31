using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Design.Designers
{
	/// <summary>
	/// Summary description for DataSourceDesigner.
	/// </summary>
	public class DataSourceDesigner: ControlDesigner {

		public override DesignerVerbCollection Verbs {
			get {
				DesignerVerbCollection verbs = new DesignerVerbCollection( 
					new DesignerVerb[] {
										   new DesignerVerb("Test connection", new EventHandler(OnTestConnection)), 
										   new DesignerVerb("Edit", new EventHandler(OnEdit))
									   });
				return verbs;
			}
		}

		private void OnEdit(object sender, EventArgs e) {
			throw new NotImplementedException();
		}

		private void OnTestConnection(object sender, EventArgs e) {
			try 
			{
			((DataSource) Component).TestConnection();
			}
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message + "|" + ex.StackTrace,
					"Error - " + ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			MessageBox.Show("Connection tested successfully!",
				"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		
	}
}
