namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for DialectConstants.
	/// </summary>
	public class DialectConstants
	{
		public const char ACCESS_PREDELIM = '[';
		public const char ACCESS_POSTDELIM = ']';
		public const char ACCESS_DATE = '#';
		public const char ACCESS_STRING = '\'';

		public const char SQLSERVER_PREDELIM = '[';
		public const char SQLSERVER_POSTDELIM = ']';
		public const char SQLSERVER_DATE = '\'';
		public const char SQLSERVER_STRING = '\'';

		public const char ORACLE_PREDELIM = ' ';
		public const char ORACLE_POSTDELIM = ' ';
		public const char ORACLE_DATE = '\'';
		public const char ORACLE_STRING = '\'';

		public const char MYSQL_PREDELIM = '`';
		public const char MYSQL_POSTDELIM = '`';
		public const char MYSQL_DATE = '\'';
		public const char MYSQL_STRING = '\'';
	}
}