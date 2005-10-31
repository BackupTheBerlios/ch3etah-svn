namespace Adapdev.Data.Sql
{
	using System;

	/// <summary>
	/// Represents a query
	/// </summary>
	public interface IQuery
	{
		/// <summary>
		/// Sets the criteria to use for the query
		/// </summary>
		/// <param name="c"></param>
		void SetCriteria(ICriteria c);
		/// <summary>
		/// Specifies the table to use for the query
		/// </summary>
		/// <param name="tableName"></param>
		void SetTable(string tableName);
		/// <summary>
		/// Returns a datastore specific ICriteria implementation
		/// </summary>
		/// <returns></returns>
		ICriteria CreateCriteria();
		/// <summary>
		/// Returns the text form of the query
		/// </summary>
		/// <returns></returns>
		string GetText();
		/// <summary>
		/// The DbProviderType for this query.  Necessary to determine how to
		/// represent dates, parameters, etc.
		/// </summary>
		DbProviderType DbProviderType { get; set; }
	}
}