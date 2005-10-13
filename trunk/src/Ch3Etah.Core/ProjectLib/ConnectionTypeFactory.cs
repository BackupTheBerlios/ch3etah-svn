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
 *   Date: 2005/7/22
 */

using System;
using System.Collections;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Factory class for ConnectionType objects.
	/// </summary>
	public class ConnectionTypeFactory
	{
		private static ArrayList _types;

		static ConnectionTypeFactory() {
			_types = new ArrayList();
			_types.Add(new ConnectionType("SqlServer", "SQL Server", 
				string.Format("Provider=sqloledb; Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};",
					new string[] {ConnectionType.SERVER_PLACEHOLDER, ConnectionType.DATABASE_PLACEHOLDER, ConnectionType.USER_PLACEHOLDER, ConnectionType.PASSWORD_PLACEHOLDER}), 
				false
				));
			_types.Add(new ConnectionType("SqlServerTrusted", "SQL Server (Trusted Connection)", 
				string.Format("Provider=sqloledb; Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI",
					new string[] {ConnectionType.SERVER_PLACEHOLDER, ConnectionType.DATABASE_PLACEHOLDER, ConnectionType.USER_PLACEHOLDER, ConnectionType.PASSWORD_PLACEHOLDER}), 
				true
				));
		}

		private ConnectionTypeFactory() {
		}

		public static ConnectionType GetConnectionType(string name) {
			return GetConnectionType(name, true);
		}

		public static ConnectionType GetConnectionType(string name, bool throwsIfNotFound) {
			foreach (ConnectionType type in _types) {
				if (type.Name == name) {
					return type;
				}
			}
			if (throwsIfNotFound) {
				throw new ApplicationException("No connection type with the name '" + name + "' could be found.");
			}
			else {
				return null;
			}
		}

		public static ConnectionType[] ListConnectionTypes() {
			return (ConnectionType[])_types.ToArray(typeof(ConnectionType));
		}

	}
}
