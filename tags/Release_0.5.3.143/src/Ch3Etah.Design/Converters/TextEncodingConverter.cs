using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;

using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Design.Converters
{
	/// <summary>
	/// Summary description for TextEncodingConverter.
	/// </summary>
	public class TextEncodingConverter : StringConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) 
		{
			try 
			{
				return new StandardValuesCollection(GetEncodings());
			}
			catch 
			{
				return null;
			}
		}

		public static ICollection GetEncodings()
		{
			ArrayList list = new ArrayList();
			
			list.Add("utf-8");
			list.Add("utf-7");
			list.Add("utf-16");
			list.Add("iso-8859-1");
			list.Add("us-ascii");
//			foreach (Encoding enc in Encoding.GetEncodings) // .NET 2.0 only
//			{
//				list.Add(enc.Name);
//			}
			return list;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) 
		{
			return false;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) 
		{
			return true;
		}
	}
}
