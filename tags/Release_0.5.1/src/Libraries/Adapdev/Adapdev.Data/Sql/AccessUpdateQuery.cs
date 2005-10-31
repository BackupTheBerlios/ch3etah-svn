using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for AccessUpdateQuery.
	/// </summary>
	public class AccessUpdateQuery : UpdateQuery
	{
		public AccessUpdateQuery():base(DbType.ACCESS, DbProviderType.OLEDB){}
		public AccessUpdateQuery(string table):base(DbType.ACCESS, DbProviderType.OLEDB, table){}
	}
}
