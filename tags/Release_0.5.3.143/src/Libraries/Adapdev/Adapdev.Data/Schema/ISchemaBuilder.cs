using System;

namespace Adapdev.Data.Schema
{
	/// <summary>
	/// Summary description for ISchemaBuilder.
	/// </summary>
	internal interface ISchemaBuilder
	{
		DatabaseSchema BuildDatabaseSchema(string connectionString, Adapdev.Data.DbType databaseType, DbProviderType providerType, string schemaFilter);
	}
}
