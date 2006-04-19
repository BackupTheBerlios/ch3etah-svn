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
 *   Date: 24/12/2004
 */

using System;
using System.Collections;
using System.Xml.Serialization;

namespace Ch3Etah.Metadata.OREntities {
	/// <summary>
	/// Description of EntityFieldCollection.
	/// </summary>
	public class EntityFieldCollection : Generated.EntityFieldCollection {
		#region Entity

		private Entity _entity;

		[XmlIgnore()]
		internal Entity Entity {
			get { return _entity; }
			set {
				_entity = value;
				foreach (EntityField val in List) {
					val.SetEntity(_entity);
				}
			}
		}

		#endregion Entity

		#region Overridden properties and methods

		new public int Add(EntityField val) {
			if (_entity != null)
				val.SetEntity(_entity);
			if (!Contains(val))
				return base.Add(val);
			return -1;
		}

		new public void Insert(int index, EntityField val) {
			if (_entity != null)
				val.SetEntity(_entity);
			if (Contains(val))
				base.Remove(val);
			if (index < 0) index = 0;
			if (index > List.Count) index = List.Count - 1;
			base.Insert(index, val);
		}

		new public EntityFieldCollectionEnumerator GetEnumerator() {
			EntityFieldCollectionEnumerator e = new EntityFieldCollectionEnumerator(this);
			e.Entity = Entity;
			return e;
		}

		new public EntityField this[int index] {
			get {
				if (_entity != null)
					((EntityField) (List[index])).SetEntity(_entity);
				return ((EntityField) (List[index]));
			}
			set {
				List[index] = (EntityField) value;
				if (_entity != null)
					((EntityField) (List[index])).SetEntity(_entity);
			}
		}

		#endregion Overridden properties and methods

		#region EntityFieldCollectionEnumerator class

		new public class EntityFieldCollectionEnumerator
			: Generated.EntityFieldCollection.EntityFieldCollectionEnumerator, IEnumerator {
			public EntityFieldCollectionEnumerator(EntityFieldCollection mappings) : base(mappings) {}

			#region Entity

			private Entity _entity;

			[XmlIgnore()]
			internal Entity Entity {
				get { return _entity; }
				set { _entity = value; }
			}

			#endregion Entity

			new public EntityField Current {
				get {
					if (_entity != null)
						base.Current.SetEntity(_entity);
					return base.Current;
				}
			}

			object IEnumerator.Current {
				get {
					if (_entity != null)
						base.Current.SetEntity(_entity);
					return base.Current;
				}
			}
			}

		#endregion EntityFieldCollectionEnumerator class

		public EntityField GetFieldFromName(String fieldName) {
			foreach (EntityField tmpField in this) {
				if (tmpField.Name.Equals(fieldName))
					return tmpField;
			}

			return null;
		}

		public EntityField GetFieldFromDBColumn(String fieldName) {
			foreach (EntityField tmpField in this) {
				if (tmpField.DBColumn.Equals(fieldName))
					return tmpField;
			}

			return null;
		}

	}
}
