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
 *   Date: 2005/7/29
 */

using System;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Implementation to solve problem where you can't
	/// tab to a datagrid field with a combobox.
	/// </summary>
	public class DataGridComboBox : ComboBox
	{
		private const int WM_KEYUP = 0x101;
		
		protected override void WndProc(ref System.Windows.Forms.Message m) {
			// Ignore the KeyUp event when it's on the combo
			if (m.Msg == WM_KEYUP) {
				return;
			}
			else {
				base.WndProc(ref m);
			}
		}
	}
}
