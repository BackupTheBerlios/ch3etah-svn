using mshtml;

using Ch3Etah.BugTracker;

using NUnit.Framework;

namespace Ch3Etah.Tests.UnitTests.BugTracker
{
	[TestFixture]
	public class TrackerItemSubmitFormFixture
	{
		private const string _KNOWN_URL_TRACKER_SANDBOX = @"https://sourceforge.net/tracker/?func=add&group_id=118003&atid=837481";
		
		[Test]
		public void TestFillOutTrackerForm()
		{
#if RunConnectedTests
			TrackerItemSubmitFormHtmlWrapper wrapper = GetTrackerFormWrapper();

			wrapper.Summary = "test summary";
			Assert.AreEqual("test summary"
				, wrapper.Summary
				, "Form summary failed.");
			
			wrapper.Description = "test description";
			Assert.AreEqual("test description"
				, wrapper.Description
				, "Form description failed.");
#else
			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
#endif
		}

		[Test]
		public void TestSubmitTrackerForm()
		{
			Assert.Ignore("Currently only submitting via web page.");
//#if RunConnectedTests
//			TrackerItemSubmitForm form = GetTrackerForm();
//			form.Summary = "test summary";
//			form.Description = "test description";
//			form.Submit();
//#else
//			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
//#endif
		}
		
		private TrackerItemSubmitFormHtmlWrapper GetTrackerFormWrapper()
		{
			WebBrowserHelper browser = new WebBrowserHelper();
			browser.LoadUrl(_KNOWN_URL_TRACKER_SANDBOX);
			return new TrackerItemSubmitFormHtmlWrapper((IHTMLDocument2)browser.WebBrowser.Document);
		}

	}
}
