using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using AccountsCore;
using System.Diagnostics;
using GenericControls;
using WindowsFormsControlLibrary1;


namespace WindowsFormsApplication6
{
	public partial class AppForm : Form
	{
		private Accounts _accounts;
        private string _filename;

		private CatagoryListView catagoryListView = new CatagoryListView();
		private PayeeListView payeeListView = new PayeeListView();
		private TaxCodeListView taxCodeListView = new TaxCodeListView();
		private PropertyListView propertyListView = new PropertyListView();
		private AccountListView accountListView = new AccountListView();
				
		//private TSHost ctrl = new TSHost();
		//private ToolStripMonthCalendar TSHostCal= new ToolStripMonthCalendar();

		private ToolTip tt = new ToolTip();

		


		public AppForm()
		{
			InitializeComponent();
			
			_accounts = new Accounts();
            _accounts.Create();
            createPages();

            _accounts.CatagoriesChanged += OnCatagoriesChanged;
			_accounts.TaxCodesChanged += OnTaxCodesChanged;
			_accounts.PayeesChanged += OnPayeesChanged;
			_accounts.PropertiesChanged += OnPropertiesChanged;
			_accounts.AccountsChanged += OnAccountsChanged;
			_accounts.EntriesChanged += OnEntriesChanged;

			_accounts.AccountAdded += OnAccountAdded;
			_accounts.AccountModified += OnAccountModified;
			_accounts.AccountRemoved += OnAccountRemoved;


			//_accounts.ActionMaster.UndoDescriptionChanged += OnUndoDescriptionChanged;
			_accounts.ActionMaster.CanUndoChanged += OnCanUndoChanged;
			//_accounts.ActionMaster.RedoDescriptionChanged += OnRedoDescriptionChanged;
			_accounts.ActionMaster.CanRedoChanged += OnCanRedoChanged;

			/*
			 * accountListView.Initialize(_accounts);
			accountListView.RefreshItems();	
			this.tabControl.TabPages.Add(new ControlHostTabPage(accountListView, "Accounts"));
			
			payeeListView.Initialize(_accounts);
			payeeListView.RefreshItems();	
			this.tabControl.TabPages.Add(new ControlHostTabPage(payeeListView, "Payees"));
			
			taxCodeListView.Initialize(_accounts);
			taxCodeListView.RefreshItems();	
			this.tabControl.TabPages.Add(new ControlHostTabPage(taxCodeListView, "Tax Codes"));

			propertyListView.Initialize(_accounts);
			propertyListView.RefreshItems();	
			this.tabControl.TabPages.Add(new ControlHostTabPage(propertyListView, "Properties"));
			
			catagoryListView.Initialize(_accounts);
			catagoryListView.RefreshItems();	
			this.tabControl.TabPages.Add(new ControlHostTabPage(catagoryListView, "Catagory"));
			 * */
		}

		private void createPages()
		{
			this.tabControl1.TabPages.Clear();
			
			accountListView.Initialize(_accounts);
			accountListView.RefreshItems();	
			ControlHostTabPage chtp = new ControlHostTabPage(accountListView, "Accounts");
			this.tabControl1.TabPages.Add(chtp);
			this.tabControl1.SelectedIndex =0;

            if (this.sliderpanel2 != null)
            {
                this.sliderpanel2.ClearControls();
            }
			Control ctrl = ((EditorFactoryListView)chtp.GetControl()).CreateEditor();
			if(ctrl != null)
			{
				this.sliderpanel2.ReplaceCtrl(ctrl);
			}
					
			payeeListView.Initialize(_accounts);
			payeeListView.RefreshItems();	
			this.tabControl1.TabPages.Add(new ControlHostTabPage(payeeListView, "Payees"));
			
			taxCodeListView.Initialize(_accounts);
			taxCodeListView.RefreshItems();	
			this.tabControl1.TabPages.Add(new ControlHostTabPage(taxCodeListView, "Tax Codes"));

			propertyListView.Initialize(_accounts);
			propertyListView.RefreshItems();	
			this.tabControl1.TabPages.Add(new ControlHostTabPage(propertyListView, "Properties"));
			
			catagoryListView.Initialize(_accounts);
			catagoryListView.RefreshItems();	
			this.tabControl1.TabPages.Add(new ControlHostTabPage(catagoryListView, "Catagory"));

			bool includeHiddenAccounts = false;
			foreach(KeyValuePair<AccountId, Account> kvp in _accounts.AccountList(includeHiddenAccounts))
			{
				EntryListView elv = new EntryListView();
				elv.Initialize(_accounts, kvp.Key);
				elv.RefreshItems(null);	
				this.tabControl1.TabPages.Add(new ControlHostTabPage(elv, kvp.Value.Name));
			}
		}

		

		private void OnCatagoriesChanged(object sender)
		{
			catagoryListView.RefreshItems();	
		}

		private void OnTaxCodesChanged(object sender)
		{
			taxCodeListView.RefreshItems();	
		}

		private void OnPayeesChanged(object sender)
		{
			payeeListView.RefreshItems();	
		}

