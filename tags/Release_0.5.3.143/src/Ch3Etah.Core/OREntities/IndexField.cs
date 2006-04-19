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
	
	#region IndexFieldNameChangedEvent
	public delegate void IndexFieldNameChangedEventHandler(object sender, IndexFieldNameChangedEventArgs e);

	public class IndexFieldNameChangedEventArgs
	{
		public readonly IndexField IndexField;
		public readonly string OldName;
		public readonly string NewName;
		public IndexFieldNameChangedEventArgs(IndexField field, string oldName, string newName)
		{
			this.IndexField = field;
			this.NewName = newName;
			this.OldName = oldName;
		}
	}
	#endregion IndexFieldNameChangedEvent

	/// <summary>
	/// Represents a Field on an Index.
	/// </summary>
	public class IndexField : MetadataNodeBase
	{
		
		#region IndexFieldNameChangedEvent
		public static event IndexFieldNameChangedEventHandler NameChanged;

		private void DoNameChanged(string oldName, string newName)
		{
			if (NameChanged != null && oldName != "" && newName != "")
			{
				NameChanged(this, new IndexFieldNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}

		#endregion IndexFieldNameChangedEvent
		
		#region Member Variables
		private string _name = "";
		private string _parameterName = "";
		private bool _partialTextMatch = false;
		#endregion Member Variables
		
		#region Index
		private Index _index;
		
		[XmlIgnore()]
		public Index Index {
			get { return _index; }
		}
		internal void SetIndex(Index index) {
			Debug.Assert(index != null, "Index parameter should not be null.");
			_index = index;
		}
		#endregion Index
		
		#region Properties
		[XmlAttribute("name")]
		public override string Name {
			get {
				if (_name == "" && _parameterName != "") {
					return _parameterName;
				}
				else {
					return _name;
				}
			}
			set
			{
				DoNameChanged(_name, value);
				_name = value;
			}
		}
		
		[XmlAttribute("parametername")]
		[TypeConverter("Ch3Etah.Design.Converters.FieldsNameConverter,Ch3Etah.Design")]
		public string ParameterName {
			get {
				if (_parameterName == "" && _name != "") {
					return _name;
				}
				else {
					return _parameterName;
				}
			}
			set { _parameterName = value; }
		}
		
		[XmlAttribute("partialtextmatch")]
		public bool PartialTextMatch {
			get { return _partialTextMatch; }
			set { _partialTextMatch = value; }
		}
		#endregion Properties
		
		public EntityField EntityField
		{
			get
			{
				if (this.Index == null || this.Index.Entity == null)
					return null;

				EntityField field = this.Index.Entity.Fields
					.GetFieldFromName(this.Name);
				if (field != null)
					return field;
				else
					throw new NullReferenceException(string.Format(
						"The field '{0}' is defined in the index '{1}', but no matching field with that name was found in the entity '{2}'.",
						this.Name,
						this.Index.Name,
						this.Index.Entity.Name));
			}
		}

		public override string ToString() {
			return this.Name;
		}
	}
}
