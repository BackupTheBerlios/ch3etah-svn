using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for MySqlInsertQuery.
	/// </summary>
	public class MySqlInsertQuery : InsertQuery
	{
		public MySqlInsertQuery():base(DbType.MYSQL, DbProviderType.MYSQL){}
		public MySqlInsertQuery(string table):base(DbType.MYSQL, DbProviderType.MYSQL, table){}
	}
}
