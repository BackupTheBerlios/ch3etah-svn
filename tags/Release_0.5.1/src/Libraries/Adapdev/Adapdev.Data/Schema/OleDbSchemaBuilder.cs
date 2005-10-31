using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Adapdev.Data.Sql;

namespace Adapdev.Data.Schema
{
	/// <summary>
	/// Summary description for OleDbSchemaBuilder.
	/// </summary>
	public class OleDbSchemaBuilder : AbstractSchemaBuilder
	{
		private Adapdev.IProgressCallback _callback = null;
		private Adapdev.Data.DbProviderType dbProviderType = DbProviderType.OLEDB;
		private int recordCount = 0;

		public OleDbSchemaBuilder(Adapdev.IProgressCallback callback, ref int recordCount)
		{
			this._callback = callback;
			this.recordCount = recordCount;
		}

		public override DatabaseSchema BuildDatabaseSchema(string connectionString, Adapdev.Data.DbType databaseType, Adapdev.Data.DbProviderType providerType, string schemaFilter)
		{
			DataTable schemaTables = this.GetOleDbSchema(_callback, connectionString, OleDbSchemaGuid.Tables, "",schemaFilter,"","");
			DatabaseSchema di = new DatabaseSchema();
			if (schemaTables != null) 
			{ 
				if (schemaTables.Rows.Count > 0) 
				{
					//TODO: Note sure if this is valid. It does not work for Oracle DB's
					di.Name = schemaTables.Rows[0]["TABLE_CATALOG"].ToString();
					if (di.Name == String.Empty) di.Name = "Unknown";
			
					// Build the base schema information
					if (!StartProgress(_callback, "Building Table Details",schemaTables.Rows.Count,ref recordCount)) return null;
					foreach (DataRow dr in schemaTables.Rows) 
					{
						TableType tableType = TableTypeConverter.Convert(dr["TABLE_TYPE"].ToString());
						if (tableType == TableType.TABLE || tableType == TableType.VIEW) 
						{
							if (!IncrProgress(_callback, dr["TABLE_NAME"].ToString(), ref recordCount)) return null;

							TableSchema ti = CreateTableSchema(dr);
							CreateColumnSchemas(ti, connectionString, databaseType);

							if(ti.Columns.Count > 0) di.AddTable(ti);
						}
					}

					// Get the primary key information
					DataTable pkeys = this.GetOleDbSchema(_callback, connectionString, OleDbSchemaGuid.Primary_Keys, "",schemaFilter,"","");
					if (pkeys != null) 
					{
						if (!StartProgress(_callback, "Building Primary Key Details",pkeys.Rows.Count,ref recordCount)) return null;
						foreach (DataRow dr in pkeys.Rows) 
						{
							string pkTable = dr["TABLE_NAME"].ToString();
							if (!IncrProgress(_callback, dr["TABLE_NAME"].ToString(), ref recordCount)) return null;

							TableSchema tip = di[pkTable];
							if (tip != null) 
							{
								ColumnSchema ci = tip[dr["COLUMN_NAME"].ToString()];
								if (ci != null) 
								{
									ci.IsPrimaryKey = true;
									tip.AddColumn(ci);
								}
							}
						}
					}
				
					// Get the foreign key information
					DataTable fkeys = this.GetOleDbSchema(_callback, connectionString, OleDbSchemaGuid.Foreign_Keys, "",schemaFilter,"","");
					if (fkeys != null) 
					{
						if (!StartProgress(_callback, "Building Foreign Key Details",fkeys.Rows.Count,ref recordCount)) return null;
						foreach (DataRow dr in fkeys.Rows) 
						{
							string fkTable = dr["FK_TABLE_NAME"].ToString();
							if (!IncrProgress(_callback, dr["FK_TABLE_NAME"].ToString(), ref recordCount)) return null;

							TableSchema tif = di[fkTable];
							if (tif != null) 
							{
								ColumnSchema ci = tif[dr["FK_COLUMN_NAME"].ToString()];
								if (ci != null) 
								{
									ci.IsForeignKey = true;
									tif.AddColumn(ci);
								}
							}
						}
					}

					// Setup the Progress Display if one is defined. 
					if (fkeys != null) 
					{
						if (!StartProgress(_callback, "Building Foreign Key Relationships",fkeys.Rows.Count,ref recordCount)) return null;
						foreach (DataRow dr in fkeys.Rows) 
						{
							if (!IncrProgress(_callback, dr["PK_TABLE_NAME"].ToString(), ref recordCount)) return null;

							// Get the name of the primary key table
							string pkTable = dr["PK_TABLE_NAME"].ToString();
							// Get the name of the foreign key table
							string fkTable = dr["FK_TABLE_NAME"].ToString();
							// Get the name of the foreign key column
							string fkColumn = dr["FK_COLUMN_NAME"].ToString();

							// Get the table containing the primary key
							TableSchema tif = di[pkTable];
							// Get the table containing the foreign key
							TableSchema fk = di[fkTable];
							if (tif != null) 
							{
								// Get the primary key
								ColumnSchema ci = tif[dr["PK_COLUMN_NAME"].ToString()];
								// Get the foreign key
								ColumnSchema cf = fk[fkColumn];
								if (ci != null) 
								{
									// Add the association to the table and column containing the foreign key
									ci.ForeignKeyTables.Add(new ForeignKeyAssociation(ci, cf ,fk));
								}
							}
						}
					}
					if (!EndProgress(_callback, "Finished Loading Tables",true)) return null;
				} 
				else 
				{
					if (!EndProgress(_callback, "No database schema information found.",false)) return null;
				} 
			} 
			else 
			{
				if (!EndProgress(_callback, "No database schema information found.",false)) return null;
			}
			return di;
		}


