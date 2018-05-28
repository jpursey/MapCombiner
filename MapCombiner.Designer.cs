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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMap = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMap = new System.Windows.Forms.ToolStripMenuItem();
            this.fileAaveMapAs = new System.Windows.Forms.ToolStripMenuItem();
            this.fileAddImages = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExportMap = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.panels = new System.Windows.Forms.SplitContainer();
            this.imageList = new System.Windows.Forms.DataGridView();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.filenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mapImage = new System.Windows.Forms.PictureBox();
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
            this.statusInfo});
            this.status.Location = new System.Drawing.Point(0, 772);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(874, 22);
            this.status.TabIndex = 0;
            this.status.Text = "status";
            // 
            // statusInfo
            // 
            this.statusInfo.BackColor = System.Drawing.SystemColors.Control;
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(112, 17);
            this.statusInfo.Text = "Tiles: X,Y Tile Size: X";
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
            this.fileSaveMap,
            this.fileAaveMapAs,
            this.fileAddImages,
            this.fileExportMap,
            this.fileExit});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            // 
            // fileOpenMap
            // 
            this.fileOpenMap.Name = "fileOpenMap";
            this.fileOpenMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpenMap.Size = new System.Drawing.Size(182, 22);
            this.fileOpenMap.Text = "Open Map...";
            this.fileOpenMap.Click += new System.EventHandler(this.fileOpenMap_Click);
            // 
            // fileSaveMap
            // 
            this.fileSaveMap.Name = "fileSaveMap";
            this.fileSaveMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSaveMap.Size = new System.Drawing.Size(182, 22);
            this.fileSaveMap.Text = "Save Map";
            this.fileSaveMap.Click += new System.EventHandler(this.fileSaveMap_Click);
            // 
            // fileAaveMapAs
            // 
            this.fileAaveMapAs.Name = "fileAaveMapAs";
            this.fileAaveMapAs.Size = new System.Drawing.Size(182, 22);
            this.fileAaveMapAs.Text = "Save Map As...";
            this.fileAaveMapAs.Click += new System.EventHandler(this.fileAaveMapAs_Click);
            // 
            // fileAddImages
            // 
            this.fileAddImages.Name = "fileAddImages";
            this.fileAddImages.Size = new System.Drawing.Size(182, 22);
            this.fileAddImages.Text = "Add Images...";
            this.fileAddImages.Click += new System.EventHandler(this.fileAddImages_Click);
            // 
            // fileExportMap
            // 
            this.fileExportMap.Name = "fileExportMap";
            this.fileExportMap.Size = new System.Drawing.Size(182, 22);
            this.fileExportMap.Text = "Export Map";
            this.fileExportMap.Click += new System.EventHandler(this.fileExportMap_Click);
            // 
            // fileExit
            // 
            this.fileExit.Name = "fileExit";
            this.fileExit.Size = new System.Drawing.Size(182, 22);
            this.fileExit.Text = "Exit";
            this.fileExit.Click += new System.EventHandler(this.fileExit_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSettings});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(39, 20);
            this.editMenu.Text = "Edit";
            // 
            // editSettings
            // 
            this.editSettings.Name = "editSettings";
            this.editSettings.Size = new System.Drawing.Size(125, 22);
            this.editSettings.Text = "Settings...";
            this.editSettings.Click += new System.EventHandler(this.editSettings_Click);
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
            this.imageColumn,
            this.filenameColumn});
            this.imageList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.imageList.DefaultCellStyle = dataGridViewCellStyle1;
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
            // filenameColumn
            // 
            this.filenameColumn.HeaderText = "Filename";
            this.filenameColumn.Name = "filenameColumn";
            this.filenameColumn.ReadOnly = true;
            this.filenameColumn.Visible = false;
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
        private System.Windows.Forms.ToolStripMenuItem fileAddImages;
        private System.Windows.Forms.SplitContainer panels;
        private System.Windows.Forms.PictureBox mapImage;
        private System.Windows.Forms.DataGridView imageList;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMap;
        private System.Windows.Forms.ToolStripMenuItem fileExportMap;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripStatusLabel statusInfo;
        private System.Windows.Forms.ToolStripMenuItem editSettings;
        private System.Windows.Forms.ToolStripMenuItem fileAaveMapAs;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameColumn;
    }
}

