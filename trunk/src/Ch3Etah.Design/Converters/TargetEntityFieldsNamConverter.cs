using System.Collections;
using System.ComponentModel;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Design.Converters {

		public class TargetEntityFieldsNamConverter : StringConverter {

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			try {
				return new StandardValuesCollection(GetFields((LinkField) context.Instance));
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

		public static IList GetFields(LinkField sender) {
			ArrayList list = new ArrayList();

			foreach (MetadataFile tmpFiles in Project.CurrentProject.MetadataFiles) {
				foreach (MetadataEntityBase tmpEntityBase in tmpFiles.MetadataEntities) {
					if (tmpEntityBase.Name.Equals(sender.Link.TargetEntityName)) {
						foreach (Index index in ((Entity) tmpEntityBase).Indexes) {
							if (index.Name.Equals(sender.Link.TargetIndexName)) {
								foreach (IndexField field in index.Fields) {
									list.Add(field.Name);
								}
							}
						}

					}
				}
			}

			// Sort List
			list.Sort(0, list.Count, null);

			return list;
		}
	}
}