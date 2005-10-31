using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Design.Converters
{
	/// <summary>
	/// Summary description for PackageNameConverter.
	/// </summary>
	public class PackageNameConverter: StringConverter {

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			try {
				return new StandardValuesCollection(GetPackages());
			}
			catch {
				return null;
			}
		}

		public static ICollection GetPackages()
		{
			ArrayList list = new ArrayList();
			
			Project project = Project.CurrentProject;
			DirectoryInfo templatesDir = new DirectoryInfo(project.GetFullTemplatePath());
			FileInfo[] files = templatesDir.GetFiles("*.xml");
			foreach (FileInfo file in files)
			{
				list.Add(file.Name);
			}
			
			return list;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
			return true;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
			return true;
		}
		
	}
}
