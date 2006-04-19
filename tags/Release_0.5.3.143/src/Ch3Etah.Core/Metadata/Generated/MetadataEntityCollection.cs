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
 *   Date: 22/9/2004
 */

//========================================================================
// THIS FILE HAS BEEN AUTO-GENERATED USING SHARP DEVELOP
// DO NOT EDIT!!!!!
//========================================================================

using System;
using System.Collections;

namespace Ch3Etah.Core.Metadata.Generated
{
	/// <summary>
	///     <para>
	///       A collection that stores <see cref='IMetadataEntity'/> objects.
	///    </para>
	/// </summary>
	/// <seealso cref='MetadataEntityCollection'/>
	[Serializable()]
	public abstract class MetadataEntityCollection : CollectionBase {
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='MetadataEntityCollection'/>.
		///    </para>
		/// </summary>
		public MetadataEntityCollection()
		{
		}
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='MetadataEntityCollection'/> based on another <see cref='MetadataEntityCollection'/>.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///       A <see cref='MetadataEntityCollection'/> from which the contents are copied
		/// </param>
		public MetadataEntityCollection(MetadataEntityCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='MetadataEntityCollection'/> containing any array of <see cref='IMetadataEntity'/> objects.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///       A array of <see cref='IMetadataEntity'/> objects with which to intialize the collection
		/// </param>
		public MetadataEntityCollection(IMetadataEntity[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// <para>Represents the entry at the specified index of the <see cref='IMetadataEntity'/>.</para>
		/// </summary>
		/// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
		/// <value>
		///    <para> The entry at the specified index of the collection.</para>
		/// </value>
		/// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		public IMetadataEntity this[int index] {
			get {
				return ((IMetadataEntity)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///    <para>Adds a <see cref='IMetadataEntity'/> with the specified value to the 
		///    <see cref='MetadataEntityCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='IMetadataEntity'/> to add.</param>
		/// <returns>
		///    <para>The index at which the new element was inserted.</para>
		/// </returns>
		/// <seealso cref='MetadataEntityCollection.AddRange'/>
		public int Add(IMetadataEntity val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// <para>Copies the elements of an array to the end of the <see cref='MetadataEntityCollection'/>.</para>
		/// </summary>
		/// <param name='value'>
		///    An array of type <see cref='IMetadataEntity'/> containing the objects to add to the collection.
		/// </param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <seealso cref='MetadataEntityCollection.Add'/>
		public void AddRange(IMetadataEntity[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///     <para>
		///       Adds the contents of another <see cref='MetadataEntityCollection'/> to the end of the collection.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///    A <see cref='MetadataEntityCollection'/> containing the objects to add to the collection.
		/// </param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <seealso cref='MetadataEntityCollection.Add'/>
		public void AddRange(MetadataEntityCollection val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// <para>Gets a value indicating whether the 
		///    <see cref='MetadataEntityCollection'/> contains the specified <see cref='IMetadataEntity'/>.</para>
		/// </summary>
		/// <param name='value'>The <see cref='IMetadataEntity'/> to locate.</param>
		/// <returns>
		/// <para><see langword='true'/> if the <see cref='IMetadataEntity'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.</para>
		/// </returns>
		/// <seealso cref='MetadataEntityCollection.IndexOf'/>
		public bool Contains(IMetadataEntity val)
		{
			return List.Contains(val);
		}
		
		/// <summary>
		/// <para>Copies the <see cref='MetadataEntityCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
		///    specified index.</para>
		/// </summary>
		/// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='MetadataEntityCollection'/> .</para></param>
		/// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='MetadataEntityCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
		/// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
		/// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
		/// <seealso cref='System.Array'/>
		public void CopyTo(IMetadataEntity[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		///    <para>Returns the index of a <see cref='IMetadataEntity'/> in 
		///       the <see cref='MetadataEntityCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='IMetadataEntity'/> to locate.</param>
		/// <returns>
		/// <para>The index of the <see cref='IMetadataEntity'/> of <paramref name='value'/> in the 
		/// <see cref='MetadataEntityCollection'/>, if found; otherwise, -1.</para>
		/// </returns>
		/// <seealso cref='MetadataEntityCollection.Contains'/>
		public int IndexOf(IMetadataEntity val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// <para>Inserts a <see cref='IMetadataEntity'/> into the <see cref='MetadataEntityCollection'/> at the specified index.</para>
		/// </summary>
		/// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
		/// <param name=' value'>The <see cref='IMetadataEntity'/> to insert.</param>
		/// <returns><para>None.</para></returns>
		/// <seealso cref='MetadataEntityCollection.Add'/>
		public void Insert(int index, IMetadataEntity val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		///    <para>Returns an enumerator that can iterate through 
		///       the <see cref='MetadataEntityCollection'/> .</para>
		/// </summary>
		/// <returns><para>None.</para></returns>
		/// <seealso cref='System.Collections.IEnumerator'/>
		public new IMetadataEntityEnumerator GetEnumerator()
		{
			return new IMetadataEntityEnumerator(this);
		}
		
		/// <summary>
		///    <para> Removes a specific <see cref='IMetadataEntity'/> from the 
		///    <see cref='MetadataEntityCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='IMetadataEntity'/> to remove from the <see cref='MetadataEntityCollection'/> .</param>
		/// <returns><para>None.</para></returns>
		/// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
		public void Remove(IMetadataEntity val)
		{
			List.Remove(val);
		}
		
		public class IMetadataEntityEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			public IMetadataEntityEnumerator(MetadataEntityCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			public IMetadataEntity Current {
				get {
					return ((IMetadataEntity)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			bool IEnumerator.MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			public void Reset()
			{
				baseEnumerator.Reset();
			}
			
			void IEnumerator.Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}
