using System;
using System.Collections;
using System.ComponentModel;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Design.Converters
{
	/// <summary>
	/// Summary description for IndexFieldsNameConverter.
	/// </summary>
	public class IndexFieldsNameConverter : StringConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) 
		{
			try 
			{
				return new StandardValuesCollection(GetFields((IndexOrderField) context.Instance));
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
		
		public static IList GetFields(IndexOrderField sender) 
		{
			ArrayList list = new ArrayList();

			foreach (IndexField field in sender.Index.Fields)
			{
				list.Add(field.Name);
			}

			// Sort List
			list.Sort(0, list.Count, null);

			return list;
		}
	}
}
