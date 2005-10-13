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
 *   Date: 9/7/2004
 *   Time: 11:06 PM
 */

//========================================================================
// THIS FILE HAS BEEN AUTO-GENERATED USING SHARP DEVELOP
// DO NOT EDIT!!!!!
//========================================================================

using System;
using System.Collections;
using System.Xml.Serialization;

namespace Ch3Etah.Core.ProjectLib.Generated
{
	/// <summary>
	///     <para>
	///       A collection that stores <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> objects.
	///    </para>
	/// </summary>
	/// <seealso cref='GeneratorCommandCollection'/>
	[Serializable()]
	public abstract class GeneratorCommandCollection : CollectionBase {
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='GeneratorCommandCollection'/>.
		///    </para>
		/// </summary>
		public GeneratorCommandCollection()
		{
		}
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='GeneratorCommandCollection'/> based on another <see cref='GeneratorCommandCollection'/>.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///       A <see cref='GeneratorCommandCollection'/> from which the contents are copied
		/// </param>
		public GeneratorCommandCollection(GeneratorCommandCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='GeneratorCommandCollection'/> containing any array of <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> objects.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///       A array of <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> objects with which to intialize the collection
		/// </param>
		public GeneratorCommandCollection(Ch3Etah.Core.ProjectLib.GeneratorCommand[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// <para>Represents the entry at the specified index of the <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/>.</para>
		/// </summary>
		/// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
		/// <value>
		///    <para> The entry at the specified index of the collection.</para>
		/// </value>
		/// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		public Ch3Etah.Core.ProjectLib.GeneratorCommand this[int index] {
			get {
				return ((Ch3Etah.Core.ProjectLib.GeneratorCommand)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///    <para>Adds a <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> with the specified value to the 
		///    <see cref='GeneratorCommandCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> to add.</param>
		/// <returns>
		///    <para>The index at which the new element was inserted.</para>
		/// </returns>
		/// <seealso cref='GeneratorCommandCollection.AddRange'/>
		public int Add(Ch3Etah.Core.ProjectLib.GeneratorCommand val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// <para>Copies the elements of an array to the end of the <see cref='GeneratorCommandCollection'/>.</para>
		/// </summary>
		/// <param name='value'>
		///    An array of type <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> containing the objects to add to the collection.
		/// </param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <seealso cref='GeneratorCommandCollection.Add'/>
		public void AddRange(Ch3Etah.Core.ProjectLib.GeneratorCommand[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///     <para>
		///       Adds the contents of another <see cref='GeneratorCommandCollection'/> to the end of the collection.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///    A <see cref='GeneratorCommandCollection'/> containing the objects to add to the collection.
		/// </param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <seealso cref='GeneratorCommandCollection.Add'/>
		public void AddRange(GeneratorCommandCollection val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// <para>Gets a value indicating whether the 
		///    <see cref='GeneratorCommandCollection'/> contains the specified <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/>.</para>
		/// </summary>
		/// <param name='value'>The <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> to locate.</param>
		/// <returns>
		/// <para><see langword='true'/> if the <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.</para>
		/// </returns>
		/// <seealso cref='GeneratorCommandCollection.IndexOf'/>
		public bool Contains(Ch3Etah.Core.ProjectLib.GeneratorCommand val)
		{
			return List.Contains(val);
		}
		
		/// <summary>
		/// <para>Copies the <see cref='GeneratorCommandCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
		///    specified index.</para>
		/// </summary>
		/// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='GeneratorCommandCollection'/> .</para></param>
		/// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='GeneratorCommandCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
		/// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
		/// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
		/// <seealso cref='System.Array'/>
		public void CopyTo(Ch3Etah.Core.ProjectLib.GeneratorCommand[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		///    <para>Returns the index of a <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> in 
		///       the <see cref='GeneratorCommandCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> to locate.</param>
		/// <returns>
		/// <para>The index of the <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> of <paramref name='value'/> in the 
		/// <see cref='GeneratorCommandCollection'/>, if found; otherwise, -1.</para>
		/// </returns>
		/// <seealso cref='GeneratorCommandCollection.Contains'/>
		public int IndexOf(Ch3Etah.Core.ProjectLib.GeneratorCommand val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// <para>Inserts a <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> into the <see cref='GeneratorCommandCollection'/> at the specified index.</para>
		/// </summary>
		/// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
		/// <param name=' value'>The <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> to insert.</param>
		/// <returns><para>None.</para></returns>
		/// <seealso cref='GeneratorCommandCollection.Add'/>
		public void Insert(int index, Ch3Etah.Core.ProjectLib.GeneratorCommand val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		///    <para>Returns an enumerator that can iterate through 
		///       the <see cref='GeneratorCommandCollection'/> .</para>
		/// </summary>
		/// <returns><para>None.</para></returns>
		/// <seealso cref='System.Collections.IEnumerator'/>
		public new GeneratorCommandEnumerator GetEnumerator()
		{
			return new GeneratorCommandEnumerator(this);
		}
		
		/// <summary>
		///    <para> Removes a specific <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> from the 
		///    <see cref='GeneratorCommandCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='Ch3Etah.Core.ProjectLib.GeneratorCommand'/> to remove from the <see cref='GeneratorCommandCollection'/> .</param>
		/// <returns><para>None.</para></returns>
		/// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
		public void Remove(Ch3Etah.Core.ProjectLib.GeneratorCommand val)
		{
			List.Remove(val);
		}
		
		public class GeneratorCommandEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			public GeneratorCommandEnumerator(GeneratorCommandCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			public Ch3Etah.Core.ProjectLib.GeneratorCommand Current {
				get {
					return ((Ch3Etah.Core.ProjectLib.GeneratorCommand)(baseEnumerator.Current));
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
