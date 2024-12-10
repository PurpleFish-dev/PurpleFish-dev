using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;

namespace WindowsFormsControlLibrary1
{
    public partial class ReconcileFrm : Form
    {
        private Accounts accounts;
        private AccountId accountId;

        public ReconcileFrm(Accounts accounts, AccountId accountId)
        {
            InitializeComponent();

            this.accounts = accounts;
            this.accountId = accountId;
            this.reconcileListView1.Initialize(accounts, accountId);
            this.reconcileListView1.RefreshItems();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.accounts.Reconcile_End();
            this.reconcileListView1.RefreshItems();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}