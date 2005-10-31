using System;
using System.Reflection;
using System.Xml;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Adapdev.Data {
	/// <summary>
	/// DBConnectionManager manages the loading of the ADAPDEV.XML file into the DBConnection* classes
	/// </summary>
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public class ProviderConfig : DataSet {

		private DbConnectionTypes _connectionTypes = null;

		public ProviderConfig() : this( FindConfigFile() ) { }
		public ProviderConfig( string config ) {
			_connectionTypes = LoadConfig ( config );
		}

		// Provides Get Access the the COnnection Types Instance
		public DbConnectionTypes ConnectionTypes {
			get { return _connectionTypes; }
		}

		/// <summary>
		/// Loads a config file into a XMLDocument and populates a DBConnectionTypes collection of the 
		/// database connection details found in the config file. 
		/// </summary>
		/// <param name="config">The name (and path) of a config file containing <connection> elements</param>
		/// <returns>A Collection of Connection Types</returns>
		private DbConnectionTypes LoadConfig ( string config ) {

			try {
				this.ReadXml(config);
				DbConnectionTypes connectionTypes = new DbConnectionTypes();
				DataRow connectionsRow = this.Tables["connections"].Rows[0];

				// Read the available connections from the connections collection
				// --------------------------------------------------------------
				foreach (DataRow connectionRow in connectionsRow.GetChildRows("connections_connection")) {

					DbConnectionType connectionType = new DbConnectionType();
					connectionType.Name = connectionRow["name"].ToString();
					connectionType.DbTypeName = connectionRow["type"].ToString();
					connectionType.InternalProviderName = connectionRow["internalProvider"].ToString();

					// Read the Settings for this connection type
					// --------------------------------------------------------------
					foreach (DataRow settingsRow in connectionRow.GetChildRows("connection_settings")) {
						if (settingsRow.Table.Columns.Contains("file"))		{
							connectionType.SupportsFile = GetSettingState(settingsRow["file"].ToString(),false);
							connectionType.PromptFile   = GetSettingValue(settingsRow["file"].ToString());
						}
						if (settingsRow.Table.Columns.Contains("server"))		{
							connectionType.SupportsServer = GetSettingState(settingsRow["server"].ToString(),true);
							connectionType.PromptServer   = GetSettingValue(settingsRow["server"].ToString());
						}
						if (settingsRow.Table.Columns.Contains("name"))		{
							connectionType.SupportsName = GetSettingState(settingsRow["name"].ToString(),true);
							connectionType.PromptName   = GetSettingValue(settingsRow["name"].ToString());
						}
						if (settingsRow.Table.Columns.Contains("userid"))		{
							connectionType.SupportsUserID = GetSettingState(settingsRow["userid"].ToString(),true);
							connectionType.PromptUserID   = GetSettingValue(settingsRow["userid"].ToString());
						}
						if (settingsRow.Table.Columns.Contains("password"))		{
							connectionType.SupportsPassword = GetSettingState(settingsRow["password"].ToString(),true);
							connectionType.PromptPassword   = GetSettingValue(settingsRow["password"].ToString());
						}
						if (settingsRow.Table.Columns.Contains("filter"))		{
							connectionType.SupportsFilter = GetSettingState(settingsRow["filter"].ToString(),false);
							connectionType.PromptFilter   = GetSettingValue(settingsRow["filter"].ToString());
						}
					}

					// Read each of the Providers Details
					// --------------------------------------------------------------
					foreach (DataRow providersRow in connectionRow.GetChildRows("connection_providers")) {
						foreach (DataRow providerRow in providersRow.GetChildRows("providers_provider")) {

							DbConnectionProvider connectionProvider = new DbConnectionProvider();
							connectionProvider.Name = providerRow["name"].ToString();
							connectionProvider.ProviderTypeName = providerRow["type"].ToString();
							connectionProvider.Parent = connectionType;
							connectionProvider.Template = Regex.Replace(providerRow["provider_Text"].ToString(), @"[\r\t\n]", "");

							if (providerRow.Table.Columns.Contains("allowEmptyParameters")) connectionProvider.AllowEmptyParameters = GetSettingState(providerRow["allowEmptyParameters"].ToString(), true);
							if (providerRow.Table.Columns.Contains("enabled")) connectionProvider.Enabled = GetSettingState(providerRow["enabled"].ToString(),true);
							if (providerRow.Table.Columns.Contains("fileMask")) connectionProvider.FileMask = providerRow["fileMask"].ToString();

							connectionType.Providers.Add(connectionProvider); 
						}
					}
					connectionTypes.Add(connectionType);
				}
				return connectionTypes;
			}
			catch (Exception ex) {
				throw new ApplicationException(String.Format("Could not reference the ProviderConfig.xml configuration file: {0}\n{1}", config, ex.Message));
			}
		}

		/// <summary>
		/// Returns the State if defined for a property. If it is false, return false 
		/// otherwise return true. 
		/// </summary>
		/// <param name="setting"></param>
		/// <returns></returns>
		private bool GetSettingState(string setting, bool defaultFlag) {
			if (setting == null) return defaultFlag;
			if (setting.Equals(string.Empty)) return defaultFlag;
			try {
				return Convert.ToBoolean(setting);
			} catch {
				return true;
			}
		}

		/// <summary>
		/// Return the setting for a property. If the property was "false" return a empty string
		/// or if the property was "true" return a empty string, otherwise return the contents. 
		/// </summary>
		/// <param name="setting"></param>
		/// <returns></returns>
		private string GetSettingValue(string setting) {
			if (setting == null) return string.Empty;
			if (setting.Equals(string.Empty)) return string.Empty;
			try {
				bool flag = Convert.ToBoolean(setting);
				return string.Empty;
			} catch {
				return setting.Trim();
			}
		}

		/// <summary>
		/// Determine the location and allow overriding of the ConfigFile
		/// </summary>
		/// <returns></returns>
		public static string FindConfigFile () {
			string configFile     = String.Empty;
			string possibleConfig = String.Empty;
		
			// Look in the current application folder for the file
			if (configFile == String.Empty) {
				possibleConfig = AppDomain.CurrentDomain.BaseDirectory + @"ProviderConfig.XML";
				if (System.IO.File.Exists(possibleConfig)) {
					configFile = possibleConfig;
				}
			}
		
			// If not found there, then override with a hardcoded default
			// TODO: Allow this to be overriden with the commandline
			if (configFile == String.Empty) {
				possibleConfig = @"..\..\..\..\..\Adapdev\src\Adapdev.Data\Xml\ProviderConfig.xml";
				if (System.IO.File.Exists(possibleConfig)) {
					configFile = possibleConfig;
				}
			}
		
			if (configFile == String.Empty) {
				throw new ApplicationException(String.Format("Could not find the ProviderConfig.xml configuration file.\n It should exist in {0}", AppDomain.CurrentDomain.BaseDirectory));
			}
			return configFile;
		}
	}
}