using System;
using System.Collections;

namespace Adapdev.Data
{
	/// <summary>
	/// Summary description for DBConnectionType.
	/// </summary>
	public class DbConnectionType {
		private string	_name					= "";
		private string  _internalProvider		= "";
		private DbType	_dbType;
		private DbConnectionProviders _dbProviders;

		private readonly string _defaultFile	= "File Location";
		private readonly string _defaultServer	= "Server";
		private readonly string _defaultName	= "Database Name";
		private readonly string _defaultUserID  = "User ID";
		private readonly string _defaultPassword= "Password";
		private readonly string _defaultFilter  = "Filter";

		private string  _promptFile 			= string.Empty;
		private string  _promptServer			= string.Empty;
		private string  _promptName				= string.Empty;
		private string	_promptUserID			= string.Empty;
		private string	_promptPassword			= string.Empty;
		private string	_promptFilter			= string.Empty;

		private bool	_supportsServer			= false;
		private bool	_supportsName			= false;
		private bool	_supportsUserID			= false;
		private bool	_supportsPassword		= false;
		private bool	_supportsFilter 		= false;

		public DbConnectionType() { 
			_dbProviders		= new DbConnectionProviders();		
		}

		public DbConnectionType( string name, DbType dbType, DbProviderType provider) : this(name, dbType, DbProviderTypeConverter.Convert(provider)) {	}

		public DbConnectionType( string name, DbType dbType, string provider) : this() {
			Name				= name;
			InternalProviderName= provider;
			DbType				= dbType;
		}

		public DbConnectionType( string name, DbType dbType, string provider, bool supportsServer, bool supportsName, bool supportsUserID, bool supportsPassword, bool supportsFilter) : this() {
			Name				= name;
			InternalProviderName= provider;
			DbType				= dbType;
			_supportsServer		= supportsServer;
			_supportsName		= supportsName;
			_supportsUserID		= supportsUserID;
			_supportsPassword	= supportsPassword;
			_supportsFilter		= supportsFilter;
		}

		/// <summary>
		/// Property to get/set the Name of this Connection Type
		/// </summary>
		public string Name {
			get { return _name; }
			set {_name = value; }
		}

		/// <summary>
		/// Gets or Sets the DbType by Name rather than enum
		/// </summary>
		public string DbTypeName {
			get { return DbTypeConverter.Convert(_dbType); }
			set { _dbType = DbTypeConverter.Convert(value); }
		}

		/// <summary>
		/// Property to Set/Get the enum of this Connection type
		/// </summary>
		public DbType DbType {
			get { return _dbType; }
			set { _dbType = value;}
		}

		/// <summary>
		/// Returns a reference to the collection of available providers
		/// </summary>
		public DbConnectionProviders Providers {
			get { return _dbProviders;}
			set { _dbProviders = value;}
		}

		/// <summary>
		/// Allows Setting/Getting the InternalProvider which is a reference to the 
		/// name of a Provider instance from the ProvidersCollection of the Provider 
		/// instance which allows access to table information (normally a OLEDB connection)
		/// </summary>
		public string InternalProviderName {
			get {return _internalProvider; }
			set {_internalProvider = value;}
		}

		public DbConnectionProvider InternalProvider {
			get{
				if (_internalProvider.Equals("") || !_dbProviders.ContainsKey(_internalProvider)) {
					throw new ApplicationException(String.Format("No Internal Provider defined for {0}.",this._name));
				}
				return _dbProviders[_internalProvider];
			}
		}

		/// <summary>
		/// Returns a provider object for a OLEDB connection type
		/// </summary>
		public DbConnectionProvider OLEDBConnection {
			get { 
				if ((_internalProvider.Equals("")) || !_dbProviders.ContainsKey(_internalProvider)) {
					throw new ApplicationException(String.Format("No Internal Provider defined for {0}", _name));
				} else {
					try {
						DbConnectionProvider provider = _dbProviders[_internalProvider];
						return provider;
					} catch (Exception ex) {
						throw new ApplicationException(String.Format("Could not find the internal provider {0} for {1}\n{2}", _internalProvider, _name, ex.Message));
					}
				}
			}
		}

		/// <summary>
		/// Returns true if this connection type supports a SERVER Name
		/// </summary>
		public bool SupportsServer {
			get { return _supportsServer; }
			set { _supportsServer = value; }
		}

		public string PromptServer {
			get { return (_promptServer == string.Empty ? _defaultServer : _promptServer).Trim() + ":"; }
			set { _promptServer = value; }
		}

		/// <summary>
		/// Returns true if this connection type supports a Name or Initial Catalog
		/// </summary>
		public bool SupportsName {
			get { return _supportsName; }
			set { _supportsName = value; }
		}

		public string PromptName {
			get { return (_promptName == string.Empty ? _defaultName : _promptName).Trim() + ":"; }
			set { _promptName = value; }
		}

		/// <summary>
		/// Returns true if this connection type supports a UserID (Trusted do not)
		/// </summary>
		public bool SupportsUserID {
			get { return _supportsUserID; }
			set { _supportsUserID = value; }
		}

		public string PromptUserID {
			get { return (_promptUserID == string.Empty ? _defaultUserID : _promptUserID).Trim() + ":"; }
			set { _promptUserID = value; }
		}

		/// <summary>
		/// Returns true if this connection type supports a Password
		/// </summary>
		public bool SupportsPassword {
			get { return _supportsPassword; }
			set { _supportsPassword = value; }
		}

		public string PromptPassword {
			get { return (_promptPassword == string.Empty ? _defaultPassword : _promptPassword).Trim() + ":"; }
			set { _promptPassword = value; }
		}

		/// <summary>
		/// Returns true if this connection type supports a Password
		/// </summary>
		public bool SupportsFilter {
			get { return _supportsFilter; }
			set { _supportsFilter = value; }
		}

		public string PromptFilter {
			get { return (_promptFilter == string.Empty ? _defaultFilter : _promptFilter).Trim() + ":"; }
			set { _promptFilter = value; }
		}

		/// <summary>
		/// Returns true if this connection type supports a File DNS specifier. If Server is false
		/// then this connection type is a file based type. 
		/// </summary>
		public bool SupportsFile {
			get { return !_supportsServer; }
			set { _supportsServer = !value; }
		}

		public string PromptFile {
			get { return (_promptFile == string.Empty ? _defaultFile : _promptFile).Trim() + ":"; }
			set { _promptFile = value; }
		}
	}
}
