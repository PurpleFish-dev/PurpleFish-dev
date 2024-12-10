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
using AccountsCore;

namespace WindowsFormsControlLibrary1
{
	public partial class QifItemSelectionCtrl : WizardControl
	{
		public QifItemSelectionCtrl()
		{
			InitializeComponent();
		}

		public override bool OnEnter()
		{
            //foreach(QifApi.Transactions.Account account in _dom.Accounts)
            //{
            //    this.listView1.Items.Add(account.AccountListTransaction.Name);
            //}
			return true;

		}

        public override bool CanGoForward()
        {
            bool AtLeastOneAccountSelected = false;
            foreach (ListViewItem item in listView1.Items)
            {
                if (!AtLeastOneAccountSelected)
                {
                    AtLeastOneAccountSelected = item.Checked;
                }
            }
            if (!AtLeastOneAccountSelected) return false; 
            return base.CanGoForward();
        }

        public override void OnLeave()
		{
			foreach(ListViewItem item in listView1.Items)
			{
				 QifApi.Transactions.Account account = dom.Accounts.Find(i => i.AccountListTransaction.Name == item.Text);
				 account.AccountListTransaction.Selected = item.Checked;				 
			}
		}

		public void Initialize(QifDom dom, Accounts accounts)
        {
			this.dom = dom;
            this.accounts = accounts;
            foreach (QifApi.Transactions.Account account in dom.Accounts)
                this.listView1.Items.Add(account.AccountListTransaction.Name);
            IdClassListReadOnly<AccountId, Account> accs = accounts.AccountList(false);

        }

		private QifDom dom;
        private AccountsCore.Accounts accounts;

    }
}
