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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Design;
using System.Xml.Serialization;
using Adapdev.Data.Schema;
using Ch3Etah.Core.Config;
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
					return Ch3Etah.Core.Utility.GetPluralForm(_name);
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

//		#region RefreshDBInfo
//
//		public void RefreshDBInfo(OleDbDataSource dataSource, DatabaseSchema database, TableSchema table) {
//			Debug.WriteLine("Parsing data source information for the table [" + table.Name + "].");
//			Debug.Indent();
//			try
//			{
//				DataSourceName = dataSource.Name;
//				DBName = database.Name;
//				DBEntityName = table.Name;
//				if (Name == string.Empty) 
//				{
//					Name = table.Name.Replace(" ", "");
//				}
//				FillFields(dataSource.OrmConfiguration, table);
//				if (dataSource.OrmConfiguration.AutoMapIndexes)
//				{
//					FillIndexes(dataSource);
//				}
//			}
//			finally
//			{
//				Debug.Unindent();
//			}
//		}
//
//
//		public void RefreshDBLinks(OleDbDataSource ds)
//		{
//			FillManyToLinks(ds);
//			FillOneToLinks(ds);
//		}
//		private void FillManyToLinks(OleDbDataSource ds)
//		{
//			FillLinks(ds, "FK_", "PK_", ds.OrmConfiguration.ExcludeFKSourceFields);
//		}
//
//		private void FillOneToLinks(OleDbDataSource ds)
//		{
//			FillLinks(ds, "PK_", "FK_", false);
//		}
//
//		private void FillLinks(OleDbDataSource ds, string sourcePrefix, string targetPrefix, bool excludeSourceFields)
//		{
//			DataView linkSchema = GetDBLinkSchema(ds);
//			linkSchema.RowFilter = string.Format(
//				"{1}TABLE_NAME = '{0}'"
//				, this.DBEntityName
//				, sourcePrefix);
//			linkSchema.Table.Columns.Add("Key", typeof(string));
//			ArrayList keys = new ArrayList();
//			foreach (DataRowView row in linkSchema)
//			{
//				row["Key"] = row[sourcePrefix + "NAME"] + "_" + row[targetPrefix + "NAME"];
//				if (row[sourcePrefix + "NAME"] is string && !keys.Contains(row[sourcePrefix + "NAME"] as string))
//				{
//					keys.Add(row["Key"] as string);
//				}
//			}
//
//			foreach (string key in keys)
//			{
//				linkSchema.RowFilter = string.Format(
//					"{2}TABLE_NAME = '{0}' and Key = '{1}'"
//					, this.DBEntityName
//					, key
//					, sourcePrefix);
//				Entity linkedEntity = FindDBEntity(linkSchema[0][targetPrefix + "TABLE_NAME"] as string);
//				if (linkedEntity == null) continue;
//				Index linkedIndex = linkedEntity.Indexes.FindByDBName(linkSchema[0][targetPrefix + "NAME"] as string);
//				if (linkedIndex == null) continue;
//				Link link = this.Links.FindByDBName(key);
//				bool isCollection = (!linkedIndex.Unique && !linkedIndex.PrimaryKey);
//				if (link == null)
//				{
//					link = new Link();
//					link.IsExcluded = !ds.OrmConfiguration.AutoEnableMappedLinks;
//					link.DBName = key;
//					link.IsProperty = true;
//					if (isCollection)
//						link.Name = linkedEntity.PluralName;
//					else
//						link.Name = linkedEntity.Name;
//					this.Links.Add(link);
//				}
//				link.IsCollection = isCollection;
//				link.TargetEntityName = linkedEntity.Name;
//				link.TargetIndexName = linkedIndex.Name;
//				link.Fields.Clear();
//				foreach (DataRowView row in linkSchema)
//				{
//					LinkField field = new LinkField();
//					field.SourceFieldName = this.Fields.GetFieldFromDBColumn(row[sourcePrefix + "COLUMN_NAME"] as string).Name;
//					field.TargetFieldName = linkedEntity.Fields.GetFieldFromDBColumn(row[targetPrefix + "COLUMN_NAME"] as string).Name;
//					link.Fields.Add(field);
//					if (excludeSourceFields && !link.IsExcluded)
//					{
//						this.Fields.GetFieldFromName(field.SourceFieldName)
//							.IsExcluded = true;
//					}
//				}
//			}
//		}
//		
//		private Entity FindDBEntity(string dbName)
//		{
//			foreach (MetadataFile file in this.OwningMetadataFile.Project.MetadataFiles)
//			{
//				foreach (IMetadataEntity entity in file.MetadataEntities)
//				{
//					if (entity is Entity && ((Entity)entity).DBEntityName == dbName)
//					{
//						return (Entity)entity;
//					}
//				}
//			}
//			return null;
//		}
//
//		private EntityField GetEntityField(ColumnSchema column) 
//		{
//			foreach (EntityField field in Fields) {
//				if (field.DBColumn == column.Name) {
//					return field;
//				}
//			}
//			return new EntityField();
//		}
//
//		private void FillFields(OrmConfiguration config, TableSchema table) {
//			foreach (ColumnSchema column in table.Columns.Values) {
//				EntityField field = GetEntityField(column);
//				field.SetEntity(this);
//				field.RefreshDBInfo(config, column);
//				if (!Fields.Contains(field)) {
//					Fields.Add(field);
//				}
//			}
//		}
//
//		private void FillIndexes(OleDbDataSource dataSource) {
//			DataView indexes = GetDBIndexes(dataSource);
//			indexes.RowFilter = "TABLE_NAME = '" + DBEntityName + "'";
//			indexes.Sort = "INDEX_NAME asc, ORDINAL_POSITION asc";
//			foreach (DataRowView row in indexes) {
//				Index index = LoadIndexRow(dataSource.OrmConfiguration, row);
//				if (index != null) {
//					index.RemoveUnusedFields(indexes);
//				}
//			}
//		}
//
//		private Index LoadIndexRow(OrmConfiguration config, DataRowView row) {
//			Debug.WriteLine(row["COLUMN_NAME"]);
//			string fieldName = (String) row["COLUMN_NAME"];
//			EntityField field = null;
//			foreach (EntityField f in Fields) {
//				if (f.DBColumn == fieldName) {
//					field = f;
//					break;
//				}
//			}
//			if (field == null) {
//				return null;
//			}
//			string name = (String) row["INDEX_NAME"];
//			Index index = null;
//			foreach (Index i in Indexes) {
//				if (i.DBName == name) {
//					index = i;
//					break;
//				}
//			}
//			if (index == null) {
//				index = new Index();
//				Indexes.Add(index);
//			}
//			index.RefreshDBInfo(config, row, field);
//			return index;
//		}
//
//		private DataView GetDBIndexes(OleDbDataSource dataSource) {
//			using (OleDbConnection cn = new OleDbConnection(dataSource.ConnectionString)) {
//				cn.Open();
//				try {
//					DataTable dbIndexes = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, null);
//					if (dataSource.OrmConfiguration.AutoMapLinks)
//					{
//						FillFKIndexes(cn, dbIndexes);
//					}
//					return new DataView(dbIndexes);
//				}
//				finally {
//					cn.Close();
//				}
//			}
//		}
//		
//		private void FillFKIndexes(OleDbConnection cn, DataTable dbIndexes)
//		{
//			DataView fks = new DataView(cn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null));
//			fks.RowFilter = string.Format("FK_TABLE_NAME = '{0}'", this.DBEntityName);
//			foreach (DataRowView fk in fks)
//			{
//				DataRow ix = dbIndexes.NewRow();
//				ix["TABLE_NAME"] = (string)fk["FK_TABLE_NAME"];
//				ix["INDEX_NAME"] = (string)fk["FK_NAME"];
//				ix["COLUMN_NAME"] = (string)fk["FK_COLUMN_NAME"];
//				ix["PRIMARY_KEY"] = false;
//				ix["UNIQUE"] = false;
//				ix["ORDINAL_POSITION"] = (Int64)fk["ORDINAL"];
//				dbIndexes.Rows.Add(ix);
//			}
//		}
//		#endregion RefreshDBInfo
//
//
//		private DataView GetDBLinkSchema(OleDbDataSource ds) {
//			using (OleDbConnection cn = new OleDbConnection(ds.ConnectionString)) {
//				cn.Open();
//				try {
//					return new DataView(cn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null));
//				}
//				finally {
//					cn.Close();
//				}
//			}
//		}
//
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