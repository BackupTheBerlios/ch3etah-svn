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
 *   Date: 9/7/2004
 *   Time: 11:20 PM
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Description of GeneratorCommands.	
	/// </summary>
	public class GeneratorCommandCollection : Ch3Etah.Core.ProjectLib.Generated.GeneratorCommandCollection
	{
		private Project _project;
		
		public GeneratorCommandCollection()
		{
		}
		
		[XmlIgnore()]
		internal Project Project {
			get {
				return _project;
			}
			set {
				_project = value;
				foreach(GeneratorCommand command in this.List) {
					command.SetProject(this.Project);
				}
			}
		}
		
		#region Overridden properties and methods
		new public int Add(GeneratorCommand val)
		{
			val.SetProject(Project);
			if ( !this.Contains(val) ) {
				return base.Add(val);
			}
			return -1;
		}
		
		[Browsable(false)]
		new public int Count {
			get {
				return base.Count;
			}
		}
		
		new public void Insert(int index, GeneratorCommand val)
		{
			val.SetProject(Project);
			if ( this.Contains(val) ) {
				base.Remove(val);
			}
			base.Insert(index, val);
		}

		new public GeneratorCommandEnumerator GetEnumerator()
		{
			GeneratorCommandEnumerator e = new GeneratorCommandEnumerator(this);
			e.Project = Project;
			return e;
		}

		new public GeneratorCommand this[int index] {
			get {
				((GeneratorCommand)(List[index])).SetProject(Project);
				return ((GeneratorCommand)(List[index]));
			}
			set {
				List[index] = (GeneratorCommand)value;
				((GeneratorCommand)(List[index])).SetProject(Project);
			}
		}
		#endregion Overridden properties and methods
		
		#region GeneratorCommandEnumerator class
		new public class GeneratorCommandEnumerator : Ch3Etah.Core.ProjectLib.Generated.GeneratorCommandCollection.GeneratorCommandEnumerator, IEnumerator
		{
			Project _project;
			
			public GeneratorCommandEnumerator(GeneratorCommandCollection mappings) : base(mappings)
			{
			}
			
			[XmlIgnore()]
			internal Project Project {
				get {
					return _project;
				}
				set {
					_project = value;
				}
			}

			new public GeneratorCommand Current {
				get {
					base.Current.SetProject(Project);
					return base.Current;
				}
			}
			
			object IEnumerator.Current {
				get {
					base.Current.SetProject(Project);
					return base.Current;
				}
			}
		}
		#endregion GeneratorCommandEnumerator class
	}
}
