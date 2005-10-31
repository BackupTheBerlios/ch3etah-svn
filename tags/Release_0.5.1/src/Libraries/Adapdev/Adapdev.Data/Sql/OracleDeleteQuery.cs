namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for SqlDeleteQuery.
	/// </summary>
	public class OracleDeleteQuery : DeleteQuery
	{
		public OracleDeleteQuery() : base(DbType.ORACLE, DbProviderType.ORACLE)
		{
		}

		public OracleDeleteQuery(string tableName) : base(DbType.ORACLE, DbProviderType.ORACLE, tableName)
		{
		}
	}
}