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
 *   Date: 9/6/2004
 *   Time: 11:52 AM
 */

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Generic <see cref="IMetadataProvider">IMetadataProvider</see> 
	/// implementation which uses simple XML serialization to load
	/// <see cref="IMetadataEntity">MetadataEntity</see> objects from an
	/// XML node based on the type information contained in a configuration
	/// file.
	/// </summary>
	public class GenericMetadataProvider : MetadataProviderBase
	{
		
		public GenericMetadataProvider() : base(null, null){
		}
		
		public GenericMetadataProvider(Type metadataEntityType, string brandName) : base(metadataEntityType, brandName){
			_brandName = brandName;
			_metadataEntityType = metadataEntityType;
		}

		public new Type MetadataEntityType {
			get {
				if (_metadataEntityType == null) {
					throw new NullReferenceException();
				}
				return _metadataEntityType;
			}
			set { _metadataEntityType = value; }
		}
		
		public new string BrandName {
			get {
				if (_brandName == null) {
					throw new NullReferenceException();
				}
				return _brandName;
			}
			set { _brandName = value; }
		}
		
	}
}
