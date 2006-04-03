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
 *   Date: 12/10/2004
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Reflector.UserInterface;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Description of TextFileEditor.	
	/// </summary>
	public class TextFileEditor : UserControl, IObjectEditor, Ch3Etah.Gui.Widgets.ITextDocument
	{
		public event System.EventHandler SelectedObjectChanged;

		protected virtual void OnSelectedObjectChanged()
		{
			if (SelectedObjectChanged != null)
			{
				SelectedObjectChanged(SelectedObject, new EventArgs());
			}
		}

		#region Constructors and member variables

		protected string _fileName;
		protected bool _readOnly;
		protected string _oldText;

		protected TextEditorControl txtDocument;
		protected CommandBarContextMenu contextMenu = new CommandBarContextMenu();
		protected CommandBarItem[] highlightingOptions = null;
		private bool isDirty = false;

		public TextFileEditor()
		{
			// setup text editor control
			txtDocument = new TextEditorControl();
			txtDocument.ShowEOLMarkers = false;
			txtDocument.ShowInvalidLines = false;
			txtDocument.ShowSpaces = false;
			txtDocument.ShowTabs = false;
			txtDocument.Encoding = Encoding.Default;
			txtDocument.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
//			txtDocument.TextEditorProperties.EnableFolding=false;
			HighlightingManager.Manager.AddSyntaxModeFileProvider(
				new FileSyntaxModeProvider(AppDomain.CurrentDomain.BaseDirectory));
			//txtDocument.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("Velocity - Default");
			contextMenu.Popup += new System.EventHandler(ContextMenu_Popup);
			txtDocument.ContextMenu = contextMenu;

			InitializeComponent();
			txtDocument.Dock = DockStyle.Fill;
			Controls.Add(txtDocument);
		}

		public TextFileEditor(string fileName) : this()
		{
			FileName = fileName;
		}

		#endregion Constructors and member variables

		#region Windows Forms Designer generated code

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TextFileEditor
			// 
			Name = "TextFileEditor";
			Size = new Size(504, 440);
		}

		#endregion

		#region Properties

		#region FileName

		public string FileName
		{
			get { return _fileName; }
			set
			{
				_fileName = value;
				LoadFile();
			}
		}

		#endregion FileName

		#region ReadOnly

		public bool ReadOnly
		{
			get { return !txtDocument.Enabled; }
			set { txtDocument.Enabled = !value; }
		}

		#endregion ReadOnly

		#region SelectedObject

		public virtual object SelectedObject
		{
			get { return FileName; }
			set { FileName = (string) value; }
		}

		#endregion SelectedObject

		#region IsDirty

		public event EventHandler IsDirtyChanged;
		
		[Browsable(false)]
		public bool IsDirty
		{
			get { return isDirty; }
			set {
				if (isDirty != value) {
					isDirty = value;
					OnIsDirtyChanged();
				}
			}
		}

		private void OnIsDirtyChanged() {
			if (IsDirtyChanged != null)
				IsDirtyChanged(this, EventArgs.Empty);
		}

		#endregion IsDirty

		#region Icon

		public Icon Icon
		{
			get { return Images.CreateIcon(Images.DocumentText); }
		}

		#endregion Icon

		#endregion Properties

		#region Load / Save

		private void LoadFile()
		{
			if (_fileName == null || _fileName == "")
			{
				if (txtDocument.Text.Length != 0)
					txtDocument.Text = "";
			}
			else
			{
				//using (StreamReader reader = new StreamReader(_fileName, System.Text.Encoding.UTF8, true)) {
				//	txtDocument.Text = reader.ReadToEnd();
				//}
//				try {
//					txtDocument.Encoding = System.Text.Encoding.Unicode;
//					txtDocument.LoadFile(_fileName, true);
//				}
//				catch {
				txtDocument.Encoding = this.GetFileEncoding(_fileName);
				txtDocument.LoadFile(_fileName, true);
//				}
			}
			_oldText = txtDocument.Text;
			IsDirty = false;
		}

		private void SaveFile()
		{
			using (StreamWriter writer = new StreamWriter(_fileName, false, txtDocument.Encoding))
			{
				writer.Write(GetText());
			}
			//txtDocument.SaveFile(_fileName, RichTextBoxStreamType.PlainText);
			_oldText = txtDocument.Text;
			IsDirty = false;
		}

		private bool SaveFileAs()
		{
			SaveFileDialog dlg = new SaveFileDialog();
			string filter =
				"XML Files (.XML)|*.xml|Velolocity Templates (.VM)|*.vm|XSLT Files (.XSL, .XSLT)|*.xsl;*.xslt|Ch3Etah Project Files (.CH3)|*.ch3|All Files (*.*)|*.*";
			bool cancel = false;
			OnSaveAs(ref filter, out cancel);
			if (cancel)
			{
				return false;
			}
			dlg.Filter = filter;
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				_fileName = dlg.FileName;
				SaveFile();
				return true;
			}
			else
			{
				return false;
			}
		}

		
		private Encoding GetFileEncoding(string fileName) {
			XmlTextReader reader = null;
			try
			{
				reader = new XmlTextReader(fileName);
				reader.Read();
				Encoding xmlEncoding = reader.Encoding;
				if (xmlEncoding == null)
					return GetFileEncodingFromBom(fileName);
				else
					return xmlEncoding;
			}
			catch (XmlException)
			{
				return GetFileEncodingFromBom(fileName);
			}
			finally
			{
				if (reader != null && reader.ReadState != ReadState.Closed) 
					reader.Close();
			}
		}
		private Encoding GetFileEncodingFromBom(string fileName) 
		{
			Encoding result = null;
			FileInfo info = new FileInfo(fileName);
			FileStream fs = null;
			try 
			{
				fs = info.OpenRead();
				Encoding[] encodings = { Encoding.BigEndianUnicode, Encoding.Unicode, Encoding.UTF8 };
				for (int i = 0; result == null && i < encodings.Length; i++) 
				{
					fs.Position = 0;
					byte[] preambles = encodings[i].GetPreamble();
					bool preamblesAreEqual = true;
					for (int j = 0; preamblesAreEqual && j < preambles.Length; j++) 
					{
						int fileByte = fs.ReadByte();
						preamblesAreEqual = preambles[j] == fileByte;
					}
					if (preamblesAreEqual) 
					{
						result = encodings[i];
					}
				}
			}
			catch (System.IO.IOException) 
			{
			}
			finally 
			{
				if (fs != null) 
				{
					fs.Close();
				}
			}
 
			if (result == null) 
			{
				result = Encoding.Default;
			}
 
			return result;
		}
		#endregion Load / Save

		#region ToString

		public override string ToString()
		{
			if (_fileName != null && _fileName != "")
			{
				return Path.GetFileName(_fileName);
			}
			else
			{
				return "UNTITLED";
			}
		}

		#endregion ToString

		#region CommitChanges

		public virtual bool CommitChanges()
		{
			if (_readOnly)
			{
				return true;
			}
			if (_fileName == null || _fileName == "")
			{
				return SaveFileAs();
			}
			SaveFile();
			return true;
		}

		#endregion CommitChanges

		#region QueryUnload

		public void QueryUnload(out bool cancel)
		{
			if (IsDirty)
			{
				DialogResult result =
					MessageBox.Show("Do you wish to save the changes you've made to the file '" + Path.GetFileName(_fileName) + "'",
					                Path.GetFileName(_fileName), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
					                MessageBoxDefaultButton.Button3);
				if (result == DialogResult.Yes)
				{
					cancel = CommitChanges();
				}
				else if (result == DialogResult.Cancel)
				{
					cancel = true;
					return;
				}
				cancel = false;
			}
			else
			{
				cancel = false;
			}
		}

		#endregion QueryUnload

		#region Key processing

