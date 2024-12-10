using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QifApi;
using GenericControls;
using WindowsFormsControlLibrary1;
using System.Diagnostics;

namespace WindowsFormsApplication6
{
	public partial class ImportWizard : UserControl
	{
		private QifDateFormatSelectionCtrl ctrl1;
		private QifItemSelectionCtrl ctrl2;
		private QifBasicTransactionTranslatorCtrl ctrl3;
        private QifDom dom;
        private AccountsCore.Accounts accounts;

        public ImportWizard()
		{
			InitializeComponent();
            
			ctrl1 = new QifDateFormatSelectionCtrl();
			var page1 = new ControlHostWizardPage(null, ctrl1, "Payees");
			this.wizardPages1.TabPages.Add(page1);

			ctrl2 = new QifItemSelectionCtrl();
			var page2 = new ControlHostWizardPage(page1, ctrl2, "Payees");
			this.wizardPages1.TabPages.Add(page2);

			ctrl3 = new QifBasicTransactionTranslatorCtrl();
			var page3 = new ControlHostWizardPage(page2, ctrl3, "Payees");
			this.wizardPages1.TabPages.Add(page3);

			this.wizardPages1.SelectedTab = page1;
		}

		private void UserControl1_Load(object sender, EventArgs e)
		{

		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			ControlHostWizardPage page = ((ControlHostWizardPage)this.wizardPages1.SelectedTab);

            if (!((WizardControl)page.GetControl()).CanGoForward())
            {
                //List<AccountsCore.Entry> entries = dom.GetEntriesFromSelectedAccounts().Values;
                    
                    
                    
                    this.btnNext.Enabled = false;
                return;
            }


            ((WizardControl)page.GetControl()).OnLeave();
			
			page = page.NextPage();
			while(page != null && ((WizardControl)page.GetControl()).CanSkip())
			{
				page = page.NextPage();
			}
			

			if(page == null)
			{
                this.ParentForm.Close();
                return;
			}

			if(page.NextPage() == null)
			{
				this.btnNext.Text = "Finish";
			}

			//while(((WizardControl)nextPage.GetControl()).CanSkip() == true)
			//{
			//	nextPage = ((ControlHostWizardPage)this.wizardPages1.TabPages[wizardPages1.SelectedIndex +1]);
			//}

			((WizardControl)page.GetControl()).OnEnter();
			
			this.wizardPages1.SelectedTab = page;//Math.Min(wizardPages1.TabPages.Count -1, wizardPages1.SelectedIndex +1);
			
			
			// = Math.Min(wizardPages1.TabPages.Count -1, wizardPages1.SelectedIndex +1);

			
			//this.btnNext.Enabled = ((WizardControl)page.GetControl()).CanGoForward();
			//this.btnPrevious.Enabled = ((WizardControl)page.GetControl()).CanGoBack();			 
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			this.wizardPages1.SelectedIndex = Math.Max(0, wizardPages1.SelectedIndex -1);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{

		}		
		
		public void Initialize(AccountsCore.Accounts accounts, string filename)
		{
			dom = QifDom.ImportFile(filename);
            this.accounts = accounts;
			ctrl1.Initialize(dom);
			ctrl2.Initialize(dom, accounts);
            ctrl3.Initialize(dom, accounts);

            bool dateDetermined = (dom.YearFormat != QifDom.yearFormat.Undetermined) 
                    && (dom.DayMonthFormat != QifDom.dayMonthFormat.Undetermined);
            this.wizardPages1.SelectedIndex = dateDetermined ? 1 : 0;
		}
	}
}
