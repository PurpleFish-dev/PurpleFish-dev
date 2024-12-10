namespace WindowsFormsApplication6
{
	partial class CatagoryFrm
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.cboTaxCode = new System.Windows.Forms.ComboBox();
			this.rdoIncome = new System.Windows.Forms.RadioButton();
			this.rdoExpense = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkAssignedToProperty = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(12, 11);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(150, 20);
			this.txtName.TabIndex = 0;
			// 
			// cboTaxCode
			// 
			this.cboTaxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTaxCode.FormattingEnabled = true;
			this.cboTaxCode.Location = new System.Drawing.Point(12, 37);
			this.cboTaxCode.Name = "cboTaxCode";
			this.cboTaxCode.Size = new System.Drawing.Size(147, 21);
			this.cboTaxCode.TabIndex = 2;
			// 
			// rdoIncome
			// 
			this.rdoIncome.AutoSize = true;
			this.rdoIncome.Location = new System.Drawing.Point(12, 94);
			this.rdoIncome.Name = "rdoIncome";
			this.rdoIncome.Size = new System.Drawing.Size(60, 17);
			this.rdoIncome.TabIndex = 3;
			this.rdoIncome.TabStop = true;
			this.rdoIncome.Text = "Income";
			this.rdoIncome.UseVisualStyleBackColor = true;
			// 
			// rdoExpense
			// 
			this.rdoExpense.AutoSize = true;
			this.rdoExpense.Location = new System.Drawing.Point(12, 117);
			this.rdoExpense.Name = "rdoExpense";
			this.rdoExpense.Size = new System.Drawing.Size(66, 17);
			this.rdoExpense.TabIndex = 4;
			this.rdoExpense.TabStop = true;
			this.rdoExpense.Text = "Expense";
			this.rdoExpense.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(264, 85);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
//			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(264, 114);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// chkAssignedToProperty
			// 
			this.chkAssignedToProperty.AutoSize = true;
			this.chkAssignedToProperty.Location = new System.Drawing.Point(13, 71);
			this.chkAssignedToProperty.Name = "chkAssignedToProperty";
			this.chkAssignedToProperty.Size = new System.Drawing.Size(162, 17);
			this.chkAssignedToProperty.TabIndex = 7;
			this.chkAssignedToProperty.Text = "Assigned to specific Property";
			this.chkAssignedToProperty.UseVisualStyleBackColor = true;
			// 
			// CatagoryFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(351, 144);
			this.Controls.Add(this.chkAssignedToProperty);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.rdoExpense);
			this.Controls.Add(this.rdoIncome);
			this.Controls.Add(this.cboTaxCode);
			this.Controls.Add(this.txtName);
			this.Name = "CatagoryFrm";
			this.Text = "Form3";
//			this.Load += new System.EventHandler(this.Catagory_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ComboBox cboTaxCode;
		private System.Windows.Forms.RadioButton rdoIncome;
		private System.Windows.Forms.RadioButton rdoExpense;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkAssignedToProperty;
	}
}