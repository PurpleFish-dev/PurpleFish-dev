using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsControlLibrary1
{
	public partial class EntryEditCtrl : UserControl
	{		
		Accounts _accounts;
		//Entry _item;
		//EntryId _idEntry;
		AccountId _idAccount;
		Entry _entry;

		public EntryEditCtrl(Accounts accounts, AccountId idAccount)
		{		
			_accounts = accounts;
			_idAccount = idAccount;
			_entry = new Entry(_idAccount);

			InitializeComponent();

			SetItemId();

			_accounts.CatagoriesChanged += OnCatagoriesChanged;
			_accounts.PayeesChanged += OnPayeesChanged;
			_accounts.PropertiesChanged += OnPropertiesChanged;
			_accounts.AccountsChanged += OnAccountsChanged;

			refreshCatagoriesCbo();
			refreshPayeesCbo();
			refreshPropertiesCbo();
		}

		private void OnCatagoriesChanged(object sender) { refreshCatagoriesCbo(); }
		private void OnPayeesChanged(object sender) { refreshPayeesCbo(); }
		private void OnPropertiesChanged(object sender) { refreshPropertiesCbo(); }
		private void OnAccountsChanged(object sender) { refreshCatagoriesCbo(); }

		private void refreshCatagoriesCbo()
		{
			this.cboCatagory.Items.Clear();
			foreach(CatagoryId id in _accounts.CatagoryList.Keys)
			{
				this.cboCatagory.Items.Add(new TagString(id, this._accounts.Catagory(id).Name));
			}

			bool includeHiddenAccounts = false;
			foreach(KeyValuePair<AccountId, Account> kvp in _accounts.AccountList(includeHiddenAccounts))
			{
				if(kvp.Key != _idAccount)
				{
					this.cboCatagory.Items.Add(new TagString(kvp.Key, "Transfer to " + kvp.Value.Name));
				}
			}
		}

		private void refreshPayeesCbo()
		{
			this.cboPayee.Items.Clear();
			//this.cboPayee.Items.Add(new TagString(PayeeId.Empty(), " - "));
			if(_accounts.PayeeList != null)
			{
				foreach(PayeeId id in _accounts.PayeeList.Keys)
				{
					this.cboPayee.Items.Add(new TagString(id, this._accounts.Payee(id).Name));
                    this.cboFilterPayee.Items.Add(new TagString(id, this._accounts.Payee(id).Name)); 
                }
			}
		}

		private void refreshPropertiesCbo()
		{
			this.cboProperty.Items.Clear();
			//this.cboProperty.Items.Add(new TagString(PropertyId.Empty(), " - "));
			foreach(KeyValuePair<PropertyId, Property> kvp in _accounts.PropertyList)
			{
				this.cboProperty.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
			}
		}
		
		private void btnAdd_Click(object sender, EventArgs e)
		{
			_entry = new Entry(_idAccount);
			SetItemId();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			this._accounts.Entry_Remove(_entry.Id);
			_entry = new Entry(_idAccount);
			SetItemId();
        }

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if(_entry.Id != null)
			{				
				_accounts.EntryReplace(_entry);
            }
			else
			{
				EntryId id = _accounts.AddEntry(_entry);
				_entry = _accounts.Entry(id);
			}
			SetItemId();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            //if (_idEntry != null)
            //{
            //    SetItemId(_accounts.AddEntry(_item));
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
		{
			SetItemId();
		}

		public void SetEntry(Entry entry)
		{
			if (entry == null) _entry = new Entry(_idAccount);
			else _entry = entry;
			SetItemId();
		}
		private void SetItemId()
		{
			if(_entry.Id != null)
			{
				this.btnDelete.Enabled = true;
				this.btnEnter.Enabled =false;
				this.btnCancel.Enabled =false;
				this.btnAdd.Enabled = true;
				this.btnCancel.Visible = true;
				this.btnEnter.Text = "Modify";
                this.btnOpenFileAssoc.Enabled = true;
            }
			else
			{
				this.btnDelete.Enabled = false;
				this.btnEnter.Enabled =false;
				this.btnAdd.Enabled = false;
				this.btnCancel.Visible = false;
				this.btnEnter.Text = "Insert";
                this.btnOpenFileAssoc.Enabled = false;
            }
			
			this.txtReceiptNo.TextChanged -= new System.EventHandler(this.txtReceiptNo_TextChanged);
			this.cboProperty.SelectionChangeCommitted -= new System.EventHandler(this.cboProperty_SelectionChangeCommitted);
			this.txtAmount.TextChanged -= new System.EventHandler(this.txtAmount_TextChanged);
			this.rdoDebit.CheckedChanged -= new System.EventHandler(this.txtAmount_TextChanged);
			this.rdoCredit.CheckedChanged -= new System.EventHandler(this.txtAmount_TextChanged);
			this.cboCatagory.SelectionChangeCommitted -= new System.EventHandler(this.cboCatagory_SelectionChangeCommitted);
			this.cboProperty.SelectionChangeCommitted -= new System.EventHandler(this.cboProperty_SelectionChangeCommitted);
			this.txtDescription.TextChanged -= new System.EventHandler(this.txtDescription_TextChanged);
			this.ctrlDate.ValueChanged -= new System.EventHandler(this.ctrlDate_DateChanged);			
			
			this.txtDescription.Text = _entry.Description;
			//this.txtReceiptNo.Text = _item.RecieptNo;
			if(_entry.GetAmount(_idAccount) >= 0.0M)
			{
				this.txtAmount.Text = _entry.GetAmount(_idAccount).ToString();
				rdoCredit.Checked = true;
			}
			else
			{
				this.txtAmount.Text = (decimal.Negate(_entry.GetAmount(_idAccount))).ToString();
				rdoDebit.Checked = true;
			}
			
			this.cboPayee.SelectedItem = TagString.FindItem(this.cboPayee, _entry.PayeeId);			
			this.cboProperty.SelectedItem = TagString.FindItem(this.cboProperty, _entry.PropertyId);           

            AccountId transId = _entry.GetTransferAccountId(_idAccount);
            this.cboCatagory.SelectedItem = TagString.FindItem(this.cboCatagory, (transId == null ? (Id)_entry.CatagoryId : (Id)transId));

            this.ctrlDate.Value = _entry.Date;
			this.txtReceiptNo.TextChanged += new System.EventHandler(this.txtReceiptNo_TextChanged);
			this.cboProperty.SelectionChangeCommitted += new System.EventHandler(this.cboProperty_SelectionChangeCommitted);
			this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
			this.rdoDebit.CheckedChanged += new System.EventHandler(this.txtAmount_TextChanged);
			this.rdoCredit.CheckedChanged += new System.EventHandler(this.txtAmount_TextChanged);
			this.cboCatagory.SelectionChangeCommitted += new System.EventHandler(this.cboCatagory_SelectionChangeCommitted);
			this.cboProperty.SelectionChangeCommitted += new System.EventHandler(this.cboProperty_SelectionChangeCommitted);
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			this.ctrlDate.ValueChanged += new System.EventHandler(this.ctrlDate_DateChanged);
		}

		private void legalAndModified()
		{				
			if(_entry.Id != null)
			{
				//this.btnOpenFileAssoc.Enabled = true;
				bool EnterEnabled = this._accounts.EntryCanReplace(_entry);
				bool CancelEnabled = (_accounts.Entry(_entry.Id) != _entry);	
			
				this.btnEnter.Enabled = EnterEnabled;
                this.button1.Enabled = EnterEnabled;
                this.btnCancel.Enabled = CancelEnabled;
			}
			else 
			{
                //this.btnOpenFileAssoc.Enabled = false;
                string reason ="";
                if ( this._accounts.EntryCanAdd(_entry, ref reason) )
				{
					this.btnEnter.Enabled =true;
                    this.lblReason.Text = "";
                }
				else
				{
					this.btnEnter.Enabled =false;
                    this.lblReason.Text = reason;
                }
			}
		}
		
		private void txtReceiptNo_TextChanged(object sender, EventArgs e)
		{
			//_item.RecieptNo = this.txtReceiptNo.Text;
			//legalAndModified();
		}
		
		private void cboProperty_SelectionChangeCommitted(object sender, EventArgs e)
		{
			_entry.PropertyId = (PropertyId)((TagString)cboProperty.SelectedItem).Id;			
			legalAndModified();
		}

		private void txtAmount_TextChanged(object sender, EventArgs e)
		{		
			decimal decValue;
			if( decimal.TryParse(txtAmount.Text, out decValue) )
			{
				_entry.SetAmount(_idAccount, (rdoCredit.Checked ? decValue : - decValue));
			}			
			legalAndModified();
		}
		
		private void ctrlDate_DateChanged(object sender, EventArgs e)
		{
            this._entry.Date = this.ctrlDate.Value;
			legalAndModified();
		}

		private void txtDescription_TextChanged(object sender, EventArgs e)
		{
			_entry.Description = this.txtDescription.Text;
			legalAndModified();
		}

		private void cboPayee_SelectionChangeCommitted(object sender, EventArgs e)
		{		
			this._entry.PayeeId = (PayeeId)((TagString)this.cboPayee.SelectedItem).Id;
			legalAndModified();
		}

		private void cboCatagory_SelectionChangeCommitted(object sender, EventArgs e)
		{		
			Id id = ((TagString)cboCatagory.SelectedItem).Id;
			CatagoryId idCat = id as CatagoryId;
			if(idCat != null)
			{
				_entry.SetEntry(_idAccount, idCat);
				if( (_entry.CatagoryId != null) && (_accounts.Catagory(_entry.CatagoryId).PropertySpecific) )
				{
					cboProperty.Enabled = true;
				}
				else
				{
					cboProperty.Enabled = false;
				}
				legalAndModified();
				return;
			}
			AccountId idTrans = id as AccountId;
			if(idTrans != null)
			{
				_entry.SetTransfer(_idAccount, idTrans);
				cboProperty.Enabled = false;
				legalAndModified();
				return;
			}			
			
			Debug.Assert(false);
			return;			
		}

        private void cboCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        internal void SetItemId(List<EntryId> entries)
        {
            //throw new NotImplementedException();
        }

        private void btnOpenFileAssoc_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string PATH = openFileDialog1.FileName;
            }




            //if (this._idEntry != null)
            //{
            //    string ext = Path.GetExtension(file);
            //    string path = @"F:\statements\LinkedDocs\" + this._idEntry.GetGuid().ToString() + ext;
            //    File.Move(file, path);


            //    Entry tmp = new Entry(_accounts.Entry(this._idEntry));  //danger Entry should be readonly
            //    tmp.RecieptNo = path;
            //    _item.RecieptNo = path;
            //    _accounts.EntryReplace(_idEntry, tmp);
            //}
            
            //if ( (this.txtReceiptNo.Text != null) && (this.txtReceiptNo.Text != ""))
             //{
             //    string ext = Path.GetExtension(this.txtReceiptNo.Text);
             //    string path = @"F:\statements\LinkedDocs\" + this._idEntry.GetGuid().ToString() + ext;
             //    File.Move(this.txtReceiptNo.Text, path);
             //    this._item.RecieptNo = path;

            //    legalAndModified();
            //    _accounts.EntryReplace(_idEntry, _item);
            //    this.txtReceiptNo.Text = "";
            //}


            //if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    this._item.RecieptNo = openFileDialog1.FileName;
            //    legalAndModified();
            //}

        }

        private void EntryEditCtrl_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
            {
                string file = files.First(); //select the first one  

                if (this._entry.Id != null)
                {
                    string ext = Path.GetExtension(file);
                    string path = @"F:\statements\LinkedDocs\" + this._entry.Id.GetGuid().ToString() + ext;
                    File.Move(file, path);            


                    Entry tmp = _accounts.Entry(this._entry.Id);
                    tmp.RecieptNo = path;
					_entry.RecieptNo = path;
					_accounts.EntryReplace(tmp);
               }
            }
        }
        

        private void EntryEditCtrl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            e.Effect = DragDropEffects.Copy;
        }

        private void EntryEditCtrl_Load(object sender, EventArgs e)
        {

        }

        private void EntryEditCtrl_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            e.Effect = DragDropEffects.Copy;
        }

        private void EntryEditCtrl_DragLeave(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void txtMinAmountFilter_TextChanged(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void refreshFilters()
        {
            Filter filter = this._accounts.GetFilter(this._idAccount);

            decimal decValue;
            if (decimal.TryParse(txtMinAmountFilter.Text, out decValue))
                filter.AmountFilter(this.chkFilterAmount.Checked, decValue);

            filter.LinkFilter(this.chkFilterLink.Checked, rdoLinked.Checked);

            PayeeId payeeId = (PayeeId)((TagString)this.cboFilterPayee.SelectedItem).Id;
            filter.PayeeFilter(this.chkFilterPayee.Checked && (payeeId != null), payeeId);

            this._accounts.SetFilter(this._idAccount, filter);


            SetItemId();
        }


        private void Refresh_Click(object sender, EventArgs e)
        {
            
        }

        private void chkFilterLink_CheckedChanged(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void rdoNoLink_CheckedChanged(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void rdoLinked_CheckedChanged(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void cboFilterPayee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void chkFilterPayee_CheckedChanged(object sender, EventArgs e)
        {
            refreshFilters();
        }
    }
}
