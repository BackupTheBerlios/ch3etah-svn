namespace Adapdev.Data.Sql
{
	using System;

	/// <summary>
	/// Creates datastore specific query implementations
	/// </summary>
	public class QueryFactory
	{
		public QueryFactory()
		{
		}

		public static IUpdateQuery CreateUpdateQuery(string db)
		{
			return CreateUpdateQuery(DbTypeConverter.Convert(db));
		}

		public static IUpdateQuery CreateUpdateQuery(string db, DbProviderType provider)
		{
			IUpdateQuery query = QueryFactory.CreateUpdateQuery(db);
			query.DbProviderType = provider;
			return query;
		}

		public static IUpdateQuery CreateUpdateQuery(DbType db)
		{
			switch (db)
			{
				case DbType.ACCESS:
					return new AccessUpdateQuery();
				case DbType.SQLSERVER:
					return new SqlUpdateQuery();
				case DbType.ORACLE:
					return new OracleUpdateQuery();
				case DbType.MYSQL:
					return new MySqlUpdateQuery();
				default:
					throw new System.NotImplementedException("DbType " + db + " not supported currently.");
			}
		}

		public static IUpdateQuery CreateUpdateQuery(DbType db, DbProviderType provider)
		{
			IUpdateQuery query = QueryFactory.CreateUpdateQuery(db);
			query.DbProviderType = provider;
			return query;
		}

		public static ISelectQuery CreateSelectQuery(string db)
		{
			return CreateSelectQuery(DbTypeConverter.Convert(db));
		}

		public static ISelectQuery CreateSelectQuery(DbType db)
		{
			switch (db)
			{
				case DbType.ACCESS:
					return new AccessSelectQuery();
				case DbType.SQLSERVER:
					return new SqlSelectQuery();
				case DbType.ORACLE:
					return new OracleSelectQuery();
				case DbType.MYSQL:
					return new MySqlSelectQuery();
				default:
					throw new System.NotImplementedException("DbType " + db + " not supported currently.");			
			}
		}

		public static ISelectQuery CreateSelectQuery(DbType db, DbProviderType provider)
		{
			ISelectQuery query = QueryFactory.CreateSelectQuery(db);
			query.DbProviderType = provider;
			return query;
		}

		public static IDeleteQuery CreateDeleteQuery(string db)
		{
			return CreateDeleteQuery(DbTypeConverter.Convert(db));
		}

		public static IDeleteQuery CreateDeleteQuery(string db, DbProviderType provider)
		{
			IDeleteQuery query = QueryFactory.CreateDeleteQuery(db);
			query.DbProviderType = provider;
			return query;
		}

		public static IDeleteQuery CreateDeleteQuery(DbType db)
		{
			switch (db)
			{
				case DbType.ACCESS:
					return new AccessDeleteQuery();
				case DbType.SQLSERVER:
					return new SqlDeleteQuery();
				case DbType.ORACLE:
					return new OracleDeleteQuery();
				case DbType.MYSQL:
					return new MySqlDeleteQuery();
				default:
					throw new System.NotImplementedException("DbType " + db + " not supported currently.");
			}
		}

		public static IDeleteQuery CreateDeleteQuery(DbType db, DbProviderType provider)
		{
			IDeleteQuery query = QueryFactory.CreateDeleteQuery(db);
			query.DbProviderType = provider;
			return query;
		}

		public static IInsertQuery CreateInsertQuery(string db)
		{
			return CreateInsertQuery(DbTypeConverter.Convert(db));
		}

		public static IInsertQuery CreateInsertQuery(string db, DbProviderType provider)
		{
			IInsertQuery query = QueryFactory.CreateInsertQuery(db);
			query.DbProviderType = provider;
			return query;
		}

		public static IInsertQuery CreateInsertQuery(DbType db)
		{
			switch (db)
			{
				case DbType.ACCESS:
					return new AccessInsertQuery();
				case DbType.SQLSERVER:
					return new SqlInsertQuery();
				case DbType.ORACLE:
					return new OracleInsertQuery();
				case DbType.MYSQL:
					return new MySqlInsertQuery();
				default:
					throw new System.NotImplementedException("DbType " + db + " not supported currently.");
			}
		}

		public static IInsertQuery CreateInsertQuery(DbType db, DbProviderType provider)
		{
			IInsertQuery query = QueryFactory.CreateInsertQuery(db);
			query.DbProviderType = provider;
			return query;
		}
	}
}