using System;

namespace Adapdev.Data.Schema
{
	/// <summary>
	/// Summary description for AbstractSchemaBuilder.
	/// </summary>
	public abstract class AbstractSchemaBuilder : ISchemaBuilder
	{
		#region ISchemaBuilder Members

		public abstract DatabaseSchema BuildDatabaseSchema(string connectionString, Adapdev.Data.DbType databaseType, Adapdev.Data.DbProviderType providerType, string schemaFilter);

		public static bool EndProgress (Adapdev.IProgressCallback _callback, string message, bool ok) 
		{
			if (_callback != null) 
			{
				if (_callback.IsAborting) return false;
				_callback.SetText(message,"");
				_callback.SetRange(0, 1);
				if (ok) 
				{
					_callback.StepTo(1);
				} 
				else 
				{
					_callback.StepTo(0);
					_callback.AddMessage(ProgressMessageTypes.Critical,"No database schema information found.");
				}
			}
			return true;
		}

		public static bool IncrProgress(Adapdev.IProgressCallback _callback, string message, ref int count)
		{
			if (_callback != null) 
			{
				if (_callback.IsAborting) return false;
				_callback.SetText(message);
				_callback.StepTo(count++);
			}
			return true;
		}

		public static bool StartProgress (Adapdev.IProgressCallback _callback, string message, int max, ref int stepto)
		{
			if (max > 0) 
			{
				if (_callback != null) 
				{
					if (_callback.IsAborting) return false;
					_callback.SetText(message,"");
					_callback.SetRange(0, max);
					_callback.StepTo(stepto = 0);
				}
			}
			return true;
		}

		#endregion
	}
}
