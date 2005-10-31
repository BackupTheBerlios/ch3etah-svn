namespace Adapdev.Data
{
	using System.Data;

	/// <summary>
	/// Provides DataSet-centric operations
	/// </summary>
	public interface IDbDataSetAccessObject : IDataSetAccessObject
	{
		/// <summary>
		/// Saves a DataSet to the underlying datastore
		/// </summary>
		/// <param name="ds">The dataset to save</param>
		/// <param name="conn">The connection to use</param>
		/// <returns>Number of records affected</returns>
		int SaveDS(DataSet ds, IDbConnection conn);
		/// <summary>
		/// Returns a DataSet, most likely with one DataTable DataRow.  It fills
		/// the DataSet by grabbing a record with a matching id
		/// </summary>
		/// <param name="id">The id of the record to retrieve</param>
		/// <param name="conn">The connection to use</param>
		/// <returns></returns>
		DataSet SelectOneDS(object id, IDbConnection conn);
		/// <summary>
		/// Returns a DataSet populated with all records
		/// </summary>
		/// <param name="conn">The connection to use</param>
		/// <returns></returns>
		DataSet SelectAllDS(IDbConnection conn);
		/// <summary>
		/// Returns a DataSet with records that match the command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <returns></returns>
		DataSet SelectDS(IDbCommand cmd);
		/// <summary>
		/// Returns a DataSet with records that match the command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <param name="conn">The connection to use</param>
		/// <returns></returns>
		DataSet SelectDS(IDbCommand cmd, IDbConnection conn);
		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		void ExecuteNonQuery(IDbCommand cmd);
		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <param name="conn">The connection to use</param>
		void ExecuteNonQuery(IDbCommand cmd, IDbConnection conn);
	}
}