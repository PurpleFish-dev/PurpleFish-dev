namespace WindowsFormsControlLibrary1
{
	partial class CatagoryEditCtrl
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
			this.chkAssignedToProperty = new System.Windows.Forms.CheckBox();
			this.rdoExpense = new System.Windows.Forms.RadioButton();
			this.rdoIncome = new System.Windows.Forms.RadioButton();
			this.cboTaxCode = new System.Windows.Forms.ComboBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnEnter = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lblName = new System.Windows.Forms.Label();
			this.chkObsolete = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// chkAssignedToProperty
			// 
			this.chkAssignedToProperty.AutoSize = true;
			this.chkAssignedToProperty.Location = new System.Drawing.Point(3, 145);
			this.chkAssignedToProperty.Name = "chkAssignedToProperty";
			this.chkAssignedToProperty.Size = new System.Drawing.Size(162, 17);
			this.chkAssignedToProperty.TabIndex = 12;
			this.chkAssignedToProperty.Text = "Assigned to specific Property";
			this.chkAssignedToProperty.UseVisualStyleBackColor = true;
			this.chkAssignedToProperty.CheckedChanged += new System.EventHandler(this.chkAssignedToProperty_CheckedChanged);
			// 
			// rdoExpense
			// 
			this.rdoExpense.AutoSize = true;
			this.rdoExpense.Location = new System.Drawing.Point(3, 191);
			this.rdoExpense.Name = "rdoExpense";
			this.rdoExpense.Size = new System.Drawing.Size(66, 17);
			this.rdoExpense.TabIndex = 11;
			this.rdoExpense.TabStop = true;
			this.rdoExpense.Text = "Expense";
			this.rdoExpense.UseVisualStyleBackColor = true;
			// 
			// rdoIncome
			// 
			this.rdoIncome.AutoSize = true;
			this.rdoIncome.Location = new System.Drawing.Point(3, 168);
			this.rdoIncome.Name = "rdoIncome";
			this.rdoIncome.Size = new System.Drawing.Size(60, 17);
			this.rdoIncome.TabIndex = 10;
			this.rdoIncome.TabStop = true;
			this.rdoIncome.Text = "Income";
			this.rdoIncome.UseVisualStyleBackColor = true;
			this.rdoIncome.CheckedChanged += new System.EventHandler(this.rdoIncome_CheckedChanged);
			// 
			// cboTaxCode
			// 
			this.cboTaxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTaxCode.FormattingEnabled = true;
			this.cboTaxCode.Location = new System.Drawing.Point(3, 95);
			this.cboTaxCode.Name = "cboTaxCode";
			this.cboTaxCode.Size = new System.Drawing.Size(147, 21);
			this.cboTaxCode.TabIndex = 9;
			this.cboTaxCode.SelectionChangeCommitted += new System.EventHandler(this.cboTaxCode_SelectionChangeCommitted);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(45, 69);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(108, 20);
			this.txtName.TabIndex = 8;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(91, 261);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(62, 20);
			this.btnCancel.TabIndex = 45;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnEnter
			// 
			this.btnEnter.Location = new System.Drawing.Point(7, 261);
			this.btnEnter.Name = "btnEnter";
			this.btnEnter.Size = new System.Drawing.Size(62, 20);
			this.btnEnter.TabIndex = 44;
			this.btnEnter.Text = "666";
			this.btnEnter.UseVisualStyleBackColor = true;
			this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(91, 12);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(62, 32);
			this.btnDelete.TabIndex = 43;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(41, 12);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(44, 32);
			this.btnAdd.TabIndex = 42;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(4, 72);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 41;
			this.lblName.Text = "Name";
			// 
			// chkObsolete
			// 
			this.chkObsolete.AutoSize = true;
			this.chkObsolete.Location = new System.Drawing.Point(3, 122);
			this.chkObsolete.Name = "chkObsolete";
			this.chkObsolete.Size = new System.Drawing.Size(68, 17);
			this.chkObsolete.TabIndex = 46;
			this.chkObsolete.Text = "Obsolete";
			this.chkObsolete.UseVisualStyleBackColor = true;
			this.chkObsolete.CheckedChanged += new System.EventHandler(this.chkObsolete_CheckedChanged);
			// 
			// CatagoryEditCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkObsolete);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnEnter);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.chkAssignedToProperty);
			this.Controls.Add(this.rdoExpense);
			this.Controls.Add(this.rdoIncome);
			this.Controls.Add(this.cboTaxCode);
			this.Controls.Add(this.txtName);
			this.Name = "CatagoryEditCtrl";
			this.Size = new System.Drawing.Size(159, 306);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkAssignedToProperty;
		private System.Windows.Forms.RadioButton rdoExpense;
		private System.Windows.Forms.RadioButton rdoIncome;
		private System.Windows.Forms.ComboBox cboTaxCode;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnEnter;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.CheckBox chkObsolete;
	}
}
