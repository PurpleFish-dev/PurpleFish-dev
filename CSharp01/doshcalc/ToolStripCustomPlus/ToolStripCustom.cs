using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ToolStripCustomCtrls
{
    public partial class ToolStripCustom : ToolStrip
    {
        public ToolStripCustom()
        {
            InitializeComponent();
        }

        private bool _customize = false;
        private bool _itemsOwnImagesInUse = true;

        private Cursor _itemCursor;              
        private ToolStripItem _dragItem;
        private ToolStripLabel _dragPanel;		
        private CustomizeToolStripForm _customizeForm;

        private bool _originalAllowDrop;
        private bool _originalAllowItemReorder;
        private ToolStripGripStyle _originalGripStyle;
        private List<ToolStripItem> _disabledItems = new List<ToolStripItem>();
        private Dictionary<string, Image> _originalToolImages = new Dictionary<string, Image>();
        private List<EventSuppressor> _eventSuppressors = new List<EventSuppressor>();

        public ToolStripData UserData 
        {
            get { return _itemData; }
            set 
			{
				if( value != null )
				{
					_itemData = value;
					_itemData.SaveDefaultLayout(this);
					_itemData.RestoreUserLayoutAndStyles(this);            
				}				
			}
        }   private ToolStripData _itemData = new ToolStripData();  

		public bool LoadUserLayoutFromRegistry(string applicationKey, string toolStripKey)
		{
            bool result = false;
			if (this._itemData != null)
            {
				this._itemData.SaveDefaultLayout(this);
			    result = this._itemData.LoadUserLayoutFromRegistry(applicationKey, toolStripKey);
				if(result == true)
				{
					_itemData.RestoreUserLayoutAndStyles(this);
				}
            }
			return result;
        }     
		
		public bool SaveUserLayoutToRegistry()
		{
            if (this._itemData != null)
            {
                return this._itemData.SaveUserLayoutToRegistry();
            }
			return false;
        }

        public bool DefaultButton
        {
            get { return _defaultButton; }
            set { _defaultButton = value; }
        }   private bool _defaultButton = true;
        
        public bool ImageSizeSelection
        {
            get { return _imageSizeSelection; }
            set { _imageSizeSelection = value; }
        }   private bool _imageSizeSelection = false;

        public ListViewDisplayStyle ListViewDisplayStyle
        {
            get { return _listViewDisplayStyle; }
            set { _listViewDisplayStyle = value; }
        }   private ListViewDisplayStyle _listViewDisplayStyle = ListViewDisplayStyle.Tiles;

        public Size LargeImageScalingSize
        {
            get { return this._largeImageScalingSize; }
            set { this._largeImageScalingSize = value; resizeImages(_largeIcons); }
        }   private Size _largeImageScalingSize = new Size(24,24);

        public Size SmallImageScalingSize
        {
            get { return this._smallImageScalingSize; }
            set { this._smallImageScalingSize = value; resizeImages(_largeIcons); }
        }   private Size _smallImageScalingSize = new Size(16, 16);

        internal void RestoreDefaultLayout()
        {
            if (_itemData != null)
                _itemData.RestoreDefaultLayout(this);
        }

        public ImageList LargeImageList
        {
            get { return this._largeImageList; }
            set { _largeImageList = value; resizeImages(_largeIcons); }
        } private ImageList _largeImageList;

        public ToolStripItemDisplayStyle DisplayStyle
        {
            get { return _displayStyle; }
            set
            {
                _displayStyle = value;
                //change the display style for each item 
                if (_displayStyle != ToolStripItemDisplayStyle.None)
                {
                    int nIndex = this.Items.Count;
                    while (nIndex > 0)
                    {
                        nIndex--;
                        this.Items[nIndex].DisplayStyle = this._displayStyle;
                    }
					//foreach (ToolStripItem tsi in this.Items)
                    //{
                    //    tsi.DisplayStyle = this.m_DisplayStyle;
                    //}
                }
            }
        }   private ToolStripItemDisplayStyle _displayStyle = ToolStripItemDisplayStyle.Image;

        public bool LargeIcons
        {
            get { return _largeIcons; }
            set  { _largeIcons = value; resizeImages(_largeIcons); }
        }   private bool _largeIcons = false;
        
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (drgevent.Data.GetDataPresent(typeof(ToolItemDragData)) == true)
            {
                _dragItem.Available = true;
				
				//if the user does not move(drag) the mouse after a call to 
				//TSItem_MouseMove starts starts dragging then OnDragOver will 
				//not have been called and the drag panel will not have been 
				//added to the toolstrip in this case we are not moving the item. 
				int dragPanelIndex = this.Items.IndexOf(this._dragPanel);
				if(dragPanelIndex != -1)
				{
					this.Items.Remove(_dragItem);					
					this.Items.Insert(dragPanelIndex, _dragItem);
					this.Items.Remove(_dragPanel);     
				}
            }
            base.OnDragDrop(drgevent);
        }

        protected override void OnLayoutCompleted(EventArgs e)
        {
            base.OnLayoutCompleted(e);
            this.resizeImages(this._largeIcons);            
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (drgevent.Data.GetDataPresent(typeof(ToolItemDragData)))
            {
                ToolItemDragData tidd;
                tidd = ((ToolItemDragData)drgevent.Data.GetData(typeof(ToolItemDragData)));

                //
				if ((tidd.type == typeof(ToolStripSeparator)) && (tidd.Inserting == true))
                {
                    this._dragItem = new ToolStripSeparator();
                    UniqueString us = new UniqueString(tidd.ItemName);
                    foreach (ToolStripItem itemToTest in this.Items)
                    {
                        us.AddExisting(itemToTest.Name);
                    }
                    string sUniqueName;
                    bool bModified = us.GetUniqueString(out sUniqueName);
                    this._dragItem.Name = sUniqueName;
                    _dragItem.Available = false;
                    this.Items.Add(this._dragItem);
                    _dragItem.Tag = "inserting";

                    this._dragItem.MouseMove += new MouseEventHandler(TSItem_MouseMove);
                }
                else
                { 
					const bool bSearchAllChildren = false;
					ToolStripItem[] items;
					items = this.Items.Find(tidd.ItemName, bSearchAllChildren);
					_dragItem = items[0];                
				}

                this._dragPanel = new ToolStripLabel();
                this._dragPanel.AutoSize = false;
                this._dragPanel.Size = _dragItem.GetPreferredSize(this.DisplayRectangle.Size);
				Debug.WriteLine(_dragItem.Size.ToString());

                drgevent.Effect = DragDropEffects.Move;
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
            }
            
            base.OnDragEnter(drgevent);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            this.Items.Remove(_dragItem);
            this.Items.Insert(this.Items.IndexOf(this._dragPanel), _dragItem);
            this.Items.Remove(_dragPanel);
            
            base.OnDragLeave(e);
        }
        
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (drgevent.Data.GetDataPresent(typeof(ToolItemDragData)) == true)
            {
                this.SuspendLayout();
				
				this.Items.Remove(_dragPanel);
				Point pt = new Point(drgevent.X, drgevent.Y);
                int nIndex = getInsertionIndex(pt);
				this.Items.Insert(Math.Min(this.Items.Count, nIndex), _dragPanel);

				this.ResumeLayout();  
			}

            base.OnDragOver(drgevent);
        }
        
        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
 	         if (_itemCursor == null)
            {
                gfbevent.UseDefaultCursors = true;
            }
            else
            {
                gfbevent.UseDefaultCursors = false;
                Cursor.Current = _itemCursor;
            }
            
            base.OnGiveFeedback(gfbevent);
        }

        private void TSItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._dragItem = ((ToolStripItem)sender);
 
                Image itemImage = this.CreateItemImage(_dragItem);
                _itemCursor = GetItemCursor(itemImage);
                _dragItem.Available = false;  

                ToolItemDragData tidd = new ToolItemDragData();
                tidd.Inserting = false;
                tidd.ItemName = this._dragItem.Name;
                tidd.ToolBarName = this.Name;
				tidd.type = this._dragItem.GetType();				

                DragDropEffects dropEffect = this.DoDragDrop(tidd, DragDropEffects.All);
                if (dropEffect == DragDropEffects.None)
                {
                    _dragItem.Available = true;
                }                
            }
        }        
        
		//returns the position in the toolstrip items array that an item should be inserted at in order
		//for it to be positioned in the toolstrip as close as possible to the given screen coordinate 
		 private int getInsertionIndex(Point ptDragOver)
        {
            int nItemIndex = -1;
            Point ItemCentre;

            if ((this.LayoutStyle == ToolStripLayoutStyle.HorizontalStackWithOverflow) || (this.LayoutStyle == ToolStripLayoutStyle.Flow))
            {
                do
                {
                    nItemIndex++;
                    Rectangle rtbounds = this.Items[nItemIndex].Bounds;

                    ItemCentre = this.PointToScreen(new Point((rtbounds.Left + (rtbounds.Width / 2)), (rtbounds.Top + (rtbounds.Height / 2))));
                }
                while (((ptDragOver.X > ItemCentre.X) || (this.Items[nItemIndex].Available == false)) && (nItemIndex < (this.Items.Count - 1)));

                if (ptDragOver.X > ItemCentre.X)
                {
                    nItemIndex++;
                }
            }
            else
            {
                do
                {
                    nItemIndex++;
                    Rectangle rtbounds = this.Items[nItemIndex].Bounds;
                    ItemCentre = this.PointToScreen(new Point((rtbounds.Left + (rtbounds.Width / 2)), (rtbounds.Top + (rtbounds.Height / 2))));
                }
                while (((ptDragOver.Y > ItemCentre.Y) || (this.Items[nItemIndex].Available == false)) && (nItemIndex < (this.Items.Count - 1)));

                if (ptDragOver.Y > ItemCentre.Y)
                    nItemIndex++;
            }
            return nItemIndex;
        }

		//puts the toolstrip into customization mode and displays the customization dialog
		public void Customize()
        {
            if (!_customize)
            {
                _customize = true;

                _originalAllowDrop = this.AllowDrop;
                _originalAllowItemReorder = this.AllowItemReorder;
                _originalGripStyle = this.GripStyle;

                this.AllowItemReorder = false;
                this.AllowDrop = true;
                this.GripStyle = ToolStripGripStyle.Hidden;
				this.Refresh(); //layout and redraw required after removing the grip
				//force a redraw before displaying the dialog				

                foreach (ToolStripItem tsi in this.Items)
                {
                    //remove all event listeners from the toolstrip items
                    //so that their actions are not called whilst the user 
                    //is customising the ToolStripGripStyle strip
                    EventSuppressor esi = new EventSuppressor(tsi);
                    _eventSuppressors.Add(esi);
                    esi.Suppress();

                    //add mouse move events to dragdrop the tools
                    tsi.MouseMove += new MouseEventHandler(TSItem_MouseMove);

                    //tools that are disabled will be enabled whilst customising
                    //cache disabled tools so the enabled state of tools can be 
                    //restored after customising
                    if (!tsi.Enabled)
                    {
                        _disabledItems.Add(tsi);
                    }
                    tsi.Enabled = true;
                }

                _customizeForm = new CustomizeToolStripForm(this);
                _customizeForm.SetLocationRelativeToToolStrip();
                _customizeForm.ShowPsudoModal();
            }
        }

		//sets the toolstrip back to normal mode 
		//restores the pre customization dfisplaystyles, and items if 'keepChanges' is false
        internal void EndCustomize(bool keepChanges)
        {
            if (_customize)
            {
                //restore the event listeners and enabled state of tools
                foreach (ToolStripItem tsi in this.Items)
                {
                    tsi.MouseMove -= TSItem_MouseMove;
                    tsi.Enabled = true;                    
                }

                foreach(ToolStripItem tsi in this._disabledItems)
                {
                    tsi.Enabled = false;
                }
                this._disabledItems.Clear();                
                                
                foreach (EventSuppressor p in _eventSuppressors)
                {
                    p.Resume();
                }
                _eventSuppressors.Clear();  
              
                if (_itemData != null)
                {
                    _itemData.SaveUserLayoutAndStyles(this);
                }

                //set the properties that were changed for customisation back to their
                //original values
                this.AllowDrop = this._originalAllowDrop;
                this.AllowItemReorder = this._originalAllowItemReorder;
                this.GripStyle = this._originalGripStyle;
                _customize = false;

                //if the given keepChanges flag is false restore the tools to their
                //original location and availablity
                if (!keepChanges)
                {
					throw new NotImplementedException();
                }
            }
        }	

		//returns a cursor that comprises of the given image with the current cursor superimposed onto it
		//the given image is centered over the existing hotspot
		//returns null if creation of the new cursor fails 
        internal static Cursor GetItemCursor(Image image)
        {
            try
			{
				using( Bitmap bmpTool = new Bitmap(image) )
				{
					Point ptNewHotSpot = new Point(Math.Max((bmpTool.Width / 2), Cursor.Current.HotSpot.X), Math.Max((bmpTool.Height / 2), Cursor.Current.HotSpot.Y));

					//bitmap the correct size for the icon and cursor, and a graphics object from it
					using( Bitmap bmpCombined = new Bitmap(ptNewHotSpot.X + Math.Max((bmpTool.Width / 2), Cursor.Current.Size.Width - Cursor.Current.HotSpot.X),
													       ptNewHotSpot.Y + Math.Max((bmpTool.Height / 2), Cursor.Current.Size.Height - Cursor.Current.HotSpot.Y)) )
					{
					
						using (Graphics gfxPic = Graphics.FromImage(bmpCombined) )
						{

							//draw the 'tool' bitmap, semi transparent, onto the new bitmap
							ColorMatrix cmxPic = new ColorMatrix();
							cmxPic.Matrix33 = (float)1.0/*0.62*/;
					
							using(ImageAttributes iaPic = new ImageAttributes())
							{
								iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

								Rectangle rtToolDest = new Rectangle(Math.Max(0, Cursor.Current.HotSpot.X - (bmpTool.Width / 2)), Math.Max(0, Cursor.Current.HotSpot.Y - (bmpTool.Height / 2)), bmpTool.Width, bmpTool.Height);
								gfxPic.DrawImage(bmpTool, rtToolDest, 0, 0, bmpTool.Width, bmpTool.Height, GraphicsUnit.Pixel, iaPic);
							}
						
              
				
							//draw the current cursor onto the new bitmap
							Rectangle rect = new Rectangle( ptNewHotSpot.X - Cursor.Current.HotSpot.X,
															ptNewHotSpot.Y - Cursor.Current.HotSpot.Y,
															(ptNewHotSpot.X - Cursor.Current.HotSpot.X) + Cursor.Current.Size.Width,
															(ptNewHotSpot.Y - Cursor.Current.HotSpot.Y) + Cursor.Current.Size.Height);
				  
							Cursor.Current.Draw(gfxPic, rect);
						}

						//create a cursor from the bitmap and return it							
						return CreateCursor(bmpCombined, ptNewHotSpot.X, ptNewHotSpot.Y);						
						
					}
				}
			}
			catch
			{
				return null;
			}     
        }

		//creates a cursor from the given bitmap and hotspot
        //*********************************************************************************
		[DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        private struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        private static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }
		//*********************************************************************************

        private void resizeImages(bool bLarge)
        {
            if (!this.DesignMode)
            {
                SuspendLayout();
                if (_itemsOwnImagesInUse)
                {
                    this._originalToolImages.Clear();
                    foreach (ToolStripItem tsItem in this.Items)
                    {
                        if (tsItem.Image != null)
                        {
                            this._originalToolImages.Add(tsItem.Name, tsItem.Image);                            
                        }
                    }
                }                    
                    
                if (bLarge)
                {
                    this.ImageScalingSize = this._largeImageScalingSize;
                    if (this.LargeImageList != null)
                    {
                        foreach (ToolStripItem tsItem in this.Items)
                        {
                            if ((tsItem.Image != null) && (this.LargeImageList.Images.ContainsKey(tsItem.Name)))
                            {
                                tsItem.Image = this.LargeImageList.Images[tsItem.Name];
                                _itemsOwnImagesInUse = false;                                
                            }
                        }                        
                    }
                }
                else
                {
                    this.ImageScalingSize = this._smallImageScalingSize;
                    foreach (ToolStripItem tsItem in this.Items)
                    {
                        if (this._originalToolImages.ContainsKey(tsItem.Name))
                        {
                            tsItem.Image = this._originalToolImages[tsItem.Name];
                            _itemsOwnImagesInUse = true;
                        }
                    }                    
                }                

                ResumeLayout();

                //force layout change
                using( ToolStripSeparator sepIS = new ToolStripSeparator() )
				{
					this.Items.Insert(0, sepIS);
					this.Items.Remove(sepIS);
				}               
            }
        }

		//creates an image of the given ToolStripItem
        internal Image CreateItemImage(ToolStripItem tsi)
        {
            if (tsi is ToolStripSeparator)
            {
                Size originalSize = tsi.Size;
                Rectangle parentDisplayRectangle = this.DisplayRectangle;
                Size displayedSepSize = tsi.GetPreferredSize(parentDisplayRectangle.Size);
                if (this.VerticalSeparator == true)
                {
                    displayedSepSize.Height = parentDisplayRectangle.Height;
                }
                else
                {
                    displayedSepSize.Width = parentDisplayRectangle.Width;
                }
				tsi.Size = displayedSepSize;			 
				
				Bitmap bmpSeparator = new Bitmap(tsi.Size.Width, tsi.Size.Height);
                Graphics gfx = Graphics.FromImage(bmpSeparator);
                this.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(gfx, ((ToolStripSeparator)tsi), this.VerticalSeparator));
				tsi.Size = originalSize;
                return bmpSeparator;
            }
            else if(tsi is ToolStripButton)
            {
                Bitmap bmpResized;
				if ((tsi.ImageScaling == ToolStripItemImageScaling.SizeToFit) && (this.ImageScalingSize != tsi.Image.Size))
                {
                    bmpResized = new Bitmap(this.ImageScalingSize.Width, this.ImageScalingSize.Height);
				}
				else
				{
					bmpResized = new Bitmap(tsi.Image.Width, tsi.Image.Height);
				}
				
				
                Graphics gfx = Graphics.FromImage(bmpResized);
                //this.Renderer.DrawButtonBackground(new ToolStripItemRenderEventArgs(gfx, ((ToolStripButton)tsi)));
                this.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(gfx, ((ToolStripButton)tsi), new Rectangle(0, 0, bmpResized.Width, bmpResized.Height)));

                //Bitmap bmpLV = new Bitmap(tsi.Size.Width, tsi.Size.Height);
                //reCanvas(ref bmpLV, bmpSeparator);
                return bmpResized;
            }
            else if (tsi is ToolStripControlHost)
            {
				Bitmap bmpSeparator = new Bitmap(tsi.Size.Width, tsi.Size.Height);
                ((ToolStripControlHost)tsi).Control.DrawToBitmap(bmpSeparator, new Rectangle(0, 0, bmpSeparator.Size.Width, bmpSeparator.Size.Height));

                Bitmap bmpLV = new Bitmap(tsi.Size.Width/* +2*/, tsi.Size.Height/* +2*/);
                reCanvas(ref bmpLV, bmpSeparator);
                return bmpLV;
            }
			else if (tsi.Image != null)
            {
                if ((tsi.ImageScaling == ToolStripItemImageScaling.SizeToFit) && (this.ImageScalingSize != tsi.Image.Size))
                {
                    Bitmap bmpResized = new Bitmap(this.ImageScalingSize.Width, this.ImageScalingSize.Height);
                    reCanvas(ref bmpResized, tsi.Image, this.ImageScalingSize);
                    return bmpResized;
                }
                else
                {
                    return tsi.Image;
                }
            }
            return null;
        }


		//creates an image of the given ToolStripItem of the given size. 
		//The item is draw the same size as it would appear on the ToolStrip 
		//if possible, other wise the width of the item is reduced to fit.
        internal Image CreateReCanvasedItemImage(ToolStripItem tsi, Size ImageSize)
        {
            if (tsi is ToolStripSeparator)
            {
                Size originalSize = tsi.Size;
                Rectangle parentDisplayRectangle = this.DisplayRectangle;
                Size displayedSepSize = tsi.GetPreferredSize(parentDisplayRectangle.Size);
                if (this.VerticalSeparator == true)
                {
                    displayedSepSize.Height = parentDisplayRectangle.Height;
                }
                else
                {
                    displayedSepSize.Width = parentDisplayRectangle.Width;
                }
				tsi.Size = displayedSepSize;			 

                using( Bitmap bmpSeparator = new Bitmap(Math.Min(ImageSize.Width, displayedSepSize.Width), Math.Min(ImageSize.Height, displayedSepSize.Height)) )
                {
					Graphics gfx = Graphics.FromImage(bmpSeparator);
					this.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(gfx, ((ToolStripSeparator)tsi), this.VerticalSeparator));

					Bitmap bmpLV = new Bitmap(ImageSize.Width, ImageSize.Height);
					reCanvas(ref bmpLV, bmpSeparator);				

					tsi.Size = originalSize;
					return bmpLV;
				}
            }
            else if (tsi is ToolStripControlHost)
            {
                //Size sepSize = tsi.Size;
                Rectangle parentDisplayRectangle = this.DisplayRectangle;
                
                Size oldhostSize = tsi.Size;
                bool oldAutoSize = tsi.AutoSize;

                tsi.AutoSize = false;
                tsi.Size = new Size(Math.Min(ImageSize.Width, tsi.Size.Width), Math.Min(ImageSize.Height, tsi.Size.Height));

                Bitmap bmpSeparator = new Bitmap(tsi.Size.Width, tsi.Size.Height);
                ((ToolStripControlHost)tsi).Control.DrawToBitmap(bmpSeparator, new Rectangle(0, 0, bmpSeparator.Size.Width, bmpSeparator.Size.Height));

                tsi.Size = oldhostSize;
                tsi.AutoSize = oldAutoSize;

                Bitmap bmpLV = new Bitmap(ImageSize.Width, ImageSize.Height);
                reCanvas(ref bmpLV, bmpSeparator);
                return bmpLV;
            }
			else if (tsi.Image != null)
            {
                Bitmap bmpLV = new Bitmap(ImageSize.Width, ImageSize.Height);
				if ((tsi.ImageScaling == ToolStripItemImageScaling.SizeToFit) && (this.ImageScalingSize != tsi.Image.Size))
                {
                    reCanvas(ref bmpLV, tsi.Image, this.ImageScalingSize);
                    return bmpLV;
                }
                else
                {
                    reCanvas(ref bmpLV, tsi.Image);
                    return bmpLV;
                }
            }
            return null;
        }

        internal bool VerticalSeparator
        {
            get
            {
                switch (this.LayoutStyle)
                {
                    case ToolStripLayoutStyle.VerticalStackWithOverflow:
                        return false;
                    case ToolStripLayoutStyle.HorizontalStackWithOverflow:
                    case ToolStripLayoutStyle.Flow:
                    case ToolStripLayoutStyle.Table:
                    default:
                        return true;
                }
            }
        }

		//draws the source image onto the middle of the destination bitmap
        internal static void reCanvas(ref Bitmap bmpDest, Image imgSource)
        {
            Point pt = new Point(((bmpDest.Width - imgSource.Width) / 2),
                                                ((bmpDest.Height - imgSource.Height) / 2));

            Graphics gfx = Graphics.FromImage(bmpDest);
            gfx.DrawImage(imgSource, pt);
        }

        //draws the source image, stretched to the given size, onto the middle of the destination bitmap
		internal static void reCanvas(ref Bitmap bmpDest, Image imgSource, Size szSource)
        {
            using( Bitmap bmpSource = new Bitmap(szSource.Width, szSource.Height) )
            {
				Graphics gfx = Graphics.FromImage(bmpSource);
				gfx.DrawImage(imgSource, new Rectangle(0, 0, szSource.Width, szSource.Height));

				Point pt = new Point(((bmpDest.Width - bmpSource.Width) / 2),
													((bmpDest.Height - bmpSource.Height) / 2));

				gfx = Graphics.FromImage(bmpDest);
				gfx.DrawImage(bmpSource, pt);
			}
        }        
    }    
}
