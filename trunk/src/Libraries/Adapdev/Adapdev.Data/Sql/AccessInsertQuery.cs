using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for AccessInsertQuery.
	/// </summary>
	public class AccessInsertQuery : InsertQuery
	{
		public AccessInsertQuery():base(DbType.ACCESS, DbProviderType.OLEDB){}
		public AccessInsertQuery(string table):base(DbType.ACCESS, DbProviderType.OLEDB, table){}
	}
}
