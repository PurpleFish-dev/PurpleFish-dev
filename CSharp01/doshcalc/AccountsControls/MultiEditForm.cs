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
    public partial class frmMultiEdit : Form
    {
        Accounts _accounts;
        AccountId _idAccount;

        public CatagoryId CatagoryId;
        public AccountId TransferAccountId;
        public PropertyId PropertyId;
        public PayeeId PayeeId;

        public frmMultiEdit(Accounts accounts, AccountId idAccount, List<Entry> entries)
        {
            _accounts = accounts;
			_idAccount = idAccount;
            
            InitializeComponent();

            foreach (CatagoryId id in _accounts.CatagoryList.Keys)
                this.cboCatagory.Items.Add(new TagString(id, this._accounts.Catagory(id).Name));

            if(entries.All(x => x.CatagoryId == entries.First().CatagoryId))
                this.cboCatagory.SelectedItem = TagString.FindItem(this.cboCatagory, entries.First().CatagoryId);

            bool includeHiddenAccounts = false;
            foreach (KeyValuePair<AccountId, Account> kvp in _accounts.AccountList(includeHiddenAccounts))
            {
                if (kvp.Key != _idAccount)
                    this.cboCatagory.Items.Add(new TagString(kvp.Key, "Transfer to " + kvp.Value.Name));
            }

            //if ()

            foreach (PropertyId id in _accounts.PropertyList.Keys)
                this.cboProperty.Items.Add(new TagString(id, this._accounts.Property(id).Name));

            if (entries.All(x => x.PropertyId == entries.First().PropertyId))
                this.cboProperty.SelectedItem = TagString.FindItem(this.cboProperty, entries.First().PropertyId);

            foreach (PayeeId id in _accounts.PayeeList.Keys)
                this.cboPayee.Items.Add(new TagString(id, this._accounts.Payee(id).Name));

            if (entries.All(x => x.PayeeId == entries.First().PayeeId))
                this.cboPayee.SelectedItem = TagString.FindItem(this.cboPayee, entries.First().PayeeId);
        }

        private void frmMultiEdit_Load(object sender, EventArgs e)
        {

        }

        private void cboCatagory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Id id = ((TagString)cboCatagory.SelectedItem).Id;
            CatagoryId idCat = id as CatagoryId;
            if (idCat != null)
            {
                CatagoryId = idCat;
                //_item.TransferAccountId = AccountId.Empty();
                //_item.CatagoryId = idCat;
                //if ((!_item.CatagoryId.IsEmpty()) && (_accounts.Catagory(_item.CatagoryId).PropertySpecific))
                //{
                //    cboProperty.Enabled = true;
                //}
                //else
                //{
                //    cboProperty.Enabled = false;
                //}
                //legalAndModified();
                //return;
            }
            AccountId idTrans = id as AccountId;
            if (idTrans != null)
            {
                TransferAccountId = idTrans;
                
                
                //_item.TransferAccountId = idTrans;
                //_item.CatagoryId = CatagoryId.Empty();
                //cboProperty.Enabled = false;
                //legalAndModified();
                //return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void cboProperty_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Id id = ((TagString)cboProperty.SelectedItem).Id;
            PropertyId = id as PropertyId;
        }

        private void cboCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboPayee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Id id = ((TagString)cboPayee.SelectedItem).Id;
            PayeeId = id as PayeeId;
        }
    }
}
