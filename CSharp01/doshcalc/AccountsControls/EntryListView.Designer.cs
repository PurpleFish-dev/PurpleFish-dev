
using BrightIdeasSoftware;
namespace WindowsFormsControlLibrary1
{
	partial class EntryListView
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
            this.olvBalance = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCatagory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvProperty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPayee = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvFileAssoc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyCatagoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.fastObjectListView.AllColumns.Add(this.olvBalance);
            this.fastObjectListView.AllColumns.Add(this.olvCatagory);
            this.fastObjectListView.AllColumns.Add(this.olvProperty);
            this.fastObjectListView.AllColumns.Add(this.olvPayee);
            this.fastObjectListView.AllColumns.Add(this.olvType);
            this.fastObjectListView.AllColumns.Add(this.olvFileAssoc);
            this.fastObjectListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fastObjectListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.fastObjectListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.fastObjectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvDate,
            this.olvDescription,
            this.olvCredit,
            this.olvDebit,
            this.olvBalance,
            this.olvCatagory,
            this.olvProperty,
            this.olvPayee,
            this.olvType,
            this.olvFileAssoc});
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
            this.fastObjectListView.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.HandleCellEditFinishing);
            this.fastObjectListView.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.HandleCellEditStarting);
            this.fastObjectListView.CellEditValidating += new BrightIdeasSoftware.CellEditEventHandler(this.HandleCellEditValidating);
            this.fastObjectListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.fastObjectListView_FormatCell);
            this.fastObjectListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastObjectListView_FormatRow);
            this.fastObjectListView.SelectionChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.fastObjectListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.fastObjectListView_ColumnClick);
            this.fastObjectListView.ItemActivate += new System.EventHandler(this.fastObjectListView_ItemActivate);
            this.fastObjectListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // olvDate
            // 
            this.olvDate.CellPadding = null;
            this.olvDate.MaximumWidth = 80;
            this.olvDate.MinimumWidth = 80;
            this.olvDate.Sortable = false;
            this.olvDate.Text = "Date";
            this.olvDate.Width = 80;
            // 
            // olvDescription
            // 
            this.olvDescription.CellPadding = null;
            this.olvDescription.Sortable = false;
            this.olvDescription.Text = "Description";
            this.olvDescription.Width = 120;
            // 
            // olvCredit
            // 
            this.olvCredit.CellPadding = null;
            this.olvCredit.Sortable = false;
            this.olvCredit.Text = "Credit";
            // 
            // olvDebit
            // 
            this.olvDebit.CellPadding = null;
            this.olvDebit.Sortable = false;
            this.olvDebit.Text = "Debit";
            // 
            // olvBalance
            // 
            this.olvBalance.CellPadding = null;
            this.olvBalance.Sortable = false;
            this.olvBalance.Text = "Balance";
            // 
            // olvCatagory
            // 
            this.olvCatagory.CellPadding = null;
            this.olvCatagory.Sortable = false;
            this.olvCatagory.Text = "Catagory";
            // 
            // olvProperty
            // 
            this.olvProperty.CellPadding = null;
            this.olvProperty.Sortable = false;
            this.olvProperty.Text = "Property";
            // 
            // olvPayee
            // 
            this.olvPayee.CellPadding = null;
            this.olvPayee.Sortable = false;
            this.olvPayee.Text = "Payee";
            // 
            // olvType
            // 
            this.olvType.CellPadding = null;
            this.olvType.MaximumWidth = 20;
            this.olvType.MinimumWidth = 20;
            this.olvType.Sortable = false;
            this.olvType.Width = 20;
            // 
            // olvFileAssoc
            // 
            this.olvFileAssoc.CellPadding = null;
            this.olvFileAssoc.MaximumWidth = 20;
            this.olvFileAssoc.MinimumWidth = 20;
            this.olvFileAssoc.Sortable = false;
            this.olvFileAssoc.Text = "File Assoc.";
            this.olvFileAssoc.Width = 20;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.modifyCatagoriesToolStripMenuItem,
            this.timeChartToolStripMenuItem,
            this.repeatTransactionToolStripMenuItem,
            this.moveTransactionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(301, 228);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(300, 36);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // modifyCatagoriesToolStripMenuItem
            // 
            this.modifyCatagoriesToolStripMenuItem.Name = "modifyCatagoriesToolStripMenuItem";
            this.modifyCatagoriesToolStripMenuItem.Size = new System.Drawing.Size(300, 36);
            this.modifyCatagoriesToolStripMenuItem.Text = "Modify Catagories";
            this.modifyCatagoriesToolStripMenuItem.Click += new System.EventHandler(this.ModifyCatagoryToolStripMenuItem_Click);
            // 
            // timeChartToolStripMenuItem
            // 
            this.timeChartToolStripMenuItem.Name = "timeChartToolStripMenuItem";
            this.timeChartToolStripMenuItem.Size = new System.Drawing.Size(300, 36);
            this.timeChartToolStripMenuItem.Text = "Time Chart";
            this.timeChartToolStripMenuItem.Click += new System.EventHandler(this.timeChartToolStripMenuItem_Click);
            // 
            // repeatTransactionToolStripMenuItem
            // 
            this.repeatTransactionToolStripMenuItem.Name = "repeatTransactionToolStripMenuItem";
            this.repeatTransactionToolStripMenuItem.Size = new System.Drawing.Size(300, 36);
            this.repeatTransactionToolStripMenuItem.Text = "Repeat Transaction";
            this.repeatTransactionToolStripMenuItem.Click += new System.EventHandler(this.repeatTransactionToolStripMenuItem_Click);
            // 
            // moveTransactionToolStripMenuItem
            // 
            this.moveTransactionToolStripMenuItem.Name = "moveTransactionToolStripMenuItem";
            this.moveTransactionToolStripMenuItem.Size = new System.Drawing.Size(300, 36);
            this.moveTransactionToolStripMenuItem.Text = "Move Transaction";
            this.moveTransactionToolStripMenuItem.Click += new System.EventHandler(this.moveTransactionToolStripMenuItem_Click);
            // 
            // EntryListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fastObjectListView);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "EntryListView";
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
        private OLVColumn olvBalance;
        private OLVColumn olvCatagory;
        private OLVColumn olvPayee;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyCatagoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeatTransactionToolStripMenuItem;
        private OLVColumn olvType;
        private OLVColumn olvProperty;
        private OLVColumn olvFileAssoc;
        private System.Windows.Forms.ToolStripMenuItem moveTransactionToolStripMenuItem;
    }
}
