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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Ch3Etah.Core.Config;

namespace Ch3Etah.Core.CodeGen.PackageLib
{
	/// <summary>
	/// Description of Package.	
	/// </summary>
	[ReadOnly(true)]
	public class Package
	{

		public Package()
		{
		}
		
		#region Fields
		private string _name;
		private string _baseFolder;
		private string _packageFileName;
		private TemplateCollection _templates;
		private MacroLibraryCollection _libraries;
		private HelperCollection _helpers;
		private InputParameterCollection _inputParameters;
		#endregion Fields

		#region Properties
		[XmlAttribute]
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		[XmlIgnore, ReadOnly(true)]
		public string BaseFolder {
			get { return _baseFolder; }
			set { _baseFolder = value; }
		}
		
		public string PackageFileName
		{
			get { return _packageFileName; }
		}

		[XmlArrayItem("Template")]
		public TemplateCollection Templates {
			get {
				if (_templates != null) {
					_templates.Package = this;
				}
				return _templates;
			}
			set {
				_templates = value;
				_templates.Package = this;
			}
		}
		
		[XmlArray("Libraries"), XmlArrayItem("Library")]
		public MacroLibraryCollection MacroLibraries {
			get {
				if (_libraries != null) 
				{
					_libraries.Package = this;
				}
				return _libraries;
			}
			set {
				_libraries = value;
				_libraries.Package = this;
			}
		}
		
		[XmlArrayItem("Helper")]
		public HelperCollection Helpers {
			get {
				if (_helpers != null) {
					_helpers.Package = this;
				}
				return _helpers;
			}
			set {
				_helpers = value;
				if (_helpers != null) {
					_helpers.Package = this;
				}
			}
		}
		
		[XmlArrayItem("Parameter")]
		public InputParameterCollection InputParameters {
			get { return _inputParameters; }
			set { _inputParameters = value; }
		}
		#endregion Properties
		
		public static string[] PackageFileExtensions
		{
			get { return new string[] {".CHP", ".CH3PKG", ".XML"}; }
		}
		
		#region Load Package
		public static Package Load(string uri) 
		{
			// REFACTOR: THIS IS REDUNDANT NOW
			if (File.Exists(uri)) {
				return LoadFile(uri);
			}
			else if (Directory.Exists(uri)) {
				return LoadFile(uri + @"\manifest.xml");
			}
			else {
				if (Path.GetExtension(uri) == "") {
					throw new DirectoryNotFoundException(
						string.Format("Directory '' could not be found.", uri));
				}
				else {
					throw new FileNotFoundException(
						string.Format("File '' could not be found.", uri));
				}
			}
		}
		
		private static Package LoadFile(string fileName) {
			Package package = (Package)XmlSerializationHelper.LoadObject(fileName, typeof(Package));
			package._packageFileName = Path.GetFullPath(fileName);
			package.BaseFolder = Path.GetDirectoryName(Path.GetFullPath(fileName));
			package.Name = Path.GetFileName(fileName);
			return package;
		}
		#endregion Load
		
		#region List / Get
		private static Hashtable _packagesDictionary = new Hashtable();
		public static Package[] ListPackages(string baseDirectory)
		{
			string fulldir = Path.GetFullPath(baseDirectory);
			ArrayList packages = GetPackagesEntry(fulldir);
			if (packages == null)
			{
				packages = new ArrayList();
				foreach (string ext in PackageFileExtensions)
				{
					DirectoryInfo dirInfo = new DirectoryInfo(fulldir);
					foreach (FileInfo file in dirInfo.GetFiles("*" + ext))
					{
						try
						{
							packages.Add(Load(file.FullName));
						}
						catch (Exception ex)
						{
							Trace.WriteLine(string.Format(
								"Package.ListPackages(): ERROR LOADING PACKAGE '{0}'\r\n{1}",
								file.FullName,
								ex.ToString()));
						}
					}
				}
				SetPackagesEntry(baseDirectory, packages);
			}

			return packages.ToArray(typeof(Package)) as Package[];
		}
		
		private static ArrayList GetPackagesEntry(string baseDirectory)
		{
			string fulldir = Path.GetFullPath(baseDirectory);
			return _packagesDictionary[fulldir] as ArrayList;
		}

		private static void SetPackagesEntry(string baseDirectory, ArrayList packages)
		{
			if (_packagesDictionary.Contains(baseDirectory))
			{
				_packagesDictionary[baseDirectory] = packages;
			}
			else
			{
				_packagesDictionary.Add(baseDirectory, packages);
			}
		}

		public static Package GetPackage(string baseDirectory, string packageName)
		{
			string currentDir = Directory.GetCurrentDirectory();
			try
			{
				//Directory.SetCurrentDirectory(this.GetFullTemplatePath());
				Directory.SetCurrentDirectory(baseDirectory);
				Package package = SearchLoadedPackages(baseDirectory, packageName);
				if (package == null)
				{
					package = Load(packageName);
					package.Name = packageName;
					ArrayList packages = GetPackagesEntry(baseDirectory);
					packages.Add(package);
				}
				return package;
			}
			finally 
			{
				Directory.SetCurrentDirectory(currentDir);
			}
		}
		
		private static Package SearchLoadedPackages(string baseDirectory, string packageName)
		{
			foreach (Package p in ListPackages(baseDirectory))
			{
				if (p.Name == packageName) return p;
			}
			return null;
		}
		#endregion List / Get

	}
}
