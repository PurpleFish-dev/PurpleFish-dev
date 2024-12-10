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
	public partial class ItemListView : EditorFactoryListView
	{
		private IdClassListWritable<Item> _list;
		private ItemEditCtrl _editor;

		public ItemListView()
		{
			InitializeComponent();
		}

		public void Initialize(IdClassListWritable<Item> list)
		{			
			_list = list;
		}

		public override Control CreateEditor()
		{
			_editor = new ItemEditCtrl(this, _list);
			return _editor;
		}

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			Identifier id =0;
			if(e.IsSelected == true)
			{
				id = (Identifier)e.Item.Tag;
			}
			_editor.SetItemId(id, false);
		}

		public void RefreshItems()
		{
			this.listView.Items.Clear();
			foreach(Identifier key in _list.Keys)
			{
				Item idItem = _list[key];
				ListViewItem item = new ListViewItem(idItem.Name);
				item.Tag = key;
				this.listView.Items.Add(item);
			}
		}

		private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(this.listView.SelectedItems.Count == 1)
			{
				_editor.SetItemId((Identifier)this.listView.SelectedItems[0].Tag, true);			
			}
		}
	}
}
