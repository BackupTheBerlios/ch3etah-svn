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
 *   User: Jacob Eggleston
 *   Date: 22/10/2004
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Ch3Etah.TemplateHelpers
{
	/// <summary>
	/// Provides string helper functions to templates.
	/// </summary>
	public class StringHelper
	{
		
		public string ReplaceRegion(string input, string regexPattern, string replacement)
		{
			return ReplaceRegion(input, regexPattern, replacement, false);
		}
		
		public string ReplaceRegion(string input, string regexPattern, string replacement, bool throwsError)
		{
			Regex expr = new Regex(regexPattern, RegexOptions.Compiled);
			Match newRegionMatch = expr.Match(replacement);
			if (throwsError && !expr.IsMatch(input))
				throw new ArgumentException(string.Format("Error: Could not find region matching RegEx <<{0}>> in source content.", regexPattern));
			
			return expr.Replace(input, newRegionMatch.Value);
		}
		
		public string CamelCase(string name)
		{
			if (name == null || name == "")
				return name;
			
			if (name.Substring(0, 1) == "_")
				return name.Substring(0, 1) + char.ToLower(name[1]) + name.Substring(2);
			else
				return char.ToLower(name[0]) + name.Substring(1);
		}

		public string PascalCase(string name)
		{
			if (name == null || name == "") 
				return name;
			
			if (name.Substring(0, 1) == "_")
				return name.Substring(0, 1) + char.ToLower(name[1]) + name.Substring(2);
			else
				return char.ToUpper(name[0]) + name.Substring(1);
		}
		
		public string LoadFile(string fileName)
		{
			if (File.Exists(fileName))
			{
				using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) 
				{
					using (StreamReader reader = new StreamReader(stream)) 
					{
						return reader.ReadToEnd();
					}
				}
			}
			else
			{
				return "";
			}
		}
		
		public string EscapeXml(string text) 
		{
			return text.Replace("<", "&lt;").Replace(">", "&gt;");
		}
		
	}
}
