namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for SqlInsertQuery.
	/// </summary>
	public class OracleInsertQuery : InsertQuery
	{
		public OracleInsertQuery() : base(DbType.ORACLE, DbProviderType.ORACLE)
		{
		}

		public OracleInsertQuery(string tableName) : base(DbType.ORACLE, DbProviderType.ORACLE, tableName)
		{
		}
	}
}