namespace WindowsFormsControlLibrary1
{
	partial class QifBasicTransactionTranslatorCtrl
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
			this.lstCoreAccounts = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoMemoToDescription = new System.Windows.Forms.RadioButton();
			this.rdoMemoIgnore = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rdoPayeeToDescription = new System.Windows.Forms.RadioButton();
			this.rdoIgnorePayee = new System.Windows.Forms.RadioButton();
			this.qifBasicTransactionListView1 = new WindowsFormsControlLibrary1.QifBasicTransactionListView();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstCoreAccounts
			// 
			this.lstCoreAccounts.FormattingEnabled = true;
			this.lstCoreAccounts.Location = new System.Drawing.Point(502, 46);
			this.lstCoreAccounts.Name = "lstCoreAccounts";
			this.lstCoreAccounts.Size = new System.Drawing.Size(141, 82);
			this.lstCoreAccounts.Sorted = true;
			this.lstCoreAccounts.TabIndex = 1;
			this.lstCoreAccounts.SelectedIndexChanged += new System.EventHandler(this.lstCoreAccounts_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(499, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Account ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoMemoToDescription);
			this.groupBox1.Controls.Add(this.rdoMemoIgnore);
			this.groupBox1.Location = new System.Drawing.Point(502, 236);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(140, 102);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Memo";
			// 
			// rdoMemoToDescription
			// 
			this.rdoMemoToDescription.AutoSize = true;
			this.rdoMemoToDescription.Checked = true;
			this.rdoMemoToDescription.Location = new System.Drawing.Point(7, 43);
			this.rdoMemoToDescription.Name = "rdoMemoToDescription";
			this.rdoMemoToDescription.Size = new System.Drawing.Size(112, 17);
			this.rdoMemoToDescription.TabIndex = 1;
			this.rdoMemoToDescription.TabStop = true;
			this.rdoMemoToDescription.Text = "Add to Description";
			this.rdoMemoToDescription.UseVisualStyleBackColor = true;
			// 
			// rdoMemoIgnore
			// 
			this.rdoMemoIgnore.AutoSize = true;
			this.rdoMemoIgnore.Location = new System.Drawing.Point(7, 20);
			this.rdoMemoIgnore.Name = "rdoMemoIgnore";
			this.rdoMemoIgnore.Size = new System.Drawing.Size(55, 17);
			this.rdoMemoIgnore.TabIndex = 0;
			this.rdoMemoIgnore.TabStop = true;
			this.rdoMemoIgnore.Text = "Ignore";
			this.rdoMemoIgnore.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rdoPayeeToDescription);
			this.groupBox2.Controls.Add(this.rdoIgnorePayee);
			this.groupBox2.Location = new System.Drawing.Point(502, 131);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(140, 99);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Payee";
			// 
			// rdoPayeeToDescription
			// 
			this.rdoPayeeToDescription.AutoSize = true;
			this.rdoPayeeToDescription.Checked = true;
			this.rdoPayeeToDescription.Location = new System.Drawing.Point(7, 52);
			this.rdoPayeeToDescription.Name = "rdoPayeeToDescription";
			this.rdoPayeeToDescription.Size = new System.Drawing.Size(112, 17);
			this.rdoPayeeToDescription.TabIndex = 3;
			this.rdoPayeeToDescription.TabStop = true;
			this.rdoPayeeToDescription.Text = "Add to Description";
			this.rdoPayeeToDescription.UseVisualStyleBackColor = true;
			// 
			// rdoIgnorePayee
			// 
			this.rdoIgnorePayee.AutoSize = true;
			this.rdoIgnorePayee.Location = new System.Drawing.Point(7, 29);
			this.rdoIgnorePayee.Name = "rdoIgnorePayee";
			this.rdoIgnorePayee.Size = new System.Drawing.Size(55, 17);
			this.rdoIgnorePayee.TabIndex = 2;
			this.rdoIgnorePayee.TabStop = true;
			this.rdoIgnorePayee.Text = "Ignore";
			this.rdoIgnorePayee.UseVisualStyleBackColor = true;
			// 
			// qifBasicTransactionListView1
			// 
			this.qifBasicTransactionListView1.Location = new System.Drawing.Point(15, 13);
			this.qifBasicTransactionListView1.Name = "qifBasicTransactionListView1";
			this.qifBasicTransactionListView1.Size = new System.Drawing.Size(478, 325);
			this.qifBasicTransactionListView1.TabIndex = 14;
			// 
			// QifBasicTransactionTranslatorCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.qifBasicTransactionListView1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstCoreAccounts);
			this.Name = "QifBasicTransactionTranslatorCtrl";
			this.Size = new System.Drawing.Size(656, 354);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		//private QifBasicTransactionListView qifBasicTransactionListView1;
		private System.Windows.Forms.ListBox lstCoreAccounts;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoMemoToDescription;
		private System.Windows.Forms.RadioButton rdoMemoIgnore;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rdoPayeeToDescription;
		private System.Windows.Forms.RadioButton rdoIgnorePayee;
		private QifBasicTransactionListView qifBasicTransactionListView1;


	}
}
