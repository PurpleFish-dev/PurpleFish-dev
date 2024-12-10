using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountsCore;


namespace WindowsFormsControlLibrary1
{
	public class TagString
	{
		private Id _id;
		private string _name;
		public TagString(Id id, string name)
		{
			_id = id;
			_name = name;
		}
		public override string ToString() {return _name;}
		public Id Id {get{return _id;}}
		
		public static TagString FindItem(ComboBox cb, Id id)
		{
			for (int i = 0; i < cb.Items.Count; i++)
			{
				TagString obj = (TagString)cb.Items[i];  
				if (obj.Id == id)   
				{
					return obj;
				}
			}
			return null;
		}	
	}
}