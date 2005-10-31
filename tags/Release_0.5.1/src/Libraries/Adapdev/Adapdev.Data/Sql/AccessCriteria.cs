namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for AccessCriteria.
	/// </summary>
	public class AccessCriteria : Criteria
	{
		public AccessCriteria() : base(DbType.ACCESS, DbProviderType.OLEDB)
		{
		}

		public AccessCriteria(string sql) : base(DbType.ACCESS, DbProviderType.OLEDB, sql)
		{
		}
	}
}