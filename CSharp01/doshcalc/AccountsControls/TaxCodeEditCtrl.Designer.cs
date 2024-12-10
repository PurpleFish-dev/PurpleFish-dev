namespace WindowsFormsControlLibrary1
{
	partial class TaxCodeEditCtrl
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
			this.txtReceiptNo = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnEnter = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkObsolete = new System.Windows.Forms.CheckBox();
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
			// txtReceiptNo
			// 
			this.txtReceiptNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtReceiptNo.Location = new System.Drawing.Point(44, 51);
			this.txtReceiptNo.Name = "txtReceiptNo";
			this.txtReceiptNo.Size = new System.Drawing.Size(147, 20);
			this.txtReceiptNo.TabIndex = 31;
			this.txtReceiptNo.TextChanged += new System.EventHandler(this.txtReceiptNo_TextChanged);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(121, 13);
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
			this.btnDelete.Location = new System.Drawing.Point(159, 12);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(32, 32);
			this.btnDelete.TabIndex = 37;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnEnter
			// 
			this.btnEnter.Location = new System.Drawing.Point(3, 105);
			this.btnEnter.Name = "btnEnter";
			this.btnEnter.Size = new System.Drawing.Size(62, 20);
			this.btnEnter.TabIndex = 38;
			this.btnEnter.Text = "666";
			this.btnEnter.UseVisualStyleBackColor = true;
			this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(71, 105);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(62, 20);
			this.btnCancel.TabIndex = 39;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkObsolete
			// 
			this.chkObsolete.AutoSize = true;
			this.chkObsolete.Location = new System.Drawing.Point(6, 79);
			this.chkObsolete.Name = "chkObsolete";
			this.chkObsolete.Size = new System.Drawing.Size(68, 17);
			this.chkObsolete.TabIndex = 40;
			this.chkObsolete.Text = "Obsolete";
			this.chkObsolete.UseVisualStyleBackColor = true;
			this.chkObsolete.CheckedChanged += new System.EventHandler(this.chkObsolete_CheckedChanged);
			// 
			// TaxCodeEditCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkObsolete);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnEnter);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtReceiptNo);
			this.Name = "TaxCodeEditCtrl";
			this.Size = new System.Drawing.Size(195, 138);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtReceiptNo;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnEnter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkObsolete;
	}
}
