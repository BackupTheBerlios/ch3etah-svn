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
 *   Date: 30/11/2005
 */

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ch3Etah.Core.Config
{
	public class ParsingConfigurationCollection : Ch3Etah.Core.Config.Generated.ParsingConfigurationCollection
	{

		public ParsingConfiguration this[string name] 
		{
			get 
			{
				foreach (ParsingConfiguration eng in this) 
				{
					if (eng.Name.ToUpper() == name.ToUpper()) 
					{
						return eng;
					}
				}
				return null;
			}
		}
		
		public bool Contains(string name) 
		{
			return (this[name] != null);
		}
	}
}
