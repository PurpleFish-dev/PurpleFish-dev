namespace WindowsFormsControlLibrary1
{
	partial class AccountEditCtrl
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
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnEnter = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.chkExternal = new System.Windows.Forms.CheckBox();
			this.rdoLockedUntil = new System.Windows.Forms.RadioButton();
			this.rdoLocked = new System.Windows.Forms.RadioButton();
			this.rdoUnLocked = new System.Windows.Forms.RadioButton();
			this.dtpLockedUntil = new System.Windows.Forms.DateTimePicker();
			this.dtpReconciledOn = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.chkHidden = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(3, 55);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 35;
			this.lblName.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(44, 51);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(180, 20);
			this.txtName.TabIndex = 31;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(154, 13);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(32, 32);
			this.btnAdd.TabIndex = 36;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(192, 12);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(32, 32);
			this.btnDelete.TabIndex = 37;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnEnter
			// 
			this.btnEnter.Location = new System.Drawing.Point(0, 249);
			this.btnEnter.Name = "btnEnter";
			this.btnEnter.Size = new System.Drawing.Size(62, 20);
			this.btnEnter.TabIndex = 38;
			this.btnEnter.Text = "666";
			this.btnEnter.UseVisualStyleBackColor = true;
			this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(68, 249);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(62, 20);
			this.btnCancel.TabIndex = 39;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cboType
			// 
			this.cboType.FormattingEnabled = true;
			this.cboType.Location = new System.Drawing.Point(6, 77);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(124, 21);
			this.cboType.TabIndex = 40;
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			// 
			// chkExternal
			// 
			this.chkExternal.AutoSize = true;
			this.chkExternal.Location = new System.Drawing.Point(136, 79);
			this.chkExternal.Name = "chkExternal";
			this.chkExternal.Size = new System.Drawing.Size(64, 17);
			this.chkExternal.TabIndex = 41;
			this.chkExternal.Text = "External";
			this.chkExternal.UseVisualStyleBackColor = true;
			this.chkExternal.CheckedChanged += new System.EventHandler(this.chkExternal_CheckedChanged);
			// 
			// rdoLockedUntil
			// 
			this.rdoLockedUntil.AutoSize = true;
			this.rdoLockedUntil.Location = new System.Drawing.Point(6, 172);
			this.rdoLockedUntil.Name = "rdoLockedUntil";
			this.rdoLockedUntil.Size = new System.Drawing.Size(83, 17);
			this.rdoLockedUntil.TabIndex = 51;
			this.rdoLockedUntil.TabStop = true;
			this.rdoLockedUntil.Text = "Locked until";
			this.rdoLockedUntil.UseVisualStyleBackColor = true;
			this.rdoLockedUntil.CheckedChanged += new System.EventHandler(this.lock_CheckedChanged);
			// 
			// rdoLocked
			// 
			this.rdoLocked.AutoSize = true;
			this.rdoLocked.Location = new System.Drawing.Point(6, 127);
			this.rdoLocked.Name = "rdoLocked";
			this.rdoLocked.Size = new System.Drawing.Size(61, 17);
			this.rdoLocked.TabIndex = 50;
			this.rdoLocked.TabStop = true;
			this.rdoLocked.Text = "Locked";
			this.rdoLocked.UseVisualStyleBackColor = true;
			this.rdoLocked.CheckedChanged += new System.EventHandler(this.lock_CheckedChanged);
			// 
			// rdoUnLocked
			// 
			this.rdoUnLocked.AutoSize = true;
			this.rdoUnLocked.Location = new System.Drawing.Point(6, 150);
			this.rdoUnLocked.Name = "rdoUnLocked";
			this.rdoUnLocked.Size = new System.Drawing.Size(71, 17);
			this.rdoUnLocked.TabIndex = 49;
			this.rdoUnLocked.TabStop = true;
			this.rdoUnLocked.Text = "Unlocked";
			this.rdoUnLocked.UseVisualStyleBackColor = true;
			this.rdoUnLocked.CheckedChanged += new System.EventHandler(this.lock_CheckedChanged);
			// 
			// dtpLockedUntil
			// 
			this.dtpLockedUntil.Location = new System.Drawing.Point(106, 172);
			this.dtpLockedUntil.Name = "dtpLockedUntil";
			this.dtpLockedUntil.Size = new System.Drawing.Size(118, 20);
			this.dtpLockedUntil.TabIndex = 48;
			this.dtpLockedUntil.ValueChanged += new System.EventHandler(this.dtpLockedUntil_ValueChanged);
			// 
			// dtpReconciledOn
			// 
			this.dtpReconciledOn.Location = new System.Drawing.Point(106, 211);
			this.dtpReconciledOn.Name = "dtpReconciledOn";
			this.dtpReconciledOn.Size = new System.Drawing.Size(118, 20);
			this.dtpReconciledOn.TabIndex = 52;
			this.dtpReconciledOn.ValueChanged += new System.EventHandler(this.dtpReconciledOn_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 215);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 56;
			this.label1.Text = "Reconciled on";
			// 
			// chkHidden
			// 
			this.chkHidden.AutoSize = true;
			this.chkHidden.Location = new System.Drawing.Point(6, 104);
			this.chkHidden.Name = "chkHidden";
			this.chkHidden.Size = new System.Drawing.Size(60, 17);
			this.chkHidden.TabIndex = 57;
			this.chkHidden.Text = "Hidden";
			this.chkHidden.UseVisualStyleBackColor = true;
			this.chkHidden.CheckedChanged += new System.EventHandler(this.chkHidden_CheckedChanged);
			// 
			// AccountEditCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.rdoLockedUntil);
			this.Controls.Add(this.rdoUnLocked);
			this.Controls.Add(this.rdoLocked);
			this.Controls.Add(this.chkHidden);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dtpReconciledOn);
			this.Controls.Add(this.dtpLockedUntil);
			this.Controls.Add(this.chkExternal);
			this.Controls.Add(this.cboType);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnEnter);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.Name = "AccountEditCtrl";
			this.Size = new System.Drawing.Size(228, 287);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnEnter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.CheckBox chkExternal;
		private System.Windows.Forms.RadioButton rdoLockedUntil;
		private System.Windows.Forms.RadioButton rdoLocked;
		private System.Windows.Forms.RadioButton rdoUnLocked;
		private System.Windows.Forms.DateTimePicker dtpLockedUntil;
		private System.Windows.Forms.DateTimePicker dtpReconciledOn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkHidden;
	}
}
