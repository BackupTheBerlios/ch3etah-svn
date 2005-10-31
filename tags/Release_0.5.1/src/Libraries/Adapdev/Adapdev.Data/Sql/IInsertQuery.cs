namespace Adapdev.Data.Sql
{
	/// <summary>
	/// Summary description for IInsertQuery.
	/// </summary>
	public interface IInsertQuery : INonSelectQuery
	{
		/// <summary>
		/// Adds the columnName to the insert query
		/// </summary>
		/// <param name="columnName"></param>
		/// <remarks>Since no value is passed in, the datastore specific parameter representation
		/// will be added</remarks>
		void Add(string columnName);
		/// <summary>
		/// Adds the column name and value to the insert query
		/// </summary>
		/// <param name="columnName"></param>
		/// <param name="columnValue"></param>
		void Add(string columnName, object columnValue);
	}
}