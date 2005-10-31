namespace Adapdev.Data.Schema
{
	using System;
	using System.Data;
	using System.Data.OleDb;
	using System.Text;
	using Adapdev.Data;
	using Adapdev.Data.Sql;
//	using MySql.Data.MySqlClient;
                                         
	/// <summary>
	/// SchemaBuilder builds the schema for a specified database
	/// </summary>
	public class SchemaBuilder
	{
		private static Adapdev.IProgressCallback _callback;

		/// <summary>
		/// Builds the DatabaseSchema for a specified database
		/// </summary>
		/// <param name="oledbConnectionString">The OleDb connection to use</param>
		/// <returns></returns>
		public static DatabaseSchema CreateDatabaseSchema(string oledbConnectionString, Adapdev.Data.DbType databaseType, DbProviderType providerType)
		{
			return SchemaBuilder.CreateDatabaseSchema(oledbConnectionString, databaseType, providerType, "", null);
		}

		/// <summary>
		/// Builds the DatabaseSchema for a specified database
		/// </summary>
		/// <param name="oledbConnectionString">The OleDb connection to use</param>
		/// <param name="providerType">The DbProviderType to set the DatabaseSchema to</param>
		/// <returns></returns>
		public static DatabaseSchema CreateDatabaseSchema(string oledbConnectionString, string databaseType, string providerType, string schemaFilter, Adapdev.IProgressCallback progress)
		{
			return SchemaBuilder.CreateDatabaseSchema(oledbConnectionString, DbTypeConverter.Convert(databaseType), DbProviderTypeConverter.Convert(providerType), schemaFilter, progress);
		}

		/// <summary>
		/// Builds the DatabaseSchema for a specified database
		/// </summary>
		/// <param name="connectionString">The OleDb connection to use</param>
		/// <param name="providerType">The DbProviderType to set the DatabaseSchema to</param>
		/// <returns></returns>
		public static DatabaseSchema CreateDatabaseSchema(string connectionString, Adapdev.Data.DbType databaseType, DbProviderType providerType, string schemaFilter, Adapdev.IProgressCallback progress)
		{
			int recordCount = 0;
			_callback = progress as Adapdev.IProgressCallback;

			if (_callback != null) {
				_callback.SetText("Obtaining Schema Details","");
				_callback.SetAutoClose(ProgressAutoCloseTypes.WaitOnError);
			}

			DatabaseSchema ds = null;
			switch(providerType)
			{
				case DbProviderType.OLEDB:
				case DbProviderType.SQLSERVER:
				case DbProviderType.ORACLE:
					ds = new OleDbSchemaBuilder(_callback, ref recordCount).BuildDatabaseSchema(connectionString, databaseType, providerType, schemaFilter);
					break;
//				case DbProviderType.MYSQL:
//					ds = new MySqlSchemaBuilder(_callback, ref recordCount).BuildDatabaseSchema(connectionString, databaseType, providerType, schemaFilter);
//					break;
			}

			return ds;
		}


//		public static StringBuilder PrintReaderSchema(string oledbConnectionString, string table, Adapdev.Data.DbType databaseType)
//		{
//			StringBuilder sb = new StringBuilder();
//
//			DataTable schemaTable;
//			schemaTable = SchemaBuilder.GetReaderSchema(oledbConnectionString, table, databaseType);
//
//			sb.Append("\r\n=========== " + table + " Schema =====================\r\n\r\n");
//
//			foreach (DataRow myField in schemaTable.Rows)
//			{
//				foreach (DataColumn myProperty in schemaTable.Columns)
//				{
//					sb.Append(myProperty.ColumnName + " : " + myField[myProperty].ToString() + "\r\n");
//				}
//				sb.Append("\r\n");
//			}
//
//			sb.Append("\r\n\r\n");
//			return sb;
//		}

	}
}