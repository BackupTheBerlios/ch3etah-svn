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
 *   User: Jacob Eggleston
 *   Date: 2005/7/10
 */

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

using Ch3Etah.Core.Config;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Core.ProjectLib {
	
	#region DataSourceNameChangedEvent
	public delegate void DataSourceNameChangedEventHandler(object sender, DataSourceNameChangedEventArgs e);

	public class DataSourceNameChangedEventArgs
	{
		public readonly DataSource DataSource;
		public readonly string OldName;
		public readonly string NewName;
		public DataSourceNameChangedEventArgs(DataSource ds, string oldName, string newName)
		{
			this.DataSource = ds;
			this.NewName = newName;
			this.OldName = oldName;
		}
	}
	#endregion DataSourceNameChangedEvent

	[Serializable()]
	[XmlInclude(typeof(OleDbDataSource))]
	[Designer("Ch3Etah.Design.Designers.DataSourceDesigner,Ch3Etah.Design", typeof(IDesigner))]
	public abstract class DataSource : IDataSource
	{
		#region DataSourceNameChangedEvent
		public static event DataSourceNameChangedEventHandler NameChanged;

		private void DoNameChanged(string oldName, string newName)
		{
			if (NameChanged != null && oldName != "" && newName != "")
			{
				NameChanged(this, new DataSourceNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}

		#endregion DataSourceNameChangedEvent

		#region Fields
		private Guid _guid = Guid.NewGuid();
		private string _name = string.Empty;
		private Project _project;
		#endregion

		#region Project

		[XmlIgnore()]
		[Browsable(false)]
		public Project Project {
			get { return _project; }
		}

		internal void SetProject(Project project) {
			Trace.Assert(project.FileName != null, "Project should not be null.");
			_project = project;
		}

		#endregion Project

		#region Properties

		[XmlAttribute()]
		[Browsable(false)]
		public Guid Guid {
			get { return _guid; }
			set { if (value != Guid.Empty) _guid = value; }
		}

		public string Name {
			get {
				if (_name == string.Empty && _project != null) {
					_name = "Data Source " + _project.DataSources.Count;
				}
				return _name;
			}
			set {
				if (_project != null) {
					foreach (DataSource ds in _project.DataSources) {
						if (ds != this && ds.Name == value) {
							throw
								new InvalidOperationException(
								"Another data source with the name '" + value + "' already exists in this project. Please choose another name."
								);
						}
					}
				}
				DoNameChanged(_name, value);
				_name = value;
			}
		}

		#endregion

		public abstract void TestConnection();
		public abstract bool IsConnectionValid();
		public abstract DataSourceEntityGroup[] ListEntities();
		public abstract void SyncProjectEntities();
		public abstract void SyncProjectEntities(DataSourceEntity[] entities);

		protected Entity GetMetadataEntity(DataSourceEntity table, bool create) 
		{
			foreach (MetadataFile file in this.Project.MetadataFiles) 
			{
				foreach (IMetadataEntity entity in file.MetadataEntities) 
				{
					if (entity is Entity &&
						((Entity) entity).DBEntityName == table.Name &&
						(((Entity) entity).DataSourceName == this.Name 
						 || file.GetFullPath() == GetEntityFileName(table))) 
					{
						return (Entity) entity;
					}
				}
			}
			if (create)
				return CreateMetadataFile(table);
			else
				return null;
		}

		private Entity CreateMetadataFile(DataSourceEntity entity) 
		{
			MetadataFile file = new MetadataFile(this.Project);
			Entity orEntity = new Entity();
			file.MetadataEntities.Add(orEntity);
			file.Save(GetEntityFileName(entity));
			this.Project.MetadataFiles.Add(file);
			return orEntity;
		}

		protected string GetEntityFileName(DataSourceEntity entity) 
		{
			string filepart = entity.Namespace == "" ? 
				entity.Name : entity.Namespace + "_" + entity.Name;
			return Path.Combine(this.Project.GetFullMetadataPath(), filepart + ".xml");
		}


	}
}