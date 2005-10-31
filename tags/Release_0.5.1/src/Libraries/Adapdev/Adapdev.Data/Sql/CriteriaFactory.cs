namespace Adapdev.Data.Sql
{
	using System;

	/// <summary>
	/// Summary description for CriteriaFactory.
	/// </summary>
	public class CriteriaFactory
	{
		public static ICriteria CreateCriteria(DbType type)
		{
			switch (type)
			{
				case DbType.ACCESS:
					return new AccessCriteria();
				case DbType.SQLSERVER:
					return new SqlServerCriteria();
				case DbType.ORACLE:
					return new OracleCriteria();
				case DbType.MYSQL:
					return new MySqlCriteria();
				default:
					throw new Exception("DbType " + type + " not supported currently.");
			}
		}
	}
}