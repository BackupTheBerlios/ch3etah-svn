// Original Copyright (c) 2004, Pedro Silva  (darthpedro99@hotmail.com)
// Taken from the following article: http://www.devhood.com/tutorials/tutorial_details.aspx?tutorial_id=703
namespace Adapdev.Data
{
	using System.Data;
	using System.Diagnostics;
	using System.Text;

	/// <summary>
	/// Provides a string print out of DataSet contents
	/// </summary>
	public class DataSetDebugger
	{
		private DataSetDebugger(){}

		/// <summary>
		/// Prints out the DataSet contents to a string
		/// </summary>
		/// <param name="dataSet">Data set.</param>
		/// <returns></returns>
		public static string ToString(DataSet dataSet)
		{
			// create a StringBuilder to use for string creation.
			StringBuilder result = new StringBuilder();

			// loop through every table in the dataset.
			foreach (DataTable table in dataSet.Tables)
			{
				result.AppendFormat("{0}\r\n", table.TableName);

				// keep a local cache of the Rows and Columns lists, 
				// so that they're not always requested again.
				DataRowCollection tableRows = table.Rows;
				DataColumnCollection tableColumns = table.Columns;

				// loop through each row in a specific DataTable.
				for (int ctrRow = 0; ctrRow < tableRows.Count; ctrRow++)
				{
					DataRow row = tableRows[ctrRow] as DataRow;
					result.AppendFormat("Row #{0}-\r\n", ctrRow + 1);
					object[] rowItems = row.ItemArray;

					// loop through each column in a specific DataTable.
					for (int ctrColumn = 0; ctrColumn < tableColumns.Count; ctrColumn++)
					{
						DataColumn column = tableColumns[ctrColumn] as DataColumn;

						// create the output based on column and RowItem.
						result.AppendFormat("\t{0}: {1}\r\n", column.ColumnName,
						                    rowItems[ctrColumn].ToString());
					}
				}
				result.Append("\r\n");
			}
			return result.ToString();
		}

		/// <summary>
		/// Writes the DataSet to Debug
		/// </summary>
		/// <param name="dataSet">Data set.</param>
		/// <param name="header">Header.</param>
		public static void DebugWriteLine(DataSet dataSet, string header)
		{
			string output = string.Format("{0}\r\n{1}", header,
			                              DataSetDebugger.ToString(dataSet));
			Debug.WriteLine(output);
		}

		/// <summary>
		/// Writes the DataSet to Debug
		/// </summary>
		/// <param name="dataSet">Data set.</param>
		public static void DebugWriteLine(DataSet dataSet)
		{
			Debug.WriteLine(DataSetDebugger.ToString(dataSet));
		}

		/// <summary>
		/// Writes the DataSet to Trace
		/// </summary>
		/// <param name="dataSet">Data set.</param>
		/// <param name="header">Header.</param>
		public static void TraceWriteLine(DataSet dataSet, string header)
		{
			string output = string.Format("{0}\r\n{1}", header,
			                              DataSetDebugger.ToString(dataSet));
			Trace.WriteLine(output);
		}

		/// <summary>
		/// Writes the DataSet to Trace
		/// </summary>
		/// <param name="dataSet">Data set.</param>
		public static void TraceWriteLine(DataSet dataSet)
		{
			Trace.WriteLine(DataSetDebugger.ToString(dataSet));
		}
	}
}