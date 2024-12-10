using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
	public partial class ImportWizardForm : Form
	{
		public ImportWizardForm(AccountsCore.Accounts _accounts, string filename)
		{
			InitializeComponent();
			this.importWizard.Initialize(_accounts, filename);
		}
	}
}
