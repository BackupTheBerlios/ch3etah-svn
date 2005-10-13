using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Dialogs {

	/// <summary>
	/// Standard dialog for creating new projects
	/// </summary>
	public class ProjectNew : Form {

		#region Constructors and members variables

		#region Member variables

		private Label label3;
		private Label label4;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlListsArea;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlRight;
		private System.Windows.Forms.Label lblTemplateDescription;
		private System.Windows.Forms.Label label5;

		#endregion
		private System.Windows.Forms.Label lblProjectName;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Button btnBrowseLocation;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.Button btnCancel;
		private System.ComponentModel.IContainer components;

		public ProjectNew() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Load project templates
			InitializeDialog();
		}

		#region Windows Form Designer generated code

		#region Dispose

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Blank Project", 0);
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectNew));
			this.pnlTop = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pnlListsArea = new System.Windows.Forms.Panel();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.lblTemplateDescription = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.btnBrowseLocation = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.lblProjectName = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.pnlTop.SuspendLayout();
			this.pnlListsArea.SuspendLayout();
			this.pnlRight.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.BackColor = System.Drawing.Color.White;
			this.pnlTop.Controls.Add(this.label4);
			this.pnlTop.Controls.Add(this.label3);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(576, 48);
			this.pnlTop.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(24, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(313, 17);
			this.label4.TabIndex = 1;
			this.label4.Text = "Select a project template from one of the categories below";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.307693F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(16, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 18);
			this.label3.TabIndex = 0;
			this.label3.Text = "New Project";
			// 
			// pnlListsArea
			// 
			this.pnlListsArea.Controls.Add(this.pnlRight);
			this.pnlListsArea.Controls.Add(this.splitter1);
			this.pnlListsArea.Controls.Add(this.pnlLeft);
			this.pnlListsArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlListsArea.DockPadding.All = 4;
			this.pnlListsArea.Location = new System.Drawing.Point(0, 48);
			this.pnlListsArea.Name = "pnlListsArea";
			this.pnlListsArea.Size = new System.Drawing.Size(576, 228);
			this.pnlListsArea.TabIndex = 6;
			// 
			// pnlRight
			// 
			this.pnlRight.Controls.Add(this.splitter2);
			this.pnlRight.Controls.Add(this.lblTemplateDescription);
			this.pnlRight.Controls.Add(this.listView1);
			this.pnlRight.Controls.Add(this.label2);
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRight.Location = new System.Drawing.Point(123, 4);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(449, 220);
			this.pnlRight.TabIndex = 14;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 169);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(449, 3);
			this.splitter2.TabIndex = 9;
			this.splitter2.TabStop = false;
			// 
			// lblTemplateDescription
			// 
			this.lblTemplateDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblTemplateDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblTemplateDescription.Location = new System.Drawing.Point(0, 172);
			this.lblTemplateDescription.Name = "lblTemplateDescription";
			this.lblTemplateDescription.Size = new System.Drawing.Size(449, 48);
			this.lblTemplateDescription.TabIndex = 8;
			this.lblTemplateDescription.Text = "Creates a blank project with no templates or packages assigned to it";
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1});
			this.listView1.Location = new System.Drawing.Point(0, 16);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(449, 204);
			this.listView1.SmallImageList = this.imageList2;
			this.listView1.TabIndex = 5;
			this.listView1.View = System.Windows.Forms.View.List;
			// 
			// imageList2
			// 
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Silver;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(449, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Project Template";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(120, 4);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 220);
			this.splitter1.TabIndex = 13;
			this.splitter1.TabStop = false;
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.treeView1);
			this.pnlLeft.Controls.Add(this.label1);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(4, 4);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(116, 220);
			this.pnlLeft.TabIndex = 12;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(0, 16);
			this.treeView1.Name = "treeView1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("Standard"),
																				  new System.Windows.Forms.TreeNode("FórumAccess")});
			this.treeView1.ShowRootLines = false;
			this.treeView1.Size = new System.Drawing.Size(116, 204);
			this.treeView1.TabIndex = 1;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Project Categories";
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.btnBrowseLocation);
			this.pnlBottom.Controls.Add(this.textBox2);
			this.pnlBottom.Controls.Add(this.lblLocation);
			this.pnlBottom.Controls.Add(this.textBox1);
			this.pnlBottom.Controls.Add(this.lblProjectName);
			this.pnlBottom.Controls.Add(this.btnOK);
			this.pnlBottom.Controls.Add(this.btnCancel);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 276);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(576, 72);
			this.pnlBottom.TabIndex = 8;
			// 
			// btnBrowseLocation
			// 
			this.btnBrowseLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseLocation.Location = new System.Drawing.Point(384, 40);
			this.btnBrowseLocation.Name = "btnBrowseLocation";
			this.btnBrowseLocation.Size = new System.Drawing.Size(64, 23);
			this.btnBrowseLocation.TabIndex = 4;
			this.btnBrowseLocation.Text = "&Browse...";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(72, 40);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(304, 21);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "textBox2";
			// 
			// lblLocation
			// 
			this.lblLocation.AutoSize = true;
			this.lblLocation.Location = new System.Drawing.Point(16, 40);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(52, 17);
			this.lblLocation.TabIndex = 2;
			this.lblLocation.Text = "&Location:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(72, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(376, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "textBox1";
			// 
			// lblProjectName
			// 
			this.lblProjectName.AutoSize = true;
			this.lblProjectName.Location = new System.Drawing.Point(16, 8);
			this.lblProjectName.Name = "lblProjectName";
			this.lblProjectName.Size = new System.Drawing.Size(39, 17);
			this.lblProjectName.TabIndex = 0;
			this.lblProjectName.Text = "&Name:";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(488, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 24);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(488, 40);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			// 
			// label5
			// 
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(0, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(576, 2);
			this.label5.TabIndex = 9;
			// 
			// ProjectNew
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(576, 348);
			this.ControlBox = false;
			this.Controls.Add(this.label5);
			this.Controls.Add(this.pnlListsArea);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.pnlBottom);
			this.Font = new System.Drawing.Font("Tahoma", 8F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectNew";
			this.ShowInTaskbar = false;
			this.Text = "New Project";
			this.pnlTop.ResumeLayout(false);
			this.pnlListsArea.ResumeLayout(false);
			this.pnlRight.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.pnlBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#endregion

		#region Template loading

		protected virtual void InitializeDialog() {

		}

		#endregion
	}
}