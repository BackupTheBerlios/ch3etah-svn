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
using System.ComponentModel;
using System.Xml.Serialization;
using Ch3Etah.Core.ComponentModel;

namespace Ch3Etah.Metadata.OREntities
{
	/// <summary>
	/// Description of IndexCollection.
	/// </summary>
	public class IndexCollection : Ch3Etah.Metadata.OREntities.Generated.IndexCollection
	{
		#region Entity
		private Entity _entity;

		[XmlIgnore()]
		internal Entity Entity
		{
			get { return _entity; }
			set 
			{
				_entity = value;
				foreach(Index val in this.List) 
				{
					val.SetEntity(_entity);
				}
			}
		}
		#endregion Entity
		
		public Index FindByDBName(string dbName)
		{
			foreach (Index val in this)
			{
				if (val.DBName == dbName) return val;
			}
			return null;
		}

		#region Overridden properties and methods
		new public int Add(Index val)
		{
			if (this._entity != null) 
			{
				val.SetEntity(this._entity);
			}
			if ( !this.Contains(val) ) 
			{
				return base.Add(val);
			}
			return -1;
		}

		new public void Insert(int index, Index val)
		{
			if (this._entity != null) 
			{
				val.SetEntity(this._entity);
			}
			if ( this.Contains(val) ) 
			{
				this.Remove(val);
			}
			this.Insert(index, val);
		}

		new public IndexCollectionEnumerator GetEnumerator()
		{
			IndexCollectionEnumerator e = new IndexCollectionEnumerator(this);
			e.Entity = this.Entity;
			return e;
		}

		new public Index this[int index] 
		{
			get 
			{
				if (this._entity != null) 
				{
					((Index)(List[index])).SetEntity(this._entity);
				}
				return ((Index)(List[index]));
			}
			set 
			{
				List[index] = (Index)value;
				if (this._entity != null) 
				{
					((Index)(List[index])).SetEntity(this._entity);
				}
			}
		}
		#endregion Overridden properties and methods

		#region IndexCollectionEnumerator class
		new public class IndexCollectionEnumerator : Ch3Etah.Metadata.OREntities.Generated.IndexCollection.IndexCollectionEnumerator, IEnumerator
		{
			public IndexCollectionEnumerator(IndexCollection mappings) : base(mappings)
			{
			}
			
			#region Entity
			private Entity _entity;

			[XmlIgnore()]
			internal Entity Entity
			{
				get { return _entity; }
				set { _entity = value; }
			}
			#endregion Entity
			
			new public Index Current 
			{
				get 
				{
					if (this._entity != null) 
					{
						base.Current.SetEntity(this._entity);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current 
			{
				get 
				{
					if (this._entity != null) 
					{
						base.Current.SetEntity(this._entity);
					}
					return base.Current;
				}
			}
		}
		#endregion IndexCollectionEnumerator class

		protected override void OnInsert(int index, object value)
		{
			((Index) value).SetEntity(_entity);
			base.OnInsert(index, value);
		}
	}
}
