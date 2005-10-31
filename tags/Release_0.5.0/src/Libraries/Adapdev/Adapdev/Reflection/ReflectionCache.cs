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
namespace Adapdev.Reflection
{
	using System;
	using System.Collections;
	using System.Collections.Specialized;
	using System.Reflection;

	/// <summary>
	/// Summary description for ReflectionCache.
	/// </summary>
	public class ReflectionCache
	{
		private static ReflectionCache instance;
		private HybridDictionary assemblies = new HybridDictionary();
		private Hashtable types = new Hashtable();

		public static ReflectionCache GetInstance()
		{
			if (instance == null)
			{
				instance = new ReflectionCache();
			}
			return instance;
		}

		public Assembly GetAssembly(string assembly)
		{
			if (assemblies.Contains(assembly))
			{
				return (Assembly) assemblies[assembly];
			}
			else
			{
				Assembly a = Assembly.LoadFrom(assembly);
				assemblies[assembly] = a;
				return a;
			}
		}

		public Type GetType(string assembly, string type)
		{
			string id = assembly + "|" + type;
			if (types.Contains(id))
			{
				return (Type) types[id];
			}
			else
			{
				Assembly a = this.GetAssembly(assembly);
				Type t = a.GetType(type, true, true);
				types[id] = t;
				return t;
			}
		}
	}
}