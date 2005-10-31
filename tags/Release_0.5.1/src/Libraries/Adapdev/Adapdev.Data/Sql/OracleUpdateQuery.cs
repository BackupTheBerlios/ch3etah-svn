namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for UpdateQuery.
	/// </summary>
	public class OracleUpdateQuery : UpdateQuery
	{
		public OracleUpdateQuery() : base(DbType.ORACLE, DbProviderType.ORACLE)
		{
		}

		public OracleUpdateQuery(string tableName) : base(DbType.ORACLE, DbProviderType.ORACLE, tableName)
		{
		}

	}
}