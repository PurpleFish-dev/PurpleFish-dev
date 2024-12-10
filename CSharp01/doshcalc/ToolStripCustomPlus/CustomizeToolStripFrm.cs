using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;




namespace ToolStripCustomCtrls
{
    public partial class CustomizeToolStripForm : Form
    {
        private ToolStripCustom _ts;
        private Cursor _itemCursor;
        private ToolStripCheckBox _tsCheckBox;
        private ToolStripStandardButton _tsDefaultButton;
        private List<Control> _disabledCtrls = new List<Control>();

        public CustomizeToolStripForm(ToolStripCustom ts)
        {           
            _ts = ts;
            InitializeComponent();

            switch(_ts.ListViewDisplayStyle)
            {
                case ListViewDisplayStyle.Tiles:
                {
                    this.listView1.View = View.LargeIcon;
                    break;
                }
                case ListViewDisplayStyle.List:
                {
                    this.listView1.View = View.List; 
                    break;
                }
            }

            if (this._ts.ImageSizeSelection)
            {
                _tsCheckBox = new ToolStripCheckBox();
                _tsCheckBox.Control.Text = @"Use Small Icons";
                Padding p = _tsCheckBox.Padding;
                p.Left += 24;
                p.Right = 0;
                _tsCheckBox.Padding = p;
                this._tsCheckBox.Click += new System.EventHandler(this.tsCheckBox_Click);
                this.toolStrip1.Items.Add(_tsCheckBox);
                
                if (this._ts.LargeIcons)
                {
                    this._tsCheckBox.CheckBoxControl.Checked = false;
                }
                else
                {
                    this._tsCheckBox.CheckBoxControl.Checked = true;                    
                }                
            }
            
            if (this._ts.DefaultButton)
            {
                _tsDefaultButton = new ToolStripStandardButton();
                _tsDefaultButton.Control.Text = @"Restore default tools";
                Padding p = _tsDefaultButton.Padding;
                p.Left += 24;
                p.Right = 0;
                _tsDefaultButton.Padding = p;
                this._tsDefaultButton.Click += new System.EventHandler(this.tsDefaultButton_Click);
                this.toolStrip1.Items.Add(_tsDefaultButton);
            }
            repopulateAvailableToolsList();
        }

        //disable all the other forms in the application except the toolstrip 
        //and it's parents, and show this from

        public void ShowPsudoModal()
        {                        
            Control p = this._ts.Parent;
            while(p != null)
            {                
                foreach (Control c in p.Controls)
                {
                    if (!c.Enabled)
                        _disabledCtrls.Add(c); 
                    c.Enabled = false; 
                }

                p = p.Parent;
            }

            _ts.Enabled = true;
            p = this._ts.Parent;
            
            while (p != null)
            {
                p.Enabled = true;                
                p = p.Parent;
            }            
            
            base.Show();
        }
		
		private void tsCheckBox_Click(object sender, EventArgs e)
        {
            bool bLarge =false;
            if (!((ToolStripCheckBox)sender).CheckBoxControl.Checked)
            {
                bLarge =true;
            }
            this._ts.LargeIcons = bLarge;
            repopulateAvailableToolsList();
        }

