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
		public object CreateInstance()
		{
			//Debug.WriteLine("WARNING: Helper.CreateInstance() - Currently loading from AppDomain.CurrentDomain.BaseDirectory instead of Package.BaseFolder.");
			if (_package != null)
			{
				return CreateInstance(_package.BaseFolder);
			}
			else
			{
				return CreateInstance(AppDomain.CurrentDomain.BaseDirectory);
			}
		}

		public object CreateInstance(string baseDir) {
			if (_instance != null) {
				return _instance;
			}
			string oldFolder = Directory.GetCurrentDirectory();
			if (baseDir != "") {
				Directory.SetCurrentDirectory(baseDir);
			}
			try {
				//Debug.WriteLine("WARNING: Helper.CreateInstance() - Cannot load same assembly from two distinct paths.");
				// A good solution that could possibly solve some of the NVelocity problems as well.
				// Use Domain.CreateInstanceFromAndUnwrap()
				// http://codebetter.com/blogs/ranjan.sakalley/archive/2005/04/08/61574.aspx
				// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp05162002.asp
				string assemblyName = this.Type.Substring(0, this.Type.IndexOf(","));
				if (!File.Exists(assemblyName)) {
					Debug.WriteLine(String.Format(
						"WARNING: Helper.CreateInstance() - Helper assembly '{0}' could not be loaded from base directory '{1}'. Trying alternative base directories."
						, assemblyName
						, baseDir
						));
					SetAlternateDirectory(baseDir, ref assemblyName);
				}
				string typeName = this.Type.Substring(this.Type.IndexOf(",") + 1);
				Type type = System.Reflection.Assembly.LoadFrom(Path.GetFullPath(assemblyName)).GetType(typeName);
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
		
		private void SetAlternateDirectory(string baseDir, ref string assemblyName)
		{
			Debug.Indent();
			try
			{
				if (this.Package != null && File.Exists(Path.Combine(this.Package.BaseFolder, assemblyName)))
				{
					Directory.SetCurrentDirectory(this.Package.BaseFolder);
					Debug.WriteLine(String.Format(
						"Helper.SetAlternateDirectory() - Helper assembly exists in or under the package directory. Loading helper from '{0}'."
						, Path.Combine(Directory.GetCurrentDirectory(), assemblyName)
						));
				}
				else if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(assemblyName))))
				{
					assemblyName = Path.GetFileName(assemblyName);
					Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
					Debug.WriteLine(String.Format(
						"Helper.SetAlternateDirectory() - Helper assembly exists in the application base directory. Loading helper from '{0}'."
						, Path.Combine(Directory.GetCurrentDirectory(), assemblyName)
						));
				}
				else if (File.Exists(Path.GetFileName(assemblyName)))
				{
					assemblyName = Path.GetFileName(assemblyName);
					Debug.WriteLine(String.Format(
						"Helper.SetAlternateDirectory() - Loading helper from '{0}'."
						, Path.Combine(Directory.GetCurrentDirectory(), assemblyName)
						));
				}
				else
				{
					Debug.WriteLine("ERROR: Helper.SetAlternateDirectory() - Could not find helper assembly in any of the known alternate directories. Loading helper will be aborted.");
				}
			}
			finally
			{
				Debug.Unindent();
			}
		}
		#endregion GetInstance
		
	}
}
