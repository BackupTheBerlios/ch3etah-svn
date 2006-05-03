using System;

namespace Ch3Etah.BugTracker
{
	public class InvalidHtmlContentException : Exception
	{
		public InvalidHtmlContentException(Uri uri, string fieldName)
			: base("Unrecognized HTML content at the URL '" 
			+ uri.AbsoluteUri + "'. Could not parse the '" + fieldName + "' field from the HTML at the specified address."){}
		public InvalidHtmlContentException(string html, string fieldName)
			: base("Unrecognized HTML content. Could not parse the '" + fieldName + "' field from the specified HTML. " 
			+ html){}
	}

	public class TrackerProcessingException : Exception
	{
		public TrackerProcessingException(Exception ex)
			: base("The list of tracker items could not be retrieved because of the following error: "
			+ ex.Message, ex){}
	}

}
