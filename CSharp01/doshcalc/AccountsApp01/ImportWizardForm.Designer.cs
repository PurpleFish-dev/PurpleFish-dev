namespace WindowsFormsApplication6
{
	partial class ImportWizardForm
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
			this.importWizard = new WindowsFormsApplication6.ImportWizard();
			this.SuspendLayout();
			// 
			// importWizard
			// 
			this.importWizard.Dock = System.Windows.Forms.DockStyle.Fill;
			this.importWizard.Location = new System.Drawing.Point(0, 0);
			this.importWizard.Name = "importWizard";
			this.importWizard.Size = new System.Drawing.Size(724, 369);
			this.importWizard.TabIndex = 0;
			// 
			// ImportWizardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 369);
			this.Controls.Add(this.importWizard);
			this.Name = "ImportWizardForm";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private ImportWizard importWizard;


	}
}