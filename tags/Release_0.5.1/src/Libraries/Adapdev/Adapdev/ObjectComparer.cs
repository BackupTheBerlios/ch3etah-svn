using System;
using Adapdev.Reflection;

namespace Adapdev
{
	/// <summary>
	/// Summary description for ObjectComparer.
	/// </summary>
	public class ObjectComparer
	{
		private ObjectComparer()
		{
		}

		public static bool AreEqual(object x, object y)
		{
			Type t = x.GetType();
			ClassAccessor accessor = ClassAccessorCache.Get(t);
			try
			{
				object p1 = null;
				object p2 = null;
				PropertyAccessor property = null;
				foreach(string key in accessor.GetPropertyAccessors().Keys)
				{
					property = accessor.GetPropertyAccessor(key);
					p1 = accessor.GetPropertyValue(x, key);
					p2 = accessor.GetPropertyValue(y, key);

					if(!p1.Equals(Convert.ChangeType(p2, property.PropertyType)))
						return false;
				}
				return true;
			}
			catch(ArgumentException)
			{
				return false;
			}
		}
	}
}
