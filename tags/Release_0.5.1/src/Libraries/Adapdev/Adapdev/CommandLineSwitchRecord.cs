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
	/// Encapsulates the storage of a single switch entry registered with
	/// the <see cref="CommandLineParser"/> class
	/// </summary>
	internal class CommandLineSwitchRecord
	{
		#region Private Variables
		/// <summary>
		/// Name of this switch
		/// </summary>
		private string _name = "";

		/// <summary>
		/// Usage description for this switch
		/// </summary>
		private string _description = "";

		/// <summary>
		/// Value of this switch
		/// </summary>
		private object _value = null;

		/// <summary>
		/// Data type of this switch
		/// </summary>
		private System.Type _switchType = typeof(bool);

		/// <summary>
		/// List of known aliases for this switch
		/// </summary>
		private System.Collections.ArrayList _Aliases = null;

		/// <summary>
		/// Regular expression used for pattern matching this switch
		/// </summary>
		private string _Pattern = "";

		/// <summary>
		/// Set method of the property associated with this switch
		/// </summary>
		private System.Reflection.MethodInfo _SetMethod = null;

		/// <summary>
		/// Get method of the property associated with this switch
		/// </summary>
		private System.Reflection.MethodInfo _GetMethod = null;

		/// <summary>
		/// Object containing the property associated with this switch
		/// </summary>
		private object _PropertyOwner = null;
		#endregion

		#region Private Utility Functions
		/// <summary>
		/// Sets the name and description of this switch and builds the
		/// regular expression for pattern matching
		/// </summary>
		/// <param name="name">Name of this switch</param>
		/// <param name="description">Usage description for this switch</param>
		private void Initialize( string name, string description )
		{
			_name = name;
			_description = description;

			BuildPattern();
		}

		/// <summary>
		/// Builds the regular expression for pattern matching of this switch
		/// on the command line
		/// </summary>
		/// <remarks>
		/// Syntax rules for switches are as follows
		/// <para>
		/// Switches begin with a slash "/", dash "-", or double dash "--"
		/// </para>
		/// <para>
		/// Strings, including whitespace, that follow the start character
		/// match if they are the same as the switch name, otherwise a
		/// whitespace terminates the switch.
		/// <example>
		/// If "help me" were a switch, placing "/help me" on the command
		/// line would be recognized but "/help you" would parse into the
		/// unknown switch "/help" and parameter "you"
		/// </example>
		/// </para>
		/// <para>
		/// Switch suffixes depend on the data type of the switch
		/// </para>
		/// <list type="table">
		/// <listheader><t>Type</t><d>Suffix</d></listheader>
		/// <item><t>Boolean</t>
		///	<d>+ or - meaning true or false respectively.  A missing
		///	suffix toggles the default value.
		/// <example>/verbose+</example></d>
		/// </item>
		/// <item><t>Integer</t>
		///	<d>Colon or space followed by a number. A missing
		///	suffix has the value zero.
		/// <example>/Retries:5 /MinCount -7</example></d>
		/// </item>
		/// <item><t>String</t>
		///	<d>Colon or space followed by a string. A missing
		///	suffix has the value empty string.  If the string is not
		///	quoted it is terminated at the first white space.
		/// <example>/FirstName John /Greeting:"Hello World"</example></d>
		/// </item>
		/// <item><t>Enum</t>
		///	<d>Colon or space followed by the Enum identifier.  Quotes are
		///	not expected since Enum identifiers cannot have spaces.
		/// <example>/Provider SQLConnection</example></d>
		/// </item>
		/// </list>
		/// </remarks>
		private void BuildPattern()
		{
			string matchString = Name;

			if ( Aliases != null && Aliases.Length > 0 )
				foreach( string s in Aliases )
					matchString += "|" + s;

			string strPatternStart = @"(\s|^)(?<match>(-{1,2}|/)(";
			string strPatternEnd;  // To be defined below.

			// The common suffix ensures that the switches are followed by
			// a white-space OR the end of the string.  This will stop
			// switches such as /help matching /helpme
			//
			string strCommonSuffix = @"(?=(\s|$))";

			if ( Type == typeof(bool) )
				strPatternEnd = @")(?<value>(\+|-){0,1}))";
			else if ( Type == typeof(string) )
				strPatternEnd = @")(?::|\s+))((?:"")(?<value>[^\""]+)(?:"")|(?<value>\S+))";
			else if ( Type == typeof(int) )
				strPatternEnd = @")(?::|\s+))((?<value>(-|\+)[0-9]+)|(?<value>[0-9]+))";
			else if ( Type.IsEnum )
			{
				string[] enumNames = Enumerations;
				string e_str = enumNames[0];
				for ( int e=1; e<enumNames.Length; e++ )
					e_str += "|" + enumNames[e];
				strPatternEnd = @")(?::|\s+))(?<value>" + e_str + @")";
			}
			else
				throw new System.ArgumentException();

			// Set the internal regular expression pattern.
			_Pattern = strPatternStart + matchString + strPatternEnd + strCommonSuffix;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the value of this switch as parsed from the command line
		/// </summary>
		public object Value 
		{
			get 
			{
				if ( ReadValue != null )
					return ReadValue;
				else
					return _value;
			}
		}

		/// <summary>
		/// Gets the value of this switch
		/// </summary>
		public object InternalValue 
		{
			get { return _value; }
		}

		/// <summary>
		/// Gets or sets the name of this switch
		/// </summary>
		public string Name 
		{
			get { return _name;  }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the usage description for this switch
		/// </summary>
		public string Description 
		{
			get { return _description;  }
			set { _description = value; }
		}

		/// <summary>
		/// Gets the data type of this switch
		/// </summary>
		public System.Type Type 
		{
			get { return _switchType; }
		}

		/// <summary>
		/// Gets the list of known aliases for this switch
		/// </summary>
		public string[] Aliases 
		{
			get { return (_Aliases != null) ? (string[])_Aliases.ToArray(typeof(string)): null; }
		}

		/// <summary>
		/// Gets the regular expression used for pattern matching this switch
		/// </summary>
		public string Pattern 
		{
			get { return _Pattern; }
		}
			
		/// <summary>
		/// Sets the set method of the property associated with this switch
		/// </summary>
		public System.Reflection.MethodInfo SetMethod 
		{
			set { _SetMethod = value; }
		}
	
		/// <summary>
		/// Sets the get method of the property associated with this switch
		/// </summary>
		public System.Reflection.MethodInfo GetMethod 
		{
			set { _GetMethod = value; }
		}

		/// <summary>
		/// Sets the object containing the property associated with this switch
		/// </summary>
		public object PropertyOwner 
		{
			set { _PropertyOwner = value; }
		}

		/// <summary>
		/// Gets the value of the property associated with this switch
		/// </summary>
		public object ReadValue 
		{
			get 
			{
				object o = null;
				if ( _PropertyOwner != null && _GetMethod != null )
					o = _GetMethod.Invoke( _PropertyOwner, null );
				return o;
			}
		}

		/// <summary>
		/// Gets a list of strings representing the enumeration indentifiers
		/// for this switch or null if the data type of the switch is not an enum
		/// </summary>
		public string[] Enumerations 
		{
			get 
			{
				if ( _switchType.IsEnum )
					return System.Enum.GetNames( _switchType );
				else
					return new string[]{};
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CommandLineSwitchRecord class
		/// creating a boolean switch with the name and description specified
		/// </summary>
		/// <remarks>
		/// The default value of this switch is false.
		/// </remarks>
		/// <param name="name">Name of the switch</param>
		/// <param name="description">Usage description of the switch</param>
		public CommandLineSwitchRecord( string name, string description )
		{
			Initialize( name, description );
		}

		/// <summary>
		/// Initializes a new instance of the CommandLineSwitchRecord class
		/// creating a switch with the name, description and type specified
		/// </summary>
		/// <exception cref="ArgumentException">
		/// Only types of bool, int, string, and enum are allowed</exception>
		/// <param name="name">Name of the switch</param>
		/// <param name="description">Usage description of the switch</param>
		/// <param name="type">Data type of the switch</param>
		public CommandLineSwitchRecord( string name, string description, System.Type type )
		{
			if ( type == typeof(bool)   ||
				type == typeof(string) ||
				type == typeof(int)    ||
				type.IsEnum )
			{
				_switchType = type;
				Initialize( name, description );
			}
			else
				throw new ArgumentException("Currently only Ints, Bool and Strings are supported");
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Registers an alias with this switch
		/// </summary>
		/// <param name="alias">Name of the alias to be added</param>
		public void AddAlias( string alias )
		{
			if ( _Aliases == null )
				_Aliases = new System.Collections.ArrayList();
			_Aliases.Add( alias );

			BuildPattern();
		}

		/// <summary>
		/// Uses reflection to invoke the set method of the property
		/// associated with this switch
		/// </summary>
		/// <param name="value">Value the property is set to.</param>
		public void Notify( object value )
		{
			if ( _PropertyOwner != null && _SetMethod != null )
			{
				object[] parameters = new object[1];
				parameters[0] = value;
				_SetMethod.Invoke( _PropertyOwner, parameters );
			}
			_value = value;
		}		
		#endregion
	}

}
