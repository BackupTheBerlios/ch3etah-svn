namespace Adapdev.Data.Schema
{
	using System;
	using System.Collections;
	using Adapdev.Text;

	/// <summary>
	/// Represents the schema for a database table
	/// </summary>
	[Serializable]
	public class TableSchema
	{
		protected TableType tableType = TableType.TABLE;
		protected bool _active = true;
		protected string _name = String.Empty;
		protected string _alias = String.Empty;
		protected ColumnSchemaDictionary columns = new ColumnSchemaDictionary();
		protected DatabaseSchema parent = null;

		/// <summary>
		/// Property TableType (TableType)
		/// </summary>
		public TableType TableType
		{
			get
			{
				return this.tableType;
			}
			set
			{
				this.tableType = value;
			}
		}

		/// <summary>
		/// Adds the specified ColumnSchema
		/// </summary>
		/// <param name="c"></param>
		public void AddColumn(ColumnSchema c)
		{
			this.columns[c.Name] = c;
		}

		/// <summary>
		/// Removes the specified ColumnSchema
		/// </summary>
		/// <param name="name">The name of the ColumnSchema to remove</param>
		public void RemoveColumn(string name)
		{
			this.columns.Remove(name);
		}

		/// <summary>
		/// Retrieves the specified ColumnSchema
		/// </summary>
		/// <param name="name">The name of the ColumnSchema to retrieve</param>
		/// <returns></returns>
		public ColumnSchema GetColumn(string name)
		{
			return this.columns[name];
		}

		/// <summary>
		/// Indexer to retrieve a ColumnSchema by name
		/// </summary>
		public ColumnSchema this[String key]
		{
			get { return (columns[key]); }
			set { columns[key] = value; }
		}

		/// <summary>
		/// Returns a collection of ColumnSchemas
		/// </summary>
		/// <returns></returns>
		public ColumnSchemaDictionary Columns
		{
			get{return this.columns;}
			set{this.columns = value;}
		}

		/// <summary>
		/// Returns a collection of ColumnSchemas that are primary keys
		/// </summary>
		/// <returns></returns>
		public ColumnSchemaDictionary PrimaryKeys
		{
			get
			{
				ColumnSchemaDictionary pks = new ColumnSchemaDictionary();
				foreach(ColumnSchema c in this.columns.Values)
				{
					if(c.IsPrimaryKey)pks[c.Name] = c;
				}
				return pks;
			}
		}

		/// <summary>
		/// Returns a collection of ColumnSchemas that are foreign keys
		/// </summary>
		/// <returns></returns>
		public ColumnSchemaDictionary ForeignKeys
		{
			get
			{
				ColumnSchemaDictionary fks = new ColumnSchemaDictionary();
				foreach(ColumnSchema c in this.columns.Values)
				{
					if(c.IsForeignKey)fks[c.Name] = c;
				}
				return fks;
			}
		}

		/// <summary>
		/// Gets the number of primary keys
		/// </summary>
		/// <returns></returns>
		public int PrimaryKeyCount
		{
			get
			{
				   int i = 0;
				   foreach (ColumnSchema c in columns.Values)
				   {
					   if(c.IsPrimaryKey)i++;
				   }
				   return i;
			   }
		}

		/// <summary>
		/// Gets the number of foreign keys
		/// </summary>
		/// <returns></returns>
		public int ForeignKeyCount
		{
			get
			{
				int i = 0;
				foreach (ColumnSchema c in columns.Values)
				{
					if(c.IsForeignKey)i++;
				}
				return i;
			}
		}

		/// <summary>
		/// Gets the specified primary key Column Schema by index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		/// <remarks>
		/// If a database table contains two primary keys, then index 0 would retrieve
		/// the first, and index 1 would be the second.  The index is in relation
		/// to the number of primary keys, not the total number of columns
		/// </remarks>
		public ColumnSchema GetPrimaryKey(int index)
		{
			ArrayList al = new ArrayList(this.PrimaryKeys.Values);
			return ((ColumnSchema) al[index]);
		}

		/// <summary>
		/// Gets the specified primary key Column Schema by index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		/// <remarks>
		/// If a database table contains two foreign keys, then index 0 would retrieve
		/// the first, and index 1 would be the second.  The index is in relation
		/// to the number of foreign keys, not the total number of columns
		/// </remarks>
		public ColumnSchema GetForeignKey(int index)
		{
			ArrayList al = new ArrayList(this.ForeignKeys.Values);
			return ((ColumnSchema) al[index]);
		}

		/// <summary>
		/// Returns the list of ColumnSchemas, sorted by their ordinal representation
		/// </summary>
		/// <returns></returns>
		public SortedList OrdinalColumns
		{
			get
			{
				SortedList sl = new SortedList();
				foreach (ColumnSchema c in this.columns.Values)
				{
					sl[c.Ordinal] = c;
				}
				return sl;
			}
		}

		/// <summary>
		/// Returns the list of ColumnSchemas, sorted by their names
		/// </summary>
		/// <returns></returns>
		public SortedList SortedColumns
		{
			get
			{
				SortedList sl = new SortedList();
				foreach (ColumnSchema c in this.columns.Values)
				{
					sl.Add(c.Name, c);
				}
				return sl;
			}
		}

		/// <summary>
		/// Returns the table name in proper case, with all spaces removed
		/// </summary>
		/// <returns></returns>
		public string ProperName
		{
			get{return StringUtil.ToTrimmedProperCase(this.Name);}
		}

		/// <summary>
		/// Specifies whether the table is active.  Used primarily
		/// for on/off state in GUIs.  It allows for a table to still
		/// be part of a DatabaseSchema, but ignored for various reasons
		/// </summary>
		public bool IsActive
		{
			get { return this._active; }
			set { this._active = value; }
		}

		/// <summary>
		/// The table name
		/// </summary>
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		/// <summary>
		/// The table alias
		/// </summary>
		public string Alias
		{
			get
			{
				if(this._alias.Length == 0 || this._alias == this._name) return this.ProperName;
				else return this._alias;
			}
			set { this._alias = value; }
		}

		public bool HasForeignKeys
		{
			get 
			{ 
				if(this.ForeignKeyCount > 0) return true;
				else return false;
			}
		}

		public bool HasPrimaryKeys
		{
			get 
			{ 
				if(this.PrimaryKeyCount > 0) return true;
				else return false;
			}
		}

		public override string ToString()
		{
			return Environment.NewLine + StringUtil.ToString(this) + Environment.NewLine + this.Columns.ToString();
		}

	}


}