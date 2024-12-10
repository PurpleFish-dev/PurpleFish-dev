namespace WindowsFormsControlLibrary1
{
    partial class ScheduledTransactionsDlg
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
            if (disposing && (components != null))
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
            this.ctrlDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPayee = new System.Windows.Forms.ComboBox();
            this.cboProperty = new System.Windows.Forms.ComboBox();
            this.cboToAccount = new System.Windows.Forms.ComboBox();
            this.rdoCredit = new System.Windows.Forms.RadioButton();
            this.rdoDebit = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFromAccount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoMonth = new System.Windows.Forms.RadioButton();
            this.rdoQuarter = new System.Windows.Forms.RadioButton();
            this.rdoNoOfTransactions = new System.Windows.Forms.RadioButton();
            this.rdoEndDate = new System.Windows.Forms.RadioButton();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoWeek = new System.Windows.Forms.RadioButton();
            this.dtbNoOfTransactions = new GenericControls.DecimalTextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.grpTransfer = new System.Windows.Forms.GroupBox();
            this.pnlTransfer = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dtbTransferAmount = new GenericControls.DecimalTextBox();
            this.rdoTransfer = new System.Windows.Forms.RadioButton();
            this.grpTransaction = new System.Windows.Forms.GroupBox();
            this.pnlTransaction = new System.Windows.Forms.Panel();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboCatagory = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtbTransactionAmount = new GenericControls.DecimalTextBox();
            this.rdoTransaction = new System.Windows.Forms.RadioButton();
            this.lblReasons = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.grpTransfer.SuspendLayout();
            this.pnlTransfer.SuspendLayout();
            this.grpTransaction.SuspendLayout();
            this.pnlTransaction.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlDate
            // 
            this.ctrlDate.Location = new System.Drawing.Point(267, -105);
            this.ctrlDate.Margin = new System.Windows.Forms.Padding(6);
            this.ctrlDate.Name = "ctrlDate";
            this.ctrlDate.Size = new System.Drawing.Size(232, 31);
            this.ctrlDate.TabIndex = 69;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1336, 971);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 38);
            this.btnCancel.TabIndex = 68;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.Location = new System.Drawing.Point(1170, 971);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(6);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(124, 38);
            this.btnEnter.TabIndex = 67;
            this.btnEnter.Text = "OK";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(13, 52);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(6);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(1447, 31);
            this.txtDescription.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 65;
            this.label1.Text = "Description";
            // 
            // cboPayee
            // 
            this.cboPayee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPayee.FormattingEnabled = true;
            this.cboPayee.Location = new System.Drawing.Point(130, 175);
            this.cboPayee.Margin = new System.Windows.Forms.Padding(6);
            this.cboPayee.Name = "cboPayee";
            this.cboPayee.Size = new System.Drawing.Size(744, 33);
            this.cboPayee.TabIndex = 64;
            // 
            // cboProperty
            // 
            this.cboProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboProperty.FormattingEnabled = true;
            this.cboProperty.Location = new System.Drawing.Point(1034, 172);
            this.cboProperty.Margin = new System.Windows.Forms.Padding(6);
            this.cboProperty.Name = "cboProperty";
            this.cboProperty.Size = new System.Drawing.Size(374, 33);
            this.cboProperty.TabIndex = 63;
            // 
            // cboToAccount
            // 
            this.cboToAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboToAccount.FormattingEnabled = true;
            this.cboToAccount.Location = new System.Drawing.Point(890, 39);
            this.cboToAccount.Margin = new System.Windows.Forms.Padding(6);
            this.cboToAccount.Name = "cboToAccount";
            this.cboToAccount.Size = new System.Drawing.Size(420, 33);
            this.cboToAccount.TabIndex = 62;
            this.cboToAccount.SelectedIndexChanged += new System.EventHandler(this.cboToAccount_SelectedIndexChanged);
            // 
            // rdoCredit
            // 
            this.rdoCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoCredit.AutoSize = true;
            this.rdoCredit.Checked = true;
            this.rdoCredit.Location = new System.Drawing.Point(113, 18);
            this.rdoCredit.Margin = new System.Windows.Forms.Padding(6);
            this.rdoCredit.Name = "rdoCredit";
            this.rdoCredit.Size = new System.Drawing.Size(100, 29);
            this.rdoCredit.TabIndex = 56;
            this.rdoCredit.TabStop = true;
            this.rdoCredit.Text = "Credit";
            this.rdoCredit.UseVisualStyleBackColor = true;
            // 
            // rdoDebit
            // 
            this.rdoDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoDebit.AutoSize = true;
            this.rdoDebit.Location = new System.Drawing.Point(8, 18);
            this.rdoDebit.Margin = new System.Windows.Forms.Padding(6);
            this.rdoDebit.Name = "rdoDebit";
            this.rdoDebit.Size = new System.Drawing.Size(93, 29);
            this.rdoDebit.TabIndex = 55;
            this.rdoDebit.Text = "Debit";
            this.rdoDebit.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 175);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 25);
            this.label5.TabIndex = 61;
            this.label5.Text = "Payee";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 25);
            this.label4.TabIndex = 60;
            this.label4.Text = "Amount";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(929, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 59;
            this.label3.Text = "Property";
            // 
            // cboFromAccount
            // 
            this.cboFromAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFromAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromAccount.FormattingEnabled = true;
            this.cboFromAccount.Location = new System.Drawing.Point(415, 39);
            this.cboFromAccount.Margin = new System.Windows.Forms.Padding(6);
            this.cboFromAccount.Name = "cboFromAccount";
            this.cboFromAccount.Size = new System.Drawing.Size(339, 33);
            this.cboFromAccount.TabIndex = 71;
            this.cboFromAccount.SelectionChangeCommitted += new System.EventHandler(this.cboFromAccount_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 73;
            this.label2.Text = "from account";
            // 
            // rdoMonth
            // 
            this.rdoMonth.AutoSize = true;
            this.rdoMonth.Checked = true;
            this.rdoMonth.Location = new System.Drawing.Point(172, 39);
            this.rdoMonth.Name = "rdoMonth";
            this.rdoMonth.Size = new System.Drawing.Size(102, 29);
            this.rdoMonth.TabIndex = 77;
            this.rdoMonth.TabStop = true;
            this.rdoMonth.Text = "month";
            this.rdoMonth.UseVisualStyleBackColor = true;
            // 
            // rdoQuarter
            // 
            this.rdoQuarter.AutoSize = true;
            this.rdoQuarter.Location = new System.Drawing.Point(280, 39);
            this.rdoQuarter.Name = "rdoQuarter";
            this.rdoQuarter.Size = new System.Drawing.Size(111, 29);
            this.rdoQuarter.TabIndex = 78;
            this.rdoQuarter.Text = "quarter";
            this.rdoQuarter.UseVisualStyleBackColor = true;
            // 
            // rdoNoOfTransactions
            // 
            this.rdoNoOfTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoNoOfTransactions.AutoSize = true;
            this.rdoNoOfTransactions.Checked = true;
            this.rdoNoOfTransactions.Location = new System.Drawing.Point(989, 395);
            this.rdoNoOfTransactions.Name = "rdoNoOfTransactions";
            this.rdoNoOfTransactions.Size = new System.Drawing.Size(223, 29);
            this.rdoNoOfTransactions.TabIndex = 83;
            this.rdoNoOfTransactions.TabStop = true;
            this.rdoNoOfTransactions.Text = "No. of transactions";
            this.rdoNoOfTransactions.UseVisualStyleBackColor = true;
            this.rdoNoOfTransactions.CheckedChanged += new System.EventHandler(this.rdoNoOfTransactions_CheckedChanged);
            // 
            // rdoEndDate
            // 
            this.rdoEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoEndDate.AutoSize = true;
            this.rdoEndDate.Location = new System.Drawing.Point(989, 355);
            this.rdoEndDate.Name = "rdoEndDate";
            this.rdoEndDate.Size = new System.Drawing.Size(132, 29);
            this.rdoEndDate.TabIndex = 82;
            this.rdoEndDate.Text = "End Date";
            this.rdoEndDate.UseVisualStyleBackColor = true;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpEndDate.Enabled = false;
            this.dtpEndDate.Location = new System.Drawing.Point(1221, 352);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(6);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(232, 31);
            this.dtpEndDate.TabIndex = 84;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.rdoQuarter);
            this.groupBox2.Controls.Add(this.rdoWeek);
            this.groupBox2.Controls.Add(this.rdoMonth);
            this.groupBox2.Location = new System.Drawing.Point(499, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 100);
            this.groupBox2.TabIndex = 86;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequency";
            // 
            // rdoWeek
            // 
            this.rdoWeek.AutoSize = true;
            this.rdoWeek.Location = new System.Drawing.Point(45, 39);
            this.rdoWeek.Name = "rdoWeek";
            this.rdoWeek.Size = new System.Drawing.Size(93, 29);
            this.rdoWeek.TabIndex = 75;
            this.rdoWeek.Text = "week";
            this.rdoWeek.UseVisualStyleBackColor = true;
            // 
            // dtbNoOfTransactions
            // 
            this.dtbNoOfTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtbNoOfTransactions.Location = new System.Drawing.Point(1221, 395);
            this.dtbNoOfTransactions.Margin = new System.Windows.Forms.Padding(6);
            this.dtbNoOfTransactions.Name = "dtbNoOfTransactions";
            this.dtbNoOfTransactions.Size = new System.Drawing.Size(149, 31);
            this.dtbNoOfTransactions.TabIndex = 87;
            this.dtbNoOfTransactions.TextChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartDate.Location = new System.Drawing.Point(169, 367);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(6);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(232, 31);
            this.dtpStartDate.TabIndex = 70;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 372);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 25);
            this.label6.TabIndex = 88;
            this.label6.Text = "Start Date";
            // 
            // grpTransfer
            // 
            this.grpTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTransfer.Controls.Add(this.pnlTransfer);
            this.grpTransfer.Controls.Add(this.rdoTransfer);
            this.grpTransfer.Location = new System.Drawing.Point(19, 469);
            this.grpTransfer.Name = "grpTransfer";
            this.grpTransfer.Size = new System.Drawing.Size(1448, 192);
            this.grpTransfer.TabIndex = 89;
            this.grpTransfer.TabStop = false;
            this.grpTransfer.Text = "Amount";
            // 
            // pnlTransfer
            // 
            this.pnlTransfer.Controls.Add(this.cboToAccount);
            this.pnlTransfer.Controls.Add(this.label4);
            this.pnlTransfer.Controls.Add(this.label8);
            this.pnlTransfer.Controls.Add(this.label2);
            this.pnlTransfer.Controls.Add(this.dtbTransferAmount);
            this.pnlTransfer.Controls.Add(this.cboFromAccount);
            this.pnlTransfer.Enabled = false;
            this.pnlTransfer.Location = new System.Drawing.Point(6, 30);
            this.pnlTransfer.Name = "pnlTransfer";
            this.pnlTransfer.Size = new System.Drawing.Size(1423, 156);
            this.pnlTransfer.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(766, 42);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 25);
            this.label8.TabIndex = 74;
            this.label8.Text = "to account";
            // 
            // dtbTransferAmount
            // 
            this.dtbTransferAmount.Location = new System.Drawing.Point(113, 39);
            this.dtbTransferAmount.Margin = new System.Windows.Forms.Padding(6);
            this.dtbTransferAmount.Name = "dtbTransferAmount";
            this.dtbTransferAmount.Size = new System.Drawing.Size(142, 31);
            this.dtbTransferAmount.TabIndex = 65;
            this.dtbTransferAmount.TextChanged += new System.EventHandler(this.dtbTransferAmount_TextChanged);
            // 
            // rdoTransfer
            // 
            this.rdoTransfer.AutoSize = true;
            this.rdoTransfer.Location = new System.Drawing.Point(12, 0);
            this.rdoTransfer.Name = "rdoTransfer";
            this.rdoTransfer.Size = new System.Drawing.Size(123, 29);
            this.rdoTransfer.TabIndex = 67;
            this.rdoTransfer.TabStop = true;
            this.rdoTransfer.Text = "Transfer";
            this.rdoTransfer.UseVisualStyleBackColor = true;
            this.rdoTransfer.CheckedChanged += new System.EventHandler(this.rdoTransfer_CheckedChanged);
            // 
            // grpTransaction
            // 
            this.grpTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTransaction.Controls.Add(this.pnlTransaction);
            this.grpTransaction.Controls.Add(this.rdoTransaction);
            this.grpTransaction.Location = new System.Drawing.Point(13, 680);
            this.grpTransaction.Name = "grpTransaction";
            this.grpTransaction.Size = new System.Drawing.Size(1447, 270);
            this.grpTransaction.TabIndex = 91;
            this.grpTransaction.TabStop = false;
            this.grpTransaction.Text = "groupBox4";
            // 
            // pnlTransaction
            // 
            this.pnlTransaction.Controls.Add(this.cboAccount);
            this.pnlTransaction.Controls.Add(this.label7);
            this.pnlTransaction.Controls.Add(this.panel1);
            this.pnlTransaction.Controls.Add(this.cboCatagory);
            this.pnlTransaction.Controls.Add(this.cboProperty);
            this.pnlTransaction.Controls.Add(this.label5);
            this.pnlTransaction.Controls.Add(this.label9);
            this.pnlTransaction.Controls.Add(this.label3);
            this.pnlTransaction.Controls.Add(this.cboPayee);
            this.pnlTransaction.Controls.Add(this.dtbTransactionAmount);
            this.pnlTransaction.Location = new System.Drawing.Point(6, 30);
            this.pnlTransaction.Name = "pnlTransaction";
            this.pnlTransaction.Size = new System.Drawing.Size(1435, 234);
            this.pnlTransaction.TabIndex = 1;
            // 
            // cboAccount
            // 
            this.cboAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(182, 63);
            this.cboAccount.Margin = new System.Windows.Forms.Padding(6);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(420, 33);
            this.cboAccount.TabIndex = 75;
            this.cboAccount.SelectedIndexChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 25);
            this.label7.TabIndex = 76;
            this.label7.Text = "to account";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoDebit);
            this.panel1.Controls.Add(this.rdoCredit);
            this.panel1.Location = new System.Drawing.Point(1189, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 53);
            this.panel1.TabIndex = 75;
            // 
            // cboCatagory
            // 
            this.cboCatagory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCatagory.FormattingEnabled = true;
            this.cboCatagory.Location = new System.Drawing.Point(130, 121);
            this.cboCatagory.Margin = new System.Windows.Forms.Padding(6);
            this.cboCatagory.Name = "cboCatagory";
            this.cboCatagory.Size = new System.Drawing.Size(851, 33);
            this.cboCatagory.TabIndex = 66;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 121);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 25);
            this.label9.TabIndex = 65;
            this.label9.Text = "Catagory";
            // 
            // dtbTransactionAmount
            // 
            this.dtbTransactionAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtbTransactionAmount.Location = new System.Drawing.Point(1034, 121);
            this.dtbTransactionAmount.Margin = new System.Windows.Forms.Padding(6);
            this.dtbTransactionAmount.Name = "dtbTransactionAmount";
            this.dtbTransactionAmount.Size = new System.Drawing.Size(146, 31);
            this.dtbTransactionAmount.TabIndex = 57;
            this.dtbTransactionAmount.TextChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // rdoTransaction
            // 
            this.rdoTransaction.AutoSize = true;
            this.rdoTransaction.Checked = true;
            this.rdoTransaction.Location = new System.Drawing.Point(12, 0);
            this.rdoTransaction.Name = "rdoTransaction";
            this.rdoTransaction.Size = new System.Drawing.Size(156, 29);
            this.rdoTransaction.TabIndex = 0;
            this.rdoTransaction.TabStop = true;
            this.rdoTransaction.Text = "Transaction";
            this.rdoTransaction.UseVisualStyleBackColor = true;
            this.rdoTransaction.CheckedChanged += new System.EventHandler(this.rdoTransaction_CheckedChanged);
            // 
            // lblReasons
            // 
            this.lblReasons.Location = new System.Drawing.Point(59, 120);
            this.lblReasons.Name = "lblReasons";
            this.lblReasons.Size = new System.Drawing.Size(1381, 208);
            this.lblReasons.TabIndex = 92;
            this.lblReasons.Text = "label10";
            this.lblReasons.Click += new System.EventHandler(this.lblReasons_Click);
            // 
            // ScheduledTransactionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 1024);
            this.Controls.Add(this.lblReasons);
            this.Controls.Add(this.grpTransaction);
            this.Controls.Add(this.grpTransfer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.dtbNoOfTransactions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.rdoNoOfTransactions);
            this.Controls.Add(this.rdoEndDate);
            this.Controls.Add(this.ctrlDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Name = "ScheduledTransactionsDlg";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpTransfer.ResumeLayout(false);
            this.grpTransfer.PerformLayout();
            this.pnlTransfer.ResumeLayout(false);
            this.pnlTransfer.PerformLayout();
            this.grpTransaction.ResumeLayout(false);
            this.grpTransaction.PerformLayout();
            this.pnlTransaction.ResumeLayout(false);
            this.pnlTransaction.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker ctrlDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPayee;
        private System.Windows.Forms.ComboBox cboProperty;
        private System.Windows.Forms.ComboBox cboToAccount;
        private System.Windows.Forms.RadioButton rdoCredit;
        private System.Windows.Forms.RadioButton rdoDebit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFromAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoMonth;
        private System.Windows.Forms.RadioButton rdoQuarter;
        private System.Windows.Forms.RadioButton rdoNoOfTransactions;
        private System.Windows.Forms.RadioButton rdoEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private GenericControls.DecimalTextBox dtbNoOfTransactions;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdoWeek;
        private System.Windows.Forms.GroupBox grpTransfer;
        private System.Windows.Forms.GroupBox grpTransaction;
        private System.Windows.Forms.RadioButton rdoTransaction;
        private System.Windows.Forms.Label label8;
        private GenericControls.DecimalTextBox dtbTransferAmount;
        private System.Windows.Forms.ComboBox cboCatagory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rdoTransfer;
        private GenericControls.DecimalTextBox dtbTransactionAmount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTransfer;
        private System.Windows.Forms.Panel pnlTransaction;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblReasons;
    }
}