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

namespace Ch3Etah.Metadata.OREntities {
	/// <summary>
	/// Description of IndexOrderFieldCollection.
	/// </summary>
	public class IndexOrderFieldCollection : Ch3Etah.Metadata.OREntities.Generated.IndexOrderFieldCollection {
		#region Index
		private Index _index;

		[XmlIgnore()]
		internal Index Index {
			get { return _index; }
			set {
				_index = value;
				foreach(IndexOrderField val in this.List) {
					val.SetIndex(_index);
				}
			}
		}
		#endregion Index
		
		#region Overridden properties and methods
		new public int Add(IndexOrderField val) {
			if (this._index != null) {
				val.SetIndex(this._index);
			}
			if ( !this.Contains(val) ) {
				return base.Add(val);
			}
			return -1;
		}

		new public void Insert(int index, IndexOrderField val) {
			if (this._index != null) {
				val.SetIndex(this._index);
			}
			if ( this.Contains(val) ) {
				this.Remove(val);
			}
			this.Insert(index, val);
		}

		new public IndexOrderFieldCollectionEnumerator GetEnumerator() {
			IndexOrderFieldCollectionEnumerator e = new IndexOrderFieldCollectionEnumerator(this);
			e.Index = this.Index;
			return e;
		}

		new public IndexOrderField this[int index] {
			get {
				if (this._index != null) {
					((IndexOrderField)(List[index])).SetIndex(this._index);
				}
				return ((IndexOrderField)(List[index]));
			}
			set {
				List[index] = (IndexOrderField)value;
				if (this._index != null) {
					((IndexOrderField)(List[index])).SetIndex(this._index);
				}
			}
		}
		#endregion Overridden properties and methods

		#region IndexOrderFieldCollectionEnumerator class
		new public class IndexOrderFieldCollectionEnumerator : Ch3Etah.Metadata.OREntities.Generated.IndexOrderFieldCollection.IndexOrderFieldCollectionEnumerator, IEnumerator {
			public IndexOrderFieldCollectionEnumerator(IndexOrderFieldCollection mappings) : base(mappings) {
			}
			
			#region Index
			private Index _index;

			[XmlIgnore()]
			internal Index Index {
				get { return _index; }
				set { _index = value; }
			}
			#endregion Index
			
			new public IndexOrderField Current {
				get {
					if (this._index != null) {
						base.Current.SetIndex(this._index);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current {
				get {
					if (this._index != null) {
						base.Current.SetIndex(this._index);
					}
					return base.Current;
				}
			}
		}
		#endregion IndexOrderFieldCollectionEnumerator class

		protected override void OnInsert(int index, object value)
		{
			((IndexOrderField) value).SetIndex(_index);
			base.OnInsert(index, value);
		}
	}
}
