namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for SqlDeleteQuery.
	/// </summary>
	public class SqlDeleteQuery : DeleteQuery
	{
		public SqlDeleteQuery() : base(DbType.SQLSERVER, DbProviderType.SQLSERVER)
		{
		}

		public SqlDeleteQuery(string tableName) : base(DbType.SQLSERVER, DbProviderType.SQLSERVER, tableName)
		{
		}
	}
}