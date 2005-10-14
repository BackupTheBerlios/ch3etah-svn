using System;
using System.Collections;

namespace Adapdev.Data {
	/// <summary>
	/// The ConnectionType class is a static class designed to provide a wrapper around the various types 
	/// of connections supported by the connection Wizard. 
	/// </summary>
	public class DbConnectionProvider {
		// Private Instance Variables

		private string			 _name		= string.Empty;
		private string			 _template	= string.Empty;
		private string			 _fileMask  = string.Empty;
		private DbProviderType	 _type		= DbProviderType.ODBC;
		private DbConnectionType _parent	= null;
		private bool			 _enabled	= true;
		private bool			 _emptyParm = false; 

		public DbConnectionProvider() { }
		public DbConnectionProvider( string name, string type, string template, DbConnectionType parent) {
			Name			 = name;
			Template		 = template;
			ProviderTypeName = type;
			Parent			 = parent;
		}

		/// <summary>
		/// Functions to return a connection string for "this" Provider
		/// </summary>
		public string ConnectionString(string server) {
			return BuildConnectionString(this.Template, server, "", "", "");
		}
		public string ConnectionString(string server, string database) {
			return BuildConnectionString(this.Template, server, database, "", "");
		}
		public string ConnectionString(string server, string userid, string password) {
			return BuildConnectionString(this.Template, server, "", userid, password);
		}
		public string ConnectionString(string server, string database, string userid, string password) {
			return BuildConnectionString(this.Template, server, database, userid, password);
		}

		/// <summary>
		/// Functions to return a connection string for the "Internally used" Provider
		/// </summary>
		public string InternalProviderString(string server) {
			return BuildConnectionString(this.InternalProvider.Template, server, "", "", "");
		}
		public string InternalProviderString(string server, string database) {
			return BuildConnectionString(this.InternalProvider.Template, server, database, "", "");
		}
		public string InternalProviderString(string server, string userid, string password) {
			return BuildConnectionString(this.InternalProvider.Template, server, "", userid, password);
		}
		public string InternalProviderString(string server, string database, string userid, string password) {
			return BuildConnectionString(this.InternalProvider.Template, server, database, userid, password);
		}

		/// <summary>
		/// Returns a Connection String formatted based on the inputs
		/// </summary>
		/// <param name="server">If specified, the server location or server name</param>
		/// <param name="database">If specified the Name of the database or Initial Catalog</param>
		/// <param name="userid">If specified the UserID</param>
		/// <param name="password">If Specified the Password</param>
		/// <returns></returns>
		private string BuildConnectionString (string template, string server, string database, string userid, string password) {
			// Format the string based on the template from the configuration file
			string connectionString = String.Format(template, server, database, userid, password);

			// We need to remove any blank entries from the connection string. For example, if no UserID is 
			// specified, rathern than having a connection string of UserID=; we remove the text between ; and =;
			if (!_emptyParm) {
				int stripTo = connectionString.IndexOf("=;");
				while (stripTo > 0) {
					int stripFrom = connectionString.Substring(0, stripTo - 1).LastIndexOf(";");
					if (stripFrom > 0) {
						connectionString = connectionString.Substring(0, stripFrom + 1).Trim() + (stripTo == 0 ? "" : connectionString.Substring(stripTo + 2)).Trim();
					} else {
						connectionString = connectionString.Substring(stripTo + 2).Trim();
					}
					stripTo = connectionString.IndexOf("=;");
				}
			}
			return connectionString;
		}

		/// <summary>
		/// Get or Sets the Name of this Connection Object
		/// </summary>
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// True if this Provider is enabled, False if it is Disabled
		/// </summary>
		public bool Enabled {
			get { return _enabled; }
			set { _enabled = value; }
		}

		/// <summary>
		/// Gets or Sets the default File Mask for this provider (if a filebase provider)
		/// </summary>
		public string FileMask {
			get { return _parent.SupportsFile ? _fileMask : ""; }
			set { _fileMask = value; }
		}

		/// <summary>
		/// Determines if empty parameters are allowed in the connection string. 
		/// If set to False, then empty parameters are cleaned and removed from the 
		/// connection string when built. 
		/// </summary>
		public bool AllowEmptyParameters {
			get { return _emptyParm; }
			set { _emptyParm = value; }
		}

		/// <summary>
		/// Gets or Sets the string used to generate the Connection String
		/// </summary>
		public string Template {
			get { return _template; }
			set { _template = value; }
		}

		/// <summary>
		/// Gets or sets the Provider of this Connection Type
		/// </summary>
		public DbProviderType ProviderType {
			get { return _type; }
			set { _type = value; }
		}

		/// <summary>
		/// Gets ort sets the Provider Type by Name
		/// </summary>
		public string ProviderTypeName {
			set { _type = DbProviderTypeConverter.Convert(value); }
			get { return DbProviderTypeConverter.Convert(_type); }
		}

		/// <summary>
		/// Property to Set/Get the enum of this Connection type
		/// </summary>
		public DbType DbType {
			get { return _parent.DbType; }
			set { _parent.DbType = value;}
		}

		/// <summary>
		/// Gets or Sets the DbType by Name rather than enum
		/// </summary>
		public string DbTypeName {
			get { return _parent.DbTypeName; }
			set { _parent.DbTypeName = value; }
		}

		/// <summary>
		/// Returns the OLEDB Version of this Objects Connection Reference
		/// </summary>
		public DbConnectionProvider InternalProvider {
			get { 
				if (this._name.Equals(_parent.InternalProviderName)) return this;
				return this._parent.InternalProvider;
			}
		}
		
		/// <summary>
		/// Gets who the Parent is for this Provider
		/// </summary>
		public DbConnectionType Parent {
			get {
				if (_parent == null) 
					return DbConnectionTypes.UnknownConnectionType();
				else
					return _parent;
			}
			set { _parent = value; }
		}
	}
}


