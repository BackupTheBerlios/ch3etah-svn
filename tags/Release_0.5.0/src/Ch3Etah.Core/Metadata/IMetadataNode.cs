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
 *   Date: 2005/7/23
 */

using System;
using System.Xml;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Summary description for IMetadataNode.
	/// </summary>
	public interface IMetadataNode
	{
		XmlNode LoadedXmlNode { get; }
		bool IsDirty { get; }
		void LoadXml(XmlNode node);
		void PersistChanges(XmlNode node);
	}
}
