#region Copyright / License Information
/*

   Copyright 2004 - 2005 Adapdev Technologies, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

============================
Author Log
============================
III	Full Name
SMM	Sean McCormack (Adapdev)


============================
Change Log
============================
III	MMDDYY	Change

*/
#endregion
using System;
using System.Reflection;

namespace Adapdev
{
	using System.Collections;

	/// <summary>
	/// Summary description for AssemblyCache.
	/// </summary>
	public class AssemblyCache
	{
		private static Hashtable ht = new Hashtable();

		private AssemblyCache(){}

		public static Assembly Add(object key, string path)
		{
//			if(!AssemblyCache.ContainsByKey(key) && !AssemblyCache.ContainsByPath(path))
				return AssemblyCache.LoadAssembly(key, path);
//			return AssemblyCache.Get(key);
		}

		public static Assembly Add(string path)
		{
			return AssemblyCache.Add(path, path);
		}

		public static Assembly Get(object key)
		{
			object o = null;
			lock (ht.SyncRoot)
			{
				o = ht[key];
				if(o == null)
				{
					o = LoadAssembly(key, key.ToString());
				}
			}
			return o as Assembly;
		}

		protected static void Set(object key, Assembly a)
		{
			lock (ht.SyncRoot)
			{
				ht[key] = a;
			}
		}

		public static bool ContainsByKey(object key)
		{
			if(ht[key] != null) return true;
			else return false;
		}

		public static bool ContainsByPath(string path)
		{
			foreach(Assembly a in ht.Values)
			{
				if(path.ToLower() == a.Location.ToLower()) return true;
			}
			return false;
		}

		public static void Clear()
		{
			ht = new Hashtable();
		}

		public static int Count()
		{
			return ht.Count;
		}

		private static Assembly LoadAssembly(object key, string path)
		{
			Assembly a = Assembly.LoadFrom(path);
			AssemblyCache.Set(key, a);
			return a;
		}
	}
}