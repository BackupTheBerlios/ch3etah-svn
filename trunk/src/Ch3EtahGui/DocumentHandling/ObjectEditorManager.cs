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
 *   Date: 24/11/2004
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;

using Ch3Etah.Gui.DocumentHandling.MdiStrategy;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Description of ObjectEditorManager.
	/// </summary>
	public class ObjectEditorManager
	{
		static IDocumentHandlingStrategy _handlingStrategy = new MdiDocumentHandlingStrategy(null);
		
		private ObjectEditorManager()
		{
		}
		
		public static IDocumentHandlingStrategy HandlingStrategy {
			get {
				return _handlingStrategy;
			}
			set {
				_handlingStrategy = value;
			}
		}
		
 		public static void OpenObjectEditor(IObjectEditor editor) {
			_handlingStrategy.ShowEditor(editor);
 		}
		public static DialogResult OpenObjectEditorDialog(IObjectEditor editor) {
			return _handlingStrategy.ShowEditorDialog(editor);
		}
		
		public static IObjectEditor FindObjectEditor(object editorContext)
		{
			return _handlingStrategy.FindObjectEditor(editorContext);
		}

		
	}
}
