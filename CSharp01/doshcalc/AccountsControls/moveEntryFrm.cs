using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;
using System.Diagnostics;

namespace WindowsFormsControlLibrary1
{
    public partial class moveEntryFrm : Form
    {
        Accounts _accounts;
        AccountId _idAccount;

        public AccountId TransferAccountId;
        public moveEntryFrm(Accounts accounts, AccountId idAccount, List<Entry> entries)
        {
            _accounts = accounts;
			_idAccount = idAccount;
            
            InitializeComponent();

            bool includeHiddenAccounts = false;
            foreach (KeyValuePair<AccountId, Account> kvp in _accounts.AccountList(includeHiddenAccounts))
            {
                if (kvp.Key != _idAccount)
                    this.cboCatagory.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
            }

        }

        private void cboCatagory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Id id = ((TagString)cboCatagory.SelectedItem).Id;
            TransferAccountId = id as AccountId;
        }        
    }
}
