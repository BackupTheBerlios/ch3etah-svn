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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

using Ch3Etah.Core.ProjectLib;


namespace Ch3Etah.Core.Metadata
{
	/// <summary>
	/// Provides a base implementation for IMetadataNode.
	/// </summary>
	public abstract class MetadataNodeBase : IMetadataNode, ISupportInitialize
	{
		protected static string CH3ETAH_NAMESPACE_PREFIX = "ch3";
		protected static string CH3ETAH_NAMESPACE_URI = "http://ch3etah.sourceforge.net/xmlns/ch3";
		/*
		 * Use PK index to lookup items for updating
		 * in a collection of IXmlNodes kind of like
		 * in O/R objects.
		 * 
		 * 
		 */

		private string _description;
		private bool isExcluded = false;

		[Category("(General)")]
		[XmlAttribute("description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		#region Changed event
		
		public event EventHandler Changed;
		
		protected virtual void OnChanged() {
			if (Changed != null && !suspendEvents)
				Changed(this, EventArgs.Empty);
		}

		#endregion
		
		[XmlAttribute("ch3:exclude")]
		public bool IsExcluded
		{
			get { return isExcluded; }
			set { isExcluded = value; }
		}

		private bool suspendEvents;				
		private XmlNode _loadedXmlNode;

		XmlNode IMetadataNode.LoadedXmlNode {
			get {
				return _loadedXmlNode;
			}
		}
		
		[Browsable(false)]
		public virtual bool IsDirty {
			get {
				if (_loadedXmlNode == null) {
					Debug.WriteLine("MetadataNodeBase.IsDirty: _loadedXmlNode==null");
					return true;
				}
				XmlNode currentNode = _loadedXmlNode.CloneNode(true);
				this.PersistChanges(currentNode);
//				Debug.WriteLine("MetadataNodeBase.IsDirty: After PersistChanges");
//				Trace.WriteLine("================== currentNode.OuterXml =================");
//				Trace.WriteLine(currentNode.OuterXml);
//				Trace.WriteLine("================= _loadedXmlNode.OuterXml ===============");
//				Trace.WriteLine(_loadedXmlNode.OuterXml);
//				Trace.WriteLine("====================================================");
				return (currentNode.OuterXml != _loadedXmlNode.OuterXml);
			}
		}
		
		
		public virtual void LoadXml(XmlNode xmlNode) {
			_loadedXmlNode = xmlNode;
			foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) {
				FillMember(xmlNode, field);
			}
			foreach (PropertyInfo property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
				FillMember(xmlNode, property);
			}
			OnChanged();
		}
		
		public virtual void SetAttributeValue(string attributeName, string value)
		{
			XmlAttribute attr = _loadedXmlNode.Attributes[attributeName];
			if (attr == null) {
				attr = _loadedXmlNode.OwnerDocument.CreateAttribute(attributeName);
				_loadedXmlNode.Attributes.Append(attr);
			}
			attr.Value = value;
			foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) {
				SetAttributeMemberValue(attributeName, field);
			}
			foreach (PropertyInfo property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
				SetAttributeMemberValue(attributeName, property);
			}
		}

		public virtual void PersistChanges(XmlNode xmlNode) {
			foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) {
				PersistMember(xmlNode, field);
			}
			foreach (PropertyInfo property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
				PersistMember(xmlNode, property);
			}
			if (_loadedXmlNode == null) {
				_loadedXmlNode = xmlNode;
			}
		}
		
		public abstract string Name { get; set; }
		
		#region Private methods
		private void SetAttributeMemberValue(string attributeName, MemberInfo member)
		{
			if (IsXmlIgnore(member)) {
				return;
			}
			if (attributeName == GetAttributeName(member)) {
				FillMemberFromAttribute(_loadedXmlNode, member, attributeName);
			}
		}

		private void FillMember(XmlNode parentNode, MemberInfo member) {
			if (IsXmlIgnore(member)) {
				return;
			}
			string attributeName = GetAttributeName(member);
			if (attributeName != string.Empty) {
				FillMemberFromAttribute(parentNode, member, attributeName);
			}
			else {
				string elementName = GetElementName(member);
				if (elementName != ""){
					FillMemberFromElement(parentNode, member, elementName);
				}
			}
		}

		private void FillMemberFromAttribute(XmlNode parentNode, MemberInfo member, string attributeName) {
			XmlAttribute attr = parentNode.Attributes[attributeName];
			if (attr == null) {
				return;
			}

			if (member is PropertyInfo) {
				SetPropertyValue((PropertyInfo)member, attr.Value);
			}
			else if (member is FieldInfo) {
				SetFieldValue((FieldInfo)member, attr.Value);
			}
		}
		
		private void SetPropertyValue(PropertyInfo prop, string xmlValue) {
			Type type = prop.PropertyType;
			if (type == typeof(Guid)) {
				prop.SetValue(this, XmlConvert.ToGuid(xmlValue), null);
			}
			else {
				prop.SetValue(this, Convert.ChangeType(xmlValue, type), null);
			}
		}
		
