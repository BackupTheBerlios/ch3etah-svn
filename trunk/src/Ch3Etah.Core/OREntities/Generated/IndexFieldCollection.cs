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

namespace Ch3Etah.Metadata.OREntities.Generated
{
	public abstract class IndexFieldCollection : CollectionBase
	{
		
		#region Collection Implementation
		public IndexFieldCollection()
		{
		}
		
		public IndexFieldCollection(IndexFieldCollection val) {
			this.AddRange(val);
		}
		
		public IndexFieldCollection(Ch3Etah.Metadata.OREntities.IndexField[] val) {
			this.AddRange(val);
		}
		
		public Ch3Etah.Metadata.OREntities.IndexField this[int index] {
			get {
				return ((Ch3Etah.Metadata.OREntities.IndexField)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		public int Add(Ch3Etah.Metadata.OREntities.IndexField val){
			return List.Add(val);
		}
		
		public void AddRange(Ch3Etah.Metadata.OREntities.IndexField[] val) {
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		public void AddRange(IndexFieldCollection val) {
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		public bool Contains(Ch3Etah.Metadata.OREntities.IndexField val) {
			return List.Contains(val);
		}
		
		public void CopyTo(Ch3Etah.Metadata.OREntities.IndexField[] array, int index) {
			List.CopyTo(array, index);
		}
		
		public int IndexOf(Ch3Etah.Metadata.OREntities.IndexField val) {
			return List.IndexOf(val);
		}
		
		public void Insert(int index, Ch3Etah.Metadata.OREntities.IndexField val) {
			List.Insert(index, val);
		}
		
		public new IndexFieldCollectionEnumerator GetEnumerator() {
			return new IndexFieldCollectionEnumerator(this);
		}
		
		public void Remove(Ch3Etah.Metadata.OREntities.IndexField val) {
			List.Remove(val);
		}
		
		#endregion Collection Implementation
		
		#region Enumerator Class
		public class IndexFieldCollectionEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			public IndexFieldCollectionEnumerator(IndexFieldCollection mappings)
			{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
			}
			
			public Ch3Etah.Metadata.OREntities.IndexField Current {
				get {
					return ((Ch3Etah.Metadata.OREntities.IndexField)(baseEnumerator.Current));
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


