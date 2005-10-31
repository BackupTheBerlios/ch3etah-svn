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
	/// Wrapper class to allow passing switch information to another program.
	/// This class hides some methods and properties of the assembly
	/// internal <see cref="CommandLineSwitchRecord" />
	/// </summary>
    public class CommandLineSwitchInfo 
	{
		#region Private Variables
		/// <summary>
		/// Storage for the <see cref="CommandLineSwitchRecord" /> object.
		/// </summary>
		private object _Switch = null;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the name of the switch.
		/// </summary>
		public string Name
		{
			get { return (_Switch as CommandLineSwitchRecord).Name; }
		}

		/// <summary>
		/// Gets or sets the usage description for this switch
		/// </summary>
		public string Description
		{
			get { return (_Switch as CommandLineSwitchRecord).Description; }
		}

		/// <summary>
		/// Gets the list of known aliases for this switch
		/// </summary>
		public string[] Aliases
		{
			get { return (_Switch as CommandLineSwitchRecord).Aliases; }
		}

		/// <summary>
		/// Gets the data type of this switch
		/// </summary>
		public System.Type Type
		{
			get { return (_Switch as CommandLineSwitchRecord).Type; }
		}
		/// <summary>
		/// Gets the value of this switch as parsed from the command line
		/// </summary>
		public object Value
		{
			get { return (_Switch as CommandLineSwitchRecord).Value; }
		}

		/// <summary>
		/// Gets the value of this switch
		/// </summary>
		public object InternalValue
		{
			get { return (_Switch as CommandLineSwitchRecord).InternalValue; }
		}

		/// <summary>
		/// Indicates if the switch is an enum type.
		/// </summary>
		public bool IsEnum
		{
			get { return (_Switch as CommandLineSwitchRecord).Type.IsEnum; }
		}

		/// <summary>
		/// Gets a list of strings representing the enumeration indentifiers
		/// for this switch or null if the data type of the switch is not an enum
		/// </summary>
		public string[] Enumerations
		{ 
			get { return (_Switch as CommandLineSwitchRecord).Enumerations; }
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the CommandLineSwitchInfo class.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// rec is not of type <see cref="CommandLineSwitchRecord" />.</exception>
		/// <param name="rec">Object this class wraps.</param>
		public CommandLineSwitchInfo( object rec )
		{
			if ( rec is CommandLineSwitchRecord )
				_Switch = rec;
			else
				throw new ArgumentException("The object passed is not a CommandLineSwitchRecord type.");
		}
	}
}
