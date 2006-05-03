
using System;

using Ch3Etah.BugTracker;

using NUnit.Framework;

namespace Ch3Etah.Tests.UnitTests.BugTracker
{
	[TestFixture]
	public class TrackerItemFixture
	{
		#region InvalidHtmlContent
		[Test,
		ExpectedException(typeof(InvalidHtmlContentException))]
		public void TestInvalidHtml()
		{
			new TrackerItem("<html />");
		}

		[Test,
		ExpectedException(typeof(InvalidHtmlContentException))]
		public void TestInvalidUrl()
		{
#if RunConnectedTests
			new TrackerItem(new Uri("http://www.google.com"));
#else
			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
#endif
		}
		#endregion InvalidHtmlContent
		
		#region KNOWN_TRACKER_ITEM_1306821
		
		#region constants
		private const string KNOWN_URL_1306821 = @"http://sourceforge.net/tracker/index.php?func=detail&aid=1306821&group_id=118003&atid=679758";
		private const string KNOWN_HTML_1306821 = 
			@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""
        ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
    <!-- Server: sf-web3 -->
<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en"">
<head>
        <meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
	<meta name=""description"" content=""The world's largest development and download repository of Open Source code and applications"" />
	<meta name=""keywords"" content=""Open Source, Development, Developers, Projects, Downloads, OSTG, VA Software, SF.net, SourceForge"" />
	<link rel=""alternate"" type=""application/rss+xml"" title=""SourceForge.net Project News"" href=""http://sourceforge.net/export/rss2_sfnews.php?group_id=1&amp;rss_fulltext=1"" />
	<link rel=""alternate"" type=""application/rss+xml"" title=""SourceForge.net News"" href=""http://sourceforge.net/export/rss2_projnews.php?group_id=141424&amp;rss_fulltext=1"" />
	<title>SourceForge.net: Detail: 1306821 - XML Text editor fails to correctly identify file encoding</title>
	<link rel=""stylesheet"" type=""text/css"" href=""http://static.sourceforge.net/sfx.css"" media=""screen"" title=""SFx"" />
	<link rel=""shortcut icon"" href=""http://static.sourceforge.net/images/favicon.ico"" type=""image/x-icon"" />
	<script type=""text/javascript"">
		var link = document.createElement('link');
		link.setAttribute('rel', 'stylesheet');
		link.setAttribute('type', 'text/css');
		link.setAttribute('href', 'http://static.sourceforge.net/sfxjs.css');
		document.getElementsByTagName('head')[0].appendChild(link);

		function help_window(helpurl) {
			HelpWin = window.open( 'http://sourceforge.net' + helpurl,'HelpWindow','scrollbars=yes,resizable=yes,toolbar=no,height=400,width=400');
		}
	</script>
	<script type=""text/javascript"">
		var sf_proj_home = 'http://sourceforge.net/projects/ch3etah';
	</script>
	<!--[if IE]><link rel=""stylesheet"" type=""text/css"" media=""screen"" href=""http://static.sourceforge.net/iestyles.css""  /><![endif]-->
	<script type=""text/javascript"" src=""http://static.sourceforge.net/sfx.js""></script> 
<!-- BEGIN: AdSolution-Tag 4.2: Global-Code [PLACE IN HTML-HEAD-AREA!] -->
        <script type=""text/javascript"" language=""javascript"" src=""http://a.as-us.falkag.net/dat/dlv/aslmain.js""></script>
<!-- END: AdSolution-Tag 4.2: Global-Code -->
<!-- after META tags -->
	<script src=""http://static.sourceforge.net/__utm.js"" type=""text/javascript""></script>
</head>
<body>
<script language=""JavaScript"">var tcdacmd=""dt"";</script>
<script src=""http://an.tacoda.net/an/11715/slf.js"" language=""JavaScript""></script>
<div id=""head"">
	<ul class=""ostgnavbar"">
		<li class=""begin""><a href=""http://ostg.com"">OSTG</a></li>
		<li><a href=""http://thinkgeek.com"">ThinkGeek</a></li>
		<li><a href=""http://slashdot.org"">Slashdot</a></li>
		<li><a href=""http://itmj.com"">ITMJ</a></li>
		<li><a href=""http://linux.com"">Linux.com</a></li>
		<li><a href=""http://newsforge.com"">NewsForge</a></li>
		<li><a href=""http://freshmeat.net"">freshmeat</a></li>
		<li><a href=""http://newsletters.ostg.com"">Newsletters</a></li>
		<li><a href=""http://sourceforge.pricegrabber.com/"">PriceGrabber</a></li>
		<li><a href=""http://sel.as-us.falkag.net/sel?cmd=lnk&amp;kid=97082&amp;bid=218684&amp;dat=121642&amp;opt=0&amp;rdm=[timestamp]"">Jobs</a></li>
		<li><a href=""http://sel.as-us.falkag.net/sel?cmd=lnk&amp;kid=97305&amp;bid=219319&amp;dat=121642&amp;opt=0&amp;rdm=[timestamp]"">Broadband</a></li>
	</ul>
	<div id=""ad1"" align=""center"">

<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p1_top_leaderboard -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='RON_P1_Leaderboard';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p1_top_leaderboard.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   <!--

		End OSDN NavBar
		gid: 118003 
		keywords: ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development 

	-->
	</div>
	<div id=""masthead""> 
		<h1>
			<a href=""/"">SourceForge.net</a>
		</h1>

		<div id=""top""> 
			<form action=""/search/"" method=""get"" class=""top"" id=""searchform"">
				<fieldset>
					<legend>Search</legend>
<input type=""hidden"" value=""118003"" name=""group_id"" /><input type=""hidden"" value=""679758"" name=""atid"" />
					<label for=""searchbox""> 
						<input type=""text"" value="""" class=""searchword"" id=""searchbox"" name=""words"" tabindex=""1"" /> 
					</label> 
					<input type=""submit"" value=""Search"" id=""searchsubmit"" tabindex=""3"" />
					<b onclick=""si('searcher')"" class=""dwns"">&nbsp;</b> 
					<label for=""searchselect"">

						<span id=""searcher"">
							<b onclick=""h('searcher')"" class=""ups"">&nbsp;</b>
							<select name=""type_of_search"" id=""searchselect"" tabindex=""2""> 
		<option value=""artifact"">This Tracker</option><option value=""pervasive"" >This Project</option><option value=""soft"">Software/Group</option><option value=""freshmeat"">Freshmeat.net</option><option value=""sitedocs"">Site Docs</option>
							</select> 
						</span>
					</label> 
				</fieldset>
			</form>
			
			 <span class=""account""> <a href=""https://sourceforge.net/account/login.php"">Log In</a> - <a href=""/account/newuser_emailverify.php"">Create Account</a>
			</span>
	
		</div>

	</div>
</div>
<ul id=""nav"">
	<li class=""begin "" onclick=""hl(this), si('sfnet'), h('projects'), h('mypage'), h('help'); "">
		<a href=""/"" onclick=""return false;"">SF.net</a>
		<ul id=""sfnet"">
			<li class=""begin""><a href=""/"">Home</a></li>
<li><a href=""/docs/about"">About</a></li>
<li><a href=""/supporters.php"">Supporters</a></li>
<li><a href=""http://blog.dev.sf.net/"">Blog</a></li>
<li><a href=""/new/site_news.php"">Site News</a></li>
<li><a href=""/register/"">Create Project</a></li>
<li><a href=""/subscribe/"">Subscribe</a></li>
<li><a href=""http://newsletters.ostg.com/wws/subrequest/sourceforge-daily"">Newsletter</a></li>
<li><a href=""/docs/compile_farm"">Compile Farm</a></li>
 
		</ul>
	</li>
	<li class=""select"" onclick=""hl(this), si('projects'), h('sfnet'), h('mypage'), h('help');"">
		<a href=""/projects/"" onclick=""return false;"">Projects</a>
		<ul id=""projects"">
			<li class=""begin""><a href=""/softwaremap/"">Software Map</a></li>
<li><a href=""/register/"">Create Project</a></li>
<li><a href=""/new/"">New Releases</a></li>
<li><a href=""/top/mostactive.php?type=week"">Top Projects</a></li>
<li><a  class=""subscribe""href=""/new/new_projects.php"">New Projects</a></li>
<li><a href=""/people/"">Help Wanted</a></li>

		</ul>
	</li>
	<li  onclick=""hl(this), si('mypage'), h('sfnet'), h('projects'), h('help');"">
		<a href=""/account/login.php"" onclick=""return false;"">My Page</a>
		<ul id=""mypage"">
			<li class=""begin""><a href=""/my/"">Summary</a></li>
<li><a href=""/my/myprojects.php"">Projects</a></li>
<li><a href=""/my/tracker.php"">Tracker</a></li>
<li><a href=""/my/task.php"">Tasks</a></li>
<li><a href=""/my/donations.php"">Donations</a></li>
<li><a href=""/account/"">Preferences</a></li>

		</ul>

	</li>
	<li  onclick=""hl(this), si('help'), h('sfnet'), h('projects'), h('mypage');"">
		<a href=""/support/getsupport.php"" onclick=""return false;"">Help</a>
		<ul id=""help"">
			<li class=""begin""><a href=""/support/getsupport.php"">Get Support</a></li>
<li><a href=""/docman/?group_id=1"">Documentation</a></li>
<li><a href=""/docs/A03/"">Site Updates</a></li>
<li><a  class=""subscribe""href=""/support/priority.php"">Priority Support</a></li>

		</ul>
	</li>
</ul>
<div class=""usernav"">
	<div id=""status"">
		&nbsp;
						<a href=""/docs/A04/"" class=""button online"">&nbsp;<span>Site Status</span></a>
	</div>
	
	<ul id=""breadcrumb"">
			<li class=""begin""><a href=""/"">SF.net</a></li>
<li><a href=""/softwaremap/"">Projects</a></li>
<li><a href=""/projects/ch3etah/"">CH3ETAH Code Generation IDE</a></li>
<li><a href=""/tracker/?group_id=118003"">Tracker</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679758"">Bugs</a></li>
<li class=""selected""><a href="""">View</a></li>
		</ul>
</div>
<div id=""frame"">
	<div class=""tbarwrap"">

