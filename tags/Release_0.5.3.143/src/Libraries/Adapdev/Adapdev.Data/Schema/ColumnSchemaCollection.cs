
using System;
using System.Collections;
using Adapdev.Data.Schema;

namespace Adapdev.Data.Schema
{
	[Serializable()]
	public class ColumnSchemaCollection : CollectionBase 
	{

		public ColumnSchemaCollection() 
		{
		}

		public ColumnSchemaCollection(IList value) 
		{
			this.AddRange(value);
		}

		public ColumnSchemaCollection(ColumnSchema[] value) 
		{
			this.AddRange(value);
		}

		public ColumnSchema this[int index] 
		{
			get 
			{
				return ((ColumnSchema)(List[index]));
			}
			set 
			{
				List[index] = value;
			}
		}

		public int Add(ColumnSchema value) 
		{
			return List.Add(value);
		}

		public void AddRange(ColumnSchema[] value) 
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
				this.Add((ColumnSchema)value[i]);
			}
		}

		public bool Contains(ColumnSchema value) 
		{
			return List.Contains(value);
		}

		public void CopyTo(ColumnSchema[] array, int index) 
		{
			List.CopyTo(array, index);
		}

		public int IndexOf(ColumnSchema value) 
		{
			return List.IndexOf(value);
		}

		public void Insert(int index, ColumnSchema value) 
		{
			List.Insert(index, value);
		}

		public new ColumnSchemaEnumerator GetEnumerator() 
		{
			return new ColumnSchemaEnumerator(this);
		}

		public void Remove(ColumnSchema value) 
		{
			List.Remove(value);
		}

	}
}
