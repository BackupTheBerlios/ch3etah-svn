using System;

namespace Adapdev.Data.Schema
{
	using Adapdev.Text;

	/// <summary>
	/// Summary description for ForeignKeyAssociation.
	/// </summary>
	public class ForeignKeyAssociation
	{
		private ColumnSchema _foreignColumn = null;
		private TableSchema _foreignTable = null;
		private ColumnSchema _columnSchema = null;
		
		public ColumnSchema ForeignColumn
		{
			get { return _foreignColumn; }
			set { _foreignColumn = value; }
		}

		public ColumnSchema Column
		{
			get { return _columnSchema; }
			set { _columnSchema = value; }
		}

		public TableSchema ForeignTable
		{
			get { return _foreignTable; }
			set { _foreignTable = value; }
		}

		public ForeignKeyAssociation(ColumnSchema columnSchema, ColumnSchema foreignColumn, TableSchema foreignTable)
		{
			this._columnSchema = columnSchema;
			this._foreignColumn = foreignColumn;
			this._foreignTable = foreignTable;
		}

		public override string ToString()
		{
			return StringUtil.ToString(this);
		}

	}
}
