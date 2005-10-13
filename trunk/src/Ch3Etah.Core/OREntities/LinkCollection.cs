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
	/// Description of LinkCollection.
	/// </summary>
	public class LinkCollection : Ch3Etah.Metadata.OREntities.Generated.LinkCollection
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
				foreach(Link val in this.List) 
				{
					val.SetEntity(_entity);
				}
			}
		}
		#endregion Entity
		
		#region Overridden properties and methods
		new public int Add(Link val)
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

		new public void Insert(int index, Link val)
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

		new public LinkCollectionEnumerator GetEnumerator()
		{
			LinkCollectionEnumerator e = new LinkCollectionEnumerator(this);
			e.Entity = this.Entity;
			return e;
		}

		new public Link this[int index] 
		{
			get 
			{
				if (this._entity != null) 
				{
					((Link)(List[index])).SetEntity(this._entity);
				}
				return ((Link)(List[index]));
			}
			set 
			{
				List[index] = (Link)value;
				if (this._entity != null) 
				{
					((Link)(List[index])).SetEntity(this._entity);
				}
			}
		}
		#endregion Overridden properties and methods

		#region LinkCollectionEnumerator class
		new public class LinkCollectionEnumerator : Ch3Etah.Metadata.OREntities.Generated.LinkCollection.LinkCollectionEnumerator, IEnumerator
		{
			public LinkCollectionEnumerator(LinkCollection mappings) : base(mappings)
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
			
			new public Link Current 
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
		#endregion LinkCollectionEnumerator class

	}
}
