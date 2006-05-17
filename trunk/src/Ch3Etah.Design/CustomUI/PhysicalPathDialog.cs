using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

using Ch3Etah.Core.ProjectLib;

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
			String sReturnData = String.Empty;
			FolderBrowserDialog oFolderBrowserDialog = new FolderBrowserDialog();
			oFolderBrowserDialog.ShowNewFolderButton = true;
			oFolderBrowserDialog.Description = "Folder Selection";

			// set the directory if it exists
			if (value != null && ! value.ToString().Trim().Equals(String.Empty))
			{
				string basedir = value.ToString().Trim();
				while (basedir.IndexOf(".\\") == 0)
				{
					basedir = basedir.Substring(2);
				}
				if (context.Instance is Project)
				{
					Project prj = (Project)context.Instance;
					basedir = Path.Combine(
						Path.GetDirectoryName(prj.FileName)
						, basedir);
				}
				
				if (System.IO.Directory.Exists(basedir))
				{
					oFolderBrowserDialog.SelectedPath = basedir;
				}
			}

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
			
			return sReturnData;
		}
	}
}
