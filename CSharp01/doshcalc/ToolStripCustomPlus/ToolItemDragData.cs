using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolStripCustomCtrls
{
    public class ToolItemDragData
    {
        public string ToolBarName
        {
            get { return _toolBarName; }
            set { _toolBarName = value; }
        } private string _toolBarName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        } private string _itemName;

        public Type type
        {
            get { return _type; }
            set { _type = value; }
        } private Type _type;

        public bool Inserting
        {
            get { return _inserting; }
            set { _inserting = value; }
        } private bool _inserting;
    }
}
