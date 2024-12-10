using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;

/*
 MyOlv.CustomSorter = delegate(OLVColumn column, SortOrder order) {
    // check which column is about to be sorted and set your custom comparer
    if (column == ArticleNumber) {
        MyOlv.ListViewItemSorter = new ArticleNumberComparer(order);
    }
};          

class ArticleNumberComparer : IComparer {
    SortOrder _Order;

    public ArticleNumberComparer(SortOrder order) {
        _Order = order;
    }

    public int Compare(object x, object y) {
         // perform you desired comparison depending on the _Order
    }
}
*/

namespace WindowsFormsControlLibrary1
{
	public partial class EntryListView : EditorFactoryListView
	{	
		private EntryEditCtrl _editor;
		private Accounts _accounts;
		private AccountId _idAccount;
		public AccountId AccountId
		{
			get { return _idAccount; }
		}

		public EntryListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts, AccountId idAccount)
		{			
			_accounts = accounts;
			_idAccount = idAccount;

            this.olvDate.AspectGetter = delegate(object x) { return ((Entry)x).Date; };            
            this.olvDescription.AspectGetter = delegate(object x) { return ((Entry)x).Description; };
            this.olvCatagory.AspectGetter = delegate(object x) 
            {
                bool transfer = ((Entry)x).IsTransfer();
                if (transfer == true)
                {
                    AccountId transId = ((Entry)x).GetTransferAccountId(this._idAccount);
                    return "Transfer to " + _accounts.Account(transId).Name;
                }
                else
                {
                    CatagoryId catId = ((Entry)x).CatagoryId;
                    if (catId == null) return "-";
                    return _accounts.Catagory(catId).Name;
                }
            };

            this.olvProperty.AspectGetter = delegate (object x)
            {
                PropertyId propertyId = ((Entry)x).PropertyId;
                if (propertyId == null) return "-";
                return _accounts.Property(propertyId).Name;
            };

            this.olvPayee.AspectGetter = delegate(object x)
            {
                PayeeId payeeId = ((Entry)x).PayeeId;
                if (payeeId == null) return "-";
                return accounts.Payee(payeeId).Name;
            };

            this.olvCredit.AspectGetter = delegate(object x) { return ((Entry)x).GetAmount(_idAccount) >= 0 ? ((Entry)x).GetAmount(_idAccount).ToString() : ""; };
            this.olvDebit.AspectGetter = delegate(object x) { return ((Entry)x).GetAmount(_idAccount) < 0 ? decimal.Negate(((Entry)x).GetAmount(_idAccount)).ToString() : ""; };
            this.olvBalance.AspectGetter = delegate(object x) { return ((Entry)x).Balance; };
            this.olvType.AspectGetter = delegate (object x) { return ((Entry)x).Type; };
            this.olvFileAssoc.AspectGetter = delegate (object x) { return ((Entry)x).RecieptNo == "" ? "" : "x"; };
        }

        public Entry SelectedEntry()
        {
            return fastObjectListView.SelectedObjects.Count == 1
                ? (Entry)fastObjectListView.SelectedObjects[0] : null;
        }

        public List<EntryId> SelectedEntries()
        {
            var result = new List<EntryId>();
            var items = fastObjectListView.SelectedObjects;
          
            if (items.Count > 0)
            {
                foreach (Entry existing in items)
                {
                    result.Add(existing.Id);
                }
            }
            return result;
        }

