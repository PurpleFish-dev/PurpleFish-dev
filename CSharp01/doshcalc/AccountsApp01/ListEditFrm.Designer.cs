namespace WindowsFormsApplication6
{
	partial class ListEditFrm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnNew = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(6, 35);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(23, 27);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = true;
//			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// listView1
			// 
			this.listView1.CheckBoxes = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.LabelEdit = true;
			this.listView1.Location = new System.Drawing.Point(0, 68);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 198);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
//			this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
//			this.listView1.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_BeforeLabelEdit);
//			this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
//			this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
//			this.listView1.Click += new System.EventHandler(this.listView1_Click);
//			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
//			this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 400;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(35, 35);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(23, 27);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 266);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnNew);
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}