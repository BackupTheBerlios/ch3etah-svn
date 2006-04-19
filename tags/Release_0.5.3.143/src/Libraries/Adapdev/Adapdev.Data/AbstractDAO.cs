namespace Adapdev.Data
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Data.Common;
	using System.Data.OleDb;
	using System.Data.SqlClient;
	using Adapdev.Data.Sql;

	/// <summary>
	/// AbstractDAO provides the required base functionality to create a full-featured
	/// Data Access Object.
	/// </summary>
	/// 
	public abstract class AbstractDAO : IDbDataAccessObject, IDbDataSetAccessObject, IDataReaderMapper
	{
		private DbProviderType provider = DbProviderType.SQLSERVER;
		private DbType db = DbType.SQLSERVER;
		private string connectionString = "";
		private string table = "";

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="providerType">The specified provider type</param>
		/// <param name="databaseType">The specified database type</param>
		/// <param name="tableName">The table being accessed</param>
		/// <param name="connectionString">The specified connection string</param>
		public AbstractDAO(DbProviderType providerType, DbType databaseType, string tableName, string connectionString)
		{
			this.provider = providerType;
			this.db = databaseType;
			this.table = tableName;
			this.connectionString = connectionString;
		}

		#endregion

		#region IDataAccessObject Members

		/// <summary>
		/// Saves the specified object to the datastore
		/// </summary>
		/// <param name="o"></param>
		/// 
		public void Save(object o)
		{
			using(IDbConnection connection = this.CreateConnection())
			{
				connection.Open();
				this.ExecuteNonQuery(this.CreateInsertCommand(o), connection);
				this.CustomSave(o, connection);
			}
		}

		/// <summary>
		/// Saves the specified object to the datastore, using the specified open connection
		/// </summary>
		/// <param name="o">The object to save</param>
		/// <param name="conn">The open connection to use</param>
		/// <remarks>The IDbConnection must already be open</remarks>
		public void Save(object o, IDbConnection conn)
		{
			this.ExecuteNonQuery(this.CreateInsertCommand(o), conn);
			this.CustomSave(o, conn);
		}

		/// <summary>
		/// Saves the specified object to the datastore, using the specified open connection
		/// </summary>
		/// <param name="o">The object to save</param>
		/// <param name="conn">The open connection to use</param>
		/// <param name="transaction">The transaction to execute under</param>
		/// <remarks>The IDbConnection must already be open</remarks>
		public void Save(object o, IDbConnection conn, IDbTransaction transaction)
		{
			this.ExecuteNonQuery(this.CreateInsertCommand(o), conn, transaction);
			this.CustomSave(o, conn, transaction);
		}

		/// <summary>
		/// Deletes the specified object by its id
		/// </summary>
		/// <param name="id">The id for the object to delete</param>
		public void Delete(object id)
		{
			this.ExecuteNonQuery(this.CreateDeleteOneCommand(id));
		}

		/// <summary>
		/// Deletes the specified object by its id
		/// </summary>
		/// <param name="id">The id for the object to delete</param>
		/// <param name="conn">The open connection to use</param>
		/// <remarks>The IDbConnection must already be open</remarks>
		public void Delete(object id, IDbConnection conn)
		{
			this.ExecuteNonQuery(this.CreateDeleteOneCommand(id), conn);
		}

		/// <summary>
		/// Deletes the specified object by its id
		/// </summary>
		/// <param name="id">The id for the object to delete</param>
		/// <param name="conn">The open connection to use</param>
		/// <param name="transaction">The transaction to execute under</param>
		/// <remarks>The IDbConnection must already be open</remarks>
		public void Delete(object id, IDbConnection conn, IDbTransaction transaction)
		{
			this.ExecuteNonQuery(this.CreateDeleteOneCommand(id), conn, transaction);
		}

		/// <summary>
		/// Updates the underlying datastore
		/// </summary>
		/// <param name="o">The object to use for the update</param>
		public void Update(object o)
		{
			this.ExecuteNonQuery(this.CreateUpdateCommand(o));
		}

		/// <summary>
		/// Updates the underlying datastore
		/// </summary>
		/// <param name="o">The object to use for the update</param>
		/// <param name="conn">The open connection to use</param>
		/// <remarks>The IDbConnection must already be open</remarks>
		public void Update(object o, IDbConnection conn)
		{
			this.ExecuteNonQuery(this.CreateUpdateCommand(o), conn);
		}

		/// <summary>
		/// Updates the underlying datastore
		/// </summary>
		/// <param name="o">The object to use for the update</param>
		/// <param name="conn">The open connection to use</param>
		/// <param name="transaction">The transaction to execute under</param>
		/// <remarks>The IDbConnection must already be open</remarks>
		public void Update(object o, IDbConnection conn, IDbTransaction transaction)
		{
			this.ExecuteNonQuery(this.CreateUpdateCommand(o), conn, transaction);
		}


		/// <summary>
		/// Selects all records in the underlying datastore
		/// </summary>
		/// <returns></returns>
		public IList SelectAll()
		{
			IList c;
			using (IDbConnection conn = this.CreateConnection())
			{
				conn.Open();
				c = this.SelectAll(conn);
			}
			return c;
		}

		/// <summary>
		/// Selects all records in the underlying datastore, using the specified open IDbConnection
		/// </summary>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public IList SelectAll(IDbConnection conn)
		{
			IList c;
			IDataReader dr = DbProviderFactory.CreateDataReader(conn, this.CreateSelectAllCommand(), this.provider);
			c = this.MapObjects(dr);
			dr.Close();
			return c;
		}

		/// <summary>
		/// Selects a set number of records in the underlying datastore
		/// </summary>
		/// <param name="maxRecords">The number of records to return</param>
		/// <returns></returns>
		public IList SelectAllWithLimit(int maxRecords)
		{
			return this.SelectAllWithLimit(maxRecords, OrderBy.ASCENDING);
		}

		/// <summary>
		/// Selects a set number of records in the underlying datastore, 
		/// using the specified open IDbConnection
		/// </summary>
		/// <param name="maxRecords">The number of records to return</param>
		/// <param name="connection">The open IDbConnection to use</param>
		/// <returns></returns>
		public IList SelectAllWithLimit(int maxRecords, IDbConnection connection)
		{
			return this.SelectAllWithLimit(maxRecords, OrderBy.ASCENDING, connection);
		}

		/// <summary>
		/// Selects a set number of records in the underlying datastore
		/// </summary>
		/// <param name="maxRecords">The number of records to return</param>
		/// <param name="order">The order the records should be returned</param>
		/// <param name="orderColumns">The columns to order by</param>
		/// <returns></returns>
		public IList SelectAllWithLimit(int maxRecords, OrderBy order, params string[] orderColumns)
		{
			ISelectQuery query = QueryFactory.CreateSelectQuery(this.db);
			query.SetTable(this.table);
			query.SetLimit(maxRecords);
			query.OrderBy = order;
			query.AddAll();

			foreach (string s in orderColumns)
			{
				query.AddOrderBy(s);
			}

			return this.Select(query);
		}

		/// <summary>
		/// Selects a set number of records in the underlying datastore
		/// </summary>
		/// <param name="maxRecords">The number of records to return</param>
		/// <param name="order">The order the records should be returned</param>
		/// <param name="connection">The open IDbConnection to use</param>
		/// <param name="orderColumns">The columns to order by</param>
		/// <returns></returns>
		public IList SelectAllWithLimit(int maxRecords, OrderBy order, IDbConnection connection, params string[] orderColumns)
		{
			ISelectQuery query = QueryFactory.CreateSelectQuery(this.db);
			query.SetTable(this.table);
			query.SetLimit(maxRecords);
			query.OrderBy = order;
			query.AddAll();

			foreach (string s in orderColumns)
			{
				query.AddOrderBy(s);
			}

			return this.Select(query, connection);
		}

		/// <summary>
		/// Selects records using the specified query
		/// </summary>
		/// <param name="query">The query to use</param>
		/// <returns></returns>
		public IList Select(ISelectQuery query)
		{
			return this.Select(this.CreateCommand(query.GetText()));
		}

		/// <summary>
		/// Selects records using the specified query
		/// </summary>
		/// <param name="query">The query to use</param>
		/// <param name="connection">The open IDbConnection</param>
		/// <returns></returns>
		public IList Select(ISelectQuery query, IDbConnection connection)
		{
			return this.Select(this.CreateCommand(query.GetText()), connection);
		}

		/// <summary>
		/// Selects records using the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <returns></returns>
		public IList Select(IDbCommand cmd)
		{
			IList c;
			using (IDbConnection conn = this.CreateConnection())
			{
				conn.Open();
				c = this.Select(cmd, conn);
			}
			return c;
		}

		/// <summary>
		/// Selects records using the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public IList Select(IDbCommand cmd, IDbConnection conn)
		{
			IList c;
			IDataReader dr = DbProviderFactory.CreateDataReader(conn, cmd, this.provider);
			c = this.MapObjects(dr);
			dr.Close();

			return c;
		}

		/// <summary>
		/// Selects records using the specified command
		/// </summary>
		/// <param name="command">The command to use</param>
		/// <returns></returns>
		public IList Select(string command)
		{
			return this.Select(this.CreateCommand(command));
		}

		/// <summary>
		/// Selects records using the specified command
		/// </summary>
		/// <param name="command">The command to use</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public IList Select(string command, IDbConnection conn)
		{
			return this.Select(this.CreateCommand(command), conn);
		}

		/// <summary>
		/// Selects a specific record, using the passed in id
		/// </summary>
		/// <param name="id">The id for the record to select</param>
		/// <returns></returns>
		public object SelectById(object id)
		{
			return this.SelectOne(id);
		}

		/// <summary>
		/// Selects a specific record, using the passed in id
		/// </summary>
		/// <param name="id">The id for the record to select</param>
		/// <param name="connection">The open IDbConnection to use</param>
		/// <returns></returns>
		public object SelectById(object id, IDbConnection connection)
		{
			return this.SelectOne(id, connection);
		}

		/// <summary>
		/// Selects a specific record, using the passed in id
		/// </summary>
		/// <param name="id">The id for the record to select</param>
		/// <returns></returns>
		/// 
		[Obsolete("Deprecated.  Please use SelectById")]
		public object SelectOne(object id)
		{
			object o;
			using (IDbConnection conn = this.CreateConnection())
			{
				conn.Open();
				o = this.SelectOne(id, conn);
			}
			return o;
		}

		/// <summary>
		/// Selects a specific record, using the passed in id
		/// </summary>
		/// <param name="id">The id for the record to select</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		[Obsolete("Deprecated.  Please use SelectById")]
		public object SelectOne(object id, IDbConnection conn)
		{
			object o = null;
			IDataReader dr = DbProviderFactory.CreateDataReader(conn, this.CreateSelectOneCommand(id), this.provider);
			while (dr.Read())
			{
				o = this.MapObject(dr);
				break;
			}
			dr.Close();

			return o;
		}

		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		public void ExecuteNonQuery(IDbCommand cmd)
		{
			using (IDbConnection conn = this.CreateConnection())
			{
				cmd.Connection = conn;
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <param name="conn">The open IDbConnection to use</param>
		public void ExecuteNonQuery(IDbCommand cmd, IDbConnection conn)
		{
			cmd.Connection = conn;
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="cmd">The command to use</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <param name="transaction">The transaction to execute under</param>
		public void ExecuteNonQuery(IDbCommand cmd, IDbConnection conn, IDbTransaction transaction)
		{
			cmd.Connection = conn;
			cmd.Transaction = transaction;
			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes the specified query
		/// </summary>
		/// <param name="query">The query to use</param>
		public void ExecuteNonQuery(INonSelectQuery query)
		{
			this.ExecuteNonQuery(this.CreateCommand(query.GetText()));
		}

		/// <summary>
		/// Executes the specified query
		/// </summary>
		/// <param name="query">The query to use</param>
		/// <param name="connection">The open IDbConnection to use</param>
		public void ExecuteNonQuery(INonSelectQuery query, IDbConnection connection)
		{
			this.ExecuteNonQuery(this.CreateCommand(query.GetText()), connection);
		}

		/// <summary>
		/// Executse the specified command
		/// </summary>
		/// <param name="command">The command to use</param>
		public void ExecuteNonQuery(string command)
		{
			this.ExecuteNonQuery(this.CreateCommand(command));
		}

		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="command">The command to use</param>
		/// <param name="connection">The open IDbConnection to use</param>
		public void ExecuteNonQuery(string command, IDbConnection connection)
		{
			this.ExecuteNonQuery(this.CreateCommand(command), connection);
		}

		/// <summary>
		/// Executes the specified command
		/// </summary>
		/// <param name="command">The command to use</param>
		/// <param name="connection">The open IDbConnection to use</param>
		/// <param name="transaction">The transaction to execute under</param>
		public void ExecuteNonQuery(string command, IDbConnection connection, IDbTransaction transaction)
		{
			this.ExecuteNonQuery(this.CreateCommand(command), connection, transaction);
		}

		#endregion

		#region IDataSetAccessObject Members

		/// <summary>
		/// Saves the specified DataSet
		/// </summary>
		/// <param name="ds">The DataSet to save</param>
		/// <returns>Number of modified records</returns>
		public int SaveDS(DataSet ds)
		{
			int i;
			using (IDbConnection conn = this.CreateConnection())
			{
				conn.Open();
				i = this.SaveDS(ds, conn);
			}
			return i;
		}

		/// <summary>
		/// Saves the specified DataSet
		/// </summary>
		/// <param name="ds">The DataSet to save</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns>Number of modified records</returns>
		public virtual int SaveDS(DataSet ds, IDbConnection conn)
		{
			DbDataAdapter da = DbProviderFactory.CreateDataAdapter(this.Provider, this.CreateSelectAllCommand(), conn);
			int modified = 0;
			DataSet modifiedDS = ds.GetChanges();

			if (modifiedDS != null)
			{
				if (this.Provider == DbProviderType.SQLSERVER)
				{
					SqlCommandBuilder cb = new SqlCommandBuilder((SqlDataAdapter) da);
					foreach (DataTable dt in modifiedDS.Tables)
					{
						modified += da.Update(modifiedDS, dt.TableName);
					}
				}
				else
				{
					OleDbCommandBuilder cb = new OleDbCommandBuilder((OleDbDataAdapter) da);
					foreach (DataTable dt in modifiedDS.Tables)
					{
						modified += da.Update(modifiedDS, dt.TableName);
					}
				}
				return modified;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Selects all records
		/// </summary>
		/// <returns></returns>
		public DataSet SelectAllDS()
		{
			return this.CreateDataSet(this.CreateSelectAllCommand());
		}

		/// <summary>
		/// Selects all records
		/// </summary>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public DataSet SelectAllDS(IDbConnection conn)
		{
			return this.GetDataSet(this.CreateSelectAllCommand(), conn);
		}

		/// <summary>
		/// Creates a DataSet using the given command
		/// </summary>
		/// <param name="cmd">The command to execute</param>
		/// <returns></returns>
		public DataSet SelectDS(IDbCommand cmd)
		{
			return this.CreateDataSet(cmd);
		}

		/// <summary>
		/// Creates a DataSet using the given command
		/// </summary>
		/// <param name="sql">The sql command to execute</param>
		/// <returns></returns>
		public DataSet SelectDS(string sql)
		{
			return this.SelectDS(this.CreateCommand(sql));
		}

		/// <summary>
		/// Creates a DataSet using the given command
		/// </summary>
		/// <param name="sql">The sql command to execute</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public DataSet SelectDS(string sql, IDbConnection conn)
		{
			return this.GetDataSet(this.CreateCommand(sql), conn);
		}

		/// <summary>
		/// Creates a DataSet using the given command
		/// </summary>
		/// <param name="cmd">The command to execute</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public DataSet SelectDS(IDbCommand cmd, IDbConnection conn)
		{
			return this.GetDataSet(cmd, conn);
		}

		/// <summary>
		/// Returns one record wrapped in a DataSet
		/// </summary>
		/// <param name="id">The id of the record to select</param>
		/// <returns></returns>
		public DataSet SelectDatasetById(object id)
		{
			return this.SelectOneDS(id);
		}

		/// <summary>
		/// Returns one record wrapped in a DataSet
		/// </summary>
		/// <param name="id">The id of the record to select</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		public DataSet SelectDatasetById(object id, IDbConnection conn)
		{
			return this.SelectOneDS(id, conn);
		}

		/// <summary>
		/// Returns one record wrapped in a DataSet
		/// </summary>
		/// <param name="id">The id of the record to select</param>
		/// <returns></returns>
		/// 
		[Obsolete("Deprecated.  Please use SelectDataSetById")]
		public DataSet SelectOneDS(object id)
		{
			return this.CreateDataSet(this.CreateSelectOneCommand(id));
		}

		/// <summary>
		/// Returns one record wrapped in a DataSet
		/// </summary>
		/// <param name="id">The id of the record to select</param>
		/// <param name="conn">The open IDbConnection to use</param>
		/// <returns></returns>
		[Obsolete("Deprecated.  Please use SelectDataSetById")]
		public DataSet SelectOneDS(object id, IDbConnection conn)
		{
			return this.GetDataSet(this.CreateSelectOneCommand(id), conn);
		}

		#endregion

		#region IDataReaderMapper Members

		/// <summary>
		/// Maps a collection of objects, using the specified DataReader
		/// </summary>
		/// <param name="dr">The DataReader to use for the object mapping</param>
		/// <returns></returns>
		public IList MapObjects(IDataReader dr)
		{
			ArrayList al = new ArrayList();
			while (dr.Read())
			{
				al.Add(this.MapObject(dr));
			}
			return al;
		}

		#endregion

		#region Other Members

		/// <summary>
		/// Gets the total count of records
		/// </summary>
		/// <returns></returns>
		public int GetCount()
		{
			return this.GetCount(null);
		}

		/// <summary>
		/// Gets the total count of records
		/// </summary>
		/// <param name="criteria">The criteria to query with</param>
		/// <returns></returns>
		public int GetCount(ICriteria criteria)
		{
			int count = 0;

			ISelectQuery query = QueryFactory.CreateSelectQuery(this.db);
			query.SetTable(this.table);
			query.AddCountAll();

			if (criteria != null)
			{
				query.SetCriteria(criteria);
			}

			using (IDbConnection conn = this.CreateConnection())
			{
				conn.Open();

				IDataReader dr = DbProviderFactory.CreateDataReader(conn, this.CreateCommand(query.GetText()), this.provider);
				if (dr.Read())
				{
					count = dr.GetInt32(0);
				}
				dr.Close();
			}
			return count;
		}


		/// <summary>
		/// Creates the correct ISelectQuery implementation for the specified database
		/// </summary>
		/// <returns></returns>
		public ISelectQuery CreateSelectQuery()
		{
			ISelectQuery query = QueryFactory.CreateSelectQuery(this.db, this.provider);
			query.SetTable(this.Table);
			return query;
		}

		/// <summary>
		/// Creates the correct IDeleteQuery implementation for the specified database
		/// </summary>
		/// <returns></returns>
		public IDeleteQuery CreateDeleteQuery()
		{
			IDeleteQuery query = QueryFactory.CreateDeleteQuery(this.db, this.provider);
			query.SetTable(this.Table);
			return query;
		}

		/// <summary>
		/// Creates the correct IInsertQuery implementation for the specified database
		/// </summary>
		/// <returns></returns>
		public IInsertQuery CreateInsertQuery()
		{
			IInsertQuery query = QueryFactory.CreateInsertQuery(this.db, this.provider);
			query.SetTable(this.Table);
			return query;
		}

		/// <summary>
		/// Creates the correct IUpdateQuery implementation for the specified database
		/// </summary>
		/// <returns></returns>
		public IUpdateQuery CreateUpdateQuery()
		{
			IUpdateQuery query = QueryFactory.CreateUpdateQuery(this.db, this.provider);
			query.SetTable(this.Table);
			return query;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The connection string
		/// </summary>
		public string ConnectionString
		{
			get { return this.connectionString; }
			set { this.connectionString = value; }
		}

		/// <summary>
		/// The database provider type
		/// </summary>
		public DbProviderType Provider
		{
			get { return this.provider; }
			set { this.provider = value; }
		}

		/// <summary>
		/// The database type
		/// </summary>
		public DbType Db
		{
			get { return this.db; }
			set { this.db = value; }
		}

		/// <summary>
		/// The name of the table
		/// </summary>
		public string Table
		{
			get { return this.table; }
			set { this.table = value; }
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Creates a DataSet, using the given command
		/// </summary>
		/// <param name="cmd">The command to execute</param>
		/// <returns></returns>
		protected DataSet CreateDataSet(IDbCommand cmd)
		{
			return DbProviderFactory.CreateDataSet(this.connectionString, cmd, this.provider);
		}

		/// <summary>
		/// Creates a DataSet, using the given command
		/// </summary>
		/// <param name="cmd">The command to execute</param>
		/// <returns></returns>
		protected DataSet CreateDataSet(string cmd)
		{
			return DbProviderFactory.CreateDataSet(this.connectionString, this.CreateCommand(cmd), this.provider);
		}

		/// <summary>
		/// Creates a DataSet, using the given command
		/// </summary>
		/// <param name="cmd">The command to execute</param>
		/// <param name="conn">The open connection to use</param>
		/// <returns></returns>
		protected DataSet GetDataSet(IDbCommand cmd, IDbConnection conn)
		{
			return DbProviderFactory.CreateDataSet(conn, cmd, this.provider);
		}

		/// <summary>
		/// Creates a connection with the proper connection string filled in
		/// </summary>
		/// <returns></returns>
		public IDbConnection CreateConnection()
		{
			IDbConnection connection = DbProviderFactory.CreateConnection(this.provider);
			connection.ConnectionString = this.connectionString;
			return connection;
		}

		/// <summary>
		/// Creates a command for the correct database provider
		/// and sets the connection
		/// </summary>
		/// <param name="command">The command to execute</param>
		/// <returns></returns>
		public IDbCommand CreateCommand(string command)
		{
			IDbCommand cmd = DbProviderFactory.CreateCommand(this.provider);
			cmd.CommandText = command;
			cmd.Connection = this.CreateConnection();
			return cmd;
		}

		/// <summary>
		/// Creates a command to select all records
		/// </summary>
		/// <returns></returns>
		protected IDbCommand CreateSelectAllCommand()
		{
			IDbCommand command = DbProviderFactory.CreateCommand(this.provider);
			ISelectQuery s = QueryFactory.CreateSelectQuery(this.db);
			s.AddAll();
			s.SetTable(this.Table);
			command.CommandText = s.GetText();
			return command;
		}

		/// <summary>
		/// Creates a command to delete all records
		/// </summary>
		/// <returns></returns>
		protected IDbCommand CreateDeleteAllCommand()
		{
			IDbCommand command = DbProviderFactory.CreateCommand(this.provider);
			IDeleteQuery s = QueryFactory.CreateDeleteQuery(this.db, this.provider);
			s.SetTable(this.Table);
			command.CommandText = s.GetText();
			return command;
		}

		#endregion

		#region Custom Items

		/// <summary>
		/// Used for custom actions when Save is called.  Allows for retrieval
		/// of autoinsert fields, etc. using the open connection.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="connection"></param>
		protected virtual void CustomSave(object o, IDbConnection connection){}

		protected virtual void CustomSave(object o, IDbConnection connection, IDbTransaction transaction){}

		#endregion

		#region Abstract Items

		/// <summary>
		/// Maps an individual object to a DataReader row
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		protected abstract object MapObject(IDataReader dr);

		/// <summary>
		/// Selects one record, using the specified id
		/// </summary>
		/// <param name="id">The id of the record to select</param>
		/// <returns></returns>
		protected abstract IDbCommand CreateSelectOneCommand(object id);

		/// <summary>
		/// Updates a record, using the values from the specified object
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		protected abstract IDbCommand CreateUpdateCommand(object o);

		/// <summary>
		/// Inserts a record, using the values from the specified object
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		protected abstract IDbCommand CreateInsertCommand(object o);

		/// <summary>
		/// Deletes one record, using the specified id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		protected abstract IDbCommand CreateDeleteOneCommand(object id);

		#endregion

	}
}