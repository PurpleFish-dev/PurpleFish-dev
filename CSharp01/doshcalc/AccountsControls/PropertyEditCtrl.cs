﻿using System;
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
	public partial class PropertyEditCtrl : UserControl
	{
		PropertyWR _item;
		PropertyId _id;
		Accounts _accounts;

		public PropertyEditCtrl(Accounts accounts)
		{		
			
			_accounts = accounts;
			InitializeComponent();
			SetItemId(null);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			SetItemId(null);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			_accounts.Property_Remove(_id);
			SetItemId(null);
		}

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if(_id != null)
			{
				this._accounts.Property_Replace(_id, _item);
			}
			else
			{
				this._accounts.AddProperty(_item);
			}			
			SetItemId(null);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			SetItemId(_id);
		}

		internal void SetItemId(PropertyId id)
		{
			_id = id;
			if(_id != null)
			{
				_item = new PropertyWR(this._accounts.Property(id));
				this.btnDelete.Enabled = this._accounts.PropertyCanRemove(id);
				this.btnEnter.Enabled =false;
				this.btnCancel.Enabled =false;
				this.btnAdd.Enabled = true;
				this.btnCancel.Visible = true;
				this.btnEnter.Text = "Modify";
			}
			else
			{
				_item = new PropertyWR();
				this.btnDelete.Enabled = false;
				this.btnEnter.Enabled =false;
				this.btnAdd.Enabled = false;
				this.btnCancel.Visible = false;
				this.btnEnter.Text = "Insert";
			}
			this.txtReceiptNo.TextChanged -= new System.EventHandler(this.txtReceiptNo_TextChanged);
			this.chkObsolete.CheckedChanged -= new System.EventHandler(this.chkObsolete_CheckedChanged);

			this.txtReceiptNo.Text = _item.Name;
			this.chkObsolete.Checked = _item.Obsolete;

			this.txtReceiptNo.TextChanged += new System.EventHandler(this.txtReceiptNo_TextChanged);
			this.chkObsolete.CheckedChanged += new System.EventHandler(this.chkObsolete_CheckedChanged);
		}

		private void catagoryLegalAndModified()
		{				
			if(_id != null)
			{				
				bool EnterEnabled = this._accounts.PropertyCanReplace(_id, _item);
				bool CancelEnabled = (_accounts.Property(_id) != _item);	
			
				this.btnEnter.Enabled = EnterEnabled;
				this.btnCancel.Enabled = CancelEnabled;
			}
			else 
			{
				if( this._accounts.PropertyCanAdd(_item) )
				{
					this.btnEnter.Enabled =true;
				}
				else
				{
					this.btnEnter.Enabled =false;
				}
			}
		}

		private void txtReceiptNo_TextChanged(object sender, EventArgs e)
		{			
			string name = ((TextBox)sender).Text;
			_item.Name = name;
			catagoryLegalAndModified();
		}

		private void chkObsolete_CheckedChanged(object sender, EventArgs e)
		{
			_item.Obsolete = this.chkObsolete.Checked;
			catagoryLegalAndModified();
		}
	}
}