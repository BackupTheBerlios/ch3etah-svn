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
 *   Date: 22/9/2004
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Description of MetadataEntities.	
	/// </summary>
	public class MetadataEntityCollection : Ch3Etah.Core.Metadata.Generated.MetadataEntityCollection
	{
		private const int INITIAL_XML_BUFFER = 4096;

		protected MetadataFile _owningMetadataFile;
		
		public MetadataEntityCollection()
		{
		}
		
		[XmlIgnore()]
		internal MetadataFile OwningMetadataFile {
			get { return _owningMetadataFile; }
			set {
				_owningMetadataFile = value;
				foreach(IMetadataEntity entity in this.List) {
					entity.OwningMetadataFile = value;
				}
			}
		}

		public string SaveAsXmlString() {
			StringBuilder sb = new StringBuilder(INITIAL_XML_BUFFER);
			foreach (IMetadataEntity entity in this ) {
				sb.Append(entity.SaveAsXmlString());
			}
			return sb.ToString();
		}
		
		public bool IsDirty {
			get {
				foreach (IMetadataEntity entity in this) {
					if (entity.IsDirty) {
						return true;
					}
				}
				return false;
			}
		}
		
		#region Overridden properties and methods
		new public int Add(IMetadataEntity val) {
			if (this._owningMetadataFile != null) {
				val.OwningMetadataFile = this._owningMetadataFile;
			}
			if ( !this.Contains(val) ) {
				return base.Add(val);
			}
			else {
				return -1;
			}
		}
		
		new public void Remove(IMetadataEntity val) {
			base.Remove(val);
		}
		
		[Browsable(false)]
		new public int Count {
			get {
				return base.Count;
			}
		}
		
		new public void Insert(int index, IMetadataEntity val) {
			if (this._owningMetadataFile != null) {
				val.OwningMetadataFile = this._owningMetadataFile;
			}
			if ( this.Contains(val) ) {
				base.Remove(val);
			}
			base.Insert(index, val);
		}

		new public IMetadataEntityEnumerator GetEnumerator() {
			IMetadataEntityEnumerator e = new IMetadataEntityEnumerator(this);
			e.OwningMetadataFile = this._owningMetadataFile;
			return e;
		}

		new public IMetadataEntity this[int index] {
			get {
				if (this._owningMetadataFile != null) {
					((IMetadataEntity)(List[index])).OwningMetadataFile = this._owningMetadataFile;
				}
				return ((IMetadataEntity)(List[index]));
			}
			set {
				List[index] = (IMetadataEntity)value;
				if (this._owningMetadataFile != null) {
					((IMetadataEntity)(List[index])).OwningMetadataFile = this._owningMetadataFile;
				}
			}
		}
		#endregion Overridden properties and methods

		#region ListChanged handling
//		public event ListChangedEventHandler ListChanged;//(object sender, ListChangedEventArgs e);
//		protected override void OnClearComplete() {
//			Debug.WriteLine("Cleared metadata file list.");
//			try {
//				ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
//			}
//			catch{}
//		}
//		protected override void OnInsertComplete(int index, object value) {
//			try {
//				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
//			}
//			catch{}
//		}
//		protected override void OnRemoveComplete(int index, object value) {
//			Debug.WriteLine("Removed metadata file '" + ((IMetadataEntity)value).Name + "' at index " + index);
//			try {
//				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
//			}
//			catch{}
//		}
//		protected override void OnSetComplete(int index, object oldValue, object newValue) {
//			try {
//				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
//			}
//			catch{}
//		}
		#endregion ListChanged handling
		
		#region IMetadataEntityEnumerator class
		new public class IMetadataEntityEnumerator : Ch3Etah.Core.Metadata.Generated.MetadataEntityCollection.IMetadataEntityEnumerator, IEnumerator {
			protected MetadataFile _owningMetadataFile;
			
			public IMetadataEntityEnumerator(MetadataEntityCollection mappings) : base(mappings) {
			}
			
			[XmlIgnore()]
			internal MetadataFile OwningMetadataFile {
				get { return _owningMetadataFile; }
				set { _owningMetadataFile = value; }
			}

			new public IMetadataEntity Current {
				get {
					if (this._owningMetadataFile != null) {
						base.Current.OwningMetadataFile = this._owningMetadataFile;
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current {
				get {
					if (this._owningMetadataFile != null) {
						base.Current.OwningMetadataFile = this._owningMetadataFile;
					}
					return base.Current;
				}
			}
		}
		#endregion IMetadataEntityEnumerator class

	}
}
