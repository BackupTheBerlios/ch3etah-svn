namespace Adapdev.Data.Schema
{
	/// <summary>
	/// Summary description for ParameterSchema.
	/// </summary>
	public class ParameterSchema
	{
		public string Name;
		public int Ordinal;
		public int DataTypeId;
		public bool HasDefault;
		public object Default;
		public bool AllowNulls;
		public int MaxLength;
		public string Description;
		public string DataTypeName;
	}
}