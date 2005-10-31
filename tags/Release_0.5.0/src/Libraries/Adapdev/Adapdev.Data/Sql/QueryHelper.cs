namespace Adapdev.Data.Sql
{
	using System;
	using System.Data;
	using Adapdev.Text;

	/// <summary>
	/// Provides common routines for building queries
	/// </summary>
	public class QueryHelper
	{
		/// <summary>
		/// Surrounds the object with the proper, datastore-specific markup.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		/// <example>
		/// If the passed in object is a date, and the DbType is Access, then the returned value would be #date#.
		/// In contrast, if the DbType is SqlServer, then the returned value would be 'date'.
		/// </example>
		public static string DressUp(object o, Adapdev.Data.DbType type)
		{
			string ro = "";
			if (Util.IsNumeric(o))
			{
				ro = o.ToString();
			}
			else if (Util.IsDateTime(o))
			{
				ro = QueryHelper.GetDateDelimeter(type) + o.ToString() + QueryHelper.GetDateDelimeter(type);
			}
//			else if(o is Boolean)
//			{
//				ro = o.ToString().ToLower();
//			}
			else
			{
				ro = QueryHelper.GetStringDelimeter(type) + o.ToString() + QueryHelper.GetStringDelimeter(type);
			}
			return ro;
		}

		/// <summary>
		/// Gets the proper date delimiter for the specified DbType
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static char GetDateDelimeter(Adapdev.Data.DbType type)
		{
			switch (type)
			{
				case Adapdev.Data.DbType.ACCESS:
					return DialectConstants.ACCESS_DATE;
				case Adapdev.Data.DbType.SQLSERVER:
					return DialectConstants.SQLSERVER_DATE;
				case Adapdev.Data.DbType.ORACLE:
					return DialectConstants.ORACLE_DATE;
				case Adapdev.Data.DbType.MYSQL:
					return DialectConstants.MYSQL_DATE;
				default:
					throw new Exception("DbType " + type + " not supported currently.");
			}
		}

		/// <summary>
		/// Gets the specified
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static char GetPreDelimeter(Adapdev.Data.DbType type)
		{
			switch (type)
			{
				case Adapdev.Data.DbType.ACCESS:
					return DialectConstants.ACCESS_PREDELIM;
				case Adapdev.Data.DbType.SQLSERVER:
					return DialectConstants.SQLSERVER_PREDELIM;
				case Adapdev.Data.DbType.ORACLE:
					return DialectConstants.ORACLE_PREDELIM;
				case Adapdev.Data.DbType.MYSQL:
					return DialectConstants.MYSQL_PREDELIM;
				default:
					throw new Exception("DbType " + type + " not supported currently.");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static char GetPostDelimeter(Adapdev.Data.DbType type)
		{
			switch (type)
			{
				case Adapdev.Data.DbType.ACCESS:
					return DialectConstants.ACCESS_POSTDELIM;
				case Adapdev.Data.DbType.SQLSERVER:
					return DialectConstants.SQLSERVER_POSTDELIM;
				case Adapdev.Data.DbType.ORACLE:
					return DialectConstants.ORACLE_POSTDELIM;
				case Adapdev.Data.DbType.MYSQL:
					return DialectConstants.MYSQL_POSTDELIM;
				default:
					throw new Exception("DbType " + type + " not supported currently.");
			}
		}

		/// <summary>
		/// Gets the datastore-specific string delimiter
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static char GetStringDelimeter(Adapdev.Data.DbType type)
		{
			switch (type)
			{
				case Adapdev.Data.DbType.ACCESS:
					return DialectConstants.ACCESS_STRING;
				case Adapdev.Data.DbType.SQLSERVER:
					return DialectConstants.SQLSERVER_STRING;
				case Adapdev.Data.DbType.ORACLE:
					return DialectConstants.ORACLE_STRING;
				case Adapdev.Data.DbType.MYSQL:
					return DialectConstants.MYSQL_STRING;
				default:
					throw new Exception("DbType " + type + " not supported currently.");
			}
		}

		/// <summary>
		/// Gets the provider type specific parameter name
		/// </summary>
		/// <param name="columnName"></param>
		/// <param name="provider"></param>
		/// <returns></returns>
		public static string GetParameterName(string columnName, DbProviderType provider)
		{
			switch (provider)
			{
				case DbProviderType.SQLSERVER:
					columnName = StringUtil.RemoveSpaces(columnName);
					return "@" + columnName;
				case DbProviderType.ORACLE:
					columnName = StringUtil.RemoveSpaces(columnName);
					return ":" + columnName;
				case DbProviderType.OLEDB:
					return "?";
				case DbProviderType.MYSQL:
					columnName = StringUtil.RemoveSpaces(columnName);
					return "?" + columnName;
				default:
					throw new Exception("DbProviderType " + provider + " is not currently supported.");
			}
		}

		public static string GetSqlServerLastInsertedCommand(string table)
		{
			return "SELECT IDENT_CURRENT('" + table + "');";
		}

		public static string GetAccessLastInsertedCommand(string table, string column)
		{
			string s = "SELECT MAX([" + column + "]) FROM [" + table + "];";
			Console.WriteLine(s);
			return s;
		}

		public static string GetOracleLastInsertedCommand(string table, string column) {
			string s = "SELECT MAX(" + column + ") FROM " + table + ";";
			Console.WriteLine(s);
			return s;
		}

		public static string GetMySqlLastInsertedCommand(string table, string column)
		{
			string s = "SELECT `" + column + "` FROM `" + table + "` ORDER BY `" + column + "` DESC LIMIT 1";
			Console.WriteLine(s);
			return s;
		}
	}
}