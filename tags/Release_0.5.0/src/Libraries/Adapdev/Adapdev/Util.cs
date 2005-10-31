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

namespace Adapdev
{
	using System;
	using System.Reflection;

	public class Util
	{
		private Util()
		{
		}

		public static bool AreObjectsEqual(Object o1, Object o2)
		{
			Type t1 = o1.GetType();
			Type t2 = o2.GetType();

			PropertyInfo[] pi1 = t1.GetProperties();
			PropertyInfo[] pi2 = t2.GetProperties();

			for (int i = 0; i < pi1.Length; i++)
			{
				try
				{
					PropertyInfo i1 = pi1[i];
					PropertyInfo i2 = pi2[i];

					if (!i1.GetValue(o1, null).Equals(null) && !i2.GetValue(o2, null).Equals(null))
					{
						if (!i1.GetValue(o1, null).Equals(i2.GetValue(o2, null)))
						{
							return false;
						}
					}
				}
				catch (Exception e)
				{
					throw;
				}
			}

			FieldInfo[] fi1 = t1.GetFields();
			FieldInfo[] fi2 = t2.GetFields();

			for (int i = 0; i < fi1.Length; i++)
			{
				try
				{
					FieldInfo i1 = fi1[i];
					FieldInfo i2 = fi2[i];

					if (!i1.GetValue(o1).Equals(null) && !i2.GetValue(o2).Equals(null))
					{
						if (!i1.GetValue(o1).Equals(i2.GetValue(o2)))
						{
							return false;
						}
					}
				}
				catch (Exception e)
				{
					throw;
				}
			}

			return true;
		}

		public static bool IsNumeric(object o)
		{
			if (o is Int16 ||
				o is Int32 ||
				o is Int64 ||
				o is Decimal ||
				o is Double ||
				o is Byte ||
				o is SByte ||
				o is Single ||
				o is UInt16 ||
				o is UInt32 ||
				o is UInt64)
			{
				return true;
			}
			return false;
		}

		public static bool IsDateTime(object o)
		{
			if (o is DateTime) return true;
			return false;
		}
	}
}