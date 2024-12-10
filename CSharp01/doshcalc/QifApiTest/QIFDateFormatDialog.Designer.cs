namespace ApiTest
{
	partial class QIFDateFormatDialog
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
			this.rdoMonthDay = new System.Windows.Forms.RadioButton();
			this.grpDateFormat = new System.Windows.Forms.GroupBox();
			this.rdoDayMonth = new System.Windows.Forms.RadioButton();
			this.grpCentury = new System.Windows.Forms.GroupBox();
			this.rdo20xx = new System.Windows.Forms.RadioButton();
			this.rdo19xx = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.grpDateFormat.SuspendLayout();
			this.grpCentury.SuspendLayout();
			this.SuspendLayout();
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
			this.rdoMonthDay.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// grpDateFormat
			// 
			this.grpDateFormat.Controls.Add(this.rdoDayMonth);
			this.grpDateFormat.Controls.Add(this.rdoMonthDay);
			this.grpDateFormat.Location = new System.Drawing.Point(12, 12);
			this.grpDateFormat.Name = "grpDateFormat";
			this.grpDateFormat.Size = new System.Drawing.Size(146, 73);
			this.grpDateFormat.TabIndex = 1;
			this.grpDateFormat.TabStop = false;
			this.grpDateFormat.Text = "Month <-> Day";
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
			// grpCentury
			// 
			this.grpCentury.Controls.Add(this.rdo20xx);
			this.grpCentury.Controls.Add(this.rdo19xx);
			this.grpCentury.Location = new System.Drawing.Point(12, 91);
			this.grpCentury.Name = "grpCentury";
			this.grpCentury.Size = new System.Drawing.Size(146, 70);
			this.grpCentury.TabIndex = 2;
			this.grpCentury.TabStop = false;
			this.grpCentury.Text = "Century";
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
			this.rdo20xx.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
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
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(260, 143);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button2.Location = new System.Drawing.Point(260, 114);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "OK";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// QIFDateFormatDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(347, 178);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.grpCentury);
			this.Controls.Add(this.grpDateFormat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "QIFDateFormatDialog";
			this.Text = "Date Format";
			this.Load += new System.EventHandler(this.QIFDateFormatDialog_Load);
			this.grpDateFormat.ResumeLayout(false);
			this.grpDateFormat.PerformLayout();
			this.grpCentury.ResumeLayout(false);
			this.grpCentury.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton rdoMonthDay;
		private System.Windows.Forms.GroupBox grpDateFormat;
		private System.Windows.Forms.RadioButton rdoDayMonth;
		private System.Windows.Forms.GroupBox grpCentury;
		private System.Windows.Forms.RadioButton rdo20xx;
		private System.Windows.Forms.RadioButton rdo19xx;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}