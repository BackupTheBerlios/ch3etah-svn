using System;

using mshtml;

namespace Ch3Etah.BugTracker
{
	public class TrackerItemSubmitFormHtmlWrapper
	{
		IHTMLDocument2 _document;

		public TrackerItemSubmitFormHtmlWrapper(IHTMLDocument2 document)
		{
			_document = document;
		}

		public string Summary
		{
			get { return GetFieldValue("summary"); }
			set { SetFieldValue("summary", value); }
		}

		public string Description
		{
			get { return GetFieldValue("details"); }
			set { SetFieldValue("details", value); }
		}
		
		public HTMLInputElement SummaryTextBox
		{
			get { return GetHtmlField("summary"); }
		}

		public HTMLInputElement DescriptionTextBox
		{
			get { return GetHtmlField("details"); }
		}

		private string GetFieldValue(string fieldName)
		{
			HTMLInputElement fld = GetHtmlField(fieldName);
			return fld.value;
		}

		private void SetFieldValue(string fieldName, string value)
		{
			HTMLInputElement fld = GetHtmlField(fieldName);
			fld.value = value;
		}
		
		private HTMLInputElement GetHtmlField(string fieldName)
		{
			HTMLInputElement fld = _document.all.item(fieldName, null) as HTMLInputElement;
			if (fld == null)
			{
				Console.WriteLine(_document.body.outerHTML);
				throw new ApplicationException(
					"Error finding field '" + fieldName 
					+ "' on the SourceForge tracker page. No HTML control with the specified ID was found.");
			}
			return fld;
		}


	}
}
