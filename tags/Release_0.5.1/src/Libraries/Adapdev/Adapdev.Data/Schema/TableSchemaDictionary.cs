namespace Adapdev.Data.Schema
{
	using System;
	using System.Collections;
	using System.Text;

	[Serializable]
	public class TableSchemaDictionary : DictionaryBase
	{
		public TableSchema this[String key]
		{
			get { return ((TableSchema) Dictionary[key]); }
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

		public void Add(String key, TableSchema value)
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
			foreach (TableSchema ti in this.Values)
			{
				sb.Append(ti.ToString());
			}
			return sb.ToString();
		}

	}
}