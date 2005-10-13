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
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

using Ch3Etah.Core.Config;
using Ch3Etah.Core.Exceptions;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Manages the creation of <see cref="IMetadataEntity">
	/// IMetadataEntity</see> objects.
	/// </summary>
	public class MetadataEntityFactory
	{
		private MetadataEntityFactory()
		{
		}
		
		public static IMetadataEntity CreateEntity(string metadataBrandName) {
			return GetMetadataProvider(metadataBrandName).CreateEntity();
		}
		
		public static IMetadataEntity LoadEntity(XmlNode node) {
			string providerName = node.Name;//.Attributes[PROVIDER_ATTRIBUTE].Value.ToString();
			//System.Windows.Forms.MessageBox.Show(providerName);
			return GetMetadataProvider(providerName).LoadEntity(node);
		}
		
		// TODO: Add seperate method to load raw XML entity
		
		#region GetMetadataProvider / GetMetadataProviderFromConfig
		private static IMetadataProvider GetMetadataProvider(string metadataBrandName) {
			if (Ch3EtahConfig.MetadataBrands.Contains(metadataBrandName)) {
				return GetMetadataProviderFromConfig(metadataBrandName);
			} else {
				//return new XmlMetadataProvider(typeof(GenericMetadataEntity), "GenericMetadataProvider");
				XmlMetadataProvider provider = new XmlMetadataProvider();
				return provider;
			}
		}
		
		private static IMetadataProvider GetMetadataProviderFromConfig(string metadataBrandName) {
			MetadataBrand brand = Ch3EtahConfig.MetadataBrands[metadataBrandName];
			Type providerType = null;
			Type entityType = null;
			
			string oldDir = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			try {
				providerType = Type.GetType(brand.ProviderType);
				if (providerType == null) {
					throw new UnknownTypeException("ProviderType '" + brand.ProviderType + "' of brand '" + metadataBrandName + "' could not be found");
				}
				entityType = Type.GetType(brand.EntityType);
				if (entityType == null) {
					System.Diagnostics.Debug.WriteLine(System.IO.Directory.GetCurrentDirectory());
					throw new UnknownTypeException("EntityType '" + brand.EntityType + "' of brand '" + metadataBrandName + "' could not be found");
				}
			}
			finally {
				Directory.SetCurrentDirectory(oldDir);
			}
			
			IMetadataProvider provider = (IMetadataProvider)Activator.CreateInstance(providerType);
			if (brand.ProviderType.IndexOf("GenericMetadataProvider") >= 0) {
				((GenericMetadataProvider)provider).BrandName = metadataBrandName;
				((GenericMetadataProvider)provider).MetadataEntityType = entityType;
			}
			return provider;
		}
		#endregion GetMetadataProvider / GetMetadataProviderFromConfig
		
	}
}