        private void tsDefaultButton_Click(object sender, EventArgs e)
        {
            this._ts.RestoreDefaultLayout();
            repopulateAvailableToolsList();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {            
            Control p = this._ts.Parent;
            while(p != null)
            {
                p.Enabled = true;
                foreach (Control c in p.Controls)
                { c.Enabled = true; }
                p = p.Parent;
            }

            foreach(Control c in _disabledCtrls)
            {
                c.Enabled =false;
            }
           
            base.OnClosing(e);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ToolItemDragData)))
            {
                e.Effect = DragDropEffects.Move; 
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(typeof(ToolItemDragData)) == true)
            {
                ToolItemDragData tidd = ((ToolItemDragData)e.Data.GetData(typeof(ToolItemDragData)));       
                
				if(tidd.type != typeof(ToolStripSeparator))
                {
                    const bool bSearchAllChildren = false;
					ToolStripItem[] items = _ts.Items.Find(tidd.ItemName, bSearchAllChildren);
					Debug.Assert(items.Length  == 1);
					ToolStripItem item = items[0];
					ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = item.Text;
                    lvItem.Name = item.Name;
                    lvItem.ImageKey = item.Name;
                    lvItem.ToolTipText = item.ToolTipText;

                    int nfound = this.listView1.Items.IndexOfKey(item.Name);
                    if (nfound == -1)
                    {
                        this.listView1.Items.Add(lvItem);
                    }
                }
            }
        }
        
        private void CustomizeToolStripFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool keepChanges = true;
            _ts.EndCustomize(keepChanges);
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {
                ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
				//start drag if we are over a listview item
                if (item != null)
                {
                    ToolItemDragData tidd = new ToolItemDragData();
                    tidd.Inserting = true;
                    tidd.ItemName = item.Name;
                    tidd.ToolBarName = this._ts.Name;
					tidd.type = (Type)item.Tag;

                   //get the item to be dragged from the toolbar 
				   //if the item we have started dragging is not a separator 
				   //remove it from the listview
				   ToolStripItem tsi;
				   if (tidd.type == typeof(ToolStripSeparator))
                    {
                        tsi = GetFirstSeparatorInToolBar();												
                    }
					else
                    {
                        item.Remove();
                        tsi = this._ts.Items[item.Name];                        
                    }
					Debug.Assert(tsi != null); //listview is populated from the toolstrip this should not be possible

					Image itemImage = this._ts.CreateItemImage(tsi);
                    _itemCursor = ToolStripCustom.GetItemCursor(itemImage);
                       
                    DragDropEffects dropEffect = this.DoDragDrop(tidd, DragDropEffects.Move);

                    //if the item is not a separator and is dropped back onto the listview add it to the list again
					if ((Type)item.Tag != typeof(ToolStripSeparator))
                    {
                        if (dropEffect == DragDropEffects.None)
                        {
                            this.listView1.Items.Add(item);
                        }
                    }
                }
            }
        }

		//set the current cursor to the cursor created from the ts item
		//default cursors if custom cursor creation failed
        private void CustomizeToolStripFrm_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (_itemCursor == null)
            {
                e.UseDefaultCursors = true;
            }
            else
            {
                e.UseDefaultCursors = false;
                Cursor.Current = _itemCursor;
            }
        }
           
        //clears the items in the forms listview and repopulates it from the tools in the toolbar
		private void repopulateAvailableToolsList()
        {
            _ts.SuspendLayout();
            
            const int nImageExpand = 6;  //todo make design time property
            this.lvImages.Images.Clear();

            Size lvImageSize = new Size(this._ts.ImageScalingSize.Width + nImageExpand, this._ts.ImageScalingSize.Height + nImageExpand);
                        
            if(this.listView1.View == View.LargeIcon)
            {
                lvImageSize.Width = this.listView1.TileSize.Width;
            }            
			 
            this.lvImages.ImageSize = lvImageSize;  
			
			foreach (ToolStripItem item in _ts.Items)
            {
                this.lvImages.Images.Add(item.Name, this._ts.CreateReCanvasedItemImage(item, this.lvImages.ImageSize));
            }

            this.listView1.SmallImageList = this.lvImages;
            this.listView1.LargeImageList = this.lvImages;
            
            //this.listView1.TileSize = new Size(this.listView1.SmallImageList.ImageSize.Width + 10, this.listView1.SmallImageList.ImageSize.Height + 10);
            this.listView1.Items.Clear();

            //find the first separator in the toolbar
			ToolStripSeparator separator = GetFirstSeparatorInToolBar();

            if (separator != null)
            {
				ToolStripSeparator sep = separator as ToolStripSeparator;
				ListViewItem lvItem = new ListViewItem();
                lvItem.Text = "Separator";
                lvItem.Name = separator.Name;
                lvItem.ImageKey = "Separator";
                lvItem.ToolTipText = separator.ToolTipText;
				lvItem.Tag = separator.GetType();
                this.listView1.Items.Add(lvItem);

				this.lvImages.Images.Add("Separator", _ts.CreateReCanvasedItemImage(sep, this.lvImages.ImageSize));				
			}

            foreach (ToolStripItem item in _ts.Items)
            {
                if ( (item.Available == false) && (item is ToolStripSeparator == false))
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = item.Text;
                    lvItem.Name = item.Name;
                    lvItem.ImageKey = item.Name;
                    lvItem.ToolTipText = item.ToolTipText;
					lvItem.Tag = item.GetType();
                    this.listView1.Items.Add(lvItem);
                }
            }
            _ts.ResumeLayout();
        }

		private ToolStripSeparator GetFirstSeparatorInToolBar()
		{
			int n =0;            
            while ((n < _ts.Items.Count))
            {
                if (_ts.Items[n] is ToolStripSeparator)
                {
				    return (ToolStripSeparator)_ts.Items[n];
				}
                n++;
            }
			return null;
		}

        internal void SetLocationRelativeToToolStrip()
        {
            const int gap = 40;
            
            Rectangle screenBounds = Screen.GetBounds(_ts);
            Point screenCenter = new Point(screenBounds.Left + (screenBounds.Width / 2), screenBounds.Top + (screenBounds.Height / 2));
            Rectangle toolStripScrBnds = _ts.Parent.RectangleToScreen(_ts.Bounds);
            Point toolSrtipCenter = new Point(toolStripScrBnds.Left + (toolStripScrBnds.Width / 2), toolStripScrBnds.Top + (toolStripScrBnds.Height / 2));

            //if the toolstrip is horizontal and below center screen 
            //display the dialog above the toolstrip
            Point newLocation = new Point();
            if ((_ts.LayoutStyle == ToolStripLayoutStyle.HorizontalStackWithOverflow) || (_ts.LayoutStyle == ToolStripLayoutStyle.Flow))            
            {
                if(toolSrtipCenter.Y > screenCenter.Y)
                {
                    newLocation.Y = toolStripScrBnds.Top - gap - this.Bounds.Height;
                    newLocation.X = toolStripScrBnds.Left;
                }
                else //display the dialog below of the toolstrip
                {
                    newLocation.Y = toolStripScrBnds.Bottom + gap;
                    newLocation.X = toolStripScrBnds.Left;
                }
            }
            else //display the dialog to the left or right of the toolstrip
            {
                if (toolSrtipCenter.X > screenCenter.X)
                {
                    newLocation.Y = toolStripScrBnds.Top - (toolStripScrBnds.Height / 2);
                    newLocation.X = toolStripScrBnds.Left - gap - this.Bounds.Width;
                }
                else
                {
                    newLocation.Y = toolStripScrBnds.Top - (toolStripScrBnds.Height / 2);
                    newLocation.X = toolStripScrBnds.Right + gap;
                }
            }

            //move the dialog in to ensure that the whole dialog is within the screen
            if (newLocation.X < 0)
            {
                newLocation.X = 0;
            }
            if (newLocation.Y < 0)
            {
                newLocation.Y = 0;
            }
            if ((newLocation.X + this.Bounds.Width) > screenBounds.Right)
            {
                newLocation.X = screenBounds.Right - this.Bounds.Width;
            }
            if ((newLocation.Y + this.Bounds.Height) > screenBounds.Bottom)
            {
                newLocation.Y = screenBounds.Bottom - this.Bounds.Height;
            }

            this.Location = newLocation;            
        }
    }
}


