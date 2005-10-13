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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Provides a data grid column that uses a combo-box
	/// for editing column values.
	/// </summary>
	public class DataGridComboBoxColumn : DataGridTextBoxColumn {
		
		private ComboBox _comboBox;
		private CurrencyManager _currencyManager;
		private int _currentRow;
		
		public DataGridComboBoxColumn() {
			_comboBox = new ComboBox();
			_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
	        _comboBox.Leave += new EventHandler(ComboBox_Leave);
		}
		
		public DataGridComboBoxColumn(PropertyDescriptor property, bool isDefault) : base(property, isDefault) {
			_comboBox = new DataGridComboBox();
			_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_comboBox.Leave += new EventHandler(ComboBox_Leave);
			_comboBox.SelectionChangeCommitted += new EventHandler(_comboBox_SelectionChangeCommitted);
		}

		public ComboBox ComboBox {
			get { return _comboBox; }
		}       
        
		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible) {
			
			base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			
			if (!this.ReadOnly && !readOnly && cellIsVisible) {
				// Save current row and currency manager to 
				// put the selected value in the correct cell 
				_currentRow = rowNum;
				_currencyManager = source;
    
				this.DataGridTableStyle.DataGrid.Scroll += new EventHandler(DataGrid_Scroll);
				
				PositionComboBox();

				_comboBox.SelectedIndex = _comboBox.FindStringExact(this.TextBox.Text);

				_comboBox.Show();
				_comboBox.BringToFront();
				_comboBox.Focus();
			}
		}
		
		private void PositionComboBox() {
			Rectangle rect = this.DataGridTableStyle.DataGrid.GetCurrentCellBounds();
			_comboBox.Parent = this.TextBox.Parent;
			_comboBox.Location = rect.Location;
			_comboBox.Size = new Size(this.TextBox.Size.Width, _comboBox.Size.Height);
			//this.TextBox.Visible = false;
		}

		// Get the display value for a row based on the underlying
		// data source value at that row.
//		protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum) {
//			// Get the underlying data source value at this row.
//			object val =  base.GetColumnValueAtRow(source, rowNum);
//			
//			// setup a temporary combo-box to find the corresponding display
//			// value for this row. This is kind of a hack, but it's simple
//			// and it works.
//			ComboBox comboBox = new ComboBox();
//			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
//			comboBox.DataSource = _comboBox.DataSource;
//			comboBox.DisplayMember = _comboBox.DisplayMember;
//			comboBox.ValueMember = _comboBox.ValueMember;
//			if ((comboBox.DataSource as IList) != null) {
//				foreach (object listVal in (IList)comboBox.DataSource) {
//					comboBox.SelectedItem = listVal;
//					if (comboBox.SelectedValue == val) {
//						return comboBox.SelectedText;
//					}
//				}
//			}
//			return val;
//			comboBox.FindStringExact(text.ToString());
//			return comboBox.SelectedValue;
//
//			// Iterate through the data source bound to the ColumnComboBox
//			CurrencyManager cm = (CurrencyManager) 
//				(this.DataGridTableStyle.DataGrid.BindingContext[_comboBox.DataSource]);
//			// Assumes the associated DataGrid is bound to a DataView or 
//			// DataTable 
//			DataView dataview = ((DataView)cm.List);
//            System.Data                
//			int i;
//			
//			for (i = 0; i < dataview.Count; i++) {
//				if (obj.Equals(dataview[i][_comboBox.ValueMember]))
//					break;
//			}
//        
//			if (i < dataview.Count)
//				return dataview[i][_comboBox.DisplayMember];
//        
//			return DBNull.Value;
//		}

		// Updates the value stored in the grid's underlying 
		// datasource, given the text displayed for the row in question
//		protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value) {
//			// setup a temporary combo-box to find the corresponding 
//			// value for this row. This is kind of a hack, but it's
//			// simple and it works.
//			ComboBox comboBox = new ComboBox();
//			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
//			comboBox.DataSource = _comboBox.DataSource;
//			comboBox.FindStringExact(value.ToString());
//			base.SetColumnValueAtRow(source, rowNum, value);//comboBox.SelectedValue);
//			
//			object s = value;
//
//			// Iterate through the data source bound to the ColumnComboBox
//			CurrencyManager cm = (CurrencyManager) 
//				(this.DataGridTableStyle.DataGrid.BindingContext[_comboBox.DataSource]);
//			// Assumes the associated DataGrid is bound to a DataView or 
//			// DataTable 
//			DataView dataview = ((DataView)cm.List);
//			int i;
//
//			for (i = 0; i < dataview.Count; i++) {
//				if (s.Equals(dataview[i][_comboBox.DisplayMember]))
//					break;
//			}
//
//			// If set item was found return corresponding value, 
//			// otherwise return DbNull.Value
//			if(i < dataview.Count)
//				s =  dataview[i][_comboBox.ValueMember];
//			else
//				s = DBNull.Value;
//        
//			base.SetColumnValueAtRow(source, rowNum, s);
//		}

		// On DataGrid scroll, hide the combobox
		private void DataGrid_Scroll(object sender, EventArgs e) {
			_comboBox.Hide();
			this.DataGridTableStyle.DataGrid.Scroll -= new EventHandler(DataGrid_Scroll);            
		}

		// On combobox losing focus, set the column value, hide the combobox,
		// and unregister scroll event handler
		private void ComboBox_Leave(object sender, EventArgs e) {
//			DataRowView rowView = (DataRowView) _comboBox.SelectedItem;
//			string s = (string) rowView.Row[_comboBox.DisplayMember];

			//SetColumnValueAtRow(_currencyManager, _currentRow, _comboBox.SelectedItem.ToString());
			Invalidate();

			_comboBox.Hide();
			this.DataGridTableStyle.DataGrid.Scroll -= new EventHandler(DataGrid_Scroll);            
		}

		private void _comboBox_SelectionChangeCommitted(object sender, EventArgs e) {
			SetColumnValueAtRow(_currencyManager, _currentRow, _comboBox.SelectedItem.ToString());
			base.ColumnStartedEditing((System.Windows.Forms.Control)sender);
		}
	}
}
