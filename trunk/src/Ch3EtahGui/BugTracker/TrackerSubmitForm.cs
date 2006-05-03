using System;
using System.Windows.Forms;

using Ch3Etah.BugTracker;

using mshtml;

using Reflector.UserInterface;

namespace Ch3Etah.Gui.BugTracker
{
	public class TrackerSubmitForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private AxSHDocVw.AxWebBrowser webBrowser;

		private CommandBarManager commandBarManager = new CommandBarManager();
		private CommandBar toolBar = new CommandBar(CommandBarStyle.ToolBar);
		private CommandBarButton cbiNavigateBack;
		private CommandBarButton cbiNavigateForward;
		private CommandBarButton cbiRefresh;
		private System.Windows.Forms.Panel panel1;
		private string _bugReportDetails;

		public TrackerSubmitForm(string bugReportDetails)
			: this()
		{
			_bugReportDetails = bugReportDetails;
		}
		public TrackerSubmitForm()
		{
			InitializeComponent();
			SetupCommandBars();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TrackerSubmitForm));
			this.label1 = new System.Windows.Forms.Label();
			this.webBrowser = new AxSHDocVw.AxWebBrowser();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.webBrowser)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(768, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please try to include any helpfull details you can offer, such as what you were d" +
				"oing when you encountered an error, steps to reproduce the error, upload screens" +
				"hots etc.";
			// 
			// webBrowser
			// 
			this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser.ContainingControl = this;
			this.webBrowser.Enabled = true;
			this.webBrowser.Location = new System.Drawing.Point(8, 40);
			this.webBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webBrowser.OcxState")));
			this.webBrowser.Size = new System.Drawing.Size(768, 446);
			this.webBrowser.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.webBrowser);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(784, 494);
			this.panel1.TabIndex = 5;
			// 
			// TrackerSubmitForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(784, 494);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TrackerSubmitForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Submit a new bug report";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.TrackerSubmitForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.webBrowser)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) 
		{
			try
			{
				// Handle keyboard shortcuts
				if (commandBarManager.PreProcessMessage(ref msg)) 
				{
					return true;
				}
			}
			catch{}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void SetupCommandBars() 
		{
			SetupCommandBarButtons();

			// Toolbar
			//toolBar.NewLine = true;
			toolBar.Items.Add(cbiNavigateBack);
			toolBar.Items.Add(cbiNavigateForward);
			toolBar.Items.Add(cbiRefresh);

			// Add to user interface
			commandBarManager.CommandBars.Add(toolBar);
			Controls.Add(commandBarManager);
		}

		private void SetupCommandBarButtons() 
		{
			cbiNavigateBack =
				new CommandBarButton(
				Images.Back, "Navigate &Back", new EventHandler(NavigateBack_Click), Keys.Alt | Keys.Left);
			cbiNavigateForward =
				new CommandBarButton(
				Images.Forward, "Navigate &Forward", new EventHandler(NavigateForward_Click), Keys.Alt | Keys.Right);
			cbiRefresh =
				new CommandBarButton(Images.Refresh, "&Refresh", new EventHandler(Refresh_Click), Keys.F5);
		}

		private void NavigateBack_Click(object sender, EventArgs e)
		{
			try
			{
				this.webBrowser.GoBack();
			}
			catch{}
		}

		private void NavigateForward_Click(object sender, EventArgs e)
		{
			try
			{
				this.webBrowser.GoForward();
			}
			catch{}
		}

		private void Refresh_Click(object sender, EventArgs e)
		{
			try
			{
				this.webBrowser.Refresh2();
			}
			catch{}
		}

		private void TrackerSubmitForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				string url = TrackerRepository.GetInstance().GetTrackerSubmitUrl();
				this.webBrowser.Navigate(url);
				this.webBrowser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(LoadedTrackerSubmitForm);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading bug tracker submit form: \r\n" + ex.ToString());
			}
		}

		private void LoadedTrackerSubmitForm(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
			try
			{
				this.webBrowser.DocumentComplete -= new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(LoadedTrackerSubmitForm);
			
				TrackerItemSubmitFormHtmlWrapper wrapper = new TrackerItemSubmitFormHtmlWrapper((IHTMLDocument2)this.webBrowser.Document);
				wrapper.Description = _bugReportDetails;
				wrapper.DescriptionTextBox.focus();
				wrapper.SummaryTextBox.focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading bug tracker page: \r\n" + ex.ToString());
			}
		}
	}
}
