/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 *
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 24/12/2004
 */
 

using System;
using System.Collections;
using System.Xml.Serialization;

namespace Ch3Etah.Metadata.OREntities
{
	/// <summary>
	/// Description of IndexFieldCollection.
	/// </summary>
	public class IndexFieldCollection : Ch3Etah.Metadata.OREntities.Generated.IndexFieldCollection
	{
		#region Index
		private Index _index;

		[XmlIgnore()]
		internal Index Index
		{
			get { return _index; }
			set 
			{
				_index = value;
				foreach(IndexField val in this.List) 
				{
					val.SetIndex(_index);
				}
			}
		}
		#endregion Index
		
		#region Overridden properties and methods
		new public int Add(IndexField val)
		{
			if (this._index != null) 
			{
				val.SetIndex(this._index);
			}
			if ( !this.Contains(val) ) 
			{
				return base.Add(val);
			}
			return -1;
		}

		new public void Insert(int index, IndexField val)
		{
			if (this._index != null) 
			{
				val.SetIndex(this._index);
			}
			if ( this.Contains(val) ) 
			{
				this.Remove(val);
			}
			this.Insert(index, val);
		}

		new public IndexFieldCollectionEnumerator GetEnumerator()
		{
			IndexFieldCollectionEnumerator e = new IndexFieldCollectionEnumerator(this);
			e.Index = this.Index;
			return e;
		}

		new public IndexField this[int index] 
		{
			get 
			{
				if (this._index != null) 
				{
					((IndexField)(List[index])).SetIndex(this._index);
				}
				return ((IndexField)(List[index]));
			}
			set 
			{
				List[index] = (IndexField)value;
				if (this._index != null) 
				{
					((IndexField)(List[index])).SetIndex(this._index);
				}
			}
		}
		#endregion Overridden properties and methods

		#region IndexFieldCollectionEnumerator class
		new public class IndexFieldCollectionEnumerator : Ch3Etah.Metadata.OREntities.Generated.IndexFieldCollection.IndexFieldCollectionEnumerator, IEnumerator
		{
			public IndexFieldCollectionEnumerator(IndexFieldCollection mappings) : base(mappings)
			{
			}
			
			#region Index
			private Index _index;

			[XmlIgnore()]
			internal Index Index
			{
				get { return _index; }
				set { _index = value; }
			}
			#endregion Index
			
			new public IndexField Current 
			{
				get 
				{
					if (this._index != null) 
					{
						base.Current.SetIndex(this._index);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current 
			{
				get 
				{
					if (this._index != null) 
					{
						base.Current.SetIndex(this._index);
					}
					return base.Current;
				}
			}
		}
		#endregion IndexFieldCollectionEnumerator class

		protected override void OnInsert(int index, object value)
		{
			((IndexField) value).SetIndex(_index);
			base.OnInsert(index, value);
		}

	}
}
