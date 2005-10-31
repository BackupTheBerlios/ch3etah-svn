namespace Adapdev.Data
{
	using System;
	using System.Data;
	using System.Data.Common;
	using System.Data.OleDb;
	using System.Data.SqlClient;
	using System.Data.OracleClient;
//	using MySql.Data.MySqlClient;

	/// <summary>
	/// Provides database neutral access to ADO.NET classes
	/// </summary>
	public class DbProviderFactory
	{
		#region IDbCommand

		/// <summary>
		/// Creates an IDbCommand implementation for the specified DbProviderType
		/// </summary>
		/// <param name="DbProviderType">The DbProviderType to use</param>
		/// <returns></returns>
		public static IDbCommand CreateCommand(DbProviderType DbProviderType)
		{
			switch (DbProviderType)
			{
				case DbProviderType.SQLSERVER:
					return new SqlCommand();
				case DbProviderType.ORACLE:
					return new OracleCommand();
//				case DbProviderType.MYSQL:
//					return new MySqlCommand();
				default:
					return new OleDbCommand();
			}
		}

		#endregion

		#region IDbConnection

		/// <summary>
		/// Creates an IDbConnection implementation for the specified DbProviderType
		/// </summary>
		/// <param name="DbProviderType"></param>
		/// <returns></returns>
		public static IDbConnection CreateConnection(DbProviderType DbProviderType)
		{
			switch (DbProviderType)
			{
				case DbProviderType.SQLSERVER:
					return new SqlConnection();
				case DbProviderType.ORACLE:
					return new OracleConnection();
//				case DbProviderType.MYSQL:
//					return new MySqlConnection();
				default:
					return new OleDbConnection();
			}
		}

		#endregion

		#region DbDataAdapter

		public static DbDataAdapter CreateDataAdapter(DbProviderType DbProviderType, IDbCommand command, IDbConnection connection)
		{
			switch (DbProviderType)
			{
				case DbProviderType.SQLSERVER:
					SqlDataAdapter sqlda = new SqlDataAdapter();
					sqlda.SelectCommand = (SqlCommand) command;
					command.Connection = connection;
					return sqlda;
				case DbProviderType.ORACLE:
					OracleDataAdapter orada = new OracleDataAdapter();
					orada.SelectCommand = (OracleCommand) command;
					command.Connection = connection;
					return orada;
//				case DbProviderType.MYSQL:
//					MySqlDataAdapter mysqlda = new MySqlDataAdapter();
//					mysqlda.SelectCommand = (MySqlCommand) command;
//					command.Connection = connection;
//					return mysqlda;
				default:
					OleDbDataAdapter oleda = new OleDbDataAdapter();
					oleda.SelectCommand = (OleDbCommand) command;
					command.Connection = connection;
					return oleda;
			}
		}

		#endregion

		#region IDataReader

		public static IDataReader CreateDataReader(string connectionString, IDbCommand command, DbProviderType DbProviderType)
		{
			IDbConnection connection = DbProviderFactory.CreateConnection(DbProviderType);
			connection.ConnectionString = connectionString;

			return CreateDataReader(connection, command, DbProviderType);
		}

		public static IDataReader CreateDataReader(string connectionString, string command, DbProviderType DbProviderType)
		{
			IDbConnection connection = DbProviderFactory.CreateConnection(DbProviderType);
			connection.ConnectionString = connectionString;

			IDbCommand cmd = DbProviderFactory.CreateCommand(DbProviderType);
			cmd.CommandText = command;

			return CreateDataReader(connection, cmd, DbProviderType);
		}

		public static IDataReader CreateDataReader(IDbConnection connection, string command, DbProviderType DbProviderType)
		{
			IDbCommand cmd = DbProviderFactory.CreateCommand(DbProviderType);
			cmd.CommandText = command;

			return CreateDataReader(connection, cmd, DbProviderType);
		}

		public static IDataReader CreateDataReader(IDbConnection connection, IDbCommand command, DbProviderType DbProviderType)
		{
			return CreateDataReader(connection, command, CommandBehavior.Default, DbProviderType);
		}

		public static IDataReader CreateDataReader(IDbConnection connection, IDbCommand command, CommandBehavior behavior, DbProviderType DbProviderType)
		{
			IDataReader reader = null;
			command.Connection = connection;
			reader = command.ExecuteReader(behavior);

			return reader;
		}

		#endregion

		#region DataSet

		public static DataSet CreateDataSet(string connectionString, IDbCommand command, DbProviderType DbProviderType)
		{
			IDbConnection connection = DbProviderFactory.CreateConnection(DbProviderType);
			connection.ConnectionString = connectionString;
			return CreateDataSet(connection, command, DbProviderType);
		}

		public static DataSet CreateDataSet(IDbConnection connection, IDbCommand command, DbProviderType DbProviderType)
		{
			DataSet ds = new DataSet();
			DbDataAdapter da = DbProviderFactory.CreateDataAdapter(DbProviderType, command, connection);
			connection.Open();
			da.Fill(ds);
			connection.Close();
			return ds;
		}

		#endregion

		#region DataTable

		public static DataTable CreateDataTable(string connection, IDbCommand command, DbProviderType DbProviderType)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}