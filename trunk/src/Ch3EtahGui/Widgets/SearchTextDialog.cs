using System;
using System.Windows.Forms;

using Ch3Etah.Gui.DocumentHandling;
using Ch3Etah.Gui.DocumentHandling.MdiStrategy;

using WeifenLuo.WinFormsUI;

namespace Ch3Etah.Gui.Widgets
{
	public class SearchTextDialog : DockContent
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox SearchMode;
		private System.Windows.Forms.RadioButton SearchCurrentDocument;
		private System.Windows.Forms.RadioButton SearchAllOpenDocuments;
		private System.Windows.Forms.RadioButton SearchProject;
		private System.Windows.Forms.RadioButton SearchSelectedText;
		private System.Windows.Forms.Button FindButton;
		private System.Windows.Forms.Button ReplaceButton;
		private System.Windows.Forms.Button ReplaceAllButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox FindText;
		private System.Windows.Forms.CheckBox MatchCase;
		private System.Windows.Forms.CheckBox MatchWholeWord;
		private System.Windows.Forms.ComboBox MatchingAlgorithm;
		private System.Windows.Forms.TextBox ReplaceText;
		private System.Windows.Forms.Button CloseDialogButton;
		private System.Windows.Forms.Label lblMatchingAlgorithm;

