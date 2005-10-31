namespace Adapdev.Data.Sql
{
	public class OracleSelectQuery : SelectQuery
	{
		public OracleSelectQuery() : base(DbType.ORACLE, DbProviderType.ORACLE)
		{
		}

		public OracleSelectQuery(string tableName) : base(DbType.ORACLE, DbProviderType.ORACLE, tableName)
		{
		}

		protected override string GetLimit()
		{
			if(this.maxRecords > 0)
			{
				return " ROWNUM <= " + this.maxRecords;
			}
			return "";
		}

		public override string GetText()
		{
			string sql = "SELECT " + this.GetColumns() + " FROM " + this._table + this._join + this.GetCriteria();
			if(this.maxRecords > 0)
			{
				if(sql.ToLower().IndexOf("where") < 1) sql+= " WHERE ";
				else sql += " AND ";

				sql += this.GetLimit();
			}
			sql += this.GetOrderBy() + this.GetGroupBy();
			return sql;
		}
	}
}