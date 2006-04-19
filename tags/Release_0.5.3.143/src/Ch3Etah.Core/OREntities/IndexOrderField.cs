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

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Ch3Etah.Core.Metadata;

namespace Ch3Etah.Metadata.OREntities
{
	/// <summary>
	/// Represents a field in an index that will be used
	/// to order the result set.
	/// </summary>
	public class IndexOrderField : MetadataNodeBase
	{
		
		private Index _index;
		private string _name = "";

		public enum eDirection: short
		{
			Asc = 0,
			Desc = 1
		}
		private eDirection _direction = eDirection.Asc;
		
		[XmlIgnore()]
		public Index Index {
			get { return _index; }
		}
		internal void SetIndex(Index index) {
			Debug.Assert(index != null, "Index parameter should not be null.");
			_index = index;
		}
		
		[XmlAttribute("name")]
		[TypeConverter("Ch3Etah.Design.Converters.IndexFieldsNameConverter,Ch3Etah.Design")]
		public override string Name {
			get { return _name; }
			set { _name = value; }
		}

		[XmlAttribute("direction")]
		[TypeConverter("Ch3Etah.Design.Converters.OrderByDirectionConverter,Ch3Etah.Design")]
		public string Direction
		{
			get
			{
				return this._direction.ToString();
			}
			set 
			{ 
				this._direction = (eDirection) Enum.Parse(typeof(eDirection), value, true);
			}
		}

		public override string ToString() {
			return this.Name;
		}

	}
}
