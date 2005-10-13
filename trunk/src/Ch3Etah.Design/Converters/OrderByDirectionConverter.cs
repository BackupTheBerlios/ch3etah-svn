using System;
using System.Collections;
using System.ComponentModel;

namespace Ch3Etah.Design.Converters
{
	/// <summary>
	/// Summary description for OrderByDirectionConverter.
	/// </summary>
	public class OrderByDirectionConverter : StringConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) 
		{
			try 
			{
				return new StandardValuesCollection(GetDirection());
			}
			catch 
			{
				return null;
			}
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) 
		{
			return false;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) 
		{
			return true;
		}
		
		public static IList GetDirection() 
		{
			ArrayList list = new ArrayList();

//			foreach (String tmpEnumName in Enum.GetNames(typeof(Ch3Etah.Metadata.OREntities.IndexOrderField.eDirection)))
//			{
//				list.Add(tmpEnumName);
//			}

			// Sort List
			list.Sort(0, list.Count, null);

			return list;
		}
	}
}
