namespace WindowsFormsControlLibrary1
{
	partial class AccountListView
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
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExternal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAccountType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHidden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLockDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReconciled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colExternal,
            this.colAccountType,
            this.colHidden,
            this.colLock,
            this.colLockDate,
            this.colReconciled});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.LabelEdit = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(608, 288);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_ItemChecked);
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            this.listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_MouseUp);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 69;
            // 
            // colExternal
            // 
            this.colExternal.Text = "External";
            // 
            // colAccountType
            // 
            this.colAccountType.Text = "AccountType";
            // 
            // colHidden
            // 
            this.colHidden.Text = "Hidden";
            // 
            // colLock
            // 
            this.colLock.Text = "Lock";
            // 
            // colLockDate
            // 
            this.colLockDate.Text = "LockDate";
            // 
            // colReconciled
            // 
            this.colReconciled.Text = "Reconciled";
            // 
            // AccountListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "AccountListView";
            this.Size = new System.Drawing.Size(608, 288);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colExternal;
		private System.Windows.Forms.ColumnHeader colAccountType;
		private System.Windows.Forms.ColumnHeader colHidden;
		private System.Windows.Forms.ColumnHeader colLock;
		private System.Windows.Forms.ColumnHeader colLockDate;
		private System.Windows.Forms.ColumnHeader colReconciled;
	}
}
