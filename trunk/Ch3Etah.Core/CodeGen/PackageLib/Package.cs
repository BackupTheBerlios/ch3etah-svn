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
using System.IO;
using System.Xml.Serialization;
using Ch3Etah.Core.Config;

namespace Ch3Etah.Core.CodeGen.PackageLib
{
	/// <summary>
	/// Description of Package.	
	/// </summary>
	public class Package
	{
		private string _name;
		private string _baseFolder;
		private TemplateCollection _templates;
		private MacroLibraryCollection _libraries;
		private HelperCollection _helpers;
		private InputParameterCollection _inputParameters;
		
		public Package()
		{
		}
		
		#region Properties
		[XmlAttribute, ReadOnly(true)]
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		[XmlIgnore, ReadOnly(true)]
		public string BaseFolder {
			get { return _baseFolder; }
			set { _baseFolder = value; }
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
		
		[XmlArrayItem("Library")]
		public MacroLibraryCollection Libraries {
			get { return _libraries; }
			set { _libraries = value; }
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
		
		public static Package Load(string address) {
			if (address.IndexOf(".chp") < 0 && address.IndexOf(".xml") < 0) {
				return LoadFile(address + "\\manifest.xml");
			}
			else {
				return LoadFile(address);
			}
		}
		
		public static Package LoadFile(string fileName) {
			Package package = (Package)XmlSerializationHelper.LoadObject(fileName, typeof(Package));
			package.BaseFolder = Path.GetDirectoryName(Path.GetFullPath(fileName));
			package.Name = Path.GetFileName(fileName);
			return package;
		}
		
	}
}
