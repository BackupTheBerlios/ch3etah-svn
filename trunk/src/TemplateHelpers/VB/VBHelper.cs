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
 *   Date: 12/11/2004
 */

using System;

namespace Ch3Etah.TemplateHelpers
{
	/// <summary>
	/// Description of Class1.	
	/// </summary>
	public class VBHelper
	{
		public VBHelper()
		{
		}
		
		public string VBDefaultValue(string val, string type) {
			switch (type.ToUpper()) {
				case "STRING":
					return "\"" + val.Replace("\"", "\"\"") + "\"";
				case "DATE":
					switch (val.ToUpper()) {
						case "DATE":
							return "Date";
						case "TODAY":
							return "Date";
						case "NOW":
							return "Now";
						default:
							return "CDate(\"" + val + "\")";
					}
				default:
					return val;
			}
		}
		
		public string VBGetVBType(string type) {
			return type;
		}
	}
}
