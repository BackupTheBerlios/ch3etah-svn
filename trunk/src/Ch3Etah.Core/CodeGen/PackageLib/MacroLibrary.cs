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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Ch3Etah.Core.CodeGen.PackageLib
{
	/// <summary>
	/// Description of Library.
	/// </summary>
	[ReadOnly(true)]
	public class MacroLibrary
	{
		private Package _package;
		private string _address;
		
		public MacroLibrary()
		{
		}
		
		[XmlIgnore()]
		[Browsable(false)]
		public Package Package 
		{
			get 
			{
				return _package;
			}
		}
		internal void SetPackage(Package package) 
		{
			Debug.Assert(package != null, "Package should not be null.");
			_package = package;
		}
		
		[XmlAttribute]
		public string Address {
			get {
				return _address;
			}
			set {
				_address = value;
			}
		}
		
		#region GetFullPath
		public string GetFullPath() 
		{
			return GetFullPath(this.Address);
		}
		
		private string GetFullPath(string fileName) 
		{
			string oldBaseFolder = Directory.GetCurrentDirectory();
			string fullPath = fileName;
			try 
			{
				if (Package != null) 
				{
					//Debug.WriteLine("Template.GetFullPath(): baseDirectory='" + this.Package.BaseFolder);
					Directory.SetCurrentDirectory(this.Package.BaseFolder);
				}
				//Debug.WriteLine("Template.GetFullPath(): baseDirectory='" + Directory.GetCurrentDirectory() + "' fileName='" + fileName + "'");
				if (fileName == "") 
				{
					fullPath = "";
				}
				else 
				{
					fullPath = Path.GetFullPath(fileName);
				}
			}
			finally 
			{
				Directory.SetCurrentDirectory(oldBaseFolder);
			}
			return fullPath;
		}
		#endregion GetFullPath

	}
}