		private void OnPropertiesChanged(object sender)
		{
			this.propertyListView.RefreshItems();	
		}

		private void OnAccountsChanged(object sender)
		{
			this.accountListView.RefreshItems();	
		}

		private void OnEntriesChanged(object sender, EntryEventArgs e)
		{
			foreach(ControlHostTabPage page in this.tabControl1.TabPages)
			{
				foreach(AccountId id in e.AccountIds)
                {
                    if ((page.GetControl() is EntryListView) && (((EntryListView)page.GetControl()).AccountId == id))
                    {
                        ((EntryListView)page.GetControl()).RefreshItems(null);
                    }
                }
                
                
                //if( (page.GetControl() is EntryListView) && (((EntryListView)page.GetControl()).AccountId == e.AccountId) )
                //{
                //    ((EntryListView)page.GetControl()).RefreshItems(e.EntryId);
                //}
			}
		}
		
		private void OnAccountAdded(object sender, AccountEventArgs e)
		{
			Account account = _accounts.Account(e.Id);
			if(account.Hidden == false)
			{
				EntryListView elv = new EntryListView();
				elv.Initialize(_accounts, e.Id);
				elv.RefreshItems(null);	
				this.tabControl1.TabPages.Add(new ControlHostTabPage(elv, _accounts.Account(e.Id).Name));
			}		
		}
		
		private void OnAccountModified(object sender, AccountEventArgs e)
		{
			foreach(ControlHostTabPage page in this.tabControl1.TabPages)
			{
				if( (page.GetControl() is EntryListView) && (((EntryListView)page.GetControl()).AccountId == e.Id) )
				{
					if(_accounts.Account(e.Id).Hidden == true)
					{
						this.tabControl1.TabPages.Remove(page);
					}
					else
					{
						page.Text = _accounts.Account(e.Id).Name;
					}
					return;
				}
			}
			if(_accounts.Account(e.Id).Hidden == false)
			{
				EntryListView elv = new EntryListView();
				elv.Initialize(_accounts, e.Id);
				elv.RefreshItems(null);	
				this.tabControl1.TabPages.Add(new ControlHostTabPage(elv, _accounts.Account(e.Id).Name));
			}	
		}

		private void OnAccountRemoved(object sender, AccountEventArgs e)
		{
			foreach(ControlHostTabPage page in this.tabControl1.TabPages)
			{
				if( page.GetControl() is EntryListView)
				{
					if( ((EntryListView)page.GetControl()).AccountId == e.Id )
					{
						this.tabControl1.TabPages.Remove(page);
					}
				}
			}
		}
		
		private void OnCanUndoChanged(object sender, bool canUndo)
		{
			this.tsiUndo.Enabled = canUndo;
		}

		private void OnCanRedoChanged(object sender, bool canRedo)
		{
			this.tsiRedo.Enabled = canRedo;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Refresh();
			//this.btnEdit.Enabled =false;
			//editMode(false);	

			//tt.SetToolTip(tscMain, "asdfasdf");



			
			


		}
		
		//private void button1_Click_1(object sender, EventArgs e)
		//{
		//	if(this.sidePanel.Dock == DockStyle.Right)
		//	{
		//		this.sidePanel.Dock = DockStyle.Left;
		//		this.splitter1.Dock = DockStyle.Left;
		//	}
		//	else
		//	{
		//		this.sidePanel.Dock = DockStyle.Right;
		//		this.splitter1.Dock = DockStyle.Right;
		//	}
		//}
		
		//private void button1_Click_2(object sender, EventArgs e)
		//{
		//	if(this.sidePanel.Dock == DockStyle.Right)
		//	{
		//		this.sidePanel.Dock = DockStyle.Left;
		//		this.splitter1.Dock = DockStyle.Left;
		//	}
		//	else
		//	{
		//		this.sidePanel.Dock = DockStyle.Right;
		//		this.splitter1.Dock = DockStyle.Right;
		//	}
		//}

		private void mnuSaveACopy(object sender, EventArgs e)
		{
			if(this.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				_accounts.ExportXML(saveFileDialog.FileName);
			}
		}

		private void mnuOpen_Click(object sender, EventArgs e)
		{
			if(this.openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_accounts.Open(openFileDialog.FileName);
				createPages();
			}
		}		

		private void cmdNew_Click(object sender, EventArgs e)
		{
            _accounts.Create();
            createPages();
            this.mnuOpen.Enabled = true;
		}

		private void updateMenu() 
		{
			if(this._accounts == null)
			{
				//this.
			}
			else
			{
			}
		}

