using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;

namespace WindowsFormsApplication6
{
	public partial class ListEditFrm : Form
	{
		bool bEditMode =false;
		bool bCheckMode =false;
		private IdClassListWritable<Item> _list;
		
		public ListEditFrm(IdClassListWritable<Item> list)
		{
			InitializeComponent();
			_list = list;
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			foreach(Guid key in _list.Keys)
			{
				Item payee = _list[key];
				ListViewItem item = new ListViewItem(payee.Name);
				item.Tag = key;
				item.SubItems.Add(payee.Name);
				this.listView1.Items.Add(item);
			}
			ListViewItem blankItem = new ListViewItem("");			
			this.listView1.Items.Add(blankItem);
			blankItem.Focused =true;
			bEditMode =true;
			blankItem.BeginEdit();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			////Payee payee = new Payee();
			//Guid key =_accounts.PayeeList.Add(payee);
			//ListViewItem item = new ListViewItem(payee.Name);
			//item.Tag = key;
			//item.SubItems.Add(payee.Name);
			//this.listView1.Items.Add(item);
			//item.Focused =true;
			//bEditMode =true;
			//item.BeginEdit();
			////
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if(this.listView1.SelectedItems.Count >= 1)
			{
				ListViewItem item = this.listView1.SelectedItems[0];
				if(!string.IsNullOrWhiteSpace(item.Text))
				{
					if(item != null)
					{
						//if(this._list[(Guid)item.Tag].InUse == false)
						//{
						//	this._list.Remove( (Guid)item.Tag );
						//	this.listView1.Items.Remove(item);
						//}
						//else
						//{
							//int n=0;
						//}
					}
				}
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			bEditMode =true;
			ListViewItem item = ((ListView)sender).FocusedItem;
			item.BeginEdit();
		}

		private void listView1_Click(object sender, EventArgs e)
		{
			bEditMode =false;
		}

		private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			if(!bEditMode)
			{
//				e.CancelEdit = true;
			}
		}

		private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if(!this.bCheckMode)
			{
				e.NewValue = e.CurrentValue;
			}
		}

		private void listView1_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.X <= 20)
			{
				bCheckMode =true;
				bEditMode =false;
			}
			else
			{
				bCheckMode =false;
			}
			
			//Rectangle bounds = this.listView1.FocusedItem.Bounds;
		}

		private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			string Text = this.listView1.Items[e.Item].Text;
			if(string.IsNullOrWhiteSpace(Text))
			{
				e.CancelEdit =true;
				if(!string.IsNullOrWhiteSpace(e.Label))
				{					
					this.listView1.Items[e.Item].Text = e.Label;
					////Guid id = _list.Add(new Item(e.Label));
					///this.listView1.Items[e.Item].Tag = id;
				
					ListViewItem newitem = new ListViewItem("");
					//item.Tag = key;
					//item.SubItems.Add(payee.Name);
					this.listView1.Items.Add(newitem);
					newitem.Focused =true;
					bEditMode =true;

					this.listView1.Sort();				
				}				
			}
			else
			{
				foreach(ListViewItem item in this.listView1.Items)
				{
					if(item.Text == e.Label)
					{
						e.CancelEdit =true;
					}
					else
					{
						
					}					
				}				
			}
			
			
			
			
			
			

		}

		private void listView1_ItemActivate(object sender, EventArgs e)
		{

		}
	}
	
}
