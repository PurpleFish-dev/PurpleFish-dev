namespace WindowsFormsControlLibrary1
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbo
            // 
            this.cbo.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo.FormattingEnabled = true;
            this.cbo.Location = new System.Drawing.Point(3, 0);
            this.cbo.Name = "cbo";
            this.cbo.Size = new System.Drawing.Size(144, 21);
            this.cbo.TabIndex = 0;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbo);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(147, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo;
    }
}
