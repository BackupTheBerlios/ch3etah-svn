using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Adapdev.Data.Sql;
//using MySql.Data.MySqlClient;

namespace Adapdev.Data.Schema
{
	/// <summary>
	/// Summary description for MySqlSchemaBuilder.
	/// </summary>
	public class MySqlSchemaBuilder : AbstractSchemaBuilder
	{
		private Adapdev.Data.DbProviderType dbProviderType = DbProviderType.MYSQL;
		private Adapdev.IProgressCallback _callback = null;
		private int recordCount = 0;

		public MySqlSchemaBuilder(Adapdev.IProgressCallback callback, ref int recordCount)
		{
			this._callback = callback;
			this.recordCount = recordCount;
		}

		public override DatabaseSchema BuildDatabaseSchema(string connectionString, Adapdev.Data.DbType databaseType, Adapdev.Data.DbProviderType providerType, string schemaFilter)
		{
			return this.CreateMySqlDatabaseSchema(connectionString);
		}

//		private DatabaseSchema CreateMySqlDatabaseSchema(string connectionString)
//		{
//			DataTable schemaTables = this.GetMySqlSchema(connectionString);
//
//			DatabaseSchema di = new DatabaseSchema();
//			MySqlConnection c = new MySqlConnection(connectionString);
//			di.Name = c.Database;
//			c = null;
//
//			foreach (DataRow dr in schemaTables.Rows)
//			{
//				TableSchema ti = CreateMySqlTableSchema(dr);
//				CreateColumnSchemas(ti, connectionString, Adapdev.Data.DbType.MYSQL);
//
//				DataTable columns = this.GetMySqlColumnSchema(connectionString, ti.Name);
//
//				foreach(DataRow columnRow in columns.Rows)
//				{
//					if (columnRow["Key"] + "" == "PRI")
//					{
//						ti[columnRow["Field"].ToString()].IsPrimaryKey = true;
//					}
//					else if (columnRow["Key"] + "" == "MUL")
//					{
//						ti[columnRow["Field"].ToString()].IsForeignKey = true;
//					}
//				}
//				di.AddTable(ti);
//			}
//
//			return di;
//		}
//
		/// <summary>
		/// Creates the ColumnSchemas for a specified table
		/// </summary>
		/// <param name="ts">The TableSchema to add the ColumnSchema to</param>
		/// <param name="connectionString">The OleDb connectionstring to use</param>
		private void CreateColumnSchemas(TableSchema ts, string connectionString, Adapdev.Data.DbType databaseType)
		{
			DataTable dt = this.GetReaderSchema(connectionString, ts.Name, databaseType);
			if (!(dt == null)) 
			{
				foreach (DataRow dr in dt.Rows) 
				{
					ColumnSchema ci = new ColumnSchema();
					ci.Alias = (string) dr["ColumnName"];
					ci.AllowNulls = (bool) dr["AllowDBNull"];
					ci.DataTypeId = (int) dr["ProviderType"];
					ci.DataType = ProviderInfoManager.GetInstance().GetNameById(this.dbProviderType, ci.DataTypeId);
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

					// hack because MySql has the same provider type for
					// strings and blobs, which results in blob
					// default and test values being incorrectly assigned to
					// string columns
					if((ci.DataTypeId == 252 && ci.NetType.Equals("System.String")) || ci.DataTypeId == 254)
					{
						ci.DataTypeId = 253;
						ci.DataType = ProviderInfoManager.GetInstance().GetNameById(this.dbProviderType, ci.DataTypeId);
						ci.DefaultTestValue = ProviderInfoManager.GetInstance().GetTestDefaultById(this.dbProviderType, ci.DataTypeId);
						ci.DefaultValue = ProviderInfoManager.GetInstance().GetDefaultById(this.dbProviderType, ci.DataTypeId);
					}
					ts.AddColumn(ci);
				} 
			}
		}

		/// <summary>
		/// Gets the OleDbDataReader.GetSchemaTable() for a specified database table
		/// </summary>
		/// <param name="oledbConnectionString">The connection string to use</param>
		/// <param name="tableName">The table to grab the schema for</param>
		/// <returns></returns>
		private DataTable GetReaderSchema(string oledbConnectionString, string tableName, Adapdev.Data.DbType databaseType)
		{
			return GetReaderSchema(new MySqlConnection(), new MySqlCommand(), oledbConnectionString, databaseType, tableName);
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


		private TableSchema CreateMySqlTableSchema(DataRow dr)
		{
			TableSchema ti = new TableSchema();
			ti.Alias = dr[0].ToString();
			ti.Name = ti.Alias;
			ti.TableType = TableType.TABLE;
			return ti;
		}

//		private DataTable GetMySqlColumnSchema(string connectionString, string tableName)
//		{
//			DataTable schemaTable = new DataTable();
//			MySqlConnection cn = new MySqlConnection();
//			MySqlCommand cmd = new MySqlCommand();
//			MySqlDataAdapter da = new MySqlDataAdapter();
//
//			cn.ConnectionString = connectionString;
//			cn.Open();
//
//			cmd.Connection = cn;
//			cmd.CommandText = "SHOW COLUMNS IN " + tableName;
//			da.SelectCommand = cmd;
//			da.Fill(schemaTable);
//
//			cn.Close();
//
//
//			return schemaTable;
//		}

		/// <summary>
		/// Gets the OleDbConnection.GetOleDbSchemaTable for a specified connection
		/// </summary>
		/// <param name="mysqlConnectionString">The connection to use</param>
		/// <returns></returns>
		private DataTable GetMySqlSchema(string mysqlConnectionString)
		{
			MySqlConnection conn = new MySqlConnection(mysqlConnectionString);
			conn.Open();
			MySqlCommand command = new MySqlCommand("SHOW TABLES", conn);
			DataTable tbl = new DataTable();
			MySqlDataAdapter da = new MySqlDataAdapter(command);
			da.Fill(tbl);
			conn.Close();
			return tbl;
		}

		public string PrintReaderSchema(string connectionString, string table)
		{
			StringBuilder sb = new StringBuilder();

			DataTable schemaTable;
			schemaTable = this.GetReaderSchema(connectionString, table, Adapdev.Data.DbType.MYSQL);

			sb.Append("\r\n=========== " + table + " Schema =====================\r\n\r\n");

			foreach (DataRow myField in schemaTable.Rows)
			{
				foreach (DataColumn myProperty in schemaTable.Columns)
				{
					sb.Append(myProperty.ColumnName + " : " + myField[myProperty].ToString() + "\r\n");
				}
				sb.Append("\r\n");
			}

			sb.Append("\r\n\r\n");
			return sb.ToString();
		}
	}
}
