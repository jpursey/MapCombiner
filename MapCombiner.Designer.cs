namespace MapCombiner
{
    partial class MapCombiner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.status = new System.Windows.Forms.StatusStrip();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMap = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenImages = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMap = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExportMap = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editTileCount = new System.Windows.Forms.ToolStripMenuItem();
            this.editTileSize = new System.Windows.Forms.ToolStripMenuItem();
            this.panels = new System.Windows.Forms.SplitContainer();
            this.imageList = new System.Windows.Forms.DataGridView();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.mapImage = new System.Windows.Forms.PictureBox();
            this.gridInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.status.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panels)).BeginInit();
            this.panels.Panel1.SuspendLayout();
            this.panels.Panel2.SuspendLayout();
            this.panels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridInfo});
            this.status.Location = new System.Drawing.Point(0, 772);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(874, 22);
            this.status.TabIndex = 0;
            this.status.Text = "status";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(874, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenMap,
            this.fileOpenImages,
            this.fileSaveMap,
            this.fileExportMap,
            this.fileExit});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            // 
            // fileOpenMap
            // 
            this.fileOpenMap.Name = "fileOpenMap";
            this.fileOpenMap.Size = new System.Drawing.Size(153, 22);
            this.fileOpenMap.Text = "Open Map...";
            // 
            // fileOpenImages
            // 
            this.fileOpenImages.Name = "fileOpenImages";
            this.fileOpenImages.Size = new System.Drawing.Size(153, 22);
            this.fileOpenImages.Text = "Open Images...";
            this.fileOpenImages.Click += new System.EventHandler(this.fileOpenImages_Click);
            // 
            // fileSaveMap
            // 
            this.fileSaveMap.Name = "fileSaveMap";
            this.fileSaveMap.Size = new System.Drawing.Size(153, 22);
            this.fileSaveMap.Text = "Save Map";
            // 
            // fileExportMap
            // 
            this.fileExportMap.Name = "fileExportMap";
            this.fileExportMap.Size = new System.Drawing.Size(153, 22);
            this.fileExportMap.Text = "Export Map";
            // 
            // fileExit
            // 
            this.fileExit.Name = "fileExit";
            this.fileExit.Size = new System.Drawing.Size(153, 22);
            this.fileExit.Text = "Exit";
            this.fileExit.Click += new System.EventHandler(this.fileExit_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTileCount,
            this.editTileSize});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(39, 20);
            this.editMenu.Text = "Edit";
            // 
            // editTileCount
            // 
            this.editTileCount.Name = "editTileCount";
            this.editTileCount.Size = new System.Drawing.Size(152, 22);
            this.editTileCount.Text = "Tile Count...";
            this.editTileCount.Click += new System.EventHandler(this.editTileCount_Click);
            // 
            // editTileSize
            // 
            this.editTileSize.Name = "editTileSize";
            this.editTileSize.Size = new System.Drawing.Size(152, 22);
            this.editTileSize.Text = "Tile Size...";
            // 
            // panels
            // 
            this.panels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panels.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.panels.IsSplitterFixed = true;
            this.panels.Location = new System.Drawing.Point(0, 24);
            this.panels.Name = "panels";
            // 
            // panels.Panel1
            // 
            this.panels.Panel1.Controls.Add(this.imageList);
            // 
            // panels.Panel2
            // 
            this.panels.Panel2.Controls.Add(this.mapImage);
            this.panels.Size = new System.Drawing.Size(874, 748);
            this.panels.SplitterDistance = 125;
            this.panels.TabIndex = 5;
            // 
            // imageList
            // 
            this.imageList.AllowUserToAddRows = false;
            this.imageList.AllowUserToDeleteRows = false;
            this.imageList.AllowUserToResizeColumns = false;
            this.imageList.AllowUserToResizeRows = false;
            this.imageList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.imageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imageList.ColumnHeadersVisible = false;
            this.imageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageColumn});
            this.imageList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.imageList.DefaultCellStyle = dataGridViewCellStyle4;
            this.imageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.imageList.Location = new System.Drawing.Point(0, 0);
            this.imageList.MultiSelect = false;
            this.imageList.Name = "imageList";
            this.imageList.ReadOnly = true;
            this.imageList.RowHeadersVisible = false;
            this.imageList.RowTemplate.Height = 100;
            this.imageList.RowTemplate.ReadOnly = true;
            this.imageList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imageList.ShowCellErrors = false;
            this.imageList.ShowCellToolTips = false;
            this.imageList.ShowEditingIcon = false;
            this.imageList.ShowRowErrors = false;
            this.imageList.Size = new System.Drawing.Size(125, 748);
            this.imageList.TabIndex = 0;
            this.imageList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.imageList_CellContentClick);
            // 
            // imageColumn
            // 
            this.imageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageColumn.HeaderText = "Image";
            this.imageColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.imageColumn.MinimumWidth = 100;
            this.imageColumn.Name = "imageColumn";
            this.imageColumn.ReadOnly = true;
            // 
            // mapImage
            // 
            this.mapImage.BackColor = System.Drawing.Color.DarkRed;
            this.mapImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapImage.Location = new System.Drawing.Point(0, 0);
            this.mapImage.Name = "mapImage";
            this.mapImage.Size = new System.Drawing.Size(745, 748);
            this.mapImage.TabIndex = 0;
            this.mapImage.TabStop = false;
            this.mapImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapImage_MouseClick);
            // 
            // gridInfo
            // 
            this.gridInfo.BackColor = System.Drawing.SystemColors.Control;
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(112, 17);
            this.gridInfo.Text = "Tiles: X,Y Tile Size: X";
            // 
            // MapCombiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(874, 794);
            this.Controls.Add(this.panels);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menu);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menu;
            this.Name = "MapCombiner";
            this.Text = "Map Combiner";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapCombiner_KeyDown);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panels.Panel1.ResumeLayout(false);
            this.panels.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panels)).EndInit();
            this.panels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileOpenMap;
        private System.Windows.Forms.ToolStripMenuItem fileExit;
        private System.Windows.Forms.ToolStripMenuItem fileOpenImages;
        private System.Windows.Forms.SplitContainer panels;
        private System.Windows.Forms.PictureBox mapImage;
        private System.Windows.Forms.DataGridView imageList;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMap;
        private System.Windows.Forms.ToolStripMenuItem fileExportMap;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem editTileSize;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.ToolStripMenuItem editTileCount;
        private System.Windows.Forms.ToolStripStatusLabel gridInfo;
    }
}

