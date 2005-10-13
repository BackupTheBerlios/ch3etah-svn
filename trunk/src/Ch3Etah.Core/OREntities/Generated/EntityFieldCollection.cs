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
 *   File Generated using CH3ETAH.
 *   Date: 24/12/2004 11:10:25
 */

using System.Collections;
using Ch3Etah.Core.ComponentModel;

namespace Ch3Etah.Metadata.OREntities.Generated {
	public abstract class EntityFieldCollection : ComponentCollection {
		#region Collection Implementation

		public EntityFieldCollection() {}

		public EntityFieldCollection(EntityFieldCollection val) {
			AddRange(val);
		}

		public EntityFieldCollection(EntityField[] val) {
			AddRange(val);
		}

		public EntityField this[int index] {
			get { return ((EntityField) (List[index])); }
			set { List[index] = value; }
		}

		public int Add(EntityField val) {
			return List.Add(val);
		}

		public void AddRange(EntityField[] val) {
			for (int i = 0; i < val.Length; i++) {
				Add(val[i]);
			}
		}

		public void AddRange(EntityFieldCollection val) {
			for (int i = 0; i < val.Count; i++) {
				Add(val[i]);
			}
		}

		public bool Contains(EntityField val) {
			return List.Contains(val);
		}

		public void CopyTo(EntityField[] array, int index) {
			List.CopyTo(array, index);
		}

		public int IndexOf(EntityField val) {
			return List.IndexOf(val);
		}

		public void Insert(int index, EntityField val) {
			List.Insert(index, val);
		}

		new public EntityFieldCollectionEnumerator GetEnumerator() {
			return new EntityFieldCollectionEnumerator(this);
		}

		public void Remove(EntityField val) {
			List.Remove(val);
		}

		#endregion Collection Implementation

		#region Enumerator Class

		public class EntityFieldCollectionEnumerator : IEnumerator {
			private IEnumerator baseEnumerator;
			private IEnumerable temp;

			public EntityFieldCollectionEnumerator(EntityFieldCollection mappings) {
				temp = ((IEnumerable) (mappings));
				baseEnumerator = temp.GetEnumerator();
			}

			public EntityField Current {
				get { return ((EntityField) (baseEnumerator.Current)); }
			}

			object IEnumerator.Current {
				get { return baseEnumerator.Current; }
			}

			public bool MoveNext() {
				return baseEnumerator.MoveNext();
			}

			bool IEnumerator.MoveNext() {
				return baseEnumerator.MoveNext();
			}

			public void Reset() {
				baseEnumerator.Reset();
			}

			void IEnumerator.Reset() {
				baseEnumerator.Reset();
			}
		}

		#endregion Enumerator Class
	}
}