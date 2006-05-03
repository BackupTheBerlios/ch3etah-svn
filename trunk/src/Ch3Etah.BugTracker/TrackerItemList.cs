using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using mshtml;

namespace Ch3Etah.BugTracker
{
	#region Events
	public delegate void TrackerItemsAffectedEventHandler(object sender, TrackerItemsAffectedEventArgs e);
	public delegate void TrackerLoadStatusEventHandler(object sender, TrackerLoadStatusEventArgs e);

	public class TrackerItemsAffectedEventArgs : EventArgs
	{
		public readonly TrackerItem[] AffectedTrackerItems;
		public TrackerItemsAffectedEventArgs(TrackerItem[] affectedItems)
		{
			AffectedTrackerItems = affectedItems;
		}
	}
	
	public class TrackerLoadStatusEventArgs : EventArgs
	{
		public readonly int StepNumber;
		public readonly int MaxSteps;
		public readonly string StatusMessage;

		public TrackerLoadStatusEventArgs(int step, int max, string msg)
		{
			StepNumber = step;
			MaxSteps = max;
			StatusMessage = msg;
		}
	}
	#endregion Events

	public class TrackerItemList
	{
		public event TrackerItemsAffectedEventHandler TrackerItemsLoaded;
		public event UnhandledExceptionEventHandler TrackerLoadingFailed;
		public event TrackerLoadStatusEventHandler TrackerLoadStatusChanged;
		protected void DoTrackerItemsLoaded(TrackerItem[] retrievedItems)
		{
			lock (this)
			{
				_isProcessing = false;
				_trackerItems = retrievedItems;
				_processingError = null;
			}
			if (TrackerItemsLoaded != null)
			{
				TrackerItemsLoaded(this, new TrackerItemsAffectedEventArgs(retrievedItems));
			}
		}
		
		protected void DoTrackerLoadingFailed(Exception ex)
		{
			lock (this)
			{
				_isProcessing = false;
				_trackerItems = null;
				_processingError = ex;
			}
			if (TrackerLoadingFailed != null)
			{
				TrackerLoadingFailed(this, new UnhandledExceptionEventArgs(ex, false));
			}
		}
		
		protected void DoTrackerLoadStatusChanged(int step, int max, string msg)
		{
			if (TrackerLoadStatusChanged != null)
			{
				TrackerLoadStatusChanged(this, new TrackerLoadStatusEventArgs(step, max, msg));
			}
		}

		
		Uri _url;
		TrackerItem[] _trackerItems;
		bool _isProcessing;
		WebBrowserHelper _browserHelper;
		int _maxItems = 10;
		private Exception _processingError;

		protected TrackerItemList(){}

		public TrackerItemList(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentException("You must specify a valid URI.");
			}
			_url = url;
		}
		
		
		public bool IsProcessing
		{
			get { return _isProcessing; }
		}

		public Exception ProcessingError
		{
			get { return _processingError; }
		}
		public TrackerItem[] TrackerItems
		{
			get
			{
				if (ProcessingError != null)
				{
					throw new TrackerProcessingException(ProcessingError);
				}
				return _trackerItems;
			}
		}

		public void ClearList()
		{
			_trackerItems = null;
		}

		/// <summary>
		/// Starts loading the list of tracker items asynchronously.
		/// To receive notification when the list has been loaded, 
		/// the client should subscribe to the TrackerItemsLoaded 
		/// event.
		/// </summary>
		/// <param name="maxItems">
		/// The maximum number of tracker items to process.
		/// </param>
		/// <param name="loadSyncronously">
		/// If true then the method will wait until the list has been
		/// loaded before returning. Otherwise it will return immediately.
		/// </param>
		public void LoadTrackerItems(int maxItems, bool loadSyncronously)
		{
			if (_isProcessing)
			{
				throw new InvalidOperationException(
					"A request is already processing. You must wait for the previous request to finish processing before starting a new request.");
			}
			_isProcessing = true;
			try
			{
				while (loadSyncronously && _isProcessing)
				{
					Application.DoEvents();
				}
				StartTrackerItemGet(maxItems);
			}
			catch(Exception ex)
			{
				_isProcessing = false;
				throw new Exception(ex.Message, ex);
			}
		}

		public void LoadTrackerItems(int maxItems)
		{
			LoadTrackerItems(maxItems, false);
		}
		public void LoadTrackerItems()
		{
			LoadTrackerItems(_maxItems);
		}

		private void StartTrackerItemGet(int maxItems)
		{
			_maxItems = maxItems;
			if (_browserHelper == null)
			{
				_browserHelper = new WebBrowserHelper();
			}
			AxSHDocVw.AxWebBrowser browser = _browserHelper.WebBrowser;
			browser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(CompletedTrackerItemsGet);
			browser.Silent = true;
			browser.Navigate(_url.AbsoluteUri);
		}

		private void CompletedTrackerItemsGet(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
			try
			{
				AxSHDocVw.AxWebBrowser browser = _browserHelper.WebBrowser;
				IHTMLDocument2 doc = (IHTMLDocument2)browser.Document;
			
				HTMLSelectElement cbo = doc.all.item("_status", null) as HTMLSelectElement;
				if (cbo == null)
				{
					throw new ApplicationException(
						"Error finding STATUS drop-down on SourceForge tracker page. No HTML control with the specified ID was found.");
				}

				// we want to make sure that we are listing all of the tracker items.
				if (cbo.value != "100")
				{
					cbo.value = "100";
					HTMLButtonElement btn = doc.all.item("submit", null) as HTMLButtonElement;
					if (btn == null)
					{
						throw new ApplicationException(
							"Error finding SUBMIT button on SourceForge tracker page. No HTML control with the specified ID was found.");
					}
					btn.click();
					return;
				}
				TrackerItem[] items = ProcessTrackerItemList(doc.body.outerHTML);
				DoTrackerItemsLoaded(items);
			}
			catch (Exception ex)
			{
				// If an exception occurs, we need to make sure that 
				// IsProcessing gets set to false and that any observers
				// are notified so that they don't get put into an
				// eternal wait-state.
				DoTrackerLoadingFailed(ex);
			}
		}

		protected virtual TrackerItem[] ProcessTrackerItemList(string html)
		{
			ArrayList items = new ArrayList();
			Uri[] urls = ParseTrackerItemListUrls(html);
			for (int i = 0; i < urls.Length; i++)
			{
				Uri url = urls[i];
				DoTrackerLoadStatusChanged(i+1
					, urls.Length
					, "Retrieving bug tracker items...");
				TrackerItem item = new TrackerItem(url);
				items.Add(item);
			}
			return (TrackerItem[]) items.ToArray(typeof (TrackerItem));
		}
		
		protected Uri[] ParseTrackerItemListUrls(string html)
		{
			ArrayList items = new ArrayList();
			MatchCollection matches = 
				Regex.Matches(html
				, @"href=""/tracker/index.php\?func=detail(?<Address>[\s\w&;_=]*)"">");
			int resultCount = 0;
			foreach (Match match in matches)
			{
				resultCount++;
				if (_maxItems > 0 && resultCount > _maxItems)
				{
					break;
				}
				string url = "http://sourceforge.net/tracker/index.php?func=detail" 
					+ match.Groups["Address"].Value
					.Replace(@"&amp;", "&");
				Uri uri = new Uri(url);
				items.Add(uri);
			}
			return (Uri[]) items.ToArray(typeof (Uri));
		}
		

	}
}
