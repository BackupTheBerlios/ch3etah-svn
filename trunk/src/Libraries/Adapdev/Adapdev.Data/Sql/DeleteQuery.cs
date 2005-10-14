namespace Adapdev.Data.Sql
{
	using System.Text;

	public abstract class DeleteQuery : IDeleteQuery
	{
		protected string _table = "";
		protected StringBuilder sb = new StringBuilder();
		protected ICriteria criteria = null;
		protected DbType type = DbType.SQLSERVER;
		protected DbProviderType provider = DbProviderType.SQLSERVER;

		public DeleteQuery(DbType type, DbProviderType provider)
		{
			this.type = type;
			this.provider = provider;
		}

		public DeleteQuery(DbType type, DbProviderType provider, string tableName) : this(type, provider)
		{
			this.SetTable(tableName);
		}

		public void SetCriteria(ICriteria c)
		{
			criteria = c;
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
			return "DELETE FROM " + this._table + this.GetCriteria();
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