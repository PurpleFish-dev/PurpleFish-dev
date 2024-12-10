using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication6
{
	//Declare a class that inherits from ToolStripControlHost.
	/*
	 * public class TSHost : ToolStripControlHost
	{
		// Call the base constructor passing in a MonthCalendar instance.
		public TSHost() : base (new EntryCtrl()) { }

		public EntryCtrl InnerControl
		{
			get
			{
				return Control as EntryCtrl;
			}
		}
	}
	 * */

	//Declare a class that inherits from ToolStripControlHost.
	public class ToolStripMonthCalendar : ToolStripControlHost
	{
		// Call the base constructor passing in a MonthCalendar instance.
		public ToolStripMonthCalendar() : base (new MonthCalendar()) { }

		public MonthCalendar MonthCalendarControl
		{
			get
			{
				return Control as MonthCalendar;
			}
		}
	}




}
