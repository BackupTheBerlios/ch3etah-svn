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
		
		private Package _package;
		
		internal Package Package 
		{
			get { return _package; }
			set { _package = value; }
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
						if (_package != null) 
						{
							library.SetPackage(_package);
						}
						return library;
					}
				}
				return null;
			}
		}
		
		#region Overrides
		new public int Add(MacroLibrary val)
		{
			if (_package != null) 
			{
				val.SetPackage(_package);
			}
			if ( !this.Contains(val) ) 
			{
				return base.Add(val);
			}
			return -1;
		}
		
		new public void Insert(int index, MacroLibrary val)
		{
			if (_package != null) 
			{
				val.SetPackage(_package);
			}
			if ( this.Contains(val) ) 
			{
				this.Remove(val);
			}
			this.Insert(index, val);
		}
		
		new public MacroLibraryCollectionEnumerator GetEnumerator()
		{
			MacroLibraryCollectionEnumerator e = new MacroLibraryCollectionEnumerator(this);
			if (_package != null) 
			{
				e.Package = _package;
			}
			return e;
		}
		
		new public MacroLibrary this[int index] 
		{
			get 
			{
				if (_package != null) 
				{
					((MacroLibrary)(List[index])).SetPackage(_package);
				}
				return ((MacroLibrary)(List[index]));
			}
			set 
			{
				List[index] = (MacroLibrary)value;
				if (_package != null) 
				{
					((MacroLibrary)(List[index])).SetPackage(_package);
				}
			}
		}
		#endregion Overrides
		
		#region MacroLibraryCollectionEnumerator
		new public class MacroLibraryCollectionEnumerator : Ch3Etah.Core.CodeGen.PackageLib.Generated.MacroLibraryCollection.MacroLibraryCollectionEnumerator, IEnumerator
		{
			Package _package;
			
			public MacroLibraryCollectionEnumerator(MacroLibraryCollection mappings) : base(mappings)
			{
			}
			
			[XmlIgnore()]
			internal Package Package 
			{
				get 
				{
					return _package;
				}
				set 
				{
					_package = value;
				}
			}

			new public MacroLibrary Current 
			{
				get 
				{
					if (_package != null) 
					{
						base.Current.SetPackage(_package);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current 
			{
				get 
				{
					if (_package != null) 
					{
						base.Current.SetPackage(_package);
					}
					return base.Current;
				}
			}
		}
		#endregion MacroLibraryCollectionEnumerator

	}
}
