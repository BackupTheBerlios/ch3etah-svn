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
 *   Time: 8:48 PM
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Abstract class representing a command to be executed 
	/// durring the code generation process.
	/// </summary>
	/// <remarks>
	/// An <see cref="System.Xml.Serialization.XmlIncludeAttribute">XmlInclude</see>
	/// attribute should be applied to the GeneratorCommand class for 
	/// all valid subclasses. Otherwise an exception will be thrown 
	/// when trying to load or save a project that uses any generator 
	/// commands which have not been included.
	/// </remarks>
	[XmlInclude(typeof(CodeGeneratorCommand))]
	public abstract class GeneratorCommand
	{
		#region Constructors and Member Variables
		private Guid _guid;
		private string _name = string.Empty;
		private bool _enabled = true;
		private Project _project;
		
		public GeneratorCommand()
		{
			_guid = Guid.NewGuid();
		}
		#endregion Constructors and Member Variables
		
		#region Properties
		[XmlAttribute()]
		[Browsable(false)]
		public Guid Guid {
			get { return _guid; }
			set { _guid = value; }
		}
		public Guid GUID {
			// Provide a read-only version for the PropertyBrowser
			get { return this.Guid; }
		}
		
		public string Name {
			get {
				if (_name == "" && Project != null) {
					_name = "Command" + Project.GeneratorCommands.Count.ToString();
				}
				return _name;
			}
			set { _name = value; }
		}
		
		public bool Enabled {
			get { return _enabled; }
			set { _enabled = value; }
		}
		
		[XmlIgnore()]
		[Browsable(false)]
		public Project Project {
			get { return _project; }
		}
		protected internal virtual void SetProject(Project project) {
			Debug.Assert(project.FileName != null, "Project should not be null.");
			_project = project;
		}
		#endregion Properties
		
		#region ToString
		public override string ToString() {
			return this.Name;
		}
		#endregion ToString
		
		public abstract void Execute();
		
	}
}
