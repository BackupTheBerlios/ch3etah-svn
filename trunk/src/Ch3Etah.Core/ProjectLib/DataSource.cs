/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 *
 *   User: Jacob Eggleston
 *   Date: 2005/7/10
 */

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Ch3Etah.Core.ProjectLib {
	
	#region DataSourceNameChangedEvent
	public delegate void DataSourceNameChangedEventHandler(object sender, DataSourceNameChangedEventArgs e);

	public class DataSourceNameChangedEventArgs
	{
		public readonly DataSource DataSource;
		public readonly string OldName;
		public readonly string NewName;
		public DataSourceNameChangedEventArgs(DataSource ds, string oldName, string newName)
		{
			this.DataSource = ds;
			this.NewName = newName;
			this.OldName = oldName;
		}
	}
	#endregion DataSourceNameChangedEvent

	[Serializable()]
	[Designer("Ch3Etah.Design.Designers.DataSourceDesigner,Ch3Etah.Design", typeof(IDesigner))]
	public class DataSource {
		
		#region DataSourceNameChangedEvent
		public static event DataSourceNameChangedEventHandler NameChanged;

		private void DoNameChanged(string oldName, string newName)
		{
			if (NameChanged != null && oldName != "" && newName != "")
			{
				NameChanged(this, new DataSourceNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}

		#endregion DataSourceNameChangedEvent

		#region Fields

		private Guid _guid = new Guid();
		private string _name = string.Empty;
		private string _server = string.Empty;
		private string _database = string.Empty;
		private string _user = string.Empty;
		private string _password = string.Empty;
		private ConnectionType _connectionType;
		private string _connectionTypeName = string.Empty;
		private string _connectionString = string.Empty;
		private InputParameterCollection _customAttributes = new InputParameterCollection();
		private Project _project;
		#endregion

		#region Constructors

		public DataSource() {}

		public DataSource(string name, string connectionString) : this() {
			this._name = name;
			this._connectionString = connectionString;
		}

		#endregion

		#region Project

		[XmlIgnore()]
		[Browsable(false)]
		public Project Project {
			get { return _project; }
		}

		internal void SetProject(Project project) {
			Trace.Assert(project.FileName != null, "Project should not be null.");
			_project = project;
		}

		#endregion Project

		#region Properties

		[XmlAttribute()]
		[Browsable(false)]
		public Guid Guid {
			get { return _guid; }
			set { _guid = value; }
		}

		public string Name {
			get {
				if (_name == string.Empty && _project != null) {
					_name = "Data Source " + _project.DataSources.Count;
				}
				return _name;
			}
			set {
				if (_project != null) {
					foreach (DataSource ds in _project.DataSources) {
						if (ds != this && ds.Name == value) {
							throw
								new InvalidOperationException(
								"Another data source with the name '" + value + "' already exists in this project. Please choose another name."
								);
						}
					}
				}
				DoNameChanged(_name, value);
				_name = value;
			}
		}

		[Browsable(false)]
		public string Server {
			get { return _server; }
			set { _server = value; }
		}

		[Browsable(false)]
		public string Database {
			get { return _database; }
			set { _database = value; }
		}

		[Browsable(false)]
		public string User {
			get { return _user; }
			set { _user = value; }
		}

		[Browsable(false)]
		public string Password {
			get { return _password; }
			set { _password = value; }
		}

		[Browsable(false), XmlIgnore()]
		public ConnectionType ConnectionType {
			get { return _connectionType; }
			set {
				_connectionType = value;
				_connectionTypeName = _connectionType.Name;
			}
		}

		[Browsable(false)]
		[XmlElement("ConnectionType")]
		public string ConnectionTypeName {
			get {
				if (_connectionType != null) {
					return _connectionType.Name;
				}
				else {
					return _connectionTypeName;
				}
			}
			set {
				if (_connectionType == null) {
					_connectionTypeName = value;
					_connectionType = ConnectionTypeFactory.GetConnectionType(value, false);
				}
				else {
					_connectionType = ConnectionTypeFactory.GetConnectionType(value);
					_connectionTypeName = _connectionType.Name;
				}
			}
		}

		[Browsable(true)]
		[Editor("Ch3Etah.Design.CustomUI.OleDbConnectionStringDialog", typeof(UITypeEditor))]
		public string ConnectionString {
			get {
				if (_connectionType != null && _connectionType.ConnectionStringTemplate != string.Empty) {
					_connectionString = BuildConnectionString(_connectionType.ConnectionStringTemplate);
				}
				return _connectionString;
			}
			set { _connectionString = value; }
		}

		[Description("Allows the definition of custom XML attributes that will be added to entities created from this datasource.")]
		[Browsable(true)]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		[XmlArrayItem("Attribute")]
		public InputParameterCollection CustomAttributes 
		{
			get { return _customAttributes; }
			set { _customAttributes = value; }
		}

		#endregion

		#region TestConnection

		public void TestConnection() 
		{
			OleDbConnection cn = new OleDbConnection(ConnectionString);
			cn.Open();
			cn.Close();
		}

		public bool IsConnectionValid() {
			try {
				TestConnection();
				return true;
			}
			catch {
				return false;
			}
		}

		#endregion

		#region Private methods

		public string BuildConnectionString(string connectionStringTemplate) {
			string cn = connectionStringTemplate;
			cn = cn.Replace(ConnectionType.SERVER_PLACEHOLDER, this.Server);
			cn = cn.Replace(ConnectionType.DATABASE_PLACEHOLDER, this.Database);
			cn = cn.Replace(ConnectionType.USER_PLACEHOLDER, this.User);
			cn = cn.Replace(ConnectionType.PASSWORD_PLACEHOLDER, this.Password);
			return cn;
		}

		#endregion
	}
}