		public SearchTextDialog()
		{
			InitializeComponent();
			MatchingAlgorithm.Text = "Literal";
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.FindText = new System.Windows.Forms.TextBox();
			this.MatchCase = new System.Windows.Forms.CheckBox();
			this.MatchWholeWord = new System.Windows.Forms.CheckBox();
			this.MatchingAlgorithm = new System.Windows.Forms.ComboBox();
			this.SearchMode = new System.Windows.Forms.GroupBox();
			this.SearchSelectedText = new System.Windows.Forms.RadioButton();
			this.SearchProject = new System.Windows.Forms.RadioButton();
			this.SearchAllOpenDocuments = new System.Windows.Forms.RadioButton();
			this.SearchCurrentDocument = new System.Windows.Forms.RadioButton();
			this.FindButton = new System.Windows.Forms.Button();
			this.ReplaceButton = new System.Windows.Forms.Button();
			this.ReplaceAllButton = new System.Windows.Forms.Button();
			this.ReplaceText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lblMatchingAlgorithm = new System.Windows.Forms.Label();
			this.CloseDialogButton = new System.Windows.Forms.Button();
			this.SearchMode.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 11);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Find What:";
			// 
			// FindText
			// 
			this.FindText.Location = new System.Drawing.Point(88, 8);
			this.FindText.Name = "FindText";
			this.FindText.Size = new System.Drawing.Size(320, 20);
			this.FindText.TabIndex = 1;
			this.FindText.Text = "";
			// 
			// MatchCase
			// 
			this.MatchCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.MatchCase.Location = new System.Drawing.Point(8, 64);
			this.MatchCase.Name = "MatchCase";
			this.MatchCase.Size = new System.Drawing.Size(136, 16);
			this.MatchCase.TabIndex = 2;
			this.MatchCase.Text = "Match Case";
			// 
			// MatchWholeWord
			// 
			this.MatchWholeWord.Enabled = false;
			this.MatchWholeWord.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.MatchWholeWord.Location = new System.Drawing.Point(8, 80);
			this.MatchWholeWord.Name = "MatchWholeWord";
			this.MatchWholeWord.Size = new System.Drawing.Size(144, 16);
			this.MatchWholeWord.TabIndex = 3;
			this.MatchWholeWord.Text = "Match Whole Word";
			// 
			// MatchingAlgorithm
			// 
			this.MatchingAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MatchingAlgorithm.Enabled = false;
			this.MatchingAlgorithm.Items.AddRange(new object[] {
																   "Literal",
																   "Wildcards",
																   "Regular Expressions"});
			this.MatchingAlgorithm.Location = new System.Drawing.Point(8, 120);
			this.MatchingAlgorithm.Name = "MatchingAlgorithm";
			this.MatchingAlgorithm.Size = new System.Drawing.Size(176, 21);
			this.MatchingAlgorithm.TabIndex = 4;
			// 
			// SearchMode
			// 
			this.SearchMode.Controls.Add(this.SearchSelectedText);
			this.SearchMode.Controls.Add(this.SearchProject);
			this.SearchMode.Controls.Add(this.SearchAllOpenDocuments);
			this.SearchMode.Controls.Add(this.SearchCurrentDocument);
			this.SearchMode.Location = new System.Drawing.Point(200, 56);
			this.SearchMode.Name = "SearchMode";
			this.SearchMode.Size = new System.Drawing.Size(208, 88);
			this.SearchMode.TabIndex = 5;
			this.SearchMode.TabStop = false;
			this.SearchMode.Text = "Search";
			// 
			// SearchSelectedText
			// 
			this.SearchSelectedText.Enabled = false;
			this.SearchSelectedText.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SearchSelectedText.Location = new System.Drawing.Point(16, 66);
			this.SearchSelectedText.Name = "SearchSelectedText";
			this.SearchSelectedText.Size = new System.Drawing.Size(128, 16);
			this.SearchSelectedText.TabIndex = 3;
			this.SearchSelectedText.Text = "Within Selected Text";
			// 
			// SearchProject
			// 
			this.SearchProject.Enabled = false;
			this.SearchProject.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SearchProject.Location = new System.Drawing.Point(16, 50);
			this.SearchProject.Name = "SearchProject";
			this.SearchProject.Size = new System.Drawing.Size(128, 16);
			this.SearchProject.TabIndex = 2;
			this.SearchProject.Text = "Current Project";
			// 
			// SearchAllOpenDocuments
			// 
			this.SearchAllOpenDocuments.Enabled = false;
			this.SearchAllOpenDocuments.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SearchAllOpenDocuments.Location = new System.Drawing.Point(16, 34);
			this.SearchAllOpenDocuments.Name = "SearchAllOpenDocuments";
			this.SearchAllOpenDocuments.Size = new System.Drawing.Size(128, 16);
			this.SearchAllOpenDocuments.TabIndex = 1;
			this.SearchAllOpenDocuments.Text = "All Open Documents";
			// 
			// SearchCurrentDocument
			// 
			this.SearchCurrentDocument.Checked = true;
			this.SearchCurrentDocument.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SearchCurrentDocument.Location = new System.Drawing.Point(16, 18);
			this.SearchCurrentDocument.Name = "SearchCurrentDocument";
			this.SearchCurrentDocument.Size = new System.Drawing.Size(128, 16);
			this.SearchCurrentDocument.TabIndex = 0;
			this.SearchCurrentDocument.TabStop = true;
			this.SearchCurrentDocument.Text = "Current Document";
			// 
			// FindButton
			// 
			this.FindButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.FindButton.Location = new System.Drawing.Point(432, 8);
			this.FindButton.Name = "FindButton";
			this.FindButton.Size = new System.Drawing.Size(80, 22);
			this.FindButton.TabIndex = 4;
			this.FindButton.Text = "Find Next";
			this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// ReplaceButton
			// 
			this.ReplaceButton.Enabled = false;
			this.ReplaceButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ReplaceButton.Location = new System.Drawing.Point(432, 32);
			this.ReplaceButton.Name = "ReplaceButton";
			this.ReplaceButton.Size = new System.Drawing.Size(80, 22);
			this.ReplaceButton.TabIndex = 6;
			this.ReplaceButton.Text = "Replace";
			this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			// 
			// ReplaceAllButton
			// 
			this.ReplaceAllButton.Enabled = false;
			this.ReplaceAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ReplaceAllButton.Location = new System.Drawing.Point(432, 56);
			this.ReplaceAllButton.Name = "ReplaceAllButton";
			this.ReplaceAllButton.Size = new System.Drawing.Size(80, 22);
			this.ReplaceAllButton.TabIndex = 7;
			this.ReplaceAllButton.Text = "Replace All";
			// 
			// ReplaceText
			// 
			this.ReplaceText.Enabled = false;
			this.ReplaceText.Location = new System.Drawing.Point(88, 32);
			this.ReplaceText.Name = "ReplaceText";
			this.ReplaceText.Size = new System.Drawing.Size(320, 20);
			this.ReplaceText.TabIndex = 11;
			this.ReplaceText.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 35);
			this.label2.Name = "label2";
			this.label2.TabIndex = 10;
			this.label2.Text = "Replace With:";
			// 
			// lblMatchingAlgorithm
			// 
			this.lblMatchingAlgorithm.Location = new System.Drawing.Point(8, 104);
			this.lblMatchingAlgorithm.Name = "lblMatchingAlgorithm";
			this.lblMatchingAlgorithm.Size = new System.Drawing.Size(168, 23);
			this.lblMatchingAlgorithm.TabIndex = 12;
			this.lblMatchingAlgorithm.Text = "Text Matching Algorithm:";
			// 
			// CloseDialogButton
			// 
			this.CloseDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseDialogButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.CloseDialogButton.Location = new System.Drawing.Point(432, 120);
			this.CloseDialogButton.Name = "CloseDialogButton";
			this.CloseDialogButton.Size = new System.Drawing.Size(80, 22);
			this.CloseDialogButton.TabIndex = 13;
			this.CloseDialogButton.Text = "Close";
			this.CloseDialogButton.Click += new System.EventHandler(this.CloseDialogButton_Click);
			// 
			// SearchTextDialog
			// 
			this.AcceptButton = this.FindButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CloseDialogButton;
			this.ClientSize = new System.Drawing.Size(520, 152);
			this.Controls.Add(this.MatchingAlgorithm);
			this.Controls.Add(this.lblMatchingAlgorithm);
			this.Controls.Add(this.MatchWholeWord);
			this.Controls.Add(this.MatchCase);
			this.Controls.Add(this.CloseDialogButton);
			this.Controls.Add(this.ReplaceText);
			this.Controls.Add(this.FindText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ReplaceAllButton);
			this.Controls.Add(this.ReplaceButton);
			this.Controls.Add(this.SearchMode);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FindButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(528, 178);
			this.Name = "SearchTextDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find / Replace";
			this.SearchMode.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FindButton_Click(object sender, System.EventArgs e)
		{
			ITextDocument doc = GetActiveTextDocument();
			if (doc != null)
			{
				int start = Math.Max(doc.GetSelectionOffset(), doc.GetSelectionOffset() + doc.GetSelectionLength());
				string query = MatchCase.Checked 
					? FindText.Text 
					: FindText.Text.ToUpper();
				int result = MatchCase.Checked 
					? doc.GetText().IndexOf(query, start)
					: doc.GetText().ToUpper().IndexOf(query, start);
				if (result >= 0)
				{
					doc.SetSelection(result, query.Length);
				}
				else
				{
					MessageBox.Show("No more occurences were found in the specified document(s).");
				}
			}
		}
		
		private ITextDocument GetActiveTextDocument()
		{
			if (base.DockPanel.ActiveDocument is ObjectEditorForm)
			{
				return GetActiveTextDocument((ObjectEditorForm)base.DockPanel.ActiveDocument);
			}
			return null;
		}
		
		private ITextDocument GetActiveTextDocument(ContainerControl container)
		{
			Control ctl = container.ActiveControl;
			if (ctl is ITextDocument)
			{
				return (ITextDocument)ctl;
			}
			else if (ctl is ContainerControl)
			{
				return GetActiveTextDocument(ctl as ContainerControl);
			}
			return null;
		}

		private void ReplaceButton_Click(object sender, System.EventArgs e)
		{
			// only replace if the currently selected text is the 
			// same as the text the user is searching for.
		}

		private void CloseDialogButton_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			base.DockPanel.ActiveDocument.Activate();
		}

	}
}
