using System.Windows.Forms;

using Ch3Etah.BugTracker;

namespace Ch3Etah.Gui.BugTracker
{
	public class TrackerListLoaderForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblStatusMessage;

		private TrackerItemList _list;
		private int _maxItems;
		public TrackerListLoaderForm(TrackerItemList list, int maxItems)
		{
			_maxItems = maxItems;
			_list = list;
			_list.TrackerLoadStatusChanged += new TrackerLoadStatusEventHandler(TrackerItemList_TrackerLoadStatusChanged);
			_list.TrackerItemsLoaded += new TrackerItemsAffectedEventHandler(TrackerItemList_TrackerItemsLoaded);
			_list.TrackerLoadingFailed += new System.UnhandledExceptionEventHandler(TrackerItemList_TrackerLoadingFailed);
			InitializeComponent();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TrackerListLoaderForm));
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblStatusMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(8, 56);
			this.progressBar.Maximum = 20;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(368, 23);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 0;
			this.progressBar.Value = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(304, 88);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(41, 41);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblStatusMessage
			// 
			this.lblStatusMessage.Location = new System.Drawing.Point(72, 16);
			this.lblStatusMessage.Name = "lblStatusMessage";
			this.lblStatusMessage.Size = new System.Drawing.Size(304, 32);
			this.lblStatusMessage.TabIndex = 3;
			this.lblStatusMessage.Text = "Please Wait. Connecting to the bug tracker server...";
			// 
			// TrackerListLoaderForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(386, 120);
			this.ControlBox = false;
			this.Controls.Add(this.lblStatusMessage);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TrackerListLoaderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Loading CH3ETAH Bug Tracker List";
			this.Load += new System.EventHandler(this.TrackerListLoaderForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TrackerItemList_TrackerLoadStatusChanged(object sender, TrackerLoadStatusEventArgs e)
		{
			lblStatusMessage.Text = e.StatusMessage;
			progressBar.Minimum = 0;
			progressBar.Maximum = e.MaxSteps;
			progressBar.Value = e.StepNumber;
			Application.DoEvents();
		}

		private void TrackerItemList_TrackerItemsLoaded(object sender, TrackerItemsAffectedEventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void TrackerItemList_TrackerLoadingFailed(object sender, System.UnhandledExceptionEventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void TrackerListLoaderForm_Load(object sender, System.EventArgs e)
		{
			_list.LoadTrackerItems(_maxItems);
		}
	}
}
