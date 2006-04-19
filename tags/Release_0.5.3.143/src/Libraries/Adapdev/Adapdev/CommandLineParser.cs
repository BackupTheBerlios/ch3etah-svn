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
SMM	111504	Renamed class to CommandLineParser
*/
#endregion
using System;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Adapdev
{
	/// <summary>
	/// A command-line parsing class that is capable of having switches
	/// programatically registered with it; or, it can use reflection to
	/// find properties with the <see cref="CommandLineSwitchAttribute" />
	/// </summary>
	public class CommandLineParser
	{
		#region Private Variables
		/// <summary>
		/// Command line on which the parser operates
		/// </summary>
		private string _commandLine = "";

		/// <summary>
		/// String holding the part of the command line that did not match
		/// any switch definitions
		/// </summary>
		private string _workingString = "";

		/// <summary>
		/// Name of the application parsed from the command line
		/// </summary>
		private string _applicationName = "";

		/// <summary>
		/// Array of parameters (whitespace separated command line
		/// elements) parsed from the command line
		/// </summary>
		private string[] _splitParameters = null;

		/// <summary>
		/// Array of recognized switches parsed from the command line
		/// </summary>
		private System.Collections.ArrayList _switches = null;
		#endregion

		#region Private Utility Functions
		/// <summary>
		/// Extracts the application name from <see cref="_commandLine" />
		/// and places it in <see cref="_applicationName" />
		/// </summary>
		private void ExtractApplicationName()
		{
			Regex r = new Regex(@"^(?<commandLine>(""[^\""]+""|(\S)+))(?<remainder>.+)",
				RegexOptions.ExplicitCapture);
			Match m = r.Match(_commandLine);
			if ( m != null && m.Groups["commandLine"] != null )
			{
				_applicationName = m.Groups["commandLine"].Value;
				_workingString = m.Groups["remainder"].Value;
			}
		}

		/// <summary>
		/// Extracts the parameters from <see cref="_commandLine" />
		/// and places them in <see cref="_splitParameters" /> 
		/// </summary>
		/// <remarks>
		/// If quotes are used, the quotes are removed but the text between
		/// the quotes is kept as a single parameter
		/// </remarks>
		/// <example>For example:
		/// if <c>_commandLine</c> has <c>one two three "four five six"</c>
		/// then <c>_splitParameters</c> would contain
		/// <list type="table">
		/// <item>
		/// <term>_splitParameters[0]</term><description>one</description>
		/// </item>
		/// <item>
		/// <term>_splitParameters[1]</term><description>two</description>
		/// </item>
		/// <item>
		/// <term>_splitParameters[2]</term><description>three</description>
		/// </item>
		/// <item>
		/// <term>_splitParameters[3]</term><description>four five six</description>
		/// </item>
		/// </list>
		/// </example>
		private void SplitParameters()
		{
			// Populate the split parameters array with the remaining parameters.
			Regex r = new Regex(@"((\s*(""(?<param>.+?)""|(?<param>\S+))))",
				RegexOptions.ExplicitCapture);
			MatchCollection m = r.Matches( _workingString );

			if ( m != null )
			{
				_splitParameters = new string[ m.Count ];
				for ( int i=0; i < m.Count; i++ )
					_splitParameters[i] = m[i].Groups["param"].Value;
			}
		}

		/// <summary>
		/// Parses the command line looking for switches and setting their
		/// proper values
		/// </summary>
		private void HandleSwitches()
		{
			if ( _switches != null )
			{
				foreach ( CommandLineSwitchRecord s in _switches )
				{
					Regex r = new Regex( s.Pattern,
						RegexOptions.ExplicitCapture
						| RegexOptions.IgnoreCase );
					MatchCollection m = r.Matches( _workingString );
					if ( m != null )
					{
						for ( int i=0; i < m.Count; i++ )
						{
							string value = null;
							if ( m[i].Groups != null && m[i].Groups["value"] != null )
								value = m[i].Groups["value"].Value;

							if ( s.Type == typeof(bool))
							{
								bool state = true;
								// The value string may indicate what value we want.
								if ( m[i].Groups != null && m[i].Groups["value"] != null )
								{
									switch ( value )
									{
										case "+": state = true;
											break;
										case "-": state = false;
											break;
										case "":  if ( s.ReadValue != null )
													  state = !(bool)s.ReadValue;
											break;
										default:  break;
									}
								}
								s.Notify( state );
								break;
							}
							else if ( s.Type == typeof(string) )
								s.Notify( value );
							else if ( s.Type == typeof(int) )
								s.Notify( int.Parse( value ) );
							else if ( s.Type.IsEnum )
								s.Notify( System.Enum.Parse(s.Type,value,true) );
						}
					}

					_workingString = r.Replace(_workingString, " ");
				}
			}
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the string holding the part of the command line that did not match
		/// any switch definitions
		/// </summary>
		public string UnhandledCommandLine
		{
			get { return _workingString.TrimStart(' '); }
		}

		/// <summary>
		/// Gets the name of the application parsed from the command line
		/// </summary>
		public string ApplicationName 
		{
			get { return _applicationName; }
		}

		/// <summary>
		/// Gets an array of parameters (whitespace separated command line
		/// elements) parsed from the command line
		/// </summary>
		public string[] UnhandledParameters 
		{
			get { return _splitParameters; }
		}

		/// <summary>
		/// Gets an array of recognized switches parsed from the command line
		/// </summary>
		public CommandLineSwitchInfo[] Switches 
		{
			get 
			{
				if ( _switches == null )
					return null;
				else
				{
					CommandLineSwitchInfo[] si = new CommandLineSwitchInfo[ _switches.Count ];
					for ( int i=0; i<_switches.Count; i++ )
						si[i] = new CommandLineSwitchInfo( _switches[i] );
					return si;
				}
			}
		}

		/// <summary>
		/// Gets the value of the switch at the specified index.
		/// </summary>
		public object this[string name] 
		{
			get
			{
				if ( _switches != null )
					for ( int i=0; i<_switches.Count; i++ )
						if ( string.Compare( (_switches[i] as CommandLineSwitchRecord).Name, name, true )==0 )
							return (_switches[i] as CommandLineSwitchRecord).Value;
				return null;
			}
		}

		/// <summary>
		/// Gets an array containing the switches the parser has
		/// recognized but have not been registered.
		/// </summary>
		/// <remark>
		/// The unhandled switches are not removed from the remainder
		/// of the command-line.
		/// </remark>
		public string[] UnhandledSwitches 
		{
			get
			{
				string switchPattern = @"(\s|^)(?<match>(-{1,2}|/)(.+?))(?=(\s|$))";
				Regex r = new Regex( switchPattern,
					RegexOptions.ExplicitCapture
					| RegexOptions.IgnoreCase );
				MatchCollection m = r.Matches( _workingString );

				if ( m != null )
				{
					string[] unhandled = new string[ m.Count ];
					for ( int i=0; i < m.Count; i++ )
						unhandled[i] = m[i].Groups["match"].Value;
					return unhandled;
				}
				else
					return new string[]{};
			}
		}

		/// <summary>
		/// Gets a string that describes the command line usage
		/// based on the registered switches
		/// </summary>
		public string Usage
		{
			get
			{
				StringBuilder builder = new StringBuilder();
                
				int oldLength;
				foreach ( CommandLineSwitchRecord s in _switches )
				{
					oldLength = builder.Length;
                    
					builder.Append("    /");
					builder.Append(s.Name);
					Type valueType = s.Type;
					if (valueType == typeof(int))
					{
						builder.Append(":<int>");
					}
					else if (valueType == typeof(uint))
					{
						builder.Append(":<uint>");
					}
					else if (valueType == typeof(bool))
					{
						builder.Append("[+|-]");
					}
					else if (valueType == typeof(string))
					{
						builder.Append(":<string>");
					}
					else
					{
						builder.Append(":{");
						bool first = true;
						foreach (FieldInfo field in valueType.GetFields())
						{
							if (field.IsStatic)
							{
								if (first)
									first = false;
								else
									builder.Append('|');
								builder.Append(field.Name);
							}
						}
						builder.Append('}');
					}
                    
                    
					builder.Append("\r\n");
				}
                
				oldLength = builder.Length;
				builder.Append("    @<file>");
				builder.Append(' ', IndentLength(builder.Length - oldLength));
				builder.Append("Read response file for more options");
				builder.Append("\r\n");
                
				return builder.ToString();
			}
		}

		/// <summary>
		/// Computes the number of characters a line should be indented
		/// so that it is right justified at 40 characters.
		/// </summary>
		/// <remarks>
		/// If <paramref name="lineLength"/> is over 36 characters, the indent
		/// is simply set to four.
		/// </remarks>
		/// <param name="lineLength">Number of characters in the line</param>
		/// <returns>Number of spaces to indent</returns>
		private static int IndentLength(int lineLength)
		{
			return Math.Max(4, 40 - lineLength);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Registers a switch with the command line parser
		/// </summary>
		/// <remarks>
		/// Switches named here should not include a switch prefix character
		/// such as a slash or minus.  This is handled by the parser in
		/// <see cref="CommandLineSwitchRecord"/>.
		/// </remarks>
		/// <example>For example:
		/// <code>
		/// parser = new CommandLineParser(System.Environment.CommandLine);
		/// parser.AddSwitch(@"\?" , "show help");
		/// parser.Parse();
		/// </code>
		/// </example>
		/// <param name="name">The name of the switch.</param>
		/// <param name="description">Usage description for the switch.</param>
		public void AddSwitch( string name, string description )
		{
			if ( _switches == null )
				_switches = new System.Collections.ArrayList();

			CommandLineSwitchRecord rec = new CommandLineSwitchRecord( name, description );
			_switches.Add( rec );
		}

		/// <summary>
		/// Registers an array of synonymous switches (aliases) with the
		///  command line parser
		/// </summary>
		/// <remarks>
		/// Switches named here should not include a switch prefix character
		/// such as a slash or minus.  This is handled by the parser in
		/// <see cref="CommandLineSwitchRecord"/>.
		/// <para>
		/// The first switch in the array is considered to be the actual
		/// name of the switch while those that follow create the aliases
		/// for the switch
		/// </para>
		/// </remarks>
		/// <example>For example:
		/// <code>
		/// parser = new CommandLineParser(System.Environment.CommandLine);
		/// parser.AddSwitch(new string[] { "help", @"usage" }, "show help");	//Add a switch with an alias
		/// parser.Parse();
		/// </code>
		/// </example>
		/// <param name="names">Name and aliases of the switch.</param>
		/// <param name="description">Usage description for the switch.</param>
		public void AddSwitch( string[] names, string description )
		{
			if ( _switches == null )
				_switches = new System.Collections.ArrayList();
			CommandLineSwitchRecord rec = new CommandLineSwitchRecord( names[0], description );
			for ( int s=1; s<names.Length; s++ )
				rec.AddAlias( names[s] );
			_switches.Add( rec );
		}

		/// <summary>
		/// Parses the command line.
		/// </summary>
		public void Parse()
		{
			ExtractApplicationName();

			// Remove switches and associated info.
			HandleSwitches();

			// Split parameters.
			SplitParameters();
		}

		/// <summary>
		/// Gets the value of the switch with the specified name
		/// </summary>
		/// <param name="name">Name of the switch</param>
		/// <returns>Value of the named switch</returns>
		public object InternalValue(string name)
		{
			if ( _switches != null )
				for ( int i=0; i<_switches.Count; i++ )
					if ( string.Compare( (_switches[i] as CommandLineSwitchRecord).Name, name, true )==0 )
						return (_switches[i] as CommandLineSwitchRecord).InternalValue;
			return null;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the CommandLineParser class
		/// </summary>
		/// <example>For example:
		/// <code>
		/// CommandLineParser clp = new CommandLineParser(System.Environment.CommandLine);
		/// clp.AddSwitch(@"\?","Show help message");
		/// clp.Parse();
		/// </code>
		/// </example>
		/// <param name="commandLine">The command line to parse when <see cref="Parse"/> is called.</param>
		public CommandLineParser( string commandLine )
		{
			_commandLine = commandLine;
		}

		/// <summary>
		/// Initializes a new instance of the CommandLineParser class
		/// </summary>
		/// <example>For example:
		/// <code>
		/// class Class1
		/// {
		///		private bool _bool;
		///
		///		[CommandLineSwitch("bool","Provide boolean value")]
		///		[CommandLineAlias(new string [] {"boolean","b"})]
		///		public bool Bool
		///		{
		///			get { return _bool; }
		///			set { this._bool = value; }
		///		}
		///		
		///		[STAThread]
		///		static void Main(string[] args)
		///		{
		///			Class1 c = new Class1();
		///		
		///			CommandLineParser clp = new CommandLineParser(System.Environment.CommandLine,c);
		///			clp.Parse();
		///		
		///			Console.WriteLine("bool={0}",c.Bool);
		///		}
		///	}
		/// </code>
		/// </example>
		/// <param name="commandLine">The command line to parse when <see cref="Parse"/> is called.</param>
		/// <param name="classForAutoAttributes">The class that contains attributes from which switches are created.</param>
		public CommandLineParser( string commandLine, object classForAutoAttributes )
		{
			_commandLine = commandLine;

			Type type = classForAutoAttributes.GetType();
			System.Reflection.MemberInfo[] members = type.GetMembers();

			for(int i=0; i<members.Length; i++)
			{
				object[] attributes = members[i].GetCustomAttributes(false);
				if(attributes.Length > 0)
				{
					CommandLineSwitchRecord rec = null;

					foreach ( Attribute attribute in attributes )
					{
						if ( attribute is CommandLineSwitchAttribute )
						{
							CommandLineSwitchAttribute switchAttrib =
								(CommandLineSwitchAttribute) attribute;

							// Get the property information.  We're only handling
							// properties at the moment!
							if ( members[i] is System.Reflection.PropertyInfo )
							{
								System.Reflection.PropertyInfo pi = (System.Reflection.PropertyInfo) members[i];

								rec = new CommandLineSwitchRecord( switchAttrib.Name,
									switchAttrib.Description,
									pi.PropertyType );

								// Map in the Get/Set methods.
								rec.SetMethod = pi.GetSetMethod();
								rec.GetMethod = pi.GetGetMethod();
								rec.PropertyOwner = classForAutoAttributes;

								// Can only handle a single switch for each property
								// (otherwise the parsing of aliases gets silly...)
								break;
							}
						}
					}

					// See if any aliases are required.  We can only do this after
					// a switch has been registered and the framework doesn't make
					// any guarantees about the order of attributes, so we have to
					// walk the collection a second time.
					if ( rec != null )
					{
						foreach ( Attribute attribute in attributes )
						{
							if ( attribute is CommandLineAliasAttribute )
							{
								CommandLineAliasAttribute aliasAttrib =
									(CommandLineAliasAttribute) attribute;
								foreach(string a in aliasAttrib.Alias)
									rec.AddAlias(a);
							}
						}
					}

					// Assuming we have a switch record (that may or may not have
					// aliases), add it to the collection of switches.
					if ( rec != null )
					{
						if ( _switches == null )
							_switches = new System.Collections.ArrayList();
						_switches.Add( rec );
					}
				}
			}
		}
		#endregion
	}
}
