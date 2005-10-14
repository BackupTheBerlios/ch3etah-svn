namespace Adapdev.Data.Sql
{
	using System.Text;
	using Adapdev.Text;

	/// <summary>
	/// Summary description for UpdateQuery.
	/// </summary>
	public abstract class InsertQuery : IInsertQuery
	{
		protected string _table = "";
		protected StringBuilder sbn = new StringBuilder();
		protected StringBuilder sbv = new StringBuilder();
		protected string[] cnames = new string[100];
		protected string[] cvalues = new string[100];
		protected int cindex = 0;
		protected ICriteria criteria = null;
		protected DbType type = DbType.SQLSERVER;
		protected DbProviderType provider = DbProviderType.SQLSERVER;

		public InsertQuery(DbType type, DbProviderType provider)
		{
			this.type = type;
			this.provider = provider;
		}

		public InsertQuery(DbType type, DbProviderType provider, string tableName) : this(type, provider)
		{
			this.SetTable(tableName);
		}

		public void SetCriteria(ICriteria c)
		{
			criteria = c;
		}

		public void Add(string columnName)
		{
			cnames[cindex] = columnName;
			cvalues[cindex] = QueryHelper.GetParameterName(columnName, this.provider);
			cindex++;
		}

		public void Add(string columnName, object columnValue)
		{
			cnames[cindex] = columnName;
			cvalues[cindex] = QueryHelper.DressUp(columnValue, this.type);
			cindex++;
		}

		public void SetTable(string tableName)
		{
			this._table = QueryHelper.GetPreDelimeter(this.type) + tableName + QueryHelper.GetPostDelimeter(this.type);
		}

		public ICriteria CreateCriteria()
		{
			return CriteriaFactory.CreateCriteria(this.type);
		}

		public virtual string GetText()
		{
			return "INSERT INTO " + this._table + " ( " + this.GetColumnNames() + " ) VALUES ( " + this.GetColumnValues() + " ) " + this.GetCriteria();
		}

		protected string GetColumnNames()
		{
			sbn.Remove(0, sbn.Length);
			for (int i = 0; i <= cindex; i++)
			{
				if (cnames[i] != null && cnames[i].Length > 0)
				{
					sbn.Append(QueryHelper.GetPreDelimeter(this.type) + cnames[i] + QueryHelper.GetPostDelimeter(this.type) + ", ");
				}
			}
			return StringUtil.RemoveFinalComma(this.sbn.ToString());
		}

		protected string GetColumnValues()
		{
			sbv.Remove(0, sbv.Length);
			for (int i = 0; i <= cindex; i++)
			{
				if (cnames[i] != null && cnames[i].Length > 0)
				{
					sbv.Append(cvalues[i] + ", ");
				}
			}
			return StringUtil.RemoveFinalComma(this.sbv.ToString());
		}

		protected string GetCriteria()
		{
			if (this.criteria == null) return "";
			else return criteria.GetText();
		}

		public DbProviderType DbProviderType
		{
			get { return this.provider; }
			set { this.provider = value; }
		}
	}
}