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
	public partial class ObjectEntryListView : EditorFactoryListView
	{	
		private EntryEditCtrl _editor;
		private Accounts _accounts;
		private AccountId _idAccount;
		public AccountId AccountId
		{
			get { return _idAccount; }
		}

		private ListViewColumnSorter lvwColumnSorter;

		public ObjectEntryListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts, AccountId idAccount)
		{			
			_accounts = accounts;
			_idAccount = idAccount;
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView.ListViewItemSorter = lvwColumnSorter;
		}

		public override Control CreateEditor()
		{
			if(_editor == null)
			{
				_editor = new EntryEditCtrl(_accounts, _idAccount);
			}
			return _editor;
		}

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
            //if (selectedItems.Count > 1)
            //{
            //    var entries = new List<EntryId>();
            //    foreach (ListViewItem item in selectedItems)
            //    {
            //        entries.Add((EntryId)item.Tag);
            //    }

            //    _editor.SetItemId(entries);
            //    return;
            //}
            
            if(e.IsSelected == true)
			{
				_editor.SetItemId((EntryId)e.Item.Tag);
			}			
		}

		private ListViewItem createlistitem(KeyValuePair<EntryId, Entry> kvp)
		{
			Entry entry = kvp.Value;				
			ListViewItem item = new ListViewItem(entry.Date.ToShortDateString());
			item.Tag = kvp.Key;

            //item.SubItems.Add(entry.Date.ToShortDateString());    
            item.SubItems.Add(entry.Date.ToString("yyyy-MM-dd"));
					
			item.SubItems.Add(entry.Description);
            item.SubItems.Add(entry.ImportDescription);
							
			if(!entry.CatagoryId.IsEmpty())
			{
                string ttemp = _accounts.Catagory(entry.CatagoryId).Name;
                item.SubItems.Add(_accounts.Catagory(entry.CatagoryId).Name);
			}
			else
			{
				item.SubItems.Add(" - ");
			}

			if(!entry.PropertyId.IsEmpty())
			{
				item.SubItems.Add(_accounts.Property(entry.PropertyId).Name);
			}
			else
			{
				item.SubItems.Add(" - ");
			}

			if(!entry.TransferAccountId.IsEmpty())
			{
				item.SubItems.Add(_accounts.Account(entry.TransferAccountId).Name);
			}
			else
			{
				item.SubItems.Add(" - ");
			}

			if(entry.Amount >= 0.0M)
			{
				ListViewItem.ListViewSubItem sub = item.SubItems.Add(entry.Amount.ToString());
				sub.Tag = entry.Amount;
				item.SubItems.Add("");
			}
			else
			{
				item.SubItems.Add("");
				ListViewItem.ListViewSubItem sub = item.SubItems.Add(Math.Abs(entry.Amount).ToString());
				sub.Tag = entry.Amount;
			}





					
					
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

		public void RefreshItems(EntryId id)
		{			
			int count = this.listView.SelectedItems.Count;
			EntryId idSelectedEntry = EntryId.Empty();
			if(count == 1)
			{
				idSelectedEntry = (EntryId)this.listView.SelectedItems[0].Tag;
			}
			
			//if(true)
            if(id.IsEmpty())
			{
				this.listView.Items.Clear();
				foreach(KeyValuePair<EntryId, Entry> kvp in _accounts.EntryList(_idAccount))
				{
					ListViewItem newItem = createlistitem(kvp);

					this.listView.Items.Add(newItem);
					if(kvp.Key == idSelectedEntry)
					{
						this.listView.Items[this.listView.Items.IndexOf(newItem)].Selected = true;
					}
				}
			}
			else
			{
				if(_accounts.Entry(id) == null)
				{
					foreach(ListViewItem item in this.listView.Items)
					{
						if(id == (EntryId)item.Tag)
						{
							this.listView.Items.Remove(item);
						}
					}
				}
				else
				{
					foreach(ListViewItem item in this.listView.Items)
					{
						if(id == (EntryId)item.Tag)
						{
							this.listView.Items.Remove(item);
							break;
						}
					}
					ListViewItem newItem = createlistitem(new KeyValuePair<EntryId, Entry>(id, _accounts.Entry(id)));
					this.listView.Items.Add(newItem);
					if(id == idSelectedEntry)
					{
						this.listView.Items[this.listView.Items.IndexOf(newItem)].Selected = true;
					}
				}
			}

			decimal total = 0.0M;
			int iCredit = 7;
			int iDebit = 8;			
			int iTotal = 9;

            int ItemPos = 0;
            Color shaded = Color.FromArgb(240, 240, 240);
			foreach(ListViewItem item in this.listView.Items)
			{
                item.SubItems[3].Text = ItemPos.ToString(); //.Tag; 
                
                if (string.IsNullOrWhiteSpace(item.SubItems[iDebit].Text))
				{
					total += (decimal)item.SubItems[iCredit].Tag;
				}
				else
				{
					total += (decimal)item.SubItems[iDebit].Tag;
				}
				if(item.SubItems.Count <= iTotal)
				{ 
					item.SubItems.Add(total.ToString());
				}
				else
				{
					item.SubItems[iTotal].Text = total.ToString();

				}

                if (ItemPos % 2 == 1)
                {
                    item.BackColor = shaded;
                    item.UseItemStyleForSubItems = true;
                }
                ++ItemPos;
			}			
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
			this.listView.Sort();
		}

		private void listView_MouseUp(object sender, MouseEventArgs e)
		{
			if(this.listView.SelectedItems.Count == 0)
			{
				_editor.SetItemId(EntryId.Empty());
			}
		}

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                frmMultiEdit frm = new frmMultiEdit(this._accounts, this._idAccount);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CatagoryId catagoryId = frm.CatagoryId;
                    ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
                    if (selectedItems.Count > 1)
                    {
                        var entries = new Dictionary<EntryId, Entry>();
                        foreach (ListViewItem item in selectedItems)
                        {
                            Entry entry = new Entry(this._accounts.Entry((EntryId)item.Tag));
                            if (!frm.CatagoryId.IsEmpty())
                            {
                                entry.CatagoryId = frm.CatagoryId;
                                entry.TransferAccountId = AccountId.Empty();
                            }
                            else if (!frm.TransferAccountId.IsEmpty())
                            {
                                entry.CatagoryId = CatagoryId.Empty(); 
                                entry.TransferAccountId = frm.TransferAccountId;
                            }
                            entries.Add((EntryId)item.Tag, entry);
                            if (_accounts.EntryCanReplace((EntryId)item.Tag, entry))
                            {
                                _accounts.EntryReplace((EntryId)item.Tag, entry);
                            }
                        }

                        
                        return;
                    }

                    //create an array of writable entries


            

                }
            }
        }	
	}
}
