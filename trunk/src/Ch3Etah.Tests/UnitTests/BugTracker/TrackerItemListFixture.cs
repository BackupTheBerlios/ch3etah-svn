
using System;

using Ch3Etah.BugTracker;

using NUnit.Framework;

namespace Ch3Etah.Tests.UnitTests.BugTracker
{
	[TestFixture]
	public class TrackerItemListFixture
	{
		
		#region Test parsing
		private const string SOURCE_FORGE_MAIN_TRACKER_URL = 
			@"http://sourceforge.net/tracker/?atid=350001&group_id=1&func=browse";

		#region KNOWN_HTML_LIST
		private const string KNOWN_HTML_LIST = 
			@"
<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""3"">
<tr bgcolor=""#ffffff"">
<td align=""middle""><font color=""#000000""><b>Request ID<b></font></td>
<td align=""middle""><font color=""#000000""><b>Summary<b></font></td>
<td align=""middle""><font color=""#000000""><b>Open Date<b></font></td>
<td align=""middle""><font color=""#000000""><b>Priority<b></font></td>
<td align=""middle""><font color=""#000000""><b>Status<b></font></td>
<td align=""middle""><font color=""#000000""><b>Assigned To<b></font></td>
<td align=""middle""><font color=""#000000""><b>Submitted By<b></font></td>
</tr>

	<tr bgcolor=""#dadada"">
		<td nowrap>
						1305191
		</td>
		<td>
			<a href=""/tracker/index.php?func=detail&amp;aid=1305191&amp;group_id=118003&amp;atid=679758"">
				Entity fields not being added in correct order
			</a>
		</td>
		<td>
							<b>*
						2005-09-26 13:47
		</td>
		<td align=""center"">
			1
		</td>

					<td>Closed</td>
				
		<td>nobody</td>
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
	</tr>
	<tr bgcolor=""#dad0d0"">
		<td nowrap>
						1305188
		</td>
		<td>
			<a href=""/tracker/index.php?func=detail&amp;aid=1305188&amp;group_id=118003&amp;atid=679758"">
				GUI always thinks custom entity is dirty
			</a>
		</td>
		<td>
							<b>*
						2005-09-26 13:43
		</td>
		<td align=""center"">
			2
		</td>

					<td>Closed</td>
				
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
	</tr>
	<tr bgcolor=""#dac0c0"">
		<td nowrap>
						1305185
		</td>
		<td>
			<a href=""/tracker/index.php?func=detail&amp;aid=1305185&amp;group_id=118003&amp;atid=679758"">
				Turn off recording of temp xml files during generation
			</a>
		</td>
		<td>
							<b>*
						2005-09-26 13:41
		</td>
		<td align=""center"">
			4
		</td>

					<td>Closed</td>
				
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
	</tr>
	<tr bgcolor=""#dababa"">
		<td nowrap>
						1305182
		</td>
		<td>
			<a href=""/tracker/index.php?func=detail&amp;aid=1305182&amp;group_id=118003&amp;atid=679758"">
				Merge support is not working correctly
			</a>
		</td>
		<td>
							<b>*
						2005-09-26 13:40
		</td>
		<td align=""center"">
			5
		</td>

					<td>Closed</td>
				
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
		<td><a href=""/users/jacobmcp/"">jacobmcp</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=jacobmcp&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a></td>
	</tr>
</table>


</table>		
</form>
";
		#endregion KNOWN_HTML_LIST

		[Test]
		public void TestParseKnownHtmlList()
		{
			TrackerItemListTester tester = new TrackerItemListTester(KNOWN_HTML_LIST);
			Uri[] urls = tester.TestParseTrackerItemListUrls();
			Assert.AreEqual(4, urls.Length, "Wrong number of URLs returned.");
			Assert.AreEqual("http://sourceforge.net/tracker/index.php?func=detail&aid=1305191&group_id=118003&atid=679758", urls[0].AbsoluteUri, "One or more of the URLs returned was incorrect.");
		}

		[Test]
		public void TestRetrieveKnownItemList()
		{
#if RunConnectedTests
			Uri url = new Uri(SOURCE_FORGE_MAIN_TRACKER_URL);
			TrackerItemListTester tester = new TrackerItemListTester(url);
			TrackerItem[] items = tester.TestParseTrackerItemListFromUrl(1);
			foreach (TrackerItem item in items)
			{
				Console.WriteLine(item.DateOpened.ToShortDateString() + "  " + item.Summary);
			}
			Assert.AreEqual(1, items.Length, "Wrong number of items returned.");
			Assert.AreNotEqual("", items[0].ID, "Tracker item ID was blank.");
#else
			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
#endif
		}

		[Test]
		public void TestParseSourceForgeMainTrackerUrl()
		{
#if RunConnectedTests
			Uri url = new Uri(SOURCE_FORGE_MAIN_TRACKER_URL);
			TrackerItemListTester tester = new TrackerItemListTester(url);
			tester.TestParseTrackerItemListUrlsFromUrl();
#else
			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
#endif
		}

		#endregion Test parsing
		
	}
}