		private void SetFieldValue(FieldInfo field, string xmlValue) {
			Type type = field.FieldType;
			if (type == typeof(Guid)) {
				field.SetValue(this, XmlConvert.ToGuid(xmlValue));
			}
			else {
				field.SetValue(this, Convert.ChangeType(xmlValue, type));
			}
		}

		private void FillMemberFromElement(XmlNode parentNode, MemberInfo member, string elementName) {
			XmlNode fieldNode = parentNode.SelectSingleNode(elementName);
			if (fieldNode == null) {
				return;
			}
				
			object memberValue = GetMemberValue(member, true);
			
			bool filled = false;
			if ((memberValue as IMetadataNode) != null) {
				((IMetadataNode)memberValue).LoadXml(fieldNode);
				filled = true;
			}
			object[] attributes = member.GetCustomAttributes(typeof(MetadataNodeCollectionAttribute), false);
			if (attributes.Length > 0) {
				MetadataNodeCollectionAttribute attr = (MetadataNodeCollectionAttribute)attributes[0];
				IList list = (IList)memberValue;
				foreach (XmlNode itemNode in fieldNode.ChildNodes) {
					if (itemNode.Name == attr.ItemName) {
						IMetadataNode item = null;
						foreach (IMetadataNode i in list) {
							if (i.LoadedXmlNode == itemNode) {
								item = i;
								break;
							}
						}
						if (item == null) {
							item = (IMetadataNode)Activator.CreateInstance(attr.ItemType);
							list.Add(item);
						}
						item.LoadXml(itemNode);
					}
				}
				filled = true;
			}
			if (!filled) {
				if ((member as PropertyInfo) != null) {
					SetPropertyValue((PropertyInfo)member, fieldNode.InnerText);
				}
				else if ((member as FieldInfo) != null) {
					SetFieldValue((FieldInfo)member, fieldNode.InnerText);
				}
			}
		}

		private void PersistMember(XmlNode parentNode, MemberInfo member) {
			if (IsXmlIgnore(member)) {
				return;
			}
			string elementName = GetElementName(member);
			string attributeName = GetAttributeName(member);
			if (attributeName != string.Empty) {
				object memberValue = GetMemberValue(member, false);
				XmlAttribute memberAttr = parentNode.Attributes[attributeName];
				if (memberAttr == null) {
					// TODO: Replace this with an extensible solution that allows more namespaces.
					if (attributeName.Trim().StartsWith(CH3ETAH_NAMESPACE_PREFIX + ":")) {
						memberAttr = parentNode.OwnerDocument.CreateAttribute(attributeName, CH3ETAH_NAMESPACE_URI);
					}
					else {
						memberAttr = parentNode.OwnerDocument.CreateAttribute(attributeName);
					}
					parentNode.Attributes.Append(memberAttr);
				}
				memberAttr.Value = GetXmlString(memberValue);
				return;
			}
			else if (elementName != string.Empty){
				object memberValue = GetMemberValue(member, false);
				object[] collectionAttributes = member.GetCustomAttributes(typeof(MetadataNodeCollectionAttribute), false);
				XmlNode memberNode = parentNode.SelectSingleNode(elementName);
				if (memberNode == null) {
					// TODO: Replace this with an extensible solution that allows more namespaces.
					if (elementName.Trim().StartsWith(CH3ETAH_NAMESPACE_PREFIX + ":")) 
					{
						memberNode = parentNode.OwnerDocument.CreateElement(elementName, CH3ETAH_NAMESPACE_URI);
					}
					else {
						memberNode = parentNode.OwnerDocument.CreateElement(elementName);
					}
					parentNode.AppendChild(memberNode);
				}
				
				if (memberValue == null) {
					parentNode.RemoveChild(memberNode);
					return;
				}
				
				if ((memberValue as IMetadataNode) == null && collectionAttributes.Length <= 0) {
					memberNode.InnerText = GetXmlString(memberValue);
					return;
				}
				
				if ((memberValue as IMetadataNode) != null) {
					((IMetadataNode)memberValue).PersistChanges(memberNode);
				}
				if (collectionAttributes.Length > 0) {
					MetadataNodeCollectionAttribute attr = (MetadataNodeCollectionAttribute)collectionAttributes[0];
					IList list = (IList)memberValue;
					RemoveDeletedNodes(memberNode, list, attr.ItemName);
					XmlNode previousNode = null;
					foreach (IMetadataNode item in list) {
						XmlNode itemNode = GetOrCreateItemNode(memberNode, item, attr.ItemName, previousNode);
						item.PersistChanges(itemNode);
						previousNode = itemNode;
					}
				}
			}
		}
		
