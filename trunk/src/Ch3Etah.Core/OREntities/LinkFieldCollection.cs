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
	/// Description of LinkFieldCollection.
	/// </summary>
	public class LinkFieldCollection : Ch3Etah.Metadata.OREntities.Generated.LinkFieldCollection
	{
		#region Link
		private Link _link;

		[XmlIgnore()]
		internal Link Link
		{
			get { return _link; }
			set 
			{
				_link = value;
				foreach(LinkField val in this.List) 
				{
					val.SetLink(_link);
				}
			}
		}
		#endregion Link
		
		#region Overridden properties and methods

		protected override void OnInsert(int index, object value)
		{
		 	((LinkField) value).SetLink(this._link);
			base.OnInsert(index, value);
		}

		public override int Add(LinkField val)
		{
			if (this._link != null) 
			{
				val.SetLink(this._link);
			}
			if ( !this.Contains(val) ) 
			{
				return base.Add(val);
			}
			return -1;
		}

		new public void Insert(int link, LinkField val)
		{
			if (this._link != null) 
			{
				val.SetLink(this._link);
			}
			if ( this.Contains(val) ) 
			{
				this.Remove(val);
			}
			this.Insert(link, val);
		}

		new public LinkFieldCollectionEnumerator GetEnumerator()
		{
			LinkFieldCollectionEnumerator e = new LinkFieldCollectionEnumerator(this);
			e.Link = this.Link;
			return e;
		}

		new public LinkField this[int link] 
		{
			get 
			{
				if (this._link != null) 
				{
					((LinkField)(List[link])).SetLink(this._link);
				}
				return ((LinkField)(List[link]));
			}
			set 
			{
				List[link] = (LinkField)value;
				if (this._link != null) 
				{
					((LinkField)(List[link])).SetLink(this._link);
				}
			}
		}
		#endregion Overridden properties and methods

		#region LinkFieldCollectionEnumerator class
		new public class LinkFieldCollectionEnumerator : Ch3Etah.Metadata.OREntities.Generated.LinkFieldCollection.LinkFieldCollectionEnumerator, IEnumerator
		{
			public LinkFieldCollectionEnumerator(LinkFieldCollection mappings) : base(mappings)
			{
			}
			
			#region Link
			private Link _link;

			[XmlIgnore()]
			internal Link Link
			{
				get { return _link; }
				set { _link = value; }
			}
			#endregion Link
			
			new public LinkField Current 
			{
				get 
				{
					if (this._link != null) 
					{
						base.Current.SetLink(this._link);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current 
			{
				get 
				{
					if (this._link != null) 
					{
						base.Current.SetLink(this._link);
					}
					return base.Current;
				}
			}
		}
		#endregion LinkFieldCollectionEnumerator class

	}
}
