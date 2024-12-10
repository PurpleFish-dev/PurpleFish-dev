
using BrightIdeasSoftware;
namespace WindowsFormsControlLibrary1
{
	partial class ReconcileListView
    {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.fastObjectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCredit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDebit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCatagory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTransfer = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvReconcileFlag = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDateB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDescriptionB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCreditB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDebitB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCatagoryB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTransferB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconcileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unmatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.olvTypeB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastObjectListView
            // 
            this.fastObjectListView.AllColumns.Add(this.olvDate);
            this.fastObjectListView.AllColumns.Add(this.olvDescription);
            this.fastObjectListView.AllColumns.Add(this.olvCredit);
            this.fastObjectListView.AllColumns.Add(this.olvDebit);
            this.fastObjectListView.AllColumns.Add(this.olvCatagory);
            this.fastObjectListView.AllColumns.Add(this.olvTransfer);
            this.fastObjectListView.AllColumns.Add(this.olvType);
            this.fastObjectListView.AllColumns.Add(this.olvReconcileFlag);
            this.fastObjectListView.AllColumns.Add(this.olvDateB);
            this.fastObjectListView.AllColumns.Add(this.olvDescriptionB);
            this.fastObjectListView.AllColumns.Add(this.olvCreditB);
            this.fastObjectListView.AllColumns.Add(this.olvDebitB);
            this.fastObjectListView.AllColumns.Add(this.olvCatagoryB);
            this.fastObjectListView.AllColumns.Add(this.olvTransferB);
            this.fastObjectListView.AllColumns.Add(this.olvTypeB);
            this.fastObjectListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fastObjectListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.fastObjectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvDate,
            this.olvDescription,
            this.olvCredit,
            this.olvDebit,
            this.olvCatagory,
            this.olvTransfer,
            this.olvType,
            this.olvReconcileFlag,
            this.olvDateB,
            this.olvDescriptionB,
            this.olvCreditB,
            this.olvDebitB,
            this.olvCatagoryB,
            this.olvTransferB,
            this.olvTypeB});
            this.fastObjectListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastObjectListView.FullRowSelect = true;
            this.fastObjectListView.GridLines = true;
            this.fastObjectListView.Location = new System.Drawing.Point(0, 0);
            this.fastObjectListView.Margin = new System.Windows.Forms.Padding(6);
            this.fastObjectListView.Name = "fastObjectListView";
            this.fastObjectListView.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastObjectListView.ShowGroups = false;
            this.fastObjectListView.Size = new System.Drawing.Size(1304, 460);
            this.fastObjectListView.TabIndex = 0;
            this.fastObjectListView.UseAlternatingBackColors = true;
            this.fastObjectListView.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView.View = System.Windows.Forms.View.Details;
            this.fastObjectListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.fastObjectListView_FormatCell);
            this.fastObjectListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastObjectListView_FormatRow);
            this.fastObjectListView.SelectionChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.fastObjectListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // olvDate
            // 
            this.olvDate.CellPadding = null;
            this.olvDate.MaximumWidth = 80;
            this.olvDate.MinimumWidth = 80;
            this.olvDate.Text = "Date";
            this.olvDate.Width = 80;
            // 
            // olvDescription
            // 
            this.olvDescription.CellPadding = null;
            this.olvDescription.Text = "Description";
            this.olvDescription.Width = 120;
            // 
            // olvCredit
            // 
            this.olvCredit.CellPadding = null;
            this.olvCredit.Text = "Credit";
            // 
            // olvDebit
            // 
            this.olvDebit.CellPadding = null;
            this.olvDebit.Text = "Debit";
            // 
            // olvCatagory
            // 
            this.olvCatagory.CellPadding = null;
            this.olvCatagory.Text = "Catagory";
            // 
            // olvTransfer
            // 
            this.olvTransfer.CellPadding = null;
            this.olvTransfer.Text = "Transfer";
            // 
            // olvType
            // 
            this.olvType.CellPadding = null;
            this.olvType.MaximumWidth = 68;
            this.olvType.MinimumWidth = 68;
            this.olvType.Text = "Entry Type";
            this.olvType.Width = 68;
            // 
            // olvReconcileFlag
            // 
            this.olvReconcileFlag.CellPadding = null;
            this.olvReconcileFlag.Text = "Reconcile Flag";
            this.olvReconcileFlag.Width = 68;
            // 
            // olvDateB
            // 
            this.olvDateB.CellPadding = null;
            this.olvDateB.Text = "Date";
            // 
            // olvDescriptionB
            // 
            this.olvDescriptionB.CellPadding = null;
            this.olvDescriptionB.Text = "Description";
            // 
            // olvCreditB
            // 
            this.olvCreditB.CellPadding = null;
            this.olvCreditB.Text = "Credit";
            // 
            // olvDebitB
            // 
            this.olvDebitB.CellPadding = null;
            this.olvDebitB.Text = "Debit";
            // 
            // olvCatagoryB
            // 
            this.olvCatagoryB.CellPadding = null;
            this.olvCatagoryB.Text = "Catagory";
            // 
            // olvTransferB
            // 
            this.olvTransferB.CellPadding = null;
            this.olvTransferB.Text = "Transfer";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.reconcileToolStripMenuItem,
            this.matchToolStripMenuItem,
            this.unmatchToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(291, 148);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(290, 36);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // modifyCatagoriesToolStripMenuItem
            // 
            this.reconcileToolStripMenuItem.Name = "ReconcileToolStripMenuItem";
            this.reconcileToolStripMenuItem.Size = new System.Drawing.Size(290, 36);
            this.reconcileToolStripMenuItem.Text = "Reconcile";
            this.reconcileToolStripMenuItem.Click += new System.EventHandler(this.ReconcileToolStripMenuItem_Click);
            // 
            // matchToolStripMenuItem
            // 
            this.matchToolStripMenuItem.Name = "matchToolStripMenuItem";
            this.matchToolStripMenuItem.Size = new System.Drawing.Size(290, 36);
            this.matchToolStripMenuItem.Text = "Match";
            this.matchToolStripMenuItem.Click += new System.EventHandler(this.matchToolStripMenuItem_Click);
            // 
            // unmatchToolStripMenuItem
            // 
            this.unmatchToolStripMenuItem.Name = "repeatTransactionToolStripMenuItem";
            this.unmatchToolStripMenuItem.Size = new System.Drawing.Size(290, 36);
            this.unmatchToolStripMenuItem.Text = "Unmatch";
            this.unmatchToolStripMenuItem.Click += new System.EventHandler(this.unmatchToolStripMenuItem_Click);
            // 
            // olvTypeB
            // 
            this.olvTypeB.CellPadding = null;
            this.olvTypeB.Text = "Type";
            // 
            // ReconcileListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fastObjectListView);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ReconcileListView";
            this.Size = new System.Drawing.Size(1304, 460);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private BrightIdeasSoftware.ObjectListView fastObjectListView;
        private BrightIdeasSoftware.OLVColumn olvDate;
        private OLVColumn olvDescription;
        private OLVColumn olvCredit;
        private OLVColumn olvDebit;
        private OLVColumn olvCatagory;
        private OLVColumn olvTransfer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconcileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unmatchToolStripMenuItem;
        private OLVColumn olvType;
        private OLVColumn olvReconcileFlag;
        private OLVColumn olvDateB;
        private OLVColumn olvDescriptionB;
        private OLVColumn olvCreditB;
        private OLVColumn olvDebitB;
        private OLVColumn olvCatagoryB;
        private OLVColumn olvTransferB;
        private OLVColumn olvTypeB;
    }
}
