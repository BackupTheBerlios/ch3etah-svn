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
using System.Xml;
using System.Xml.Serialization;

using Ch3Etah.Core.CodeGen.PackageLib;

namespace Ch3Etah.Core.CodeGen
{
	/// <summary>
	/// Description of ITransformationEngine.	
	/// </summary>
	public interface ITransformationEngine
	{
		string BaseFolder { get; set; }
		//string TemplateName { get; set; }
		//TextReader Template { get; set; }
		Template Template { get; set; }
		CodeGenerationContext Context { get; set; }
		
		void Transform(XmlNode input, TextWriter output);
		/// <summary>
		/// In transformation engines that implement a
		/// caching mechanism, this method allows callers
		/// to make sure the cache is cleared.
		/// </summary>
		void ClearCache();
	}
}
