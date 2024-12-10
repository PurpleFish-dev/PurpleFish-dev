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
	public partial class PropertyListView : EditorFactoryListView
	{
		private Accounts _accounts;
		private PropertyEditCtrl _editor;

		public PropertyListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts)
		{			
			_accounts = accounts;
		}

		public override Control CreateEditor()
		{
			_editor = new PropertyEditCtrl(_accounts);
			return _editor;
		}

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if(e.IsSelected == true)
			{
				_editor.SetItemId((PropertyId)e.Item.Tag);
			}			
		}

		public void RefreshItems()
		{
			this.listView.Items.Clear();
			if(_accounts != null)
			{
				foreach(KeyValuePair<PropertyId, Property> kvp in _accounts.PropertyList)
				{
					ListViewItem item = new ListViewItem(kvp.Value.Name);
					item.Tag = kvp.Key;
					Font font = item.Font;
					if(kvp.Value.Obsolete == true)
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
