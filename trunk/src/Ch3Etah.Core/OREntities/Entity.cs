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

using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Design;
using System.Xml.Serialization;
using Adapdev.Data.Schema;
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

		private const string ROOT_NODE_NAME = "OREntity";

		public Entity() : base(ROOT_NODE_NAME, ROOT_NODE_NAME)
		{
			DataSource.NameChanged +=new DataSourceNameChangedEventHandler(DataSource_NameChanged);
		}


		#region Member Variables

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
					return _name;
				}
				else {
					return _pluralName;
				}
			}
			set { _pluralName = value; }
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
		[MetadataNodeCollectionAttribute("Fields", "Field", typeof (EntityField))]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		public EntityFieldCollection Fields {
			get {
				_fields.Entity = this;
				return _fields;
			}
			set { _fields = value; }
		}

		[Category("Entity")]
		[MetadataNodeCollectionAttribute("Indexes", "Index", typeof (Index))]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		public IndexCollection Indexes {
			get {
				_indexes.Entity = this;
				return _indexes;
			}
			set { _indexes = value; }
		}

		[Category("Entity")]
		[MetadataNodeCollectionAttribute("Links", "Link", typeof (Link))]
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

		#region RefreshDBInfo

		public void RefreshDBInfo(DataSource dataSource, DatabaseSchema database, TableSchema table) {
			DataSourceName = dataSource.Name;
			DBName = database.Name;
			DBEntityName = table.Name;
			if (Name == string.Empty) {
				Name = table.Name.Replace(" ", "");
			}
			FillFields(table);
			FillIndexes(dataSource);
		}

		public void AutoFillLinks(DataSource ds)
		{
			// Create Object's
			Link oLink;
			DataView dbLinkSchemas;
			Entity oTargetEntity = null;
			Index oTargetIndex = null;
			IndexField oTargetIndexField = null;
			LinkField oSourceLinkField = null;
			Boolean bIndexExists = false;

			Trace.Unindent();
				Trace.WriteLine(String.Format("Create Links from Entity \"{0}\"", this.Name));
			Trace.Indent();

			// Get Schemas
			dbLinkSchemas = GetDBLinkSchema(ds);

			// Lopping on Fields
			foreach (EntityField tmpField in this.Fields)
			{
				// Clear Flag
				bIndexExists = false;

				if (tmpField.KeyField) // PK
				{
					Trace.WriteLine(String.Format("Parsing PK \"{0}\"", tmpField.Name));

					// Filter Indexes
					dbLinkSchemas.RowFilter = String.Format("PK_COLUMN_NAME = '{0}' AND PK_TABLE_NAME = '{1}'", tmpField.DBColumn, this.DBEntityName);

					// Looping on FK's
					foreach (DataRowView tmpDataRowView in dbLinkSchemas)
					{
						Trace.WriteLine(String.Format("Parsing FK \"{0}\"", tmpDataRowView["FK_NAME"]));

						// Get Target Entity
						oTargetEntity = Project.CurrentProject.GetEntity(tmpDataRowView["FK_TABLE_NAME"].ToString());

						if (oTargetEntity == null)
						{
							Trace.WriteLine(String.Format("Entity for Table \"{0}\" not Found.", tmpDataRowView["FK_TABLE_NAME"]));
							continue;
						}

						// Check if the Link Alread Exists
						foreach (Link tmpLink in this.Links)
						{
							if (tmpLink.Fields.Count.Equals(1) && tmpLink.Fields[0].Name.Equals(tmpDataRowView["FK_COLUMN_NAME"].ToString()))
							{
								Trace.WriteLine(String.Format("Link por Field \"{0}\" Alread Exists", tmpField.Name));
								bIndexExists = true;
								break;
							}
						}

						if (bIndexExists) { break; }

						// Lopping on TargetEntity to Find the Correct Index
						foreach (Index tmpIndex in oTargetEntity.Indexes)
						{
							if (tmpIndex.Fields.Count.Equals(1) && tmpIndex.Fields[0].Name.EndsWith(tmpDataRowView["FK_COLUMN_NAME"].ToString()))
							{
								oTargetIndex = tmpIndex;
								break;
							}
						}

						// Check if Exists
						if (oTargetIndex == null)
						{
							Trace.WriteLine(String.Format("Creating Target Index for FK \"{0}\"", tmpDataRowView["FK_NAME"]));

							// Create Target Index
							oTargetIndex = new Index();
								oTargetIndex.SelectBy = true;
								oTargetIndex.DeleteBy = false;
								oTargetIndex.Name = tmpDataRowView["FK_COLUMN_NAME"].ToString();
								oTargetIndex.Unique = false;
								oTargetIndex.DBName = tmpDataRowView["FK_COLUMN_NAME"].ToString();
								oTargetIndex.PrimaryKey = false;

							// Create Target Index Field
							oTargetIndexField = new IndexField();
								oTargetIndexField.Name = tmpDataRowView["FK_COLUMN_NAME"].ToString();
								oTargetIndexField.PartialTextMatch = false;

							// Add Target Index Field on Target Index
							oTargetIndex.Fields.Add(oTargetIndexField);

							// Add Target Index on Target Entity
							oTargetEntity.Indexes.Add(oTargetIndex);
						}

						// Create Link
						oLink = new Link();
							oLink.TargetEntityName = oTargetEntity.Name;
							oLink.TargetIndexName = oTargetIndex.Name;
							oLink.IsCollection = true;
							oLink.IsProperty = true;
							oLink.ReadOnly = true;

						// Check if the Target Index is Unique
						if (oTargetIndex.Unique) // 1-N Relation
						{
							oLink.Name = oTargetEntity.Name; // N-M Relation
						}
						else { oLink.Name = oTargetEntity.PluralName; }
							
						// Check Delete Relation Action
						if (tmpDataRowView["DELETE_RULE"].ToString().Equals("NO ACTION"))
						{
							oLink.CascadeDelete = false;
						}
						else { oLink.CascadeUpdate = true; }

						// Check Update Relation Action
						if (tmpDataRowView["UPDATE_RULE"].ToString().Equals("NO ACTION"))
						{
							oLink.CascadeDelete = false;
						}
						else { oLink.CascadeUpdate = true; }

						// Create Link Field
						oSourceLinkField = new LinkField();
							oSourceLinkField.SourceFieldName = this.Fields.GetFieldFromDBColumn(tmpDataRowView["PK_COLUMN_NAME"].ToString()).Name;
							oSourceLinkField.TargetFieldName = oTargetEntity.Fields.GetFieldFromDBColumn(tmpDataRowView["FK_COLUMN_NAME"].ToString()).Name;

						// Add Link Field on Link
						oLink.Fields.Add(oSourceLinkField);

						// Add Link on Entity
						this.Links.Add(oLink);
					}

					if (bIndexExists) { continue; }
				}
			}
		}



		private EntityField GetEntityField(ColumnSchema column) 
		{
			foreach (EntityField field in Fields) {
				if (field.DBColumn == column.Name) {
					return field;
				}
			}
			return new EntityField();
		}

		private void FillFields(TableSchema table) {
			foreach (ColumnSchema column in table.Columns.Values) {
				EntityField field = GetEntityField(column);
				field.RefreshDBInfo(column);
				if (!Fields.Contains(field)) {
					Fields.Add(field);
				}
			}
		}

		private void FillIndexes(DataSource dataSource) {
			DataView indexes = GetDBIndexes(dataSource);
			indexes.RowFilter = "TABLE_NAME = '" + DBEntityName + "'";
			indexes.Sort = "INDEX_NAME asc, ORDINAL_POSITION asc";
			foreach (DataRowView row in indexes) {
				Index index = LoadIndexRow(row);
				if (index != null) {
					index.RemoveUnusedFields(indexes);
				}
			}
		}

		private Index LoadIndexRow(DataRowView row) {
			string fieldName = (String) row["COLUMN_NAME"];
			EntityField field = null;
			foreach (EntityField f in Fields) {
				if (f.DBColumn == fieldName) {
					field = f;
					break;
				}
			}
			if (field == null) {
				return null;
			}
			string name = (String) row["INDEX_NAME"];
			Index index = null;
			foreach (Index i in Indexes) {
				if (i.DBName == name) {
					index = i;
					break;
				}
			}
			if (index == null) {
				index = new Index();
				Indexes.Add(index);
			}
			index.RefreshDBInfo(row, field);
			return index;
		}

		private DataView GetDBIndexes(DataSource dataSource) {
			using (OleDbConnection cn = new OleDbConnection(dataSource.ConnectionString)) {
				cn.Open();
				try {
					return new DataView(cn.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, null));
				}
				finally {
					cn.Close();
				}
			}
		}

		#endregion RefreshDBInfo

		public void RefreshDBLinks(DataSource ds) {
//			DataView schema = GetDBLinkSchema(ds);
//			string[] toManyIndexes = GetFKIndexes(schema);
//			foreach (string index in fkIndexes) {
//
//				string[] fields = GetFKIndexFields(schema, index);
//				foreach (
//			}
//			
//			string[] pkIndexes = GetPKIndexes(schema);
		}

		private DataView GetDBLinkSchema(DataSource ds) {
			using (OleDbConnection cn = new OleDbConnection(ds.ConnectionString)) {
				cn.Open();
				try {
					return new DataView(cn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null));
				}
				finally {
					cn.Close();
				}
			}
		}

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
	}
}