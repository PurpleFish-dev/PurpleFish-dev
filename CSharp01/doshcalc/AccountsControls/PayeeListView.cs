using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;

namespace WindowsFormsControlLibrary1
{
	public partial class PayeeListView : EditorFactoryListView
	{
		private Accounts _accounts;
		private PayeeEditCtrl _editor;

		public PayeeListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts)
		{			
			_accounts = accounts;
		}

		public override Control CreateEditor()
		{
			_editor = new PayeeEditCtrl(_accounts);
			return _editor;
		}

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if(e.IsSelected == true)
			{
				_editor.SetItemId((PayeeId)e.Item.Tag);
			}			
		}

		public void RefreshItems()
		{
			this.listView.Items.Clear();
			if(_accounts != null)
			{
				foreach (KeyValuePair<PayeeId, Payee> pair in _accounts.PayeeList)
				{
                    Payee idItem = pair.Value;
					ListViewItem item = new ListViewItem(idItem.Name);
					item.Tag = pair.Key;
					Font font = item.Font;
					if(idItem.Obsolete == true)
					{
						item.Font = new Font(font, FontStyle.Strikeout);
					}
					else
					{
						item.Font = new Font(font, FontStyle.Regular);
					}

					this.listView.Items.Add(item);
				}
			}
		}

		private void listView_MouseUp(object sender, MouseEventArgs e)
		{
			if(this.listView.SelectedItems.Count == 0)
			{
				_editor.SetItemId(null);
			}
		}
	}
}
