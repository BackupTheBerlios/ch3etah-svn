using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Ch3Etah.BugTracker
{
	public class TrackerItem
	{
		Uri _uri;
		string _html;
		
		string _id = "";
		string _summary = "";
		private string _description = "";
		private DateTime _dateOpened = DateTime.MinValue;
		private DateTime _dateClosed = DateTime.MinValue;
		private TrackerItemStatus _status;

		public TrackerItem(Uri uri)
		{
			_uri = uri;
			_html = GetUrlText(uri.AbsoluteUri);
			ParseHtml();
		}
		public TrackerItem(string html)
		{
			_html = html;
			ParseHtml();
		}
		

		#region Properties
		[Browsable(false)]
		public string ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public DateTime DateOpened
		{
			get { return _dateOpened; }
			set { _dateOpened = value; }
		}

		[Browsable(false)]
		public DateTime DateClosed
		{
			get { return _dateClosed; }
			set { _dateClosed = value; }
		}

		public TrackerItemStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}
		public string Summary
		{
			get { return _summary; }
			set { _summary = value; }
		}

		[Browsable(false)]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		#endregion Properties
		
		private void ParseHtml()
		{
			this.ID = GetRegexValue(_html, @"name=""artifact_id"" value=""(?<Val>\d*)", "Val");
			if (this.ID.Trim() == "")
			{
				if (_uri == null)
				{
					throw new InvalidHtmlContentException(_html, "artifact_id");
				}
				else
				{
					throw new InvalidHtmlContentException(_uri, "artifact_id");
				}
			}
			this.DateOpened = DateTime.Parse(
				GetRegexValue(_html, @"Date Submitted:[\s</br>]*(?<Val>[\d-: ]*)", "Val"));
			string closed = GetRegexValue(_html, @"Closed as of:[\s</br>]*(?<Val>[\d-: ]*)", "Val");
			if (closed.Trim() != "")
			{
				this.DateClosed = DateTime.Parse(closed);
			}
			string rawSummary = GetRegexValue(_html, @"<title>SourceForge.net: Detail: [\d]* - [\s]*(?<Val>[\S\s]*)</title>", "Val");
			string summary = System.Web.HttpUtility.HtmlDecode(rawSummary);
			this.Summary = summary.Trim();
			string descregex = @"Summary:[\S\s]*(<td[\w =""]*>){1}(?<Val>[\S\s]*)<form action=""/tracker/index.php";
			string desc = GetRegexValue(_html, descregex, "Val");
			desc = System.Web.HttpUtility.HtmlDecode(desc);
			desc = desc.Replace("\r\n", "");
			desc = Regex.Replace(desc, @"<p[\s\w/]*>", "\r\n", RegexOptions.IgnoreCase);
			desc = Regex.Replace(desc, @"<br[\s\w/]*>", "\r\n", RegexOptions.IgnoreCase);
			if (desc.Trim() == "")
			{
				Debug.WriteLine(descregex);
			}
			this.Description = desc.Trim();
			string status = GetRegexValue(_html, @"Status:[\S\s]*helpname=status[(\?)'""<>/\w\s]*<br[\s\w/]*>\s*(?<Val>[\w]*)\s*</td>", "Val");
			this.Status = TrackerItemStatus.FindByName(status);
		}

		private string GetUrlText(string url)
		{
			WebRequest request = WebRequest.Create(url);
			WebResponse response = request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(responseStream);
			return reader.ReadToEnd();
		}
		private string GetRegexValue(string input, string pattern, string groupName)
		{
			Regex exp = new Regex(pattern, RegexOptions.IgnoreCase);
			MatchCollection matches = exp.Matches(input);
			if (matches.Count > 0)
			{
				try
				{
					return matches[0].Groups[groupName].Value;
				}
				catch { return ""; }
			}
			return "";
		}

	}
}
