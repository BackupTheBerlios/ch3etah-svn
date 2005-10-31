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
 *   Date: 9/5/2004
 *   Time: 9:11 PM
 */

using System;
using System.IO;
using System.Xml;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Primary interface to be implemented by metadata providers.
	/// </summary>
	/// <remarks>
	/// Classes needing to implement this interface can inherit from
	/// the <see cref="MetadataProviderBase">MetadataProviderBase</see> 
	/// class, which provides base functionality for this interface.
	/// </remarks>
	public interface IMetadataProvider
	{
		Type MetadataEntityType { get; }
		string BrandName { get; }
		
		IMetadataEntity CreateEntity();
		
		IMetadataEntity LoadEntity(XmlNode node);
	}
}
