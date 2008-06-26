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
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 17/10/2004
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Description of InputParameterCollectionEditor.	
	/// </summary>
	public class InputParameterCollectionEditor : System.Windows.Forms.UserControl, IObjectEditor
	{
		private System.Windows.Forms.DataGrid gridInputParameters;
		private System.Windows.Forms.Button btnAddInputParameter;
		private System.Windows.Forms.Button btnRemoveInputParameter;
		
		#region Constructors and member variables
		InputParameterCollection _parameters;
		
		public InputParameterCollectionEditor()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.Enabled = false;
		}
		
		public InputParameterCollectionEditor(InputParameterCollection parameters) : this()
		{
			_parameters = parameters;
		}
		#endregion Constructors and member variables
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.btnRemoveInputParameter = new System.Windows.Forms.Button();
			this.btnAddInputParameter = new System.Windows.Forms.Button();
			this.gridInputParameters = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.gridInputParameters)).BeginInit();
			this.SuspendLayout();
			// 
			// btnRemoveInputParameter
			// 
			this.btnRemoveInputParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRemoveInputParameter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveInputParameter.Location = new System.Drawing.Point(80, 272);
			this.btnRemoveInputParameter.Name = "btnRemoveInputParameter";
			this.btnRemoveInputParameter.Size = new System.Drawing.Size(80, 23);
			this.btnRemoveInputParameter.TabIndex = 14;
			this.btnRemoveInputParameter.Text = "Remove";
			this.btnRemoveInputParameter.Visible = false;
			// 
			// btnAddInputParameter
			// 
			this.btnAddInputParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddInputParameter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddInputParameter.Location = new System.Drawing.Point(0, 272);
			this.btnAddInputParameter.Name = "btnAddInputParameter";
			this.btnAddInputParameter.Size = new System.Drawing.Size(80, 23);
			this.btnAddInputParameter.TabIndex = 13;
			this.btnAddInputParameter.Text = "Add";
			this.btnAddInputParameter.Click += new System.EventHandler(this.btnAddInputParameter_Click);
			// 
			// gridInputParameters
			// 
			this.gridInputParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
						| System.Windows.Forms.AnchorStyles.Left) 
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gridInputParameters.BackgroundColor = System.Drawing.SystemColors.Window;
			this.gridInputParameters.CaptionVisible = false;
			this.gridInputParameters.DataMember = "";
			this.gridInputParameters.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridInputParameters.Location = new System.Drawing.Point(0, 0);
			this.gridInputParameters.Name = "gridInputParameters";
			this.gridInputParameters.ParentRowsVisible = false;
			this.gridInputParameters.PreferredColumnWidth = 110;
			this.gridInputParameters.Size = new System.Drawing.Size(272, 272);
			this.gridInputParameters.TabIndex = 12;
			// 
			// InputParameterCollectionEditor
			// 
			this.Controls.Add(this.btnRemoveInputParameter);
			this.Controls.Add(this.btnAddInputParameter);
			this.Controls.Add(this.gridInputParameters);
			this.Name = "InputParameterCollectionEditor";
			this.Size = new System.Drawing.Size(272, 296);
			this.Load += new System.EventHandler(this.InputParameterCollectionEditorLoad);
			((System.ComponentModel.ISupportInitialize)(this.gridInputParameters)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
		
		public event EventHandler SelectedObjectChanged;
		protected virtual void OnSelectedObjectChanged() {
			if (this.SelectedObjectChanged != null) {
				SelectedObjectChanged(this.SelectedObject, new EventArgs());
			}
		}
		
		#region DoBinding
		private void DoBinding() {
			gridInputParameters.DataSource = _parameters;
			this.Enabled = true;
		}
		#endregion DoBinding
		
		public void RefreshContent()
		{
			this.SelectedObject = this.SelectedObject;
		}
		
		#region SelectedObject
		public object SelectedObject 
		{
			get {
				return _parameters;
			}
			set {
				_parameters = (InputParameterCollection)value;
				DoBinding();
			}
		}
		#endregion SelectedObject
		
		#region IsDirty
		public bool IsDirty {
			get {
				return false;
			}
		}
		#endregion IsDirty
		
		#region Icon
		public Icon Icon {
			get {
				return Images.CreateIcon(Images.DocumentArrowGreen);
			}
		}
		#endregion Icon
		
		#region ToString
		public override string ToString() {
			return "Input Parameters";
		}
		#endregion ToString
		
		#region CommitChanges
		public bool CommitChanges() {
			gridInputParameters.SetDataBinding(null, "");
			DoBinding();
			return true;
		}
		#endregion CommitChanges

		#region QueryUnload
		public void QueryUnload(out bool cancel) {
			cancel = (!CommitChanges());
		}
		#endregion QueryUnload

		#region Events
		void btnAddInputParameter_Click(object sender, System.EventArgs e)
		{
			_parameters.Add(new InputParameter());
		}
		void InputParameterCollectionEditorLoad(object sender, System.EventArgs e)
		{
			if (_parameters != null) {
				DoBinding();
			}
		}
		#endregion Events
		
		
	}
}