		private TableSchema CreateTableSchema(DataRow dr)
		{
			TableSchema ti = new TableSchema();
			ti.Alias = dr["TABLE_NAME"].ToString();
			ti.Name = dr["TABLE_NAME"].ToString();
			ti.TableType = TableTypeConverter.Convert(dr["TABLE_TYPE"].ToString());
			return ti;
		}

		/// <summary>
		/// Creates the ColumnSchemas for a specified table
		/// </summary>
		/// <param name="ts">The TableSchema to add the ColumnSchema to</param>
		/// <param name="oledbConnectionString">The OleDb connectionstring to use</param>
		public void CreateColumnSchemas(TableSchema ts, string oledbConnectionString, Adapdev.Data.DbType databaseType)
		{
			DataTable dt = this.GetReaderSchema(oledbConnectionString, ts.Name, databaseType);
			if (!(dt == null)) 
			{
				foreach (DataRow dr in dt.Rows) 
				{
					ColumnSchema ci = new ColumnSchema();
					ci.Alias = (string) dr["ColumnName"];
					ci.AllowNulls = (bool) dr["AllowDBNull"];
					ci.DataTypeId = (int) dr["ProviderType"];
					ci.DataType = ProviderInfoManager.GetInstance().GetNameById(dbProviderType, ci.DataTypeId);
					ci.DefaultTestValue = ProviderInfoManager.GetInstance().GetTestDefaultById(this.dbProviderType, ci.DataTypeId);
					ci.DefaultValue = ProviderInfoManager.GetInstance().GetDefaultById(this.dbProviderType, ci.DataTypeId);
					ci.IsAutoIncrement = (bool) dr["IsAutoIncrement"];
					ci.IsForeignKey = false;
					ci.IsPrimaryKey = false;
					ci.IsUnique = (bool) dr["IsUnique"];
					ci.Length = (int) dr["ColumnSize"];
					ci.Name = (string) dr["ColumnName"];
					ci.NetType = dr["DataType"].ToString();
					ci.Ordinal = (int) dr["ColumnOrdinal"];
					ci.IsReadOnly = (bool) dr["IsReadOnly"];
					ts.AddColumn(ci);
				} 
			}
		}

