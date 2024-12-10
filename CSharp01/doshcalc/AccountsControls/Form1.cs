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
    public partial class ScheduledTransactionsDlg : Form
    {
        public ScheduledTransactionsDlg(Accounts accounts, AccountId account, List<EntryId> ids)
        {
            InitializeComponent();

            _accounts = accounts;
            _account = account;

            foreach (CatagoryId id in accounts.CatagoryList.Keys)
                cboCatagory.Items.Add(new TagString(id, accounts.Catagory(id).Name));
            if(cboCatagory.Items.Count > 0) cboCatagory.SelectedIndex = 0;

            bool includeHiddenAccounts = false;
            foreach (KeyValuePair<AccountId, Account> kvp in accounts.AccountList(includeHiddenAccounts))
            {
                cboFromAccount.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
                cboToAccount.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
                cboAccount.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
            }

            foreach (PayeeId id in accounts.PayeeList.Keys)
                cboPayee.Items.Add(new TagString(id, accounts.Payee(id).Name));
            if (cboPayee.Items.Count > 0) cboPayee.SelectedIndex = 0;

            foreach (KeyValuePair<PropertyId, Property> kvp in accounts.PropertyList)
                cboProperty.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
            if (cboProperty.Items.Count > 0) cboProperty.SelectedIndex = 0;

            if (ids.Count != 0)
            {
                Entry template = _accounts.Entry(ids[0]);
                this.dtpStartDate.Value = template.Date;
                if (rdoMonth.Checked) this.dtpStartDate.Value = this.dtpStartDate.Value.AddMonths(1);
                if (rdoWeek.Checked) this.dtpStartDate.Value = this.dtpStartDate.Value.AddDays(7);
                if (rdoQuarter.Checked) this.dtpStartDate.Value = this.dtpStartDate.Value.AddMonths(3);
                this.txtDescription.Text = template.Description;

                if (template.IsTransfer() == false)
                {
                    if (template.CatagoryId != null)
                        this.cboCatagory.SelectedItem = TagString.FindItem(cboCatagory, template.CatagoryId);
                    this.cboAccount.SelectedItem = TagString.FindItem(cboAccount, account);
                    if (template.GetAmount(account) < 0)
                    {
                        this.rdoDebit.Checked = true;
                        this.dtbTransactionAmount.Text = decimal.Negate(template.GetAmount(account)).ToString();
                    }
                    else
                    {
                        this.rdoCredit.Checked = true;
                        this.dtbTransactionAmount.Text = template.GetAmount(account).ToString();
                    }
                }
                else
                {
                    this.rdoTransfer.Checked = true;                    
                    if (template.GetAmount(account) < 0)
                    {
                        this.cboFromAccount.SelectedItem = TagString.FindItem(cboFromAccount, account);
                        this.cboToAccount.SelectedItem = TagString.FindItem(cboToAccount, template.GetTransferAccountId(account));
                        this.dtbTransferAmount.Text = decimal.Negate(template.GetAmount(account)).ToString();
                    }
                    else
                    {
                        this.cboFromAccount.SelectedItem = TagString.FindItem(cboFromAccount, template.GetTransferAccountId(account));
                        this.cboToAccount.SelectedItem = TagString.FindItem(cboToAccount, account);
                        this.dtbTransferAmount.Text = template.GetAmount(account).ToString();
                    }
                }
            }
            
            //cboFromAccount.Items.Add(new TagString(PayeeId.Empty(), " - "));
            //cboFromAccount.SelectedIndex = 0;



            //foreach (KeyValuePair<AccountId, Account> kvp in accounts.AccountList(includeHiddenAccounts))
            //    this.cboFromAccount.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
           

            //var reasons = new List<string>();
           //if (_accounts.EntryCanAdd(entries, ref reasons))
            //{
                IEnumerable<EntryId> ids = new List<EntryId>();
                _accounts.AddEntries(create(), ref ids);
            //}

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void rdoNoOfTransactions_CheckedChanged(object sender, EventArgs e)
        {
            dtbNoOfTransactions.Enabled = rdoNoOfTransactions.Checked;
            dtpEndDate.Enabled = !rdoNoOfTransactions.Checked;
        }

        private void cboFromAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboToAccount.SelectedIndex == cboFromAccount.SelectedIndex)
                cboToAccount.SelectedIndex = -1;
        }

        private void rdoTransfer_CheckedChanged(object sender, EventArgs e)
        {
            rdoTransaction.Checked = !rdoTransfer.Checked;
            pnlTransfer.Enabled = rdoTransfer.Checked;
        }

        private void rdoTransaction_CheckedChanged(object sender, EventArgs e)
        {
            rdoTransfer.Checked = !rdoTransaction.Checked;
            pnlTransaction.Enabled = rdoTransaction.Checked;
        }

        private Accounts _accounts;
        private AccountId _account;

        private void cboToAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFromAccount.SelectedIndex == cboToAccount.SelectedIndex)
                cboFromAccount.SelectedIndex = -1;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAccount.SelectedItem == null) return;

            var reasons = new List<string>();
            btnEnter.Enabled = _accounts.EntryCanAdd(create(), ref reasons);

            StringBuilder builder = new StringBuilder();
            foreach (string reason in reasons)
            {
                // Append each int to the StringBuilder overload.
                builder.Append(reason).Append("\n");
            }
            string result = builder.ToString();
            lblReasons.Text = result;

        }

        private List<Entry> create()
        {
            var entries = new List<Entry>();
            decimal decValue = 0;
            decimal.TryParse(dtbNoOfTransactions.Text, out decValue);
            DateTime date = dtpStartDate.Value;

            AccountId accountId = rdoTransaction.Checked ? (AccountId)((TagString)cboAccount.SelectedItem).Id : (AccountId)((TagString)cboToAccount.SelectedItem).Id;

            if (rdoNoOfTransactions.Checked)
            {
                for (int i = 0; i < decValue; ++i)
                {
                    var entry = new Entry(accountId);
                    entry.Date = date;
                    entry.Description = txtDescription.Text;
                    if (rdoTransaction.Checked)
                    {
                        decimal amount = 0;
                        decimal.TryParse(dtbTransactionAmount.Text, out amount);
                        entry.SetAmount(accountId, rdoCredit.Checked ? amount : decimal.Negate(amount));
                        CatagoryId catId = (cboCatagory.SelectedItem == null) ? null : (CatagoryId)((TagString)cboCatagory.SelectedItem).Id;
                        entry.SetEntry(accountId, catId);
                    }
                    else
                    {                        
                        decimal amount = 0;
                        decimal.TryParse(dtbTransferAmount.Text, out amount);
                        entry.SetAmount(accountId, amount);
                        entry.SetTransfer(accountId, (AccountId)((TagString)cboFromAccount.SelectedItem).Id);
                    }
                    entries.Add(entry);

                    if (rdoMonth.Checked) date = date.AddMonths(1);
                    if (rdoWeek.Checked) date = date.AddDays(7);
                    if (rdoQuarter.Checked) date = date.AddMonths(3);
                }
            }
            else
            {
            }
            return entries;
        }

        private void lblReasons_Click(object sender, EventArgs e)
        {

        }

        private void dtbTransferAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
