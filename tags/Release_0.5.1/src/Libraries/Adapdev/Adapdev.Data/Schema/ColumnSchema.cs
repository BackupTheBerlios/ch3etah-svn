namespace Adapdev.Data.Schema
{
	using System;
	using System.Collections;
	using Adapdev.Text;

	/// <summary>
	/// Represents the schema for a column in a table
	/// </summary>
	/// 
	[Serializable]
	public class ColumnSchema
	{
		private string _name = String.Empty;
		private string _alias = String.Empty;
		private int _length;
		private string _dataType = String.Empty;
		private int _dataTypeId;
		private string _netType = String.Empty;
		private bool _isForeignKey;
		private bool _isPrimaryKey;
		private bool _isAutoIncrement;
		private bool _isUnique;
		private bool _allowNulls;
		private bool _active = true;
		private int _ordinal;
		private bool _isReadOnly;
		private string _defaultValue = String.Empty;
		private string _defaultTestValue = String.Empty;
		private ArrayList _fkReferences = new ArrayList();
		private TableSchema _parent = null;

		/// <summary>
		/// Specifies whether the column is readonly
		/// </summary>
		public bool IsReadOnly
		{
			get { return this._isReadOnly; }
			set { this._isReadOnly = value; }
		}

		/// <summary>
		/// Specifies whether the column is active.  Used primarily
		/// for on/off state in GUIs.  It allows for a column to still
		/// be part of a TableSchema, but ignored for various reasons
		/// </summary>
		public bool IsActive
		{
			get { return this._active; }
			set { this._active = value; }
		}

		/// <summary>
		/// The ordinal number of the column
		/// </summary>
		public int Ordinal
		{
			get { return this._ordinal; }
			set { this._ordinal = value; }
		}

		/// <summary>
		/// Specifies whether the column allows nulls
		/// </summary>
		public bool AllowNulls
		{
			get { return this._allowNulls; }
			set { this._allowNulls = value; }
		}

		/// <summary>
		/// Specifies whether the column is unique
		/// </summary>
		public bool IsUnique
		{
			get { return this._isUnique; }
			set { this._isUnique = value; }
		}

		/// <summary>
		/// Specifies whether the column is an autoincrement column
		/// </summary>
		public bool IsAutoIncrement
		{
			get { return this._isAutoIncrement; }
			set { this._isAutoIncrement = value; }
		}

		/// <summary>
		/// Specifies whether the column is a primary key
		/// </summary>
		public bool IsPrimaryKey
		{
			get { return this._isPrimaryKey; }
			set { this._isPrimaryKey = value; }
		}

		/// <summary>
		/// Specifies whether the column is a foreign key
		/// </summary>
		public bool IsForeignKey
		{
			get { return this._isForeignKey; }
			set { this._isForeignKey = value; }
		}

		/// <summary>
		/// Specifies whether the column is a key (primary or foreign)
		/// </summary>
		public bool IsKey
		{
			get 
			{ 
				if(this.IsPrimaryKey || this.IsForeignKey) return true;
				else return false;
			}
		}

		/// <summary>
		/// The .NET Object equivalent for the column
		/// </summary>
		public string NetType
		{
			get { return this._netType; }
			set { this._netType = value; }
		}

		/// <summary>
		/// The database type
		/// </summary>
		public string DataType
		{
			get { return this._dataType; }
			set { this._dataType = value; }
		}

		/// <summary>
		/// The column length
		/// </summary>
		public int Length
		{
			get { return this._length; }
			set { this._length = value; }
		}

		/// <summary>
		/// The column name
		/// </summary>
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		/// <summary>
		/// The column alias
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

		/// <summary>
		/// The numeric id (assigned by ADO.NET) for the specified data type
		/// </summary>
		public int DataTypeId
		{
			get { return this._dataTypeId; }
			set { this._dataTypeId = value; }
		}

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value></value>
		public string DefaultValue
		{
			get { return _defaultValue; }
			set { _defaultValue = value; }
		}

		/// <summary>
		/// Gets or sets the default test value.
		/// </summary>
		/// <value></value>
		public string DefaultTestValue
		{
			get { return _defaultTestValue; }
			set { _defaultTestValue = value; }
		}

		/// <summary>
		/// Gets or sets the foreign key tables.
		/// </summary>
		/// <value></value>
		public ArrayList ForeignKeyTables
		{
			get{ return this._fkReferences;}
			set{ this._fkReferences = value;}
		}

		/// <summary>
		/// Returns a proper case rendition of the column name, with 
		/// all spaces removed
		/// </summary>
		/// <returns></returns>
		public string ProperName
		{
			get{return StringUtil.ToTrimmedProperCase(this.Name);}
		}

		/// <summary>
		/// Returns a .NET field name for the specified column, in the format of
		/// _columnName
		/// </summary>
		/// <returns></returns>
		public string MemberName
		{
			get{return "_" + this.ProperName;}
		}

//		public string GetMySqlDbTypeName()
//		{
//			MySql.Data.MySqlClient.MySqlDbType dbType = (MySql.Data.MySqlClient.MySqlDbType)DataTypeId;
//			return dbType.ToString();
//		}

		public override string ToString()
		{
			return StringUtil.ToString(this);
		}

	}
}