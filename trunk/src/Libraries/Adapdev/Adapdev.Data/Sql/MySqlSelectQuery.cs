using System;

namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for MySqlSelectQuery.
	/// </summary>
	public class MySqlSelectQuery : SelectQuery
	{
		public MySqlSelectQuery():base(DbType.MYSQL, DbProviderType.MYSQL){}
		public MySqlSelectQuery(string table):base(DbType.MYSQL, DbProviderType.MYSQL, table){}

		protected override string GetLimit()
		{
			if (maxRecords > 0)
			{
				return " LIMIT " + maxRecords;
			}
			return "";
		}

		public override string GetText()
		{
			return "SELECT " + this.GetColumns() + " FROM " + this._table + this._join + this.GetCriteria() + this.GetOrderBy() + this.GetGroupBy() + this.GetLimit();	
		}


	}
}
