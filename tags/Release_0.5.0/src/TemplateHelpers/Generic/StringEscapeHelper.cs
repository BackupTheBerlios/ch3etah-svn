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
 *   Date: 22/10/2004
 */

using System;

namespace Ch3Etah.TemplateHelpers
{
	/// <summary>
	/// Description of StringUtils.	
	/// </summary>
	public class StringEscapeHelper
	{
		public StringEscapeHelper()
		{
		}
		
		public string EscapeXml(string text) {
			return text.Replace("<", "&lt;").Replace(">", "&gt;");
		}
		
	}
}