		<b class=""ttopw""><b class=""t1w"">&nbsp;</b><b class=""t2w"">&nbsp;</b></b> 
		<div class=""tboxw"">
			<div class=""wrap"">
				<div class=""tshade"">
				</div>
<!-- begin content -->
	
				<div id=""innerframe"" class=""project"">
					<div class=""topnav"">
						<h2><span>CH3ETAH Code Generation IDE</span>
							<small>
								&nbsp; 
								&nbsp; 
								<a href=""/project/stats/?group_id=118003&amp;ugn=ch3etah"" class=""stats"">Stats - Activity: 98.51%</a> 
							
							<span class=""rss"">

								<a href=""/export/rss2_project.php?group_id=118003"">RSS</a>
							</span>
							</small>
						</h2>
												<ul class=""nav"">
							<li class=""begin""><a href=""/projects/ch3etah/"">Summary</a></li>
<li><a href=""/project/admin/?group_id=118003"">Admin</a></li>
<li><a href=""http://ch3etah.sourceforge.net"">Home Page</a></li>
<li><a href=""/forum/?group_id=118003"">Forums</a></li>
<li><a href=""/tracker/?group_id=118003"">Tracker</a></li>
<li class=""selected""><a href=""/tracker/?group_id=118003&amp;atid=679758"">Bugs</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679759"">Support Requests</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679761"">Feature Requests</a></li>
<li><a href=""/mail/?group_id=118003"">Mail</a></li>
<li><a href=""/pm/?group_id=118003"">Tasks</a></li>
<li><a href=""/news/?group_id=118003"">News</a></li>
<li><a href=""/cvs/?group_id=118003"">CVS</a></li>
<li><a href=""/project/showfiles.php?group_id=118003"">Files</a></li>
			
						</ul>
																		<ul class=""nav"">
							<li class=""begin""><a href=""/tracker/?func=add&amp;group_id=118003&amp;atid=679758"">Submit New</a></li>
<li class=""selected""><a href=""/tracker/?func=browse&amp;group_id=118003&amp;atid=679758"">Browse</a></li>
<li><a href=""/tracker/admin/?group_id=118003"">Admin</a></li>
<li><a href=""/search/index.php?type_of_search=artifact&amp;group_id=118003&amp;atid=679758"">Search</a></li>
			
						</ul>
											</div>
                                        <br class=""break"" />
<!-- begin right column -->
                    

<h2>[ 1306821 ] XML Text editor fails to correctly identify file encoding</h2>
<table cellpadding=""0"" width=""100%"">
	<tr>
		<td colspan=""2"">
			You may monitor this Tracker item after you 
			<a href=""/account/login.php"">login</a> 
			(<a href=""/account/register.php"">register an account, 
			if you do not already have one</a>)
		</td>
	</tr>
	<tr>
		<td>
			<b>Submitted By:</b>
			<br>
			Igor Abade - <a href=""/users/igoravl/"">igoravl</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=igoravl&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a> 
		</td>
		
		<td>
			<b>Date Submitted:</b>
			<br>
			2005-09-28 04:45
		</td>
	</tr>
	
			<tr>
			<td>
				<b>Changed to Closed status by:</b>
				<br>
				<a href=""/users/igoravl/"">igoravl</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=igoravl&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a>
			</td>
			<td>
				<b>Closed as of:</b>
				<br>
				2005-09-28 04:46
			</td>
		</tr>
	
	<tr>
		<td>
			<b>Last Updated By:</b>
			<br>
			igoravl - Comment added
		</td>
		<td>
			<b>Date Last Updated:</b>
			<br>
							2005-09-28 04:46
					</td>
	</tr>

	<tr>
		<td>
			<b>Number of Comments:</b>
			<br>
			2
		</td>
		<td>
			<b>Number of Attachments:</b>
			<br>
			0
		</td>
	</tr>

	<tr>
		<td>
			<b>Category: <a href=""javascript:help_window('/help/tracker.php?helpname=category')"">(?)</a></b>
			<br>
			IDE / Framework
		</td>
		<td>
			<b>Group: <a href=""javascript:help_window('/help/tracker.php?helpname=group')"">(?)</a></b>
			<br>
			None
		</td>
	</tr>

	<tr>
		<td>
			<b>Assigned To: <a href=""javascript:help_window('/help/tracker.php?helpname=assignee')"">(?)</a></b>
			<br>
			Igor Abade
		</td>
		<td>
			<b>Priority: <a href=""javascript:help_window('/help/tracker.php?helpname=priority')"">(?)</a></b>
			<br>
			7
		</td>
	</tr>

	<tr>
		<td>
			<b>Status: <a href=""javascript:help_window('/help/tracker.php?helpname=status')"">(?)</a></b>
			<br>
			Closed
		</td>
					<td>
				<b>Resolution: <a href=""javascript:help_window('/help/tracker.php?helpname=resolution')"">(?)</a></b>
				<br>
				Fixed
			</td>
			</tr>
	
	<tr>
		<td colspan=""2"">
			<b>Summary: <a href=""javascript:help_window('/help/tracker.php?helpname=summary')"">(?)</a></b>
			<br>
			XML Text editor fails to correctly identify file encoding
		</td>
	</tr>

