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
 *   Date: 14/9/2004
 */


using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Description of MetadataFiles.	
	/// </summary>
	public class MetadataFileCollection : Ch3Etah.Core.ProjectLib.Generated.MetadataFileCollection
	{
		private Project _project;
		
		public MetadataFileCollection()
		{
		}
		
		[XmlIgnore()]
		internal Project Project {
			get {
				return _project;
			}
			set {
				_project = value;
				foreach(MetadataFile file in this.List) {
					file.SetProject(this.Project);
				}
			}
		}
		
		public MetadataFile GetMetadataFile(Guid guid) {
			foreach (MetadataFile file in List) {
				if (file.Guid == guid) {
					return file;
				}
			}
			return null;
		}
		
		public MetadataFile GetMetadataFile(string fileName) {
			foreach (MetadataFile file in List) {
				if (file.Name == Path.GetFileName(fileName)) {
					return file;
				}
			}
			return null;
		}
		
		// TODO: Notify other objects when a metadata file is added or removed
		
		#region Overridden properties and methods
		new public int Add(MetadataFile val)
		{
			if (this.Project != null) {
				val.SetProject(this.Project);
			}
			if ( !this.Contains(val) ) {
				if (this.Project != null && !Project.IsLoading) {
					foreach (IMetadataFileObserver observer in this.Project.MetadataFileObservers) {
						observer.OnMetadataFileAdded(val);
					}
				}
				return base.Add(val);
			}
			else {
				return -1;
			}
		}
		
		new public void Remove(MetadataFile val) {
			if (this.Project != null && !Project.IsLoading) {
				foreach (IMetadataFileObserver observer in this.Project.MetadataFileObservers) {
					observer.OnMetadataFileRemoved(val);
				}
			}
			base.Remove(val);
		}
		
		new public bool Contains (MetadataFile file) {
			return ( GetMetadataFile(file.Guid) != null );
		}
		
		public bool Contains (Guid guid) {
			return ( GetMetadataFile(guid) != null );
		}
		
		public bool Contains (string fileName) {
			return ( GetMetadataFile(fileName) != null );
		}
		
		[Browsable(false)]
		new public int Count {
			get {
				return base.Count;
			}
		}
		
		new public void Insert(int index, MetadataFile val)
		{
			if (this.Project != null) {
				val.SetProject(this.Project);
			}
			if ( this.Contains(val) ) {
				base.Remove(val);
			}
			base.Insert(index, val);
		}

		new public MetadataFileEnumerator GetEnumerator()
		{
			MetadataFileEnumerator e = new MetadataFileEnumerator(this);
			e.Project = Project;
			return e;
		}

		public override MetadataFile this[int index] {
			get {
				if (this.Project != null) {
					((MetadataFile)(List[index])).SetProject(Project);
				}
				return ((MetadataFile)(List[index]));
			}
			set {
				List[index] = (MetadataFile)value;
				if (this.Project != null) {
					((MetadataFile)(List[index])).SetProject(Project);
				}
			}
		}
		#endregion Overridden properties and methods

		#region ListChanged handling
		public event ListChangedEventHandler ListChanged;//(object sender, ListChangedEventArgs e);
		protected override void OnClearComplete() {
			Debug.WriteLine("Cleared metadata file list.");
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
			}
			catch{}
		}
		protected override void OnInsertComplete(int index, object value) {
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			catch{}
		}
		protected override void OnRemoveComplete(int index, object value) {
			Debug.WriteLine("Removed metadata file '" + ((MetadataFile)value).Name + "' at index " + index);
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			catch{}
		}
		protected override void OnSetComplete(int index, object oldValue, object newValue) {
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
			catch{}
		}
		#endregion ListChanged handling
		
		#region MetadataFileEnumerator class
		new public class MetadataFileEnumerator : Ch3Etah.Core.ProjectLib.Generated.MetadataFileCollection.MetadataFileEnumerator, IEnumerator
		{
			Project _project;
			
			public MetadataFileEnumerator(MetadataFileCollection mappings) : base(mappings)
			{
			}
			
			[XmlIgnore()]
			internal Project Project {
				get {
					return _project;
				}
				set {
					_project = value;
				}
			}

			new public MetadataFile Current {
				get {
					if (this.Project != null) {
						base.Current.SetProject(Project);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current {
				get {
					if (this.Project != null) {
						base.Current.SetProject(Project);
					}
					return base.Current;
				}
			}
		}
		#endregion MetadataFileEnumerator class
		
	}
}
