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

using Ch3Etah.Core.Config;

namespace Ch3Etah.Core.CodeGen.PackageLib
{
	/// <summary>
	/// Description of CodeTemplate.
	/// </summary>
	public class Template
	{
		private Package _package;
		private string _name = "";
		private string _fileName = "";
		private string _content = "";
		private string _engine = "";
		private string _outputType = "";
		private InputParameterCollection _inputParameters = new InputParameterCollection();
		private MetadataBrandCollection _individualMetadataBrands = new MetadataBrandCollection();
		private MetadataBrandCollection _groupedMetadataBrands = new MetadataBrandCollection();
		
		#region Constructors
		public Template()
		{
		}
		
		public Template(string name) {
			_name = name;
		}
		
		public Template(string name, string fileName) {
			_name = name;
			_fileName = fileName;
		}
		
		public Template(string name, TextReader content) {
			_name = name;
			this.Content = content;
		}
		#endregion Constructors
		
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
		
		[XmlAttribute("Address")]
		public string FileName {
			get { return _fileName; }
			set { _fileName = value; }
		}
		
		[XmlIgnore()]
		[Browsable(false)]
		public TextReader Content {
			get {
				if (_content == "" && _fileName != "") {
					using (StreamReader file = new StreamReader(this.GetFullPath())) {
						_content = file.ReadToEnd();
					}
				}
				return new StringReader(_content);
			}
			set {
				_content = value.ReadToEnd();
			}
		}
		
		[XmlAttribute]
		public string Engine {
			get { return _engine; }
			set { _engine = value;}
		}
		
		[XmlAttribute]
		public string OutputType {
			get { return _outputType; }
			set { _outputType = value; }
		}
		
		public InputParameterCollection InputParameters {
			get { return _inputParameters; }
			set { _inputParameters = value; }
		}
		
		[XmlArrayItem("Brand")]
		public MetadataBrandCollection IndividualMetadataBrands {
			get { return _individualMetadataBrands; }
			set { _individualMetadataBrands = value; }
		}
		
		[XmlArrayItem("Brand")]
		public MetadataBrandCollection GroupedMetadataBrands {
			get { return _groupedMetadataBrands; }
			set { _groupedMetadataBrands = value; }
		}
		#endregion Properties
		
		#region GetFullPath / GetRelativePath
		public string GetFullPath() {
			return GetFullPath(this.FileName);
		}
		
		private string GetFullPath(string fileName) {
			string oldBaseFolder = Directory.GetCurrentDirectory();
			string fullPath = fileName;
			try {
				if (Package != null) {
					//Debug.WriteLine("Template.GetFullPath(): baseDirectory='" + this.Package.BaseFolder);
					Directory.SetCurrentDirectory(this.Package.BaseFolder);
				}
				//Debug.WriteLine("Template.GetFullPath(): baseDirectory='" + Directory.GetCurrentDirectory() + "' fileName='" + fileName + "'");
				if (fileName == "") {
					fullPath = "";
				}
				else {
					fullPath = Path.GetFullPath(fileName);
				}
			}
			finally {
				Directory.SetCurrentDirectory(oldBaseFolder);
			}
			return fullPath;
		}
		
		public string GetRelativePath(string fileName) {
			string relativePath = "";
			if (Package != null) {
				string baseDirectory = this.Package.BaseFolder;
				relativePath = PathResolver.GetRelativePath(baseDirectory, fileName);
			}
			else {
				relativePath = fileName;
			}
			
			return relativePath;
		}
		#endregion GetFullPath / GetRelativePath
		
	}
}
