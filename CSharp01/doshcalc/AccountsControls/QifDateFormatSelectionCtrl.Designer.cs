namespace WindowsFormsControlLibrary1
{
	partial class QifDateFormatSelectionCtrl
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
			this.rdo19xx = new System.Windows.Forms.RadioButton();
			this.rdoMonthDay = new System.Windows.Forms.RadioButton();
			this.rdo20xx = new System.Windows.Forms.RadioButton();
			this.grpCentury = new System.Windows.Forms.GroupBox();
			this.rdoDayMonth = new System.Windows.Forms.RadioButton();
			this.grpDateFormat = new System.Windows.Forms.GroupBox();
			this.grpCentury.SuspendLayout();
			this.grpDateFormat.SuspendLayout();
			this.SuspendLayout();
			// 
			// rdo19xx
			// 
			this.rdo19xx.AutoSize = true;
			this.rdo19xx.Location = new System.Drawing.Point(6, 19);
			this.rdo19xx.Name = "rdo19xx";
			this.rdo19xx.Size = new System.Drawing.Size(47, 17);
			this.rdo19xx.TabIndex = 3;
			this.rdo19xx.Text = "19xx";
			this.rdo19xx.UseVisualStyleBackColor = true;
			// 
			// rdoMonthDay
			// 
			this.rdoMonthDay.AutoSize = true;
			this.rdoMonthDay.Checked = true;
			this.rdoMonthDay.Location = new System.Drawing.Point(6, 19);
			this.rdoMonthDay.Name = "rdoMonthDay";
			this.rdoMonthDay.Size = new System.Drawing.Size(95, 17);
			this.rdoMonthDay.TabIndex = 0;
			this.rdoMonthDay.TabStop = true;
			this.rdoMonthDay.Text = "mm / dd / yyyy";
			this.rdoMonthDay.UseVisualStyleBackColor = true;
			// 
			// rdo20xx
			// 
			this.rdo20xx.AutoSize = true;
			this.rdo20xx.Checked = true;
			this.rdo20xx.Location = new System.Drawing.Point(6, 42);
			this.rdo20xx.Name = "rdo20xx";
			this.rdo20xx.Size = new System.Drawing.Size(47, 17);
			this.rdo20xx.TabIndex = 2;
			this.rdo20xx.TabStop = true;
			this.rdo20xx.Text = "20xx";
			this.rdo20xx.UseVisualStyleBackColor = true;
			// 
			// grpCentury
			// 
			this.grpCentury.Controls.Add(this.rdo20xx);
			this.grpCentury.Controls.Add(this.rdo19xx);
			this.grpCentury.Location = new System.Drawing.Point(96, 140);
			this.grpCentury.Name = "grpCentury";
			this.grpCentury.Size = new System.Drawing.Size(146, 70);
			this.grpCentury.TabIndex = 6;
			this.grpCentury.TabStop = false;
			this.grpCentury.Text = "Century";
			// 
			// rdoDayMonth
			// 
			this.rdoDayMonth.AutoSize = true;
			this.rdoDayMonth.Location = new System.Drawing.Point(6, 42);
			this.rdoDayMonth.Name = "rdoDayMonth";
			this.rdoDayMonth.Size = new System.Drawing.Size(95, 17);
			this.rdoDayMonth.TabIndex = 1;
			this.rdoDayMonth.Text = "dd / mm / yyyy";
			this.rdoDayMonth.UseVisualStyleBackColor = true;
			// 
			// grpDateFormat
			// 
			this.grpDateFormat.Controls.Add(this.rdoDayMonth);
			this.grpDateFormat.Controls.Add(this.rdoMonthDay);
			this.grpDateFormat.Location = new System.Drawing.Point(96, 61);
			this.grpDateFormat.Name = "grpDateFormat";
			this.grpDateFormat.Size = new System.Drawing.Size(146, 73);
			this.grpDateFormat.TabIndex = 5;
			this.grpDateFormat.TabStop = false;
			this.grpDateFormat.Text = "Month <-> Day";
			// 
			// QifDateFormatSelectionCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpCentury);
			this.Controls.Add(this.grpDateFormat);
			this.Name = "QifDateFormatSelectionCtrl";
			this.Size = new System.Drawing.Size(515, 277);
			this.Load += new System.EventHandler(this.QifDateFormatSelectionCtrl_Load);
			this.grpCentury.ResumeLayout(false);
			this.grpCentury.PerformLayout();
			this.grpDateFormat.ResumeLayout(false);
			this.grpDateFormat.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton rdo19xx;
		private System.Windows.Forms.RadioButton rdoMonthDay;
		private System.Windows.Forms.RadioButton rdo20xx;
		private System.Windows.Forms.GroupBox grpCentury;
		private System.Windows.Forms.RadioButton rdoDayMonth;
		private System.Windows.Forms.GroupBox grpDateFormat;
	}
}
