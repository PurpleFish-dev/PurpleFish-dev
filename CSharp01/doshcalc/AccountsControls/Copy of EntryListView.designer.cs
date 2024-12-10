namespace WindowsFormsControlLibrary1
{
	partial class ObjectEntryListView
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
            this.listView = new System.Windows.Forms.ListView();
            this.clmIcons = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmImportDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCatagory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTransferAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCredit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDebit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmBalance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmIcons,
            this.clmDate,
            this.clmDescription,
            this.clmImportDescription,
            this.clmCatagory,
            this.clmProperty,
            this.clmTransferAccount,
            this.clmCredit,
            this.clmDebit,
            this.clmBalance});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(652, 239);
            this.listView.TabIndex = 7;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            this.listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_MouseUp);
            // 
            // clmIcons
            // 
            this.clmIcons.Text = "Icons";
            // 
            // clmDate
            // 
            this.clmDate.Text = "Date";
            this.clmDate.Width = 85;
            // 
            // clmDescription
            // 
            this.clmDescription.Text = "Description";
            // 
            // clmImportDescription
            // 
            this.clmImportDescription.Text = "Import Description";
            // 
            // clmCatagory
            // 
            this.clmCatagory.Text = "Catagory";
            this.clmCatagory.Width = 101;
            // 
            // clmProperty
            // 
            this.clmProperty.DisplayIndex = 6;
            this.clmProperty.Text = "Property";
            // 
            // clmTransferAccount
            // 
            this.clmTransferAccount.DisplayIndex = 5;
            this.clmTransferAccount.Text = "TransferAccount";
            this.clmTransferAccount.Width = 106;
            // 
            // clmCredit
            // 
            this.clmCredit.Text = "Credit";
            // 
            // clmDebit
            // 
            this.clmDebit.Text = "Debit";
            // 
            // clmBalance
            // 
            this.clmBalance.Text = "Balance";
            // 
            // EntryListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Name = "EntryListView";
            this.Size = new System.Drawing.Size(652, 239);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader clmDate;
		private System.Windows.Forms.ColumnHeader clmDescription;
		private System.Windows.Forms.ColumnHeader clmCatagory;
		private System.Windows.Forms.ColumnHeader clmTransferAccount;
		private System.Windows.Forms.ColumnHeader clmProperty;
		private System.Windows.Forms.ColumnHeader clmCredit;
		private System.Windows.Forms.ColumnHeader clmDebit;
		private System.Windows.Forms.ColumnHeader clmBalance;
        private System.Windows.Forms.ColumnHeader clmImportDescription;
        private System.Windows.Forms.ColumnHeader clmIcons;
	}
}
