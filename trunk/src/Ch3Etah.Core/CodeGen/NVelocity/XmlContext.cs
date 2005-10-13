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
 *   Date: 19/11/2004
 */

using System;
using System.Collections;
using System.Xml;

using NVelocity.Context;

namespace Ch3Etah.Core.CodeGen.NVelocityEngine
{
	
	/// <summary>
	/// An NVelocity context which exposes an XML Document in
	/// a template as if it were an object hierarchy.
	/// </summary>
	/// <remarks>
	/// Using this Context, the following XML document:
	/// <code escaped="true">
	/// &lt;Metadata>
	///   &lt;Tables>
	///     &lt;Table name="Table_1" />
	///     &lt;Table name="Table_2" />
	///   &lt;/Tables>
	/// &lt;/Metadata>
	/// </code>
	/// <br/>
	/// Could be accessed in an NVelocity template like this:
	/// <code escaped="true">
	/// #foreach ($table in $Metadata.Tables)
	///     $table.name -- output each table's name
	/// #end
	/// </code>
	/// </remarks>
	public class XmlContext : IContext, IEnumerable
	{
		private XmlNode _node;
		
		/// <summary>
		/// Creates a new instance of <b>XmlContext</b> that wraps
		/// an <b>XmlNode</b>.
		/// </summary>
		/// <param name="node">
		/// An <b>XmlNode</b> to wrap in the <b>XmlContext</b>
		/// </param>
		public XmlContext(XmlNode node) {
			_node = node;
		}
		
		#region XmlDoc
		/// <summary>
		/// Informs whether or not the current <b>XmlContext</b>
		/// contains a value or child objec with the specified
		/// <b>key</b>.
		/// </summary>
		/// <param name="key">
		/// String value used to look up a contained value or child object.
		/// </param>
		/// <returns>
		/// <b>True</b> if the key is contained in the current context, 
		/// <b>false</b> otherwise.
		/// </returns>
		#endregion XmlDoc
		public bool ContainsKey(object key) {
			string name = key.ToString();
			return (Get(name) != null);
		}
		
		#region XmlDoc
		/// <summary>
		/// Retrieves a value or child object from the current <b>XmlContext</b>.
		/// </summary>
		/// <remarks>
		/// <p>Possible Return values are (in order of priority):
		/// <list type="bullet">
		/// 	<item><description>
		/// 		If <b>key</b> is the name of an attribute of the current 
		/// 		XML element, then the value of that attribute will be
		/// 		returned.
		/// 	</description></item>
		/// 	<item><description>
		/// 		If <b>key</b> is the name of exactly one child element of 
		/// 		the current XML element, then a new <b>XmlContext</b> which  
		/// 		wraps that child element will be returned.
		/// 	</description></item>
		/// 	<item><description>
		/// 		If <b>key</b> is the name of multiple child elements, then an
		/// 		array of <b>XmlContext</b>s will be returned.
		/// 	</description></item>
		/// 	<item><description>
		/// 		If <b>key</b> is the string literal <b>"XmlNode"</b>, then the
		/// 		.NET <b>XmlNode</b> which is wrapped by the current 
		/// 		<b>XmlContext</b> will be returned.
		/// 	</description></item>
		/// </list>
		/// </p>
		/// <p><b>Note:</b> Key values are case-sensitive.</p>
		/// </remarks>
		/// <param name="key">
		/// String value used to look up a contained value or child object.
		/// </param>
		/// <returns></returns>
		#endregion XmlDoc
		public object Get(string key) {
//			if (key == "Page" && _node.Name == "Metadata") {
//				System.Diagnostics.Debug.WriteLine("Key '" + key + "' from XmlContext '" + _node.Name + "' requested.");
//			}
			if (_node.Attributes != null && _node.Attributes[key] != null) {
				//Debug.WriteLine("XmlContext.Get(): Matching attribute found having value of '" + node.Attributes[key].Value + "'");
				return _node.Attributes[key].Value;
			} else {
				ArrayList nodes = this.SelectNodes(key);
				
				if (nodes.Count == 1) {
					return new XmlContext((XmlNode)nodes[0]);
				} else if (nodes.Count > 1) {
					return GetChildContexts(nodes);
				} else if (key == "XmlNode") {
					return _node;
				} else if (key == "Count" || key == "Length") {
					return _node.ChildNodes.Count;
				} else {
					//======================================================
					//CODE INCLUDED FOR TEMPLATE COMPATABILITY WITH NCodeGen
					//======================================================
					//There is no node with this key (Tables)
					ArrayList singularNodes = new ArrayList();
					foreach (XmlNode childNode in _node.ChildNodes) {
						//But maybe Nodes in Singular form (Table)
						if (GetPluralForm(childNode.Name) == key) {
							singularNodes.Add(childNode);
						}
					}
					//======================================================
					if (singularNodes.Count > 0) {
						return GetChildContexts();
					}
					else {
						return null;
					}
				}
			}
		}
		
