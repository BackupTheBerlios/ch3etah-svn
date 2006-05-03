using System;

namespace Ch3Etah.BugTracker
{
	public class TrackerRepository
	{
		private string _trackerUrl;
		private TrackerItemList _itemList;

		private TrackerRepository(string trackerUrl)
		{
			_trackerUrl = trackerUrl;
		}
		
		private static TrackerRepository _instance;
		public static TrackerRepository GetInstance()
		{
			if (_instance == null)
			{
				_instance = new TrackerRepository(
					@"http://sourceforge.net/tracker/?group_id=118003&atid=679758");
			}
			return _instance;
		}
		public static void ResetInstance()
		{
			_instance = null;
		}

		public void ResetTrackerItemList()
		{
			if (_itemList != null)
			{
				_itemList.ClearList();
			}
		}
		public TrackerItemList GetTrackerItemList()
		{
			if (_itemList == null)
			{
				_itemList = new TrackerItemList(new Uri(_trackerUrl));
			}
			return _itemList;
		}

		public string GetTrackerBrowseUrl()
		{
			return _trackerUrl;
		}
		
		public string GetTrackerSubmitUrl()
		{
			return _trackerUrl.Replace(@"tracker/?group_id=", @"tracker/?func=add&group_id=");
		}
		
	}
}
