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
	public partial class ItemEditCtrl : UserControl
	{
		private ItemListView _factoryCtrl;
		IdClassListWritable<Item> _list;
		Item _item;
		Identifier _id;
		bool _editMode;

		public ItemEditCtrl(ItemListView factoryCtrl, IdClassListWritable<Item> list)
		{		
			_factoryCtrl = factoryCtrl;
			_list = list;

			InitializeComponent();

			SetItemId(0, false);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			this.setEditMode(true);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			this._list.Remove(_id);
			SetItemId(0, false);
			this._factoryCtrl.RefreshItems();
		}

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if(this._editMode == true)
			{
				this._list[_id] = _item;
				this._factoryCtrl.RefreshItems();
			}
			else
			{
				this._list.Add(_item);
				this._factoryCtrl.RefreshItems();
			}			
			SetItemId(0, false);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			SetItemId(_id, false);
			
			
			//setEditMode(false);
			//_item = this._list[_id];
			//this.txtReceiptNo.Text = _item.Name;
		}

		internal void SetItemId(Identifier id, bool editMode)
		{
			this.btnEnter.Enabled =false;
			if(id != 0)
			{
				_id = id;
				_item = new Item(this._list[id]);
				this.btnDelete.Enabled = true;
				this.btnEdit.Enabled = true;
				setEditMode(editMode);
			}
			else
			{
				_id = 0;
				_item = new Item();
				this.btnDelete.Enabled = false;
				this.btnEdit.Enabled = false;
				setEditMode(false);
			}
			this.txtReceiptNo.Text = _item.Name;			
		}

		private void setEditMode(bool editMode)
		{
			_editMode = editMode;
			if(_editMode == true)
			{
				Debug.Assert(_id > 0); //UI should not allow selection of edit mode
				this.btnCancel.Visible = true;
				this.btnEnter.Text = "Modify";
			}
			else{
				this.btnCancel.Visible = false;
				this.btnEnter.Text = "Insert";
			}
		}

		private void txtReceiptNo_TextChanged(object sender, EventArgs e)
		{			
			/*string name = ((TextBox)sender).Text;
			if( this._list.CanAdd(name) )
			{
				this.btnEnter.Enabled =true;
				_item.Name = name;
			}
			else
			{
				this.btnEnter.Enabled =false;
			}
			 * */
			 //ttemp
		}
	}
}