		/// <summary>
		/// Gets the OLE db schema.
		/// </summary>
		/// <param name="oledbConnectionString">Oledb connection string.</param>
		/// <param name="guid">GUID.</param>
		/// <param name="filterCatalog">Filter catalog.</param>
		/// <param name="filterSchema">Filter schema.</param>
		/// <param name="filterName">Name of the filter.</param>
		/// <param name="filterType">Filter type.</param>
		/// <returns></returns>
		public DataTable GetOleDbSchema(Adapdev.IProgressCallback _callback, string oledbConnectionString, Guid guid, string filterCatalog, string filterSchema, string filterName, string filterType) 
		{
			DataTable schemaTable = null;
			OleDbConnection conn = new OleDbConnection(oledbConnectionString);
			conn.Open();

			try 
			{
				schemaTable = conn.GetOleDbSchemaTable(guid, GetFilters(guid, filterCatalog, filterSchema, filterName, filterType));
			} 
			catch (Exception ex) 
			{
				if (_callback != null) _callback.AddMessage(ProgressMessageTypes.Critical, "Error obtaining Schema Information: " + ex.Message);
			}
			conn.Close();
			return schemaTable;
		}

		private static object[] GetFilters(Guid guid, string filterCatalog, string filterSchema, string filterName, string filterType) 
		{
			// Different OleDbSchemaGuid's require a different number of parameters. 
			// These parameter depend on what we are trying to retirve from the database
			// so this function returns the correct parameter sets. 
			// This should be a Switch statement, but the compiler did not like it. 

			filterCatalog	= filterCatalog	== string.Empty ? null : filterCatalog;
			filterSchema	= filterSchema	== string.Empty ? null : filterSchema;
			filterName		= filterName	== string.Empty ? null : filterName;
			filterType		= filterType	== string.Empty ? null : filterType;

			if (guid.Equals(OleDbSchemaGuid.Tables))		return new object[] {filterCatalog, filterSchema, filterName, filterType};
			if (guid.Equals(OleDbSchemaGuid.Views))			return new object[] {filterCatalog, filterSchema, filterName};
			if (guid.Equals(OleDbSchemaGuid.Primary_Keys))	return new object[] {filterCatalog, filterSchema, filterName};
			if (guid.Equals(OleDbSchemaGuid.Foreign_Keys))	return new object[] {filterCatalog, filterSchema, filterName, filterCatalog, filterSchema, filterName};
			if (guid.Equals(OleDbSchemaGuid.Columns))		return new object[] {filterCatalog, filterSchema, filterName, string.Empty};
			return null;
		}

		public StringBuilder PrintOleDbSchema(string oledbConnection, Guid guid, string filter)
		{
			StringBuilder sb = new StringBuilder();
			DataTable schemaTable;
			schemaTable = GetOleDbSchema(null, oledbConnection, guid, "","","",filter);

			foreach (DataRow row in schemaTable.Rows)
			{
				foreach (DataColumn column in schemaTable.Columns)
				{
					sb.Append("\t\t" + column + " : " + row[column] + "\r\n");
				}
				sb.Append("\r\n");
			}

			sb.Append("\r\n\r\n");
			return sb;
		}

		public DataTable GetReaderSchema(string oledbConnectionString, string tableName, Adapdev.Data.DbType databaseType)
		{
			return GetReaderSchema(new OleDbConnection(), new OleDbCommand(), oledbConnectionString, databaseType, tableName);
		}

		private DataTable GetReaderSchema(IDbConnection cn, IDbCommand cmd, string connectionString, Adapdev.Data.DbType databaseType, string tableName)
		{
			DataTable schemaTable = null;
			try 
			{
				cn.ConnectionString = connectionString;
				cn.Open();

				// Please Note: Use the GetPre and GetPostDelimiters here as in the case of
				// Oracle using [ ] around a Column Name is not valid. 
				cmd.Connection = cn;
				cmd.CommandText = "SELECT * FROM " + QueryHelper.GetPreDelimeter(databaseType) + tableName + QueryHelper.GetPostDelimeter(databaseType);
				IDataReader myReader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly);

				schemaTable = myReader.GetSchemaTable();

				myReader.Close();
				cn.Close();
			} 
			catch (Exception ex) 
			{
				schemaTable = null;
				if (_callback != null) _callback.AddMessage(ProgressMessageTypes.Warning, "Could not load Column information for " + tableName + ". " + ex.Message);
			}
			return schemaTable;
		}

		public StringBuilder PrintOleDbSchema(string oledbConnectionString, Guid guid)
		{
			return PrintOleDbSchema(oledbConnectionString, guid, "");
		}
	}
}
