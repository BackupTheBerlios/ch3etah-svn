// Original Copyright (c) 2004 Andy Brummer. All Rights Reserved. - http://www.codeproject.com/dotnet/ABTransClockArticle.asp
#region Modified Copyright / License Information
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
using System.Xml;
using System.Xml.XPath;

namespace Adapdev.Scheduling.Timer
{
	/// <summary>
	/// Local event strorage keeps the last time in memory so that skipped events are not recovered.
	/// </summary>
	public class LocalEventStorage : IEventStorage
	{
		public LocalEventStorage()
		{
			_LastTime = DateTime.MaxValue;
		}

		public void RecordLastTime(DateTime Time)
		{
			_LastTime = Time;
		}

		public DateTime ReadLastTime()
		{
			if (_LastTime == DateTime.MaxValue)
				_LastTime = DateTime.Now;
			return _LastTime;
		}

		DateTime _LastTime;
	}

	/// <summary>
	/// FileEventStorage saves the last time in an XmlDocument so that recovery will include periods that the 
	/// process is shutdown.
	/// </summary>
	public class FileEventStorage : IEventStorage
	{
		public FileEventStorage(string FileName, string XPath)
		{
			_FileName = FileName;
			_XPath = XPath;
		}

		public void RecordLastTime(DateTime Time)
		{
			_Doc.SelectSingleNode(_XPath).Value = Time.ToString();
			_Doc.Save(_FileName);
		}

		public DateTime ReadLastTime()
		{
			_Doc.Load(_FileName);
			string Value = _Doc.SelectSingleNode(_XPath).Value;
			if (Value == null || Value == string.Empty)
				return DateTime.Now;
			return DateTime.Parse(Value);
		}

		string _FileName;
		string _XPath;
		XmlDocument _Doc = new XmlDocument();
	}
}
