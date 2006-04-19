namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for UpdateQuery.
	/// </summary>
	public class SqlUpdateQuery : UpdateQuery
	{
		public SqlUpdateQuery() : base(DbType.SQLSERVER, DbProviderType.SQLSERVER)
		{
		}

		public SqlUpdateQuery(string tableName) : base(DbType.SQLSERVER, DbProviderType.SQLSERVER, tableName)
		{
		}

	}
}