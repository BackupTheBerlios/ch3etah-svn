using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Ch3Etah.Core.Config;

namespace Ch3Etah.Gui
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class SettingsDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureHeader;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.TreeView tvwCategories;
		private System.Windows.Forms.Panel panelButtons;
		private System.Windows.Forms.Panel panelContent;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SettingsDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SettingsDialog));
			this.pictureHeader = new System.Windows.Forms.PictureBox();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.tvwCategories = new System.Windows.Forms.TreeView();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.panelContent = new System.Windows.Forms.Panel();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.panelButtons.SuspendLayout();
			this.panelContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureHeader
			// 
			this.pictureHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureHeader.Location = new System.Drawing.Point(0, 0);
			this.pictureHeader.Name = "pictureHeader";
			this.pictureHeader.Size = new System.Drawing.Size(640, 24);
			this.pictureHeader.TabIndex = 29;
			this.pictureHeader.TabStop = false;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 392);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(640, 22);
			this.statusBar1.TabIndex = 34;
			// 
			// tvwCategories
			// 
			this.tvwCategories.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tvwCategories.HideSelection = false;
			this.tvwCategories.HotTracking = true;
			this.tvwCategories.ImageIndex = -1;
			this.tvwCategories.ItemHeight = 16;
			this.tvwCategories.LabelEdit = true;
			this.tvwCategories.Location = new System.Drawing.Point(0, 24);
			this.tvwCategories.Name = "tvwCategories";
			this.tvwCategories.SelectedImageIndex = -1;
			this.tvwCategories.ShowRootLines = false;
			this.tvwCategories.Size = new System.Drawing.Size(176, 368);
			this.tvwCategories.TabIndex = 35;
			this.tvwCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwCategories_AfterSelect);
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.btnCancel);
			this.panelButtons.Controls.Add(this.btnOK);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButtons.Location = new System.Drawing.Point(176, 352);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(464, 40);
			this.panelButtons.TabIndex = 36;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(384, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(304, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "Save";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// panelContent
			// 
			this.panelContent.Controls.Add(this.propertyGrid);
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(176, 24);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(464, 328);
			this.panelContent.TabIndex = 37;
			// 
			// propertyGrid
			// 
			this.propertyGrid.CommandsVisibleIfAvailable = true;
			this.propertyGrid.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.LargeButtons = false;
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(464, 328);
			this.propertyGrid.TabIndex = 15;
			this.propertyGrid.Text = "propertyGrid1";
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// SettingsDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 414);
			this.ControlBox = false;
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.panelButtons);
			this.Controls.Add(this.tvwCategories);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.pictureHeader);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Application Settings";
			this.Load += new System.EventHandler(this.Settings_Load);
			this.panelButtons.ResumeLayout(false);
			this.panelContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void Settings_Load(object sender, System.EventArgs e)
		{
			TreeNode node = tvwCategories.Nodes.Add("ORM Settings");
			node.Tag = Ch3EtahConfig.OrmConfiguration;
		}

		private void tvwCategories_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			propertyGrid.SelectedObject = e.Node.Tag;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			Ch3EtahConfig.SaveSettings();
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Ch3EtahConfig.LoadSettings();
			this.Close();
		}

	}
}