	<tr>
		<td colspan=""2"">
			If the file has no byte-order mark, the text editor<br />
does not identify a file as being Unicode (UTF-16 or<br />
UTF-8). In fact, even if the file is ISO-8859-1 it will<br />
default to ANSI, mostly due to the fact that it relies<br />
on the byte-order mark, which can be absent. It should<br />
take into account the XML processing instruction, but<br />
it is ignored.
						<p>
			<form action=""/tracker/index.php"" method=""post"">
				<input type=""hidden"" name=""group_id"" value=""118003"">
				<input type=""hidden"" name=""atid"" value=""679758"">
				<input type=""hidden"" name=""func"" value=""postaddcomment"">
				<input type=""hidden"" name=""artifact_id"" value=""1306821"">
				<b>Add a Comment:</b>
				<br>
				<textarea name=""details"" rows=""10"" cols=""60"" wrap=""hard""></textarea>
		</td>
	</tr>
	<tr>
		<td colspan=""2"">
							<h3 class=""error"">
						Please <a href=""/account/login.php?return_to=%2Ftracker%2Findex.php%3Ffunc%3Ddetail%26aid%3D1306821%26group_id%3D118003%26atid%3D679758"">log in!</a>
				</h3>
				<br>
				<p>Tracker items submitted anonymously should include a valid 
				email address in the detailed description field.  You will not 
				receive notification of changes to Tracker items submitted 
				anonymously.
						<p>
			<h3>DO NOT enter passwords or other confidential information!</h3>
			<p>
			<input type=""submit"" name=""submit"" value=""SUBMIT"">
			</form>
					</td>
	</tr>

	<tr>
		<td colspan=""2"">
			<h3><br />Followups:</h3>
			<p>
							
<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""3"">
<tr bgcolor=""#ffffff"">
<td align=""middle""><font color=""#000000""><b>Comments<b></font></td>
</tr>

									<tr bgcolor=""#FFFFFF"">
						<td>
							<hr width=""100%"" noshade>
							<pre>
Date: 2005-09-28 04:46
Sender: <a href=""/users/igoravl/"">igoravl</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=igoravl&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a>
Logged In: YES 
user_id=1099041

Changed implementation to use XmlTextReader&#039;s encoding
detection
							</pre>
						</td>
					</tr>
									<tr bgcolor=""#EAECEF"">
						<td>
							<hr width=""100%"" noshade>
							<pre>
Date: 2005-09-28 04:46
Sender: <a href=""/users/igoravl/"">igoravl</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=igoravl&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a>
Logged In: YES 
user_id=1099041

Fixed
							</pre>
						</td>
					</tr>
								</table>
					</td>
	</tr>

	<tr>
		<td colspan=""2"">
			&nbsp;
		</td>
	</tr>
	
	<tr>
		<td colspan=""2"">
			<h4>Attached Files:</h4>
			
<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""3"">
<tr bgcolor=""#ffffff"">
<td align=""middle""><font color=""#000000""><b>&nbsp;<b></font></td>
<td align=""middle""><font color=""#000000""><b>Name<b></font></td>
<td align=""middle""><font color=""#000000""><b>Description<b></font></td>
<td align=""middle""><font color=""#000000""><b>Download<b></font></td>
</tr>

							<tr>
					<td colspan=""3"">No Files Currently Attached</td>
				</tr>
						</table>
		</td>
	</tr>

	<tr>
		<td colspan=""2"">&nbsp;</td>
	</tr>
	<tr>
		<td colspan=""2"">
			<h3>Changes:</h3>
			<p>
							
<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""3"">
<tr bgcolor=""#ffffff"">
<td align=""middle""><font color=""#000000""><b>Field<b></font></td>
<td align=""middle""><font color=""#000000""><b>Old Value<b></font></td>
<td align=""middle""><font color=""#000000""><b>Date<b></font></td>
<td align=""middle""><font color=""#000000""><b>By<b></font></td>
</tr>

									<tr bgcolor=""#FFFFFF"">
						<td>
							close_date
						</td>
						<td>
							-
						</td>
						<td>
							2005-09-28 04:46
						</td>
						<td>
							igoravl
						</td>
					</tr>
									<tr bgcolor=""#EAECEF"">
						<td>
							resolution_id
						</td>
						<td>
							None
						</td>
						<td>
							2005-09-28 04:46
						</td>
						<td>
							igoravl
						</td>
					</tr>
									<tr bgcolor=""#FFFFFF"">
						<td>
							status_id
						</td>
						<td>
							Open
						</td>
						<td>
							2005-09-28 04:46
						</td>
						<td>
							igoravl
						</td>
					</tr>
								</table>
					</td>
	</tr>

</table>

			<br class=""break"" />
					<div id=""btmad"">
						<div id=""ad34"">
			<div class=""tbarhigh"">
				<b class=""ttop""><b class=""t1"">&nbsp;</b><b class=""t2"">&nbsp;</b></b>
				<div class=""tbox"">
					<h3> Find a Tech Job </h3>
				</div>
				<b class=""tbtm""><b class=""t2 tbg"">&nbsp;</b><b class=""t1 tbg"">&nbsp;</b></b>
			</div><div class=""sponsor"">
			
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p34_left_fixed_utility -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p34_left_fixed_utility.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   <!-- here -->	
		</div> 
			</div>
						<div class=""dual"">
							<div id=""ad7"">
								
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p7_bottom_left_sky -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='RON_Other_Skyscraper';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p7_bottom_left_sky.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   
							</div>
							<div id=""ostgservices"">
								
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p40_bottom_right_svcs -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p40_bottom_right_svcs.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   
							</div>
						</div>
					</div>	
				</div>
				<div class=""bshade"">
				</div>
			</div>
		</div>
		<b class=""tbtmw""><b class=""t2w tbgw"">&nbsp;</b><b class=""t1w tbgw"">&nbsp;</b></b> 
	</div>
</div>
<div id=""footer"">
	<ul>
		<li><a href=""/docs/about"">About SourceForge.net</a></li>
		<li><a href=""http://www.ostg.com/about/"">About OSTG</a></li>
		<li><a href=""/tos/privacy.php"">Privacy Statement</a></li>
		<li><a href=""/tos/tos.php"">Terms of Use</a></li>
		<li><a href=""http://www.ostg.com/advertising/"">Advertise</a></li>
		<li><a href=""/support/getsupport.php"">Get Support</a></li>
		<li><a href=""/export/rss2_sfnews.php?group_id=1&amp;rss_fulltext=1"">RSS</a></li>
	</ul>
	<p>
		Powered by the <a href=""http://www.vasoftware.com/sourceforge/index.php"">SourceForge&reg;</a> collaborative development environment from VA Software 
		<br />
		&copy;Copyright 2006 - <a href=""http://ostg.com"">OSTG</a> Open Source Technology Group, All Rights Reserved 
	</p>

</div>
</body>
</html>
";
		#endregion constants
		
		[Test]
		public void TestParseKnownHtml_1306821()
		{
			ValidateKnownTrackerItem_1306821(
				new TrackerItem(KNOWN_HTML_1306821));
		}
		
		[Test]
		public void TestParseKnownUrl_1306821()
		{
#if RunConnectedTests
			Uri url = new Uri(KNOWN_URL_1306821);
			ValidateKnownTrackerItem_1306821(new TrackerItem(url));
#else
			Assert.Ignore(TestingMessages.DisconnectedTestsMessage);
#endif
		}
		
		private void ValidateKnownTrackerItem_1306821(TrackerItem item)
		{
			Assert.AreEqual("1306821", item.ID, "Incorrect ID");
			Assert.AreEqual(DateTime.Parse("2005-09-28 04:45"), item.DateOpened, "Incorrect opened date");
			Assert.AreEqual(DateTime.Parse("2005-09-28 04:46"), item.DateClosed, "Incorrect closed date");
			Assert.AreEqual("CLOSED", item.Status.Name.ToUpper(), "Incorrect status");
			Assert.AreEqual("XML Text editor fails to correctly identify file encoding", item.Summary, "Incorrect summary");
			string desc = "If the file has no byte-order mark, the text editor\r\ndoes not identify a file as being Unicode";
			Assert.AreEqual(desc, item.Description.Substring(0, desc.Length), "Incorrect description");
		}
		#endregion KNOWN_TRACKER_ITEM_1306821

		#region KNOWN_TRACKER_ITEM_OPEN_ITEM
		
