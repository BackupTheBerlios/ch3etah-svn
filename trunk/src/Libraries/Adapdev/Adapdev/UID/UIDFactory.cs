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

namespace Adapdev.UID
{
	/// <summary>
	/// Summary description for UIDFactory.
	/// </summary>
	public class UIDFactory
	{
		private static UIDType t = UIDType.TimeSpan;
		private static string domain = "ABC";
		private static bool showDomain = true;

		private UIDFactory()
		{
		}

		public static object GetNextId()
		{
			return GetNextId(t, showDomain);
		}

		public static object GetNextId(UIDType uidType)
		{
			return GetNextId(t, false);
		}

		public static object GetNextId(UIDType uidType, bool appendDomain)
		{
			object id;
			switch (uidType)
			{
				case UIDType.TimeSpan:
					id = TimeSpanUIDGenerator.GetInstance().GetNextId();
					break;
				case UIDType.Guid:
					id = GuidUIDGenerator.GetInstance().GetNextId();
					break;
				default:
					id = TimeSpanUIDGenerator.GetInstance().GetNextId();
					break;
			}

			if (appendDomain)
			{
				id = domain + id;
			}
			return id;
		}

		public static UIDType Type
		{
			get { return UIDFactory.t; }
			set { UIDFactory.t = value; }
		}

		public static string Domain
		{
			get { return domain; }
			set { domain = value; }
		}

		public static bool AppendDomain
		{
			get { return showDomain; }
			set { showDomain = value; }
		}

	}

	public enum UIDType
	{
		TimeSpan,
		Guid
	}
}