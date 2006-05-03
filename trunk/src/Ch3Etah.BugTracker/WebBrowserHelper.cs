using System;
using System.Windows.Forms;

using mshtml;

namespace Ch3Etah.BugTracker
{
	public class WebBrowserHelper : System.Windows.Forms.Form
	{
		private AxSHDocVw.AxWebBrowser webBrowser;
		
		public AxSHDocVw.AxWebBrowser WebBrowser
		{
			get { return webBrowser; }
		}

		public WebBrowserHelper()
		{
			InitializeComponent();
		}

//		protected override void Dispose( bool disposing )
//		{
//			webBrowser.Dispose();
//			base.Dispose( disposing );
//		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WebBrowserHelper));
			this.webBrowser = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.webBrowser)).BeginInit();
			this.SuspendLayout();
			// 
			// WebBrowser
			// 
			this.webBrowser.Enabled = true;
			this.webBrowser.Location = new System.Drawing.Point(16, 16);
			this.webBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebBrowser.OcxState")));
			this.webBrowser.Size = new System.Drawing.Size(264, 88);
			this.webBrowser.TabIndex = 3;
			// 
			// WebBrowserHelper
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 118);
			this.ControlBox = false;
			this.Controls.Add(this.webBrowser);
			this.Name = "WebBrowserHelper";
			this.Text = "Processing request...";
			((System.ComponentModel.ISupportInitialize)(this.webBrowser)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public string LoadUrl(string url)
		{
			webBrowser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(WebBrowser_DocumentComplete);
			webBrowser.Silent = true;
			
			StartProcessing();
			webBrowser.Navigate(url);
			WaitForProcessing();
			
			IHTMLDocument2 doc = (IHTMLDocument2)webBrowser.Document;
			return doc.body.outerHTML;
		}
		
		public void StartProcessing()
		{
			_processing = true;
		}

		public void WaitForProcessing()
		{
			while (_processing)
			{
				Application.DoEvents();
			}
		}

		private bool _processing;
		
		private void WebBrowser_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
			_processing = false;
		}

		public HTMLInputElement GetHtmlField(string fieldName)
		{
			IHTMLDocument2 doc = (IHTMLDocument2)WebBrowser.Document;
			HTMLInputElement fld = doc.all.item(fieldName, null) as HTMLInputElement;
			if (fld == null)
			{
				Console.WriteLine(doc.body.outerHTML);
				throw new ApplicationException(
					"Error finding field '" + fieldName 
					+ "' on the SourceForge tracker page. No HTML control with the specified ID was found.");
			}
			return fld;
		}


	}
}
