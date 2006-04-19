namespace Adapdev.Data.Sql
{
	using System;
	using System.Collections;
	using System.Text;
	using Adapdev.Text;

	public abstract class SelectQuery : ISelectQuery
	{
		protected string _table = "";
		protected StringBuilder sb = new StringBuilder();
		protected Queue order = new Queue();
		protected Queue group = new Queue();
		protected OrderBy ob = OrderBy.ASCENDING;
		protected ICriteria criteria = null;
		protected DbType type = DbType.SQLSERVER;
		protected DbProviderType provider = DbProviderType.SQLSERVER;
		protected int maxRecords = 0;
		protected string _join = "";

		public SelectQuery(DbType type, DbProviderType provider)
		{
			this.type = type;
			this.provider = provider;
		}

		public SelectQuery(DbType type, DbProviderType provider, string tableName): this(type, provider)
		{
			this.SetTable(tableName);
		}

		public void SetCriteria(ICriteria c)
		{
			criteria = c;
		}

		public void Add(string columnName)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(",");
		}

		public void Add(string tableName, string columnName)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(tableName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(".");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(",");
		}

		public void AddColumnAlias(string columnName, string alias)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" AS ");
			sb.Append(alias);
			sb.Append(",");
		}

		public void AddColumnAlias(string tableName, string columnName, string alias)
		{
			sb.Append(" ");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(tableName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(".");
			sb.Append(QueryHelper.GetPreDelimeter(this.type));
			sb.Append(columnName);
			sb.Append(QueryHelper.GetPostDelimeter(this.type));
			sb.Append(" AS ");
			sb.Append(alias);
			sb.Append(",");
		}

		public void AddAll()
		{
			sb.Append(" * ");
		}

		public virtual void AddCount(string columnName)
		{
			sb.Append(" COUNT(" + QueryHelper.GetPreDelimeter(this.type) + columnName + QueryHelper.GetPostDelimeter(this.type) + ") ");
		}

		public virtual void AddCountAll()
		{
			sb.Append(" COUNT(*) ");
		}

		public void AddOrderBy(string columnName)
		{
			order.Enqueue(QueryHelper.GetPreDelimeter(this.type) + columnName + QueryHelper.GetPostDelimeter(this.type));
		}

		public void AddOrderBy(params string[] columns)
		{
			foreach (string s in columns)
			{
				this.AddOrderBy(s);
			}
		}

		public void AddGroupBy(string columnName)
		{
			group.Enqueue(QueryHelper.GetPreDelimeter(this.type) + columnName + QueryHelper.GetPostDelimeter(this.type));
		}

		public void AddGroupBy(params string[] columns)
		{
			foreach (string s in columns)
			{
				this.AddGroupBy(s);
			}
		}

		public virtual void AddJoin(string secondTable, string firstTableColumn, string secondTableColumn, JoinType type)
		{
			this._join = String.Format(" {0} {1} ON {2}.{3} = {4}.{5} ",	this.GetJoinType(type), 
																			QueryHelper.GetPreDelimeter(this.type) + secondTable + QueryHelper.GetPostDelimeter(this.type), 
																			QueryHelper.GetPreDelimeter(this.type) + this._table + QueryHelper.GetPostDelimeter(this.type), 
																			QueryHelper.GetPreDelimeter(this.type) + firstTableColumn + QueryHelper.GetPostDelimeter(this.type),
																			QueryHelper.GetPreDelimeter(this.type) + secondTable + QueryHelper.GetPostDelimeter(this.type),
																			QueryHelper.GetPreDelimeter(this.type) + secondTableColumn + QueryHelper.GetPostDelimeter(this.type));
		}

		public void SetTable(string tableName)
		{
			this._table = QueryHelper.GetPreDelimeter(this.type) + tableName + QueryHelper.GetPostDelimeter(this.type);
		}

		public virtual string GetText()
		{
			return "SELECT " + this.GetLimit() + this.GetColumns() + " FROM " + this._table + this._join + this.GetCriteria() + this.GetOrderBy() + this.GetGroupBy();
		}

		/// <summary>
		/// The DbProviderType for this query.  Necessary to determine how to
		/// represent dates, parameters, etc.
		/// </summary>
		public DbProviderType DbProviderType
		{
			get { return this.provider; }
			set { this.provider = value; }
		}

		public OrderBy OrderBy
		{
			get { return this.ob; }
			set { this.ob = value; }
		}

		public void SetLimit(int maxRecords)
		{
			this.maxRecords = maxRecords;
		}

		public ICriteria CreateCriteria()
		{
			return CriteriaFactory.CreateCriteria(this.type);
		}

		protected string GetColumns()
		{
			return StringUtil.RemoveFinalComma(this.sb.ToString());
		}

		protected virtual string GetOrderBy()
		{
			StringBuilder sbo = new StringBuilder();
			if (order.Count > 0)
			{
				sbo.Append(" ORDER BY ");
				IEnumerator enumerator = order.GetEnumerator();
				while (enumerator.MoveNext())
				{
					sbo.Append(enumerator.Current + ", ");
				}
				string s = StringUtil.RemoveFinalComma(sbo.ToString());
				s += this.TranslateOrderBy();
				return s;
			}
			return "";
		}

		protected virtual string GetGroupBy()
		{
			StringBuilder sbo = new StringBuilder();
			if (group.Count > 0)
			{
				sbo.Append(" GROUP BY ");
				IEnumerator enumerator = group.GetEnumerator();
				while (enumerator.MoveNext())
				{
					sbo.Append(enumerator.Current + ", ");
				}
				return StringUtil.RemoveFinalComma(sbo.ToString());
			}
			return "";
		}

		protected virtual string GetLimit()
		{
			if (this.maxRecords > 0)
			{
				return " TOP " + this.maxRecords;
			}
			return "";
		}

		protected string GetCriteria()
		{
			if (this.criteria == null) return "";
			else return criteria.GetText();
		}

		protected string GetJoinType(JoinType type)
		{
			switch (type)
			{
				case JoinType.INNER:
					return "INNER JOIN";
				case JoinType.LEFT:
					return "LEFT OUTER JOIN";
				case JoinType.RIGHT:
					return "RIGHT OUTER JOIN";
				default:
					throw new Exception("JoinType " + type + " not supported.");
			}
		}

		protected virtual string TranslateOrderBy()
		{
			if (this.ob == OrderBy.DESCENDING)
			{
				return " DESC ";
			}
			else
			{
				return " ASC ";
			}
		}
	}

}