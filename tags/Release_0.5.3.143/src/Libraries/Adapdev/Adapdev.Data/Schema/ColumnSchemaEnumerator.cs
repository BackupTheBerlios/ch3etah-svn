using System;
using System.Collections;

namespace Adapdev.Data.Schema
{
	public class ColumnSchemaEnumerator : IEnumerator 
	{

		private IEnumerator baseEnumerator;
		private IEnumerable temp;

		public ColumnSchemaEnumerator(ColumnSchemaCollection mappings) 
		{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
		}

		public ColumnSchemaEnumerator(ColumnSchemaDictionary mappings) 
		{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
		}

		public ColumnSchema Current 
		{
			get 
			{
				return ((ColumnSchema)(baseEnumerator.Current));
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

