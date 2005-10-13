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
 *   Date: 4/10/2004
 */

using System;
using System.Windows.Forms;

using Ch3Etah.Gui.DocumentHandling;
using WeifenLuo.WinFormsUI;

namespace Ch3Etah.Gui.DocumentHandling.MdiStrategy
{
	/// <summary>
	/// Description of ObjectEditorForm.	
	/// </summary>
	public class ObjectEditorForm : DockContent
	{
		private IObjectEditor _objectEditor;
		
		public ObjectEditorForm(IObjectEditor editor)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			ObjectEditor = editor;
			this.Controls.Add((Control)editor);
			if (editor is ContainerControl) {
				ContainerControl control = (ContainerControl) editor;
				this.Text = control.ToString();
				control.TextChanged += new EventHandler(control_TextChanged);
			}
			else {
				this.Text = editor.ToString();
			}
			this.Icon = editor.Icon;
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			// 
			// ObjectEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 422);
			this.Name = "ObjectEditorForm";
			this.Text = "ObjectEditorForm";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form_Closing);
			this.Activated += new System.EventHandler(this.ObjectEditorForm_Activated);
			this.Enter += new System.EventHandler(this.ObjectEditorForm_Enter);

		}
		#endregion
		
		public IObjectEditor ObjectEditor {
			get {
				return _objectEditor;
			}
			set {
				_objectEditor = value;
				((Control)_objectEditor).Dock = DockStyle.Fill;
			}
		}
		
		public void UnloadEditorPanel(out bool cancel) {
			cancel = false;
			if (_objectEditor != null) {
				_objectEditor.QueryUnload(out cancel);
				if ( !cancel ) {
					_objectEditor = null;
				}
			}
		}
		
		void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			bool cancel = false;
			UnloadEditorPanel(out cancel);
			e.Cancel = cancel;
		}
		
		void ObjectEditorForm_Activated(object sender, System.EventArgs e)
		{
			try {
				((MainForm)this.MdiParent).SelectContextItem(_objectEditor.SelectedObject);
			}
			catch {}
		}
		
		void ObjectEditorForm_Enter(object sender, System.EventArgs e)
		{
			try {
				((MainForm)this.MdiParent).SelectContextItem(_objectEditor.SelectedObject);
			}
			catch {}
		}

		private void control_TextChanged(object sender, EventArgs e) {
			this.Text = ((ContainerControl) sender).Text;
		}
	}
}
