// Original Copyright (c) 2003 Ray Hayes. http://www.codeproject.com/csharp/commandlineparser.asp
#region Modified Copyright / License Information
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
	/// Implements a property attribute called CommandLineAliasAttribute
	/// to work in conjunction with the <see cref="CommandLineSwitchAttribute"/>
	/// attribute.
	/// </summary>
	/// <remarks>
	/// If the CommandLineSwitchAttribute does not exists, then this
	/// attribute is ignored.
	/// </remarks>
	[AttributeUsage( AttributeTargets.Property )]
	public class CommandLineAliasAttribute : System.Attribute
	{
		#region Private Variables
		/// <summary>
		/// Array of alias names for a command line switch that the
		/// property with is attribute is associated with
		/// </summary>
		private string [] _Alias = new string [] {""};
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets an array of alias names for a command line switch that the
		/// property with is attribute is associated with
		/// </summary>
		public string [] Alias 
		{
			get { return _Alias; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CommandLineAliasAttribute class.
		/// </summary>
		/// <example>For example:
		/// <code>
		/// [CommandLineSwitch("string","provide string value")]
		/// [CommandLineAlias("s")]
		/// public string StrSwitch
		/// {
		///		get { return _string; }
		///		set { this._string = value; }
		/// }
		/// </code>
		/// </example>
		/// <param name="alias">Switch alias to which the property is associated</param>
		public CommandLineAliasAttribute( string alias ) 
		{
			_Alias = new string[1];
			_Alias[0] = alias;
		}

		/// <summary>
		/// Initializes a new instance of the CommandLineAliasAttribute class.
		/// </summary>
		/// <example>For example:
		/// <code>
		/// [CommandLineSwitch("int","provide integer value")]
		/// [CommandLineAlias(new string [] {"integer","i"})]
		/// public int Int
		/// {
		/// 	get { return _int; }
		/// 	set { this._int = value; }
		/// }
		/// </code>
		/// </example>
		/// <param name="alias">List of switch alias to which the property is associated</param>
		public CommandLineAliasAttribute( string [] alias ) 
		{
			_Alias = (string [])alias.Clone();
		}
		#endregion
	}
}
