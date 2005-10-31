using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Ch3Etah.Design.CustomUI
{
	public class PhysicalPathDialog: UITypeEditor
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
			FolderBrowserDialog oFolderBrowserDialog;

			// Instancia oFolderBrowserDialog
			oFolderBrowserDialog = new FolderBrowserDialog();
			
			// Configura
			oFolderBrowserDialog.ShowNewFolderButton = true;
			oFolderBrowserDialog.Description = "Folder Selection";

			// Seta (Se já existir) o diretório
			if (value != null && ! value.ToString().Trim().Equals(String.Empty))
			{
				if (System.IO.Directory.Exists(value.ToString().Trim()))
				{
					oFolderBrowserDialog.SelectedPath = value.ToString();
				}
			}

			// Mostra Dialog
			if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				sReturnData = oFolderBrowserDialog.SelectedPath;
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
