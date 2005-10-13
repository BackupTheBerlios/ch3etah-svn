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
 *   Date: 9/11/2004
 *   Time: 10:40 AM
 */

using System;
using System.ComponentModel;

namespace Ch3Etah.Core.ProjectLib {
	/// <summary>
	/// Description of InputParameterCollection.	
	/// </summary>
	public class InputParameterCollection : Generated.InputParameterCollection, ICloneable, IBindingList {
		
		new public bool Contains(InputParameter parameter) {
			return (GetInputParameter(parameter.Name) != null);
		}

		[Browsable(false)]
		new public int Count {
			get { return base.Count; }
		}

		public InputParameter Add(string name, string value) {
			InputParameter newItem = new InputParameter();
			newItem.Name = name;
			newItem.Value = value;
			Add(newItem);
			return newItem;
		}

		public bool Contains(string name) {
			return (GetInputParameter(name) != null);
		}

		public InputParameter GetInputParameter(string name) {
			foreach (InputParameter parameter in List) {
				if (parameter.Name == name) {
					return parameter;
				}
			}
			return null;
		}

		#region ICloneable implementation

		object ICloneable.Clone() {
			return Clone();
		}

		public InputParameterCollection Clone() {
			InputParameterCollection clone = new InputParameterCollection();
			foreach (InputParameter parameter in List) {
				clone.Add(parameter.Name, parameter.Value);
			}
			return clone;
		}

		#endregion ICloneable implementation

		#region ListChanged handling

		private void RemoveChild(object sender, RemoveInputParameterEventArgs e) {
			Remove(e.InputParameter);
		}

		protected override void OnClearComplete() {
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
			}
			catch {}
		}

		protected override void OnInsertComplete(int index, object value) {
			((InputParameter) value).RemoveMe += new RemoveInputParameterEventHandler(RemoveChild);
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			catch {}
		}

		protected override void OnRemoveComplete(int index, object value) {
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			catch {}
		}

		protected override void OnSetComplete(int index, object oldValue, object newValue) {
			try {
				ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
			catch {}
		}

		#endregion ListChanged handling

		#region IBindingList implementation

		public event ListChangedEventHandler ListChanged; //(object sender, ListChangedEventArgs e);

		bool IBindingList.SupportsChangeNotification {
			get { return true; }
		}

		bool IBindingList.AllowNew {
			get { return false; }
		}

		bool IBindingList.AllowEdit {
			get { return true; }
		}

		bool IBindingList.AllowRemove {
			get { return true; }
		}

		bool IBindingList.SupportsSearching {
			get { return false; }
		}

		bool IBindingList.SupportsSorting {
			get { return false; }
		}

		bool IBindingList.IsSorted {
			get { return false; }
		}

		PropertyDescriptor IBindingList.SortProperty {
			get { return null; }
		}

		ListSortDirection IBindingList.SortDirection {
			get { return ListSortDirection.Ascending; }
		}

		void IBindingList.RemoveSort() {}

		void IBindingList.RemoveIndex(PropertyDescriptor property) {}

		int IBindingList.Find(PropertyDescriptor property, object key) {
			return 0;
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) {}

		void IBindingList.AddIndex(PropertyDescriptor property) {}

		public object AddNew() {
			InputParameter newItem = new InputParameter();
			newItem.IsNew = true;
			Add(newItem);
			return newItem;
		}

		#endregion IBindingList implementation
	}

}