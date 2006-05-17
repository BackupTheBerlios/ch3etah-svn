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
 *   Date: 23/12/2004
 */

using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;

using Ch3Etah.Core;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Metadata.OREntities {
	
	#region EntityNameChangedEvent
	public delegate void EntityNameChangedEventHandler(object sender, EntityNameChangedEventArgs e);

	public class EntityNameChangedEventArgs
	{
		public readonly Entity Entity;
		public readonly string OldName;
		public readonly string NewName;
		public EntityNameChangedEventArgs(Entity entity, string oldName, string newName)
		{
			this.Entity = entity;
			this.NewName = newName;
			this.OldName = oldName;
		}
	}
	#endregion EntityNameChangedEvent

	/// <summary>
	/// Represents an Entity used for Object-Relational Mapping.
	/// </summary>
	[XmlRoot("OREntity")]
	public class Entity : MetadataEntityBase {
		
		#region EntityNameChangedEvent
		public static event EntityNameChangedEventHandler NameChanged;

		private void DoNameChanged(string oldName, string newName)
		{
			if (NameChanged != null && oldName != "" && newName != "")
			{
				NameChanged(this, new EntityNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}
		#endregion EntityNameChangedEvent

		#region EntityPluralNameChangedEvent
		public static event EntityNameChangedEventHandler PluralNameChanged;

		private void DoPluralNameChanged(string oldName, string newName)
		{
			if (PluralNameChanged != null && oldName != "" && newName != "")
			{
				PluralNameChanged(this, new EntityNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}
		#endregion EntityPluralNameChangedEvent
		
		private const string ROOT_NODE_NAME = "OREntity";

		public Entity() : base(ROOT_NODE_NAME, ROOT_NODE_NAME)
		{
			DataSource.NameChanged += new DataSourceNameChangedEventHandler(DataSource_NameChanged);
		}


		#region Member Variables

		private string _assemblyName = string.Empty;
		private string _namespace = string.Empty;
		private string _name = string.Empty;
		private string _pluralName = string.Empty;
		private string _collectionName = string.Empty;
		private string _dataSourceName = string.Empty;
		private string _dbName = string.Empty;
		private string _dbEntityName = string.Empty;
		private bool _allowSelect = true;
		private bool _allowInsert = true;
		private bool _allowUpdate = true;
		private bool _allowDelete = true;
		private bool _readOnly = false;
		private string _insertText = string.Empty;
		private string _updateText = string.Empty;

		private EntityFieldCollection _fields = new EntityFieldCollection();
		private IndexCollection _indexes = new IndexCollection();
		private LinkCollection _links = new LinkCollection();
		private bool _cacheable;

		#endregion Member Variables

		#region Entity Properties
		
		[Category("(General)")]
		[XmlAttribute("assemblyname")]
		public string AssemblyName 
		{
			get { return _assemblyName; }
			set { _assemblyName = value; }
		}

		[Category("(General)")]
		[XmlAttribute("namespace")]
		public string Namespace
		{
			get { return _namespace; }
			set { _namespace = value; }
		}

		[Category("(General)")]
		[XmlAttribute("name")]
		public override string Name {
			get { return _name; }
			set
			{
				DoNameChanged(_name, value);
				_name = value;
			}
		}

		[Category("Entity")]
		[XmlAttribute("pluralname")]
		public string PluralName {
			get {
				if (_pluralName == string.Empty && _name != string.Empty) {
					return Utility.GetPluralForm(_name);
				}
				else {
					return _pluralName;
				}
			}
			set
			{
				DoPluralNameChanged(_pluralName, value);
				_pluralName = value;
			}
		}

		[Category("Entity")]
		[XmlAttribute("collectionname")]
		public string CollectionName {
			get {
				if (_collectionName == string.Empty && _name != string.Empty) {
					return _name + "Collection";
				}
				else {
					return _collectionName;
				}
			}
			set { _collectionName = value; }
		}

		[Category("Entity")]
		[XmlAttribute("readonly")]
		public bool ReadOnly {
			get { return _readOnly; }
			set { _readOnly = value; }
		}

		[XmlAttribute("cacheable"),
			Category("Entity")]
		public bool Cacheable 
		{
			get { return _cacheable; }
			set { _cacheable = value; }
		}
		
		#endregion Entity Properties

		#region Collection Properties

		[Category("Entity")]
		[MetadataNodeCollection("Fields", "Field", typeof (EntityField))]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		public EntityFieldCollection Fields {
			get {
				_fields.Entity = this;
				return _fields;
			}
			set { _fields = value; }
		}

		[Category("Entity")]
		[MetadataNodeCollection("Indexes", "Index", typeof (Index))]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		public IndexCollection Indexes {
			get {
				_indexes.Entity = this;
				return _indexes;
			}
			set { _indexes = value; }
		}

		[Category("Entity")]
		[MetadataNodeCollection("Links", "Link", typeof (Link))]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		public LinkCollection Links {
			get {
				_links.Entity = this;
				return _links;
			}
			set { _links = value; }
		}

		#endregion Collection Properties

		#region DataSource Properties

		[Category("DataSource"), XmlAttribute("datasource")]
		public string DataSourceName {
			get { return _dataSourceName; }
			set { _dataSourceName = value; }
		}

		/// <summary>
		/// Gets or sets the name of the database
		/// to which this entity is mapped.
		/// </summary>
		[Category("DataSource"), XmlAttribute("dbname")]
		public string DBName {
			get { return _dbName; }
			set { _dbName = value; }
		}

		/// <summary>
		/// Gets or sets the name of the table or other 
		/// relational entity to which this business object 
		/// entity is mapped.
		/// </summary>
		[Category("DataSource"), XmlAttribute("dbentityname")]
		public string DBEntityName {
			get { return _dbEntityName; }
			set { _dbEntityName = value; }
		}

		#endregion DataSource Properties

		#region Operation Properties

		[Category("Operations")]
		[XmlAttribute("allowdelete")]
		public bool AllowDelete {
			get { return _allowDelete; }
			set { _allowDelete = value; }
		}

		[Category("Operations")]
		[XmlAttribute("allowinsert")]
		public bool AllowInsert {
			get { return _allowInsert; }
			set { _allowInsert = value; }
		}

		[Category("Operations")]
		[XmlAttribute("allowselect")]
		public bool AllowSelect {
			get { return _allowSelect; }
			set { _allowSelect = value; }
		}

		[Category("Operations")]
		[XmlAttribute("allowupdate")]
		public bool AllowUpdate {
			get { return _allowUpdate; }
			set { _allowUpdate = value; }
		}

		#endregion Operation Properties

		#region Query field Properties

		[XmlElement(),
			Category("Query field"),
			Editor("Ch3Etah.Design.Editors.SQLEditor,Ch3Etah.Design", typeof (UITypeEditor))]
		public string InsertText {
			get { return _insertText; }
			set { _insertText = value; }
		}

		[XmlElement(),
			Category("Query field"),
			Editor("Ch3Etah.Design.Editors.SQLEditor,Ch3Etah.Design", typeof (UITypeEditor))]
		public string UpdateText {
			get { return _updateText; }
			set { _updateText = value; }
		}

		#endregion Misc Properties

		public override string ToString() {
			return Name;
		}

		private void DataSource_NameChanged(object sender, DataSourceNameChangedEventArgs e)
		{
			if (this.DataSourceName == e.OldName)
			{
				bool save = this.OwningMetadataFile != null && !this.OwningMetadataFile.IsDirty;
				this.DataSourceName = e.NewName;
				if (save) this.OwningMetadataFile.Save();
			}
		}

		public bool HasCompositePK
		{
			get
			{
				bool hasKey = false;
				foreach (EntityField field in this.Fields)
				{
					if (field.KeyField)
					{
						if (hasKey)
						{
							return true;
						}
						hasKey = true;
					}
				}
				return false;
			}
		}
	}
}