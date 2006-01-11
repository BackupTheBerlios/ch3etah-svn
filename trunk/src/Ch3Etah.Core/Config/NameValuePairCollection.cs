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
 */

using System;
using System.ComponentModel;

namespace Ch3Etah.Core.Config 
{
	/// <summary>
	/// Description of NameValuePairCollection.	
	/// </summary>
	public class NameValuePairCollection : Generated.NameValuePairCollection, ICloneable, IBindingList 
	{
		
		new public bool Contains(NameValuePair pair) 
		{
			return (GetNameValuePair(pair.Name) != null);
		}

		[Browsable(false)]
		new public int Count 
		{
			get { return base.Count; }
		}

		public NameValuePair Add(string name, string value) 
		{
			NameValuePair newItem = new NameValuePair();
			newItem.Name = name;
			newItem.Value = value;
			Add(newItem);
			return newItem;
		}

		public bool Contains(string name) 
		{
			return (GetNameValuePair(name) != null);
		}

		public NameValuePair GetNameValuePair(string name) 
		{
			foreach (NameValuePair pair in List) 
			{
				if (pair.Name == name) 
				{
					return pair;
				}
			}
			return null;
		}

		#region ICloneable implementation

		object ICloneable.Clone() 
		{
			return Clone();
		}

		public NameValuePairCollection Clone() 
		{
			NameValuePairCollection clone = new NameValuePairCollection();
			foreach (NameValuePair pair in List) 
			{
				clone.Add(pair.Name, pair.Value);
			}
			return clone;
		}

		#endregion ICloneable implementation

		#region ListChanged handling

		private void RemoveChild(object sender, RemoveNameValuePairEventArgs e) 
		{
			Remove(e.NameValuePair);
		}

		protected override void OnClearComplete() 
		{
			try 
			{
				ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
			}
			catch {}
		}

		protected override void OnInsertComplete(int index, object value) 
		{
			((NameValuePair) value).RemoveMe += new RemoveNameValuePairEventHandler(RemoveChild);
			try 
			{
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			catch {}
		}

		protected override void OnRemoveComplete(int index, object value) 
		{
			try 
			{
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			catch {}
		}

		protected override void OnSetComplete(int index, object oldValue, object newValue) 
		{
			try 
			{
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
			catch {}
		}

		#endregion ListChanged handling

		#region IBindingList implementation

		public event ListChangedEventHandler ListChanged; //(object sender, ListChangedEventArgs e);

		bool IBindingList.SupportsChangeNotification 
		{
			get { return true; }
		}

		bool IBindingList.AllowNew 
		{
			get { return false; }
		}

		bool IBindingList.AllowEdit 
		{
			get { return true; }
		}

		bool IBindingList.AllowRemove 
		{
			get { return true; }
		}

		bool IBindingList.SupportsSearching 
		{
			get { return false; }
		}

		bool IBindingList.SupportsSorting 
		{
			get { return false; }
		}

		bool IBindingList.IsSorted 
		{
			get { return false; }
		}

		PropertyDescriptor IBindingList.SortProperty 
		{
			get { return null; }
		}

		ListSortDirection IBindingList.SortDirection 
		{
			get { return ListSortDirection.Ascending; }
		}

		void IBindingList.RemoveSort() {}

		void IBindingList.RemoveIndex(PropertyDescriptor property) {}

		int IBindingList.Find(PropertyDescriptor property, object key) 
		{
			return 0;
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) {}

		void IBindingList.AddIndex(PropertyDescriptor property) {}

		public object AddNew() 
		{
			NameValuePair newItem = new NameValuePair();
			newItem.IsNew = true;
			Add(newItem);
			return newItem;
		}

		#endregion IBindingList implementation
	}

}