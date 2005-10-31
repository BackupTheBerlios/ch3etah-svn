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
 *   Date: 14/9/2004
 */


using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Ch3Etah.Core.Config;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Description of IMetadataEntity.	
	/// </summary>
	public interface IMetadataEntity
	{
		Guid Guid { get; set; }
		
		string BrandName { get; }
		
		string RootNodeName { get; }
		
		bool IsDirty { get; }
		
		//void SetDirty(bool dirty);
		
		string SaveAsXmlString();

		MetadataFile OwningMetadataFile { get; set; }
	}
}
