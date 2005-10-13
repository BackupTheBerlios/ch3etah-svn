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
 *   Date: 7/12/2005
 */

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Wraps custom data sources for a project,
	/// allowing extensions to store data and settings 
	/// in a project file without having to change the 
	/// Project class.
	/// </summary>
	public class CustomDataItem
	{
		private string _name = string.Empty;
		private string _persistedData = string.Empty;
		private ICustomDataProvider _provider;
		private Project _project;

		public CustomDataItem()
		{
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public string PersistedData {
			get {
				if (_provider != null) {
					_persistedData = _provider.GetPersistentData();
				}
				return _persistedData;
			}
			set {
				_persistedData = value;
				if (_provider != null) {
					_provider.RestorePersistentData(_persistedData);
				}
			}
		}
		
		[XmlIgnore]
		public ICustomDataProvider Provider {
			get { return _provider; }
			set { _provider = value; }
		}
		
		protected internal virtual void SetProject(Project project) {
			Debug.Assert(project.FileName != null, "Project should not be null.");
			_project = project;
		}
		
	}
}
