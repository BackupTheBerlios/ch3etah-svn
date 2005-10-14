namespace Adapdev.Data.Sql
{
	using System.Text;
	using Adapdev.Text;

	/// <summary>
	/// Summary description for UpdateQuery.
	/// </summary>
	public abstract class UpdateQuery : IUpdateQuery
	{
		protected string _table = "";
		protected StringBuilder sb = new StringBuilder();
		protected ICriteria criteria = null;
		protected DbType type = DbType.SQLSERVER;
		protected DbProviderType provider = DbProviderType.SQLSERVER;

		public UpdateQuery(DbType type, DbProviderType provider)
		{
			this.type = type;
			this.provider = provider;
		}

		public UpdateQuery(DbType type, DbProviderType provider, string tableName) : this(type, provider)
		{
			this.SetTable(tableName);
		}

		public void SetCriteria(ICriteria c)
		{
			criteria = c;
		}

		public void Add(string columnName)
		{
			sb.Append(" " + QueryHelper.GetPreDelimeter(this.type) + columnName + QueryHelper.GetPostDelimeter(this.type) + " = " + QueryHelper.GetParameterName(columnName, this.provider) + ",");
		}

		public void Add(string columnName, object columnValue)
		{
			sb.Append(" " + QueryHelper.GetPreDelimeter(this.type) + columnName + QueryHelper.GetPostDelimeter(this.type) + " = " + QueryHelper.DressUp(columnValue, this.type) + ",");
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
			return "UPDATE " + this._table + " SET " + this.GetColumns() + this.GetCriteria();
		}

		protected string GetColumns()
		{
			return StringUtil.RemoveFinalComma(this.sb.ToString());
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