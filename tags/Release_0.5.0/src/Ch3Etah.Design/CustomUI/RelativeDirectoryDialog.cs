using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Ch3Etah.Design.CustomUI
{
	public class RelativeDirectoryDialog : UITypeEditor
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
			String sCurrentPath = String.Empty;
			String sPhysicalPath = String.Empty;

			// Instancia oFolderBrowserDialog
			oFolderBrowserDialog = new FolderBrowserDialog();

			// Recupera Path Atual
			sCurrentPath = AppDomain.CurrentDomain.BaseDirectory;

			// Configura
			oFolderBrowserDialog.ShowNewFolderButton = true;
			oFolderBrowserDialog.Description = "Folder Selection";

			// Seta (Se já existir) o diretório
			if (value != null && ! value.ToString().Trim().Equals(String.Empty))
			{
				// Calcula Path Física pela Path Relativa Atual
				sPhysicalPath = Path.GetFullPath(value.ToString().Trim());

				if (Directory.Exists(sPhysicalPath))
				{
					oFolderBrowserDialog.SelectedPath = sPhysicalPath;
				}
			}

			// Mostra Dialog
			if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				// Recupera
				sReturnData = oFolderBrowserDialog.SelectedPath;

				// Calcula Caminho Relativo
				sReturnData = this.EvaluateRelativePath(sCurrentPath, sReturnData);

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
		
	
		private string EvaluateRelativePath(string sourcePath, string destinationPath)
		{
			string[]
				firstPathParts = sourcePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
			string[]
				secondPathParts = destinationPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

			int sameCounter = 0;
			for (int i = 0;
				i < Math.Min(firstPathParts.Length,
				secondPathParts.Length);
				i++)
			{
				if (
					!firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()))
				{
					break;
				}
				sameCounter++;
			}

			if (sameCounter == 0)
			{
				return destinationPath;
			}

			string newPath = String.Empty;
			for (int i = sameCounter; i < firstPathParts.Length; i++)
			{
				if (i > sameCounter)
				{
					newPath += Path.DirectorySeparatorChar;
				}
				newPath += "..";
			}
			if (newPath.Length == 0)
			{
				newPath = ".";
			}
			for (int i = sameCounter; i < secondPathParts.Length; i++)
			{
				newPath += Path.DirectorySeparatorChar;
				newPath += secondPathParts[i];
			}
			return newPath;
		}

	}
}