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
	public partial class CatagoryEditCtrl : UserControl
	{
		Accounts _accounts;
		CatagoryWR _item;
		CatagoryId _id;

		public CatagoryEditCtrl(Accounts accounts)
		{		
			_accounts = accounts;
			InitializeComponent();

			this.cboTaxCode.Items.Clear();
			bool includeObsoleteItems = false;
			foreach(KeyValuePair<TaxCodeId, TaxCode> kvp in _accounts.TaxCodeList(includeObsoleteItems))
			{
				this.cboTaxCode.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
			}
			_accounts.TaxCodesChanged += OnTaxCodesChanged;

			SetItemId(null);
		}

		private void OnTaxCodesChanged(object sender)
		{
			this.cboTaxCode.Items.Clear();
			bool includeObsoleteItems = false;
			foreach(KeyValuePair<TaxCodeId, TaxCode> kvp in _accounts.TaxCodeList(includeObsoleteItems))
			{
				this.cboTaxCode.Items.Add(new TagString(kvp.Key, kvp.Value.Name));
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			SetItemId(null);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			this._accounts.Category_Remove(_id);
			SetItemId(null);
		}

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if(_id != null)
			{
				this._accounts.Catagory_Replace(_id, _item);
			}
			else
			{
				this._accounts.AddCatagory(_item);
			}			
			SetItemId(null);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			SetItemId(_id);
		}
				
		internal void SetItemId(CatagoryId id)
		{
			_id = id;
			if(_id != null)
			{
                _item = new CatagoryWR(this._accounts.Catagory(id));
				this.btnDelete.Enabled = this._accounts.CatagoryCanRemove(id);
				this.btnEnter.Enabled =false;
				this.btnCancel.Enabled =false;
				this.btnAdd.Enabled = true;
				this.btnCancel.Visible = true;
				this.btnEnter.Text = "Modify";
			}
			else
			{
                _item = new CatagoryWR();
				this.btnDelete.Enabled = false;
				this.btnEnter.Enabled =false;
				this.btnAdd.Enabled = false;
				this.btnCancel.Visible = false;
				this.btnEnter.Text = "Insert";
			}
			this.chkAssignedToProperty.CheckedChanged -= new System.EventHandler(this.chkAssignedToProperty_CheckedChanged);
			this.rdoIncome.CheckedChanged -= new System.EventHandler(this.rdoIncome_CheckedChanged);
			this.txtName.TextChanged -= new System.EventHandler(this.txtName_TextChanged);
			this.cboTaxCode.SelectionChangeCommitted -= new System.EventHandler(this.cboTaxCode_SelectionChangeCommitted);
			this.chkObsolete.CheckedChanged -= new System.EventHandler(this.chkObsolete_CheckedChanged);

			this.txtName.Text = _item.Name;
			this.cboTaxCode.SelectedItem = TagString.FindItem(this.cboTaxCode, _item.TaxCodeId);//SelectedItem.
			this.rdoExpense.Checked = _item.Expense;
			this.rdoIncome.Checked = _item.Income;
			this.chkAssignedToProperty.Checked = _item.PropertySpecific;

			this.chkAssignedToProperty.CheckedChanged += new System.EventHandler(this.chkAssignedToProperty_CheckedChanged);
			this.rdoIncome.CheckedChanged += new System.EventHandler(this.rdoIncome_CheckedChanged);
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			this.cboTaxCode.SelectionChangeCommitted += new System.EventHandler(this.cboTaxCode_SelectionChangeCommitted);
			this.chkObsolete.CheckedChanged += new System.EventHandler(this.chkObsolete_CheckedChanged);			
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{			
			string name = ((TextBox)sender).Text;
			_item.Name = name;			
			legalAndModified();
		}

		private void cboTaxCode_SelectionChangeCommitted(object sender, EventArgs e)
		{
			_item.TaxCodeId = ((TaxCodeId)((TagString)cboTaxCode.SelectedItem).Id);
			legalAndModified();
		}

		private void legalAndModified()
		{				
			if(_id != null)
			{				
				bool EnterEnabled = this._accounts.Catagory_CanReplace(_id, _item);
				bool CancelEnabled = (_accounts.Catagory(_id) != _item);	
			
				this.btnEnter.Enabled = EnterEnabled;
				this.btnCancel.Enabled = CancelEnabled;
			}
			else 
			{
				if( this._accounts.CatagoryCanAdd(_item) )
				{
					this.btnEnter.Enabled =true;
				}
				else
				{
					this.btnEnter.Enabled =false;
				}
			}
		}

		private void chkAssignedToProperty_CheckedChanged(object sender, EventArgs e)
		{
			_item.PropertySpecific = ((CheckBox)sender).Checked;
			legalAndModified();
		}

		private void rdoIncome_CheckedChanged(object sender, EventArgs e)
		{
			_item.Income = ((RadioButton)sender).Checked;
			legalAndModified();
		}

		private void chkObsolete_CheckedChanged(object sender, EventArgs e)
		{
			_item.Obsolete = this.chkObsolete.Checked;
			legalAndModified();
		}		
	}
}
