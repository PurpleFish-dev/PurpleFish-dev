using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
	public class EditorFactoryListView : UserControl
	{		
		public virtual Control CreateEditor()
		{
			return null;
		}
	}
}