		#region constants
		private const string KNOWN_HTML_OPEN_ITEM = 
			@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""
        ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
    <!-- Server: sf-web1 -->
<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en"">
<head>
        <meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
	<meta name=""description"" content=""The world's largest development and download repository of Open Source code and applications"" />
	<meta name=""keywords"" content=""Open Source, Development, Developers, Projects, Downloads, OSTG, VA Software, SF.net, SourceForge"" />
	<link rel=""alternate"" type=""application/rss+xml"" title=""SourceForge.net Project News"" href=""http://sourceforge.net/export/rss2_sfnews.php?group_id=1&amp;rss_fulltext=1"" />
	<link rel=""alternate"" type=""application/rss+xml"" title=""SourceForge.net News"" href=""http://sourceforge.net/export/rss2_projnews.php?group_id=141424&amp;rss_fulltext=1"" />
	<title>SourceForge.net: Detail: 1351554 - Data Source Editor window tab does not change after rename</title>
	<link rel=""stylesheet"" type=""text/css"" href=""http://static.sourceforge.net/sfx.css"" media=""screen"" title=""SFx"" />
	<link rel=""shortcut icon"" href=""http://static.sourceforge.net/images/favicon.ico"" type=""image/x-icon"" />
	<script type=""text/javascript"">
		var link = document.createElement('link');
		link.setAttribute('rel', 'stylesheet');
		link.setAttribute('type', 'text/css');
		link.setAttribute('href', 'http://static.sourceforge.net/sfxjs.css');
		document.getElementsByTagName('head')[0].appendChild(link);

		function help_window(helpurl) {
			HelpWin = window.open( 'http://sourceforge.net' + helpurl,'HelpWindow','scrollbars=yes,resizable=yes,toolbar=no,height=400,width=400');
		}
	</script>
	<script type=""text/javascript"">
		var sf_proj_home = 'http://sourceforge.net/projects/ch3etah';
	</script>
	<!--[if IE]><link rel=""stylesheet"" type=""text/css"" media=""screen"" href=""http://static.sourceforge.net/iestyles.css""  /><![endif]-->
	<script type=""text/javascript"" src=""http://static.sourceforge.net/sfx.js""></script> 
<!-- BEGIN: AdSolution-Tag 4.2: Global-Code [PLACE IN HTML-HEAD-AREA!] -->
        <script type=""text/javascript"" language=""javascript"" src=""http://a.as-us.falkag.net/dat/dlv/aslmain.js""></script>
<!-- END: AdSolution-Tag 4.2: Global-Code -->
<!-- after META tags -->
	<script src=""http://static.sourceforge.net/__utm.js"" type=""text/javascript""></script>
</head>
<body>
<script language=""JavaScript"">var tcdacmd=""dt"";</script>
<script src=""http://an.tacoda.net/an/11715/slf.js"" language=""JavaScript""></script>
<div id=""head"">
	<ul class=""ostgnavbar"">
		<li class=""begin""><a href=""http://ostg.com"">OSTG</a></li>
		<li><a href=""http://thinkgeek.com"">ThinkGeek</a></li>
		<li><a href=""http://slashdot.org"">Slashdot</a></li>
		<li><a href=""http://itmj.com"">ITMJ</a></li>
		<li><a href=""http://linux.com"">Linux.com</a></li>
		<li><a href=""http://newsforge.com"">NewsForge</a></li>
		<li><a href=""http://freshmeat.net"">freshmeat</a></li>
		<li><a href=""http://newsletters.ostg.com"">Newsletters</a></li>
		<li><a href=""http://sourceforge.pricegrabber.com/"">PriceGrabber</a></li>
		<li><a href=""http://sel.as-us.falkag.net/sel?cmd=lnk&amp;kid=97082&amp;bid=218684&amp;dat=121642&amp;opt=0&amp;rdm=[timestamp]"">Jobs</a></li>
		<li><a href=""http://sel.as-us.falkag.net/sel?cmd=lnk&amp;kid=97305&amp;bid=219319&amp;dat=121642&amp;opt=0&amp;rdm=[timestamp]"">Broadband</a></li>
	</ul>
	<div id=""ad1"" align=""center"">

<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p1_top_leaderboard -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='RON_P1_Leaderboard';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p1_top_leaderboard.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   <!--

		End OSDN NavBar
		gid: 118003 
		keywords: ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development 

	-->
	</div>
	<div id=""masthead""> 
		<h1>
			<a href=""/"">SourceForge.net</a>
		</h1>

		<div id=""top""> 
			<form action=""/search/"" method=""get"" class=""top"" id=""searchform"">
				<fieldset>
					<legend>Search</legend>
<input type=""hidden"" value=""118003"" name=""group_id"" /><input type=""hidden"" value=""679758"" name=""atid"" />
					<label for=""searchbox""> 
						<input type=""text"" value="""" class=""searchword"" id=""searchbox"" name=""words"" tabindex=""1"" /> 
					</label> 
					<input type=""submit"" value=""Search"" id=""searchsubmit"" tabindex=""3"" />
					<b onclick=""si('searcher')"" class=""dwns"">&nbsp;</b> 
					<label for=""searchselect"">

						<span id=""searcher"">
							<b onclick=""h('searcher')"" class=""ups"">&nbsp;</b>
							<select name=""type_of_search"" id=""searchselect"" tabindex=""2""> 
		<option value=""artifact"">This Tracker</option><option value=""pervasive"" >This Project</option><option value=""soft"">Software/Group</option><option value=""freshmeat"">Freshmeat.net</option><option value=""sitedocs"">Site Docs</option>
							</select> 
						</span>
					</label> 
				</fieldset>
			</form>
			
			 <span class=""account""> <a href=""https://sourceforge.net/account/login.php"">Log In</a> - <a href=""/account/newuser_emailverify.php"">Create Account</a>
			</span>
	
		</div>

	</div>
</div>
<ul id=""nav"">
	<li class=""begin "" onclick=""hl(this), si('sfnet'), h('projects'), h('mypage'), h('help'); "">
		<a href=""/"" onclick=""return false;"">SF.net</a>
		<ul id=""sfnet"">
			<li class=""begin""><a href=""/"">Home</a></li>
<li><a href=""/docs/about"">About</a></li>
<li><a href=""/supporters.php"">Supporters</a></li>
<li><a href=""http://blog.dev.sf.net/"">Blog</a></li>
<li><a href=""/new/site_news.php"">Site News</a></li>
<li><a href=""/register/"">Create Project</a></li>
<li><a href=""/subscribe/"">Subscribe</a></li>
<li><a href=""http://newsletters.ostg.com/wws/subrequest/sourceforge-daily"">Newsletter</a></li>
<li><a href=""/docs/compile_farm"">Compile Farm</a></li>
 
		</ul>
	</li>
	<li class=""select"" onclick=""hl(this), si('projects'), h('sfnet'), h('mypage'), h('help');"">
		<a href=""/projects/"" onclick=""return false;"">Projects</a>
		<ul id=""projects"">
			<li class=""begin""><a href=""/softwaremap/"">Software Map</a></li>
<li><a href=""/register/"">Create Project</a></li>
<li><a href=""/new/"">New Releases</a></li>
<li><a href=""/top/mostactive.php?type=week"">Top Projects</a></li>
<li><a  class=""subscribe""href=""/new/new_projects.php"">New Projects</a></li>
<li><a href=""/people/"">Help Wanted</a></li>

		</ul>
	</li>
	<li  onclick=""hl(this), si('mypage'), h('sfnet'), h('projects'), h('help');"">
		<a href=""/account/login.php"" onclick=""return false;"">My Page</a>
		<ul id=""mypage"">
			<li class=""begin""><a href=""/my/"">Summary</a></li>
<li><a href=""/my/myprojects.php"">Projects</a></li>
<li><a href=""/my/tracker.php"">Tracker</a></li>
<li><a href=""/my/task.php"">Tasks</a></li>
<li><a href=""/my/donations.php"">Donations</a></li>
<li><a href=""/account/"">Preferences</a></li>

		</ul>

	</li>
	<li  onclick=""hl(this), si('help'), h('sfnet'), h('projects'), h('mypage');"">
		<a href=""/support/getsupport.php"" onclick=""return false;"">Help</a>
		<ul id=""help"">
			<li class=""begin""><a href=""/support/getsupport.php"">Get Support</a></li>
<li><a href=""/docman/?group_id=1"">Documentation</a></li>
<li><a href=""/docs/A03/"">Site Updates</a></li>
<li><a  class=""subscribe""href=""/support/priority.php"">Priority Support</a></li>

		</ul>
	</li>
</ul>
<div class=""usernav"">
	<div id=""status"">
		&nbsp;
						<a href=""/docs/A04/"" class=""button online"">&nbsp;<span>Site Status</span></a>
	</div>
	
	<ul id=""breadcrumb"">
			<li class=""begin""><a href=""/"">SF.net</a></li>
<li><a href=""/softwaremap/"">Projects</a></li>
<li><a href=""/projects/ch3etah/"">CH3ETAH Code Generation IDE</a></li>
<li><a href=""/tracker/?group_id=118003"">Tracker</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679758"">Bugs</a></li>
<li class=""selected""><a href="""">View</a></li>
		</ul>
</div>
<div id=""frame"">
	<div class=""tbarwrap"">

