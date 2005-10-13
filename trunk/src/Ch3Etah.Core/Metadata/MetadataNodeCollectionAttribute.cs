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
 *   Date: 2005/7/24
 */

using System;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Specifies XML mappings for a field or property that
	/// is a collection of IMetadataNodes.
	/// </summary>
	public class MetadataNodeCollectionAttribute : Attribute
	{
		private string _elementName;
		private string _itemName;
		private Type _itemType;
		
		/// <summary>
		/// Specifies XML mappings for a field or property that
		/// is a collection of IMetadataNodes.
		/// </summary>
		/// <param name="nodeName">
		/// The name of the XML element containing the data
		/// for this member.
		/// </param>
		/// <param name="itemName">
		/// The name to use for the XML child elements representing
		/// items in the collection.
		/// </param>
		/// <param name="itemType">
		/// The System.Type of the items in the collection. NOTE: the
		/// elements in the collection must be of a type which implements
		/// IMetadataNode.
		/// </param>
		public MetadataNodeCollectionAttribute(string elementName, string itemName, Type itemType) {
			_elementName = elementName;
			_itemName = itemName;
			_itemType = itemType;
		}

		public string ElementName {
			get {return _elementName; }
		}

		public string ItemName {
			get { return _itemName; }
		}

		public Type ItemType {
			get { return _itemType; }
		}

	}
}
