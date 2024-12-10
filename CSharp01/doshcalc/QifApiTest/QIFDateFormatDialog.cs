using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using QifApi;
using System.Diagnostics;

namespace ApiTest
{
	public partial class QIFDateFormatDialog : Form
	{
		public QIFDateFormatDialog(QifDom.fileDateFormat format)
		{
			DateFormat = format;
			InitializeComponent();
			switch(format)
			{
				case QifDom.fileDateFormat.ddmmxxyy:
				{
					this.grpDateFormat.Enabled = false;
					this.rdoDayMonth.Checked = true;
					break;
				}
				case QifDom.fileDateFormat.mmddxxyy:
				{
					this.grpDateFormat.Enabled = false;
					this.rdoMonthDay.Checked = true;
					break;
				}
				case QifDom.fileDateFormat.xxxxyyyy:
				{
					this.grpCentury.Enabled = false;
					break;
				}				
				case QifDom.fileDateFormat.Undetermined:
				{
					break;
				}
			}
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{

		}

		public QifDom.fileDateFormat DateFormat { get; set; }

		private void button2_Click(object sender, EventArgs e)
		{
			if(DateFormat == QifDom.fileDateFormat.xxxxyyyy)
			{
				if(this.rdoDayMonth.Checked == true)
				{
					DateFormat = QifDom.fileDateFormat.ddmmyyyy;
				}
				else
				{
					DateFormat = QifDom.fileDateFormat.mmddyyyy;
				}
			}
			else if( (DateFormat == QifDom.fileDateFormat.ddmmxxyy)
					|| (DateFormat == QifDom.fileDateFormat.mmddxxyy)
					|| (DateFormat == QifDom.fileDateFormat.Undetermined) )
			{
				if( (this.rdoDayMonth.Checked == true) && (this.rdo19xx.Checked == true) )
				{
					DateFormat = QifDom.fileDateFormat.ddmm19yy;
				}
				else if( (this.rdoDayMonth.Checked == true) && (this.rdo19xx.Checked == false) )
				{
					DateFormat = QifDom.fileDateFormat.ddmm20yy;
				}
				else if( (this.rdoDayMonth.Checked == false) && (this.rdo19xx.Checked == true) )
				{
					DateFormat = QifDom.fileDateFormat.mmdd19yy;
				}
				else
				{
					DateFormat = QifDom.fileDateFormat.mmdd20yy;
				}
			}
			else 
			{
				Debug.Assert(false);
			}
		}

		private void QIFDateFormatDialog_Load(object sender, EventArgs e)
		{

		}
	}
}
