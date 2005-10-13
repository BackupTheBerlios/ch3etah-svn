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
 *   Date: 30/9/2004
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Description of MainPanelBase.	
	/// </summary>
	public abstract class ObjectEditorPanelBase : System.Windows.Forms.UserControl, IObjectEditor
	{
		#region Constructors and Member variables
		protected ObjectEditorPanelBase()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.Dock = DockStyle.Fill;
		}
		#endregion Constructors and Member variables
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			// 
			// ObjectEditorPanelBase
			// 
			this.Name = "ObjectEditorPanelBase";
			this.Size = new System.Drawing.Size(392, 352);
		}
		#endregion
		
		public abstract bool IsDirty { get; }
		
		public abstract object SelectedObject { get; set; }
		
		public abstract Icon Icon { get; }
		
		public abstract override string ToString();
		
		public abstract bool CommitChanges();
		
		public abstract void QueryUnload(out bool cancel);
		
	}
}
