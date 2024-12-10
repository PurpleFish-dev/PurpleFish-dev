using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace GenericControls
{
	public class ControlHostTabPage : TabPage
	{
		private Control _control;	
		public ControlHostTabPage(Control control, string title)
		{
			this._control = control;
			this._control.Parent = this;		
			this._control.Dock = System.Windows.Forms.DockStyle.Fill;		
			this.Text = title;	
		}
	
		public Control GetControl()
		{
			return this._control;
		}
	}
}
