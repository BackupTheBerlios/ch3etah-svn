using System;
using System.Data;
using System.Text;

namespace Adapdev.Data
{
	/// <summary>
	/// Summary description for DataReaderDebugger.
	/// </summary>
	public class DataReaderDebugger
	{
		private IDataReader _reader = null;

		public DataReaderDebugger(IDataReader reader)
		{
			this._reader = reader;
		}

		public string Text
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				do
				{
					int i = _reader.FieldCount;
					for(int j = 0; j < i; j++)
					{
						sb.Append(this._reader.GetName(j) + "|");
					}
					sb.Append(Environment.NewLine);


					while(this._reader.Read())
					{
						for(int j = 0; j < i; j++)
						{
							sb.Append(this._reader.GetValue(j).ToString() + "|");
						}
						sb.Append(Environment.NewLine);
					}
					sb.Append(Environment.NewLine);
				}
				while(this._reader.NextResult());

				return sb.ToString();
			}
		}
	}
}
