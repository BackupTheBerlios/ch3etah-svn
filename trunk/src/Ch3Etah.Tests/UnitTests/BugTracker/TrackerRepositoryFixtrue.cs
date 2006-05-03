using Ch3Etah.BugTracker;

using NUnit.Framework;

namespace Ch3Etah.Tests.UnitTests.BugTracker
{
	[TestFixture]
	public class TrackerRepositoryFixtrue
	{
		[Test]
		public void TestTrackerListNotLoaded()
		{
			// make sure the list is only loaded when the client requests
			// that it be loaded.
			TrackerRepository.ResetInstance();
			TrackerRepository repo = TrackerRepository.GetInstance();
			Assert.IsNull(repo.GetTrackerItemList().TrackerItems
				, "Tracker item list was loaded prematurely.");
		}
		
		[Test]
		public void TestTrackerSubmitUrl()
		{
			TrackerRepository repo = TrackerRepository.GetInstance();
			Assert.AreEqual(@"http://sourceforge.net/tracker/?func=add&group_id=118003&atid=679758"
				, repo.GetTrackerSubmitUrl());
		}

		[Test]
		public void TestResetTrackerItemList()
		{
			Assert.Fail("ResetTrackerItemList is not working. Right now it hangs indefinitely.");
#if RunConnectedTests
			TrackerRepository repo = TrackerRepository.GetInstance();
			repo.GetTrackerItemList().LoadTrackerItems(1, true);
			repo.ResetTrackerItemList();
			repo.GetTrackerItemList().LoadTrackerItems(1, true);
#else
			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
#endif
		}

	}
}