		<b class=""ttopw""><b class=""t1w"">&nbsp;</b><b class=""t2w"">&nbsp;</b></b> 
		<div class=""tboxw"">
			<div class=""wrap"">
				<div class=""tshade"">
				</div>
<!-- begin content -->
	
				<div id=""innerframe"" class=""project"">
					<div class=""topnav"">
						<h2><span>CH3ETAH Code Generation IDE</span>
							<small>
								&nbsp; 
								&nbsp; 
								<a href=""/project/stats/?group_id=118003&amp;ugn=ch3etah"" class=""stats"">Stats - Activity: 98.51%</a> 
							
							<span class=""rss"">

								<a href=""/export/rss2_project.php?group_id=118003"">RSS</a>
							</span>
							</small>
						</h2>
												<ul class=""nav"">
							<li class=""begin""><a href=""/projects/ch3etah/"">Summary</a></li>
<li><a href=""/project/admin/?group_id=118003"">Admin</a></li>
<li><a href=""http://ch3etah.sourceforge.net"">Home Page</a></li>
<li><a href=""/forum/?group_id=118003"">Forums</a></li>
<li><a href=""/tracker/?group_id=118003"">Tracker</a></li>
<li class=""selected""><a href=""/tracker/?group_id=118003&amp;atid=679758"">Bugs</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679759"">Support Requests</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679761"">Feature Requests</a></li>
<li><a href=""/mail/?group_id=118003"">Mail</a></li>
<li><a href=""/pm/?group_id=118003"">Tasks</a></li>
<li><a href=""/news/?group_id=118003"">News</a></li>
<li><a href=""/cvs/?group_id=118003"">CVS</a></li>
<li><a href=""/project/showfiles.php?group_id=118003"">Files</a></li>
			
						</ul>
																		<ul class=""nav"">
							<li class=""begin""><a href=""/tracker/?func=add&amp;group_id=118003&amp;atid=679758"">Submit New</a></li>
<li class=""selected""><a href=""/tracker/?func=browse&amp;group_id=118003&amp;atid=679758"">Browse</a></li>
<li><a href=""/tracker/admin/?group_id=118003"">Admin</a></li>
<li><a href=""/search/index.php?type_of_search=artifact&amp;group_id=118003&amp;atid=679758"">Search</a></li>
			
						</ul>
											</div>
                                        <br class=""break"" />
<!-- begin right column -->
                    

<h2>[ 1351554 ] Data Source Editor window tab does not change after rename</h2>
<table cellpadding=""0"" width=""100%"">
	<tr>
		<td colspan=""2"">
			You may monitor this Tracker item after you 
			<a href=""/account/login.php"">login</a> 
			(<a href=""/account/register.php"">register an account, 
			if you do not already have one</a>)
		</td>
	</tr>
	<tr>
		<td>
			<b>Submitted By:</b>
			<br>
			Igor Abade - <a href=""/users/igoravl/"">igoravl</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=igoravl&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a> 
		</td>
		
		<td>
			<b>Date Submitted:</b>
			<br>
			2005-11-08 09:56
		</td>
	</tr>
	
	
	<tr>
		<td>
			<b>Last Updated By:</b>
			<br>
			Item Submitter - Tracker Item Submitted
		</td>
		<td>
			<b>Date Last Updated:</b>
			<br>
							No updates since submission
					</td>
	</tr>

	<tr>
		<td>
			<b>Number of Comments:</b>
			<br>
			0
		</td>
		<td>
			<b>Number of Attachments:</b>
			<br>
			0
		</td>
	</tr>

	<tr>
		<td>
			<b>Category: <a href=""javascript:help_window('/help/tracker.php?helpname=category')"">(?)</a></b>
			<br>
			None
		</td>
		<td>
			<b>Group: <a href=""javascript:help_window('/help/tracker.php?helpname=group')"">(?)</a></b>
			<br>
			None
		</td>
	</tr>

	<tr>
		<td>
			<b>Assigned To: <a href=""javascript:help_window('/help/tracker.php?helpname=assignee')"">(?)</a></b>
			<br>
			Nobody/Anonymous
		</td>
		<td>
			<b>Priority: <a href=""javascript:help_window('/help/tracker.php?helpname=priority')"">(?)</a></b>
			<br>
			5
		</td>
	</tr>

	<tr>
		<td>
			<b>Status: <a href=""javascript:help_window('/help/tracker.php?helpname=status')"">(?)</a></b>
			<br>
			Open
		</td>
					<td>
				<b>Resolution: <a href=""javascript:help_window('/help/tracker.php?helpname=resolution')"">(?)</a></b>
				<br>
				None
			</td>
			</tr>
	
	<tr>
		<td colspan=""2"">
			<b>Summary: <a href=""javascript:help_window('/help/tracker.php?helpname=summary')"">(?)</a></b>
			<br>
			Data Source Editor window tab does not change after rename
		</td>
	</tr>

	<tr>
		<td colspan=""2"">
			When a data source name is changed thru the Properties<br />
window, its new name is not shown in the window tab
						<p>
			<form action=""/tracker/index.php"" method=""post"">
				<input type=""hidden"" name=""group_id"" value=""118003"">
				<input type=""hidden"" name=""atid"" value=""679758"">
				<input type=""hidden"" name=""func"" value=""postaddcomment"">
				<input type=""hidden"" name=""artifact_id"" value=""1351554"">
				<b>Add a Comment:</b>
				<br>
				<textarea name=""details"" rows=""10"" cols=""60"" wrap=""hard""></textarea>
		</td>
	</tr>
	<tr>
		<td colspan=""2"">
							<h3 class=""error"">
						Please <a href=""/account/login.php?return_to=%2Ftracker%2Findex.php%3Ffunc%3Ddetail%26aid%3D1351554%26group_id%3D118003%26atid%3D679758"">log in!</a>
				</h3>
				<br>
				<p>Tracker items submitted anonymously should include a valid 
				email address in the detailed description field.  You will not 
				receive notification of changes to Tracker items submitted 
				anonymously.
						<p>
			<h3>DO NOT enter passwords or other confidential information!</h3>
			<p>
			<input type=""submit"" name=""submit"" value=""SUBMIT"">
			</form>
					</td>
	</tr>

	<tr>
		<td colspan=""2"">
			<h3><br />Followups:</h3>
			<p>
							<h3>No follow-up comments have been posted.</h3>
					</td>
	</tr>

	<tr>
		<td colspan=""2"">
			&nbsp;
		</td>
	</tr>
	
	<tr>
		<td colspan=""2"">
			<h4>Attached Files:</h4>
			
<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""3"">
<tr bgcolor=""#ffffff"">
<td align=""middle""><font color=""#000000""><b>&nbsp;<b></font></td>
<td align=""middle""><font color=""#000000""><b>Name<b></font></td>
<td align=""middle""><font color=""#000000""><b>Description<b></font></td>
<td align=""middle""><font color=""#000000""><b>Download<b></font></td>
</tr>

							<tr>
					<td colspan=""3"">No Files Currently Attached</td>
				</tr>
						</table>
		</td>
	</tr>

	<tr>
		<td colspan=""2"">&nbsp;</td>
	</tr>
	<tr>
		<td colspan=""2"">
			<h3>Changes:</h3>
			<p>
							<h3>No Changes Have Been Made to This Item</h3>
					</td>
	</tr>

</table>

