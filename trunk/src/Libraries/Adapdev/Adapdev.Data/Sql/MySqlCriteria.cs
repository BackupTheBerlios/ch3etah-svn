namespace Adapdev.Data.Sql
{
	using System.Collections;
	using System.Text;
	using Adapdev.Text;

	/// <summary>
	/// Summary description for MySqlCriteria.
	/// </summary>
	public class MySqlCriteria : Criteria
	{
		public MySqlCriteria() : base(DbType.MYSQL, DbProviderType.MYSQL)
		{
		}

		public MySqlCriteria(string sql) : base(DbType.MYSQL, DbProviderType.MYSQL, sql)
		{
		}
	}
}