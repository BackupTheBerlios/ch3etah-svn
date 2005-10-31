// Original Copyright (c) 2003 Ray Hayes. http://www.codeproject.com/csharp/commandlineparser.asp
#region Copyright / License Information
/*

   Copyright 2004 - 2005 Adapdev Technologies, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

============================
Author Log
============================
III	Full Name
SMM	Sean McCormack (Adapdev)


============================
Change Log
============================
III	MMDDYY	Change

*/
#endregion
using System;

namespace Adapdev
{
	/// <summary>
	/// Implements a property attribute called CommandLineSwitchAttribute
	/// to create command line switches for <see cref="CommandLineParser"/>
	/// via reflection.
	/// </summary>
	/// <remark>
	/// Properties labeled with this attribute must be read/write
	/// </remark>
	[AttributeUsage( AttributeTargets.Property )]
	public class CommandLineSwitchAttribute : System.Attribute
	{
		#region Private Variables
		/// <summary>
		/// Switch name associated with the property to which this attribute
		/// is applied
		/// </summary>
		private string _name = "";

		/// <summary>
		/// Usage description of the switch associated with the property
		/// to which this attribute is applied
		/// </summary>
		private string _description = "";
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the switch name associated with the property to which this attribute
		/// is applied
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets the usage description of the switch associated with the property
		/// to which this attribute is applied
		/// </summary>
		public string Description
		{
			get { return _description; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CommandLineSwitchAttribute class.
		/// </summary>
		/// <example>
		/// <see cref="CommandLineAliasAttribute"/> constructor.
		/// </example>
		/// <param name="name">The name of the switch.</param>
		/// <param name="description">Usage description of the switch.</param>
		public CommandLineSwitchAttribute( string name, string description )
		{
			_name = name;
			_description = description;
		}
		#endregion
	}
}
