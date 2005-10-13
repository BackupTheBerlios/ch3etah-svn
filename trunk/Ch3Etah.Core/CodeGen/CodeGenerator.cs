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

namespace Ch3Etah.Core.CodeGen
{
	/// <summary>
	/// Description of CodeGenerator.
	/// </summary>
	public class CodeGenerator
	{
		private Package _package;
		private string _baseFolder = "";
		private Template _template;
		private CodeGenerationContext _context = new CodeGenerationContext();
		
		#region Constructors
		public CodeGenerator(Package package) {
			_package = package;
			_baseFolder = package.BaseFolder;
			this.Context.Helpers = new HelperCollection(package.Helpers);
			this.Context.Libraries = new MacroLibraryCollection(package.Libraries);
		}
		
		public CodeGenerator(string baseFolder) {
			_baseFolder = baseFolder;
		}
		
		public CodeGenerator(Package package, Template template) : this(package) {
			_template = template;
		}
		
		public CodeGenerator(string baseFolder, Template template) : this(baseFolder) {
			_template = template;
		}
		#endregion Constructors
		
		#region Properties
		public Package Package {
			get { return _package; }
			set { _package = value; }
		}
		
		public Template Template {
			get { return _template; }
			set { _template = value; }
		}
		
		public CodeGenerationContext Context {
			get { return _context; }
		}
		#endregion Properties
		
		public void Generate(XmlDocument input, TextWriter output) {
			Generate(input.DocumentElement, output);
		}
		
		public void Generate(XmlNode input, TextWriter output) {
			ITransformationEngine engine = GetEngine();
			engine.Transform(input, output);
		}
		
		public void Generate(TextReader input, TextWriter output) {
			XmlDocument doc = new XmlDocument();
			doc.Load(input);
			Generate(doc, output);
		}
		
		public void Generate(TextWriter output) {
			ITransformationEngine engine = GetEngine();
			engine.Transform(null, output);
		}
		
		private ITransformationEngine GetEngine() {
			ITransformationEngine engine = TransformationEngineFactory.CreateEngine(_template.Engine);
			
			engine.BaseFolder = _baseFolder;
			engine.Template = this.Template;
			
			CheckParameters();
			engine.Context = this.Context;
			
			return engine;
		}
		
		private void CheckParameters()
		{
			if (_package != null) {
				// We do the template level params first so that parameters defined at the 
				// template level will override those defined at the package level.
				InputParameterCollection parameters = this.Template.InputParameters;
				CheckParameters(parameters, Context.Parameters);
				
				parameters = _package.InputParameters;
				CheckParameters(parameters, Context.Parameters);
			}
		}
		
		private void CheckParameters(InputParameterCollection requiredParameters, InputParameterCollection contextParameters)
		{
			if (requiredParameters != null) {
				foreach (InputParameter parameter in requiredParameters) {
					if (!contextParameters.Contains(parameter.Name)) {
						if (!parameter.IsRequired) {
							contextParameters.Add(parameter.Name, parameter.Value);
						}
						else {
							throw new UnspecifiedParameterException("Parameter '" + parameter.Name + "' was not specified");
						}
					}
				}
			}
		}
		
	}
}
