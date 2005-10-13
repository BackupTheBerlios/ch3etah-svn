using System.Collections;
using System.ComponentModel;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Design.Converters {
	/// <summary>
	/// Summary description for EntitiesIndexesNameConverter.
	/// </summary>
	public class EntitiesIndexesNameConverter : StringConverter {

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			try {
				return new StandardValuesCollection(GetIndexes((Link) context.Instance));
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

		public static IList GetIndexes(Link sourceLink) {
			ArrayList list = new ArrayList();

			foreach (MetadataFile tmpFiles in Project.CurrentProject.MetadataFiles) {
				foreach (MetadataEntityBase tmpEntityBase in tmpFiles.MetadataEntities) {
					if (tmpEntityBase.Name.Equals(sourceLink.TargetEntityName)) {
						foreach (Index tmpIndex in ((Entity) tmpEntityBase).Indexes) {
							list.Add(tmpIndex.Name);
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