using System.Collections;
using System.ComponentModel;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Design.Converters {
	/// <summary>
	/// Summary description for EntitiesNameConverter.
	/// </summary>
	public class EntitiesNameConverter : StringConverter {
		public EntitiesNameConverter() {}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			try {
				return new StandardValuesCollection(GetEntities());
			}
			catch {
				return null;
			}
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
			return false;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
			return true;
		}

		public static IList GetEntities() {
			ArrayList list = new ArrayList();

			foreach (MetadataFile tmpFiles in Project.CurrentProject.MetadataFiles) {
				foreach (MetadataEntityBase tmpEntityBase in tmpFiles.MetadataEntities) {
					if (! list.Contains(tmpEntityBase.Name)) {
						list.Add(tmpEntityBase.Name);
					}
				}
			}

			// Sort List
			list.Sort(0, list.Count, null);

			return list;
		}
	}
}