namespace Adapdev.Data.Sql
{
	using System.Collections;
	using System.Text;
	using Adapdev.Text;

	/// <summary>
	/// Summary description for ICriteria.
	/// </summary>
	public class SqlServerCriteria : Criteria
	{
		public SqlServerCriteria() : base(DbType.SQLSERVER, DbProviderType.SQLSERVER)
		{
		}

		public SqlServerCriteria(string sql) : base(DbType.SQLSERVER, DbProviderType.SQLSERVER, sql)
		{
		}
	}
}