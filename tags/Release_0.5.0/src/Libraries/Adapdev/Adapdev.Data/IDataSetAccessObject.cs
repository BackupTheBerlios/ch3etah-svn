namespace Adapdev.Data
{
	using System.Data;

	/// <summary>
	/// Provides DataSet-centric data access
	/// </summary>
	public interface IDataSetAccessObject
	{
		/// <summary>
		/// Persists a DataSet to the underlying datastore
		/// </summary>
		/// <param name="ds">The DataSet to persist</param>
		/// <returns></returns>
		int SaveDS(DataSet ds);
		/// <summary>
		/// Returns a DataSet populated with the record that matches the passed in id
		/// </summary>
		/// <param name="id">The id of the record to retrieve</param>
		/// <returns></returns>
		DataSet SelectOneDS(object id);
		/// <summary>
		/// Returns a DataSet populated with all records from the underlying datastore
		/// </summary>
		/// <returns></returns>
		DataSet SelectAllDS();
	}
}