			<br class=""break"" />
					<div id=""btmad"">
						<div id=""ad34"">
			<div class=""tbarhigh"">
				<b class=""ttop""><b class=""t1"">&nbsp;</b><b class=""t2"">&nbsp;</b></b>
				<div class=""tbox"">
					<h3> Find a Tech Job </h3>
				</div>
				<b class=""tbtm""><b class=""t2 tbg"">&nbsp;</b><b class=""t1 tbg"">&nbsp;</b></b>
			</div><div class=""sponsor"">
			
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p34_left_fixed_utility -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p34_left_fixed_utility.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   <!-- here -->	
		</div> 
			</div>
						<div class=""dual"">
							<div id=""ad7"">
								
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p7_bottom_left_sky -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='RON_Other_Skyscraper';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p7_bottom_left_sky.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   
							</div>
							<div id=""ostgservices"">
								
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p40_bottom_right_svcs -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p40_bottom_right_svcs.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   
							</div>
						</div>
					</div>	
				</div>
				<div class=""bshade"">
				</div>
			</div>
		</div>
		<b class=""tbtmw""><b class=""t2w tbgw"">&nbsp;</b><b class=""t1w tbgw"">&nbsp;</b></b> 
	</div>
</div>
<div id=""footer"">
	<ul>
		<li><a href=""/docs/about"">About SourceForge.net</a></li>
		<li><a href=""http://www.ostg.com/about/"">About OSTG</a></li>
		<li><a href=""/tos/privacy.php"">Privacy Statement</a></li>
		<li><a href=""/tos/tos.php"">Terms of Use</a></li>
		<li><a href=""http://www.ostg.com/advertising/"">Advertise</a></li>
		<li><a href=""/support/getsupport.php"">Get Support</a></li>
		<li><a href=""/export/rss2_sfnews.php?group_id=1&amp;rss_fulltext=1"">RSS</a></li>
	</ul>
	<p>
		Powered by the <a href=""http://www.vasoftware.com/sourceforge/index.php"">SourceForge&reg;</a> collaborative development environment from VA Software 
		<br />
		&copy;Copyright 2006 - <a href=""http://ostg.com"">OSTG</a> Open Source Technology Group, All Rights Reserved 
	</p>

</div>
</body>
</html>
";
		#endregion constants
		
		[Test]
		public void TestParseKnownHtml_OpenItem()
		{
			ValidateKnownTrackerItem_OpenItem(
				new TrackerItem(KNOWN_HTML_OPEN_ITEM));
		}
		
		
		private void ValidateKnownTrackerItem_OpenItem(TrackerItem item)
		{
			Assert.AreEqual("OPEN", item.Status.Name.ToUpper(), "Incorrect status");
		}
		#endregion KNOWN_TRACKER_ITEM_OPEN_ITEM

		#region HTML_ENCODED_SUMMARY_AND_MATCHING_DESCRIPTION
		
		#region constants
		private const string HTML_ENCODED_SUMMARY_AND_MATCHING_DESCRIPTION = 
			@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""
        ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
    <!-- Server: sf-web1 -->
<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en"">
<head>
        <meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
	<meta name=""description"" content=""The world's largest development and download repository of Open Source code and applications"" />
	<meta name=""keywords"" content=""Open Source, Development, Developers, Projects, Downloads, OSTG, VA Software, SF.net, SourceForge"" />
	<link rel=""alternate"" type=""application/rss+xml"" title=""SourceForge.net Project News"" href=""http://sourceforge.net/export/rss2_sfnews.php?group_id=1&amp;rss_fulltext=1"" />
	<link rel=""alternate"" type=""application/rss+xml"" title=""SourceForge.net News"" href=""http://sourceforge.net/export/rss2_projnews.php?group_id=141424&amp;rss_fulltext=1"" />
	<title>SourceForge.net: Detail: 1351554 - \&quot;Packages\&quot; node... doesn&#039;t work</title>
	<link rel=""stylesheet"" type=""text/css"" href=""http://static.sourceforge.net/sfx.css"" media=""screen"" title=""SFx"" />
	<link rel=""shortcut icon"" href=""http://static.sourceforge.net/images/favicon.ico"" type=""image/x-icon"" />
	<script type=""text/javascript"">
		var link = document.createElement('link');
		link.setAttribute('rel', 'stylesheet');
		link.setAttribute('type', 'text/css');
		link.setAttribute('href', 'http://static.sourceforge.net/sfxjs.css');
		document.getElementsByTagName('head')[0].appendChild(link);

		function help_window(helpurl) {
			HelpWin = window.open( 'http://sourceforge.net' + helpurl,'HelpWindow','scrollbars=yes,resizable=yes,toolbar=no,height=400,width=400');
		}
	</script>
	<script type=""text/javascript"">
		var sf_proj_home = 'http://sourceforge.net/projects/ch3etah';
	</script>
	<!--[if IE]><link rel=""stylesheet"" type=""text/css"" media=""screen"" href=""http://static.sourceforge.net/iestyles.css""  /><![endif]-->
	<script type=""text/javascript"" src=""http://static.sourceforge.net/sfx.js""></script> 
<!-- BEGIN: AdSolution-Tag 4.2: Global-Code [PLACE IN HTML-HEAD-AREA!] -->
        <script type=""text/javascript"" language=""javascript"" src=""http://a.as-us.falkag.net/dat/dlv/aslmain.js""></script>
<!-- END: AdSolution-Tag 4.2: Global-Code -->
<!-- after META tags -->
	<script src=""http://static.sourceforge.net/__utm.js"" type=""text/javascript""></script>
</head>
<body>
<script language=""JavaScript"">var tcdacmd=""dt"";</script>
<script src=""http://an.tacoda.net/an/11715/slf.js"" language=""JavaScript""></script>
<div id=""head"">
	<ul class=""ostgnavbar"">
		<li class=""begin""><a href=""http://ostg.com"">OSTG</a></li>
		<li><a href=""http://thinkgeek.com"">ThinkGeek</a></li>
		<li><a href=""http://slashdot.org"">Slashdot</a></li>
		<li><a href=""http://itmj.com"">ITMJ</a></li>
		<li><a href=""http://linux.com"">Linux.com</a></li>
		<li><a href=""http://newsforge.com"">NewsForge</a></li>
		<li><a href=""http://freshmeat.net"">freshmeat</a></li>
		<li><a href=""http://newsletters.ostg.com"">Newsletters</a></li>
		<li><a href=""http://sourceforge.pricegrabber.com/"">PriceGrabber</a></li>
		<li><a href=""http://sel.as-us.falkag.net/sel?cmd=lnk&amp;kid=97082&amp;bid=218684&amp;dat=121642&amp;opt=0&amp;rdm=[timestamp]"">Jobs</a></li>
		<li><a href=""http://sel.as-us.falkag.net/sel?cmd=lnk&amp;kid=97305&amp;bid=219319&amp;dat=121642&amp;opt=0&amp;rdm=[timestamp]"">Broadband</a></li>
	</ul>
	<div id=""ad1"" align=""center"">

<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p1_top_leaderboard -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='RON_P1_Leaderboard';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p1_top_leaderboard.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   <!--

		End OSDN NavBar
		gid: 118003 
		keywords: ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development 

	-->
	</div>
	<div id=""masthead""> 
		<h1>
			<a href=""/"">SourceForge.net</a>
		</h1>

