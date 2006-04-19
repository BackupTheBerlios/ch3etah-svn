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
using System.IO;

using Ch3Etah.Core.CodeGen.PackageLib;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Core.CodeGen
{
	/// <summary>
	/// Description of CodeGenerationContext.
	/// </summary>
	public class CodeGenerationContext
	{
		private MacroLibraryCollection _libraries = new MacroLibraryCollection();
		private HelperCollection _helpers = new HelperCollection();
		private Ch3Etah.Core.CodeGen.PackageLib
			.InputParameterCollection _parameters = 
			new Ch3Etah.Core.CodeGen.PackageLib.InputParameterCollection();
		private MetadataFile _currentMetadataFile;
		private MetadataFileCollection _projectMetadataFiles = new MetadataFileCollection();

		public CodeGenerationContext()
		{
		}
		
		// TODO: REFACTOR - MacroLibraries
		public MacroLibraryCollection Libraries {
			get { return _libraries;}
			set { _libraries = value;}
		}
		
		public HelperCollection Helpers {
			get { return _helpers;}
			set { _helpers = value;}
		}
		
		public Ch3Etah.Core.CodeGen.PackageLib
			.InputParameterCollection Parameters {
			get { return _parameters;}
			set { _parameters = value;}
		}
		
		public MetadataFile CurrentMetadataFile
		{
			get { return _currentMetadataFile; }
			set { _currentMetadataFile = value; }
		}

		public MetadataFileCollection ProjectMetadataFiles
		{
			get { return _projectMetadataFiles; }
			set { _projectMetadataFiles = value; }
		}

	}
}
