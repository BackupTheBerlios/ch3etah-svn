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
using System.Diagnostics;
using System.IO;
using System.Xml;

using Ch3Etah.Core.CodeGen.PackageLib;
using Ch3Etah.Core.Exceptions;

//using Commons.Collections;

using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace Ch3Etah.Core.CodeGen.NVelocityEngine
{
	/// <summary>
	/// Implements an <see cref="ITransformationEngine">ITransformationEngine</see> 
	/// which executes NVelocity templates.
	/// </summary>
	public class NVelocityTransformationEngine : TransformationEngineBase
	{
		private VelocityContext _velocityContext;
		private XmlNode _inputNode;
		
		public NVelocityTransformationEngine()
		{
		}
		
		public override void Transform(XmlNode input, TextWriter output) {
			_inputNode = input;
			PrepareVelocityEngine();
			Velocity.Evaluate(_velocityContext, output, this.Template.Name, this.Template.Content);
		}
		
		private void PrepareVelocityEngine()
		{
			ExtendedProperties properties = LoadExtendedProperties();
			PrepareVelocityContext();
			SetMacroLibraries(properties);
			SetLoaderPath(properties);
			Velocity.Init(properties);
		}
		
		private ExtendedProperties LoadExtendedProperties() {
			ExtendedProperties properties = new ExtendedProperties();
			if (File.Exists(BaseFolder + "\\nvelocity.properties")) {
				using (FileStream fs = new FileStream(BaseFolder + "\\nvelocity.properties", FileMode.Open, FileAccess.Read)) {
					properties.Load(fs);
					fs.Flush();
					fs.Close();
				}
			}
			return properties;
		}
		
		private void SetLoaderPath(ExtendedProperties properties) {
			string loaderPath = "";
			// TODO: Solve multiple loader path problem in NVelocity
			if (this.Template.FileName == "") {
				loaderPath = BaseFolder;
			}
			else {
				loaderPath = Path.GetDirectoryName(this.Template.GetFullPath());
				if (loaderPath.IndexOf(BaseFolder) < 0 && loaderPath != BaseFolder) {
					loaderPath = BaseFolder + "," + loaderPath;
				}
				else if(loaderPath != BaseFolder) {
					loaderPath += "," + BaseFolder;
				}
			}
			// HACK: Setting loader path to base folder until loader problem is solved
			//loaderPath = BaseFolder;
			//System.Diagnostics.Debug.WriteLine("NVeleocity:loaderPath=" + loaderPath);
			if (properties.Contains("file.resource.loader.path")) {
				properties["file.resource.loader.path"] = loaderPath;
			} else {
				properties.AddProperty("file.resource.loader.path", loaderPath);
			}
		}
		
		private void SetMacroLibraries(ExtendedProperties properties) {
			string libraries = "";
			foreach (MacroLibrary library in Context.Libraries) {
				if (libraries != "") {
					libraries += ",";
				}
				libraries += library.Address;
			}
			if (libraries == "") {
				return;
			} else {
				//System.Diagnostics.Debug.WriteLine("NVeleocity:libraries=" + libraries);
			}
			if (libraries.Length > 1) {
				if (properties.Contains("velocimacro.library")) {
					string library = properties["velocimacro.library"].ToString();
					properties["velocimacro.library"] = library + "," + libraries;
				} else {
					properties.AddProperty("velocimacro.library", libraries);
				}
			}
		}
		
		private void PrepareVelocityContext()
		{
			_velocityContext = new VelocityContext();
			SetXmlContext();
			SetContextHelpers();
			SetContextParameters();
 		}
		
		private void SetXmlContext() {
			if (_inputNode != null) {
				XmlContext xmlContext = new XmlContext(_inputNode);
				_velocityContext.Put(_inputNode.Name, xmlContext);
			}
		}
		
		private void SetContextHelpers()
		{
			foreach (Helper helper in this.Context.Helpers)
			{
				_velocityContext.Put(helper.Name, helper.CreateInstance());
			}
		}
		
		private void SetContextParameters() {
			foreach (InputParameter parameter in this.Context.Parameters) {
				_velocityContext.Put(parameter.Name, parameter.Value);
			}
		}
	}
}
