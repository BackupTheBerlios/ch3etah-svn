namespace Adapdev.Data.Schema
{
	using System;
	using System.Collections;
	using Adapdev.Text;

	/// <summary>
	/// Represents the schema for a database
	/// </summary>
	/// 
	[Serializable]
	public class DatabaseSchema
	{
		protected TableSchemaDictionary tables = new TableSchemaDictionary();
		protected string name = String.Empty;
		protected string connectionString = String.Empty;
		protected DbType databaseType = DbType.SQLSERVER;
		protected DbProviderType databaseProviderType = DbProviderType.SQLSERVER;

		/// <summary>
		/// Constructor
		/// </summary>
		public DatabaseSchema()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The name of the database</param>
		public DatabaseSchema(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// The database name
		/// </summary>
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		/// <summary>
		/// The database connectionstring
		/// </summary>
		public string ConnectionString
		{
			get { return this.connectionString; }
			set { this.connectionString = value; }
		}

		/// <summary>
		/// The database type
		/// </summary>
		public DbType DatabaseType
		{
			get { return this.databaseType; }
			set { this.databaseType = value; }
		}

		/// <summary>
		/// The database provider type
		/// </summary>
		public DbProviderType DatabaseProviderType
		{
			get { return this.databaseProviderType; }
			set { this.databaseProviderType = value; }
		}

		/// <summary>
		/// Adds the specified table schema to the database
		/// </summary>
		/// <param name="table">The TableSchema to add</param>
		public void AddTable(TableSchema table)
		{
			tables[table.Name] = table;
		}

		/// <summary>
		/// Gets the specified table schema
		/// </summary>
		/// <param name="name">The name of the TableSchema to retrieve</param>
		/// <returns>TableSchema</returns>
		public TableSchema GetTable(string name)
		{
			return tables[name];
		}

		/// <summary>
		/// Indexer to retrieve a TableSchema
		/// </summary>
		public TableSchema this[String key]
		{
			get { return (tables[key]); }
			set { tables[key] = value; }
		}

		/// <summary>
		/// Returns a dictionary of the database's TableSchemas
		/// </summary>
		/// <returns></returns>
		public TableSchemaDictionary Tables
		{
			get{return this.tables;}
			set{this.tables = value;}
		}

		/// <summary>
		/// Returnes a SortedList of TableSchemas, sorted by the table names
		/// </summary>
		/// <returns>SortedList</returns>
		public SortedList SortedTables
		{
			get
			{
				SortedList sl = new SortedList();
				foreach (TableSchema t in this.tables.Values)
				{
					sl.Add(t.Name, t);
				}
				return sl;
			}
		}

		public override string ToString()
		{
			return Environment.NewLine + StringUtil.ToString(this) + Environment.NewLine + this.Tables.ToString();
		}

	}
}