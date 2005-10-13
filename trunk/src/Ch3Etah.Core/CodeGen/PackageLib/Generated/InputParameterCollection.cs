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
 *   Date: 11/28/2004
 */


using System;
using System.Collections;

namespace Ch3Etah.Core.CodeGen.PackageLib.Generated {
	public class InputParameterCollection : CollectionBase {
		
		#region Collection Implementation
		public InputParameterCollection()
		{
		}
		
		public InputParameterCollection(InputParameterCollection val) {
			this.AddRange(val);
		}
		
		public InputParameterCollection(Ch3Etah.Core.CodeGen.PackageLib.InputParameter[] val) {
			this.AddRange(val);
		}
		
		public Ch3Etah.Core.CodeGen.PackageLib.InputParameter this[int index] {
			get {
				return ((Ch3Etah.Core.CodeGen.PackageLib.InputParameter)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		public int Add(Ch3Etah.Core.CodeGen.PackageLib.InputParameter val){
			return List.Add(val);
		}
		
		public void AddRange(Ch3Etah.Core.CodeGen.PackageLib.InputParameter[] val) {
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		public void AddRange(InputParameterCollection val) {
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		public bool Contains(Ch3Etah.Core.CodeGen.PackageLib.InputParameter val) {
			return List.Contains(val);
		}
		
		public void CopyTo(Ch3Etah.Core.CodeGen.PackageLib.InputParameter[] array, int index) {
			List.CopyTo(array, index);
		}
		
		public int IndexOf(Ch3Etah.Core.CodeGen.PackageLib.InputParameter val) {
			return List.IndexOf(val);
		}
		
		public void Insert(int index, Ch3Etah.Core.CodeGen.PackageLib.InputParameter val) {
			List.Insert(index, val);
		}
		
		public new InputParameterCollectionEnumerator GetEnumerator() {
			return new InputParameterCollectionEnumerator(this);
		}
		
		public void Remove(Ch3Etah.Core.CodeGen.PackageLib.InputParameter val) {
			List.Remove(val);
		}
		
		#endregion Collection Implementation
		
		#region Enumerator Class
		public class InputParameterCollectionEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			public InputParameterCollectionEnumerator(InputParameterCollection mappings)
			{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
			}
			
			public Ch3Etah.Core.CodeGen.PackageLib.InputParameter Current {
				get {
					return ((Ch3Etah.Core.CodeGen.PackageLib.InputParameter)(baseEnumerator.Current));
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


