using AccountsCore;
//using LiveCharts;
//using LiveCharts.WinForms;//Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
    public partial class BasicBar : Form
    {        
        struct Chartpoint
        {
            public List<Entry> ents; 
            public decimal vvalue { get; set; }
            public decimal credit { get; set; }
            public decimal debit { get; set; }
        };
        SortedDictionary<DateTime, Chartpoint> data;// = new SortedDictionary<DateTime, Chartpoint>();

        public BasicBar(Accounts accounts)
        {
            InitializeComponent();

            this.cartesianChart1.Zoom = ZoomingOptions.Xy;

            data = new SortedDictionary<DateTime, Chartpoint>();

            Filter filter = new Filter();
            //EntrySorter sorter = new EntrySorter();
            //sorter.SetPivotAccount(accounts, null);
            List<Entry> ents = accounts.EntryList(filter, null);
        
            
            foreach (Entry ent in ents)
            {
                if (!ent.IsTransfer())
                {
                    DateTime month = ent.Date;
                    month = month.AddDays(1 - month.Day);
                    if (data.ContainsKey(month))
                    {
                        Chartpoint pt = data[month];
                        pt.ents.Add(ent);
                        pt.vvalue += ent.GetAmount(null);
                        if (ent.GetAmount(null) > 0) pt.credit += ent.GetAmount(null);
                        if (ent.GetAmount(null) < 0) pt.debit += ent.GetAmount(null);
                        data[month] = pt;
                    }
                    else
                    {
                        Chartpoint pt = new Chartpoint();
                        pt.ents = new List<Entry>();
                        pt.ents.Add(ent);
                        pt.vvalue = ent.GetAmount(null);
                        if (ent.GetAmount(null) > 0) pt.credit = ent.GetAmount(null);
                        if (ent.GetAmount(null) < 0) pt.debit = ent.GetAmount(null);
                        data.Add(month, pt);
                    }
                }
            }
           // data.sort(x => x.)
            //List<KeyValuePair<DateTime, decimal>> myList = data.ToList();

            //myList.Sort(x => x.Key
            //    delegate (KeyValuePair<DateTime, decimal> pair1,
            //    KeyValuePair<DateTime, decimal> pair2)
            //    {
            //        return pair1.Value.CompareTo(pair2.Value);
            //    }
            //);

            var values = new ChartValues<decimal>();// { 10, 50, 39, 50 }
            var credit = new ChartValues<decimal>();
            var debit = new ChartValues<decimal>();
            var labels = new List<string>();
            foreach (var col in data)
            {
                values.Add(col.Value.vvalue);
                credit.Add(col.Value.credit);
                debit.Add(col.Value.debit);
                labels.Add(col.Key.ToShortDateString());
            }

            //cartesianChart1.Series = new SeriesCollection();
            //var series = new LiveCharts.Wpf.ColumnSeries();

            cartesianChart1.Series = new SeriesCollection();
            //{
            //    //new LiveCharts.Wpf.ColumnSeries
            //    new LiveCharts.Wpf.LineSeries
            //    {
            //        Title = "2015",
            //        Values = values // new ChartValues<double> { 10, 50, 39, 50 }
            //    }
            //};

            ////adding series will update and animate the chart automatically
            cartesianChart1.Series.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "credit",
                Values = credit
            });

            ////adding series will update and animate the chart automatically
            cartesianChart1.Series.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "debit",
                Values = debit
            });

            ////also adding values updates and animates the chart automatically
            //cartesianChart1.Series[1].Values.Add(48d);

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Sales Man",
                Labels = labels//new[] { "Maria", "Susan", "Charles", "Frida" }
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Sold Apps",
                LabelFormatter = value => value.ToString("N")
            });
        }

        private void cartesianChart1_DataClick(object sender, ChartPoint chartPoint)
        {
            data.ElementAt(chartPoint.Key);

            var sb = new System.Text.StringBuilder();
            



            //string s = new string();
            foreach (Entry ent in data.ElementAt(chartPoint.Key).Value.ents)
            {
                //s += ent.ToString() + "\n" ;
                sb.AppendLine(ent.ToString());
            }
            MessageBox.Show(sb.ToString());
        }
    }
}