		public override Control CreateEditor()
		{
			if(_editor == null)
			{
				_editor = new EntryEditCtrl(_accounts, _idAccount);
			}
			return _editor;
		}

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = this.fastObjectListView.SelectedObjects;
            if (selectedItems.Count == 1)
            {
                _editor.SetEntry((Entry)selectedItems[0]);
            }
            else
            {
                _editor.SetEntry(null);
            }	
		}

		public void RefreshItems(EntryId id)
		{
            Filter filter = this._accounts.GetFilter(_idAccount);// new Filter();
            int top = fastObjectListView.TopItemIndex;                
                this.fastObjectListView.SetObjects(_accounts.EntryList(filter, _accounts.GetSorter(_idAccount)));
            fastObjectListView.TopItemIndex = top;

        }
		
		private void listView_MouseUp(object sender, MouseEventArgs e)
        {
            var selectedItems = this.fastObjectListView.SelectedObjects;
            if (selectedItems.Count == 0)
            {
                _editor.SetEntry(null);
            }
        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(Cursor.Position);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EntryId> ids = SelectedEntries();
            _accounts.Entry_Remove(ids);
        }

        private void ModifyCatagoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Collections.IList items = this.fastObjectListView.SelectedObjects;
            List<Entry> entries = items.Cast<Entry>().ToList();

            frmMultiEdit frm = new frmMultiEdit(this._accounts, this._idAccount, entries);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CatagoryId catagoryId = frm.CatagoryId;
                
                if (items.Count >= 1)
                {
                    foreach (Entry existing in entries)
                    {
                        //Entry entry = new Entry(existing);
                        if (frm.CatagoryId != null)
                            existing.SetEntry(_idAccount, frm.CatagoryId);
                        else if (frm.TransferAccountId != null)
                            existing.SetTransfer(_idAccount, frm.TransferAccountId);

                        if(frm.PropertyId != null)
                            existing.PropertyId = frm.PropertyId;

                        if(frm.PayeeId != null)
                            existing.PayeeId = frm.PayeeId;

                        //if (_accounts.EntryCanReplace(entry))
                        ///    _accounts.EntryReplace(entry);
                    }
                    if (_accounts.EntryCanReplace(entries))
                        _accounts.EntryReplace(entries);
                    return;
                }
            }
        }

        private void moveTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Collections.IList items = this.fastObjectListView.SelectedObjects;
            List<Entry> entries = items.Cast<Entry>().ToList();

            moveEntryFrm frm = new moveEntryFrm(this._accounts, this._idAccount, entries);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AccountId newAccountId = frm.TransferAccountId;

                if (items.Count >= 1)
                {
                    foreach (Entry existing in items)
                    {
                        Entry entry = new Entry(existing);
                        if (entry.IsTransfer())
                        {
                           entry.SetTransfer(entry.GetTransferAccountId(this._idAccount), newAccountId);// pivot, change                            
                        }
                        else
                        {
                            entry.SetEntry(newAccountId, entry.CatagoryId);
                        }


                        if (_accounts.EntryCanReplace(entry))
                            _accounts.EntryReplace(entry);
                    }
                    return;
                }
            }
        }

        private void timeChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var chartfrm = new TimeChartFrm(_accounts, _idAccount);
            chartfrm.ShowDialog(this);
        }

        private void repeatTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EntryId> ids = SelectedEntries();
            ScheduledTransactionsDlg dlg = new ScheduledTransactionsDlg(this._accounts, _idAccount, ids);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                int tmp = 0;
            }
        }

        private void fastObjectListView_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            
            Entry customer = (Entry)e.Model;
            if (customer.Date < DateTime.Now)
                e.Item.ForeColor = Color.DarkGray;
            else if (customer.Balance < 1000)
                e.UseCellFormatEvents = true;
        }

        private void fastObjectListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.ColumnIndex == this.olvBalance.Index)
            {
                Entry customer = (Entry)e.Model;
                if (customer.Balance < 0)
                    e.SubItem.ForeColor = Color.Red;
                else if (customer.Balance < 1000)
                    e.SubItem.ForeColor = Color.Yellow;
            }
        }

        private void HandleCellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            var item = (Entry)e.RowObject;
            var ctrl = (System.Windows.Forms.ComboBox)e.Control;

            if (ctrl.SelectedItem == null)
                return;

            Id id = ((TagString)ctrl.SelectedItem).Id;

            CatagoryId idCat = id as CatagoryId;
            if (idCat != null)
            {
                item.SetEntry(_idAccount, idCat);
                this._accounts.EntryReplace(item);
                return;
            }

            AccountId idTrans = id as AccountId;
            if (idTrans != null)
            {
                item.SetTransfer(_idAccount, idTrans);
                this._accounts.EntryReplace(item);
                return;
            }
        }

        private void HandleCellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Column == this.olvCatagory)
            {
                var item = (Entry)e.RowObject;
                if (!this._accounts.Account(this._idAccount).IsLocked(item.Date))
                {
                    var ctrl = new System.Windows.Forms.ComboBox();
                    ctrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

                    ctrl.Bounds = e.CellBounds;
                    e.Control = ctrl;
                   
                    ctrl.Items.Clear();
                    foreach (CatagoryId id in _accounts.CatagoryList.Keys)
                        ctrl.Items.Add(new TagString(id, this._accounts.Catagory(id).Name));

                    bool includeHiddenAccounts = false;
                    foreach (KeyValuePair<AccountId, Account> kvp in _accounts.AccountList(includeHiddenAccounts))
                    {
                        if (kvp.Key != _idAccount)
                            ctrl.Items.Add(new TagString(kvp.Key, "Transfer to " + kvp.Value.Name));
                    }

                    if (item.IsTransfer())
                        ctrl.SelectedItem = TagString.FindItem(ctrl, item.GetTransferAccountId(this._idAccount));
                    else
                        ctrl.SelectedItem = TagString.FindItem(ctrl, item.CatagoryId);
                    return;

                    //if (e.Value is Color)
                    //{
                    //    //ColorCellEditor cce = new ColorCellEditor();
                    //    //cce.Bounds = e.CellBounds;
                    //    //cce.Value = e.Value;
                    //    //e.Control = cce;
                    //}
                }
            }
            e.Cancel = true;
        }

        private void HandleCellEditValidating(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
        }

        private void fastObjectListView_ItemActivate(object sender, EventArgs e)
        {
            if (SelectedEntry() != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(SelectedEntry().RecieptNo);
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Exception Source: {0}", ee.Source);
                    Console.WriteLine("Exception Message: {0}", ee.Message);
                }                
            }
        }

        private void fastObjectListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Filter filter = new Filter();
            filter.Accounts.Add(this._idAccount);
            int nCol = e.Column;
            if (nCol == 0) this._accounts.GetSorter(this._idAccount).SetPrimaryCol("Date");// Date();
            else if (nCol == 1) this._accounts.GetSorter(this._idAccount).SetPrimaryCol("Description");
            else if (nCol == 2) this._accounts.GetSorter(this._idAccount).SetPrimaryCol("Amount");
            else if (nCol == 3) this._accounts.GetSorter(this._idAccount).SetPrimaryCol("Amount");
            else this._accounts.GetSorter(this._idAccount).SetPrimaryCol("CategoryAndTransfer");
            int top = fastObjectListView.TopItemIndex;
            this.fastObjectListView.SetObjects(_accounts.EntryList(filter, this._accounts.GetSorter(this._idAccount)));
            fastObjectListView.TopItemIndex = top;
        }
    }
}
