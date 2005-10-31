using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for AccessDeleteQuery.
	/// </summary>
	public class AccessDeleteQuery : DeleteQuery
	{
		public AccessDeleteQuery():base(DbType.ACCESS, DbProviderType.OLEDB){}
		public AccessDeleteQuery(string table):base(DbType.ACCESS, DbProviderType.OLEDB, table){}
	}
}
