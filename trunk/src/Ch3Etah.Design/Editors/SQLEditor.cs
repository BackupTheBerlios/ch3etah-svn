using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Ch3Etah.Design.Editors
{
	/// <summary>
	/// Summary description for SQLEditor.
	/// </summary>
	public class SQLEditor: UITypeEditor
	{
		#region Fields

		private IWindowsFormsEditorService _Service;

		#endregion

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			// Create Object's
			SQLEditorUI oForm;

			if (context != null && context.Instance != null)
			{
				// Create Service
				this._Service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));

				if (this._Service != null)
				{
					// Validate Data
					if (value == null) {  value = ""; }

					// Open Form and Check Results
					oForm = new SQLEditorUI();
					oForm.SqlQuery = value.ToString();
					
					if (oForm.ShowDialog() == DialogResult.OK)
					{
						return oForm.SqlQuery;
					}
					else
					{
						return value;
					}
				}
			}

			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				return UITypeEditorEditStyle.Modal;
			}

			return base.GetEditStyle(context);
		}

	}
}
