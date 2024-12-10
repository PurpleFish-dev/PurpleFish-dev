namespace WindowsFormsControlLibrary1
{
	partial class EntryEditCtrl
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReceiptNo = new System.Windows.Forms.TextBox();
            this.rdoDebit = new System.Windows.Forms.RadioButton();
            this.rdoCredit = new System.Windows.Forms.RadioButton();
            this.cboCatagory = new System.Windows.Forms.ComboBox();
            this.cboProperty = new System.Windows.Forms.ComboBox();
            this.cboPayee = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblReason = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlDate = new System.Windows.Forms.DateTimePicker();
            this.btnOpenFileAssoc = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chkFilterAmount = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkFilterLink = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoNoLink = new System.Windows.Forms.RadioButton();
            this.rdoLinked = new System.Windows.Forms.RadioButton();
            this.txtMinAmountFilter = new GenericControls.DecimalTextBox();
            this.decimalTextBox1 = new GenericControls.DecimalTextBox();
            this.txtAmount = new GenericControls.DecimalTextBox();
            this.cboFilterPayee = new System.Windows.Forms.ComboBox();
            this.chkFilterPayee = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.decimalTextBox2 = new GenericControls.DecimalTextBox();
            this.decimalTextBox3 = new GenericControls.DecimalTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 596);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 25);
            this.label5.TabIndex = 26;
            this.label5.Text = "Payee";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 715);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 25);
            this.label4.TabIndex = 24;
            this.label4.Text = "Catagory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 765);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "Property";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 454);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 25);
            this.label6.TabIndex = 18;
            this.label6.Text = "RecieptNo.";
            // 
            // txtReceiptNo
            // 
            this.txtReceiptNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiptNo.Location = new System.Drawing.Point(-2, 485);
            this.txtReceiptNo.Margin = new System.Windows.Forms.Padding(6);
            this.txtReceiptNo.Name = "txtReceiptNo";
            this.txtReceiptNo.Size = new System.Drawing.Size(294, 31);
            this.txtReceiptNo.TabIndex = 2;
            this.txtReceiptNo.TabStop = false;
            this.txtReceiptNo.TextChanged += new System.EventHandler(this.txtReceiptNo_TextChanged);
            // 
            // rdoDebit
            // 
            this.rdoDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoDebit.AutoSize = true;
            this.rdoDebit.Location = new System.Drawing.Point(285, 810);
            this.rdoDebit.Margin = new System.Windows.Forms.Padding(6);
            this.rdoDebit.Name = "rdoDebit";
            this.rdoDebit.Size = new System.Drawing.Size(93, 29);
            this.rdoDebit.TabIndex = 7;
            this.rdoDebit.TabStop = true;
            this.rdoDebit.Text = "Debit";
            this.rdoDebit.UseVisualStyleBackColor = true;
            this.rdoDebit.CheckedChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // rdoCredit
            // 
            this.rdoCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoCredit.AutoSize = true;
            this.rdoCredit.Location = new System.Drawing.Point(282, 854);
            this.rdoCredit.Margin = new System.Windows.Forms.Padding(6);
            this.rdoCredit.Name = "rdoCredit";
            this.rdoCredit.Size = new System.Drawing.Size(100, 29);
            this.rdoCredit.TabIndex = 8;
            this.rdoCredit.TabStop = true;
            this.rdoCredit.Text = "Credit";
            this.rdoCredit.UseVisualStyleBackColor = true;
            this.rdoCredit.CheckedChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // cboCatagory
            // 
            this.cboCatagory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCatagory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCatagory.FormattingEnabled = true;
            this.cboCatagory.Location = new System.Drawing.Point(140, 708);
            this.cboCatagory.Margin = new System.Windows.Forms.Padding(6);
            this.cboCatagory.Name = "cboCatagory";
            this.cboCatagory.Size = new System.Drawing.Size(244, 33);
            this.cboCatagory.TabIndex = 4;
            this.cboCatagory.SelectedIndexChanged += new System.EventHandler(this.cboCatagory_SelectedIndexChanged);
            this.cboCatagory.SelectionChangeCommitted += new System.EventHandler(this.cboCatagory_SelectionChangeCommitted);
            // 
            // cboProperty
            // 
            this.cboProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboProperty.FormattingEnabled = true;
            this.cboProperty.Location = new System.Drawing.Point(140, 760);
            this.cboProperty.Margin = new System.Windows.Forms.Padding(6);
            this.cboProperty.Name = "cboProperty";
            this.cboProperty.Size = new System.Drawing.Size(244, 33);
            this.cboProperty.TabIndex = 5;
            this.cboProperty.SelectionChangeCommitted += new System.EventHandler(this.cboProperty_SelectionChangeCommitted);
            // 
            // cboPayee
            // 
            this.cboPayee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayee.FormattingEnabled = true;
            this.cboPayee.Location = new System.Drawing.Point(138, 585);
            this.cboPayee.Margin = new System.Windows.Forms.Padding(6);
            this.cboPayee.Name = "cboPayee";
            this.cboPayee.Size = new System.Drawing.Size(238, 33);
            this.cboPayee.TabIndex = 2;
            this.cboPayee.SelectionChangeCommitted += new System.EventHandler(this.cboPayee_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 634);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 30;
            this.label1.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(12, 665);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(6);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(372, 31);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 898);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 38);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(36, 898);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(6);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(124, 38);
            this.btnEnter.TabIndex = 9;
            this.btnEnter.Text = "666";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(278, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 62);
            this.btnDelete.TabIndex = 47;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(174, 6);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 62);
            this.btnAdd.TabIndex = 46;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(6, 953);
            this.lblReason.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(93, 25);
            this.lblReason.TabIndex = 51;
            this.lblReason.Text = "Property";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(202, 963);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 38);
            this.button1.TabIndex = 52;
            this.button1.Text = "666";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlDate
            // 
            this.ctrlDate.Location = new System.Drawing.Point(60, 417);
            this.ctrlDate.Margin = new System.Windows.Forms.Padding(6);
            this.ctrlDate.Name = "ctrlDate";
            this.ctrlDate.Size = new System.Drawing.Size(232, 31);
            this.ctrlDate.TabIndex = 1;
            // 
            // btnOpenFileAssoc
            // 
            this.btnOpenFileAssoc.Location = new System.Drawing.Point(306, 485);
            this.btnOpenFileAssoc.Name = "btnOpenFileAssoc";
            this.btnOpenFileAssoc.Size = new System.Drawing.Size(76, 31);
            this.btnOpenFileAssoc.TabIndex = 54;
            this.btnOpenFileAssoc.TabStop = false;
            this.btnOpenFileAssoc.Text = "Open";
            this.btnOpenFileAssoc.UseVisualStyleBackColor = true;
            this.btnOpenFileAssoc.Click += new System.EventHandler(this.btnOpenFileAssoc_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // chkFilterAmount
            // 
            this.chkFilterAmount.AutoSize = true;
            this.chkFilterAmount.Location = new System.Drawing.Point(20, 91);
            this.chkFilterAmount.Name = "chkFilterAmount";
            this.chkFilterAmount.Size = new System.Drawing.Size(117, 29);
            this.chkFilterAmount.TabIndex = 57;
            this.chkFilterAmount.Text = "Amount";
            this.chkFilterAmount.UseVisualStyleBackColor = true;
            this.chkFilterAmount.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 25);
            this.label2.TabIndex = 58;
            this.label2.Text = "Min";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 25);
            this.label7.TabIndex = 59;
            this.label7.Text = "Max";
            // 
            // chkFilterLink
            // 
            this.chkFilterLink.AutoSize = true;
            this.chkFilterLink.Location = new System.Drawing.Point(9, 0);
            this.chkFilterLink.Name = "chkFilterLink";
            this.chkFilterLink.Size = new System.Drawing.Size(84, 29);
            this.chkFilterLink.TabIndex = 60;
            this.chkFilterLink.TabStop = false;
            this.chkFilterLink.Text = "Link";
            this.chkFilterLink.UseVisualStyleBackColor = true;
            this.chkFilterLink.CheckedChanged += new System.EventHandler(this.chkFilterLink_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoNoLink);
            this.groupBox1.Controls.Add(this.chkFilterLink);
            this.groupBox1.Controls.Add(this.rdoLinked);
            this.groupBox1.Location = new System.Drawing.Point(12, 231);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 110);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            // 
            // rdoNoLink
            // 
            this.rdoNoLink.AutoSize = true;
            this.rdoNoLink.Location = new System.Drawing.Point(8, 35);
            this.rdoNoLink.Name = "rdoNoLink";
            this.rdoNoLink.Size = new System.Drawing.Size(116, 29);
            this.rdoNoLink.TabIndex = 61;
            this.rdoNoLink.Text = "No Link";
            this.rdoNoLink.UseVisualStyleBackColor = true;
            this.rdoNoLink.CheckedChanged += new System.EventHandler(this.rdoNoLink_CheckedChanged);
            // 
            // rdoLinked
            // 
            this.rdoLinked.AutoSize = true;
            this.rdoLinked.Location = new System.Drawing.Point(8, 75);
            this.rdoLinked.Name = "rdoLinked";
            this.rdoLinked.Size = new System.Drawing.Size(107, 29);
            this.rdoLinked.TabIndex = 62;
            this.rdoLinked.Text = "Linked";
            this.rdoLinked.UseVisualStyleBackColor = true;
            this.rdoLinked.CheckedChanged += new System.EventHandler(this.rdoLinked_CheckedChanged);
            // 
            // txtMinAmountFilter
            // 
            this.txtMinAmountFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinAmountFilter.Location = new System.Drawing.Point(81, 126);
            this.txtMinAmountFilter.Margin = new System.Windows.Forms.Padding(6);
            this.txtMinAmountFilter.Name = "txtMinAmountFilter";
            this.txtMinAmountFilter.Size = new System.Drawing.Size(144, 31);
            this.txtMinAmountFilter.TabIndex = 56;
            this.txtMinAmountFilter.TextChanged += new System.EventHandler(this.txtMinAmountFilter_TextChanged);
            // 
            // decimalTextBox1
            // 
            this.decimalTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decimalTextBox1.Location = new System.Drawing.Point(81, 172);
            this.decimalTextBox1.Margin = new System.Windows.Forms.Padding(6);
            this.decimalTextBox1.Name = "decimalTextBox1";
            this.decimalTextBox1.Size = new System.Drawing.Size(144, 31);
            this.decimalTextBox1.TabIndex = 55;
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAmount.Location = new System.Drawing.Point(12, 831);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(6);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(144, 31);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // cboFilterPayee
            // 
            this.cboFilterPayee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterPayee.FormattingEnabled = true;
            this.cboFilterPayee.Location = new System.Drawing.Point(140, 350);
            this.cboFilterPayee.Margin = new System.Windows.Forms.Padding(6);
            this.cboFilterPayee.Name = "cboFilterPayee";
            this.cboFilterPayee.Size = new System.Drawing.Size(238, 33);
            this.cboFilterPayee.TabIndex = 64;
            this.cboFilterPayee.TabStop = false;
            this.cboFilterPayee.SelectionChangeCommitted += new System.EventHandler(this.cboFilterPayee_SelectionChangeCommitted);
            // 
            // chkFilterPayee
            // 
            this.chkFilterPayee.AutoSize = true;
            this.chkFilterPayee.Location = new System.Drawing.Point(14, 352);
            this.chkFilterPayee.Name = "chkFilterPayee";
            this.chkFilterPayee.Size = new System.Drawing.Size(105, 29);
            this.chkFilterPayee.TabIndex = 65;
            this.chkFilterPayee.TabStop = false;
            this.chkFilterPayee.Text = "Payee";
            this.chkFilterPayee.UseVisualStyleBackColor = true;
            this.chkFilterPayee.CheckedChanged += new System.EventHandler(this.chkFilterPayee_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(174, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 62);
            this.button2.TabIndex = 46;
            this.button2.TabStop = false;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(278, 6);
            this.button3.Margin = new System.Windows.Forms.Padding(6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 62);
            this.button3.TabIndex = 47;
            this.button3.TabStop = false;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // decimalTextBox2
            // 
            this.decimalTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decimalTextBox2.Location = new System.Drawing.Point(81, 172);
            this.decimalTextBox2.Margin = new System.Windows.Forms.Padding(6);
            this.decimalTextBox2.Name = "decimalTextBox2";
            this.decimalTextBox2.Size = new System.Drawing.Size(144, 31);
            this.decimalTextBox2.TabIndex = 55;
            this.decimalTextBox2.TabStop = false;
            // 
            // decimalTextBox3
            // 
            this.decimalTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decimalTextBox3.Location = new System.Drawing.Point(81, 126);
            this.decimalTextBox3.Margin = new System.Windows.Forms.Padding(6);
            this.decimalTextBox3.Name = "decimalTextBox3";
            this.decimalTextBox3.Size = new System.Drawing.Size(144, 31);
            this.decimalTextBox3.TabIndex = 56;
            this.decimalTextBox3.TabStop = false;
            this.decimalTextBox3.TextChanged += new System.EventHandler(this.txtMinAmountFilter_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 91);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 29);
            this.checkBox1.TabIndex = 57;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Amount";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // EntryEditCtrl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkFilterPayee);
            this.Controls.Add(this.cboFilterPayee);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chkFilterAmount);
            this.Controls.Add(this.decimalTextBox3);
            this.Controls.Add(this.txtMinAmountFilter);
            this.Controls.Add(this.decimalTextBox2);
            this.Controls.Add(this.decimalTextBox1);
            this.Controls.Add(this.btnOpenFileAssoc);
            this.Controls.Add(this.ctrlDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPayee);
            this.Controls.Add(this.cboProperty);
            this.Controls.Add(this.cboCatagory);
            this.Controls.Add(this.rdoCredit);
            this.Controls.Add(this.rdoDebit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReceiptNo);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "EntryEditCtrl";
            this.Size = new System.Drawing.Size(396, 1048);
            this.Load += new System.EventHandler(this.EntryEditCtrl_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.EntryEditCtrl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.EntryEditCtrl_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.EntryEditCtrl_DragOver);
            this.DragLeave += new System.EventHandler(this.EntryEditCtrl_DragLeave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtReceiptNo;
		private System.Windows.Forms.RadioButton rdoDebit;
		private System.Windows.Forms.RadioButton rdoCredit;
		private System.Windows.Forms.ComboBox cboCatagory;
		private System.Windows.Forms.ComboBox cboProperty;
		private System.Windows.Forms.ComboBox cboPayee;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnEnter;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
        private GenericControls.DecimalTextBox txtAmount;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker ctrlDate;
        private System.Windows.Forms.Button btnOpenFileAssoc;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private GenericControls.DecimalTextBox decimalTextBox1;
        private GenericControls.DecimalTextBox txtMinAmountFilter;
        private System.Windows.Forms.CheckBox chkFilterAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkFilterLink;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoNoLink;
        private System.Windows.Forms.RadioButton rdoLinked;
        private System.Windows.Forms.ComboBox cboFilterPayee;
        private System.Windows.Forms.CheckBox chkFilterPayee;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private GenericControls.DecimalTextBox decimalTextBox2;
        private GenericControls.DecimalTextBox decimalTextBox3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
