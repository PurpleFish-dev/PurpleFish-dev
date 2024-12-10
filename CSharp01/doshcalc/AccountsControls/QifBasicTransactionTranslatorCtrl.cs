using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QifApi;
using GenericControls;
using System.Diagnostics;

namespace WindowsFormsControlLibrary1
{
	public partial class QifBasicTransactionTranslatorCtrl : WizardControl
	{
		public QifBasicTransactionTranslatorCtrl()
		{
			InitializeComponent();
		}

		public override bool OnEnter()
		{
            bool result = _dom.Finalize();
            Debug.Assert(result); 
            
            foreach(QifApi.Transactions.Account account in _dom.Accounts)
			{
			    if(account.AccountListTransaction.Selected)
				{
					this.qifBasicTransactionListView1.AddTransaction(account.Transactions);
				}
			}

			bool includeHiddenAccounts = false;
			foreach(KeyValuePair<AccountsCore.AccountId, AccountsCore.Account> kvp in _accounts.AccountList(includeHiddenAccounts))
			{
				this.lstCoreAccounts.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
			}
            //_selectedAccountId = ((AccountsCore.AccountId)((TagString)this.lstCoreAccounts.SelectedItem).Id);

			return true;
		}

        public override void OnLeave()
		{
            List<AccountsCore.Entry> entries = _dom.GetEntriesFromSelectedAccounts(_selectedAccountId, rdoPayeeToDescription.Checked, rdoMemoToDescription.Checked);

            foreach (AccountsCore.Entry enrty in entries)
                enrty.Type = AccountsCore.Entry.eType.Imported;

            DateTime recon = this._accounts.Account(_selectedAccountId).ReconciledOn;
            entries.RemoveAll(x => x.Date <= recon);

            IEnumerable<AccountsCore.EntryId> ids = new List<AccountsCore.EntryId>();
            _accounts.AddEntries(entries, ref ids);
        }

        public override bool CanGoForward()
        {
            if (_selectedAccountId == null) return false;
            return base.CanGoForward();
        }

		public void Initialize(QifDom dom, AccountsCore.Accounts accounts)
		{
			_dom = dom;
			_accounts = accounts;
		}

		private QifDom _dom;
		private AccountsCore.Accounts _accounts;
        private AccountsCore.AccountId _selectedAccountId;
		//= ((AccountsCore.AccountId)((TagString)this.lstCoreAccounts.SelectedItem).Id);

		private void lstCoreAccounts_SelectedIndexChanged(object sender, EventArgs e)
		{
			_selectedAccountId = ((AccountsCore.AccountId)((TagString)this.lstCoreAccounts.SelectedItem).Id);
		}
	}
}
