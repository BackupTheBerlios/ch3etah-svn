/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 *
 *   User: Jacob Eggleston
 *   Date: 2005/7/28
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Adapdev.Data.Schema;

using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Summary description for DbEntitySelector.
	/// </summary>
	public class DbEntitySelector : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView tvwEntities;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DataSource _dataSource;
		private TableSchemaCollection _selectedTables;

		public DbEntitySelector(DataSource dataSource)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_dataSource = dataSource;
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
			this.tvwEntities = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tvwEntities
			// 
			this.tvwEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwEntities.ImageIndex = -1;
			this.tvwEntities.Location = new System.Drawing.Point(8, 32);
			this.tvwEntities.Name = "tvwEntities";
			this.tvwEntities.SelectedImageIndex = -1;
			this.tvwEntities.Size = new System.Drawing.Size(280, 256);
			this.tvwEntities.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(280, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select the entities to add to this project";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(128, 296);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(208, 296);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// DbEntitySelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 334);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tvwEntities);
			this.Controls.Add(this.label1);
			this.MinimizeBox = false;
			this.Name = "DbEntitySelector";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "DbEntitySelector";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e) {
			_selectedTables = null;
			this.DialogResult = DialogResult.Cancel;
		}

		public DataSource DbDataSource {
			get { return _dataSource; }
		}
		
		public TableSchemaCollection SelectedTables {
			get { return _selectedTables; }
		}

	}
}
