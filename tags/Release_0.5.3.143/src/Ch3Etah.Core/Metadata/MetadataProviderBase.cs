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
 *   Date: 23/9/2004
 */

using System;
using System.Xml;
using Ch3Etah.Core.Config;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Provides a base implementation for IMetadataProvider
	/// which uses the built-in .NET XML serialization to
	/// load and save metadata entities from an XML file.
	/// </summary>
	public abstract class MetadataProviderBase : IMetadataProvider
	{
		
		protected string _brandName;
		protected Type _metadataEntityType;
		
		public MetadataProviderBase (Type metadataEntityType, string brandName) {
			_brandName = brandName;
			_metadataEntityType = metadataEntityType;
		}
		
		public virtual Type MetadataEntityType {
			get {
				if (_metadataEntityType == null) {
					throw new NullReferenceException();
				}
				return _metadataEntityType;
			}
		}
		
		public virtual string BrandName {
			get {
				if (_brandName == null) {
					throw new NullReferenceException();
				}
				return _brandName;
			}
		}
		
		public virtual IMetadataEntity CreateEntity () {
			return (IMetadataEntity)Activator.CreateInstance(MetadataEntityType);
		}

		public virtual IMetadataEntity LoadEntity(XmlNode node){
			IMetadataEntity entity = (IMetadataEntity)Activator.CreateInstance(MetadataEntityType);
			if ((entity as IMetadataNode) == null) {
				return (IMetadataEntity)XmlSerializationHelper.LoadObject(node, MetadataEntityType);
			}
			else {
				((IMetadataNode)entity).LoadXml(node);
				return entity;
			}
		}
		
	}
}
