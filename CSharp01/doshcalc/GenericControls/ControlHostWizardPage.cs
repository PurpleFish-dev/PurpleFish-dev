using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenericControls
{
	public class ControlHostWizardPage : ControlHostTabPage
	{
		private ControlHostWizardPage _previousPage;
		private ControlHostWizardPage _nextPage;

		//public ControlHostWizardPage(){}
		//public ControlHostWizardPage(Control control, string title) : base(control, title) {}
		public ControlHostWizardPage(ControlHostWizardPage previousPage, Control control, string title) : base(control, title) 
		{
			_previousPage = previousPage;
			if(_previousPage != null)
			{
				_previousPage._nextPage = this; 
			}
		}

		public ControlHostWizardPage NextPage()
		{
			return _nextPage;
		}
	}
}
