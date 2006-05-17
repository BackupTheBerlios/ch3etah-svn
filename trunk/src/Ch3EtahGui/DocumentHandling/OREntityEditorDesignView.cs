using System;
using System.Collections;
using System.Windows.Forms;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Summary description for OREntityEditorDesignView.
	/// </summary>
	public class OREntityEditorDesignView : UserControl
	{
		#region Component Designer generated code
		
		private System.Windows.Forms.Panel pnlEditorToolbar;
		private System.Windows.Forms.TreeView treeViewFIL;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OREntityEditorDesignView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlEditorToolbar = new System.Windows.Forms.Panel();
			this.treeViewFIL = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// pnlEditorToolbar
			// 
			this.pnlEditorToolbar.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlEditorToolbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnlEditorToolbar.Location = new System.Drawing.Point(0, 0);
			this.pnlEditorToolbar.Name = "pnlEditorToolbar";
			this.pnlEditorToolbar.Size = new System.Drawing.Size(592, 24);
			this.pnlEditorToolbar.TabIndex = 26;
			// 
			// treeViewFIL
			// 
			this.treeViewFIL.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeViewFIL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.treeViewFIL.HideSelection = false;
			this.treeViewFIL.HotTracking = true;
			this.treeViewFIL.ImageIndex = -1;
			this.treeViewFIL.ItemHeight = 16;
			this.treeViewFIL.LabelEdit = true;
			this.treeViewFIL.Location = new System.Drawing.Point(0, 24);
			this.treeViewFIL.Name = "treeViewFIL";
			this.treeViewFIL.SelectedImageIndex = -1;
			this.treeViewFIL.ShowRootLines = false;
			this.treeViewFIL.Size = new System.Drawing.Size(176, 400);
			this.treeViewFIL.TabIndex = 27;
			this.treeViewFIL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeViewFIL_KeyDown);
			this.treeViewFIL.DoubleClick += new System.EventHandler(this.treeViewFIL_DoubleClick);
			this.treeViewFIL.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			this.treeViewFIL.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewFIL_AfterLabelEdit);
			// 
			// splitter1
			// 
			this.splitter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.splitter1.Location = new System.Drawing.Point(176, 24);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 400);
			this.splitter1.TabIndex = 28;
			this.splitter1.TabStop = false;
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.propertyGrid1.HelpVisible = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(179, 24);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(413, 400);
			this.propertyGrid1.TabIndex = 29;
			this.propertyGrid1.Text = "PropertyGrid";
			this.propertyGrid1.ToolbarVisible = false;
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// OREntityEditorDesignView
			// 
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeViewFIL);
			this.Controls.Add(this.pnlEditorToolbar);
			this.Name = "OREntityEditorDesignView";
			this.Size = new System.Drawing.Size(592, 424);
			this.ResumeLayout(false);

		}
		#endregion

		#region Constructor

		#endregion

		#region Destructor

		#endregion

		#region Constantes

		#endregion

		#region Enums

		#endregion

		#region Fields

		public event System.EventHandler OnInsert;
		public event System.EventHandler OnExclude;
		public event System.EventHandler OnDelete;
		public event System.EventHandler OnEdit;
		public event System.EventHandler OnRename;
		public event System.EventHandler OnTreeViewSelectItem;
		public event System.EventHandler AfterItemLabelEdit;

		private DocumentHandling.OREntityEditor _ParentEditor;
		public TreeNode _OldNode;
		private Boolean _EditControl = false;

		#endregion

		#region Properties

		public OREntityEditor ParentEditor
		{
			set { this._ParentEditor = value; }
		}
			
		public Entity CurrentEntity
		{
			get { return (Entity) this.TreeView.Nodes[0].Tag; }
		}
		public TreeNode CurrentEntityNode
		{
			get { return this.TreeView.Nodes[0]; }
		}
		public Panel ToolbarPanel 
		{
			get { return pnlEditorToolbar; }
		}

		public TreeView TreeView 
		{
			get { return treeViewFIL; }
		}

		public PropertyGrid PropertyGrid 
		{
			get { return propertyGrid1; }
		}

		public TreeNode CurrentSelectedNode
		{
			get
			{
				if (this.TreeView.SelectedNode != null) { return this.TreeView.SelectedNode; }
				else { return null; }
			}
		}


		#endregion

		#region Events

		private void treeViewFIL_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if (AfterItemLabelEdit != null)
			{
				this._EditControl = true;
				AfterItemLabelEdit(e.Label == null ? e.Node.Text : e.Label, null);
			}
		}

	
		#endregion

		#region Methods
		
		internal void RemoveFieldFromList(TreeNode rootNode, EntityField field)
		{
			// Looping on Child Nodes
			foreach (TreeNode tmpNode in rootNode.Nodes)
			{
				RemoveFieldFromList(tmpNode, field);
			}

			// Verify
			if (rootNode.Tag.Equals(field))
			{
				// Try to Select Previous Item
				if (rootNode.PrevVisibleNode != null && rootNode.NextVisibleNode.Tag is EntityField)
				{
					rootNode.TreeView.SelectedNode = rootNode.NextVisibleNode;
				}
				else { rootNode.TreeView.SelectedNode = rootNode.PrevVisibleNode; }

				// Remove
				rootNode.Parent.Nodes.Remove(rootNode);
			}
		}

		internal void RemoveIndexFromList(TreeNode rootNode, Index index)
		{
			// Looping on Child Nodes
			foreach (TreeNode tmpNode in rootNode.Nodes)
			{
				RemoveIndexFromList(tmpNode, index);
			}

			// Verify
			if (rootNode.Tag.Equals(index))
			{
				// Try to Select Previous Item
				if (rootNode.PrevVisibleNode != null && rootNode.NextVisibleNode.Tag is Index)
				{
					rootNode.TreeView.SelectedNode = rootNode.NextVisibleNode;
				}
				else { rootNode.TreeView.SelectedNode = rootNode.PrevVisibleNode; }

				// Remove
				rootNode.Parent.Nodes.Remove(rootNode);
			}
		}

		internal void RemoveLinkFromList(TreeNode rootNode, Link link)
		{
			// Looping on Child Nodes
			foreach (TreeNode tmpNode in rootNode.Nodes)
			{
				RemoveLinkFromList(tmpNode, link);
			}

			// Verify
			if (rootNode.Tag.Equals(link))
			{
				// Try to Select Previous Item
				if (rootNode.PrevVisibleNode != null && rootNode.NextVisibleNode != null && rootNode.NextVisibleNode.Tag is Link)
				{
					rootNode.TreeView.SelectedNode = rootNode.NextVisibleNode;
				}
				else { rootNode.TreeView.SelectedNode = rootNode.PrevVisibleNode; }

				// Remove
				rootNode.Parent.Nodes.Remove(rootNode);
			}
		}

		internal void RefreshFieldsList(TreeNode rootNode, Entity entity, bool autoSelectNewNode)
		{
			TreeNode fieldsNode;
			TreeNode currentTreeNode = null;

			// Create Objetos
			if (rootNode.Text != "Fields")
			{
				fieldsNode = new TreeNode("Fields", Images.Indexes.FolderOpen, Images.Indexes.FolderOpen);
				fieldsNode.Tag = entity.Fields;
				rootNode.Nodes.Add(fieldsNode);
			}
			else
				fieldsNode = rootNode;

			// Clear Nodes
			fieldsNode.Nodes.Clear();

			// Add Fields
			foreach (EntityField field in entity.Fields)
			{
				TreeNode oNode = null;
				if (field.IsExcluded)
					oNode = new TreeNode(field.Name, Images.Indexes.EntityField + Images.Count, Images.Indexes.EntityField + Images.Count);
				else
					oNode = new TreeNode(field.Name, Images.Indexes.EntityField, Images.Indexes.EntityField);
				oNode.Tag = field;
				fieldsNode.Nodes.Add(oNode);

				// Check to Select New Node's
				if (autoSelectNewNode)
				{
					if (oNode.Text.Equals(OREntityEditor.Const_DefaultNewFieldText))
					{
						currentTreeNode = oNode;
					}
				}
			}

			// Re-Select Current Node
			if (currentTreeNode != null)
			{
				currentTreeNode.EnsureVisible();
				this.TreeView.SelectedNode = currentTreeNode;
			}

		}

		internal void RefreshIndexesList(TreeNode rootNode, Entity entity, bool autoSelectNewNode)
		{
			TreeNode IndexesNode;

			// Create Objetos
			if (rootNode.Text != "Indexes")
			{
				IndexesNode = new TreeNode("Indexes", Images.Indexes.FolderOpen, Images.Indexes.FolderOpen);
				IndexesNode.Tag = entity.Indexes;
				rootNode.Nodes.Add(IndexesNode);
			}
			else
				IndexesNode = rootNode;

			// Clear Nodes
			IndexesNode.Nodes.Clear();

			// Add Indexes
			foreach (Index index in entity.Indexes)
			{
				TreeNode oNode = null;
				if (index.IsExcluded)
					oNode = new TreeNode(index.Name, Images.Indexes.EntityField + Images.Count, Images.Indexes.EntityField + Images.Count);
				else
					oNode = new TreeNode(index.Name, Images.Indexes.EntityField, Images.Indexes.EntityField);
				oNode.Tag = index;
				IndexesNode.Nodes.Add(oNode);

				// Check to Select New Node's
				if (autoSelectNewNode)
				{
					if (oNode.Text.Equals(OREntityEditor.Const_DefaultNewIndexText))
					{
						oNode.EnsureVisible();
						this.TreeView.SelectedNode = oNode;
					}
				}
			}
		}

		internal void RefreshLinksList(TreeNode rootNode, Entity entity, bool autoSelectNewNode)
		{
			TreeNode LinksNode;

			// Create Objetos
			if (rootNode.Text != "Links")
			{
				LinksNode = new TreeNode("Links", Images.Indexes.FolderOpen, Images.Indexes.FolderOpen);
				LinksNode.Tag = entity.Links;
				rootNode.Nodes.Add(LinksNode);
			}
			else
				LinksNode = rootNode;

			// Clear Nodes
			LinksNode.Nodes.Clear();

			// Add Links
			foreach (Link link in entity.Links)
			{
				TreeNode oNode = null;
				if (link.IsExcluded)
					oNode = new TreeNode(link.Name, Images.Indexes.EntityField + Images.Count, Images.Indexes.EntityField + Images.Count);
				else
					oNode = new TreeNode(link.Name, Images.Indexes.EntityField, Images.Indexes.EntityField);
				oNode.Tag = link;
				LinksNode.Nodes.Add(oNode);

				// Check to Select New Node's
				if (autoSelectNewNode)
				{
					if (oNode.Text.Equals(OREntityEditor.Const_DefaultNewLinkText))
					{
						oNode.EnsureVisible();
						this.TreeView.SelectedNode = oNode;
					}
				}
			}
		}

		internal void RefreshCollectionList(TreeNode collectionNode, int itemImageIndex, int itemSelectedImageIndex)
		{
			if ( !(collectionNode.Tag is IList) ) throw new ApplicationException("Node tag does not implement IList.");
			
			IList list = (IList)collectionNode.Tag;
			foreach (TreeNode node in collectionNode.Nodes)
			{
				if (node.Tag == null || !list.Contains(node.Tag))
				{
					node.Remove();
				}
			}
			foreach (object item in list)
			{
				TreeNode node = FindContextNode(item, collectionNode.Nodes);
				if (node == null)
				{
					node = new TreeNode(item.ToString());
					node.Tag = item;
					collectionNode.Nodes.Insert(list.IndexOf(item), node);
				}
				
				if (item is IMetadataNode && ((IMetadataNode)item).IsExcluded)
				{
					node.ImageIndex = itemImageIndex + Images.Count;
					node.SelectedImageIndex = itemSelectedImageIndex + Images.Count;
				}
				else
				{
					node.ImageIndex = itemImageIndex;
					node.SelectedImageIndex = itemSelectedImageIndex;
				}
			}
		}
		
		private TreeNode FindContextNode(object contextItem, TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes) 
			{
				if (node.Tag == contextItem) 
				{
					return node;
				}
				TreeNode subNode = FindContextNode(contextItem, node.Nodes);
				if (subNode != null) 
				{
					return subNode;
				}
			}
			return null;
		}
		
		#endregion

		internal void RefreshPropertyGrid()
		{
			this.TreeView_AfterSelect(null, new TreeViewEventArgs(this.TreeView.SelectedNode));
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			// Clear PropertyGrid Selecion
			this.PropertyGrid.SelectedObject = null;

			// Handle Event
			if (this.OnTreeViewSelectItem != null)
			{
				this.OnTreeViewSelectItem(e.Node.Tag, new EventArgs());
			}

			// Simple Select
			if (_OldNode != null && _OldNode.Parent != null && e.Node.Nodes.Count > 0)
			{
				//_OldNode.Collapse();
			}
			if (e.Node.Nodes.Count > 0)
			{
				//e.Node.Expand();
			}

			// End Simple Select
			this.PropertyGrid.SelectedObject = e.Node.Tag;

			// Save Old Node
			if (e.Node.Nodes.Count > 0)
			{
				_OldNode = e.Node;
			}
		}


		private void treeViewFIL_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (this.TreeView.SelectedNode != null && this.TreeView.SelectedNode.IsEditing) { return; }

			if (e.KeyData == Keys.Insert) // New
			{
				if (this.OnInsert != null) { this.OnInsert(null, null); }
			}
			else if (e.KeyData == Keys.Delete) // Remove
			{
				if (this.OnExclude != null) { this.OnExclude(null, null); }
			}
			else if (e.KeyData == (Keys.Shift | Keys.Delete)) // Remove
			{
				if (this.OnDelete != null) { this.OnDelete(null, null); }
			}
			else if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return) // Edit (Enter or KeyPad Enter)
			{
				if (this.OnEdit != null && !this._EditControl) { this.OnEdit(null, null); }
			}
			else if (e.KeyData == Keys.F2) // Rename
			{
				if (this.OnRename != null) { this.OnRename(null, null); }
			}

			// Clear Edit Control
			this._EditControl = false;
		}

		private void treeViewFIL_DoubleClick(object sender, System.EventArgs e)
		{
			if (this.OnEdit != null && !this._EditControl) { this.OnEdit(null, null); }
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			UpdateCurrentNodeContext();
		}

		internal void UpdateCurrentNodeContext()
		{
			if (this.CurrentSelectedNode != null
				&& this.CurrentSelectedNode.Parent != null
				&& this.CurrentSelectedNode.Parent.Tag is IList)
			{
				int itemImageIndex = this.CurrentSelectedNode.ImageIndex;
				int itemSelectedImageIndex = this.CurrentSelectedNode.SelectedImageIndex;
				if (itemImageIndex > Images.Count) itemImageIndex -= Images.Count;
				if (itemSelectedImageIndex > Images.Count) itemSelectedImageIndex -= Images.Count;
				
				this.RefreshCollectionList(this.CurrentSelectedNode.Parent, itemImageIndex, itemSelectedImageIndex);
			}
		}

	}
}
