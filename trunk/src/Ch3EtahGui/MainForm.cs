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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Ch3Etah.Core;
using Ch3Etah.Core.Config;
using Ch3Etah.Core.CodeGen.PackageLib;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Gui.DocumentHandling;
using Ch3Etah.Gui.DocumentHandling.MdiStrategy;
using Ch3Etah.Gui.Widgets;
using Ch3Etah.Metadata.OREntities;
using Reflector.UserInterface;
using WeifenLuo.WinFormsUI;

namespace Ch3Etah.Gui {
	
	public class MainForm : Form, IMdiContainer {
		private const string ERROR_MESSAGE_1 = " [ERROR LOADING FILE - CHECK THAT FILE EXISTS AND THAT THE PROJECT'S MetadataBaseDir property is correct.]";

		private Splitter splitter3;
		private StatusBar statusBar;

		#region Windows Forms Designer generated code

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			ResourceManager resources = new ResourceManager(typeof (MainForm));
			statusBar = new StatusBar();
			splitter3 = new Splitter();
			dockPanel1 = new DockPanel();
			SuspendLayout();
			// 
			// statusBar
			// 
			statusBar.Location = new Point(0, 459);
			statusBar.Name = "statusBar";
			statusBar.Size = new Size(758, 21);
			statusBar.TabIndex = 6;
			// 
			// splitter3
			// 
			splitter3.Dock = DockStyle.Bottom;
			splitter3.Location = new Point(0, 456);
			splitter3.Name = "splitter3";
			splitter3.Size = new Size(758, 3);
			splitter3.TabIndex = 21;
			splitter3.TabStop = false;
			// 
			// dockPanel1
			// 
			dockPanel1.ActiveAutoHideContent = null;
			dockPanel1.Dock = DockStyle.Fill;
			dockPanel1.Font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.World);
			dockPanel1.Location = new Point(0, 0);
			dockPanel1.Name = "dockPanel1";
			dockPanel1.Size = new Size(758, 456);
			dockPanel1.TabIndex = 23;
			// 
			// MainForm
			// 
			AutoScaleBaseSize = new Size(5, 13);
			ClientSize = new Size(758, 480);
			Controls.Add(dockPanel1);
			Controls.Add(splitter3);
			Controls.Add(statusBar);
			Icon = ((Icon) (resources.GetObject("$this.Icon")));
			IsMdiContainer = true;
			Name = "MainForm";
			Text = "CH3ETAH";
			WindowState = FormWindowState.Maximized;
			Closing += new CancelEventHandler(Form_Closing);
			Load += new EventHandler(MainForm_Load);
			ResumeLayout(false);

		}

		#endregion

		#region Constructors and Member variables

		private Project _project;
		private EventTraceListener _listener;
		private bool _running;
		private bool _cancelGeneration;
		private string[] autoRunFiles;

		#region UI member variables

		private MRUList _mruList;

		private CommandBarManager commandBarManager = new CommandBarManager();
		private CommandBar menuBar = new CommandBar(CommandBarStyle.Menu);
		private CommandBar toolBar = new CommandBar(CommandBarStyle.ToolBar);
		private CommandBarContextMenu treeviewContextMenu = new CommandBarContextMenu();

		private CommandBarItem cbiNewProject;
		private CommandBarItem cbiOpenProject;
		private CommandBarItem cbiSaveDocument;
		private CommandBarItem cbiSaveProject;
		private CommandBarItem cbiSaveProjectAs;
		private CommandBarItem cbiRunProject;
		private CommandBarItem cbiCancelGeneration;
		private CommandBarItem cbiAddNewMetadataFile;
		private CommandBarItem cbiAddExistingMetadataFile;
		private CommandBarItem cbiAddCodeGenCommand;
		private CommandBarItem cbiAddDataSource;
		private CommandBarItem cbiEditProjectParameters;

		private CommandBarItem cbiViewProjectExplorer;
		private CommandBarItem cbiViewPropertiesWindow;
		private CommandBarItem cbiViewOutputWindow;

		private CommandBarItem cbiExit;

		private CommandBarItem cbiWebSite;
		private CommandBarItem cbiRunTests;
		private CommandBarItem cbiFixFileAssociations;
		private DockPanel dockPanel1;
		private CommandBarItem cbiAbout;
		private TreeView tvwProject;
		private PropertyGrid propertyGrid;
		private TextBox txtOutput;
		private PropertiesWindow propertiesWindow;
		private OutputWindow outputWindow;
		private ProjectExplorer projectExplorer;

		#endregion UI member variables

		public MainForm() {
			InitializeForm();
		}

		public MainForm(string fileName) : base() {
			InitializeForm();
			OpenProject(fileName);
		}

		public MainForm(string[] fileNames) : base() {
			autoRunFiles = fileNames;
		}

		private void InitializeForm() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			// Dock panes
			if (!LoadDockSetings()) {
				CreateDockWindows();
			}

			ObjectEditorManager.HandlingStrategy = new MdiDocumentHandlingStrategy(this);

			// Context menu
			treeviewContextMenu.Popup += new EventHandler(TreeviewContextMenu_Popup);
			tvwProject.ContextMenu = treeviewContextMenu;

			// Trace listener
			_listener = new EventTraceListener();
			_listener.TextWritten += new TraceEvent(listener_TextWritten);
			Trace.Listeners.Add(_listener);

			// Menu bar
			SetupCommandBars();

			// Tree view
			RefreshUI();
		}

		#endregion Constructors and Member variables

		#region Project operations

		private bool DoSaveConfirmation() {
			if (_project == null || !_project.IsDirty || autoRunFiles != null) {
				return true;
			}
			DialogResult result =
				MessageBox.Show("Do you wish to save the changes made to the open project?", "Confirmation", MessageBoxButtons.YesNoCancel,
				                MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
			if (result == DialogResult.Yes) {
				return SaveProject();
			}
			else if (result == DialogResult.No) {
				return true;
			}
			else {
				return false;
			}
		}

		private bool SaveDocument() {
			object activeMdiChild = dockPanel1.ActiveDocument;

			if (_project == null) {
				return true;
			}

			if (activeMdiChild != null) {
				if (activeMdiChild is ObjectEditorForm) {
					((ObjectEditorForm) activeMdiChild).ObjectEditor.CommitChanges();
				}
				else {
					MessageBox.Show(activeMdiChild.GetType().FullName);
				}
			}
			return true;
		}

		private void VerifyOpenProject() {
			if (_project != null) {
				MessageBox.Show(
					"WARNING: This version has a bug which causes NVelocity's context not to reset after generating. If you are going to generate a second project, you should close the CH3ETAH GUI and reopen it.");
			}
		}

		private void NewProject() {
			if (!CloseDocuments()) {
				return;
			}
			if (!DoSaveConfirmation()) {
				return;
			}
			VerifyOpenProject();


//			// TEMP
//			(new ProjectNew()).ShowDialog();
//			return;
//			// END TEMP


			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Title = "New Project...";
			dlg.Filter = "CH3ETAH Project Files (.CH3)|*.ch3|All Files (*.*)|*.*";
			if (dlg.ShowDialog(this) == DialogResult.OK) {
				_project = new Project();
				_project.Save(dlg.FileName);
				Directory.SetCurrentDirectory(Path.GetDirectoryName(dlg.FileName));
			}
			else {
				return;
			}
			RefreshUI();
		}

		private void OpenProject() {
			if (!CloseDocuments()) {
				return;
			}
			if (!DoSaveConfirmation()) {
				return;
			}
			VerifyOpenProject();
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "CH3ETAH Project Files (.CH3)|*.ch3|XML Files (.XML)|*.xml|All Files (*.*)|*.*";
			if (dlg.ShowDialog(this) == DialogResult.OK) {
				OpenProject(dlg.FileName);
			}
		}

		private void OpenProject(string fileName) {
			if (!CloseDocuments()) {
				return;
			}
			VerifyOpenProject();
			Project project = Project.Load(fileName);
			if (!project.IsFileVersionCompatible) {
				MessageBox.Show(this,
				                "The file that you are trying to load is not compatible with this version of CH3ETAH. In order to prevent information loss, the project file will not be loaded. Please make sure you are using the most recent version of CH3ETAH in order to load this project file.",
				                "Incompatible file version",
				                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			tvwProject.Nodes.Clear();
			_project = project;
			Directory.SetCurrentDirectory(Path.GetDirectoryName(fileName));
			RefreshUI();
			_mruList.Add(fileName);
		}

		private bool SaveProject() {
			if (_project == null) {
				return true;
			}
			else if (_project.FileName == "") {
				return SaveProjectAs();
			}
			_project.Save();
			return true;
		}

		private bool SaveProjectAs() {
			if (_project == null) {
				return true;
			}
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "CH3ETAH Project Files (.CH3)|*.ch3|All Files (*.*)|*.*";
			if (dlg.ShowDialog(this) == DialogResult.OK) {
				_project.Save(dlg.FileName);
				return true;
			}
			else {
				return false;
			}
		}

		private void RunProject() {
			if (_running) {
				throw new InvalidOperationException("This project is already being generated.");
			}
			try {
				
				// Fly-out the output window if needed
				if (!outputWindow.IsFloat && outputWindow.DockState == DockState.DockBottomAutoHide)
				{
					outputWindow.Activate();
				}
				
				_running = true;
				_cancelGeneration = false;
				EnableCommandBarButtons();
				txtOutput.AppendText("\r\n BEGIN PROJECT GENERATION \r\n========================================\r\n");
				DateTime start = DateTime.Now;
				foreach (GeneratorCommand command in _project.GeneratorCommands) {
					if (_cancelGeneration) {
						txtOutput.AppendText("\r\n========================================");
						txtOutput.AppendText("\r\n PROJECT GENERATION CANCELED \r\n");
						txtOutput.AppendText("\r\n========================================\r\n");
						break;
					}
					command.Execute();
				}
				txtOutput.AppendText("\r\n Time to generate project: " + (DateTime.Now - start).ToString() + "\r\n ");
				txtOutput.AppendText("\r\n END PROJECT GENERATION \r\n========================================\r\n");
				if (!_cancelGeneration && (autoRunFiles == null)) {
					MessageBox.Show(this, "Project generated succesfully.");
				}
			}
			catch (Exception ex) {
				txtOutput.AppendText("\r\n========================================");
				txtOutput.AppendText("\r\n ERROR RUNNING PROJECT \r\n CurrentDirectory=" + Directory.GetCurrentDirectory() +
				                     "\r\n========================================\r\n");
				txtOutput.AppendText(ex.ToString());
				txtOutput.AppendText("\r\n========================================\r\n");
				MessageBox.Show(this, "Error running project:\r\n\r\n" + ex.ToString());
			}
			finally {
				_running = false;
				EnableCommandBarButtons();
			}
		}

		#endregion Project operations

		#region UI code

		private void RefreshUI() {
			RefreshTitleBar();
			EnableCommandBarButtons();
			FillTreeview();
		}

		private void RefreshTitleBar() {
			string title = "CH3ETAH";
			if (_project != null) {
				Text = title + " - [" + _project.Name + "]";
				return;
			}
			else {
				Text = title + " - [NO PROJECT LOADED]";
				return;
			}
		}

		private void EnableCommandBarButtons() {
			cbiRunProject.IsEnabled = (_project != null && _project.GeneratorCommands.Count > 0 && !_running);
			cbiCancelGeneration.IsEnabled = (_running && !_cancelGeneration);
			cbiSaveProject.IsEnabled = (_project != null && !_running);
			cbiSaveProjectAs.IsEnabled = (_project != null && !_running);
			cbiAddExistingMetadataFile.IsEnabled = (_project != null && !_running);
			cbiAddNewMetadataFile.IsEnabled = (_project != null && !_running);
			cbiAddCodeGenCommand.IsEnabled = (_project != null && !_running);
			cbiEditProjectParameters.IsEnabled = (_project != null && !_running);
			cbiOpenProject.IsEnabled = (!_running);
			cbiNewProject.IsEnabled = (!_running);
		}

		private void FillTreeview() {
			if (_project == null) {
				tvwProject.Nodes.Clear();
				tvwProject.Nodes.Add("No projet loaded");
				return;
			}
			else {
				RefreshTreeview();
				//tvwProject.Nodes[0].Expand();
			}
		}

		private void RefreshTreeview() {
			try {
				foreach (TreeNode node in tvwProject.Nodes) {
					if (node.Tag == null) {
						node.Remove();
					}
				}
				tvwProject.BeginUpdate();
				tvwProject.ImageList = Images.GetImageList();

				TreeNode projectNode = SetupProjectNode();
				foreach (TreeNode node in projectNode.Nodes) 
				{
					if (node.Tag == null) 
					{
						node.Remove();
					}
				}

				TreeNode generatorCommandsNode = SetupGeneratorCommandsNode(projectNode);
				SetupDataSourcesNode(projectNode);
				SetupMetadataFilesNode(projectNode);
				SetupPackagesNode(projectNode);

				projectNode.Expand();
				generatorCommandsNode.Expand();

			}
			finally {
				tvwProject.EndUpdate();
			}
		}

		private TreeNode SetupProjectNode() {
			TreeNode projectNode = GetContextNode(_project, tvwProject.Nodes);

			if (projectNode == null) {
				projectNode = tvwProject.Nodes.Add(_project.Name);
				projectNode.Expand();

				projectNode.Text = _project.Name;
				projectNode.ImageIndex = Images.Indexes.DocumentCh3Etah;
				projectNode.SelectedImageIndex = Images.Indexes.DocumentCh3Etah;
				projectNode.Tag = _project;

				TreeNode inputParametersNode = projectNode.Nodes.Add("Global Parameters");
				inputParametersNode.ImageIndex = Images.Indexes.Properties;
				inputParametersNode.SelectedImageIndex = Images.Indexes.Properties;
				inputParametersNode.Tag = _project.InputParameters;
			}

			return projectNode;
		}

		private TreeNode SetupMetadataFilesNode(TreeNode projectNode) {
			
			TreeNode metadataFilesNode = GetContextNode(_project.MetadataFiles, tvwProject.Nodes);
			SortedList sortedItems = new SortedList();
			
			if (metadataFilesNode == null) {
				metadataFilesNode = projectNode.Nodes.Add("Metadata Files");
			}
			
			//TreeNode metadataFilesNode = projectNode.Nodes.Add("Metadata Files");
			metadataFilesNode.ImageIndex = Images.Indexes.FolderOpen;
			metadataFilesNode.SelectedImageIndex = Images.Indexes.FolderOpen;
			metadataFilesNode.Tag = _project.MetadataFiles;
			//metadataFilesNode.Nodes.Clear();
			
			foreach (MetadataFile file in _project.MetadataFiles) {
				TreeNode node = GetContextNode(file, metadataFilesNode.Nodes);
				if (node == null && !sortedItems.ContainsKey(file.Name)) {
					node = new TreeNode(file.Name);
					sortedItems.Add(file.Name, node);
				}
				try 
				{
					node.Tag = file;
					file.Load();
					node.Text = file.Name;
					node.ImageIndex = Images.Indexes.DocumentText;
					node.SelectedImageIndex = Images.Indexes.DocumentText;
					SetupMetadataFileNode(node);
				}
				catch
				{
					int i = 1;
					if (node == null)
						node = new TreeNode(file.Name);
					node.Text = file.Name + ERROR_MESSAGE_1;
					while (sortedItems.ContainsKey(node.Text))
					{
						node.Text = file.Name + string.Format(" ({0})", i++) + ERROR_MESSAGE_1;
					}
					sortedItems.Add(node.Text, node);
					node.ImageIndex = Images.Indexes.Delete;
					node.SelectedImageIndex = Images.Indexes.Delete;
				}
			}
			
			foreach (DictionaryEntry entry in sortedItems)
			{
				TreeNode node = (TreeNode)entry.Value;
				metadataFilesNode.Nodes.Add(node);
				if (node.Text.IndexOf("ERROR LOADING FILE") > 0)
					node.Parent.Expand();	
			}
			
			foreach (TreeNode node in metadataFilesNode.Nodes) {
				if (
					node.Tag != null && node.Tag is MetadataFile &&
					!_project.MetadataFiles.Contains((MetadataFile) node.Tag)) {
					node.Remove();
				}
			}
			if (Directory.Exists(_project.GetFullMetadataPath())) {
				string[] files = Directory.GetFiles(_project.GetFullMetadataPath(), "*.xml");
				foreach (string file in files) {
					if (!_project.MetadataFiles.Contains(file)) {
						TreeNode node = metadataFilesNode.Nodes.Add(Path.GetFileName(file));
						node.ImageIndex = Images.Indexes.New;
						node.SelectedImageIndex = Images.Indexes.New;
						node.Tag = new MetadataFilePlaceholder(file);
					}
				}
			}
			return metadataFilesNode;
		}

		private void SetupMetadataFileNode(TreeNode node) {
			if (node.Nodes.Count > 0) {
//				foreach(TreeNode entityNode in node.Nodes) {
//					Entity entity = (Entity) entityNode.Tag;
//				}
				node.Nodes.Clear();
			}

//			foreach (Entity entity in ((MetadataFile) node.Tag).MetadataEntities) {
//				TreeNode entityNode = node.Nodes.Add(entity.Name);
//				entityNode.ImageIndex = Images.Indexes.Entity;
//				entityNode.SelectedImageIndex = Images.Indexes.Entity;
//				entityNode.Tag = entity;
//
//				TreeNode fieldsNode = entityNode.Nodes.Add("Fields");
//				fieldsNode.ImageIndex = Images.Indexes.FolderOpen;
//				fieldsNode.SelectedImageIndex = Images.Indexes.FolderOpen;
//				fieldsNode.Tag = entity.Fields;
//				SetupMetadataCollectionNode(fieldsNode, entity.Fields, Images.Indexes.EntityField);
//
//				TreeNode indexesNode = entityNode.Nodes.Add("Indexes");
//				indexesNode.ImageIndex = Images.Indexes.FolderOpen;
//				indexesNode.SelectedImageIndex = Images.Indexes.FolderOpen;
//				indexesNode.Tag = entity.Indexes;
//				SetupMetadataCollectionNode(indexesNode, entity.Indexes, Images.Indexes.EntityIndex);
//
//				TreeNode linksNode = entityNode.Nodes.Add("Links");
//				linksNode.ImageIndex = Images.Indexes.FolderOpen;
//				linksNode.SelectedImageIndex = Images.Indexes.FolderOpen;
//				linksNode.Tag = entity.Links;
//				SetupMetadataCollectionNode(linksNode, entity.Links, Images.Indexes.Properties);
//			}
		}

		private void SetupMetadataCollectionNode(TreeNode node, ICollection items, int image) {
			foreach (object item in items) {
				TreeNode itemNode = node.Nodes.Add(((MetadataNodeBase) item).Name);
				itemNode.ImageIndex = image;
				itemNode.SelectedImageIndex = image;
				itemNode.Tag = item;
			}
		}

		private TreeNode SetupGeneratorCommandsNode(TreeNode projectNode) {
			TreeNode generatorCommandsNode = GetContextNode(_project.GeneratorCommands, tvwProject.Nodes);
			if (generatorCommandsNode == null) {
				generatorCommandsNode = projectNode.Nodes.Add("Generator Commands");
			}
			//TreeNode generatorCommandsNode = projectNode.Nodes.Add("Generator Commands");
			generatorCommandsNode.ImageIndex = Images.Indexes.FolderOpen;
			generatorCommandsNode.SelectedImageIndex = Images.Indexes.FolderOpen;
			generatorCommandsNode.Tag = _project.GeneratorCommands;

			foreach (GeneratorCommand command in _project.GeneratorCommands) {
				TreeNode node = GetContextNode(command, generatorCommandsNode.Nodes);
				if (node == null) {
					node = generatorCommandsNode.Nodes.Add(command.Name);
				}
				node.Text = command.Name;
				//TreeNode node = generatorCommandsNode.Nodes.Add(command.Name);
				node.ImageIndex = Images.Indexes.DocumentArrowGreen;
				node.SelectedImageIndex = Images.Indexes.DocumentArrowGreen;
				node.Tag = command;
			}
			foreach (TreeNode node in generatorCommandsNode.Nodes) {
				if (node.Tag != null && !_project.GeneratorCommands.Contains((GeneratorCommand) node.Tag)) {
					node.Remove();
				}
			}

			return generatorCommandsNode;
		}

		private TreeNode SetupDataSourcesNode(TreeNode projectNode) 
		{
			TreeNode dataSourcesNode = GetContextNode(_project.DataSources, tvwProject.Nodes);
			if (dataSourcesNode == null) 
			{
				dataSourcesNode = projectNode.Nodes.Add("Data Sources");
			}
			//TreeNode generatorCommandsNode = projectNode.Nodes.Add("Generator Commands");
			dataSourcesNode.ImageIndex = Images.Indexes.DataSourceFolder;
			dataSourcesNode.SelectedImageIndex = Images.Indexes.DataSourceFolder;
			dataSourcesNode.Tag = _project.DataSources;

			foreach (DataSource ds in _project.DataSources) 
			{
				TreeNode node = GetContextNode(ds, dataSourcesNode.Nodes);
				if (node == null) 
				{
					node = dataSourcesNode.Nodes.Add(ds.Name);
				}
				node.Text = ds.Name;
				//TreeNode node = generatorCommandsNode.Nodes.Add(command.Name);
				node.ImageIndex = Images.Indexes.DataSource;
				node.SelectedImageIndex = Images.Indexes.DataSource;
				node.Tag = ds;
			}
			foreach (TreeNode node in dataSourcesNode.Nodes) 
			{
				if (node.Tag != null && !_project.DataSources.Contains((DataSource) node.Tag)) 
				{
					node.Remove();
				}
			}

			return dataSourcesNode;
		}

		private TreeNode SetupPackagesNode(TreeNode projectNode) 
		{
			TreeNode packagesNode;
			try
			{
				packagesNode = GetContextNode(_project.ListPackages(), tvwProject.Nodes);
			}
			catch
			{
				packagesNode = projectNode.Nodes.Add("[ERROR LOADING PACKAGES - Check the 'PackagesBaseDir' of your project.]");
				packagesNode.ImageIndex = Images.Indexes.Delete;
				packagesNode.SelectedImageIndex = Images.Indexes.Delete;
				return packagesNode;
			}

			if (packagesNode == null) 
			{
				packagesNode = projectNode.Nodes.Add("Packages");
				packagesNode.Expand();
			}

			packagesNode.ImageIndex = Images.Indexes.FolderOpen;
			packagesNode.SelectedImageIndex = Images.Indexes.FolderOpen;
			packagesNode.Tag = _project.ListPackages();
			
			foreach (Package p in _project.ListPackages()) 
			{
				TreeNode node = GetContextNode(p, packagesNode.Nodes);
				if (node == null) 
				{
					node = packagesNode.Nodes.Add(p.Name);
					node.Parent.Expand();
				}
				node.Text = p.Name;
				//TreeNode node = generatorCommandsNode.Nodes.Add(command.Name);
				node.ImageIndex = Images.Indexes.Icons;
				node.SelectedImageIndex = Images.Indexes.Icons;
				node.Tag = p;
				SetupTemplatesNode(node, p);
				SetupMacrosNode(node, p);
			}
			foreach (TreeNode node in packagesNode.Nodes) 
			{
				bool found = false;
				foreach (Package p in _project.ListPackages())
				{
					if (p == node.Tag)
					{
						found = true;
						break;
					}
				}
				if (!found) 
				{
					node.Remove();
				}
			}
			
			return packagesNode;
		}

		private void SetupTemplatesNode(TreeNode packageNode, Package package) 
		{

			TreeNode templatesNode = GetContextNode(package.Templates, packageNode.Nodes);
			if (templatesNode == null) 
			{
				templatesNode = packageNode.Nodes.Add("Templates");
				packageNode.Expand();
			}

			templatesNode.ImageIndex = Images.Indexes.FolderOpen;
			templatesNode.SelectedImageIndex = Images.Indexes.FolderOpen;
			templatesNode.Tag = package.Templates;
			
			foreach (Template t in package.Templates) 
			{
				TreeNode node = GetContextNode(t, templatesNode.Nodes);
				if (node == null) 
				{
					node = templatesNode.Nodes.Add(t.Name);
				}
				node.Text = Path.GetFileName(t.GetFullPath());
				//TreeNode node = generatorCommandsNode.Nodes.Add(command.Name);
				node.ImageIndex = Images.Indexes.DocumentText;
				node.SelectedImageIndex = Images.Indexes.DocumentText;
				node.Tag = t;
			}
			foreach (TreeNode node in templatesNode.Nodes) 
			{
				if (node.Tag != null && !package.Templates.Contains((Template)node.Tag)) 
				{
					node.Remove();
				}
			}
		}
		private void SetupMacrosNode(TreeNode packageNode, Package package) 
		{

			TreeNode macrosNode = GetContextNode(package.MacroLibraries, packageNode.Nodes);
			if (macrosNode == null) 
			{
				macrosNode = packageNode.Nodes.Add("Macro Libraries");
				packageNode.Expand();
			}

			macrosNode.ImageIndex = Images.Indexes.FolderOpen;
			macrosNode.SelectedImageIndex = Images.Indexes.FolderOpen;
			macrosNode.Tag = package.MacroLibraries;
			
			foreach (MacroLibrary m in package.MacroLibraries) 
			{
				TreeNode node = GetContextNode(m, macrosNode.Nodes);
				if (node == null) 
				{
					node = macrosNode.Nodes.Add(m.Address);
				}
				node.Text = Path.GetFileName(m.Address);
				//TreeNode node = generatorCommandsNode.Nodes.Add(command.Name);
				node.ImageIndex = Images.Indexes.DocumentText;
				node.SelectedImageIndex = Images.Indexes.DocumentText;
				node.Tag = m;
			}
			foreach (TreeNode node in macrosNode.Nodes) 
			{
				if (node.Tag != null && !package.MacroLibraries.Contains((MacroLibrary)node.Tag)) 
				{
					node.Remove();
				}
			}
		}
		
		private void SetupTreeviewContextMenu() 
		{
			treeviewContextMenu.Items.Clear();

			// get selected node
			TreeNode node = tvwProject.SelectedNode; //.GetNodeAt(tvwProject.PointToClient(Cursor.Position));
			if (node == null) {
				return; //node = tvwProject.SelectedNode;
			}

			if (_project == null || node == null || node.Tag == null) {
				return;
			}
			if (node.Tag.GetType() == typeof (Project)) {
				treeviewContextMenu.Items.Add(cbiEditProjectParameters);
				treeviewContextMenu.Items.Add(cbiRunProject);
			}
			if (node.Tag.GetType() == typeof (MetadataFile)) {
				CommandBarItem command;
				foreach (IMetadataEntity entity in ((MetadataFile) node.Tag).MetadataEntities) {
					if ((entity as Entity) != null) {
						command = new CommandBarButton(Images.Edit, "&Edit O/R Entity", new EventHandler(EditOREntity_Click));
						command.Tag = entity;
						treeviewContextMenu.Items.Add(command);
					}
				}
				command = new CommandBarButton(Images.Delete, "&Remove Metadata File", new EventHandler(RemoveMetadataFile_Click));
				treeviewContextMenu.Items.Add(command);
				command.Tag = node.Tag;
			}
			if (node.Tag.GetType() == typeof (MetadataFilePlaceholder)) {
				CommandBarItem command =
					new CommandBarButton(Images.DocumentText, "&Include Metadata File", new EventHandler(IncludeMetadataFile_Click));
				treeviewContextMenu.Items.Add(command);
				command.Tag = node.Tag;
			}
			if (node.Tag.GetType() == typeof (MetadataFileCollection)) {
				treeviewContextMenu.Items.Add(cbiAddExistingMetadataFile);
				treeviewContextMenu.Items.Add(cbiAddNewMetadataFile);
			}
			if (node.Tag.GetType() == typeof (CodeGeneratorCommand)) {
				CommandBarItem command;
				command = new CommandBarButton("&Edit Template", new EventHandler(EditTemplate_Click));
				command.Tag = node.Tag;
				treeviewContextMenu.Items.Add(command);
				command = new CommandBarButton(Images.ArrowGreen, "&Run Command", new EventHandler(RunCommand_Click));
				command.Tag = node.Tag;
				treeviewContextMenu.Items.Add(command);
				command = new CommandBarButton(Images.Delete, "Re&move Command", new EventHandler(RemoveCommand_Click));
				command.Tag = node.Tag;
				treeviewContextMenu.Items.Add(command);
			}
			if (node.Tag.GetType() == typeof (GeneratorCommandCollection)) {
				treeviewContextMenu.Items.Add(cbiAddCodeGenCommand);
			}
			if (node.Tag.GetType() == typeof (DataSourceCollection)) {
				treeviewContextMenu.Items.Add(cbiAddDataSource);
			}

		}

		public void SelectContextItem(object contextItem) {
			TreeNode node = GetContextNode(contextItem, tvwProject.Nodes);
			if (node != null) {
				tvwProject.SelectedNode = node;
			}
		}

		private TreeNode GetContextNode(object contextItem, TreeNodeCollection nodes) {
			foreach (TreeNode node in nodes) {
				if (node.Tag == contextItem) {
					return node;
				}
				TreeNode subNode = GetContextNode(contextItem, node.Nodes);
				if (subNode != null) {
					return subNode;
				}
			}
			return null;
		}

		#endregion UI code

		#region CommandBar code

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			// Handle keyboard shortcuts
			if (commandBarManager.PreProcessMessage(ref msg)) {
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void SetupCommandBars() {
			SetupCommandBarButtons();

			// File menu
			CommandBarMenu fileMenu = menuBar.Items.AddMenu("&File");
			fileMenu.Items.Add(cbiNewProject);
			fileMenu.Items.Add(cbiOpenProject);
			fileMenu.Items.Add(cbiSaveProject);
			fileMenu.Items.Add(cbiSaveProjectAs);
			fileMenu.Items.AddSeparator();
			fileMenu.Items.Add(cbiSaveDocument);
			fileMenu.Items.AddSeparator();
			SetupMRUList(fileMenu);
			fileMenu.Items.Add(cbiExit);

			// View menu
			CommandBarMenu viewMenu = menuBar.Items.AddMenu("&View");
			viewMenu.Items.Add(cbiViewProjectExplorer);
			viewMenu.Items.Add(cbiViewPropertiesWindow);
			viewMenu.Items.Add(cbiViewOutputWindow);

			CommandBarMenu projectMenu = menuBar.Items.AddMenu("&Project");
			projectMenu.Items.Add(cbiRunProject);
			projectMenu.Items.AddSeparator();
			projectMenu.Items.Add(cbiEditProjectParameters);
			projectMenu.Items.AddSeparator();
			projectMenu.Items.Add(cbiAddDataSource);
			projectMenu.Items.Add(cbiAddCodeGenCommand);
			projectMenu.Items.Add(cbiAddExistingMetadataFile);
			projectMenu.Items.Add(cbiAddNewMetadataFile);

			CommandBarMenu helpMenu = menuBar.Items.AddMenu("&Help");
			helpMenu.Items.Add(cbiWebSite);
			helpMenu.Items.AddSeparator();
			helpMenu.Items.Add(cbiRunTests);
			helpMenu.Items.AddSeparator();
			helpMenu.Items.Add(cbiFixFileAssociations);
			helpMenu.Items.AddSeparator();
			helpMenu.Items.Add(cbiAbout);

			// Toolbar
			toolBar.NewLine = true;
			toolBar.Items.Add(cbiNewProject);
			toolBar.Items.Add(cbiOpenProject);
			toolBar.Items.Add(cbiSaveDocument);
			toolBar.Items.AddSeparator();
			toolBar.Items.Add(cbiRunProject);
			toolBar.Items.Add(cbiCancelGeneration);

			// Add to user interface
			commandBarManager.CommandBars.Add(menuBar);
			commandBarManager.CommandBars.Add(toolBar);
			Controls.Add(commandBarManager);
		}

		private void SetupMRUList(CommandBarMenu fileMenu) {
			bool added = true;

			_mruList = new MRUList();

			foreach (MRUList.MRUEntry entry in _mruList) {
				if (entry.Path != null) {
					CommandBarButton item = new CommandBarButton(entry.ToString(), new EventHandler(MRUEntry_Click));
					item.Tag = entry;
					fileMenu.Items.Add(item);
					added = true;
				}
			}

			if (added) {
				fileMenu.Items.AddSeparator();
			}
		}


		private void SetupCommandBarButtons() {
			cbiSaveDocument =
				new CommandBarButton(
					Images.Save, "&Save Active Document", new EventHandler(SaveDocument_Click), Keys.Control | Keys.S);
			cbiNewProject =
				new CommandBarButton(Images.New, "&New Project", new EventHandler(NewProject_Click), Keys.Control | Keys.N);
			cbiOpenProject =
				new CommandBarButton(Images.Open, "&Open Project", new EventHandler(OpenProject_Click), Keys.Control | Keys.O);
			cbiSaveProject =
				new CommandBarButton(
					Images.Save, "Save &Project", new EventHandler(SaveProject_Click), Keys.Control | Keys.Shift | Keys.S);
			cbiSaveProjectAs = new CommandBarButton("Save Project &As", new EventHandler(SaveProjectAs_Click), Keys.F12);
			cbiRunProject =
				new CommandBarButton(
					Images.ArrowGreen, "&Run Project", new EventHandler(RunProject_Click), Keys.Control | Keys.F5);
			cbiCancelGeneration =
				new CommandBarButton(Images.Stop, "Cancel Generation", new EventHandler(CancelGeneration_Click));

			cbiAddExistingMetadataFile =
				new CommandBarButton("Add &Existing Metadata File(s)", new EventHandler(AddExistingMetadataFile_Click));
			cbiAddNewMetadataFile =
				new CommandBarButton(Images.New, "Add New &Metadata File", new EventHandler(AddNewMetadataFile_Click));
			cbiAddCodeGenCommand =
				new CommandBarButton(
					Images.DocumentArrowGreen, "Add New Code Generation Command", new EventHandler(AddNCodeGenCommand_Click));
			cbiAddDataSource =
				new CommandBarButton(Images.DataSourceNew, "Add New Data Source", new EventHandler(AddDataSource_Click));
			cbiEditProjectParameters =
				new CommandBarButton(
					"Edit Code Generation &Parameters", new EventHandler(EditProjectParameters_Click),
					Keys.Control | Keys.Shift | Keys.P);

			cbiViewProjectExplorer =
				new CommandBarButton(
					Images.Ch3Etah, "&Project Explorer", new EventHandler(ViewProjectExplorer_Click), Keys.Control | Keys.Alt | Keys.L);
			cbiViewPropertiesWindow =
				new CommandBarButton(Images.Properties, "&Properties", new EventHandler(ViewProperties_Click), Keys.F4);
			cbiViewOutputWindow =
				new CommandBarButton(Images.Output, "&Output", new EventHandler(ViewOutput_Click));

			cbiWebSite = new CommandBarButton(Images.Home, "CH3ETAH &Web Site", new EventHandler(WebSite_Click));
			cbiRunTests = new CommandBarButton(Images.Properties, "&Run Tests", new EventHandler(RunTests_Click));
			cbiFixFileAssociations =
				new CommandBarButton(Images.Tools, "Fi&x file associations", new EventHandler(FixFileAssociations_Click));
			cbiAbout = new CommandBarButton(Images.Help, "&About", new EventHandler(About_Click));

			cbiExit = new CommandBarButton("E&xit", new EventHandler(Exit_Click));
		}

		private void ViewOutput_Click(object sender, EventArgs e) {
			outputWindow.Show();
		}

		private void ViewProperties_Click(object sender, EventArgs e) {
			propertiesWindow.Show();
		}

		private void ViewProjectExplorer_Click(object sender, EventArgs e) {
			projectExplorer.Show();
		}

		#endregion CommandBar code

		#region Test Cases

		#region Metadata Files

//		private void Test_MetadataFiles() {
//			try {
//				Trace.WriteLine("Create a new, non-branded metadata file and save...");
//				Trace.Indent();
//				MetadataFile f = new MetadataFile();
//				f.MetadataEntities.Add(new XmlMetadataEntity());
//				XmlMetadataEntity en = (XmlMetadataEntity) f.MetadataEntities[0];
//				en.XmlNode = (new XmlDocument()).CreateElement("Books");
//				en.XmlNode.InnerXml = "<book price=\"100.00\"/>";
//				f.Save(".\\test\\output\\test_create.xml");
//				Trace.Unindent();
//			}
//			catch (Exception ex) {
//				Trace.WriteLine("\r\n" + ex.ToString() + "\r\n");
//			}
//
//			try {
//				Trace.WriteLine("Load a non-branded metadata file and save as...");
//				Trace.Indent();
//				MetadataFile f = new MetadataFile(null, ".\\test\\output\\test_create.xml");
//				f.Save(".\\test\\output\\test_loadsaveas.xml");
//
//				// UNDONE: need to test whether files are identical
//
//				Trace.Unindent();
//			}
//			catch (Exception ex) {
//				Trace.WriteLine("\r\n" + ex.ToString() + "\r\n");
//			}
//
//			try {
//				Trace.WriteLine("Load plain XML file and save as....");
//				Trace.Indent();
//				MetadataFile f = new MetadataFile(null, ".\\test\\raw.xml");
//				f.Save(".\\test\\output\\test_rawsaveas.xml");
//
//				// UNDONE: need to test whether files are identical
//				// this can be a seperate method which takes source and dest file names
//
//				Trace.Unindent();
//			}
//			catch (Exception ex) {
//				Trace.WriteLine("\r\n" + ex.ToString() + "\r\n");
//			}
//
//
//			// Create branded metadata file and save
//
//			// Load a branded metadata file and save as
//
//			// Create mixed (branded/not branded) metadata file and save
//
//			// Load a mixed (branded/not branded) metadata file and save as
//
//		}
//

		#endregion Metadata Files

		#region Project

		private void Test_Project() {
			try {
				Trace.WriteLine("Create a new project and save...");
				Trace.Indent();
				Project p = new Project();
				p.Save(".\\test\\output\\test_projectcreate.ch3");
				p.MetadataBaseDir = "..\\";
				MetadataFile mf = new MetadataFile(p, "raw.xml");
				p.MetadataFiles.Add(mf);
				CodeGeneratorCommand gc = new CodeGeneratorCommand();
				p.GeneratorCommands.Add(gc);
				gc.IndividualMetadataFiles.Add(p.MetadataFiles[0]);
// 				gc.GroupedMetadataFiles.Add(p.MetadataFiles[0]);
				gc.Package = "CSLA";
				gc.Template = "SP_S_Object_By_Index";
				p.Save();
				Trace.Unindent();
			}
			catch (Exception ex) {
				Trace.WriteLine("\r\n" + ex.ToString() + "\r\n");
			}

			try {
				Trace.WriteLine("Load project file and save as...");
				Trace.Indent();
				Project p = Project.Load(".\\test\\output\\test_projectcreate.ch3");
				p.Save(".\\test\\output\\test_projectsaveas.ch3");

				// UNDONE: need to test whether files are identical

				Trace.Unindent();
			}
			catch (Exception ex) {
				Trace.WriteLine("\r\n" + ex.ToString() + "\r\n");
			}
		}

		#endregion Project

		#region CodeGeneratorCommand

		private void Test_CodeGeneratorCommand() {
			try {
				Trace.WriteLine("Create a new project and generate using OREntity...");
				Trace.Indent();
				Project p = new Project();
				p.Save(".\\test\\output\\test_ncodegencommandproject_01.ch3");
				p.MetadataBaseDir = "..\\";
				p.TemplatePackageBaseDir = "..\\";
				p.OutputBaseDir = "..\\Output";
				MetadataFile mf = new MetadataFile(p, "UsuarioInformatica.xml");
				p.MetadataFiles.Add(mf);
				CodeGeneratorCommand gc = new CodeGeneratorCommand();
				p.GeneratorCommands.Add(gc);
				gc.IndividualMetadataFiles.Add(p.MetadataFiles[0]);
				gc.Package = "TEST_NCODEGEN_PACKAGE";
				gc.Template = "SPs_CRUD_Object";
				p.GeneratorCommands.Add(gc);
				gc.InputParameters.Add("FileNamePartial", "NCodeGen_Output_01");
				gc.OutputPath = "test_${FileNamePartial}.sql";
				p.Save();
				gc.Execute();
				// UNDONE: need to test whether files were outputted correctly

				Trace.Unindent();
			}
			catch (Exception ex) {
				Trace.WriteLine("\r\n" + ex.ToString() + "\r\n");
			}

		}

		#endregion CodeGeneratorCommand

		#endregion Test Cases

		#region CloseDocuments

		private bool CloseDocuments() {
			ObjectEditorForm[] forms = new ObjectEditorForm[DockPanel.Documents.Length];
			int c = 0;
			foreach (object form in DockPanel.Documents) {
				if (form is ObjectEditorForm) {
					forms[c++] = (ObjectEditorForm) form;
				}
			}
			foreach (ObjectEditorForm form in forms) {
				if (form != null)
				{
					bool cancel = false;
					form.UnloadEditorPanel(out cancel);
					if (cancel) 
					{
						return false;
					}
					form.Close();
				}
			}
			SaveDockSettings();
			return true;
		}

		#endregion CloseDocuments

		#region Events

		private void NewProject_Click(object sender, EventArgs e) {
			Text = sender.ToString();
			NewProject();
		}

		private void OpenProject_Click(object sender, EventArgs e) {
			OpenProject();
		}

		private void MRUEntry_Click(object sender, EventArgs e) {
			MRUList.MRUEntry entry = (MRUList.MRUEntry) ((CommandBarItem) sender).Tag;
			OpenProject(entry.Path);
		}

		private void SaveProject_Click(object sender, EventArgs e) {
			try {
				SaveProject();
			}
			catch (UnauthorizedAccessException ex) {
				MessageBox.Show(
					"Error saving project file: \r\n\r\n" + ex.Message +
					"\r\n\r\n Make sure that you have permission to access the file and that it is not read-only.", "Error saving file",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch (Exception ex) {
				MessageBox.Show("Error saving project file: \r\n\r\n" + ex.ToString(), "Error saving file", MessageBoxButtons.OK,
				                MessageBoxIcon.Error);
			}
		}

		private void SaveDocument_Click(object sender, EventArgs e) {
			try {
				SaveDocument();
			}
			catch (UnauthorizedAccessException ex) {
				MessageBox.Show(
					"Error saving document: \r\n\r\n" + ex.Message +
					"\r\n\r\n Make sure that you have permission to access the file and that it is not read-only.",
					"Error saving document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch (Exception ex) {
				MessageBox.Show("Error saving document: \r\n\r\n" + ex.ToString(), "Error saving document", MessageBoxButtons.OK,
				                MessageBoxIcon.Error);
			}
		}

		private void SaveProjectAs_Click(object sender, EventArgs e) {
			SaveProjectAs();
		}

		private void RunProject_Click(object sender, EventArgs e) {
			RunProject();
			// TODO: open nvelocity log file
		}

		private void CancelGeneration_Click(object sender, EventArgs e) {
			_cancelGeneration = true;
			EnableCommandBarButtons();
		}

		private void WebSite_Click(object sender, EventArgs e) {
			Process.Start(@"http://ch3etah.sourceforge.net/");
		}

		private void EditProjectParameters_Click(object sender, EventArgs e) {
			IObjectEditor editor = new InputParameterCollectionEditor(_project.InputParameters);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void AddNewMetadataFile_Click(object sender, EventArgs e) {
			// TODO: Add metadata file to appropriate generator commands
			MetadataFile file = new MetadataFile(_project);
			_project.MetadataFiles.Add(file);
			RefreshUI();
			IObjectEditor editor = new MetadataFileEditor();
			editor.SelectedObject = file;
			editor.SelectedObjectChanged += new EventHandler(IObjectEditor_SelectedObjectChanged);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void AddExistingMetadataFile_Click(object sender, EventArgs e) {
			// TODO: Add metadata file to appropriate generator commands
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "XML Files (.XML)|*.xml|All Files (*.*)|*.*";
			dlg.Multiselect = true;
			if (dlg.ShowDialog(this) == DialogResult.OK) {
				foreach (string fileName in dlg.FileNames) {
					IncludeMetadataFile(fileName);
				}
				RefreshUI();
			}
		}

		private void RemoveMetadataFile_Click(object sender, EventArgs e) {
			if (
				MessageBox.Show("Are you sure you want to remove this metadata file?", "Confirmation", MessageBoxButtons.YesNo,
				                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) {
				return;
			}
			// TODO: Ask user if they want to delete, remove or cancel
			// TODO: remove file from generator commands
			MetadataFile file = (MetadataFile) ((CommandBarItem) sender).Tag;
			_project.MetadataFiles.Remove(file);
			RefreshUI();
		}

		private void IncludeMetadataFile_Click(object sender, EventArgs e) {
			// TODO: Add metadata file to appropriate generator commands
			IncludeMetadataFile(((MetadataFilePlaceholder) ((CommandBarItem) sender).Tag).FileName);
			RefreshUI();
		}

		private void IncludeMetadataFile(string fileName) {
			string oldDir = Directory.GetCurrentDirectory();
			try {
				Directory.SetCurrentDirectory(Path.GetDirectoryName(_project.FileName));
				if (_project.MetadataBaseDir != "") {
					Directory.SetCurrentDirectory(_project.MetadataBaseDir);
				}
				string path = PathResolver.GetRelativePath(Directory.GetCurrentDirectory(), fileName);
				_project.MetadataFiles.Add(new MetadataFile(_project, path));
			}
			finally {
				Directory.SetCurrentDirectory(oldDir);
			}
		}

		private void AddNCodeGenCommand_Click(object sender, EventArgs e) {
			CodeGeneratorCommand command = new CodeGeneratorCommand();
			_project.GeneratorCommands.Add(command);
			RefreshUI();
			IObjectEditor editor = new CodeGeneratorCommandEditor(command);
			editor.SelectedObjectChanged += new EventHandler(IObjectEditor_SelectedObjectChanged);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void AddDataSource_Click(object sender, EventArgs e) {
			DataSource ds = new DataSource();
			_project.DataSources.Add(ds);
			RefreshUI();
			TreeNode dsNode = GetContextNode(ds, tvwProject.Nodes);
			dsNode.EnsureVisible();
			tvwProject.SelectedNode = dsNode;
			IObjectEditor editor = new OleDbDataSourceEditor(ds);
			editor.SelectedObjectChanged += new EventHandler(IObjectEditor_SelectedObjectChanged);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void EditTemplate_Click(object sender, EventArgs e) {
			CodeGeneratorCommand command = (CodeGeneratorCommand) ((CommandBarItem) sender).Tag;
			if (command.Template == "") {
				MessageBox.Show("You must specify a template for this command first!", "Specify a template", MessageBoxButtons.OK,
				                MessageBoxIcon.Exclamation);
				return;
			}
			CodeGenerationTemplateEditor editor = new CodeGenerationTemplateEditor(command);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void EditOREntity_Click(object sender, EventArgs e) {
			MetadataFile file = tvwProject.SelectedNode.Tag as MetadataFile;
			OREntityEditor editor = new OREntityEditor(file);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void RunCommand_Click(object sender, EventArgs e) {
			GeneratorCommand command = (GeneratorCommand) ((CommandBarItem) sender).Tag;
			try {
				command.Execute();
				MessageBox.Show("Done!");
			}
			catch (Exception ex) {
				MessageBox.Show("Error running command: \r\n\r\n" + ex.ToString());
			}
		}

		private void RemoveCommand_Click(object sender, EventArgs e) {
			if (
				MessageBox.Show("Are you sure you want to remove this command?", "Confirmation", MessageBoxButtons.YesNo,
				                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) {
				return;
			}
			GeneratorCommand command = (GeneratorCommand) ((CommandBarItem) sender).Tag;
			_project.GeneratorCommands.Remove(command);
			RefreshUI();
		}

		private void About_Click(object sender, EventArgs e) {
			(new AboutDialog()).ShowDialog(this);
		}

		private void Exit_Click(object sender, EventArgs e) {
			Close();
		}

		private void tvwProject_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				tvwProject.SelectedNode = tvwProject.GetNodeAt(e.X, e.Y);
			}
		}

		private void TreeviewContextMenu_Popup(object sender, EventArgs e) {
			try {
				SetupTreeviewContextMenu();
			}
			catch (Exception ex) {
				MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void tvwProject_AfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node != null) {
				object nodeData = e.Node.Tag;
				if (nodeData is MetadataNodeBase) {
					propertyGrid.Enabled = false;
				}
				else {
					propertyGrid.Enabled = true;
				}
				propertyGrid.SelectedObject = nodeData;
			}
		}

		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
			RefreshTreeview();
		}
		private void propertyGrid_Validated(object sender, EventArgs e)
		{
			RefreshTreeview();
		}
		private void Form_Closing(object sender, CancelEventArgs e) 
		{
			CloseDocuments();
			e.Cancel = (_running || MdiChildren.Length > 0 || !DoSaveConfirmation());
		}

		private void listener_TextWritten(object sender, TraceEventArgs args) {
			string indent = "";
			for (int x = 0; x < Trace.IndentLevel; x++) {
				indent += "\t";
			}
			try {
				if (txtOutput.Text.Length > 23000)
					txtOutput.Text = "...(OUTPUT TRUNCATED)..." + txtOutput.Text.Substring(txtOutput.Text.Length - 15000);
			}
			catch {}
			txtOutput.AppendText(indent + args.TraceText);
			//statusBar.Text = args.TraceText.Trim();//txtOutput.Text.Length.ToString();
			Application.DoEvents();
		}

		private void FixFileAssociations_Click(object sender, EventArgs e) {
			try {
				if (
					MessageBox.Show("Are you sure you want to fix the shell file associations?", "Question", MessageBoxButtons.YesNo,
					                MessageBoxIcon.Question) == DialogResult.Yes) {
					(new ShellInstaller()).Install(new Hashtable());
					MessageBox.Show("File associations successfully fixed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch {
				MessageBox.Show("There was an error while fixing file associations", "Error", MessageBoxButtons.OK,
				                MessageBoxIcon.Warning);
			}
		}

		private void RunTests_Click(object sender, EventArgs e) {
			try {
				Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
				if (Directory.Exists(".\\test\\output\\")) {
					Directory.Delete(".\\test\\output\\", true);
				}
				Directory.CreateDirectory(".\\test\\output\\");
			}
			catch (Exception ex) {
				MessageBox.Show(this, "Error durring setting up tests:\r\n\r\n" + ex.ToString());
				return;
			}

			// run the tests
			try {
				_running = true;
				txtOutput.AppendText("\r\n BEGIN TESTS \r\n========================================\r\n");
//				Test_MetadataFiles();
				Test_Project();
				Test_CodeGeneratorCommand();
				txtOutput.AppendText("\r\n END TESTS \r\n========================================\r\n");
				MessageBox.Show(this, "Done testing.");
			}
			catch (Exception ex) {
				txtOutput.AppendText("\r\n========================================\r\n");
				txtOutput.AppendText("\r\n ERROR \r\n========================================\r\n");
				txtOutput.AppendText(ex.ToString());
				txtOutput.AppendText("\r\n========================================\r\n");
				MessageBox.Show(this, "Error running tests:\r\n\r\n" + ex.ToString());
			}
			finally {
				_running = false;
			}
		}

		private void tvwProject_DoubleClick(object sender, EventArgs e) {
			try
			{
				TreeNode node = tvwProject.SelectedNode; //.GetNodeAt(tvwProject.PointToClient(Cursor.Position));
				if (node == null || node.Tag == null) 
				{
					return;
				}

				IObjectEditor editor = ObjectEditorFactory.CreateObjectEditor(node.Tag);

				if (editor != null) 
				{
					editor.SelectedObjectChanged += new EventHandler(IObjectEditor_SelectedObjectChanged);
					ObjectEditorManager.OpenObjectEditor(editor);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void IObjectEditor_SelectedObjectChanged(object sender, EventArgs e) {
			RefreshTreeview();
		}


		private void MainForm_Load(object sender, EventArgs e) {

			tvwProject.Focus();

			if (autoRunFiles != null) {
				foreach (string file in autoRunFiles) {
					OpenProject(file);
					RunProject();
					CloseDocuments();
				}
				Close();
			}

			SplashScreen.Hide();
		}


		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			if (disposing) {
				_mruList.Dispose();
			}
		}

		#endregion Events

		#region Dock settings persistence

		private bool LoadDockSetings() {
			string fileName = FileSystemHelper.GetAssemblyPath(GetType().Assembly) + "\\docksettings.xml";
			try {
				DeserializeDockContent deserializeDelegate = new DeserializeDockContent(DeserializeContent);
				dockPanel1.LoadFromXml(fileName, deserializeDelegate);
				HookDockContents();
				return true;
			}
			catch {
				return false;
			}
		}

		private void SaveDockSettings() {
			string fileName = FileSystemHelper.GetAssemblyPath(GetType().Assembly) + "\\docksettings.xml";
			try {
				dockPanel1.SaveAsXml(fileName);
			}
			catch {}
		}
		
		private DockContent DeserializeContent(string typeName) {
			return Activator.CreateInstance(this.GetType().Assembly.GetType(typeName)) as DockContent;
		}

		private void CreateDockWindows() {
			outputWindow = new OutputWindow();
			outputWindow.Show(dockPanel1, DockState.DockBottomAutoHide);

			projectExplorer = new ProjectExplorer();
			projectExplorer.Show(dockPanel1, DockState.DockLeft);

			propertiesWindow = new PropertiesWindow();
			propertiesWindow.Show(projectExplorer.Pane, DockAlignment.Bottom | DockAlignment.Left, .7);
			
			HookDockContents();
		}

		private void HookDockContents() {
			foreach(DockContent content in dockPanel1.Contents) {
				if (content is ProjectExplorer) {
					projectExplorer = (ProjectExplorer) content;
					tvwProject = projectExplorer.TvwProject;
					tvwProject.AfterSelect += new TreeViewEventHandler(tvwProject_AfterSelect);
					tvwProject.DoubleClick += new EventHandler(tvwProject_DoubleClick);
					tvwProject.MouseDown += new MouseEventHandler(tvwProject_MouseDown);
				}
				else if (content is PropertiesWindow) {
					propertiesWindow = (PropertiesWindow) content;
					propertyGrid = propertiesWindow.propertyGrid;
					propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
					propertyGrid.Validated += new EventHandler(propertyGrid_Validated);
				}
				else if (content is OutputWindow) {
					outputWindow = (OutputWindow) content;
					txtOutput = outputWindow.TxtOutput;
				}
			}
		}

		#endregion

		#region IMdiContainer Members

		public DockPanel DockPanel {
			get { return dockPanel1; }
		}

		#endregion

	}

}
