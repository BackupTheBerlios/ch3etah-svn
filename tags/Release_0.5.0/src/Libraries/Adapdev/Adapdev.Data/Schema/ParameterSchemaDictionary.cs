namespace Adapdev.Data.Schema
{
	using System;
	using System.Collections;
	using System.Text;

	/// <summary>
	/// Summary description for CacheMetaDataDictionary.
	/// </summary>
	/// 
	[Serializable]
	public class ParameterSchemaDictionary : DictionaryBase
	{
		public ParameterSchema this[String key]
		{
			get { return ((ParameterSchema) Dictionary[key]); }
			set { Dictionary[key] = value; }
		}

		public ICollection Keys
		{
			get { return (Dictionary.Keys); }
		}

		public ICollection Values
		{
			get { return (Dictionary.Values); }
		}

		public void Add(String key, ParameterSchema value)
		{
			Dictionary.Add(key, value);
		}

		public bool Contains(String key)
		{
			return (Dictionary.Contains(key));
		}

		public void Remove(String key)
		{
			Dictionary.Remove(key);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (ParameterSchema ci in this.Values)
			{
				sb.Append(ci.ToString());
			}
			return sb.ToString();
		}


	}
}