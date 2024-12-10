namespace ToolStripCustomCtrls
{
    partial class CustomizeToolStripForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.listView1 = new System.Windows.Forms.ListView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.panel3 = new System.Windows.Forms.Panel();
			this.LabelPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnDone = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lvImages = new System.Windows.Forms.ImageList(this.components);
			this.LabelPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.BackColor = System.Drawing.SystemColors.Control;
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(9, 10);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(587, 195);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 1;
			this.listView1.TabStop = false;
			this.listView1.TileSize = new System.Drawing.Size(48, 48);
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.List;
			this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
			this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
			this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Location = new System.Drawing.Point(9, 205);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 6, 1, 6);
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(587, 25);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(9, 266);
			this.panel3.TabIndex = 6;
			// 
			// LabelPanel
			// 
			this.LabelPanel.Controls.Add(this.label1);
			this.LabelPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.LabelPanel.Location = new System.Drawing.Point(9, 0);
			this.LabelPanel.Name = "LabelPanel";
			this.LabelPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.LabelPanel.Size = new System.Drawing.Size(587, 10);
			this.LabelPanel.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Location = new System.Drawing.Point(16, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 10);
			this.label1.TabIndex = 0;
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.Location = new System.Drawing.Point(517, 5);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(68, 24);
			this.btnDone.TabIndex = 1;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.btnDone);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(9, 230);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(587, 36);
			this.panel1.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(596, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 50, 0);
			this.panel2.Size = new System.Drawing.Size(12, 266);
			this.panel2.TabIndex = 5;
			// 
			// lvImages
			// 
			this.lvImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.lvImages.ImageSize = new System.Drawing.Size(16, 16);
			this.lvImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CustomizeToolStripForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 266);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.LabelPanel);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CustomizeToolStripForm";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Customize Toolbar";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CustomizeToolStripFrm_FormClosed);
			this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.CustomizeToolStripFrm_GiveFeedback);
			this.LabelPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel LabelPanel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ImageList lvImages;

    }
}