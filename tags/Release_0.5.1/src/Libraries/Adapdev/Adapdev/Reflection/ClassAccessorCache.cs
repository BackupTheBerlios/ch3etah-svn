using System;
using System.Collections;
using System.Text;

namespace Adapdev.Reflection
{
	/// <summary>
	/// Summary description for ClassAccessorCache.
	/// </summary>
	public class ClassAccessorCache
	{
		private static Hashtable _classes = new Hashtable();

		private ClassAccessorCache()
		{
		}

		public static ClassAccessor Get(Type t)
		{
			ClassAccessor c = null;
			if(!_classes.ContainsKey(t.FullName))
			{
				c = new ClassAccessor(t);
				Add(c);
				c.LoadAllProperties();
			}
			else
			{
				c = _classes[t.FullName] as ClassAccessor;
			}
			return c;
		}

		public static ClassAccessor Get(object o)
		{
			return Get(o.GetType());
		}

		public static void Add(ClassAccessor c)
		{
			_classes[c.Type.FullName] = c;
		}

		public static void Remove(Type t)
		{
			_classes.Remove(t.FullName);
		}

		public static void Clear()
		{
			_classes.Clear();
		}

		public static int Count
		{
			get{return _classes.Count;}
		}

		public static string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Properties for ClassAccessorCache: " + Environment.NewLine);
			foreach(ClassAccessor c in _classes.Values)
			{
				sb.Append(c + Environment.NewLine);
			}
			return sb.ToString();
		}

	}
}
