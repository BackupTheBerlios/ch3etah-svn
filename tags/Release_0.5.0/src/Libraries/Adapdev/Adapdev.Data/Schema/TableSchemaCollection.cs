
using System;
using System.Collections;
using Adapdev.Data.Schema;

namespace Adapdev.Data.Schema
{
	using System.Text;
	using Adapdev.Text;

	[Serializable()]
	public class TableSchemaCollection : CollectionBase 
	{

		public TableSchemaCollection() 
		{
		}

		public TableSchemaCollection(IList value) 
		{
			this.AddRange(value);
		}

		public TableSchemaCollection(TableSchema[] value) 
		{
			this.AddRange(value);
		}

		public TableSchema this[int index] 
		{
			get 
			{
				return ((TableSchema)(List[index]));
			}
			set 
			{
				List[index] = value;
			}
		}

		public int Add(TableSchema value) 
		{
			return List.Add(value);
		}

		public void AddRange(TableSchema[] value) 
		{
			for (int i = 0; (i < value.Length); i = (i + 1)) 
			{
				this.Add(value[i]);
			}
		}

		public void AddRange(IList value) 
		{
			for (int i = 0; (i < value.Count); i = (i + 1)) 
			{
				this.Add((TableSchema)value[i]);
			}
		}

		public bool Contains(TableSchema value) 
		{
			return List.Contains(value);
		}

		public void CopyTo(TableSchema[] array, int index) 
		{
			List.CopyTo(array, index);
		}

		public int IndexOf(TableSchema value) 
		{
			return List.IndexOf(value);
		}

		public void Insert(int index, TableSchema value) 
		{
			List.Insert(index, value);
		}

		public new TableSchemaEnumerator GetEnumerator() 
		{
			return new TableSchemaEnumerator(this);
		}

		public void Remove(TableSchema value) 
		{
			List.Remove(value);
		}

	}
}
