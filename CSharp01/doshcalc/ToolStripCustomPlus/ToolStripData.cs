using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ToolStripCustomCtrls
{
    [Serializable]
	public class ToolStripData
	{
	  
        private enum FlagFields
		{
			Separator = 1,
			Visible   = 2
		}
		
		public ToolStripData()
		{
		}
		
		private static void UpdateData(ToolStripCustom ts, 
                                    out string[] Names, 
                                    out Byte[] Flagss, 
                                    out ToolStripItemDisplayStyle displayStyle, 
                                    out bool largeIcons )
        {
            displayStyle = ts.DisplayStyle;
            largeIcons = ts.LargeIcons;
            UpdateData(ts, out Names, out Flagss);
        }

        private static bool UpdateData(ToolStripCustom ts, out string[] names, out Byte[] flagss)
        {
            names = new string[ts.Items.Count];
            flagss = new byte[ts.Items.Count];

            for (int n = 0; n < ts.Items.Count; n++)
            {
                ToolStripItem item = ts.Items[n];
                names[n] = ts.Items[n].Name;
                if (ts.Items[n] is ToolStripSeparator)
                {
                    flagss[n] = (byte)FlagFields.Separator;
                }
                if (ts.Items[n].Available == false)
                {
                    flagss[n] = (byte)(flagss[n] &~ (byte)FlagFields.Visible);
				}
                else
                {
                    flagss[n] = (byte)(flagss[n] | (byte)FlagFields.Visible);
                }
            }
            return true;
        }

        private static bool UpdateToolStrip(ToolStripCustom ts, string[] names, Byte[] flagss, ToolStripItemDisplayStyle displayStyle, bool largeIcons)
        {
            bool bResult = UpdateToolStrip(ts, names, flagss);
            if (bResult)
            {
                ts.DisplayStyle = displayStyle;
                ts.LargeIcons = largeIcons;
            }
            return bResult;  
        }

        private static bool UpdateToolStrip(ToolStripCustom ts, string[] names, Byte[] flagss)
        {
            if ((names == null) || (flagss == null))
            {
                return false;
            }
            
            //check the data matches the tools in the toolbar,

            //if a tool in the toolstrip other than a separator is not in the registry
            //ignor the registry settings and use the tool bar without customization                        
            int nLoopTSItems = 0;
            while (nLoopTSItems < ts.Items.Count)
            {
                if (ts.Items[nLoopTSItems] is ToolStripSeparator == false)
                {
                    int nLoop = 0;
                    bool bFound = false;
                    while ((nLoop < names.Length) && !bFound)
                    {
                        if (names[nLoop] == ts.Items[nLoopTSItems].Name)
                        {
                            bFound = true;
                        }
                        nLoop++;
                    }

                    if (!bFound)
                    {
                        return false;
                    }
                }
                nLoopTSItems++;
            }

            //if a tool in the registry is not in the toolstrip
            int nLoopRegItems = 0;
            while (nLoopRegItems < names.Length)
            {
                if ( (flagss[nLoopRegItems] & (byte)FlagFields.Visible) > 0)
                {
                    const bool bSearchAllChilden = false;
                    if (ts.Items.Find(names[nLoopRegItems], bSearchAllChilden) == null)
                    {
                        return false;
                    }
                }
                nLoopRegItems++;
            } 
            
            ts.SuspendLayout();

            //create an array of all the toolstrip items then remove them from the toolstrip
            ToolStripItem[] OldTSItems = new ToolStripItem[ts.Items.Count];
            for (int n = 0; n < OldTSItems.Length; n++)
            {
                OldTSItems[n] = ts.Items[n];
            }
            ts.Items.Clear();

			//if there is an invisible separator in the toolstrip 

            //Add the toolstrip items back to the toolstrip according to the registry data
            for (nLoopRegItems = 0; nLoopRegItems < names.Length; nLoopRegItems++)
            {
                if ( (flagss[nLoopRegItems] & (byte)FlagFields.Separator) > 0)
                {
                    ToolStripSeparator tss = new ToolStripSeparator();
                    tss.Name = names[nLoopRegItems];

                    if ( (flagss[nLoopRegItems] & (byte)FlagFields.Visible) == 0)
                    {
                        tss.Available = false;
                    }
                    else if ( (flagss[nLoopRegItems] & (byte)FlagFields.Visible) > 0)
                    {
                        tss.Available = true;
                    }
                    else
                    {
                        //invalid flag
                        return false;
                    }					
					
                    ts.Items.Add(tss);
                }
                else
                {
                    //other item
                    int nloopTSItems = 0;
                    while ((nloopTSItems < (OldTSItems.Length - 1)) && (names[nLoopRegItems] != ((ToolStripItem)OldTSItems[nloopTSItems]).Name))
                    {
                        nloopTSItems++;
                    }
                    
					if ( (flagss[nLoopRegItems] & (byte)FlagFields.Visible) == 0)
                    {
                        ((ToolStripItem)OldTSItems[nloopTSItems]).Available = false;
                    }
                    else if ( (flagss[nLoopRegItems] & (byte)FlagFields.Visible) > 0)
                    {
                        ((ToolStripItem)OldTSItems[nloopTSItems]).Available = true;
                    }
                    else
                    {
                        //invalid flag
                        return false;
                    }

                    ts.Items.Add(((ToolStripItem)OldTSItems[nloopTSItems]));
                }
            }
            ts.ResumeLayout();
            return true;
        }

        protected string[] _userNames;
        protected Byte[] _userFlagss;
        protected ToolStripItemDisplayStyle _userDisplayStyle;
        protected bool _userLargeIcons;

        [NonSerialized]private string[] _defaultNames;
        [NonSerialized]private Byte[] _defaultFlagss;
        
        internal void RestoreDefaultLayout(ToolStripCustom ts)
        {
            UpdateToolStrip(ts, _defaultNames, _defaultFlagss);
        }

        internal void SaveDefaultLayout(ToolStripCustom ts)
        {
            UpdateData(ts, out _defaultNames, out _defaultFlagss);
        }

        internal void SaveUserLayoutAndStyles(ToolStripCustom ts)
        {
            UpdateData(ts, out _userNames, out _userFlagss, out _userDisplayStyle, out _userLargeIcons);
        }

        internal bool RestoreUserLayoutAndStyles(ToolStripCustom ts)
        {
            return UpdateToolStrip(ts, _userNames, _userFlagss, _userDisplayStyle, _userLargeIcons);
        }

		private string _appRegKey;
		private string _toolbarRegKey;

        internal bool SaveUserLayoutToRegistry()
        {
            if (    (_appRegKey != null) &&
                    (_appRegKey.Length > 0) &&
                    (_toolbarRegKey != null) &&
                    (_toolbarRegKey.Length > 0)    )
            {
                //get ts data from the registry
                RegistryKey regKeyToolStrip = Registry.CurrentUser.CreateSubKey(_appRegKey + "\\" + _toolbarRegKey);

                regKeyToolStrip.SetValue("Names", _userNames);
                regKeyToolStrip.SetValue("Flags", _userFlagss);

                regKeyToolStrip.SetValue("LargeIcons", _userLargeIcons);

                string s = _userDisplayStyle.ToString();
                regKeyToolStrip.SetValue("DisplayStyle", s);  
				return true;              
            }
			return false;
        }

        internal bool LoadUserLayoutFromRegistry(string applicationKey, string toolStripKey)
        {
            _appRegKey = applicationKey;
			_toolbarRegKey = toolStripKey;
			
			
			if ((_appRegKey == null) ||
                    (_appRegKey.Length == 0) ||
                    (_toolbarRegKey == null) ||
                    (_toolbarRegKey.Length == 0)    )
            {
                return false;
            }
                
            //load the data from the memory
            RegistryKey regKeyToolStrip = Registry.CurrentUser.CreateSubKey(_appRegKey + "\\" + _toolbarRegKey);
            _userNames = (string[])regKeyToolStrip.GetValue("Names");
            _userFlagss = (Byte[])regKeyToolStrip.GetValue("Flags");

            if (_userNames == null)
            {
                 return true; //no setting in reg yet
            }

            _userLargeIcons = Boolean.Parse((string)regKeyToolStrip.GetValue("LargeIcons"));

            string sDisplayStyle = (string)regKeyToolStrip.GetValue("DisplayStyle");
            _userDisplayStyle = (ToolStripItemDisplayStyle)Enum.Parse(typeof(ToolStripItemDisplayStyle), sDisplayStyle);

            return true;
        }   
    }
}
