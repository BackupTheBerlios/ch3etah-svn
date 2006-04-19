namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for ISelectQuery.
	/// </summary>
	public interface ISelectQuery : IQuery
	{
		/// <summary>
		/// Adds the specified column
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		void Add(string columnName);
		/// <summary>
		/// Adds the specified table.column
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="columnName">Name of the column.</param>
		void Add(string tableName, string columnName);
		/// <summary>
		/// Adds the column alias.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="alias">Alias.</param>
		void AddColumnAlias(string tableName, string columnName, string alias);
		/// <summary>
		/// Adds the column alias.
		/// </summary>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="alias">Alias.</param>
		void AddColumnAlias(string columnName, string alias);
		/// <summary>
		/// Creates a SELECT * FROM statement, so that individual column names
		/// don't have to be added
		/// </summary>
		void AddAll();
		/// <summary>
		/// Adds a COUNT([columnName]) statement in the datastore specific format
		/// </summary>
		/// <param name="columnName"></param>
		void AddCount(string columnName);
		/// <summary>
		/// Adds a COUNT(*) statement in the datastore specific format
		/// </summary>
		void AddCountAll();
		/// <summary>
		/// Adds a ORDER BY [columnName] statement in the datastore specific format
		/// </summary>
		/// <param name="columnName"></param>
		void AddOrderBy(string columnName);
		/// <summary>
		/// Adds a ORDER BY [column1], [column2]... statement in the datastore specific format
		/// </summary>
		/// <param name="columns"></param>
		void AddOrderBy(params string[] columns);
		/// <summary>
		/// Adds a GROUP BY [columnName] statement in the datastore specific format
		/// </summary>
		/// <param name="columnName"></param>
		void AddGroupBy(string columnName);
		/// <summary>
		/// Adds a GROUP BY [column1], [column2]... statement in the datastore specific format
		/// </summary>
		/// <param name="columns"></param>
		void AddGroupBy(params string[] columns);
		/// <summary>
		/// Adds a SELECT ... FROM [table] [JoinType] [secondTable] ON [firstTableColumn] = [secondTableColumn]
		/// </summary>
		/// <param name="secondTable">The name of the second table to join on</param>
		/// <param name="firstTableColumn">The name of the first table's join column</param>
		/// <param name="secondTableColumn">The name of the second table's join column</param>
		/// <param name="type">The join type</param>
		void AddJoin(string secondTable, string firstTableColumn, string secondTableColumn, JoinType type);
		/// <summary>
		/// Set's the maximum number of records to retrieve
		/// </summary>
		/// <param name="maxRecords"></param>
		void SetLimit(int maxRecords);
		OrderBy OrderBy { get; set; }
	}

	public enum OrderBy
	{
		ASCENDING,
		DESCENDING
	}

	public enum JoinType
	{
		LEFT,
		RIGHT,
		INNER
	}
}