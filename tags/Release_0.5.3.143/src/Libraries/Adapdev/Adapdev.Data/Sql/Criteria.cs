namespace Adapdev.Data.Sql
{
	using System.Collections;
	using System.Text;
	using Adapdev.Text;

	/// <summary>
	/// Summary description for Criteria.
	/// </summary>
	public abstract class Criteria : ICriteria
	{
		protected StringBuilder sb = new StringBuilder();
		protected DbType type = DbType.SQLSERVER;
		protected DbProviderType provider = DbProviderType.SQLSERVER;

		public Criteria(DbType type, DbProviderType provider)
		{
			this.type = type;
			this.provider = provider;
		}

		public Criteria(DbType type, DbProviderType provider, string sql): this(type, provider)
		{
			sql = sql.Replace("WHERE", "");
			this.AddSql(sql);
		}

		public void AddAnd()
		{
			this.AddCriteriaSeparator(CriteriaType.AND);
		}

		public virtual void AddAndCriteria(ICriteria c)
		{
			this.AddAnd();
			sb.Append("(");
			sb.Append(c.GetText());
			sb.Append(") ");
		}

		public virtual void AddCriteriaSeparator(CriteriaType ct)
		{
			if (ct == CriteriaType.AND) sb.Append(" AND ");
			else sb.Append(" OR ");
		}

		public virtual void AddBetween(string columnName, object value1, object value2)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" BETWEEN ");
			sb.Append(QueryHelper.DressUp(value1,this.type));
			sb.Append(" AND ");
			sb.Append(QueryHelper.DressUp(value2,this.type));
			sb.Append(" ");
		}

		public virtual void AddEqualTo(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" = ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddEqualTo(string tableName, string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(tableName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(".");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" = ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public void AddEqualTo(string columnName)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" = ");
			sb.Append(QueryHelper.GetParameterName(columnName, this.DbProviderType));
			sb.Append(" ");
		}

		public virtual void AddExists(IQuery subQuery)
		{
		}

		public virtual void AddGreaterThanOrEqualTo(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" >= ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddGreaterThan(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" > ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddIn(string columnName, IQuery subQuery)
		{
			sb.Append(" IN (");
			sb.Append(subQuery.GetText());
			sb.Append(") ");
		}

		public virtual void AddIn(string columnName, ICollection values)
		{
			StringBuilder sbo = new StringBuilder();
			sb.Append(columnName);
			sb.Append(" IN (");
			IEnumerator enumerator = values.GetEnumerator();
			while (enumerator.MoveNext())
			{
				sbo.Append(QueryHelper.DressUp(enumerator.Current, this.type) + ", ");
			}
			sb.Append(StringUtil.RemoveFinalComma(sbo.ToString()));
			sb.Append(") ");
		}

		public virtual void AddIsNull(string columnName)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" IS NULL ");
		}

		public virtual void AddLessThanOrEqualTo(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" <= ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddLessThan(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" < ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddLike(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" LIKE ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddNotBetween(string columnName, object value1, object value2)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" NOT BETWEEN ");
			sb.Append(QueryHelper.DressUp(value1, this.type));
			sb.Append(" AND ");
			sb.Append(QueryHelper.DressUp(value2, this.type));
			sb.Append(" ");
		}

		public virtual void AddNotEqualTo(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" <> ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddNotExists(IQuery subQuery)
		{
			sb.Append(" EXISTS (" + subQuery.GetText() + ") ");
		}

		public virtual void AddNotIn(string columnName, ICollection values)
		{
			StringBuilder sbo = new StringBuilder();
			sb.Append(" NOT IN (");
			IEnumerator enumerator = values.GetEnumerator();
			while (enumerator.MoveNext())
			{
				sbo.Append(QueryHelper.DressUp(enumerator.Current, this.type) + ", ");
			}
			sb.Append(StringUtil.RemoveFinalComma(sbo.ToString()));
			sb.Append(")");
		}

		public virtual void AddNotIn(string columnName, IQuery subQuery)
		{
			sb.Append(" NOT IN (" + subQuery.GetText() + ") ");
		}

		public virtual void AddNotLike(string columnName, object columnValue)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" NOT LIKE ");
			sb.Append(QueryHelper.DressUp(columnValue,this.type));
			sb.Append(" ");
		}

		public virtual void AddNotNull(string columnName)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" NOT IS NULL ");
		}

		public void AddOr()
		{
			this.AddCriteriaSeparator(CriteriaType.OR);
		}

		public void AddOrCriteria(ICriteria c)
		{
			this.AddOr();
			sb.Append("(" + c.GetText() + ")");
		}

		public virtual void AddSql(string sql)
		{
			sb.Append(sql);
		}

		public virtual string GetText()
		{
			if (sb.Length > 2)
			{
				return " WHERE " + sb.ToString();
			}
			else
			{
				return "";
			}
		}

		public DbProviderType DbProviderType
		{
			get { return this.provider; }
			set { this.provider = value; }
		}

	}

	public enum CriteriaType
	{
		AND,
		OR
	}
}
