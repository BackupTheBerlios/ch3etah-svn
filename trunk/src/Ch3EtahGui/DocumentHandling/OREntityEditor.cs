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
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Metadata.OREntities;
using Reflector.UserInterface;

namespace Ch3Etah.Gui.DocumentHandling
{
	public class OREntityEditor : UserControl, IObjectEditor
	{
		#region Windows Form Designer generated code

		private CommandBarManager commandBarManagerLink = new CommandBarManager();
		private CommandBarManager commandBarManagerField = new CommandBarManager();
		private CommandBarManager commandBarManagerIndex = new CommandBarManager();
		private CommandBarManager commandBarManagerFunctions = new CommandBarManager();
		private CommandBar toolBarLink = new CommandBar(CommandBarStyle.ToolBar);
		private CommandBar toolBarField = new CommandBar(CommandBarStyle.ToolBar);
		private CommandBar toolBarIndex = new CommandBar(CommandBarStyle.ToolBar);
		private CommandBar toolBarFunctions = new CommandBar(CommandBarStyle.ToolBar);

		private CommandBarButton cbiAddLink;
		private CommandBarButton cbiEditLink;
		private CommandBarButton cbiRemoveLink;
		private CommandBarButton cbiAutoFillLink;
		
		private CommandBarButton cbiAddField;
		private CommandBarButton cbiEditField;
		private CommandBarButton cbiRemoveField;
		private CommandBarButton cbiFieldAutoIndex;
		
		private CommandBarButton cbiAddIndex;
		private CommandBarButton cbiEditIndex;
		private CommandBarButton cbiRemoveIndex;
		private CommandBarButton cbiSaveChanges;
		private ErrorProvider errorProvider;
		private ToolBarButton tbiEditor;
		private ToolBarButton tbiXML;
		private ToolBar tlbMode;
		private OREntityEditorDesignView designView;
		private System.Windows.Forms.ToolBarButton tbiSep1;
		private System.Windows.Forms.Panel pnlXmlEditor;
		private Ch3Etah.Gui.DocumentHandling.TextFileEditor textFileEditor1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public OREntityEditor(MetadataFile file) : this()
		{
			_MetadataFile = file;
			Enabled = true;
			Text = ToString();
		}

