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
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Description of MetadataEntityBase.	
	/// </summary>
	public abstract class MetadataEntityBase : MetadataNodeBase, IMetadataEntity
	{
		protected string _brandName;
		protected string _rootNodeName;
		protected Guid _guid;
		protected MetadataFile _owningMetadataFile;

		public MetadataEntityBase(string brandName, string rootNodeName){
			_guid = Guid.NewGuid();
			_brandName = brandName;
			_rootNodeName = rootNodeName;
		}
		
		[XmlAttribute]
		[Browsable(false)]
		public Guid Guid {
			get {
				return _guid;
			}
			set {
				if (_guid != value) {
					_guid = value;
				}
			}
		}
		
		// Provide a read-only property for the PropertyBrowser
		public Guid GUID {
			get {
				return this.Guid;
			}
		}
		
		[Browsable(false)]
		public string BrandName {
			get {
				return _brandName;
			}
		}
		
		[Browsable(false)]
		public virtual string RootNodeName {
			get { return _rootNodeName; }
		}
		
		/// <summary>
		/// Gets an Xml string representation of the current 
		/// metadata entity.
		/// </summary>
		/// <returns></returns>
		public virtual string SaveAsXmlString() {
			XmlNode node = ((IMetadataNode)this).LoadedXmlNode;
			if (node == null) {
				XmlDocument doc = new XmlDocument();
				doc.LoadXml("<" + this.RootNodeName + " />");
				node = doc.DocumentElement;
			}
			((IMetadataNode)this).PersistChanges(node);
			return node.OuterXml;
//			TextWriter writer = new StringWriter();
//			XmlSerializer serializer = new XmlSerializer(this.GetType());
//			
//			serializer.Serialize(writer, this);
//			XmlDocument doc = new XmlDocument();
//			doc.LoadXml(writer.ToString());
//			
//			return doc.DocumentElement.OuterXml;
		}
		
		[XmlIgnore()]
		[Browsable(false)]
		public MetadataFile OwningMetadataFile {
			get { return _owningMetadataFile; }
			set {
				if (_owningMetadataFile != null && _owningMetadataFile != value) {
					throw new InvalidOperationException("It is not possible to set the owner metadata file for this entity because it already belongs to another metadata file.");
				}
				_owningMetadataFile = value;
			}
		}

	}
}
