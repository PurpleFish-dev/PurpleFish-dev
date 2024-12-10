using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
	public partial class sliderpanel : Panel
	{
		private Control _ctrl;
		public void ClearControls()
		{
			if(_ctrl != null)
			{
				_ctrl.Parent =null;
				_ctrl.Visible =false;
			}
		}
		public void ReplaceCtrl(Control ctrl)
		{
			//_ctrl.un
			
			
			_ctrl = ctrl;
			_ctrl.Parent = this;
			_ctrl.Visible = true;
			_ctrl.Dock = DockStyle.Top;
		}
		
		public sliderpanel()
		{
			InitializeComponent();
		}

		public sliderpanel(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}


	}
}
