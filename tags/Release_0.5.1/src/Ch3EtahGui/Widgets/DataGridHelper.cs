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
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Some helper functions for binding collection-based
	///  data sources to a DataGrid.
	/// </summary>
	public class DataGridHelper
	{
		private DataGridHelper(){
		}

		public static void SetGridDataSource(DataGrid grid, object dataSource) {
			grid.DataSource = dataSource;
			DataGridTableStyle ts = new DataGridTableStyle((CurrencyManager) grid.BindingContext[dataSource]);
			grid.TableStyles.Clear();
			grid.TableStyles.Add(ts);
			ts.GridColumnStyles.Clear();
		}

		public static DataGridColumnStyle AddDataGridColumn(DataGrid grid, string propertyName, string headerText) {
			PropertyDescriptorCollection props =((CurrencyManager) grid.BindingContext[grid.DataSource]).GetItemProperties();
			PropertyDescriptor prop = props[propertyName];
			DataGridColumnStyle style = null;
			if (prop == null) {
				style = new DataGridTextBoxColumn();
			}
			else if (prop.PropertyType == typeof(bool)) {
				style = new DataGridBoolColumn(prop, true);
				((DataGridBoolColumn)style).AllowNull = false;
			}
			else {
				style = new DataGridTextBoxColumn(prop, true);
			}
			grid.TableStyles[0].GridColumnStyles.Add(style);
			style.HeaderText = headerText;
			
			return style;
		}

		public static DataGridColumnStyle AddDataGridColumn(DataGrid grid, string propertyName, string headerText, Type columnType) {
			PropertyDescriptorCollection props =((CurrencyManager) grid.BindingContext[grid.DataSource]).GetItemProperties();
			PropertyDescriptor prop = props[propertyName];
			DataGridColumnStyle style = null;
			if (prop == null) {
				style = (DataGridColumnStyle)Activator.CreateInstance(columnType);
			}
			else {
				style = (DataGridColumnStyle)Activator.CreateInstance(columnType, new object[] {prop, true});
			}
			style.PropertyDescriptor = prop;
			grid.TableStyles[0].GridColumnStyles.Add(style);
			style.HeaderText = headerText;
			
			return style;
		}
	}
}
