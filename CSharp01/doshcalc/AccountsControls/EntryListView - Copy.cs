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
	public partial class ReconcileListView : EditorFactoryListView
	{
        private EntryEditCtrl _editor;
		private Accounts _accounts;
		private AccountId _idAccount;
		public AccountId AccountId
		{
			get { return _idAccount; }
		}

		private ListViewColumnSorter lvwColumnSorter;

		public ReconcileListView()
		{
			InitializeComponent();
		}

		public void Initialize(Accounts accounts, AccountId idAccount)
		{			
			_accounts = accounts;
			_idAccount = idAccount;
            //lvwColumnSorter = new ListViewColumnSorter();

            this.olvType.AspectGetter = delegate (object x)
            { return ((Accounts.ReconcileEntry)x).Imported == null ? "" : ((Accounts.ReconcileEntry)x).Imported.Type.ToString(); };

            this.olvTypeB.AspectGetter = delegate (object x)
            { return ((Accounts.ReconcileEntry)x).Predicted == null ? "" : ((Accounts.ReconcileEntry)x).Predicted.Type.ToString(); };

            this.olvReconcileFlag.AspectGetter = delegate (object x) { return ((Accounts.ReconcileEntry)x).reconciled.ToString(); };

            this.olvDate.AspectGetter = delegate (object x)
            { return ((Accounts.ReconcileEntry)x).Imported == null ? "" : ((Accounts.ReconcileEntry)x).Imported.Date.ToString(); };

            this.olvDateB.AspectGetter = delegate (object x) 
            { return ((Accounts.ReconcileEntry)x).Predicted == null ? "" : ((Accounts.ReconcileEntry)x).Predicted.Date.ToString(); };

            this.olvDescription.AspectGetter = delegate (object x)
            { return ((Accounts.ReconcileEntry)x).Imported == null ? "" : ((Accounts.ReconcileEntry)x).Imported.Description; };

            this.olvDescriptionB.AspectGetter = delegate (object x)
            { return ((Accounts.ReconcileEntry)x).Predicted == null ? "" : ((Accounts.ReconcileEntry)x).Predicted.Description; };

            this.olvCatagory.AspectGetter = delegate (object x)
            {
                Entry entryB = ((Accounts.ReconcileEntry)x).Imported;
                if (entryB == null) return "";
                CatagoryId catId = ((Accounts.ReconcileEntry)x).Imported.CatagoryId;
                if (catId == null) return "-";

                return _accounts.Catagory(catId).Name;
            };

            this.olvCatagoryB.AspectGetter = delegate (object x)
            {
                Entry entryB =((Accounts.ReconcileEntry)x).Predicted;
                if (entryB == null) return "";
                CatagoryId catId = ((Accounts.ReconcileEntry)x).Predicted.CatagoryId;
                if (catId == null) return "-";

                return _accounts.Catagory(catId).Name;
            };

            this.olvTransfer.AspectGetter = delegate (object x)
            {
                Entry entryB = ((Accounts.ReconcileEntry)x).Imported;
                if (entryB == null) return "";
                AccountId transId = ((Accounts.ReconcileEntry)x).Imported.GetTransferAccountId(idAccount);
                if (transId == null) return "-";
                return accounts.Account(transId).Name;
            };

            this.olvTransferB.AspectGetter = delegate (object x)
            {
                Entry entryB = ((Accounts.ReconcileEntry)x).Predicted;
                if (entryB == null) return "";
                AccountId transId = ((Accounts.ReconcileEntry)x).Predicted.GetTransferAccountId(idAccount);
                if (transId == null) return "-";
                return accounts.Account(transId).Name;
            };

            this.olvCredit.AspectGetter = delegate (object x)
            {
                Entry entryA = ((Accounts.ReconcileEntry)x).Imported;
                if ((entryA != null) && (entryA.GetAmount(_idAccount) >= 0))
                    return entryA.GetAmount(_idAccount).ToString();
                return "";
            };

            this.olvDebit.AspectGetter = delegate (object x)
            {
                Entry entryA = ((Accounts.ReconcileEntry)x).Imported;
                if ((entryA != null) && (entryA.GetAmount(_idAccount) < 0))
                    return decimal.Negate(entryA.GetAmount(_idAccount)).ToString();
                return "";
            };

            this.olvCreditB.AspectGetter = delegate (object x)
            {
                Entry entryB = ((Accounts.ReconcileEntry)x).Predicted;
                if ((entryB != null) && (entryB.GetAmount(_idAccount) >= 0))
                    return entryB.GetAmount(_idAccount).ToString();
                return "";
            };

            this.olvDebitB.AspectGetter = delegate (object x)
            {
                Entry entryB = ((Accounts.ReconcileEntry)x).Predicted;
                if ((entryB != null) && (entryB.GetAmount(_idAccount) < 0))
                    return decimal.Negate(entryB.GetAmount(_idAccount)).ToString();
                return "";
            };

            




            //this.olvDebit.AspectGetter = delegate(object x) { return ((Accounts.ReconcileItem)x).entryB.GetAmount(_idAccount) < 0 ? decimal.Negate(((Accounts.ReconcileItem)x).entryB.GetAmount(_idAccount)).ToString() : ""; };
            ///this.olvType.AspectGetter = delegate (object x) { return ((Accounts.ReconcileItem)x).entryA.Type; };

            accounts.Reconcile_Start(idAccount);
        }

        public List<Accounts.ReconcileEntry> SelectedEntries()
        {
            var result = new List<Accounts.ReconcileEntry>();
            var items = fastObjectListView.SelectedObjects;
          
            if (items.Count > 0)
            {
                foreach (Accounts.ReconcileEntry existing in items)
                {
                    result.Add(existing);
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
            //var selectedItems = this.fastObjectListView.SelectedObjects;
            //if (selectedItems.Count == 1)
            //{
            //    _editor.SetItemId(((Entry)selectedItems[0]).Id);
            //}
            //else
            //{
            //    _editor.SetItemId((EntryId)null);
            //}	
		}

		public void RefreshItems()
		{
            int top = fastObjectListView.TopItemIndex;                
                this.fastObjectListView.SetObjects(_accounts.Reconcile_ListEntries());
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
            var ids = new List<Accounts.ReconcileEntryId>();
            foreach (Accounts.ReconcileEntry item in fastObjectListView.SelectedObjects)
                ids.Add(item.Id);
            _accounts.Reconcile_Remove(ids);
            RefreshItems();
        }

        private void ReconcileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ids = new List<Accounts.ReconcileEntryId>();
            foreach(Accounts.ReconcileEntry item in fastObjectListView.SelectedObjects)
                ids.Add(item.Id);

            _accounts.Reconcile_Reconcile(ids);
            RefreshItems();
        }

        private void matchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var items = this.fastObjectListView.SelectedObjects;
            if (items.Count == 2)
            {
                _accounts.Reconcile_Match(((Accounts.ReconcileEntry)items[0]).Id, ((Accounts.ReconcileEntry)items[1]).Id);
                RefreshItems();
            }            
        }

        private void unmatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ids = new List<Accounts.ReconcileEntryId>();
            foreach (Accounts.ReconcileEntry item in fastObjectListView.SelectedObjects)
                ids.Add(item.Id);
            _accounts.Reconcile_UnMatch(ids);
            RefreshItems();      
        }

        private void fastObjectListView_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            string reason = "";
            var rowItem = (Accounts.ReconcileEntry)e.Model;
            if ((rowItem.Predicted != null) && !_accounts.EntryCanRemove(rowItem.Predicted.Id, ref reason))
            {
                e.Item.ForeColor = Color.Gray;
                return;
            }
                    
              

            //Imported
            //red if no match, orange if fuzzy match, green if exact match
            //white if reconciled

            //No Imported.
            //red if after end date.

            if (rowItem.Imported != null)
            {
                if (rowItem.reconciled == true)
                {
                    e.Item.ForeColor = Color.White;
                    return;
                }

                if (rowItem.Predicted == null)
                {
                    e.Item.ForeColor = Color.Red;
                    return;
                }

                //Entry predictedEntry = rowItem.Predicted;
                //Entry importedEntry = rowItem.ImportedId;
                if ((rowItem.Imported.GetAmount(AccountId) == rowItem.Predicted.GetAmount(AccountId))
                    && (rowItem.Imported.Date == rowItem.Predicted.Date))
                {
                    e.Item.ForeColor = Color.Green;
                }
                else
                {
                    e.Item.ForeColor = Color.Orange;
                    e.UseCellFormatEvents = true;
                }
                return;
            }

            if (rowItem.Predicted.Date <= this._accounts.endDate)
            {
                e.Item.ForeColor = Color.Red;
            }

            if (rowItem.Predicted == null || rowItem.Predicted.Date <= this._accounts.endDate)
            {
                if (rowItem.reconciled == true)
                {
                    e.Item.ForeColor = Color.White;
                }
                else if (rowItem.Predicted == null)// ((rowItem.Predicted == null || rowItem.Imported == null) && (this._accounts.endDate <= entryB.Date))
                {
                    e.Item.ForeColor = Color.Red;
                }
                else if (rowItem.Imported == null)
                {
                }
                else if ((rowItem.Imported != null) && (rowItem.Predicted != null))
                {
                    System.Diagnostics.Debug.Assert(false);
                    if ((rowItem.Predicted.GetAmount(AccountId) == rowItem.Imported.GetAmount(AccountId))
                    && (rowItem.Predicted.Date == rowItem.Imported.Date))
                    {
                        e.Item.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.Item.ForeColor = Color.Orange;
                    }
                }
            }   
        }

        private void fastObjectListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            e.SubItem.ForeColor = Color.Green;
            var rowItem = (Accounts.ReconcileEntry)e.Model;

            if ((e.ColumnIndex == this.olvDebit.Index || e.ColumnIndex == this.olvDebitB.Index)
                     && (rowItem.Imported.GetAmount(AccountId) != rowItem.Predicted.GetAmount(AccountId)))
                e.SubItem.ForeColor = Color.Yellow;

            if ((e.ColumnIndex == this.olvDate.Index || e.ColumnIndex == this.olvDateB.Index)
               && (rowItem.Imported.Date != rowItem.Predicted.Date))
                e.SubItem.ForeColor = Color.Yellow;
        }
    }
}