		private string GetXmlString(object memberValue) {
			if (memberValue == null) {
				return "";
			}
			Type type = memberValue.GetType();
			if (type == typeof(string)) {
				return (string)memberValue;
			}
			else if (type == typeof(bool)) {
				return XmlConvert.ToString((bool)memberValue);
			}
			else if (type == typeof(int)) {
				return XmlConvert.ToString((int)memberValue);
			}
			else if (type == typeof(Int16)) {
				return XmlConvert.ToString((Int16)memberValue);
			}
			else if (type == typeof(Int64)) {
				return XmlConvert.ToString((Int64)memberValue);
			}
			else if (type == typeof(DateTime)) {
				return XmlConvert.ToString((DateTime)memberValue);
			}
			else if (type == typeof(Guid)) {
				return XmlConvert.ToString((Guid)memberValue);
			}
			else if (type == typeof(Double)) {
				return XmlConvert.ToString((Double)memberValue);
			}
			else if (type == typeof(float)) {
				return XmlConvert.ToString((float)memberValue);
			}
			else if (type == typeof(byte)) {
				return XmlConvert.ToString((byte)memberValue);
			}
			else if (type == typeof(Decimal)) {
				return XmlConvert.ToString((Decimal)memberValue);
			}
			else if (type == typeof(char)) {
				return XmlConvert.ToString((char)memberValue);
			}
			throw new ArgumentException("The parameter memberValue is not a recognized type.");
		}

		private XmlNode GetOrCreateItemNode (XmlNode listNode, IMetadataNode item, string itemElementName, XmlNode insertAfter) {
			foreach (XmlNode itemNode in listNode.SelectNodes(itemElementName)) {
				if (item.LoadedXmlNode == itemNode) {
					if (insertAfter != null)
					{
						listNode.RemoveChild(itemNode);
						listNode.InsertAfter(itemNode, insertAfter);
					}
					return itemNode;
				}
			}
			XmlNode node = listNode.OwnerDocument.CreateElement(itemElementName);
			if (item.LoadedXmlNode != null)
			{
				foreach (XmlAttribute attr in item.LoadedXmlNode.Attributes)
				{
					node.Attributes.Append((XmlAttribute)attr.Clone());
				}
				node.InnerXml = item.LoadedXmlNode.InnerXml;
			}
			if (insertAfter != null)
			{
				listNode.InsertAfter(node, insertAfter);
			}
			else
			{
				listNode.AppendChild(node);
			}
			return node;
		}

		private void RemoveDeletedNodes(XmlNode listNode, IList list, string itemElementName) {
			foreach (XmlNode itemNode in listNode.SelectNodes(itemElementName)) {
				if (!ListContainsNodeItem(list, itemNode)) {
					listNode.RemoveChild(itemNode);
				}
			}
		}
		
		private bool ListContainsNodeItem(IList list, XmlNode node) {
			foreach (IMetadataNode item in list) {
				if (item.LoadedXmlNode == node) {
					return true;
				}
			}
			return false;
		}

		private bool IsXmlIgnore(MemberInfo member) {
			return member.IsDefined(typeof(XmlIgnoreAttribute), false);
		}
		
		private object GetMemberValue(MemberInfo member, bool createIfNotPresent) {
			if ((member as FieldInfo) != null) {
				object fieldValue = ((FieldInfo)member).GetValue(this);
//				if (fieldValue == null && createIfNotPresent) {
//					fieldValue = Activator.CreateInstance(((FieldInfo)member).FieldType);
//					((FieldInfo)member).SetValue(this, fieldValue);
//				}
				return fieldValue;
			}
			else {
				object propValue = ((PropertyInfo)member).GetValue(this, null);
//				if (propValue == null) {
//					propValue = Activator.CreateInstance(((PropertyInfo)member).PropertyType);
//					((PropertyInfo)member).SetValue(this, propValue, null);
//				}
				return propValue;
			}
		}
		
		private string GetAttributeName(MemberInfo member) {
			object[] attributes;
			attributes = member.GetCustomAttributes(typeof(XmlAttributeAttribute), false);
			if (attributes.Length > 0) {
				XmlAttributeAttribute attr = (XmlAttributeAttribute)attributes[0];
				return attr.AttributeName;
			}
			return "";
		}

		private string GetElementName(MemberInfo member) {
			object[] attributes;
			attributes = member.GetCustomAttributes(typeof(MetadataNodeCollectionAttribute), false);
			if (attributes.Length > 0) {
				MetadataNodeCollectionAttribute attr = (MetadataNodeCollectionAttribute)attributes[0];
				return attr.ElementName;
			}
			attributes = member.GetCustomAttributes(typeof(XmlElementAttribute), false);
			if (attributes.Length > 0) {
				XmlElementAttribute attr = (XmlElementAttribute)attributes[0];
				if (attr.ElementName == string.Empty) {
					if ((member as PropertyInfo) != null) {
						return ((PropertyInfo)member).Name;
					}
					else if ((member as FieldInfo) != null) {
						return ((FieldInfo)member).Name;
					}
				}
				return attr.ElementName;
			}
			if ((member as PropertyInfo) != null && ((PropertyInfo)member).CanRead && ((PropertyInfo)member).CanWrite ) {
				return ((PropertyInfo)member).Name;
			}
			return "";
		}
		#endregion Private methods

		#region ISupportInitialize Members

		public void BeginInit() {
			suspendEvents = true;
		}

		public void EndInit() {
			suspendEvents = false;
		}

		#endregion
	}
}
