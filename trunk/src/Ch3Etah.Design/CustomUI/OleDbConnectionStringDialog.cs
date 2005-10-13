using System;
using System.ComponentModel;
using System.Drawing.Design;
using ADODB;
using MSDASC;

namespace Ch3Etah.Design.CustomUI {
	public class OleDbConnectionStringDialog : UITypeEditor {
		public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
			return false;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			if (context != null && context.Instance != null)
				return UITypeEditorEditStyle.Modal;

			return base.GetEditStyle(context);
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
			DataLinks dl = new DataLinks();
			object cn;

			if (value != null && !value.Equals("")) {
				cn = new Connection();
				((Connection) cn).ConnectionString = value.ToString();
				dl.PromptEdit(ref cn);
			}
			else cn = dl.PromptNew();

			return ((Connection) cn).ConnectionString;
		}
	}
}