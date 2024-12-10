using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QifApi;
using System.Diagnostics;
using GenericControls;

namespace WindowsFormsControlLibrary1
{
	public partial class QifDateFormatSelectionCtrl : WizardControl
	{
		public QifDateFormatSelectionCtrl()
		{
			InitializeComponent();
		}

		private void QifDateFormatSelectionCtrl_Load(object sender, EventArgs e)
		{

		}

        public override void OnLeave()
		{
			if(_dom.DayMonthFormat == QifDom.dayMonthFormat.Undetermined)
			{
				if(this.rdoDayMonth.Checked == true)
				{
                    _dom.DayMonthFormat = QifDom.dayMonthFormat.ddmm;
				}
				else
				{
					_dom.DayMonthFormat = QifDom.dayMonthFormat.mmdd;
				}
			}			
			else 
			{
				Debug.Assert(false);
			}
        }

		internal void Initialize(QifDom dom)
		{
			_dom = dom;
			InitializeComponent();
			switch(_dom.DayMonthFormat)
			{
				case QifDom.dayMonthFormat.Undetermined:
				{
					this.grpDateFormat.Enabled = true;
					this.rdoDayMonth.Checked = true;
					break;
				}
                case QifDom.dayMonthFormat.mmdd:
				{
					this.grpDateFormat.Enabled = false;
					this.rdoMonthDay.Checked = true;
					break;
				}
                case QifDom.dayMonthFormat.ddmm:
                {
                    this.grpDateFormat.Enabled = false;
                    this.rdoDayMonth.Checked = true;
                    break;
                }
			}
		}
		
		private QifDom _dom;
	}
}
