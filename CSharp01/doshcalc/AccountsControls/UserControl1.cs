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
    public partial class UserControl1 : UserControl
    {
        public CatagoryId CatagoryId;
        public AccountId TransferId;

        private Accounts _accounts;
        public UserControl1(Accounts accounts)
        {
            _accounts = accounts;
            InitializeComponent();

            if (TransferId != null)
            {
                this.cbo.SelectedItem = TagString.FindItem(this.cbo, TransferId);
            }
            else
            {
                this.cbo.SelectedItem = TagString.FindItem(this.cbo, CatagoryId);
            }

            refreshCatagoriesCbo();
        }

        private void refreshCatagoriesCbo()
        {
            this.cbo.Items.Clear();
            foreach (CatagoryId id in _accounts.CatagoryList.Keys)
            {
                this.cbo.Items.Add(new TagString(id, this._accounts.Catagory(id).Name));
            }

            bool includeHiddenAccounts = false;
            foreach (KeyValuePair<AccountId, Account> kvp in _accounts.AccountList(includeHiddenAccounts))
            {
                //if (kvp.Key != _idAccount)
                //{
                this.cbo.Items.Add(new TagString(kvp.Key, "Transfer to " + kvp.Value.Name));
                //}
            }
        }
    }
}
