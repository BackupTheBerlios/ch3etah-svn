using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections;

namespace Ch3Etah.Core.ProjectLib {
	/// <summary>
	/// Summary description for DataSourceCollection.
	/// </summary>
	public class DataSourceCollection: Generated.DataSourceCollection {

		private Project _project;
		
		public DataSourceCollection() {
		}

		[XmlIgnore()]
		internal Project Project {
			get {
				return _project;
			}
			set {
				_project = value;
				foreach(DataSource ds in this.List) {
					ds.SetProject(this.Project);
				}
			}
		}
		
		#region Overridden properties and methods
		new public int Add(DataSource val) {
			val.SetProject(Project);
			if ( !this.Contains(val) ) {
				return base.Add(val);
			}
			return -1;
		}
		
		[Browsable(false)]
		new public int Count {
			get {
				return base.Count;
			}
		}
		
		new public void Insert(int index, DataSource val) {
			val.SetProject(Project);
			if ( this.Contains(val) ) {
				base.Remove(val);
			}
			base.Insert(index, val);
		}

		new public DataSourceEnumerator GetEnumerator() {
			DataSourceEnumerator e = new DataSourceEnumerator(this);
			e.Project = Project;
			return e;
		}

		new public DataSource this[int index] {
			get {
				((DataSource)(List[index])).SetProject(Project);
				return ((DataSource)(List[index]));
			}
			set {
				List[index] = (DataSource)value;
				((DataSource)(List[index])).SetProject(Project);
			}
		}
		#endregion Overridden properties and methods
		
		#region DataSourceEnumerator class
		new public class DataSourceEnumerator : Ch3Etah.Core.ProjectLib.Generated.DataSourceCollection.DataSourceEnumerator, IEnumerator {
			Project _project;
			
			public DataSourceEnumerator(DataSourceCollection mappings) : base(mappings) {
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

			new public DataSource Current {
				get {
					base.Current.SetProject(Project);
					return base.Current;
				}
			}
			
			object IEnumerator.Current {
				get {
					base.Current.SetProject(Project);
					return base.Current;
				}
			}
		}
		#endregion DataSourceEnumerator class

		public DataSource Find(String name)
		{
			// Looping
			foreach (DataSource tmpDataSource in this)
			{
				if (tmpDataSource.Name.Equals(name))
				{
					return tmpDataSource;
				}
			}

			// Return Null Value
			return null;
		}
	}
}
