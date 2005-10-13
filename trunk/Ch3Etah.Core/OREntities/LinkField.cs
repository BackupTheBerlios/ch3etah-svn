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
 *   Date: 23/12/2004
 */

using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using Ch3Etah.Core.Metadata;

namespace Ch3Etah.Metadata.OREntities
{
	/// <summary>
	/// Represents a Field on a Link.
	/// </summary>
	public class LinkField : MetadataNodeBase
	{
		
		#region Member Variables
		private string _sourceFieldName = "";
		private string _targetFieldName = "";
		#endregion Member Variables
		
		#region Link
		private Link _link;
		
		[XmlIgnore()]
		public Link Link {
			get { return _link; }
		}
		internal void SetLink(Link link) {
			Debug.Assert(link != null, "Link parameter should not be null.");
			_link = link;
		}
		#endregion Link
		
		#region Properties
		[XmlAttribute("source")]
		[TypeConverter("Ch3Etah.Design.Converters.FieldsNameConverter,Ch3Etah.Design")]
		public string SourceFieldName {
			get { return _sourceFieldName; }
			set { _sourceFieldName = value; }
		}
		
		[XmlAttribute("target")]
		[TypeConverter("Ch3Etah.Design.Converters.TargetEntityFieldsNamConverter,Ch3Etah.Design")]
		public string TargetFieldName 
		{
			get { return _targetFieldName; }
			set { _targetFieldName = value; }
		}
		#endregion Properties
		
		public override string ToString() {
			return this.TargetFieldName;
		}

		public override string Name {
			get { return ToString(); }
			set {  }
		}
	}
}
