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
	public partial class CatagoryListView : EditorFactoryListView
	{	
		private CatagoryEditCtrl _editor;
		private Accounts _accounts;
		private ListViewColumnSorter lvwColumnSorter;

		public CatagoryListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts)
		{			
			_accounts = accounts;
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView.ListViewItemSorter = lvwColumnSorter;
		}

		public override Control CreateEditor()
		{
			_editor = new CatagoryEditCtrl(_accounts);
            if (this.listView.SelectedItems.Count != 0)
                _editor.SetItemId((CatagoryId)this.listView.SelectedItems[0].Tag);
            return _editor;
		}
        /*
        public override Control CreateEditor()
		{
            _editor = new AccountEditCtrl(_accounts);
            if (this.listView.SelectedItems.Count != 0)
            {
                _editor.SetItemId((AccountId)this.listView.SelectedItems[0].Tag);
            }
            return _editor;
		} */

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{			
			if(e.IsSelected == true)
			{
				_editor.SetItemId((CatagoryId)e.Item.Tag);
			}			
		}

		public void RefreshItems()
		{
			this.listView.Items.Clear();
			if(_accounts != null)
			{
				foreach(KeyValuePair<CatagoryId, Catagory> kvp  in _accounts.CatagoryList)
				{
					ListViewItem item = new ListViewItem(kvp.Value.Name);
					item.Tag = kvp.Key;				
					item.Text = kvp.Value.Name;					
					if(kvp.Value.Income == true)
					{
						item.SubItems.Add("Inc.");
					}
					else
					{
						item.SubItems.Add("Exp.");
					}
					if(kvp.Value.PropertySpecific == true)
					{
						item.SubItems.Add("Yes");
					}
					else
					{
						item.SubItems.Add("No");
					}
					Font font = item.Font;
					if(kvp.Value.Obsolete == true)
					{
						item.Font = new Font(font, FontStyle.Strikeout);
					}
					else
					{
						item.Font = new Font(font, FontStyle.Regular);
					}
                    TaxCode tc = this._accounts.TaxCode(kvp.Value.TaxCodeId);
                    item.SubItems.Add(tc == null ? "" : tc.Name);		

					this.listView.Items.Add(item);
				}
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
				_editor.SetItemId(null);
			}
		}	
	}
}
