namespace Adapdev.Data.Schema
{
	/// <summary>
	/// Represents the schema for a database procedure
	/// </summary>
	public class ProcedureSchema
	{
		protected ParameterSchemaDictionary parameters = new ParameterSchemaDictionary();

		/// <summary>
		/// The name of the procedure
		/// </summary>
		public string Name;

		/// <summary>
		/// Adds a ParameterSchema
		/// </summary>
		/// <param name="p"></param>
		public void AddParameter(ParameterSchema p)
		{
			parameters[p.Name] = p;
		}

		/// <summary>
		/// Removes a ParameterSchema
		/// </summary>
		/// <param name="name">The name of the ParameterSchema to remove</param>
		public void RemoveParameter(string name)
		{
			parameters.Remove(name);
		}

		/// <summary>
		/// Retrieves a ParameterSchema
		/// </summary>
		/// <param name="name">The name of the ParameterSchema to retrieve</param>
		/// <returns></returns>
		public ParameterSchema GetParameter(string name)
		{
			return parameters[name];
		}

		/// <summary>
		/// Returns a collection of parameters
		/// </summary>
		/// <returns></returns>
		public ParameterSchemaDictionary GetParameters()
		{
			return parameters;
		}
	}
}