		private ArrayList SelectNodes(string key) {
			ArrayList nodes = new ArrayList();
			foreach (XmlNode node in _node.ChildNodes) {
				if (node.Name == key) {
					nodes.Add(node);
				}
			}
			return nodes;
		}

		/// <summary>
		/// Gets the text contained in the current XML element.
		/// </summary>
		public override string ToString() {
			return _node.InnerText;
		}
		
		private XmlContext[] GetChildContexts() {
			ArrayList nodes = new ArrayList();
			foreach (XmlNode node in _node.ChildNodes) {
				nodes.Add(node);
			}
			return GetChildContexts(nodes);
		}
		
		private XmlContext[] GetChildContexts(IList nodes) {
			XmlContext[] childContexts = new XmlContext[nodes.Count];
			//System.Diagnostics.Debug.WriteLine(_node.Name + " : " + nodes.Count);
			for (int x = 0; x < nodes.Count; x++) {
				//System.Diagnostics.Debug.WriteLine(x.ToString() + " : " + nodes[x].Name);
				childContexts[x] = new XmlContext((XmlNode)nodes[x]);
			}
			return childContexts;
		}
		
		#region System.Collections.IEnumerable interface implementation
		IEnumerator IEnumerable.GetEnumerator() {
			return GetChildContexts().GetEnumerator();
		}
		#endregion
		
		#region Not Implemented IContext interface members
		/// <summary>
		/// <b><font color="Red">
		/// This interface member is not implemented on this class.
		/// </font></b>
		/// </summary>
		[Obsolete()]
		public object[] Keys {
			get { return null; }
		}
		
		/// <summary>
		/// <b><font color="Red">
		/// This interface member is not implemented on this class.
		/// </font></b>
		/// </summary>
		[Obsolete()]
		public object Put(string key, object value) {
			return null;
		}
		
		/// <summary>
		/// <b><font color="Red">
		/// This interface member is not implemented on this class.
		/// </font></b>
		/// </summary>
		[Obsolete()]
		public object Remove(object key) {
			return null;
		}
		#endregion Not Implemented IContext interface members
		
		/// <summary>
		/// Gets the plural form of a word.
		/// INCLUDED FOR TEMPLATE COMPATABILITY WITH THE NCodeGen FRAMEWORK.
		/// </summary>
		private string GetPluralForm(string word) {
			if (word.EndsWith("y")) {
				if (word.EndsWith("ay") || word.EndsWith("ey") || word.EndsWith("oy") || word.EndsWith("iy") || word.EndsWith("uy")) {
					return word+ "s";
				}
				else {
					return word.Remove(word.Length-1,1) + "ies";
				}
			}
			else if (word.EndsWith("f")) {
				return word.Remove(word.Length-1,1) + "ves";
			}
			else if (word.EndsWith("fe")) {
				return word.Remove(word.Length-2,2) + "ves";
			}
			else if (word.EndsWith("x")) {
				return word + "es";
			}
			else {
				return word + "s";
			}
		}
		
	}
}
