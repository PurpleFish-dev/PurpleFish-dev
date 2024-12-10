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
	public partial class DecimalTextBox : FilterTextBox
	{
		public DecimalTextBox()
		{
			InitializeComponent();
		}

		protected override void OnTextChanging (
			TextChangingEventArgs e )
		{
			// If not cancelling and not deleting...
			if (!e.Cancel && !e.IsDelete)
			{
				// Get text we need to validate
				string text;
				if (e.IsAssign)
					text = e.AfterText;
				else
					text = e.InsertedText;

				// Loop to validate text...
				foreach(char charValue in text)
				{
					// If character is not a number...
					if ( (!Char.IsDigit(charValue) && charValue != '.') || (e.BeforeText.Contains('.') && (charValue == '.')) )
					{
						// Cancel the event
						e.Cancel = true;
						break;
					} // If character is not a number...
				} // Loop to validate text...
			} // If not cancelling and not deleting...

			// Notify other subscribers
			base.OnTextChanging(e);
		}
		
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}
	}
}
