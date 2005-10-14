namespace Adapdev.Data.Sql
{
	public class SqlSelectQuery : SelectQuery
	{
		public SqlSelectQuery() : base(DbType.SQLSERVER, DbProviderType.SQLSERVER)
		{
		}

		public SqlSelectQuery(string tableName) : base(DbType.SQLSERVER, DbProviderType.SQLSERVER, tableName)
		{
		}

	}
}