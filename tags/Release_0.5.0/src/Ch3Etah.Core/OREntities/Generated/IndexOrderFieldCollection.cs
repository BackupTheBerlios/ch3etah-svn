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

using System;
using System.Collections;

namespace Ch3Etah.Metadata.OREntities.Generated {
	public abstract class IndexOrderFieldCollection : CollectionBase {
		
		#region Collection Implementation
		public IndexOrderFieldCollection() {
		}
		
		public IndexOrderFieldCollection(IndexOrderFieldCollection val) {
			this.AddRange(val);
		}
		
		public IndexOrderFieldCollection(Ch3Etah.Metadata.OREntities.IndexOrderField[] val) {
			this.AddRange(val);
		}
		
		public Ch3Etah.Metadata.OREntities.IndexOrderField this[int index] {
			get {
				return ((Ch3Etah.Metadata.OREntities.IndexOrderField)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		public int Add(Ch3Etah.Metadata.OREntities.IndexOrderField val){
			return List.Add(val);
		}
		
		public void AddRange(Ch3Etah.Metadata.OREntities.IndexOrderField[] val) {
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		public void AddRange(IndexOrderFieldCollection val) {
			for (int i = 0; i < val.Count; i++) {
				this.Add(val[i]);
			}
		}
		
		public bool Contains(Ch3Etah.Metadata.OREntities.IndexOrderField val) {
			return List.Contains(val);
		}
		
		public void CopyTo(Ch3Etah.Metadata.OREntities.IndexOrderField[] array, int index) {
			List.CopyTo(array, index);
		}
		
		public int IndexOf(Ch3Etah.Metadata.OREntities.IndexOrderField val) {
			return List.IndexOf(val);
		}
		
		public void Insert(int index, Ch3Etah.Metadata.OREntities.IndexOrderField val) {
			List.Insert(index, val);
		}
		
		public new IndexOrderFieldCollectionEnumerator GetEnumerator() {
			return new IndexOrderFieldCollectionEnumerator(this);
		}
		
		public void Remove(Ch3Etah.Metadata.OREntities.IndexOrderField val) {
			List.Remove(val);
		}
		
		#endregion Collection Implementation
		
		#region Enumerator Class
		public class IndexOrderFieldCollectionEnumerator : IEnumerator {
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			public IndexOrderFieldCollectionEnumerator(IndexOrderFieldCollection mappings) {
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			public Ch3Etah.Metadata.OREntities.IndexOrderField Current {
				get {
					return ((Ch3Etah.Metadata.OREntities.IndexOrderField)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
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


