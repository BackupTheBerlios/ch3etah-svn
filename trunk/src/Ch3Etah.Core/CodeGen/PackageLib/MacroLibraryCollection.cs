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
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace Ch3Etah.Core.CodeGen.PackageLib
{
	/// <summary>
	/// Description of Libraries.
	/// </summary>
	public class MacroLibraryCollection : Ch3Etah.Core.CodeGen.PackageLib.Generated.MacroLibraryCollection
	{
		
		public MacroLibraryCollection()
		{
		}
		
		public MacroLibraryCollection(MacroLibraryCollection val) : base(val)
		{
		}
		
		public MacroLibraryCollection(Ch3Etah.Core.CodeGen.PackageLib.MacroLibrary[] val) : base(val)
		{
		}
		
		public bool Contains(string address) {
			if (this[address] != null) {
				return true;
			}
			else {
				return false;
			}
		}
		
		public MacroLibrary this[string address] {
			get {
				foreach (MacroLibrary library in this) {
					if (library.Address == address) {
						return library;
					}
				}
				return null;
			}
		}
		
	}
}
