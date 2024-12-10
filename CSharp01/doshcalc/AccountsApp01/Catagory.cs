using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication6
{
	public partial class CatagoryFrm : Form
	{
		/*
		 * private Accounts _accounts;	
		private Catagory _catagory;	
		public Catagory Catagory
		{
			get { return _catagory; }
			set { _catagory = value; }
		}

		public CatagoryFrm(Accounts accounts)
		{
			InitializeComponent();
			_accounts = accounts;
			_catagory = new Catagory();
		}

		public CatagoryFrm(Accounts accounts, Guid catId)
		{
			InitializeComponent();
			_accounts = accounts;
			_catagory = new Catagory(accounts.CatagoryList[catId]);
		}
		
		private void Catagory_Load(object sender, EventArgs e)
		{
			this.cboTaxCode.Items.Clear();
			foreach(Guid id in this._accounts.TaxCodeList.Keys)
			{
				this.cboTaxCode.Items.Add(new TagString(id, this._accounts.TaxCodeList[id].Name));
			}

			this.txtName.Text = this._catagory.Name;			
			this.cboTaxCode.SelectedItem = TagString.FindItem(this.cboTaxCode, _catagory.TaxCodeId);
			this.rdoIncome.Checked = this._catagory.Income;
			this.rdoExpense.Checked = !this._catagory.Income;
			this.chkAssignedToProperty.Checked = this._catagory.PropertySpecific;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_catagory.Name = this.txtName.Text;
			_catagory.TaxCodeId = ((TagString)this.cboTaxCode.SelectedItem).Id;
			_catagory.Income = this.rdoIncome.Checked;
			_catagory.PropertySpecific = this.chkAssignedToProperty.Checked;
			this.Close();
		}
		 * */
	}
}
