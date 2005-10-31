namespace Adapdev.Data
{
	using System;

	/// <summary>
	/// Converts strings to their corresponding DbType
	/// </summary>
	public class DbTypeConverter
	{
		public static DbType Convert(string s)
		{
			switch (s.Trim().ToLower())
			{
				case "unknown":
					return DbType.UNKNOWN;
				case "access":
					return DbType.ACCESS;
				case "db2":
					return DbType.DB2;
				case "mysql":
					return DbType.MYSQL;
				case "oracle":
					return DbType.ORACLE;
				case "sqlserver":
				case "sqlclient":
					return DbType.SQLSERVER;
				default:
					throw new Exception("DbType " + s + " not found.");
			}
		}

		public static string Convert(DbType t) {
			switch (t) {
				case DbType.UNKNOWN:
					return "UNKNOWN";
				case DbType.ACCESS:
					return "ACCESS";
				case DbType.DB2:
					return "DB2";
				case DbType.MYSQL:
					return "MYSQL";
				case DbType.ORACLE:
					return "ORACLE";
				case DbType.SQLSERVER:
					return "SQLSERVER";
				default:
					throw new Exception("DbType " + t.ToString() + " not found.");
			}
		}

	}
}
