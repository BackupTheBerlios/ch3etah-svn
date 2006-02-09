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
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Xml.Serialization;

using Adapdev.Data;
using Adapdev.Data.Schema;

using Ch3Etah.Core.Config;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Core.ProjectLib
{
	public class OleDbDataSource : DataSource
	{
		
		#region Fields
		DatabaseSchema _dbschema;

		private string _server = string.Empty;
		private string _database = string.Empty;
		private string _user = string.Empty;
		private string _password = string.Empty;
		private ConnectionType _connectionType;
		private string _connectionTypeName = string.Empty;
		private string _connectionString = string.Empty;
		private OrmConfiguration _ormConfiguration = Ch3EtahConfig.OrmConfiguration.Clone();
		#endregion Fields

		#region Properties
		[Browsable(false)]
		public string Server 
		{
			get { return _server; }
			set { _server = value; }
		}

		[Browsable(false)]
		public string Database 
		{
			get { return _database; }
			set { _database = value; }
		}

		[Browsable(false)]
		public string User 
		{
			get { return _user; }
			set { _user = value; }
		}

		[Browsable(false)]
		public string Password 
		{
			get { return _password; }
			set { _password = value; }
		}

		[Browsable(false), XmlIgnore()]
		public ConnectionType ConnectionType 
		{
			get { return _connectionType; }
			set 
			{
				_connectionType = value;
				_connectionTypeName = _connectionType.Name;
			}
		}

		[Browsable(false)]
		[XmlElement("ConnectionType")]
		public string ConnectionTypeName 
		{
			get 
			{
				if (_connectionType != null) 
				{
					return _connectionType.Name;
				}
				else 
				{
					return _connectionTypeName;
				}
			}
			set 
			{
				if (_connectionType == null) 
				{
					_connectionTypeName = value;
					_connectionType = ConnectionTypeFactory.GetConnectionType(value, false);
				}
				else 
				{
					_connectionType = ConnectionTypeFactory.GetConnectionType(value);
					_connectionTypeName = _connectionType.Name;
				}
			}
		}

		[Browsable(true)]
		[Editor("Ch3Etah.Design.CustomUI.OleDbConnectionStringDialog", typeof(UITypeEditor))]
		public string ConnectionString 
		{
			get 
			{
				if (_connectionType != null && _connectionType.ConnectionStringTemplate != string.Empty) 
				{
					_connectionString = BuildConnectionString(_connectionType.ConnectionStringTemplate);
				}
				return _connectionString;
			}
			set { _connectionString = value; }
		}

		[TypeConverter(typeof (ExpandableObjectConverter))]
		public OrmConfiguration OrmConfiguration
		{
			get { return _ormConfiguration; }
			set { _ormConfiguration = value; }
		}
		#endregion Properties

		public override void TestConnection() 
		{
			OleDbConnection cn = new OleDbConnection(ConnectionString);
			cn.Open();
			cn.Close();
		}

		public override bool IsConnectionValid() 
		{
			try 
			{
				TestConnection();
				return true;
			}
			catch 
			{
				return false;
			}
		}

		public override DataSourceEntityGroup[] ListEntities()
		{
			_dbschema = SchemaBuilder.CreateDatabaseSchema(this.ConnectionString, Adapdev.Data.DbType.SQLSERVER, Adapdev.Data.DbProviderType.OLEDB);
			DataSourceEntityGroup tables = new DataSourceEntityGroup("Tables");
			DataSourceEntityGroup views = new DataSourceEntityGroup("Views");
			foreach (TableSchema entity in _dbschema.SortedTables.Values) 
			{
				if (entity.TableType == TableType.TABLE) 
				{
					tables.AddEntity(entity.Name);
				}
				else if (entity.TableType == TableType.VIEW) 
				{
					views.AddEntity(entity.Name);
				}
			}
			return new DataSourceEntityGroup[] {tables, views};
		}
		[Obsolete("", true)]
		public override void SyncProjectEntities()
		{
			throw new NotImplementedException();
		}
		
		public override void SyncProjectEntities(DataSourceEntity[] entities)
		{
			if (this.Project == null) throw new ApplicationException("Project is null.");
			ArrayList updatedEntities = new ArrayList();
			foreach (DataSourceEntity entity in entities) {
				TableSchema table = this.GetTableSchema(entity);
				Entity orEntity = this.GetMetadataEntity(entity, true);
				this.RefreshDBInfo(table, orEntity);
				updatedEntities.Add(orEntity);
				if (this.OrmConfiguration.CustomEntityAttributes.Count > 0)
				{
					foreach (NameValuePair att in this.OrmConfiguration.CustomEntityAttributes)
					{
						((IMetadataNode)orEntity).SetAttributeValue(att.Name, att.Value);
					}
				}
				orEntity.OwningMetadataFile.Save();
			}
			if (this.OrmConfiguration.AutoMapLinks)
			{
				foreach (Entity orEntity in updatedEntities)
				{
					this.RefreshDBLinks(orEntity);
					orEntity.OwningMetadataFile.Save();
				}
			}
		}
		
		private TableSchema GetTableSchema(DataSourceEntity entity)
		{
			foreach (TableSchema table in _dbschema.Tables.Values)
			{
				if (table.Name == entity.Name) return table;
			}
			return null;
		}

		private string BuildConnectionString(string connectionStringTemplate) 
		{
			string cn = connectionStringTemplate;
			cn = cn.Replace(ConnectionType.SERVER_PLACEHOLDER, this.Server);
			cn = cn.Replace(ConnectionType.DATABASE_PLACEHOLDER, this.Database);
			cn = cn.Replace(ConnectionType.USER_PLACEHOLDER, this.User);
			cn = cn.Replace(ConnectionType.PASSWORD_PLACEHOLDER, this.Password);
			return cn;
		}


		#region RefreshDBInfo
		private void RefreshDBInfo(TableSchema table, Entity entity) 
		{
			Debug.WriteLine("Parsing data source information for the table [" + table.Name + "].");
			Debug.Indent();
			try
			{
				entity.DataSourceName = this.Name;
				entity.DBName = _dbschema.Name;
				entity.DBEntityName = table.Name;
				if (entity.Name == string.Empty) 
				{
					entity.Name = table.Name.Replace(" ", "");
				}
				this.FillFields(table, entity);
				if (this.OrmConfiguration.AutoMapIndexes)
				{
					this.FillIndexes(entity);
				}
			}
			finally
			{
				Debug.Unindent();
			}
		}

		private Entity FindDBEntity(string entityDbName)
		{
			foreach (MetadataFile file in this.Project.MetadataFiles)
			{
				foreach (IMetadataEntity entity in file.MetadataEntities)
				{
					if (entity is Entity && ((Entity)entity).DBEntityName == entityDbName)
					{
						return (Entity)entity;
					}
				}
			}
			return null;
		}

		#endregion RefreshDBInfo

		#region FillFields
		private ArrayList _dbfields;
		private void FillFields(TableSchema table, Entity entity) 
		{
			_dbfields = new ArrayList();
			foreach (ColumnSchema column in table.Columns.Values) 
			{
				_dbfields.Add(column.Name);
				EntityField field = GetEntityField(column, entity);
				field.SetEntity(entity);
				this.RefreshFieldDBInfo(column, field);
				if (!entity.Fields.Contains(field)) 
				{
					entity.Fields.Add(field);
				}
			}
			RemoveStaleDBFields(entity);
		}

		private EntityField GetEntityField(ColumnSchema column, Entity entity) 
		{
			foreach (EntityField field in entity.Fields) 
			{
				if (field.DBColumn == column.Name) 
				{
					return field;
				}
			}
			return new EntityField();
		}

		private void RefreshFieldDBInfo(ColumnSchema column, EntityField field) 
		{
			if (field.Name == string.Empty) 
			{
				field.Name = column.Name.Replace(" ", "");
				field.ReadOnly = column.IsReadOnly;
				field.AllowNull = column.AllowNulls;
			}
			Debug.WriteLine(
				column.Name + " " + column.DataTypeId + " " + column.DataType + " " + column.NetType + " " + column.Length + " " +
				column.DefaultValue + " " + column.DefaultTestValue);
			field.DBColumn = column.Name;
			field.KeyField = column.IsPrimaryKey;
			if (field.KeyField 
				&& this.OrmConfiguration.RenameSurrogateKeys 
				&& this.OrmConfiguration.SurrogateKeyName != "")
			{
				field.Name = this.OrmConfiguration.SurrogateKeyName;
			}
			field.DBIdentity = column.IsAutoIncrement;
			field.DBReadOnly = column.IsReadOnly;
			field.DBType = GetFieldDbType(column.DataTypeId, field);
			field.Type = column.NetType.Replace("System.", "");
			//this.VBType
			field.DBSize = column.Length;
			field.DBPrecision = 8;
		}

		// HACK
		private string GetFieldDbType(int typeID, EntityField field) 
		{
			switch (typeID) 
			{
				case 2:
					return "smallint";
				case 3:
					return "int";
				case 4:
					return "real";
				case 5:
					return "float";
				case 6:
					return "money"; //smallmoney
				case 11:
					return "bit";
				case 17:
					return "tinyint";
				case 20:
					return "bigint";
				case 72:
					return "uniqueidentifier";
				case 128:
					return "binary";
				case 129:
					return "char";
				case 130:
					return "nchar";
				case 131:
					return "decimal"; //numeric
				case 135:
					return "datetime"; //smalldatetime
				case 200:
					return "varchar";
				case 201:
					return "text";
				case 202:
					return "nvarchar";
				case 203:
					return "ntext";
				case 204:
					return "varbinary";
				case 205:
					return "image";
				default:
					throw new InvalidOperationException("Type ID '" + typeID.ToString() + "' is not a recognized type code. (" + this.GetFieldFullyQualifiedName(field) + ")");
			}
		}
		
		private string GetFieldFullyQualifiedName(EntityField field) 
		{
			string nm = "";
			if (field.Entity != null) 
			{
				nm = "Entity=" + field.Entity.ToString() + ":";
			}
			nm += "Field=" + field.ToString();
			return nm;
		}

		private void RemoveStaleDBFields(Entity entity)
		{
			foreach (EntityField field in entity.Fields)
			{
				if (field.DBColumn != "" && !_dbfields.Contains(field.DBColumn))
				{
					field.IsExcluded = true;
				}
			}
		}
		#endregion FillFields

		#region FillIndexes
		private ArrayList _dbindexes;
		private void FillIndexes(Entity entity) 
		{
			_dbindexes = new ArrayList();
			DataView indexes = GetDBIndexes(entity);
			indexes.RowFilter = "TABLE_NAME = '" + entity.DBEntityName + "'";
			indexes.Sort = "INDEX_NAME asc, ORDINAL_POSITION asc";
			foreach (DataRowView row in indexes) 
			{
				_dbindexes.Add(row["INDEX_NAME"] as string);
				Index index = LoadIndexRow(row, entity);
				if (index != null) 
				{
					RemoveUnusedIndexFields(indexes, index);
				}
			}
			RemoveStaleDBIndexes(entity);
		}

		private DataView GetDBIndexes(Entity entity) 
		{
			using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) 
			{
				cn.Open();
				try 
				{
					DataTable dbIndexes = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, null);
					if (this.OrmConfiguration.AutoMapLinks)
					{
						FillFKIndexes(dbIndexes, cn, entity);
					}
					return new DataView(dbIndexes);
				}
				finally 
				{
					cn.Close();
				}
			}
		}
		
		private void FillFKIndexes(DataTable dbIndexes, OleDbConnection cn, Entity entity)
		{
			DataView fks = new DataView(cn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null));
			fks.RowFilter = string.Format("FK_TABLE_NAME = '{0}'", entity.DBEntityName);
			foreach (DataRowView fk in fks)
			{
				DataRow ix = dbIndexes.NewRow();
				ix["TABLE_NAME"] = (string)fk["FK_TABLE_NAME"];
				ix["INDEX_NAME"] = (string)fk["FK_NAME"];
				ix["COLUMN_NAME"] = (string)fk["FK_COLUMN_NAME"];
				ix["PRIMARY_KEY"] = false;
				ix["UNIQUE"] = false;
				ix["ORDINAL_POSITION"] = (Int64)fk["ORDINAL"];
				dbIndexes.Rows.Add(ix);
			}
		}

		private Index LoadIndexRow(DataRowView row, Entity entity) 
		{
			Debug.WriteLine(row["COLUMN_NAME"]);
			string fieldName = (String) row["COLUMN_NAME"];
			EntityField field = null;
			foreach (EntityField f in entity.Fields) 
			{
				if (f.DBColumn == fieldName) 
				{
					field = f;
					break;
				}
			}
			if (field == null) 
			{
				return null;
			}
			string name = (String) row["INDEX_NAME"];
			Index index = null;
			foreach (Index i in entity.Indexes) 
			{
				if (i.DBName == name) 
				{
					index = i;
					break;
				}
			}
			if (index == null) 
			{
				index = new Index();
				entity.Indexes.Add(index);
			}
			this.RefreshIndexDBInfo(row, index, field);
			return index;
		}

		private void RefreshIndexDBInfo(DataRowView schema, Index index, EntityField field) 
		{
			IndexField indexField = null;
			foreach (IndexField f in index.Fields) 
			{
				if (f.Name == field.Name) 
				{
					indexField = f;
					break;
				}
			}
			if (indexField == null) 
			{
				indexField = new IndexField();
				indexField.Name = field.Name;
				index.Fields.Add(indexField);
			}
			index.DBName = (string)schema["INDEX_NAME"];
			index.PrimaryKey = (bool)schema["PRIMARY_KEY"];
			index.Unique = (bool)schema["UNIQUE"];
			if (index.Name == string.Empty) 
			{
				if (index.PrimaryKey) 
				{
					index.IsExcluded = !this.OrmConfiguration.AutoEnableMappedIndexes && !this.OrmConfiguration.AutoEnablePrimaryIndex;
					if (this.OrmConfiguration.RenamePrimaryIndex && this.OrmConfiguration.PrimaryIndexName != "") 
					{
						index.Name = this.OrmConfiguration.PrimaryIndexName;
					}
					else 
					{
						index.Name = index.DBName;
					}
					index.DeleteBy = true;
				}
				else 
				{
					index.IsExcluded = !this.OrmConfiguration.AutoEnableMappedIndexes;
					index.Name = index.DBName;
				}
			}
		}
		
		private void RemoveUnusedIndexFields(DataView schema, Index index) 
		{
			for (int i=index.Fields.Count-1; i>=0; i--) 
			{
				IndexField field = index.Fields[i];
				if (!SchemaContainsField(schema, field, index)) 
				{
					index.Fields.Remove(field);
				}
			}
		}
		
		private bool SchemaContainsField(DataView schema, IndexField field, Index index) 
		{
			string dbColumn = string.Empty;
			foreach (EntityField f in index.Entity.Fields) 
			{
				if (f.Name == field.Name) 
				{
					dbColumn = f.DBColumn;
					break;
				}
			}
			if (dbColumn == string.Empty) 
			{
				dbColumn = field.Name;
			}
			foreach (DataRowView row in schema) 
			{
				if ((string)row["TABLE_NAME"] == index.Entity.DBEntityName 
					&& (string)row["INDEX_NAME"] == index.DBName 
					&& (string)row["COLUMN_NAME"] == dbColumn) 
				{
					return true;
				}
			}
			return false;
		}
		private void RemoveStaleDBIndexes(Entity entity)
		{
			foreach (Index index in entity.Indexes)
			{
				if (index.DBName != "" && !_dbindexes.Contains(index.DBName))
				{
					index.IsExcluded = true;
				}
			}
		}
		#endregion FillIndexes

		#region RefreshDBLinks
		private ArrayList _dblinks;
		private void RefreshDBLinks(Entity entity)
		{
			_dblinks = new ArrayList();
			FillManyToLinks(entity);
			FillOneToLinks(entity);
			RemoveStaleDBLinks(entity);
		}
		private void FillManyToLinks(Entity entity)
		{
			FillLinks("FK_", "PK_", entity, this.OrmConfiguration.ExcludeFKSourceFields);
		}

		private void FillOneToLinks(Entity entity)
		{
			FillLinks("PK_", "FK_", entity, false);
		}

		private void FillLinks(string sourcePrefix, string targetPrefix, Entity entity, bool excludeSourceFields)
		{
			DataView linkSchema = GetDBLinkSchema(entity);
			linkSchema.RowFilter = string.Format(
				"{0}TABLE_NAME = '{1}'"
				, sourcePrefix
				, entity.DBEntityName);
			linkSchema.Table.Columns.Add("Key", typeof(string));
			ArrayList keys = new ArrayList();
			foreach (DataRowView row in linkSchema)
			{
				row["Key"] = row[sourcePrefix + "NAME"] + "_" + row[targetPrefix + "NAME"];
				if (row[sourcePrefix + "NAME"] is string && !keys.Contains(row[sourcePrefix + "NAME"] as string))
				{
					keys.Add(row["Key"] as string);
				}
			}

			foreach (string key in keys)
			{
				_dblinks.Add(key);
				linkSchema.RowFilter = string.Format(
					"{2}TABLE_NAME = '{0}' and Key = '{1}'"
					, entity.DBEntityName
					, key
					, sourcePrefix);
				Entity linkedEntity = FindDBEntity(linkSchema[0][targetPrefix + "TABLE_NAME"] as string);
				if (linkedEntity == null) continue;
				Index linkedIndex = linkedEntity.Indexes.FindByDBName(linkSchema[0][targetPrefix + "NAME"] as string);
				if (linkedIndex == null) continue;
				Link link = entity.Links.FindByDBName(key);
				bool isCollection = (!linkedIndex.Unique && !linkedIndex.PrimaryKey);
				if (link == null)
				{
					link = new Link();
					link.IsExcluded = !this.OrmConfiguration.AutoEnableMappedLinks;
					link.DBName = key;
					link.IsProperty = true;
					link.IsConstrained = (sourcePrefix == "FK_");
					if (isCollection)
						link.Name = linkedEntity.PluralName;
					else
						link.Name = linkedEntity.Name;
					entity.Links.Add(link);
				}
				link.IsCollection = isCollection;
				link.TargetEntityName = linkedEntity.Name;
				link.TargetIndexName = linkedIndex.Name;
				link.Fields.Clear();
				foreach (DataRowView row in linkSchema)
				{
					LinkField field = new LinkField();
					field.SourceFieldName = entity.Fields.GetFieldFromDBColumn(row[sourcePrefix + "COLUMN_NAME"] as string).Name;
					field.TargetFieldName = linkedEntity.Fields.GetFieldFromDBColumn(row[targetPrefix + "COLUMN_NAME"] as string).Name;
					link.Fields.Add(field);
					if (excludeSourceFields && !link.IsExcluded)
					{
						entity.Fields.GetFieldFromName(field.SourceFieldName)
							.IsExcluded = true;
					}
				}
			}
		}
		
		private DataView GetDBLinkSchema(Entity entity) 
		{
			using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) 
			{
				cn.Open();
				try 
				{
					return new DataView(cn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null));
				}
				finally 
				{
					cn.Close();
				}
			}
		}

		private void RemoveStaleDBLinks(Entity entity)
		{
			foreach (Link link in entity.Links)
			{
				if (link.DBName != "" && !_dblinks.Contains(link.DBName))
				{
					link.IsExcluded = true;
				}
			}
		}
		#endregion RefreshDBLinks

	}
}
