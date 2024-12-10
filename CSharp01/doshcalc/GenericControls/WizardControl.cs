using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenericControls
{
	public partial class WizardControl : UserControl
	{
		public WizardControl()
		{
			InitializeComponent();
		}

        public virtual void OnLeave() { }

		public virtual bool CanSkip()
		{
			return false;
		}

		public virtual bool OnEnter()
		{
			return true;
		}

		public virtual bool CanGoForward()
		{
			return true;
		}

		public virtual bool CanGoBack()
		{
			return true;
		}
	}
}
