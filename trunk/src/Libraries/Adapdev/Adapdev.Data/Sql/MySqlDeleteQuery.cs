using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for MySqlDeleteQuery.
	/// </summary>
	public class MySqlDeleteQuery : DeleteQuery
	{
		public MySqlDeleteQuery():base(DbType.MYSQL, DbProviderType.MYSQL){}
		public MySqlDeleteQuery(string table):base(DbType.MYSQL, DbProviderType.MYSQL, table){}
	}
}
