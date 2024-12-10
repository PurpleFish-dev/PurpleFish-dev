using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AccountsCore;

namespace WindowsFormsControlLibrary1
{
    public partial class TimeChartFrm : Form
    {
        private Accounts _accounts;
        private AccountId _accId;

        public TimeChartFrm(Accounts accounts, AccountId accId)
        {
            InitializeComponent();
            _accounts = accounts;
            _accId = accId;
                
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM-yy";
            // this sets the type of the X-Axis values
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            Filter filter= new Filter();
            filter.Accounts.Add(_accId);
            EntrySorter sorter = new EntrySorter();
            sorter.SetPivotAccount(_accounts, _accId);
            foreach (Entry entry in _accounts.EntryList(filter, sorter))
            {
                chart1.Series[0].Points.AddXY(entry.Date, entry.Balance);
            }
        }
    }
}
