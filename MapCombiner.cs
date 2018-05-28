// Copyright 2018 John Pursey
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MapCombiner
{
    public partial class MapCombiner : Form
    {
        public MapCombiner()
        {
            InitializeComponent();
            mapImage.MouseWheel += mapImage_MouseWheel;
            fileSaveMap.Enabled = false;
            editUndo.Enabled = false;
            editRedo.Enabled = false;

            m_commands = new Commands();
            m_tileMap = new TileMap(4, 4, 400);
            m_tilePos = new Point(0, 0);
            m_tileViewSize = 200;
            m_mapFilename = "";
            UpdateStatus();
            ResizeImage();
        }

        //--------------------------------------------------------------------------------------------------------------
        // Data

        private TileMap m_tileMap;
        private Point m_tilePos;
        private int m_tileViewSize;
        private string m_mapFilename;
        private Bitmap m_previewImage;
        private Commands m_commands;

        //--------------------------------------------------------------------------------------------------------------
        // Operations

        private void LoadMap(string filename)
        {
            TileMap newMap = null;
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    using (var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                    {
                        var serializer = new DataContractSerializer(typeof(TileMap));
                        newMap = (TileMap)serializer.ReadObject(reader);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to open map file: " + filename, "Open Map", MessageBoxButtons.OK);
                return;
            }
            m_mapFilename = filename;
            fileSaveMap.Enabled = true;
            m_tileMap = newMap;
            m_commands.Reset();
            editUndo.Enabled = false;
            editRedo.Enabled = false;
            ResizeImage();
            UpdateStatus();
        }

        private void SaveMap(string filename)
        {
            try
            {
                using (var fs = new FileStream(filename, FileMode.Create))
                {
                    using (var writer = XmlDictionaryWriter.CreateTextWriter(fs))
                    {
                        var serializer = new DataContractSerializer(typeof(TileMap));
                        serializer.WriteObject(writer, m_tileMap);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to save map file: " + filename, "Save Map", MessageBoxButtons.OK);
                return;
            }
            m_mapFilename = filename;
            fileSaveMap.Enabled = true;
        }

        private void SaveImage(string filename)
        {
            int tileSize = ClampTileSize(m_tileMap.TileSize);
            if (tileSize != m_tileMap.TileSize)
            {
                var message = new StringBuilder().
                    AppendFormat("Tile size reduced to {0}. Continue?", tileSize).ToString();
                if (MessageBox.Show(message, "Export Map", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                using (var image = new Bitmap(m_tileMap.CountX * tileSize, m_tileMap.CountY * tileSize))
                {
                    using (var graphics = Graphics.FromImage(image))
                    {
                        DrawImage(graphics, tileSize);
                    }
                    image.Save(filename);
                }
            }
            catch
            {
                MessageBox.Show("Failed to save image to file: " + filename, "Export Map", MessageBoxButtons.OK);
            }
        }

        private void UpdateStatus()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Count: {0},{1} Size: {2}px", 
                m_tileMap.CountX, m_tileMap.CountY, m_tileMap.TileSize);
            statusInfo.Text = builder.ToString();

        }

        private void UpdateSettings(int countX, int countY, int tileSize)
        {
            m_tileMap.Resize(countX, countY);
            UpdateStatus();
            ResizeImage();
        }

        private int ClampTileSize(int tileSize)
        {
            while (m_tileMap.CountX * tileSize > 10000 ||
                   m_tileMap.CountY * tileSize > 10000)
            {
                tileSize = tileSize / 2;
            }
            return tileSize;
        }

        private void ResizeImage()
        {
            m_tileViewSize = ClampTileSize(m_tileViewSize);
            if (m_previewImage != null)
            {
                mapImage.Image = null;
                m_previewImage.Dispose();
            }
            m_previewImage = new Bitmap(m_tileMap.CountX * m_tileViewSize, m_tileMap.CountY * m_tileViewSize);
            mapImage.Image = m_previewImage;
            UpdateImage();
        }

        private void DrawImage(Graphics graphics, int tileSize)
        {
            graphics.Clear(Color.Black);
            for (int x = 0; x < m_tileMap.CountX; ++x)
            {
                for (int y = 0; y < m_tileMap.CountY; ++y)
                {
                    if (m_tileMap[x, y].Image != null)
                    {
                        graphics.ResetTransform();
                        var rot = Rotation.Values[m_tileMap[x, y].Rotation];
                        graphics.TranslateTransform((x + rot.offset.X) * tileSize,
                                                    (y + rot.offset.Y) * tileSize);
                        graphics.RotateTransform(rot.angle);
                        graphics.DrawImage(m_tileMap[x, y].Image, 0, 0,
                            tileSize + rot.offset.Y, tileSize + rot.offset.X);
                    }
                }
            }
        }

        private void UpdateImage()
        {
            using (var graphics = Graphics.FromImage(m_previewImage))
            {
                DrawImage(graphics, m_tileViewSize);

                graphics.ResetTransform();
                graphics.DrawRectangle(Pens.Yellow, 
                    m_tilePos.X * m_tileViewSize, m_tilePos.Y * m_tileViewSize, 
                    m_tileViewSize - 1, m_tileViewSize - 1);
            }
            mapImage.Invalidate();
        }

        //--------------------------------------------------------------------------------------------------------------
        // Commands

        void DoSetTileFilename(string filename)
        {
            m_commands.Do(new ChangeTileCommand(this, 
                new Tile(filename, m_tileMap[m_tilePos.X, m_tilePos.Y].Rotation)));
            editUndo.Enabled = m_commands.CanUndo;
            editRedo.Enabled = m_commands.CanRedo;
        }

        void DoSetTileRotation(int rotation)
        {
            m_commands.Do(new ChangeTileCommand(this, 
                new Tile(m_tileMap[m_tilePos.X, m_tilePos.Y].Filename, rotation)));
            editUndo.Enabled = m_commands.CanUndo;
            editRedo.Enabled = m_commands.CanRedo;
        }

        void DoDeleteTile()
        {
            m_commands.Do(new ChangeTileCommand(this, new Tile()));
            editUndo.Enabled = m_commands.CanUndo;
            editRedo.Enabled = m_commands.CanRedo;
        }

        void DoChangeSettings(int countX, int countY, int tileSize)
        {
            m_commands.Do(new ChangeSettingsCommand(this, countX, countY, tileSize));
            editUndo.Enabled = m_commands.CanUndo;
            editRedo.Enabled = m_commands.CanRedo;
        }

        class ChangeTileCommand : ICommand
        {
            public ChangeTileCommand(MapCombiner form, Tile tile)
            {
                m_form = form;
                m_pos = form.m_tilePos;
                m_tile = tile;
                m_oldTile = m_form.m_tileMap[m_pos.X, m_pos.Y];
            }

            public void Redo()
            {
                m_form.m_tileMap[m_pos.X, m_pos.Y] = m_tile;
                m_form.UpdateImage();
            }

            public void Undo()
            {
                m_form.m_tileMap[m_pos.X, m_pos.Y] = m_oldTile;
                m_form.UpdateImage();
            }

            private MapCombiner m_form;
            private Point m_pos;
            private Tile m_tile;
            private Tile m_oldTile;
        }

        class ChangeSettingsCommand : ICommand
        {
            public ChangeSettingsCommand(MapCombiner form, int countX, int countY, int tileSize)
            {
                m_form = form;
                m_oldMap = m_form.m_tileMap;
                m_countChanged = (countX != m_form.m_tileMap.CountX || countY != m_form.m_tileMap.CountY);
                m_countX = countX;
                m_countY = countY;
                m_tileSize = tileSize;
                m_oldTileSize = m_oldMap.TileSize;
            }

            public void Redo()
            {
                if (m_countChanged)
                {
                    m_form.m_tileMap = (TileMap)m_form.m_tileMap.Clone();
                    m_form.m_tileMap.Resize(m_countX, m_countY);
                    m_form.ResizeImage();
                }
                m_form.m_tileMap.TileSize = m_tileSize;
                m_form.UpdateStatus();
            }

            public void Undo()
            {
                if (m_countChanged)
                {
                    m_form.m_tileMap = m_oldMap;
                    m_form.ResizeImage();
                }
                m_form.m_tileMap.TileSize = m_oldTileSize;
                m_form.UpdateStatus();
            }

            private MapCombiner m_form;
            private bool m_countChanged;
            private TileMap m_oldMap;
            private int m_countX;
            private int m_countY;
            private int m_tileSize;
            private int m_oldTileSize;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Event handlers

        private void fileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fileAddImages_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open Images";
            ofd.Multiselect = true;
            ofd.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*jpeg;*.png;*.bmp|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string errorText = "";
            foreach (var filename in ofd.FileNames)
            {
                var result = ImageDatabase.Load(filename);
                if (result.Ok)
                {
                    imageList.Rows.Add(new object[] { result.Value, filename });
                } else
                {
                    errorText = errorText + result.Status + "\n";
                }
            }
            if (errorText.Length > 0)
            {
                MessageBox.Show(errorText, "Image Load Errors", MessageBoxButtons.OK);
            }
        }

        private void imageList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DoSetTileFilename(imageList.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value as string);
        }

        private void mapImage_MouseClick(object sender, MouseEventArgs e)
        {
            var newPos = new Point(e.Location.X / m_tileViewSize, e.Location.Y / m_tileViewSize);
            if (newPos.X < 0 || newPos.X >= m_tileMap.CountX || 
                newPos.Y < 0 || newPos.Y >= m_tileMap.CountY)
            {
                return;
            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                    m_tilePos = newPos;
                    UpdateImage();
                    break;
                case MouseButtons.Right:
                    m_tilePos = newPos;
                    DoSetTileRotation((m_tileMap[m_tilePos.X, m_tilePos.Y].Rotation + 1) % Rotation.Values.Length);
                    break;
            }
        }

        private void mapImage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0 && m_tileViewSize > 50)
            {
                m_tileViewSize /= 2;
                ResizeImage();
            }
            else if (e.Delta > 0 && m_tileViewSize < 500)
            {
                m_tileViewSize *= 2;
                ResizeImage();
            }
        }

        private void MapCombiner_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                case Keys.Back:
                    DoDeleteTile();
                    break;
                case Keys.Up:
                    if (m_tilePos.Y > 0)
                    {
                        m_tilePos.Y -= 1;
                        UpdateImage();
                    }
                    break;
                case Keys.Down:
                    if (m_tilePos.Y < m_tileMap.CountY - 1)
                    {
                        m_tilePos.Y += 1;
                        UpdateImage();
                    }
                    break;
                case Keys.Left:
                    if (m_tilePos.X > 0)
                    {
                        m_tilePos.X -= 1;
                        UpdateImage();
                    }
                    break;
                case Keys.Right:
                    if (m_tilePos.X < m_tileMap.CountX - 1)
                    {
                        m_tilePos.X += 1;
                        UpdateImage();
                    }
                    break;
            }
        }

        private void editSettings_Click(object sender, EventArgs e)
        {
            using (var settings = new Settings())
            {
                settings.CountX = m_tileMap.CountX;
                settings.CountY = m_tileMap.CountY;
                settings.OutputSize = m_tileMap.TileSize;
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    if (settings.CountX < 1 || settings.CountX > 20 || settings.CountY < 1 || settings.CountY > 20)
                    {
                        MessageBox.Show("Tile count values must be between 1 and 20",
                                        "Invalid Tile Count", MessageBoxButtons.OK);
                        return;
                    }
                    if (settings.OutputSize < 50 || settings.OutputSize > 1000)
                    {
                        MessageBox.Show("Tile output size must be between 50 and 1000",
                                        "Invalid Tile Size", MessageBoxButtons.OK);
                        return;
                    }
                    DoChangeSettings(settings.CountX, settings.CountY, settings.OutputSize);
                }
            }
        }

        private void fileOpenMap_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open Map";
            ofd.Multiselect = false;
            ofd.Filter = "Map files (*.mc)|*.mc|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            LoadMap(ofd.FileName);
        }

        private void fileSaveMap_Click(object sender, EventArgs e)
        {
            SaveMap(m_mapFilename);
        }

        private void fileSaveMapAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.FileName = Path.GetFileNameWithoutExtension(m_mapFilename);
            sfd.Title = "Save Map As";
            sfd.Filter = "Map files (*.mc)|*.mc|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            SaveMap(sfd.FileName);
        }

        private void fileExportMap_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.FileName = Path.GetFileNameWithoutExtension(m_mapFilename);
            sfd.Title = "Export Map";
            sfd.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            SaveImage(sfd.FileName);
        }

        private void editUndo_Click(object sender, EventArgs e)
        {
            m_commands.Undo();
            editUndo.Enabled = m_commands.CanUndo;
            editRedo.Enabled = m_commands.CanRedo;
        }

        private void editRedo_Click(object sender, EventArgs e)
        {
            m_commands.Redo();
            editUndo.Enabled = m_commands.CanUndo;
            editRedo.Enabled = m_commands.CanRedo;
        }

        private void helpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Copyright 2018 John Pursey\n\n" +
                "Licensed under the Apache License, Version 2.0 (the \"License\");\n" +
                "you may not use this file except in compliance with the License.\n" +
                "You may obtain a copy of the License at\n\n" +
                "http://www.apache.org/licenses/LICENSE-2.0\n\n" +
                "Unless required by applicable law or agreed to in writing, software\n" +
                "distributed under the License is distributed on an \"AS IS\" BASIS,\n" +
                "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.\n" +
                "See the License for the specific language governing permissions and\n" +
                "limitations under the License.\n", "MapCombiner", MessageBoxButtons.OK);
        }
    }
}
