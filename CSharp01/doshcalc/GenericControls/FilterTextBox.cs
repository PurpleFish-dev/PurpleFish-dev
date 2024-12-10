/******************************************************************************
* Copyright © 2007 Transeric Solutions.  All Rights Reserved.
* Author: Eric David Lynch
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the company Transeric Solutions nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY Transeric Solutions "AS IS" AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL Transeric Solutions BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
******************************************************************************/
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GenericControls
{
	/// <summary>
	/// Text box with the ability to filter text so that 
	/// invalid values do not become visible.
	/// </summary>
	public class FilterTextBox : TextBox
	{
		#region Constructors
		/// <summary>
		/// Constructor for FilterTextBox.
		/// </summary>
		public FilterTextBox ()
		{
		}
		#endregion // Constructors

		#region Windows message constants
		/// <summary>
		/// Windows message sent when user cuts text.
		/// </summary>
		private const int WM_CUT = 0x300;

		/// <summary>
		/// Windows message sent when user copies text.
		/// </summary>
		private const int WM_COPY = 0x301;

		/// <summary>
		/// Windows message sent when user pastes text.
		/// </summary>
		private const int WM_PASTE = 0x302;

		/// <summary>
		/// Windows message sent when user deletes text.
		/// </summary>
		private const int WM_CLEAR = 0x303;
		#endregion // Windows message constants

		#region Events
		/// <summary>
		/// Occurs when text is changing, but before it has changed.
		/// </summary>
		[Category("Property Changed")]
		[Description("Event fired when the value of Text property is changing on Control, but before it is changed.")]
		public event TextChangingEventHandler TextChanging;
		#endregion // Events

		#region Properties
		/// <summary>
		/// Gets the current position of the caret within the
		/// text box.
		/// </summary>
		public int CaretPosition
		{
			get {return SelectionStart + SelectionLength;}
		}

		/// <summary>
		/// Override of Text property to validate changes by
		/// assignment to the property.
		/// </summary>
		public override string Text
		{
			get {return base.Text;}
			set
			{
				// Get new value
				string newValue = value;

				// Necessary because Clear() uses null value
				// instead of empty string.
				if (newValue == null)
					newValue = string.Empty;

				// Create event arguments
				TextChangingEventArgs eventArgs =
					new TextChangingEventArgs(base.Text, newValue);

				// Notify subscribers
				OnTextChanging(eventArgs);

				// If event was cancelled...
				if (eventArgs.Cancel)
				{
					// If excuting in DesignMode, just return
					if (DesignMode)
						return;

					// Get exception for assignment
					Exception assignmentException =
						eventArgs.AssignmentException;

					// If exception is requested, throw it
					if (assignmentException != null)
						throw assignmentException;

					// Return without changing value
					return;
				} // If event was cancelled...

				// Change the value
				base.Text = value;
			}
		}
		#endregion // Properties

		#region Methods to handle events
		/// <summary>
		/// Invoked when key is pressed down.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		/// <remarks>
		/// The Delete key is intercepted here because no
		/// KeyPress event occurs for a Delete key.
		/// </remarks>
		protected override void OnKeyDown (
			KeyEventArgs e )
		{
			// If key is not Delete or is combined with Shift or Alt...
			if (e.KeyCode != Keys.Delete ||
				(e.Modifiers & (Keys.Shift | Keys.Alt)) != 0)
			{
				// Notify other subscribers and return
				base.OnKeyDown(e);
				return;
			} // If key is not Delete or is combined with Shift or Alt...

			// Get current text
			string current = base.Text;

			// Assume we are deleting selection text
			int removeStart = SelectionStart;
			int removeLength = SelectionLength;

			// If no text is selected...
			if (removeLength == 0)
			{
				// If caret is at end of text...
				if (removeStart == current.Length)
				{
					// Notify other subscribers and return
					base.OnKeyDown(e);
					return;
				} // If caret is at end of text...

				// Remove next character
				removeLength = 1;
			} // If no text is selected...

			// Create event arguments
			TextChangingEventArgs eventArgs =
				new TextChangingEventArgs(
				TextChangingType.Clear, current,
				removeStart, removeLength);

			// Notify subscribers
			OnTextChanging(eventArgs);

			// If event was cancelled...
			if (eventArgs.Cancel)
			{
				// Beep and eat the key
				TextChangingWarn(TextChangingType.Clear);
				e.Handled = true;
			} // If event was cancelled...

			// Notify other subscribers
			base.OnKeyDown(e);
		}

		/// <summary>
		/// Invoked when key is pressed.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		/// <remarks>
		/// This event is intercepted so that invalid characters
		/// are filtered before they are displayed.  Another
		/// option is to override the Validating event.  However,
		/// the Validating event does not fire until focus is
		/// leaving the control.  So, users would see the invalid
		/// characters until that point.  This solution is a bit
		/// ugly from a developer standpoint; but, in my opinion,
		/// it yields a much cleaner user interface.
		/// </remarks>
		protected override void OnKeyPress (
			KeyPressEventArgs e )
		{
			// Get the key that was pressed
			char keyChar = e.KeyChar;

			// If key is not a control character...
			if ( !Char.IsControl(keyChar) )
			{
				// Create event arguments
				TextChangingEventArgs eventArgs =
					new TextChangingEventArgs(
					TextChangingType.KeyPress, base.Text,
					SelectionStart, SelectionLength,
					keyChar.ToString());

				// Notify subscribers
				OnTextChanging(eventArgs);

				// If event was cancelled...
				if (eventArgs.Cancel)
				{
					// Beep and eat character (so it is not inserted)
					TextChangingWarn(TextChangingType.KeyPress);
					e.Handled = true;
				} // If event was cancelled...
			} // If key is not a control character...
			else
			{ // ELSE: If key is not a control character...
				// If BACKSPACE key is pressed...
				if (keyChar == (char) Keys.Back)
				{
					// Assume we are deleting selection text
					int removeStart = SelectionStart;
					int removeLength = SelectionLength;

					// If caret is not at start of text...
					if (removeStart > 0 || removeLength > 0)
					{
						// If no text is selected...
						if (removeLength == 0)
						{
							// Remove the previous character
							removeLength = 1;
							removeStart--;
						} // If no text is selected...

						// Create event arguments
						TextChangingEventArgs eventArgs =
							new TextChangingEventArgs(
							TextChangingType.Backspace, base.Text,
							removeStart, removeLength);

						// Notify subscribers
						OnTextChanging(eventArgs);

						// If event was cancelled...
						if (eventArgs.Cancel)
						{
							// Beep and eat character (so it is not processed)
							TextChangingWarn(TextChangingType.Backspace);
							e.Handled = true;
						} // If event was cancelled...
					} // If caret is not at start of text...
				} // If BACKSPACE key is pressed...
			} // ELSE: If key is not a control character...

			// Notify other subscribers
			base.OnKeyPress(e);
		}

		/// <summary>
		/// Invoked when text is changing, but before it changes.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected virtual void OnTextChanging (
			TextChangingEventArgs e )
		{
            // If we have subscribers to the event, notify them
            TextChanging?.Invoke(this, e);
        }

		/// <summary>
		/// Method invoked when Windows sends a message.
		/// </summary>
		/// <param name="m">Message from Windows.</param>
		/// <remarks>
		/// This is over-ridden so that the user can not use
		/// cut or paste operations to bypass the TextChanging event.
		/// This catches ContextMenu Paste, Shift+Insert, Ctrl+V,
		/// Cut method, ContextMenu Cut, Shift+Delete, and Ctrl+X.
		/// While it is generally frowned upon to override WndProc, no
		/// other simple mechanism was apparent to simultaneously and
		/// transparently intercept so many different operations.
		/// </remarks>
		protected override void WndProc (
			ref Message m )
		{
			// Switch to handle message...
			switch(m.Msg)
			{
				case WM_CLEAR:
				{
					// Get the number of characters to remove
					int selectionLength = SelectionLength;

					// If no characters are removed, we are done
					if (selectionLength == 0)
						break;

					// Create event arguments
					TextChangingEventArgs eventArgs =
						new TextChangingEventArgs(
						TextChangingType.Clear, base.Text,
						SelectionStart, selectionLength);

					// Notify subscribers
					OnTextChanging(eventArgs);

					// If event was cancelled...
					if (eventArgs.Cancel)
					{
						// Beep and return without cutting
						TextChangingWarn(TextChangingType.Clear);
						return;
					} // If event was cancelled...
					break;
				}

				case WM_CUT:
				{
					// Get the number of characters to remove
					int selectionLength = SelectionLength;

					// If no characters are removed, we are done
					if (selectionLength == 0)
						break;

					// Create event arguments
					TextChangingEventArgs eventArgs =
						new TextChangingEventArgs(
						TextChangingType.Cut, base.Text,
						SelectionStart, selectionLength);

					// Notify subscribers
					OnTextChanging(eventArgs);

					// If event was cancelled...
					if (eventArgs.Cancel)
					{
						// Beep and return without cutting
						TextChangingWarn(TextChangingType.Cut);
						return;
					} // If event was cancelled...
					break;
				}

				case WM_PASTE:
				{
					// Get clipboard object to paste
					IDataObject clipboardData = Clipboard.GetDataObject();

					// Get text from clipboard data
					string pasteText = (string) clipboardData.GetData(
						DataFormats.UnicodeText);

					// Get the number of characters to replace
					int selectionLength = SelectionLength;

					// If no replacement or insertion, we are done
					if (selectionLength == 0 && pasteText.Length == 0)
						break;

					// Create event arguments
					TextChangingEventArgs eventArgs =
						new TextChangingEventArgs(
						TextChangingType.Paste, base.Text,
						SelectionStart, selectionLength, pasteText);

					// Notify subscribers
					OnTextChanging(eventArgs);

					// If event was cancelled...
					if (eventArgs.Cancel)
					{
						// Beep and return without pasting
						TextChangingWarn(TextChangingType.Paste);
						return;
					} // If event was cancelled...
					break;
				}
			} // Switch to handle message...

			// Allow base class to handle message
			base.WndProc(ref m);
		}
		#endregion // Methods to handle events

		#region Protected methods
		/// <summary>
		/// Method used to warn user when TextValidating is cancelled.
		/// </summary>
		protected virtual void TextChangingWarn (
			TextChangingType type )
		{
			MessageBeep(0);
		}
		#endregion // Protected methods

		#region Private imported methods
		/// <summary>
		/// Plays a waveform sound.
		/// </summary>
		/// <param name="uType">Type of beep.</param>
		/// <returns>True, for success; otherwise, false.</returns>
		[DllImport("User32.dll")]
		private static extern bool MessageBeep(
			uint uType);
		#endregion // Private imported methods
	}
}
