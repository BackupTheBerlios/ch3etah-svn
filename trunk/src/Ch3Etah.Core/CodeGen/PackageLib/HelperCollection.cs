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
	/// Description of PackageHelperCollection.
	/// </summary>
	public class HelperCollection : Ch3Etah.Core.CodeGen.PackageLib.Generated.HelperCollection
	{
		
		private Package _package;
		
		internal Package Package {
			get { return _package; }
			set { _package = value; }
		}
		
		#region Constructors
		public HelperCollection()
		{
		}
		
		public HelperCollection(HelperCollection val) : base(val)
		{
		}
		
		public HelperCollection(Ch3Etah.Core.CodeGen.PackageLib.Helper[] val) : base(val)
		{
		}
		#endregion Constructors
		
		public Helper Add(string helperName, string typeDescription) {
			Helper newItem = new Helper();
			newItem.Name = helperName;
			newItem.Type = typeDescription;
			this.Add(newItem);
			return newItem;
		}
		
		public bool Contains(string helperName) {
			if (this[helperName] != null) {
				return true;
			}
			else {
				return false;
			}
		}

		public Helper this[string name] {
			get {
				foreach (Helper helper in this) {
					if (helper.Name == name) {
						if (_package != null) {
							helper.SetPackage(_package);
						}
						return helper;
					}
				}
				return null;
			}
		}
		
		#region Overrides
		new public int Add(Helper val)
		{
			if (_package != null) {
				val.SetPackage(_package);
			}
			if ( !this.Contains(val) ) {
				return base.Add(val);
			}
			return -1;
		}
		
		new public void Insert(int index, Helper val)
		{
			if (_package != null) {
				val.SetPackage(_package);
			}
			if ( this.Contains(val) ) {
				this.Remove(val);
			}
			this.Insert(index, val);
		}
		
		new public HelperCollectionEnumerator GetEnumerator()
		{
			HelperCollectionEnumerator e = new HelperCollectionEnumerator(this);
			if (_package != null) {
				e.Package = _package;
			}
			return e;
		}
		
		new public Helper this[int index] {
			get {
				if (_package != null) {
					((Helper)(List[index])).SetPackage(_package);
				}
				return ((Helper)(List[index]));
			}
			set {
				List[index] = (Helper)value;
				if (_package != null) {
					((Helper)(List[index])).SetPackage(_package);
				}
			}
		}
		#endregion Overrides
		
		#region HelperCollectionEnumerator
		new public class HelperCollectionEnumerator : Ch3Etah.Core.CodeGen.PackageLib.Generated.HelperCollection.HelperCollectionEnumerator, IEnumerator
		{
			Package _package;
			
			public HelperCollectionEnumerator(HelperCollection mappings) : base(mappings)
			{
			}
			
			[XmlIgnore()]
			internal Package Package {
				get {
					return _package;
				}
				set {
					_package = value;
				}
			}

			new public Helper Current {
				get {
					if (_package != null) {
						base.Current.SetPackage(_package);
					}
					return base.Current;
				}
			}
			
			object IEnumerator.Current {
				get {
					if (_package != null) {
						base.Current.SetPackage(_package);
					}
					return base.Current;
				}
			}
		}
		#endregion HelperCollectionEnumerator
		
	}
}
