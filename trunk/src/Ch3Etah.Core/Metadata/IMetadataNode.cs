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

		// METHODS TO IMPLEMENT ONCE CODE IS BEING GENERATED
		/// <summary>
		/// Gets the current state of the object in the form 
		/// of an XML string. <br><br>
		/// <b>NOTE:</b>
		/// Unlike the PersistChanges method, this method simply
		/// returns a snapshot of the object's current state,
		/// without affecting it's loaded state.
		/// </summary>
		//string GetCurrentState();
		/// <summary>
		/// Saves the current state of the object to an XML node.
		/// <b>NOTE:</b>
		/// Unlike the PersistChanges method, this method simply
		/// saves a snapshot of the object's current state to the
		/// XML node, without affecting it's loaded state.
		/// </summary>
		//void GetCurrentState(ref XmlNode saveToNode);
		/// <summary>
		/// Sets current state of the object, loading the state
		/// from an XML node.
		/// <b>NOTE:</b>
		/// Unlike the LoadXml method, this method simply
		/// sets the object's current state, without affecting 
		/// it's loaded state.
		/// </summary>
		//void SetCurrentState(XmlNode node);
		/// <summary>
		/// Sets current state of the object, loading the state
		/// from an XML string.
		/// <b>NOTE:</b>
		/// Unlike the LoadXml method, this method simply
		/// sets the object's current state, without affecting 
		/// it's loaded state.
		/// </summary>
		//void SetCurrentState(string xml);
		
		// Rename to IXmlNode or IXmlObject
		// Should implement IUndoable
		
		XmlNode LoadedXmlNode { get; }
		bool IsDirty { get; }
		void LoadXml(XmlNode node);
		void PersistChanges(XmlNode node);
	}
}
