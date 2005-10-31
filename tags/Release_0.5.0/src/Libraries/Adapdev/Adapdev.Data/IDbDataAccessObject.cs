namespace Adapdev.Data
{
	using System.Collections;
	using System.Data;
	using Adapdev.Data.Sql;

	/// <summary>
	/// Summary description for IDbDataAccessObject.
	/// </summary>
	public interface IDbDataAccessObject : IDataAccessObject
	{
		void Save(object o, IDbConnection connection);
		void Delete(object id, IDbConnection connection);
		void Update(object o, IDbConnection connection);
		IList SelectAll(IDbConnection connection);
		IList SelectAllWithLimit(int maxRecords);
		IList SelectAllWithLimit(int maxRecords, IDbConnection connection);
		IList SelectAllWithLimit(int maxRecords, OrderBy order, params string[] orderColumns);
		IList SelectAllWithLimit(int maxRecords, OrderBy order, IDbConnection connection, params string[] orderColumns);
		IList Select(IDbCommand cmd);
		IList Select(IDbCommand cmd, IDbConnection connection);
		IList Select(ISelectQuery query);
		IList Select(ISelectQuery query, IDbConnection connection);
		IList Select(string command, IDbConnection connection);
		object SelectOne(object id, IDbConnection connection);
		void ExecuteNonQuery(IDbCommand cmd);
		void ExecuteNonQuery(IDbCommand cmd, IDbConnection connection);
		void ExecuteNonQuery(INonSelectQuery query);
		void ExecuteNonQuery(INonSelectQuery query, IDbConnection connection);
		void ExecuteNonQuery(string command);
		void ExecuteNonQuery(string command, IDbConnection connection);
		ISelectQuery CreateSelectQuery();
		IDeleteQuery CreateDeleteQuery();
		IInsertQuery CreateInsertQuery();
		IUpdateQuery CreateUpdateQuery();

	}
}