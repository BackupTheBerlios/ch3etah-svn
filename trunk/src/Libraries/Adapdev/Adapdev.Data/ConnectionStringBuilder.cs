namespace Adapdev.Data {
	using System;

	/// <summary>
	/// Utility class to build connection strings
	/// </summary>
	public class ConnectionStringBuilder {
		private static ProviderConfig _databases = new ProviderConfig();

		private ConnectionStringBuilder() {}

		private ConnectionStringBuilder( string config ) {
			_databases = new ProviderConfig( config ); // Build using a override config file
		}

		/// <summary>
		/// Getter - Provides access to the Databases (DbConnectionTypes) collection of available
		/// database types, which in turn has available providers. 
		/// </summary>
		public DbConnectionTypes Databases {
			get { return _databases.ConnectionTypes; }
		}

		/// <summary>
		/// Builds a connection string for an Access database
		/// </summary>
		/// <param name="type">The DbProviderType to use</param>
		/// <param name="filePath">The file path to the database</param>
		/// <returns></returns>
		public static string BuildAccess( DbProviderType type, string filePath ) {
			return _databases.ConnectionTypes[ "ACCESS" ].Providers[ "OLEDB" ].ConnectionString( filePath );
		}

		/// <summary>
		/// Builds a connection string for an Access database
		/// </summary>
		/// <param name="type">The DbProviderType to use</param>
		/// <param name="filePath">The file path to the database</param>
		/// <param name="password">The database password</param>
		/// <param name="userid">The database userid</param>
		/// <returns></returns>
		public static string BuildAccess( DbProviderType type, string filePath, string userid, string password ) {
			return _databases.ConnectionTypes[ "ACCESS" ].Providers[ "OLEDB" ].ConnectionString( filePath, userid, password );
		}

		/// <summary>
		/// Builds a connection string for a specified DSN entry
		/// </summary>
		/// <param name="symbolicName">The DSN name</param>
		/// <param name="userid">The password</param>
		/// <param name="password">The userid</param>
		/// <returns></returns>
		public static string BuildDSN( string symbolicName, string userid, string password ) {
			return _databases.ConnectionTypes[ "ACCESS" ].Providers[ "OLEDB (DNS)" ].ConnectionString( symbolicName, userid, password );
		}

		/// <summary>
		/// Builds a Sql Server connection
		/// </summary>
		/// <param name="type">The DbProviderType to use</param>
		/// <param name="server">The server</param>
		/// <param name="database">The database</param>
		/// <param name="userid">The userid</param>
		/// <param name="password">The password</param>
		/// <returns></returns>
		public static string BuildSqlServer( DbProviderType type, string server, string database, string userid, string password ) {
			switch (type) {
			case DbProviderType.ODBC:
				return _databases.ConnectionTypes[ "SQL Server" ].Providers[ "ODBC" ].ConnectionString( server, database, userid, password );
			case DbProviderType.OLEDB:
				return _databases.ConnectionTypes[ "SQL Server" ].Providers[ "OLEDB" ].ConnectionString( server, database, userid, password );
			case DbProviderType.SQLSERVER:
				return _databases.ConnectionTypes[ "SQL Server" ].Providers[ "Sql Connect" ].ConnectionString( server, database, userid, password );
			default:
				throw new Exception( type.ToString() + " is not supported.  Please use DbType.ODBC, DbType.OLEDB or DbType.SQLSERVER" );
			}
		}

		/// <summary>
		/// Builds a trusted Sql Server connection
		/// </summary>
		/// <param name="type">The DbProviderType</param>
		/// <param name="server">The server</param>
		/// <param name="database">The database</param>
		/// <returns></returns>
		public static string BuildSqlServerTrusted( DbProviderType type, string server, string database ) {
			switch (type) {
			case DbProviderType.ODBC:
				return _databases.ConnectionTypes[ "SQL Server - Trusted" ].Providers[ "ODBC" ].ConnectionString( server, database );
			case DbProviderType.OLEDB:
				return _databases.ConnectionTypes[ "SQL Server - Trusted" ].Providers[ "OLEDB" ].ConnectionString( server, database );
			case DbProviderType.SQLSERVER:
				return _databases.ConnectionTypes[ "SQL Server - Trusted" ].Providers[ "Sql Connect" ].ConnectionString( server, database );
			default:
				throw new Exception( type.ToString() + " is not supported.  Please use DbType.ODBC, DbType.OLEDB or DbType.SQLSERVER" );
			}
		}

	}
}