		public OREntityEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			tlbMode.ImageList = Images.GetImageList();
			Text = "Entity Editor";
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.tlbMode = new System.Windows.Forms.ToolBar();
			this.tbiEditor = new System.Windows.Forms.ToolBarButton();
			this.tbiSep1 = new System.Windows.Forms.ToolBarButton();
			this.tbiXML = new System.Windows.Forms.ToolBarButton();
			this.designView = new Ch3Etah.Gui.DocumentHandling.OREntityEditorDesignView();
			this.pnlXmlEditor = new System.Windows.Forms.Panel();
			this.textFileEditor1 = new Ch3Etah.Gui.DocumentHandling.TextFileEditor();
			this.pnlXmlEditor.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// tlbMode
			// 
			this.tlbMode.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tlbMode.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbiEditor,
																					   this.tbiSep1,
																					   this.tbiXML});
			this.tlbMode.ButtonSize = new System.Drawing.Size(39, 24);
			this.tlbMode.Divider = false;
			this.tlbMode.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tlbMode.DropDownArrows = true;
			this.tlbMode.Location = new System.Drawing.Point(2, 518);
			this.tlbMode.Name = "tlbMode";
			this.tlbMode.ShowToolTips = true;
			this.tlbMode.Size = new System.Drawing.Size(772, 28);
			this.tlbMode.TabIndex = 4;
			this.tlbMode.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tlbMode.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tlbMode_ButtonClick);
			// 
			// tbiEditor
			// 
			this.tbiEditor.ImageIndex = 52;
			this.tbiEditor.Pushed = true;
			this.tbiEditor.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.tbiEditor.Text = "Design";
			// 
			// tbiSep1
			// 
			this.tbiSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbiXML
			// 
			this.tbiXML.ImageIndex = 51;
			this.tbiXML.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.tbiXML.Text = "XML";
			// 
			// designView
			// 
			this.designView.Location = new System.Drawing.Point(304, 120);
			this.designView.Name = "designView";
			this.designView.Size = new System.Drawing.Size(352, 336);
			this.designView.TabIndex = 5;
			this.designView.AfterItemLabelEdit += new System.EventHandler(this.designView_AfterItemLabelEdit);
			this.designView.OnEdit += new System.EventHandler(this.designView_OnEdit);
			this.designView.OnDelete += new System.EventHandler(this.designView_OnDelete);
			this.designView.OnInsert += new System.EventHandler(this.designView_OnInsert);
			this.designView.OnTreeViewSelectItem += new System.EventHandler(this.designView_OnTreeViewSelectItem);
			this.designView.OnRename += new System.EventHandler(this.designView_OnRename);
			// 
			// pnlXmlEditor
			// 
			this.pnlXmlEditor.Controls.Add(this.textFileEditor1);
			this.pnlXmlEditor.Location = new System.Drawing.Point(56, 60);
			this.pnlXmlEditor.Name = "pnlXmlEditor";
			this.pnlXmlEditor.Size = new System.Drawing.Size(236, 420);
			this.pnlXmlEditor.TabIndex = 6;
			this.pnlXmlEditor.Visible = false;
			// 
			// textFileEditor1
			// 
			this.textFileEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textFileEditor1.FileName = null;
			this.textFileEditor1.IsDirty = false;
			this.textFileEditor1.Location = new System.Drawing.Point(0, 0);
			this.textFileEditor1.Name = "textFileEditor1";
			this.textFileEditor1.ReadOnly = false;
			this.textFileEditor1.SelectedObject = null;
			this.textFileEditor1.Size = new System.Drawing.Size(236, 420);
			this.textFileEditor1.TabIndex = 0;
			// 
			// OREntityEditor
			// 
			this.Controls.Add(this.pnlXmlEditor);
			this.Controls.Add(this.designView);
			this.Controls.Add(this.tlbMode);
			this.DockPadding.All = 2;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "OREntityEditor";
			this.Size = new System.Drawing.Size(776, 548);
			this.Load += new System.EventHandler(this.OREntityEditor_Load);
			this.pnlXmlEditor.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		#endregion

		#region Fields

		private MetadataFile _MetadataFile;
		private bool loading;
		internal const String Const_DefaultNewIndexText = "New Index...";
		internal const String Const_DefaultNewLinkText = "New Link...";
		internal const String Const_DefaultNewFieldText = "New Field...";

		#endregion

		#region IObjectEditor implementation

		public event EventHandler SelectedObjectChanged;

		protected virtual void OnSelectedObjectChanged()
		{
			if (SelectedObjectChanged != null)
			{
				SelectedObjectChanged(SelectedObject, new EventArgs());
			}
		}

		#region DoBinding

		private void DoBinding()
		{
			CreateInitialTreeData();
			Enabled = true;
		}

		private void CreateInitialTreeData()
		{
			designView.TreeView.Nodes.Clear();
			// Setup Treeview
			designView.TreeView.ImageList = Images.GetImageList();

			foreach (object metadataEntity in _MetadataFile.MetadataEntities)
			{
				Entity entity = metadataEntity as Entity;
				
				if (entity == null)
					continue;
				
				// Instanciate RootNode
				TreeNode rootNode = new TreeNode(entity.Name, Images.Indexes.Entity, Images.Indexes.Entity);
				rootNode.Tag = entity;

				// Insert RootNode in TreeView
				designView.TreeView.Nodes.Add(rootNode);

				// Refresh / Generate Field List						
				this.designView.RefreshFieldsList(rootNode, entity, false);

				// Refresh / Generate Indexes List
				this.designView.RefreshIndexesList(rootNode, entity, false);

				// Refresh / Generate Links List
				this.designView.RefreshLinksList(rootNode, entity, false);

				// Expand Root Node
				rootNode.ExpandAll();
			}
		}

		#endregion DoBinding

		#region SelectedObject

		public object SelectedObject
		{
			get { return _MetadataFile; }
			set
			{
				_MetadataFile = (MetadataFile) value;
				DoBinding();
			}
		}

		#endregion SelectedObject

		#region IsDirty

		public bool IsDirty
		{
			get { return _MetadataFile.IsDirty; }
		}

		#endregion IsDirty

		#region Icon

		public Icon Icon
		{
			get { return Images.CreateIcon(Images.Edit); }
		}

		#endregion Icon

		#region ToString

		public override string ToString()
		{
			return "Entity: " + _MetadataFile.Name;
		}

		#endregion ToString

		#region CommitChanges

		public bool CommitChanges()
		{
			if (tbiXML.Pushed)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(textFileEditor1.GetText());
				_MetadataFile.LoadXml(doc);
				bool ok = textFileEditor1.CommitChanges();
				if (ok) 
				{
					_MetadataFile.FileName = _MetadataFile.GetRelativePath(textFileEditor1.FileName);
				}
				this.DoBinding();
				return ok;
			}
			else
			{
				if (_MetadataFile != null && _MetadataFile.FileName != string.Empty)
				{
					_MetadataFile.Save();
				}
				return true;
			}
		}

		#endregion CommitChanges

		#region QueryUnload

		public void QueryUnload(out bool cancel)
		{
			if (!Validate())
			{
				DialogResult res =
					MessageBox.Show("There are one or more errors with the data on this form. Would you like to close it anyway?",
					                "Validation Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
					                MessageBoxDefaultButton.Button2);
				if (res != DialogResult.Yes)
				{
					cancel = true;
					return;
				}
			}
			if (IsDirty)
			{
				DialogResult result =
					MessageBox.Show("Do you wish to save the changes you've made to the entity '" + _MetadataFile.Name + "'",
					                _MetadataFile.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
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
				// TODO: Check what this is supposed to do
//				_MetadataFile.LoadXml(((IMetadataNode) _MetadataFile).LoadedXmlNode);
				cancel = false;
			}
			else
			{
				cancel = false;
			}
		}

		#endregion QueryUnload

		#endregion IObjectEditor implementation

		#region Refresh Grids

		#endregion Refresh Grids

		#region Events

		private void OREntityEditor_Load(object sender, EventArgs e)
		{
			loading = true;
			SetupToolBar();
			if (designView.TreeView.Nodes.Count > 0) {
				tbiEditor.Pushed = true;
				tlbMode_ButtonClick(tlbMode, new ToolBarButtonClickEventArgs(tbiEditor));
			}
			else {
				tbiEditor.Enabled = false;
				tbiXML.Pushed = true;
				tlbMode_ButtonClick(tlbMode, new ToolBarButtonClickEventArgs(tbiXML));
			}
			loading = false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			CommitChanges();
		}

		private void designView_OnTreeViewSelectItem(object sender, EventArgs e)
		{
			this.FormatButtons(sender);
		}

		private void tlbMode_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Pushed)
			{
				this.SuspendLayout();
				if (e.Button == tbiXML)
				{
					SwitchToXML();
				}
				else
				{
					SwitchToDesign();
				}
				this.ResumeLayout();
			}
		}

		#region Field buttons

		private void btnAddField_Click(object sender, EventArgs e)
		{
			EntityFieldCollection fields = (EntityFieldCollection) GetSelectedItemOfType(typeof (EntityFieldCollection));
			EntityField oField = new EntityField();
			fields.Add(oField);
			IObjectEditor editor = new OREntityFieldEditor(oField);
			ObjectEditorManager.OpenObjectEditorDialog(editor);
			this.designView.RefreshFieldsList(this.GetSelectedNodeOfType(typeof(EntityFieldCollection)), this.designView.CurrentEntity, false);
		}

		private void btnEditField_Click(object sender, EventArgs e)
		{
			EntityField field = (EntityField) GetSelectedItemOfType(typeof (EntityField));
			if (field != null)
			{
				IObjectEditor editor = new OREntityFieldEditor(field);
				ObjectEditorManager.OpenObjectEditorDialog(editor);
				this.designView.RefreshFieldsList(this.GetSelectedNodeOfType(typeof(EntityFieldCollection)), this.designView.CurrentEntity, false);
			}
		}

		private void btnRemoveField_Click(object sender, EventArgs e)
		{
			EntityFieldCollection fields = (EntityFieldCollection) GetSelectedItemOfType(typeof (EntityFieldCollection));
			EntityField field = (EntityField) GetSelectedItemOfType(typeof (EntityField));
			if (field != null)
			{
				fields.Remove(field);
				this.designView.RemoveFieldFromList(this.designView.CurrentEntityNode, field);
			}
		}

		private void btnFieldAutoIndex_Click(object sender, EventArgs e) 
		{
			// Create Objects
			EntityFieldCollection entityFieldCollection = new EntityFieldCollection();
			EntityField field;
			Entity entity;
			Index tmpNewIndex;
			IndexField tmpNewIndexField;
			Boolean bSub;

			// Get Selected Field
			field = (EntityField) GetSelectedItemOfType(typeof (EntityField));
			
			// Get Current Entity
			entity = this.designView.CurrentEntity;
			
			// Check if Index exists
			if (field == null) // Create All Field Index
			{
				// Get All Fields
				entityFieldCollection = entity.Fields;
			}
			else
			{
				entityFieldCollection.Add(field);
			}

			// Looping on entityFieldCollection
			foreach (EntityField tmpEntityField in entityFieldCollection)
			{
				// Clear Found Flag
				bSub = false;

				// Check the Index Exists
				foreach(Index tmpIndex in entity.Indexes) 
				{
					// Index Alread exists, Set Found Flag
					if (tmpIndex.Name.Equals(tmpEntityField.Name)) { bSub = true; break; }
				}

				if (! bSub) // Check's Found Flag
				{
					// Create Index
					tmpNewIndex = new Index();
					tmpNewIndex.Name = tmpEntityField.Name;
					tmpNewIndex.PrimaryKey = tmpEntityField.KeyField;
					tmpNewIndex.Unique = tmpEntityField.DBIdentity;
					tmpNewIndex.SelectBy = true;
					tmpNewIndex.DeleteBy = tmpEntityField.KeyField;
					entity.Indexes.Add(tmpNewIndex);
			
					// Create Index Fields
					tmpNewIndexField = new IndexField();
					tmpNewIndexField.Name = tmpEntityField.Name;
					tmpNewIndexField.ParameterName = tmpEntityField.Name;
					tmpNewIndex.Fields.Add(tmpNewIndexField);
				}
			}
			
			TreeNode rootNode = GetSelectedNodeOfType(typeof (Entity));
			TreeNode indexesNode = rootNode.Nodes[1];

			// Refresh List
			this.designView.RefreshIndexesList(indexesNode, entity, false);
		}


		#endregion

		#region Index buttons

		private void btnAddIndex_Click(object sender, EventArgs e)
		{
			IndexCollection Indexes = (IndexCollection) GetSelectedItemOfType(typeof (IndexCollection));
			Index oIndex = new Index();
			Indexes.Add(oIndex);
			IObjectEditor editor = new ORIndexEditor(oIndex);
			ObjectEditorManager.OpenObjectEditorDialog(editor);
			this.designView.RefreshIndexesList(this.GetSelectedNodeOfType(typeof(IndexCollection)), this.designView.CurrentEntity, false);
		}

		private void btnEditIndex_Click(object sender, EventArgs e)
		{
			Index Index = (Index) GetSelectedItemOfType(typeof (Index));
			if (Index != null)
			{
				IObjectEditor editor = new ORIndexEditor(Index);
				ObjectEditorManager.OpenObjectEditorDialog(editor);
				this.designView.RefreshIndexesList(this.GetSelectedNodeOfType(typeof(IndexCollection)), this.designView.CurrentEntity, false);
			}
		}

		private void btnRemoveIndex_Click(object sender, EventArgs e)
		{
			IndexCollection Indexes = (IndexCollection) GetSelectedItemOfType(typeof (IndexCollection));
			Index Index = (Index) GetSelectedItemOfType(typeof (Index));
			if (Index != null)
			{
				Indexes.Remove(Index);
				this.designView.RemoveIndexFromList(this.designView.CurrentEntityNode, Index);
			}
		}


		#endregion

		#region Link buttons

		private void btnAddLink_Click(object sender, EventArgs e)
		{
			LinkCollection Links = (LinkCollection) GetSelectedItemOfType(typeof (LinkCollection));
			Link oLink = new Link();
			Links.Add(oLink);
			IObjectEditor editor = new ORLinkEditor(oLink);
			ObjectEditorManager.OpenObjectEditorDialog(editor);
			this.designView.RefreshLinksList(this.GetSelectedNodeOfType(typeof(LinkCollection)), this.designView.CurrentEntity, false);
		}

		private void btnEditLink_Click(object sender, EventArgs e)
		{
			Link Link = (Link) GetSelectedItemOfType(typeof (Link));
			if (Link != null)
			{
				IObjectEditor editor = new ORLinkEditor(Link);
				ObjectEditorManager.OpenObjectEditorDialog(editor);
				this.designView.RefreshLinksList(this.GetSelectedNodeOfType(typeof(LinkCollection)), this.designView.CurrentEntity, false);
			}
		}

		private void btnRemoveLink_Click(object sender, EventArgs e)
		{
			LinkCollection Links = (LinkCollection) GetSelectedItemOfType(typeof (LinkCollection));
			Link Link = (Link) GetSelectedItemOfType(typeof (Link));
			if (Link != null)
			{
				Links.Remove(Link);
				this.designView.RemoveLinkFromList(this.designView.CurrentEntityNode, Link);
			}
		}


		#endregion

		#region designView.TreeView

		private void designView_OnInsert(object sender, EventArgs e)
		{
			if (this.designView.CurrentSelectedNode != null)
			{
				if (this.designView.CurrentSelectedNode.Tag is EntityFieldCollection)
				{
					EntityField oEntity = new EntityField();
					oEntity.Name = Const_DefaultNewFieldText;

					((EntityFieldCollection) this.designView.CurrentSelectedNode.Tag).Add(oEntity);

					this.designView.RefreshFieldsList(this.designView.CurrentSelectedNode, this.designView.CurrentEntity, true);
				}
				else if (this.designView.CurrentSelectedNode.Tag is IndexCollection)
				{
					Index oIndex = new Index();
					oIndex.Name = Const_DefaultNewIndexText;

					((IndexCollection) this.designView.CurrentSelectedNode.Tag).Add(oIndex);

					this.designView.RefreshIndexesList(this.designView.CurrentSelectedNode, this.designView.CurrentEntity, true);
				}
				else if (this.designView.CurrentSelectedNode.Tag is LinkCollection)
				{
					Link oLink = new Link();
					oLink.Name = Const_DefaultNewLinkText;

					((LinkCollection) this.designView.CurrentSelectedNode.Tag).Add(oLink);

					this.designView.RefreshLinksList(this.designView.CurrentSelectedNode, this.designView.CurrentEntity, true);
				}
			}
		}

		private void designView_OnEdit(object sender, EventArgs e)
		{
			if (this.designView.CurrentSelectedNode != null)
			{
				if (this.designView.CurrentSelectedNode.Tag is EntityField)
				{
					this.btnEditField_Click(null, null);
				}
				else if (this.designView.CurrentSelectedNode.Tag is Index)
				{
					this.btnEditIndex_Click(null, null);
				}
				else if (this.designView.CurrentSelectedNode.Tag is Link)
				{
					this.btnEditLink_Click(null, null);
				}
			}
		}

		private void designView_OnDelete(object sender, EventArgs e)
		{
			if (this.designView.CurrentSelectedNode != null)
			{
				if (this.designView.CurrentSelectedNode.Tag is EntityField)
				{
					this.btnRemoveField_Click(null, null);
				}
				else if (this.designView.CurrentSelectedNode.Tag is Index)
				{
					this.btnRemoveIndex_Click(null, null);
				}
				else if (this.designView.CurrentSelectedNode.Tag is Link)
				{
					this.btnRemoveLink_Click(null, null);
				}
			}
		}


		#endregion

		private void btnAutoFillLinks_Click(object sender, EventArgs e)
		{
			// Create Objects
			DataSource oDataSource;

			// Get Entity DataSource
			oDataSource = Project.CurrentProject.DataSources.Find(this.designView.CurrentEntity.DataSourceName);

			
//			this.designView.CurrentEntity.DataSourceName
//			Project project = _MetadataFile.Project;
//			if (project == null)
//			{
//				return;
//			}
//			foreach (DataSource ds in project.DataSources)
//			{
//				if (ds.Name == _MetadataFile.DataSourceName)
//				{
//					_MetadataFile.RefreshDBLinks(ds);
//					return;
//				}
//			}
//			RefreshLinksList();
		}


		#endregion Events

		#region Methods
		
		private object GetSelectedItemOfType(Type type)
		{
			TreeNode node = GetSelectedNodeOfType(type);
			if (node != null) {
				return node.Tag;
			}
			else {
				return null;
			}
		}

		private TreeNode GetSelectedNodeOfType(Type type)
		{
			// Bottom-top search
			TreeNode node = designView.TreeView.SelectedNode;
			while (node != null && node.Tag != null && node.Tag.GetType() != type)
			{
				node = node.Parent;
			}
			if (node != null && node.Tag != null && node.Tag.GetType() == type)
				return node;
			else {
				return null;
			}
		}

		private void SwitchToDesign()
		{
			pnlXmlEditor.Visible = false;
			designView.Dock = DockStyle.Fill;
			designView.Visible = true;
			tbiXML.Pushed = false;
			this.ResumeLayout();
			this.SuspendLayout();
			ReloadEntity();
		}

		private void SwitchToXML()
		{
			designView.Visible = false;
			pnlXmlEditor.Dock = DockStyle.Fill;
			pnlXmlEditor.Visible = true;
			tbiEditor.Pushed = false;
			this.ResumeLayout();
			this.SuspendLayout();
			ReloadXML();
		}

		public void SetupToolBar()
		{
			// Buttons Creation
			cbiAddLink = new CommandBarButton(Images.New, "Add Link", new EventHandler(btnAddLink_Click));
			cbiEditLink = new CommandBarButton(Images.Edit, "Edit Link", new EventHandler(btnEditLink_Click));
			cbiRemoveLink = new CommandBarButton(Images.Delete, "Remove Link", new EventHandler(btnRemoveLink_Click));
			cbiAutoFillLink = new CommandBarButton(Images.Tools, "Auto Fill Link", new EventHandler(btnAutoFillLinks_Click));
			cbiAddField = new CommandBarButton(Images.New, "Add Field", new EventHandler(btnAddField_Click));
			cbiEditField = new CommandBarButton(Images.Edit, "Edit Field", new EventHandler(btnEditField_Click));
			cbiRemoveField = new CommandBarButton(Images.Delete, "Remove Field", new EventHandler(btnRemoveField_Click));
			cbiFieldAutoIndex = new CommandBarButton(Images.DocumentArrowGreen, "Create index from field", new EventHandler(btnFieldAutoIndex_Click));
			cbiAddIndex = new CommandBarButton(Images.New, "Add Index", new EventHandler(btnAddIndex_Click));
			cbiEditIndex = new CommandBarButton(Images.Edit, "Edit Index", new EventHandler(btnEditIndex_Click));
			cbiRemoveIndex = new CommandBarButton(Images.Delete, "Remove Index", new EventHandler(btnRemoveIndex_Click));
			cbiSaveChanges = new CommandBarButton(Images.Save, "Save Changes", new EventHandler(btnSave_Click));

			// Toolbar's Setup
			toolBarLink.NewLine = false;
			toolBarLink.UseChevron = false;

			toolBarField.NewLine = false;
			toolBarField.UseChevron = false;

			toolBarIndex.NewLine = false;
			toolBarIndex.UseChevron = false;

			toolBarFunctions.NewLine = false;
			toolBarFunctions.UseChevron = false;

			// ToolBarLink
			toolBarLink.Items.Add(cbiSaveChanges);
			toolBarLink.Items.AddSeparator();

			toolBarLink.Items.Add(cbiAddLink);
			toolBarLink.Items.Add(cbiEditLink);
			toolBarLink.Items.AddSeparator();

			toolBarLink.Items.Add(cbiAutoFillLink);
			toolBarLink.Items.AddSeparator();

			toolBarLink.Items.Add(cbiRemoveLink);

			// ToolBar Fields
			toolBarField.Items.Add(cbiSaveChanges);
			toolBarField.Items.AddSeparator();

			toolBarField.Items.Add(cbiAddField);
			toolBarField.Items.Add(cbiEditField);
			toolBarField.Items.Add(cbiFieldAutoIndex);
			toolBarField.Items.AddSeparator();

			toolBarField.Items.Add(cbiRemoveField);

			// ToolBar Indexes
			toolBarIndex.Items.Add(cbiSaveChanges);
			toolBarIndex.Items.AddSeparator();

			toolBarIndex.Items.Add(cbiAddIndex);
			toolBarIndex.Items.Add(cbiEditIndex);
			toolBarIndex.Items.AddSeparator();
			toolBarIndex.Items.Add(cbiRemoveIndex);

			// Toolbar Functions
			toolBarFunctions.Items.Add(cbiSaveChanges);

			// Add to user interface
			commandBarManagerFunctions.CommandBars.Add(toolBarFunctions);
			commandBarManagerLink.CommandBars.Add(toolBarLink);
			commandBarManagerField.CommandBars.Add(toolBarField);
			commandBarManagerIndex.CommandBars.Add(toolBarIndex);

			designView.ToolbarPanel.Controls.Add(commandBarManagerFunctions);
			designView.ToolbarPanel.Controls.Add(commandBarManagerLink);
			designView.ToolbarPanel.Controls.Add(commandBarManagerField);
			designView.ToolbarPanel.Controls.Add(commandBarManagerIndex);
		}


		public void FormatButtons(object selectedObject)
		{
			SuspendLayout();

			if (selectedObject is Index || selectedObject is IndexCollection)
			{
				commandBarManagerLink.Visible = false;
				commandBarManagerField.Visible = false;
				commandBarManagerFunctions.Visible = false;
				commandBarManagerIndex.Visible = true;
			}
			else if (selectedObject is Link || selectedObject is LinkCollection)
			{
				commandBarManagerField.Visible = false;
				commandBarManagerIndex.Visible = false;
				commandBarManagerFunctions.Visible = false;
				commandBarManagerLink.Visible = true;
			}
			else if (selectedObject is EntityField || selectedObject is EntityFieldCollection)
			{
				commandBarManagerLink.Visible = false;
				commandBarManagerIndex.Visible = false;
				commandBarManagerFunctions.Visible = false;
				commandBarManagerField.Visible = true;
			}
			else
			{
				commandBarManagerLink.Visible = false;
				commandBarManagerField.Visible = false;
				commandBarManagerIndex.Visible = false;
				commandBarManagerFunctions.Visible = true;
			}

			ResumeLayout();
		}


		private void ReloadEntity()
		{
			if (textFileEditor1.IsDirty)
			{
				textFileEditor1.CommitChanges();
				_MetadataFile.Load();
			}
			CreateInitialTreeData();
		}

	
		private void ReloadXML()
		{
			if (!loading && _MetadataFile.IsDirty)
			{
				_MetadataFile.Save();
			}
			textFileEditor1.SelectedObject = _MetadataFile.GetFullPath();
		}

		
		#endregion

		private void designView_OnRename(object sender, EventArgs e)
		{
			if (this.designView.CurrentSelectedNode != null)
			{
				if (this.designView.CurrentSelectedNode.Tag is EntityField)
				{
					this.designView.CurrentSelectedNode.BeginEdit();
				}
				else if (this.designView.CurrentSelectedNode.Tag is Index)
				{
					this.designView.CurrentSelectedNode.BeginEdit();
				}
				else if (this.designView.CurrentSelectedNode.Tag is Link)
				{
					this.designView.CurrentSelectedNode.BeginEdit();
				}
			}
		}

		private void designView_AfterItemLabelEdit(object sender, EventArgs e)
		{
			if (this.designView.CurrentSelectedNode != null)
			{
				this.designView.CurrentSelectedNode.EndEdit(false);

				if (this.designView.CurrentSelectedNode.Tag is EntityField)
				{
					((EntityField) this.designView.CurrentSelectedNode.Tag).Name = sender.ToString();
				}
				else if (this.designView.CurrentSelectedNode.Tag is Index)
				{
					((Index) this.designView.CurrentSelectedNode.Tag).Name = sender.ToString();
				}
				else if (this.designView.CurrentSelectedNode.Tag is Link)
				{
					((Link) this.designView.CurrentSelectedNode.Tag).Name = sender.ToString();
				}

				this.designView.RefreshPropertyGrid();
			}
		}

		private void textFileEditor1_IsDirtyChanged(object sender, System.EventArgs e) {
			string text;
			if (textFileEditor1.IsDirty) {
				text = _MetadataFile.Name + " *";
			}
			else {
				text = _MetadataFile.Name;
			}
			if (Text != text) Text = text;
		}
	}
}