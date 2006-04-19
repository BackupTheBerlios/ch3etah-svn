namespace Adapdev.Data.Sql
{
	using System.Collections;
	using System.Text;
	using Adapdev.Text;

	/// <summary>
	/// Summary description for ICriteria.
	/// </summary>
	public class OracleCriteria : Criteria
	{
		public OracleCriteria() : base(DbType.ORACLE, DbProviderType.ORACLE)
		{
		}

		public OracleCriteria(string sql) : base(DbType.ORACLE, DbProviderType.ORACLE, sql)
		{
		}
	}
}