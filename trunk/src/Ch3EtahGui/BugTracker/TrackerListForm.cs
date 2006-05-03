using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using Ch3Etah.BugTracker;
using Ch3Etah.Gui.Widgets;

namespace Ch3Etah.Gui.BugTracker
{
	public class TrackerListForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtSummary;
		private System.Windows.Forms.TextBox txtDateSubmitted;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.TextBox txtDetails;
		private System.Windows.Forms.Panel panelItemList;
		private System.Windows.Forms.Button btnCreateTracker;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGrid gridItemList;
		System.Windows.Forms.Label lblNoData;

		Exception _exception;
		TrackerRepository _repository;
		private System.Windows.Forms.Label lblMessage;
		bool _connected = false;

		public TrackerListForm(Exception ex)
			: this()
		{
			_exception = ex;
		}
		public TrackerListForm()
		{
			_repository = TrackerRepository.GetInstance();
			InitializeComponent();
		}
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TrackerListForm));
			this.lblMessage = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSummary = new System.Windows.Forms.TextBox();
			this.txtDateSubmitted = new System.Windows.Forms.TextBox();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDetails = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.panelItemList = new System.Windows.Forms.Panel();
			this.gridItemList = new System.Windows.Forms.DataGrid();
			this.lblNoData = new System.Windows.Forms.Label();
			this.btnCreateTracker = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.panelItemList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridItemList)).BeginInit();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Location = new System.Drawing.Point(8, 8);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(632, 32);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Please help us avoid duplicate entries. Before submitting a new bug report, pleas" +
				"e look through this list to make sure no one has already subitted a report for t" +
				"he bug you are experiencing.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Report Summary";
			// 
			// txtSummary
			// 
			this.txtSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSummary.Location = new System.Drawing.Point(8, 24);
			this.txtSummary.Name = "txtSummary";
			this.txtSummary.ReadOnly = true;
			this.txtSummary.Size = new System.Drawing.Size(432, 20);
			this.txtSummary.TabIndex = 2;
			this.txtSummary.Text = "";
			// 
			// txtDateSubmitted
			// 
			this.txtDateSubmitted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDateSubmitted.Location = new System.Drawing.Point(448, 24);
			this.txtDateSubmitted.Name = "txtDateSubmitted";
			this.txtDateSubmitted.ReadOnly = true;
			this.txtDateSubmitted.Size = new System.Drawing.Size(88, 20);
			this.txtDateSubmitted.TabIndex = 4;
			this.txtDateSubmitted.Text = "";
			// 
			// txtStatus
			// 
			this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStatus.Location = new System.Drawing.Point(542, 24);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ReadOnly = true;
			this.txtStatus.Size = new System.Drawing.Size(80, 20);
			this.txtStatus.TabIndex = 6;
			this.txtStatus.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 48);
			this.label3.Name = "label3";
			this.label3.TabIndex = 7;
			this.label3.Text = "Report Details";
			// 
			// txtDetails
			// 
			this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDetails.Location = new System.Drawing.Point(8, 64);
			this.txtDetails.Multiline = true;
			this.txtDetails.Name = "txtDetails";
			this.txtDetails.ReadOnly = true;
			this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDetails.Size = new System.Drawing.Size(614, 96);
			this.txtDetails.TabIndex = 8;
			this.txtDetails.Text = "";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(448, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Date Submitted";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(542, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 23);
			this.label5.TabIndex = 10;
			this.label5.Text = "Status";
			// 
			// panelItemList
			// 
			this.panelItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panelItemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelItemList.Controls.Add(this.gridItemList);
			this.panelItemList.Controls.Add(this.txtDetails);
			this.panelItemList.Controls.Add(this.txtSummary);
			this.panelItemList.Controls.Add(this.label2);
			this.panelItemList.Controls.Add(this.txtDateSubmitted);
			this.panelItemList.Controls.Add(this.label4);
			this.panelItemList.Controls.Add(this.txtStatus);
			this.panelItemList.Controls.Add(this.label5);
			this.panelItemList.Controls.Add(this.label3);
			this.panelItemList.Location = new System.Drawing.Point(8, 40);
			this.panelItemList.Name = "panelItemList";
			this.panelItemList.Size = new System.Drawing.Size(632, 312);
			this.panelItemList.TabIndex = 11;
			// 
			// gridItemList
			// 
			this.gridItemList.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
			this.gridItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridItemList.BackColor = System.Drawing.Color.White;
			this.gridItemList.CaptionVisible = false;
			this.gridItemList.DataMember = "";
			this.gridItemList.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridItemList.Location = new System.Drawing.Point(8, 168);
			this.gridItemList.Name = "gridItemList";
			this.gridItemList.ReadOnly = true;
			this.gridItemList.RowHeadersVisible = false;
			this.gridItemList.Size = new System.Drawing.Size(616, 136);
			this.gridItemList.TabIndex = 11;
			this.gridItemList.CurrentCellChanged += new System.EventHandler(this.gridItemList_CurrentCellChanged);
			// 
			// lblNoData
			// 
			this.lblNoData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblNoData.Location = new System.Drawing.Point(40, 152);
			this.lblNoData.Name = "lblNoData";
			this.lblNoData.Size = new System.Drawing.Size(544, 104);
			this.lblNoData.TabIndex = 12;
			this.lblNoData.Text = "No bug tracker items were found matching the specified criteria.";
			this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCreateTracker
			// 
			this.btnCreateTracker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCreateTracker.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCreateTracker.Location = new System.Drawing.Point(8, 360);
			this.btnCreateTracker.Name = "btnCreateTracker";
			this.btnCreateTracker.Size = new System.Drawing.Size(368, 23);
			this.btnCreateTracker.TabIndex = 12;
			this.btnCreateTracker.Text = "&Submit a new bug report...";
			this.btnCreateTracker.Click += new System.EventHandler(this.btnCreateTracker_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(520, 360);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 24);
			this.btnCancel.TabIndex = 13;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// TrackerListForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(648, 390);
			this.Controls.Add(this.lblNoData);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnCreateTracker);
			this.Controls.Add(this.panelItemList);
			this.Controls.Add(this.lblMessage);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "TrackerListForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CH3ETAH Bug Tracker";
			this.Load += new System.EventHandler(this.TrackerListForm_Load);
			this.panelItemList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridItemList)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCreateTracker_Click(object sender, System.EventArgs e)
		{
			SubmitNewBugReport();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void TrackerListForm_Load(object sender, System.EventArgs e)
		{
			LoadItemList();
		}

		private void LoadItemList()
		{
			try
			{
				TrackerItem[] items = GetTrackerItems();
				gridItemList.DataSource = items;
				BindTrackerItemsGrid(items);
				RefreshItemDetails();
				if (_exception != null)
				{
					lblMessage.Text = 
						"Below is a list of bug tracker entries that appear to match the exception which you just experienced. Please help us avoid duplicate entries by looking at this list before adding a new bug report.";
					lblMessage.Font = lblNoData.Font;
				}

				if (items == null || items.Length == 0)
				{
					panelItemList.Visible = false;
					lblNoData.Visible= true;
					SubmitNewBugReport();
				}
				else
				{
					panelItemList.Visible = true;
					lblNoData.Visible= false;
				}
			}
			catch (Exception ex)
			{
				_connected = false;
				panelItemList.Visible = false;
				lblNoData.Visible= true;
				MessageBox.Show("Error loading bug tracker items: \r\n" + ex.ToString());
				SubmitNewBugReport();
			}
		}

		private void BindTrackerItemsGrid(TrackerItem[] items)
		{
			DataGridHelper.SetGridDataSource(gridItemList, items);

			DataGridHelper.AddDataGridColumn(gridItemList, "ID", "ID")
				.Width = 55;
			DataGridColumnStyle date = 
				DataGridHelper.AddDataGridColumn(gridItemList, "DateOpened", "Date Submitted", "yyyy-MM-dd");
			date.Width = 85;
			DataGridHelper.AddDataGridColumn(gridItemList, "Status", "Status")
				.Width = 55;
			DataGridHelper.AddDataGridColumn(gridItemList, "Summary", "Summary")
				.Width = 395;
			
			gridItemList.TableStyles[0].RowHeadersVisible = false;
			gridItemList.TableStyles[0].BackColor = Color.White;
			gridItemList.TableStyles[0].AlternatingBackColor = Color.WhiteSmoke;
		}

		private TrackerItem[] GetTrackerItems()
		{
			TrackerItemList list = _repository.GetTrackerItemList();
			if (list.TrackerItems == null)
			{
				TrackerListLoaderForm loader = new TrackerListLoaderForm(list, 20);
				DialogResult res = loader.ShowDialog();
				_connected = res==System.Windows.Forms.DialogResult.OK;
			}
			else
			{
				_connected = true;
			}

			TrackerItem[] items = list.TrackerItems;
			
			if (items==null || items.Length <= 0 || _exception == null)
			{
				return items;
			}
			else
			{
				return FilterExceptionItems(items, _exception);
			}
		}
		
		private TrackerItem[] FilterExceptionItems(TrackerItem[] items, Exception ex)
		{
			ArrayList filteredItems = new ArrayList();
			for (int i = 0; i < items.Length; i++)
			{
				TrackerItem item = items[i];
				if (item.Description.Replace("\r\n", "")
					.IndexOf(ex.StackTrace.Replace("\r\n", "")) >= 0)
				{
					filteredItems.Add(item);
				}
			}
			return (TrackerItem[]) filteredItems.ToArray(typeof (TrackerItem));
		}

		private void SubmitNewBugReport()
		{
			string errorDesc = "";
			if (_exception != null)
			{
				errorDesc += "CH3ETAH Version: " + Utility.GetCh3EtahVersion() + "\r\n";
				errorDesc += _exception.ToString();
			}
	
			if (_connected)
			{
				TrackerSubmitForm submitForm = new TrackerSubmitForm(errorDesc);
				submitForm.Show();
				//_repository.ResetTrackerItemList();
				this.Close();
			}
			else
			{
				try
				{
					Clipboard.SetDataObject(
						new DataObject(DataFormats.Text, errorDesc)
						, true);
				}
				catch{}
				Utility.OpenUrl(_repository.GetTrackerBrowseUrl());
				//_repository.ResetTrackerItemList();
				this.Close();
			}
		}

		private void gridItemList_CurrentCellChanged(object sender, System.EventArgs e)
		{
			RefreshItemDetails();
		}

		private void RefreshItemDetails()
		{
			txtSummary.Text = "";
			txtStatus.Text = "";
			txtDateSubmitted.Text = "";
			txtDetails.Text = "";
			int idx = gridItemList.CurrentRowIndex;
			TrackerItem[] items = gridItemList.DataSource as TrackerItem[];
			if (items != null && idx < items.Length && items.Length > 0)
			{
				TrackerItem item = items[idx];
				txtSummary.Text = item.Summary;
				txtStatus.Text = item.Status.Name;
				txtDateSubmitted.Text = item.DateOpened.ToShortDateString();
				txtDetails.Text = item.Description;
			}
		}


	}
}
