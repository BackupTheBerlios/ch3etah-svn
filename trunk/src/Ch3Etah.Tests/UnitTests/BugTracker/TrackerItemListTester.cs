
using System;
using System.Windows.Forms;

using Ch3Etah.BugTracker;

namespace Ch3Etah.Tests.UnitTests.BugTracker
{
	internal class TrackerItemListTester : TrackerItemList
	{
		string _html;
		Uri _url;

		public TrackerItemListTester(Uri url) : base(url)
		{
			_url = url;
		}
		public TrackerItemListTester(string html)
		{
			_html = html;
		}
		
		public TrackerItem[] TestParseTrackerItemListFromUrl(int maxItems)
		{
			base.LoadTrackerItems(maxItems);
			while (base.IsProcessing)
			{
				Application.DoEvents();
			}
			return this.TrackerItems;
		}
		
		public Uri[] TestParseTrackerItemListUrlsFromUrl()
		{
			base.LoadTrackerItems();
			while (base.IsProcessing)
			{
				Application.DoEvents();
			}
			return TestParseTrackerItemListUrls();
		}
		
		public Uri[] TestParseTrackerItemListUrls()
		{
			return ParseTrackerItemListUrls(_html);
		}

		protected override TrackerItem[] ProcessTrackerItemList(string html)
		{
			_html = html;
			if (_url == null)
			{
				return null;
			}
			else
			{
				return base.ProcessTrackerItemList(html);
			}
		}
	}
}
