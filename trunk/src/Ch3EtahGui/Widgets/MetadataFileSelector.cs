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
 *   Date: 5/10/2004
 */

using System;
using System.Collections;
using System.Windows.Forms;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Description of MetadataFileSelector.	
	/// </summary>
	public class MetadataFileSelector : System.Windows.Forms.TreeView
	{
		private MetadataFileCollection _availableFiles;
		private MetadataFileCollection _selectedFiles;
		
		public MetadataFileSelector() {
			base.CheckBoxes = true;
			base.FullRowSelect = true;
			base.HideSelection = false;
			base.ShowLines = false;
			base.ShowPlusMinus = false;
			base.ShowRootLines = false;

			base.ImageList = Images.GetImageList();
			base.ImageIndex = Images.Indexes.DocumentText;
			base.SelectedImageIndex = Images.Indexes.DocumentText;
		}
		
		public void Bind(MetadataFileCollection availableFiles, MetadataFileCollection selectedFiles) {
			_availableFiles = availableFiles;
			_selectedFiles = selectedFiles;
			RefreshView();
		}
		
		public void RefreshView() {
			SortedList files = new SortedList();
			foreach (MetadataFile file in _availableFiles) {
				files.Add(file.Name, file);
			}
			this.Nodes.Clear();
			foreach (DictionaryEntry entry in files) {
				MetadataFile file = (MetadataFile)entry.Value;
				TreeNode node = this.Nodes.Add(file.Name);
				node.Tag = file;
				node.Checked = _selectedFiles.Contains(file);
			}
		}
		
		public int SelectedCount {
			get {
				return _selectedFiles.Count;
			}
		}
		
		protected override void OnAfterCheck(System.Windows.Forms.TreeViewEventArgs e) {
			MetadataFile file = (MetadataFile)e.Node.Tag;
			if (e.Node.Checked) {
				_selectedFiles.Add(file);
			}
			else {
				if (_selectedFiles.Contains(file)) {
					_selectedFiles.Remove(file);
				}
			}
		}
		
	}
}
