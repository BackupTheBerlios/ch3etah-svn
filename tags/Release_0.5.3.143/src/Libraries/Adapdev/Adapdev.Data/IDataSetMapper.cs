namespace Adapdev.Data
{
	using System.Collections;
	using System.Data;

	/// <summary>
	/// Maps DataSet information to entity objects
	/// </summary>
	public interface IDataSetMapper
	{
		/// <summary>
		/// Maps the DataSet information to entity objects
		/// </summary>
		/// <param name="ds">The DataSet to map</param>
		/// <returns>An IList of mapped entity objects</returns>
		IList MapObjects(DataSet ds);
	}
}