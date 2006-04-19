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
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 2005/7/22
 */

using System;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Different types of connections to be used for DataSources.
	/// </summary>
	public class ConnectionType
	{
		// These constants should be used as plceholders when 
		// setting the ConnectionStringTemplate value.
		// (see ConnectionTypeFactory)
		public const string SERVER_PLACEHOLDER = "${Server}";
		public const string DATABASE_PLACEHOLDER = "${Database}";
		public const string USER_PLACEHOLDER = "${User}";
		public const string PASSWORD_PLACEHOLDER = "${Password}";

		private string _name;
		private string _desc;
		private string _connectionStringTemplate;
		private bool _usesIntegratedSecurity;

		internal ConnectionType(string name, 
								string desc, 
								string connectionStringTemplate,
								bool usesIntegratedSecurity) {
			_name = name;
			_desc = desc;
			_connectionStringTemplate = connectionStringTemplate;
			_usesIntegratedSecurity = usesIntegratedSecurity;
		}
		
		public string Name {
			get { return _name; }
		}

		public string Description {
			get { return _desc; }
		}

		public string ConnectionStringTemplate {
			get { return _connectionStringTemplate; }
		}
		
		public bool UsesIntegratedSecurity {
			get { return _usesIntegratedSecurity; }
		}
		
		public override string ToString() {
			return this.Description;
		}

	}
}
