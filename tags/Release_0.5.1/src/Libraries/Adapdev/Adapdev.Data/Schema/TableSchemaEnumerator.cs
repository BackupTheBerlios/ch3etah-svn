using System;
using System.Collections;

namespace Adapdev.Data.Schema
{
	public class TableSchemaEnumerator : IEnumerator 
	{

		private IEnumerator baseEnumerator;
		private IEnumerable temp;

		public TableSchemaEnumerator(TableSchemaCollection mappings) 
		{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
		}

		public TableSchemaEnumerator(TableSchemaDictionary mappings) 
		{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
		}

		public TableSchema Current 
		{
			get 
			{
				return ((TableSchema)(baseEnumerator.Current));
			}
		}

		object IEnumerator.Current 
		{
			get 
			{
				return baseEnumerator.Current;
			}
		}

		public bool MoveNext() 
		{
			return baseEnumerator.MoveNext();
		}

		bool IEnumerator.MoveNext() 
		{
			return baseEnumerator.MoveNext();
		}

		public void Reset() 
		{
			baseEnumerator.Reset();
		}

		void IEnumerator.Reset() 
		{
			baseEnumerator.Reset();
		}
	}

}

