namespace Adapdev.Data.Schema
{
	using System;
	using System.Collections;
	using System.Text;

	/// <summary>
	/// Strongly-typed collection for ColumnSchemas
	/// </summary>
	/// 
	[Serializable]
	public class ColumnSchemaDictionary : DictionaryBase
	{
		public ColumnSchema this[String key]
		{
			get { return ((ColumnSchema) Dictionary[key]); }
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

		public void Add(String key, ColumnSchema value)
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
			foreach (ColumnSchema ci in this.Values)
			{
				sb.Append(ci.ToString());
			}
			return sb.ToString();
		}


	}
}