		/*
		 * private void btnEnter_Click(object sender, EventArgs e)
		{
			if(this._editMode == false)
			{
				//_accounts.DisplayedAccount().EntryList.Add(this.entryCtrl1.Entry);
			}
			else
			{
				Debug.Assert(_edit != 0);
				//_accounts.DisplayedAccount().EntryList[_edit] = this.entryCtrl1.Entry;
				

			}
			editMode(false);
			//this.btnEdit.Enabled =false;
			
			//this._bModeEdit = true;
			//this.entryCtrl1.Entry = new Entry();
			//this.btnDelete.Enabled = false;
		
			this.Refresh();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			//this._accounts.DisplayedAccount()._entryList.Remove(_edit);
			_edit = 0;
			//this.entryCtrl1.Entry = new Entry();
			//this.btnDelete.Enabled = false;
			Refresh();
		}
		 * */

		
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (_accounts.IsModified())
            {
                const string message = "Do you want to save changes?";
                const string caption = "Form Closing";
                DialogResult result = MessageBox.Show(message, caption,
                                                      MessageBoxButtons.YesNoCancel,
                                                      MessageBoxIcon.Warning);
                // If the no button was pressed ...
                if (result == DialogResult.Yes)
                {
                    saveFileDialog.FileName = _filename;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _accounts.SaveAs(saveFileDialog.FileName);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }            
        }
		
		private void TaxCodesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//ListEditFrm frm = new ListEditFrm(_accounts.TaxCodeList);
			//frm.ShowDialog();					
		}

		private void catagoriesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//CatagoriesFrm frm = new CatagoriesFrm(_accounts);
			//frm.ShowDialog();		
		}

		

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this._accounts.ActionMaster.Undo();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this._accounts.ActionMaster.Redo();
		}

		private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			this.undoToolStripMenuItem.Enabled = this._accounts.ActionMaster.CanUndo();
			this.undoToolStripMenuItem.ToolTipText = this._accounts.ActionMaster.UndoDescription();

			this.redoToolStripMenuItem.Enabled = this._accounts.ActionMaster.CanRedo();
			this.redoToolStripMenuItem.ToolTipText = this._accounts.ActionMaster.RedoDescription();
		}

		

		/*
		 * private void lvProperties_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			editMode(false);			
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			editMode(true);
			
		}

		private void editMode(bool edit)
		{
			this._editMode = edit;
			if(edit == true)
			{	
				Debug.Assert(this._edit > 0);
				//this.btnEnter.Text = "Modify";
				//this.btnDelete.Enabled = true;
				//btnCancel.Visible = true;
			}
			else
			{
				//this.btnEnter.Text = "Insert";
				//this.btnDelete.Enabled = false;
				//btnCancel.Visible = false;
			}
		}
		 * */

		private void tabControl_Selected(object sender, TabControlEventArgs e)
		{
			if( (e.TabPage != null) && (e.TabPage.GetType() == typeof(ControlHostTabPage)) )
			{
				this.sliderpanel2.ClearControls();
				Control ctrl = ((EditorFactoryListView)((ControlHostTabPage)e.TabPage).GetControl()).CreateEditor();
				if(ctrl != null)
				{
					this.sliderpanel2.ReplaceCtrl(ctrl);
				}
			} 
			else
			{
				this.sliderpanel2.ClearControls();
			}
		}

		private void qIFParserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void tsiUndo_Click(object sender, EventArgs e)
		{
			if( _accounts.ActionMaster.CanUndo() )
			{
				_accounts.ActionMaster.Undo();
				if( _accounts.ActionMaster.CanUndo() )
				{
					Point mp = MousePosition;
					mp.Y += 22;
					mp = this.tscMain.PointToClient(mp);
					tt.Show(_accounts.ActionMaster.UndoDescription(), this.tscMain, mp, 4000);
				}
				else
				{
					((ToolStripItem)sender).Enabled = false;			
				}
			}
		}

		private void tsiRedo_Click(object sender, EventArgs e)
		{
			_accounts.ActionMaster.Redo();
		}

		private void tsiUndo_MouseHover(object sender, EventArgs e)
		{
			Point mp = MousePosition;
			mp.Y += 22;
			mp = this.tscMain.PointToClient(mp);
			tt.Show(_accounts.ActionMaster.UndoDescription(), this.tscMain, mp, 4000);			
		}

		private void tsiUndo_MouseLeave(object sender, EventArgs e)
		{
			tt.Hide(this.tscMain);
		}

		private void mnufile_Click(object sender, EventArgs e)
		{
			
		}

		private void importToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (openImportFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openImportFileDialog.FileName;
				var frm = new ImportWizardForm(_accounts, fileName);
                frm.ShowDialog(this);
                {
                    
                }
			}
		}

		private void openImportFileDialog_FileOk(object sender, CancelEventArgs e)
		{
           
		}

		private void cmdCustomizeToolStrip_Click(object sender, EventArgs e)
		{
			this.tscMain.Customize();
		}

		private void tscMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //var frm = new BasicBar(this._accounts);
            //frm.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ControlHostTabPage page = (ControlHostTabPage)this.tabControl1.SelectedTab;
            //page.GetControl();


            if (page.GetControl() is EntryListView)
            {
                EntryListView elv = (EntryListView)page.GetControl();//).AccountId
                ReconcileFrm frm = new ReconcileFrm(this._accounts, elv.AccountId);
                frm.ShowDialog();            
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

		}

		private void toolStripButton1_Click(object sender, EventArgs e)
        {
			//PdfFrm frm = new PdfFrm(this._accounts);
			//frm.Show();
		}
	}
}
