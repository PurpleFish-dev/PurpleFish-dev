using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;

namespace WindowsFormsApplication6
{
	public partial class CatagoriesFrm : Form
	{
		private Accounts _accounts;
		
		public CatagoriesFrm(Accounts accounts)
		{
			InitializeComponent();
			_accounts = accounts;
		}

		private void CatagoriesFrm_Load(object sender, EventArgs e)
		{
			foreach(Guid key in _accounts.CatagoryList.Keys)
			{
				ListViewItem item = new ListViewItem();
				populateListViewItem( ref item, key );
				this.listView1.Items.Add(item);
			}			
		}

		private void populateListViewItem(ref ListViewItem item, Guid id)
		{
			Catagory catagory = _accounts.CatagoryList[id];
			
			item.SubItems.Clear();
			item.Text = catagory.Name;
			item.Tag = id;			
			if(catagory.Income == true)
			{
				item.SubItems.Add("Inc.");
			}
			else
			{
				item.SubItems.Add("Exp.");
			}
			if(catagory.PropertySpecific == true)
			{
				item.SubItems.Add("Yes");
			}
			else
			{
				item.SubItems.Add("No");
			}
			item.SubItems.Add(this._accounts.TaxCodeList[catagory.TaxCodeId].Name);		
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			CatagoryFrm frm = new CatagoryFrm(this._accounts);
			if( frm.ShowDialog() == DialogResult.OK)
			{
				Guid id = _accounts.AddCatagory(frm.Catagory);
				ListViewItem item = new ListViewItem(frm.Catagory.Name);
				populateListViewItem(ref item, id);			
				this.listView1.Items.Add(item);
			}
		}		

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if(this.listView1.SelectedItems.Count >= 1)
			{
				ListViewItem item = this.listView1.SelectedItems[0];
				if(!string.IsNullOrWhiteSpace(item.Text))
				{
					if(item != null)
					{
						this._accounts.RemoveCatagory( (Guid)item.Tag );
						this.listView1.Items.Remove(item);
					}
				}
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = ((ListView)sender).FocusedItem;
			//ListViewItem item = ((ListView)sender).SelectedItems[0];
				CatagoryFrm frm = new CatagoryFrm(this._accounts, (Guid)item.Tag);
				if( frm.ShowDialog() == DialogResult.OK)
				{
					//_accounts.CatagoryList[(Guid)item.Tag] = frm.Catagory;
					_accounts.ReplaceCatagory((Guid)item.Tag, frm.Catagory);
					populateListViewItem( ref item, (Guid)item.Tag );
				}
			//item.BeginEdit();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{			
			/*
			 * if(((ListView)sender).SelectedItems.Count == 1)
			{
				ListViewItem item = ((ListView)sender).SelectedItems[0];
				CatagoryFrm frm = new CatagoryFrm(this._accounts, (Guid)item.Tag);
				if( frm.ShowDialog() == DialogResult.OK)
				{
					//_accounts.CatagoryList[(Guid)item.Tag] = frm.Catagory;
					_accounts.ReplaceCatagory((Guid)item.Tag, frm.Catagory);
					populateListViewItem( ref item, (Guid)item.Tag );
				}
			}
			 * */
		}

			
	}
}
