namespace Adapdev.Data
{
	using System;

	/// <summary>
	/// Summary description for DbProviderTypeConverter.
	/// </summary>
	public class DbProviderTypeConverter
	{
		public static DbProviderType Convert(string s)
		{
			switch (s.Trim().ToLower())
			{
				case "unknown":
					return DbProviderType.UNKNOWN;
				case "db2":
					return DbProviderType.DB2;
				case "mysql":
					return DbProviderType.MYSQL;
				case "odbc":
					return DbProviderType.ODBC;
				case "oledb":
					return DbProviderType.OLEDB;
				case "oracle":
					return DbProviderType.ORACLE;
				case "sqlserver":
				case "sqlclient":
					return DbProviderType.SQLSERVER;
				default:
					throw new Exception("DbProviderType " + s + " not found.");
			}
		}

		public static string Convert(DbProviderType t) {

			switch (t) {
				case DbProviderType.UNKNOWN:
					return "UNKNOWN";
				case DbProviderType.ODBC:
					return "ODBC";
				case DbProviderType.OLEDB:
					return "OLEDB";
				case DbProviderType.ORACLE:
					return "ORACLE";
				case DbProviderType.SQLSERVER:
					return "SQLSERVER";
				case DbProviderType.MYSQL:
					return "MYSQL";
				case DbProviderType.DB2:
					return "DB2";
				default:
					throw new Exception("DbProviderType " + t.ToString() + " not found.");
			}
		}

	}
}