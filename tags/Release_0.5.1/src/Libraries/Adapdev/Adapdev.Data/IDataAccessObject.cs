namespace Adapdev.Data
{
	using System.Collections;

	/// <summary>
	/// Provides access to an underlying datastore
	/// </summary>
	public interface IDataAccessObject
	{
		/// <summary>
		/// Saves the specified object to the underlying datastore
		/// </summary>
		/// <param name="o">The object to persist</param>
		void Save(object o);
		/// <summary>
		/// Deletes the specified object from the underlying datastore, using
		/// the passed in id
		/// </summary>
		/// <param name="id">The id of the object to delete</param>
		void Delete(object id);
		/// <summary>
		/// Updates the specified object in the underlying datastore
		/// </summary>
		/// <param name="o">The object to update</param>
		void Update(object o);
		/// <summary>
		/// Selects an IList of objects that match the specified string criteria
		/// </summary>
		/// <param name="s">The criteria to use (for example, an XPath statement, SQL query, etc.)</param>
		/// <returns></returns>
		IList Select(string s);
		/// <summary>
		/// Retrieves all objects in the underlying datastore
		/// </summary>
		/// <returns></returns>
		IList SelectAll();
		/// <summary>
		/// Retrieves an object with the matching id
		/// </summary>
		/// <param name="id">The id of the object to retrieve</param>
		/// <returns></returns>
		object SelectOne(object id);
		/// <summary>
		/// Gets the number of persisted objects in the underlying datastore
		/// </summary>
		/// <returns></returns>
		int GetCount();
	}
}