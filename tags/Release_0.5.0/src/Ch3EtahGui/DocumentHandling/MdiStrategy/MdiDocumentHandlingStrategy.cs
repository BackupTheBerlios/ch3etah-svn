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
 *   Date: 25/11/2004
 */

using System;
using System.Collections;
using System.Windows.Forms;

using Ch3Etah.Gui.DocumentHandling;

namespace Ch3Etah.Gui.DocumentHandling.MdiStrategy
{
	/// <summary>
	/// Description of MdiDocumentHandlingStrategy.
	/// </summary>
	public class MdiDocumentHandlingStrategy : IDocumentHandlingStrategy
	{
		private Form _mdiParent = null;
		
		public MdiDocumentHandlingStrategy(Form mdiParentForm)
		{
			_mdiParent = mdiParentForm;
		}
		
		public Form MdiParent {
			get {
				return _mdiParent;
			}
			set {
				_mdiParent = value;
			}
		}
		
		public IObjectEditor[] Editors {
			get {
				ArrayList editors = new ArrayList();
				if (_mdiParent != null) {
					foreach (Form form in _mdiParent.MdiChildren) {
						if (form.GetType() == typeof(ObjectEditorForm)) {
							editors.Add(((ObjectEditorForm)form).ObjectEditor);
						}
					}
				}
				return (IObjectEditor[])editors.ToArray(typeof(IObjectEditor));
			}
		}
		
		public DialogResult ShowEditorDialog(IObjectEditor editor) {
			ObjectEditorForm f = new ObjectEditorForm((IObjectEditor)editor);
			f.StartPosition = FormStartPosition.CenterScreen;
			return f.ShowDialog(_mdiParent);
		}

		public void ShowEditor(IObjectEditor editor) {
			ObjectEditorForm f = FindObjectEditor(editor);
 			if (f == null) {
				f = new ObjectEditorForm((IObjectEditor)editor);
				try {
					f.Show(((IMdiContainer)_mdiParent).DockPanel);
				}
				catch (Exception ex) {
					MessageBox.Show(ex.ToString());
				}
 			}
 			else {
 				f.Activate();
 			}
		}
		
 		private ObjectEditorForm FindObjectEditor(IObjectEditor editor) {
			if (_mdiParent == null) {
				return null;
			}
 			foreach(Object form in ((IMdiContainer) _mdiParent).DockPanel.Contents) {
 				if (form.GetType() == typeof(ObjectEditorForm)) {
 				    if ( ((ObjectEditorForm)form).ObjectEditor.SelectedObject == editor.SelectedObject
					     && ((ObjectEditorForm)form).ObjectEditor.GetType() == editor.GetType()) {
 						return (ObjectEditorForm)form;
 				    }
 				}
 			}
 			return null;
 		}
		
	}
}
