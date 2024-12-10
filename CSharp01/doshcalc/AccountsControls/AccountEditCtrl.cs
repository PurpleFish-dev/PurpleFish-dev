using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;
using System.Diagnostics;

namespace WindowsFormsControlLibrary1
{
	public partial class AccountEditCtrl : UserControl
	{
		Account _account;
		AccountId _id;
		Accounts _accounts;

		public AccountEditCtrl(Accounts accounts)
		{		
			_accounts = accounts;
			InitializeComponent();
			
			this.cboType.Items.Add(Account.eType.Credit);
			this.cboType.Items.Add(Account.eType.Liability);
			SetItemId(null);			
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
            SetItemId(null);
        }

		private void btnDelete_Click(object sender, EventArgs e)
		{
			_accounts.Account_Remove(_id);
            SetItemId(null);
        }

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if(_id != null)
			{
				this._accounts.Account_Replace(_id, _account);
			}
			else
			{
				_id = this._accounts.AddAccount(_account);
			}			
			SetItemId(_id/*AccountId.Empty()*/);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			SetItemId(_id);
		}

		internal void SetItemId(AccountId id)
		{
			_id = id;
			if(_id != null)
			{				
				_account = new Account(this._accounts.Account(_id));
				this.btnDelete.Enabled = this._accounts.Account_CanRemove(id);
				this.btnEnter.Enabled =false;
				this.btnCancel.Enabled =false;
				this.btnAdd.Enabled = true;
				this.btnCancel.Visible = true;
				this.btnEnter.Text = "Modify";
			}
			else
			{
				_account = new Account();
				this.btnDelete.Enabled = false;
				this.btnEnter.Enabled =false;
				this.btnAdd.Enabled = false;
				this.btnCancel.Visible = false;
				this.btnEnter.Text = "Insert";
			}

			this.dtpReconciledOn.ValueChanged -= new System.EventHandler(this.dtpReconciledOn_ValueChanged);
			this.dtpLockedUntil.ValueChanged -= new System.EventHandler(this.dtpLockedUntil_ValueChanged);
			this.rdoLockedUntil.CheckedChanged -= new System.EventHandler(this.lock_CheckedChanged);
			this.rdoLocked.CheckedChanged -= new System.EventHandler(this.lock_CheckedChanged);
			this.rdoUnLocked.CheckedChanged -= new System.EventHandler(this.lock_CheckedChanged);
			this.chkHidden.CheckedChanged -= new System.EventHandler(this.chkHidden_CheckedChanged);
			this.chkExternal.CheckedChanged -= new System.EventHandler(this.chkExternal_CheckedChanged);
			this.cboType.SelectedIndexChanged -= new System.EventHandler(this.cboType_SelectedIndexChanged);
			this.txtName.TextChanged -= new System.EventHandler(this.txtName_TextChanged);	

			this.txtName.Text = _account.Name;
			this.chkExternal.Checked = _account.External;
			this.chkHidden.Checked = _account.Hidden;
			this.rdoLocked.Checked = (_account.Lock == Account.eLock.Locked);
			this.rdoLockedUntil.Checked = (_account.Lock == Account.eLock.ByDate);
			this.rdoUnLocked.Checked = (_account.Lock == Account.eLock.Open);
			this.dtpLockedUntil.Value = _account.LockedUntil;
			this.dtpReconciledOn.Value = _account.ReconciledOn;
			if(_account.Type == Account.eType.Credit)
			{
				this.cboType.SelectedIndex =0;
			}
			else if(_account.Type == Account.eType.Liability)
			{
				this.cboType.SelectedIndex =1;
			}
			else
			{
				Debug.Assert(false);
			}

			this.dtpReconciledOn.ValueChanged += new System.EventHandler(this.dtpReconciledOn_ValueChanged);
			this.dtpLockedUntil.ValueChanged += new System.EventHandler(this.dtpLockedUntil_ValueChanged);
			this.rdoLockedUntil.CheckedChanged += new System.EventHandler(this.lock_CheckedChanged);
			this.rdoLocked.CheckedChanged += new System.EventHandler(this.lock_CheckedChanged);
			this.rdoUnLocked.CheckedChanged += new System.EventHandler(this.lock_CheckedChanged);
			this.chkHidden.CheckedChanged += new System.EventHandler(this.chkHidden_CheckedChanged);
			this.chkExternal.CheckedChanged += new System.EventHandler(this.chkExternal_CheckedChanged);
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);	
		}

		private void legalAndModified()
		{
			if (_id != null)
			{								
				bool EnterEnabled = this._accounts.Account_CanReplace(_id, _account);
				bool CancelEnabled = (_accounts.Account(_id) != _account);	
			
				this.btnEnter.Enabled = EnterEnabled;
				this.btnCancel.Enabled = CancelEnabled;
			}
			else 
			{
				if( this._accounts.AccountCanAdd(_account) )
				{
					this.btnEnter.Enabled =true;
				}
				else
				{
					this.btnEnter.Enabled =false;
				}
			}
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{			
			string name = ((TextBox)sender).Text;
			_account.Name = name;
			legalAndModified();
		}

		private void cboType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(this.cboType.SelectedItem != null)
			{
				this._account.Type = (Account.eType)this.cboType.SelectedItem;
			}
			legalAndModified();
		}

			private void chkExternal_CheckedChanged(object sender, EventArgs e)
		{
			_account.External = this.chkExternal.Checked;
			legalAndModified();
		}

		private void chkHidden_CheckedChanged(object sender, EventArgs e)
		{
			_account.Hidden = this.chkHidden.Checked;
			legalAndModified();
		}

		private void lock_CheckedChanged(object sender, EventArgs e)
		{
			if(this.rdoLocked.Checked == true)
			{
				_account.Lock = Account.eLock.Locked;
			}
			else if(this.rdoUnLocked.Checked == true)
			{
				_account.Lock = Account.eLock.Open;
			}
			else
			{
				_account.Lock = Account.eLock.ByDate;
			}
			legalAndModified();
		}		

		private void dtpLockedUntil_ValueChanged(object sender, EventArgs e)
		{
			_account.LockedUntil = this.dtpLockedUntil.Value;
			legalAndModified();
		}

		private void dtpReconciledOn_ValueChanged(object sender, EventArgs e)
		{
			_account.ReconciledOn = this.dtpReconciledOn.Value;
			legalAndModified();
		}
	}
}
