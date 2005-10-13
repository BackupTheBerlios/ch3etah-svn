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
	/// Description of TemplateCollection.
	/// </summary>
	public class TemplateCollection : Ch3Etah.Core.CodeGen.PackageLib.Generated.TemplateCollection
	{
		private Package _package;
		
		internal Package Package {
			get { return _package; }
			set { _package = value; }
		}
		
		public bool Contains(string templateName) {
			if (this[templateName] != null) {
				return true;
			}
			else {
				return false;
			}
		}

		public Template this[string name] {
			get {
				foreach (Template template in this) {
					if (template.Name == name) {
						if (_package != null) {
							template.SetPackage(_package);
						}
						return template;
					}
				}
				return null;
			}
		}
		
		#region Overrides
		new public int Add(Template val)
		{
			if (_package != null) {
				val.SetPackage(_package);
			}
			if ( !this.Contains(val) ) {
				return base.Add(val);
			}
			return -1;
		}
		
		new public void Insert(int index, Template val)
		{
			if (_package != null) {
				val.SetPackage(_package);
			}
			if ( this.Contains(val) ) {
				this.Remove(val);
			}
			this.Insert(index, val);
		}
		
		new public TemplateCollectionEnumerator GetEnumerator()
		{
			TemplateCollectionEnumerator e = new TemplateCollectionEnumerator(this);
			if (_package != null) {
				e.Package = _package;
			}
			return e;
		}
		
		new public Template this[int index] {
			get {
				if (_package != null) {
					((Template)(List[index])).SetPackage(_package);
				}
				return ((Template)(List[index]));
			}
			set {
				List[index] = (Template)value;
				if (_package != null) {
					((Template)(List[index])).SetPackage(_package);
				}
			}
		}
		#endregion Overrides
		
		#region TemplateCollectionEnumerator
		new public class TemplateCollectionEnumerator : Ch3Etah.Core.CodeGen.PackageLib.Generated.TemplateCollection.TemplateCollectionEnumerator, IEnumerator
		{
			Package _package;
			
			public TemplateCollectionEnumerator(TemplateCollection mappings) : base(mappings)
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

			new public Template Current {
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
		#endregion TemplateCollectionEnumerator
		
	}
}