		<div id=""top""> 
			<form action=""/search/"" method=""get"" class=""top"" id=""searchform"">
				<fieldset>
					<legend>Search</legend>
<input type=""hidden"" value=""118003"" name=""group_id"" /><input type=""hidden"" value=""679758"" name=""atid"" />
					<label for=""searchbox""> 
						<input type=""text"" value="""" class=""searchword"" id=""searchbox"" name=""words"" tabindex=""1"" /> 
					</label> 
					<input type=""submit"" value=""Search"" id=""searchsubmit"" tabindex=""3"" />
					<b onclick=""si('searcher')"" class=""dwns"">&nbsp;</b> 
					<label for=""searchselect"">

						<span id=""searcher"">
							<b onclick=""h('searcher')"" class=""ups"">&nbsp;</b>
							<select name=""type_of_search"" id=""searchselect"" tabindex=""2""> 
		<option value=""artifact"">This Tracker</option><option value=""pervasive"" >This Project</option><option value=""soft"">Software/Group</option><option value=""freshmeat"">Freshmeat.net</option><option value=""sitedocs"">Site Docs</option>
							</select> 
						</span>
					</label> 
				</fieldset>
			</form>
			
			 <span class=""account""> <a href=""https://sourceforge.net/account/login.php"">Log In</a> - <a href=""/account/newuser_emailverify.php"">Create Account</a>
			</span>
	
		</div>

	</div>
</div>
<ul id=""nav"">
	<li class=""begin "" onclick=""hl(this), si('sfnet'), h('projects'), h('mypage'), h('help'); "">
		<a href=""/"" onclick=""return false;"">SF.net</a>
		<ul id=""sfnet"">
			<li class=""begin""><a href=""/"">Home</a></li>
<li><a href=""/docs/about"">About</a></li>
<li><a href=""/supporters.php"">Supporters</a></li>
<li><a href=""http://blog.dev.sf.net/"">Blog</a></li>
<li><a href=""/new/site_news.php"">Site News</a></li>
<li><a href=""/register/"">Create Project</a></li>
<li><a href=""/subscribe/"">Subscribe</a></li>
<li><a href=""http://newsletters.ostg.com/wws/subrequest/sourceforge-daily"">Newsletter</a></li>
<li><a href=""/docs/compile_farm"">Compile Farm</a></li>
 
		</ul>
	</li>
	<li class=""select"" onclick=""hl(this), si('projects'), h('sfnet'), h('mypage'), h('help');"">
		<a href=""/projects/"" onclick=""return false;"">Projects</a>
		<ul id=""projects"">
			<li class=""begin""><a href=""/softwaremap/"">Software Map</a></li>
<li><a href=""/register/"">Create Project</a></li>
<li><a href=""/new/"">New Releases</a></li>
<li><a href=""/top/mostactive.php?type=week"">Top Projects</a></li>
<li><a  class=""subscribe""href=""/new/new_projects.php"">New Projects</a></li>
<li><a href=""/people/"">Help Wanted</a></li>

		</ul>
	</li>
	<li  onclick=""hl(this), si('mypage'), h('sfnet'), h('projects'), h('help');"">
		<a href=""/account/login.php"" onclick=""return false;"">My Page</a>
		<ul id=""mypage"">
			<li class=""begin""><a href=""/my/"">Summary</a></li>
<li><a href=""/my/myprojects.php"">Projects</a></li>
<li><a href=""/my/tracker.php"">Tracker</a></li>
<li><a href=""/my/task.php"">Tasks</a></li>
<li><a href=""/my/donations.php"">Donations</a></li>
<li><a href=""/account/"">Preferences</a></li>

		</ul>

	</li>
	<li  onclick=""hl(this), si('help'), h('sfnet'), h('projects'), h('mypage');"">
		<a href=""/support/getsupport.php"" onclick=""return false;"">Help</a>
		<ul id=""help"">
			<li class=""begin""><a href=""/support/getsupport.php"">Get Support</a></li>
<li><a href=""/docman/?group_id=1"">Documentation</a></li>
<li><a href=""/docs/A03/"">Site Updates</a></li>
<li><a  class=""subscribe""href=""/support/priority.php"">Priority Support</a></li>

		</ul>
	</li>
</ul>
<div class=""usernav"">
	<div id=""status"">
		&nbsp;
						<a href=""/docs/A04/"" class=""button online"">&nbsp;<span>Site Status</span></a>
	</div>
	
	<ul id=""breadcrumb"">
			<li class=""begin""><a href=""/"">SF.net</a></li>
<li><a href=""/softwaremap/"">Projects</a></li>
<li><a href=""/projects/ch3etah/"">CH3ETAH Code Generation IDE</a></li>
<li><a href=""/tracker/?group_id=118003"">Tracker</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679758"">Bugs</a></li>
<li class=""selected""><a href="""">View</a></li>
		</ul>
</div>
<div id=""frame"">
	<div class=""tbarwrap"">

		<b class=""ttopw""><b class=""t1w"">&nbsp;</b><b class=""t2w"">&nbsp;</b></b> 
		<div class=""tboxw"">
			<div class=""wrap"">
				<div class=""tshade"">
				</div>
<!-- begin content -->
	
				<div id=""innerframe"" class=""project"">
					<div class=""topnav"">
						<h2><span>CH3ETAH Code Generation IDE</span>
							<small>
								&nbsp; 
								&nbsp; 
								<a href=""/project/stats/?group_id=118003&amp;ugn=ch3etah"" class=""stats"">Stats - Activity: 98.51%</a> 
							
							<span class=""rss"">

								<a href=""/export/rss2_project.php?group_id=118003"">RSS</a>
							</span>
							</small>
						</h2>
												<ul class=""nav"">
							<li class=""begin""><a href=""/projects/ch3etah/"">Summary</a></li>
<li><a href=""/project/admin/?group_id=118003"">Admin</a></li>
<li><a href=""http://ch3etah.sourceforge.net"">Home Page</a></li>
<li><a href=""/forum/?group_id=118003"">Forums</a></li>
<li><a href=""/tracker/?group_id=118003"">Tracker</a></li>
<li class=""selected""><a href=""/tracker/?group_id=118003&amp;atid=679758"">Bugs</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679759"">Support Requests</a></li>
<li><a href=""/tracker/?group_id=118003&amp;atid=679761"">Feature Requests</a></li>
<li><a href=""/mail/?group_id=118003"">Mail</a></li>
<li><a href=""/pm/?group_id=118003"">Tasks</a></li>
<li><a href=""/news/?group_id=118003"">News</a></li>
<li><a href=""/cvs/?group_id=118003"">CVS</a></li>
<li><a href=""/project/showfiles.php?group_id=118003"">Files</a></li>
			
						</ul>
																		<ul class=""nav"">
							<li class=""begin""><a href=""/tracker/?func=add&amp;group_id=118003&amp;atid=679758"">Submit New</a></li>
<li class=""selected""><a href=""/tracker/?func=browse&amp;group_id=118003&amp;atid=679758"">Browse</a></li>
<li><a href=""/tracker/admin/?group_id=118003"">Admin</a></li>
<li><a href=""/search/index.php?type_of_search=artifact&amp;group_id=118003&amp;atid=679758"">Search</a></li>
			
						</ul>
											</div>
                                        <br class=""break"" />
<!-- begin right column -->
                    

<h2>[ 1351554 ] \&quot;Packages\&quot; node... doesn&#039;t work</h2>
<table cellpadding=""0"" width=""100%"">
	<tr>
		<td colspan=""2"">
			You may monitor this Tracker item after you 
			<a href=""/account/login.php"">login</a> 
			(<a href=""/account/register.php"">register an account, 
			if you do not already have one</a>)
		</td>
	</tr>
	<tr>
		<td>
			<b>Submitted By:</b>
			<br>
			Igor Abade - <a href=""/users/igoravl/"">igoravl</a><a href=""/help/icon_legend.php?context=group_admin&amp;uname=igoravl&amp;this_group=118003&amp;return_to=%2F""><img src=""http://images.sourceforge.net/images/icons/prj_adm.png"" alt=""Project Admin"" width=""16"" height=""16"" /></a> 
		</td>
		
		<td>
			<b>Date Submitted:</b>
			<br>
			2005-11-08 09:56
		</td>
	</tr>
	
	
	<tr>
		<td>
			<b>Last Updated By:</b>
			<br>
			Item Submitter - Tracker Item Submitted
		</td>
		<td>
			<b>Date Last Updated:</b>
			<br>
							No updates since submission
					</td>
	</tr>

	<tr>
		<td>
			<b>Number of Comments:</b>
			<br>
			0
		</td>
		<td>
			<b>Number of Attachments:</b>
			<br>
			0
		</td>
	</tr>