//		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
//		{
//			switch (keyData)
//			{
//				case Keys.Control | Keys.S:
//					try
//					{
//						CommitChanges();
//					}
//					catch (Exception ex)
//					{
//						MessageBox.Show("Error saving file: " + ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
//					}
//					return true;
//			}
//			return base.ProcessCmdKey(ref msg, keyData);
//		}

		#endregion Key processing

		#region Highlighting Strategy

		private CommandBarItem[] BuildHighlightingOptions()
		{
			ArrayList menuItems = new ArrayList();
			SortedList strategies = new SortedList(HighlightingManager.Manager.HighlightingDefinitions);

			foreach (DictionaryEntry entry in strategies /*HighlightingManager.Manager.HighlightingDefinitions*/)
			{
				CommandBarCheckBox item = new CommandBarCheckBox(entry.Key.ToString());
				item.Click += new System.EventHandler(ChangeSyntax);
				item.IsChecked = txtDocument.Document.HighlightingStrategy.Name == entry.Key.ToString();
				menuItems.Add(item);
			}
			highlightingOptions = (CommandBarItem[]) menuItems.ToArray(typeof (CommandBarItem));
			return highlightingOptions;
		}

		private void ChangeSyntax(object sender, EventArgs e)
		{
			if (txtDocument != null)
			{
				CommandBarCheckBox item = (CommandBarCheckBox) sender;
				foreach (CommandBarCheckBox i in highlightingOptions)
				{
					i.IsChecked = false;
				}
				item.IsChecked = true;
				IHighlightingStrategy strat = HighlightingStrategyFactory.CreateHighlightingStrategy(item.Text);
				if (strat == null)
				{
					throw new Exception("Strategy can't be null");
				}
				txtDocument.Document.HighlightingStrategy = strat;
				txtDocument.Refresh();
			}
		}

		#endregion Highlighting Strategy

		#region Context Menu

		private void ContextMenu_Popup(object sender, EventArgs e)
		{
			contextMenu.Items.Clear();
			SetupContextMenu();
		}

		protected virtual void SetupContextMenu()
		{
			CommandBarItem[] commands = BuildHighlightingOptions();
			foreach (CommandBarItem command in commands)
			{
				contextMenu.Items.Add(command);
			}
		}

		#endregion Context Menu

		// TODO: On activate, check to see if file has been changed outside the editor

		public string GetText()
		{
			return txtDocument.Text;
		}
		public void SetSelection(int offset, int length)
		{
			int start = Math.Min(offset, offset + length);
			int end = Math.Max(offset, offset + length);
			txtDocument.ActiveTextAreaControl.Caret.Position = txtDocument.Document.OffsetToPosition(start);
			txtDocument.ActiveTextAreaControl.SelectionManager.ClearSelection();
			txtDocument.ActiveTextAreaControl.SelectionManager.SetSelection(txtDocument.Document.OffsetToPosition(start), txtDocument.Document.OffsetToPosition(end));
			txtDocument.Refresh();
		}
		public int GetSelectionOffset()
		{
			return txtDocument.ActiveTextAreaControl.Caret.Offset;
		}
		public int GetSelectionLength()
		{
			if (txtDocument.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection.Count > 0)
			{
				int start = txtDocument.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].Offset;
				int length = txtDocument.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].Length;
				if (start < txtDocument.ActiveTextAreaControl.Caret.Offset)
				{
					return -length;
				}
				else
				{
					return length;
				}
			}
			return 0;
		}
		protected void OnSaveAs(ref string fileDialogFilter, out bool cancel)
		{
			cancel = false;
		}

		private void Document_DocumentChanged(object sender, DocumentEventArgs e) {
			if (!IsDirty) 
				IsDirty = true;
		}
	}
}