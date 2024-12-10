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
	public partial class AccountListView : EditorFactoryListView
	{
		private Accounts _accounts;
		private AccountEditCtrl _editor;

		public AccountListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts)
		{			
			_accounts = accounts;
		}

		public override Control CreateEditor()
		{
            _editor = new AccountEditCtrl(_accounts);
            if (this.listView.SelectedItems.Count != 0)
            {
                _editor.SetItemId((AccountId)this.listView.SelectedItems[0].Tag);
            }
            return _editor;
		}

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if(e.IsSelected == true)
			{
				_editor.SetItemId((AccountId)e.Item.Tag);
			}			
		}

		public void RefreshItems()
		{
			this.listView.Items.Clear();
			if(_accounts != null)
			{
				bool includeHiddenAccounts = true;
				foreach(KeyValuePair<AccountId, Account> kvp  in _accounts.AccountList(includeHiddenAccounts))
				{
					ListViewItem item = new ListViewItem(kvp.Value.Name);
					item.Tag = kvp.Key;			
					Font font = item.Font;
					if(kvp.Value.Hidden == true)
					{
						item.Font = new Font(font, FontStyle.Strikeout);
					}
					else
					{
						item.Font = new Font(font, FontStyle.Regular);
					}
					
					if(kvp.Value.External)
					{
						item.SubItems.Add("External.");
					}
					else
					{
						item.SubItems.Add("Internal");
					}
					if(kvp.Value.Type == Account.eType.Credit)
					{
						item.SubItems.Add("Credit");
					}
					else
					{
						item.SubItems.Add("Liability");
					}

					if(kvp.Value.Lock == Account.eLock.Locked)
					{
						item.SubItems.Add("Locked");
					}
					else if(kvp.Value.Lock == Account.eLock.ByDate)
					{
						item.SubItems.Add("ByDate");
					}
					else
					{
						item.SubItems.Add("Open");
					}

					item.SubItems.Add(kvp.Value.LockedUntil.ToShortDateString());
					item.SubItems.Add(kvp.Value.ReconciledOn.ToShortDateString());						

					this.listView.Items.Add(item);
				}
			}
		}

		private void listView_MouseUp(object sender, MouseEventArgs e)
		{
			if(this.listView.SelectedItems.Count == 0)
			{
				_editor.SetItemId(null);
			}
		}

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }
    }
}
