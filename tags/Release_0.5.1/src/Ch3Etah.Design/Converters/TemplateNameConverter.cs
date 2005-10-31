using System.Collections;
using System.ComponentModel;
using System.Xml;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Design.Converters {
	/// <summary>
	/// Summary description for TemplateNameConverter.
	/// </summary>
	public class TemplateNameConverter : StringConverter {
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			try {
				CodeGeneratorCommand command = (CodeGeneratorCommand) context.Instance;
				return new StandardValuesCollection(GetTemplates(command.Package));
			}
			catch {
				return null;
			}
		}

		public static ICollection GetTemplates(string packageName) {
			ArrayList list = new ArrayList();
			XmlDocument doc = new XmlDocument();
			Project project = Project.CurrentProject;

			doc.Load(project.GetFullTemplatePath() + "\\" + packageName);

			foreach (XmlAttribute attr in doc.SelectNodes("/Package/Templates/Template/@Name")) {
				list.Add(attr.Value);
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