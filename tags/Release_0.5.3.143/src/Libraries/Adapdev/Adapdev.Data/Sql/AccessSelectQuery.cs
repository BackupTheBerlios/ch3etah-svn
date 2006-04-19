using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for AccessSelectQuery.
	/// </summary>
	public class AccessSelectQuery : SelectQuery
	{
		public AccessSelectQuery():base(DbType.ACCESS, DbProviderType.OLEDB){}
		public AccessSelectQuery(string table):base(DbType.ACCESS, DbProviderType.OLEDB, table){}
	}
}
