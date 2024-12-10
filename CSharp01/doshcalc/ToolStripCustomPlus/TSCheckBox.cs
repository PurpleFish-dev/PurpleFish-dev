using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToolStripCustomCtrls
{

internal class ToolStripCheckBox : ToolStripControlHost
{
    public ToolStripCheckBox() : base(new CheckBox()) {}    

    public CheckBox CheckBoxControl
    {
        get
        {
            return Control as CheckBox;
        }
    }    
}

internal class ToolStripStandardButton : ToolStripControlHost
{
    public ToolStripStandardButton() : base(new Button()) { }

    public Button ButtonControl
    {
        get
        {
            return Control as Button;
        }
    }
}
/*
internal class ToolStripPanel : ToolStripControlHost
{
    public ToolStripPanel() : base(new Panel()) { }
    public Panel PanelControl 
    {
        get
        {
            return Control as Panel;
        }
    }
}
*/

    /*
//Declare a class that inherits from ToolStripControlHost.
public class ToolStripMonthCalendar : ToolStripControlHost
{
    // Call the base constructor passing in a MonthCalendar instance.
    public ToolStripMonthCalendar() : base(new MonthCalendar()) { }

    public MonthCalendar MonthCalendarControl
    {
        get
        {
            return Control as MonthCalendar;
        }
    }

    // Expose the MonthCalendar.FirstDayOfWeek as a property.
    public Day FirstDayOfWeek
    {
        get
        {
            return MonthCalendarControl.FirstDayOfWeek;
        }
        set { value = MonthCalendarControl.FirstDayOfWeek; }
    }

    // Expose the AddBoldedDate method.
    public void AddBoldedDate(DateTime dateToBold)
    {
        MonthCalendarControl.AddBoldedDate(dateToBold);
    }

    // Subscribe and unsubscribe the control events you wish to expose.
    protected override void OnSubscribeControlEvents(Control c)
    {
        // Call the base so the base events are connected.
        base.OnSubscribeControlEvents(c);

        // Cast the control to a MonthCalendar control.
        MonthCalendar monthCalendarControl = (MonthCalendar)c;

        // Add the event.
        monthCalendarControl.DateChanged +=
            new DateRangeEventHandler(OnDateChanged);
    }

    protected override void OnUnsubscribeControlEvents(Control c)
    {
        // Call the base method so the basic events are unsubscribed.
        base.OnUnsubscribeControlEvents(c);

        // Cast the control to a MonthCalendar control.
        MonthCalendar monthCalendarControl = (MonthCalendar)c;

        // Remove the event.
        monthCalendarControl.DateChanged -=
            new DateRangeEventHandler(OnDateChanged);
    }

    // Declare the DateChanged event.
    public event DateRangeEventHandler DateChanged;

    // Raise the DateChanged event.
    private void OnDateChanged(object sender, DateRangeEventArgs e)
    {
        if (DateChanged != null)
        {
            DateChanged(this, e);
        }
    }
}
     * */
}