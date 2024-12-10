namespace WindowsFormsControlLibrary1
{
    partial class frmMultiEdit
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboCatagory = new System.Windows.Forms.ComboBox();
            this.cboProperty = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPayee = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(54, 225);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 44);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(328, 225);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 44);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboCatagory
            // 
            this.cboCatagory.FormattingEnabled = true;
            this.cboCatagory.Location = new System.Drawing.Point(110, 15);
            this.cboCatagory.Margin = new System.Windows.Forms.Padding(6);
            this.cboCatagory.Name = "cboCatagory";
            this.cboCatagory.Size = new System.Drawing.Size(238, 33);
            this.cboCatagory.TabIndex = 2;
            this.cboCatagory.SelectedIndexChanged += new System.EventHandler(this.cboCatagory_SelectedIndexChanged);
            this.cboCatagory.SelectionChangeCommitted += new System.EventHandler(this.cboCatagory_SelectionChangeCommitted);
            // 
            // cboProperty
            // 
            this.cboProperty.FormattingEnabled = true;
            this.cboProperty.Location = new System.Drawing.Point(110, 60);
            this.cboProperty.Margin = new System.Windows.Forms.Padding(6);
            this.cboProperty.Name = "cboProperty";
            this.cboProperty.Size = new System.Drawing.Size(238, 33);
            this.cboProperty.TabIndex = 3;
            this.cboProperty.SelectionChangeCommitted += new System.EventHandler(this.cboProperty_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // cboPayee
            // 
            this.cboPayee.FormattingEnabled = true;
            this.cboPayee.Location = new System.Drawing.Point(165, 142);
            this.cboPayee.Margin = new System.Windows.Forms.Padding(6);
            this.cboPayee.Name = "cboPayee";
            this.cboPayee.Size = new System.Drawing.Size(238, 33);
            this.cboPayee.TabIndex = 6;
            this.cboPayee.SelectionChangeCommitted += new System.EventHandler(this.cboPayee_SelectionChangeCommitted);
            // 
            // frmMultiEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 317);
            this.Controls.Add(this.cboPayee);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProperty);
            this.Controls.Add(this.cboCatagory);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmMultiEdit";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMultiEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboCatagory;
        private System.Windows.Forms.ComboBox cboProperty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPayee;
    }
}