using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;

namespace WindowsFormsControlLibrary1
{
	public partial class QifBasicTransactionListView : EditorFactoryListView
	{	
		private EntryEditCtrl _editor;
		QifApi.QifDom _dom;
		
		private ListViewColumnSorter lvwColumnSorter;

		public QifBasicTransactionListView()
		{
			InitializeComponent();
		}

		public void Initialize(QifApi.QifDom dom)
		{			
			_dom = dom;
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView1.ListViewItemSorter = lvwColumnSorter;
		}

		private ListViewItem createlistitem(QifApi.Transactions.BasicTransaction transaction)
		{
			ListViewItem item = new ListViewItem(transaction.Date.ToShortDateString());
							
			item.SubItems.Add(transaction.Memo);	
			item.SubItems.Add(transaction.Payee);
			item.SubItems.Add(transaction.Category);
			item.SubItems.Add(transaction.Amount.ToString());
							
			



					
					
			//item.Text = entry.Name;
			//item.Tag = id;			
			//if(catagory.Income == true)
			//{
			//	item.SubItems.Add("Inc.");
			//}
			//else
			//{
			//	item.SubItems.Add("Exp.");
			//}
			//if(catagory.PropertySpecific == true)
			//{
			//	item.SubItems.Add("Yes");
			//}
			//else
			//{
			//	item.SubItems.Add("No");
			//}
			//item.SubItems.Add(this._accounts.TaxCodeList[catagory.TaxCodeId].Name);	
			return item;
		}

		private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if ( e.Column == lvwColumnSorter.SortColumn )
			{
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.listView1.Sort();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		internal void AddTransaction(List<QifApi.Transactions.BasicTransaction> list)
		{
			foreach(QifApi.Transactions.BasicTransaction transaction in list)
			{
				this.listView1.Items.Add(this.createlistitem(transaction));
			}
		}
	}
}
