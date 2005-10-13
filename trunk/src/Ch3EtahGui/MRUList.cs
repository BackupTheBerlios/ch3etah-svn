using System;
using System.Collections;
using System.IO;
using Microsoft.Win32;

namespace Ch3Etah.Gui {
	/// <summary>
	/// Summary description for MRUList.
	/// </summary>
	public class MRUList : ICollection, IDisposable {
		#region Constructors and member variables

		protected const int MAX_MRULIST_ITEMS = 12;
		
		private const string REGISTRY_PATH = "Software\\Ch3Etah\\MRUList";
		private MRUEntry[] list = new MRUEntry[MAX_MRULIST_ITEMS];

		public MRUList() {
			Refresh();
		}

		#endregion

		#region Load and Save

		public void Refresh() {
			RegistryKey baseKey = GetKey(REGISTRY_PATH, Registry.CurrentUser);

			for (int c = 0; c < MAX_MRULIST_ITEMS; c++) {
				string path = baseKey.GetValue(string.Format("entry{0}", c)) as string;
				if (File.Exists(path)) {
					list[c].Rank = c;
					list[c].Path = path;
				}
				else {
					list[c].Rank = MAX_MRULIST_ITEMS;
				}
			}

			baseKey.Close();

		}

		public void Save() {
			RegistryKey baseKey = GetKey(REGISTRY_PATH, Registry.CurrentUser);

			for (int c = 0; c < MAX_MRULIST_ITEMS; c++) {
				string valueName = string.Format("entry{0}", c);
				if (list[c].Path != null) 
					baseKey.SetValue(valueName, list[c].Path);
				else if (baseKey.GetValue(valueName) != null)
					baseKey.DeleteValue(valueName);
			}

			baseKey.Close();
		}

		private RegistryKey GetKey(string keyName, RegistryKey parentKey) {
			RegistryKey key = parentKey.OpenSubKey(keyName, true);
			if (key == null)
				key = parentKey.CreateSubKey(keyName);
			return key;
		}

		#endregion

		#region Add and Remove

		public void Add(string path) 
		{
			
			int pos = -1;
			int len = list.Length;
			MRUEntry entry;
			
			for (int c = 0; c < len; c++) {
				if (list[c].Path != null && (string.Compare(list[c].Path, path, true) == 0)) {
					pos = c;
					break;
				}
			}
			
			if (pos >= 0) {
				// Item already in list - promote it
				entry = list[pos];
				entry.Rank = 0;
				int current = 1;
				for (int c = 0; c < MAX_MRULIST_ITEMS; c++) {
					if (c != pos) {
						list[c].Rank = current++;
					}
				}
			}
			else {
				// Add to list
				list[len-1].Rank = 0;
				list[len-1].Path = path;
				for (int c = 0; c < MAX_MRULIST_ITEMS; c++) 
				{
					list[c].Rank = c+1;
				}
			}
			Array.Sort(list, ListComparer.DefaultInstance);
		}

		public void Remove(string path) {

			int pos;
			
			if ((pos = Array.BinarySearch(list, path, ListComparer.DefaultInstance)) >= 0) 
			{
				// Remove item
				MRUEntry entry = list[pos];
				entry.Rank = MAX_MRULIST_ITEMS;
				entry.Path = null;
			}
			Array.Sort(list, ListComparer.DefaultInstance);
		}

		#endregion

		#region IDisposable interface

		public void Dispose() {
			Save();
		}

		#endregion

		#region ICollection interface

		public void CopyTo(Array array, int index) {
			list.CopyTo(array, index);
		}

		public int Count {
			get {
				int count = 0;
				foreach(MRUEntry entry in list) {
					if (entry.Path != null)
						count++;
				}
				return count;
			}
		}

		public object SyncRoot {
			get { return list.SyncRoot; }
		}

		public bool IsSynchronized {
			get { return list.IsSynchronized; }
		}

		public IEnumerator GetEnumerator() {
			return new ListEnumerator(list);
		}

		#endregion

		#region MRUEntry Struct

		internal struct MRUEntry {
			public int Rank;
			public string Path;

			public override string ToString() {
				return string.Format("&{0} - {1}", Rank+1, Path);
			}
		}

		#endregion

		#region ListComparer class

		private class ListComparer : IComparer {
			public static ListComparer DefaultInstance = new ListComparer();

			public int Compare(object x, object y) {
				if (x == null) {
					if (y == null) {
						return 0;
					}
					else {
						return 1;
					}
				}
				else {
					if (y == null) {
						return -1;
					}
					else {
						return ((MRUEntry) x).Rank.CompareTo(((MRUEntry)y).Rank);
					}
				}
			}
		}

		#endregion

		#region ListEnumerator class

		private class ListEnumerator : IEnumerator {
			private IEnumerator enumerator;


			public ListEnumerator(MRUEntry[] entriesList) {
				this.enumerator = entriesList.GetEnumerator();
			}

			#region IEnumerator interface

			public void Reset() {
				enumerator.Reset();
			}

			public object Current {
				get {
					MRUEntry entry = (MRUEntry) enumerator.Current;
					return entry;
				}
			}

			public bool MoveNext() {
				return enumerator.MoveNext();
			}

			#endregion
		}

		#endregion
	}

}