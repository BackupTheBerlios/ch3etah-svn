namespace Adapdev.Data.Mappings
{
	/// <summary>
	/// Represents the provider specific information for a database column type
	/// </summary>
	public class ProviderInfo
	{
		/// <summary>
		/// The numeric provider-specific id
		/// </summary>
		public string Id;
		/// <summary>
		/// The provider-specific name
		/// </summary>
		public string Name;
		/// <summary>
		/// The object equivalent for the provider-specific type
		/// </summary>
		public string Object;
		/// <summary>
		/// The prefix for data
		/// </summary>
		public string Prefix;
		/// <summary>
		/// The postfix for data
		/// </summary>
		public string Postfix;
		/// <summary>
		/// The default value
		/// </summary>
		public string Default;
		/// <summary>
		/// The test default value
		/// </summary>
		public string TestDefault;

	}
}