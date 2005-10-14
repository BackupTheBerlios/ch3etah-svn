namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for SqlInsertQuery.
	/// </summary>
	public class SqlInsertQuery : InsertQuery
	{
		public SqlInsertQuery() : base(DbType.SQLSERVER, DbProviderType.SQLSERVER)
		{
		}

		public SqlInsertQuery(string tableName) : base(DbType.SQLSERVER, DbProviderType.SQLSERVER, tableName)
		{
		}
	}
}