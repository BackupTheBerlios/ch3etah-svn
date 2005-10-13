using System;
using System.Collections;
using System.ComponentModel;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Design.Converters
{
	/// <summary>
	/// Summary description for FieldsNameConverter.
	/// </summary>
	public class FieldsNameConverter: StringConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) 
		{
			try 
			{
				if (context.Instance is LinkField)
				{
					return new StandardValuesCollection(GetFields((LinkField) context.Instance));
				}
				else if (context.Instance is IndexField)
				{
					return new StandardValuesCollection(GetFields((IndexField) context.Instance));
				}
				else
				{
					return null;
				}
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
		
		public static IList GetFields(LinkField sender) 
		{
			ArrayList list = new ArrayList();

			foreach (EntityField field in sender.Link.Entity.Fields)
			{
				list.Add(field.Name);
			}

			// Sort List
			list.Sort(0, list.Count, null);

			return list;
		}

		public static IList GetFields(IndexField sender) 
		{
			ArrayList list = new ArrayList();

			foreach (EntityField field in sender.Index.Entity.Fields)
			{
				list.Add(field.Name);
			}

			// Sort List
			list.Sort(0, list.Count, null);

			return list;
		}

	}
}
