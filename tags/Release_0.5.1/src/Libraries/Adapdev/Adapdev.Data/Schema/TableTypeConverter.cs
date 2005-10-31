namespace Adapdev.Data.Schema
{
	using System;

	/// <summary>
	/// Converts the string representation of a table type to a TableType enum
	/// </summary>
	public class TableTypeConverter
	{
		private TableTypeConverter()
		{
		}

		/// <summary>
		/// Converts the string representation of a table type to a TableType enum
		/// </summary>
		/// <param name="tableType">The string representation to convert</param>
		/// <returns></returns>
		public static TableType Convert(string tableType)
		{
			switch (tableType.ToUpper(new System.Globalization.CultureInfo("en-US")))
			{
				case "SYSTEM TABLE":
				case "ACCESS TABLE":
					return TableType.SYSTEM_TABLE;
				case "SYSTEM VIEW":
					return TableType.SYSTEM_VIEW;
				case "TABLE":
					return TableType.TABLE;
				case "VIEW":
					return TableType.VIEW;
				case "SYNONYM":
					return TableType.SYNONYM;
				default:
					throw new Exception("TableType " + tableType + " is not supported.");
			}
		}
	}
}