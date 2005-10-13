using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Ch3Etah.Design.CustomUI
{
	/// <summary>
	/// Summary description for XmlFileOpenDialog.
	/// </summary>
	public class XmlFileOpenDialog : UITypeEditor
	{
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}


		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				return UITypeEditorEditStyle.Modal;
			}

			return base.GetEditStyle(context);
		}

	
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			// Cria Objetos
			String sReturnData = String.Empty;
			OpenFileDialog oFileDialog;

			// Instancia oFileDialog
			oFileDialog = new OpenFileDialog();

			// Configura
			oFileDialog.CheckFileExists = false;
			oFileDialog.CheckPathExists = true;
			oFileDialog.Title = "File Load...";
			oFileDialog.Filter = "Xml Files (.XML)|*.xml|All Files (*.*)|*.*";

			// Seta (Se já existir) o diretório
			if (value != null && ! value.ToString().Trim().Equals(String.Empty))
			{
				if (File.Exists(value.ToString().Trim()))
				{
					oFileDialog.FileName = value.ToString().Trim();
				}
			}

			// Mostra Dialog
			if (oFileDialog.ShowDialog() == DialogResult.OK)
			{
				// Recupera
				sReturnData = oFileDialog.FileName;

			}
			else
			{
				if (value != null)
				{
					sReturnData = value.ToString();
				}
				else
				{
					sReturnData = String.Empty;
				}
			}

			// Retorna
			return sReturnData;
		}
	}
}
