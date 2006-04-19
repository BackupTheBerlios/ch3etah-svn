using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for MySqlUpdateQuery.
	/// </summary>
	public class MySqlUpdateQuery : UpdateQuery
	{
		public MySqlUpdateQuery():base(DbType.MYSQL, DbProviderType.MYSQL){}
		public MySqlUpdateQuery(string table):base(DbType.MYSQL, DbProviderType.MYSQL, table){}
	}
}