	<tr>
		<td>
			<b>Category: <a href=""javascript:help_window('/help/tracker.php?helpname=category')"">(?)</a></b>
			<br>
			None
		</td>
		<td>
			<b>Group: <a href=""javascript:help_window('/help/tracker.php?helpname=group')"">(?)</a></b>
			<br>
			None
		</td>
	</tr>

	<tr>
		<td>
			<b>Assigned To: <a href=""javascript:help_window('/help/tracker.php?helpname=assignee')"">(?)</a></b>
			<br>
			Nobody/Anonymous
		</td>
		<td>
			<b>Priority: <a href=""javascript:help_window('/help/tracker.php?helpname=priority')"">(?)</a></b>
			<br>
			5
		</td>
	</tr>

	<tr>
		<td>
			<b>Status: <a href=""javascript:help_window('/help/tracker.php?helpname=status')"">(?)</a></b>
			<br>
			Open
		</td>
					<td>
				<b>Resolution: <a href=""javascript:help_window('/help/tracker.php?helpname=resolution')"">(?)</a></b>
				<br>
				None
			</td>
			</tr>
	
	<tr>
		<td colspan=""2"">
			<b>Summary: <a href=""javascript:help_window('/help/tracker.php?helpname=summary')"">(?)</a></b>
			<br>
			\&quot;Packages\&quot; node... doesn&#039;t work
		</td>
	</tr>

	<tr>
		<td colspan=""2"">
			\&quot;Packages\&quot; node... doesn&#039;t work.<br />
When a data source name is changed thru the Properties<br />
window, its new name is not shown in the window tab
						<p>
			<form action=""/tracker/index.php"" method=""post"">
				<input type=""hidden"" name=""group_id"" value=""118003"">
				<input type=""hidden"" name=""atid"" value=""679758"">
				<input type=""hidden"" name=""func"" value=""postaddcomment"">
				<input type=""hidden"" name=""artifact_id"" value=""1351554"">
				<b>Add a Comment:</b>
				<br>
				<textarea name=""details"" rows=""10"" cols=""60"" wrap=""hard""></textarea>
		</td>
	</tr>
	<tr>
		<td colspan=""2"">
							<h3 class=""error"">
						Please <a href=""/account/login.php?return_to=%2Ftracker%2Findex.php%3Ffunc%3Ddetail%26aid%3D1351554%26group_id%3D118003%26atid%3D679758"">log in!</a>
				</h3>
				<br>
				<p>Tracker items submitted anonymously should include a valid 
				email address in the detailed description field.  You will not 
				receive notification of changes to Tracker items submitted 
				anonymously.
						<p>
			<h3>DO NOT enter passwords or other confidential information!</h3>
			<p>
			<input type=""submit"" name=""submit"" value=""SUBMIT"">
			</form>
					</td>
	</tr>

	<tr>
		<td colspan=""2"">
			<h3><br />Followups:</h3>
			<p>
							<h3>No follow-up comments have been posted.</h3>
					</td>
	</tr>

	<tr>
		<td colspan=""2"">
			&nbsp;
		</td>
	</tr>
	
	<tr>
		<td colspan=""2"">
			<h4>Attached Files:</h4>
			
<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""3"">
<tr bgcolor=""#ffffff"">
<td align=""middle""><font color=""#000000""><b>&nbsp;<b></font></td>
<td align=""middle""><font color=""#000000""><b>Name<b></font></td>
<td align=""middle""><font color=""#000000""><b>Description<b></font></td>
<td align=""middle""><font color=""#000000""><b>Download<b></font></td>
</tr>

							<tr>
					<td colspan=""3"">No Files Currently Attached</td>
				</tr>
						</table>
		</td>
	</tr>

	<tr>
		<td colspan=""2"">&nbsp;</td>
	</tr>
	<tr>
		<td colspan=""2"">
			<h3>Changes:</h3>
			<p>
							<h3>No Changes Have Been Made to This Item</h3>
					</td>
	</tr>

</table>

			<br class=""break"" />
					<div id=""btmad"">
						<div id=""ad34"">
			<div class=""tbarhigh"">
				<b class=""ttop""><b class=""t1"">&nbsp;</b><b class=""t2"">&nbsp;</b></b>
				<div class=""tbox"">
					<h3> Find a Tech Job </h3>
				</div>
				<b class=""tbtm""><b class=""t2 tbg"">&nbsp;</b><b class=""t1 tbg"">&nbsp;</b></b>
			</div><div class=""sponsor"">
			
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p34_left_fixed_utility -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p34_left_fixed_utility.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   <!-- here -->	
		</div> 
			</div>
						<div class=""dual"">
							<div id=""ad7"">
								
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p7_bottom_left_sky -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='RON_Other_Skyscraper';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p7_bottom_left_sky.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   
							</div>
							<div id=""ostgservices"">
								
<!-- BEGIN: AdSolution-Website-Tag 4.2 : SourceForge / none_p40_bottom_right_svcs -->
<script language='javascript' type='text/javascript'>
Ads_kid=0;Ads_bid=0;Ads_xl=0;Ads_yl=0;Ads_xp='';Ads_yp='';Ads_xp1='';Ads_yp1='';Ads_opt=0;
Ads_wrd='ch3etah,frameworks,by_industrysector,mswin_all32bit,os_groups,informationtechnology,csharp,codegen,development';
Ads_prf='logged_in=0;page=/tracker/index.php';Ads_par='';Ads_cnturl='';Ads_sec=0;Ads_channels='';
</script>
<script type='text/javascript' language='javascript' src='http://a.as-us.falkag.net/dat/njf/104/sourceforge/none_p40_bottom_right_svcs.js'></script>
<!-- END:AdSolution-Tag 4.2 -->
   
							</div>
						</div>
					</div>	
				</div>
				<div class=""bshade"">
				</div>
			</div>
		</div>
		<b class=""tbtmw""><b class=""t2w tbgw"">&nbsp;</b><b class=""t1w tbgw"">&nbsp;</b></b> 
	</div>
</div>
<div id=""footer"">
	<ul>
		<li><a href=""/docs/about"">About SourceForge.net</a></li>
		<li><a href=""http://www.ostg.com/about/"">About OSTG</a></li>
		<li><a href=""/tos/privacy.php"">Privacy Statement</a></li>
		<li><a href=""/tos/tos.php"">Terms of Use</a></li>
		<li><a href=""http://www.ostg.com/advertising/"">Advertise</a></li>
		<li><a href=""/support/getsupport.php"">Get Support</a></li>
		<li><a href=""/export/rss2_sfnews.php?group_id=1&amp;rss_fulltext=1"">RSS</a></li>
	</ul>
	<p>
		Powered by the <a href=""http://www.vasoftware.com/sourceforge/index.php"">SourceForge&reg;</a> collaborative development environment from VA Software 
		<br />
		&copy;Copyright 2006 - <a href=""http://ostg.com"">OSTG</a> Open Source Technology Group, All Rights Reserved 
	</p>

</div>
</body>
</html>
";
		#endregion constants
		
		[Test]
		public void TestHtmlEncodedSummary()
		{
			TrackerItem item =
				new TrackerItem(HTML_ENCODED_SUMMARY_AND_MATCHING_DESCRIPTION);
			Assert.AreEqual(@"\""Packages\"" node... doesn't work"
				, item.Summary
				, "Incorrect summary");
		}
		
		[Test]
		public void TestDescriptionMatchesSummary_WithBackSlash()
		{
			// Make sure we get the correct description when
			// the beginning of the description is the same as
			// the summary.
			// Also tests that description parsing works when the 
			// summary has a backslash character in it.
			TrackerItem item =
				new TrackerItem(HTML_ENCODED_SUMMARY_AND_MATCHING_DESCRIPTION);
			Assert.Greater(item.Description.Length
				, item.Summary.Length
				, "Item description should be longer than the summary.");
			Assert.AreEqual(item.Summary
				, item.Description.Substring(0, item.Summary.Length)
				, "Item description does not start with summary text.");
		}
		#endregion HTML_ENCODED_SUMMARY_AND_MATCHING_DESCRIPTION

	}
}
