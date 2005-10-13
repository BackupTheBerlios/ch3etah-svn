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
 *   Date: 19/11/2004
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

using Ch3Etah.Core.Exceptions;

namespace Ch3Etah.Core.CodeGen.PackageLib
{
	/// <summary>
	/// Description of Helper.
	/// </summary>
	public class Helper
	{
		private Package _package;
		private string _name = "";
		private string _type = "";
		
		private object _instance;
		
		public Helper()
		{
		}
		
		#region Properties
		[XmlIgnore()]
		[Browsable(false)]
		public Package Package {
			get {
				return _package;
			}
		}
		internal void SetPackage(Package package) {
			Debug.Assert(package != null, "Package should not be null.");
			_package = package;
		}
		
		[XmlAttribute]
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		[XmlAttribute]
		public string Type {
			get { return _type; }
			set { _type = value; }
		}
		#endregion Properties
		
		#region GetInstance
		public object CreateInstance() {
			if (_instance != null) {
				return _instance;
			}
			string oldFolder = Directory.GetCurrentDirectory();
			if (_package != null && _package.BaseFolder != "") {
				Directory.SetCurrentDirectory(_package.BaseFolder);
			}
			try {
				string assemblyName = this.Type.Substring(0, this.Type.IndexOf(","));
				string typeName = this.Type.Substring(this.Type.IndexOf(",") + 1);
				Type type = System.Reflection.Assembly.LoadFrom(assemblyName).GetType(typeName);
				//Type type = System.Type.GetType(this.Type);
				if (type == null) {
					throw new UnknownTypeException("Helper '" + this.Name + "' of type '" + this.Type + "' could not be created.");
				}
				_instance = Activator.CreateInstance(type);
				return _instance;
			}
			catch (Exception ex) {
				throw new Exception(string.Format("Error loading helper \"{0}\": {1}", this.Type, ex.Message), ex);
			}
			finally {
				Directory.SetCurrentDirectory(oldFolder);
			}
		}
		#endregion GetInstance
		
	}
}
