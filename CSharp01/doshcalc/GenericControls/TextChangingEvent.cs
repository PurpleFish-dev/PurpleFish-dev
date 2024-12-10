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
using System.ComponentModel;
using System;

namespace GenericControls
{
	/// <summary>
	/// Delegate for TextChanging event handler.
	/// </summary>
	public delegate void TextChangingEventHandler(
		object sender, TextChangingEventArgs e);

	/// <summary>
	/// Class for TextChanging event arguments.
	/// </summary>
	public class TextChangingEventArgs : CancelEventArgs
	{
		#region Constructors
		/// <summary>
		/// Constructor for TextChangingEventArgs (type Assignment).
		/// </summary>
		/// <param name="beforeText">Original text (before change).</param>
		/// <param name="afterText">Proposed new value (after change).</param>
		public TextChangingEventArgs (
			string beforeText,
			string afterText )
		{
			// Type is assignment
			type = TextChangingType.Assign;

			// Remember before and after values
			this.beforeText = beforeText;
			this.afterText = afterText;

			// Set change start and length to something sensible
			beforeRemoveStart = 0;
			beforeRemoveLength = beforeText.Length;
		}

		/// <summary>
		/// Constructor for TextChangingEventArgs (type is
		/// Backspace, Clear, or Cut).
		/// </summary>
		/// <param name="type">Type of change.</param>
		/// <param name="beforeText">Original text (before change).</param>
		/// <param name="beforeRemoveStart">Index of first character removed from original text.</param>
		/// <param name="beforeRemoveLength">Number of characters removed from original text.</param>
		public TextChangingEventArgs (
			TextChangingType type,
			string beforeText,
			int beforeRemoveStart,
			int beforeRemoveLength )
		{
			// If type of change is invalid, throw an exception
			if (type != TextChangingType.Backspace &&
				type != TextChangingType.Clear &&
				type != TextChangingType.Cut)
				throw new ArgumentException(
					"Invalid 'type' for TextChangingEventArgs.");

			// Remember values for arguments
			this.type = type;
			this.beforeText = beforeText;
			this.beforeRemoveStart = beforeRemoveStart;
			this.beforeRemoveLength = beforeRemoveLength;
		}

		/// <summary>
		/// Constructor for TextChangingEventArgs (type is
		/// KeyPress or Paste).
		/// </summary>
		/// <param name="type">Type of change.</param>
		/// <param name="beforeText">Original text (before change).</param>
		/// <param name="beforeRemoveStart">Index of first character removed from original text (and insertion point).</param>
		/// <param name="beforeRemoveLength">Number of characters removed from original text.</param>
		/// <param name="insertedText">Text to insert into new value.</param>
		public TextChangingEventArgs (
			TextChangingType type,
			string beforeText,
			int beforeRemoveStart,
			int beforeRemoveLength,
			string insertedText )
		{
			// If type of change is invalid, throw an exception
			if (type != TextChangingType.KeyPress &&
				type != TextChangingType.Paste)
				throw new ArgumentException(
					"Invalid 'type' for TextChangingEventArgs.");

			// Remember values for arguments
			this.type = type;
			this.beforeText = beforeText;
			this.beforeRemoveStart = beforeRemoveStart;
			this.beforeRemoveLength = beforeRemoveLength;
			this.insertedText = insertedText;
		}
		#endregion // Constructors

		#region Static data
		/// <summary>
		/// Default exception raised when Assignment is cancelled.
		/// </summary>
		public static readonly ArgumentException DefaultAssignmentException =
			new ArgumentException("Value of 'Text' is invalid.");
		#endregion // Static data

		#region Private data
		/// <summary>
		/// Exception raised when TextChangingType.Assignment is
		/// cancelled.
		/// </summary>
		private Exception assignmentException = (ArgumentException)
			DefaultAssignmentException;

		/// <summary>
		/// Number of characters removed from original text.
		/// </summary>
		private int beforeRemoveLength;

		/// <summary>
		/// Index of first character removed from original text.
		/// Also the insertion point when IsInsert is true.
		/// </summary>
		private int beforeRemoveStart;

		/// <summary>
		/// Original value of text.
		/// </summary>
		private string beforeText;

		/// <summary>
		/// The text that was inserted (when IsInsert is true).
		/// </summary>
		private string insertedText;

		/// <summary>
		/// Proposed new value of text.
		/// </summary>
		private string afterText;

		/// <summary>
		/// Type of change.
		/// </summary>
		private TextChangingType type;
		#endregion Private data

		#region Properties
		/// <summary>
		/// Gets proposed new value for text (after change).
		/// </summary>
		public string AfterText
		{
			get
			{
				// If new value is already computed, just return it
				if (afterText != null)
					return afterText;

				// If no text is being inserted...
				if (insertedText == null)
				{
					// Remove characters
					if (beforeRemoveLength > 0)
						afterText = beforeText.Remove(
							beforeRemoveStart, beforeRemoveLength);
					else
						afterText = beforeText;
				} // If no text is being inserted...
				else
				{ // ELSE: If no text is being inserted...
					// Replace characters
					if (beforeRemoveStart > 0)
						afterText = beforeText.Substring(0,
							beforeRemoveStart) + insertedText +
							beforeText.Substring(beforeRemoveStart +
							beforeRemoveLength);
					else
						afterText = insertedText +
							beforeText.Substring(beforeRemoveStart +
							beforeRemoveLength);
				} // ELSE: If no text is being inserted...

				// Return new text
				return afterText;
			}
		}

		/// <summary>
		/// Gets or sets exception raised when TextChangingType.Assignment
		/// is cancelled.
		/// </summary>
		/// <remarks>
		/// If set to null, the assignment is cancelled but no
		/// exception is raised.
		/// </remarks>
		public Exception AssignmentException
		{
			get {return assignmentException;}
			set {assignmentException = value;}
		}

		/// <summary>
		/// Gets the number of characters removed from original text.
		/// </summary>
		public int BeforeRemoveLength
		{
			get {return beforeRemoveLength;}
		}


		/// <summary>
		/// Gets the index of first character removed from original
		/// text.  Also the insertion point when IsInsert is true.
		/// </summary>
		public int BeforeRemoveStart
		{
			get {return beforeRemoveStart;}
		}

		/// <summary>
		/// Gets the original text (before change).
		/// </summary>
		public string BeforeText
		{
			get {return beforeText;}
		}

		/// <summary>
		/// Gets the text that was inserted (when IsInsert is true).
		/// </summary>
		public string InsertedText
		{
			get {return insertedText;}
		}

		/// <summary>
		/// The event is associated with text assignment.
		/// </summary>
		public bool IsAssign
		{
			get {return (type == TextChangingType.Assign);}
		}

		/// <summary>
		/// The event is associated with text deletion.
		/// </summary>
		public bool IsDelete
		{
			get {return (insertedText == null);}
		}

		/// <summary>
		/// The event is associated with text insertion.
		/// </summary>
		public bool IsInsert
		{
			get 
			{
				return (type == TextChangingType.KeyPress ||
					type == TextChangingType.Paste);
			}
		}

		/// <summary>
		/// Gets type of change.
		/// </summary>
		public TextChangingType Type
		{
			get {return type;}
		}
		#endregion // Properties
	}
}
