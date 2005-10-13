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
using System.IO;

using Ch3Etah.Core.CodeGen.PackageLib;

namespace Ch3Etah.Core.CodeGen
{
	/// <summary>
	/// Provides a base from which <see cref="ITransformationEngine">ITransformationEngine</see> 
	/// implementations can inherit.
	/// </summary>
	public abstract class TransformationEngineBase : ITransformationEngine
	{
		protected CodeGenerationContext _context;
		protected string _baseFolder = ".";
		protected Template _template;
		
		public TransformationEngineBase()
		{
		}
		
		public abstract void Transform(System.Xml.XmlNode input, TextWriter output);
		
		public CodeGenerationContext Context {
			get { return _context; }
			set { _context = value; }
		}
		
		public string BaseFolder {
			get { return _baseFolder; }
			set { _baseFolder = value; }
		}
		
		public Template Template {
			get { return _template; }
			set { _template = value; }
		}
		
	}
}
