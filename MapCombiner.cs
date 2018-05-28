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

            m_tileMap = new TileMap(4, 4, 400);
            m_tilePos = new Point(0, 0);
            m_tileViewSize = 200;
            m_mapFilename = "";
            UpdateStatus();
            ResizeImage();
        }

        //--------------------------------------------------------------------------------------------------------------
        // Private Implementation

        private TileMap m_tileMap;
        private Point m_tilePos;
        private int m_tileViewSize;
        private string m_mapFilename;
        private Bitmap m_previewImage;

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

        private void UpdateTileCount(int countX, int countY)
        {
            if (countX < 1 || countX > 20 || countY < 1 || countY > 20)
            {
                MessageBox.Show("Tile count values must be between 1 and 20", 
                                "Invalid Tile Count", MessageBoxButtons.OK);
                return;
            }
            m_tileMap.Resize(countX, countY);
            UpdateStatus();
            ResizeImage();
        }

        private void UpdateOutputSize(int size)
        {
            if (size < 50 || size > 1000)
            {
                MessageBox.Show("Tile output size must be between 50 and 1000", 
                                "Invalid Tile Size", MessageBoxButtons.OK);
                return;
            }
            m_tileMap.TileSize = size;
            UpdateStatus();
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
            m_tileMap[m_tilePos.X, m_tilePos.Y].Reset( 
                imageList.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value as string);
            UpdateImage();
        }

        private void mapImage_MouseClick(object sender, MouseEventArgs e)
        {
            var newPos = new Point(e.Location.X / m_tileViewSize, e.Location.Y / m_tileViewSize);
            if (newPos.X < 0 || newPos.X >= m_tileMap.CountX || 
                newPos.Y < 0 || newPos.Y >= m_tileMap.CountY)
            {
                return;
            }

            m_tilePos = newPos;
            if (e.Button == MouseButtons.Right)
            {
                m_tileMap[m_tilePos.X, m_tilePos.Y].Rotation = 
                    (m_tileMap[m_tilePos.X, m_tilePos.Y].Rotation + 1) % Rotation.Values.Length;
            }
            UpdateImage();
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
                    m_tileMap[m_tilePos.X, m_tilePos.Y].Reset();
                    UpdateImage();
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
                    UpdateTileCount(settings.CountX, settings.CountY);
                    UpdateOutputSize(settings.OutputSize);
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

        private void fileAaveMapAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.FileName = m_mapFilename;
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
            sfd.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*jpeg;*.png;*.bmp|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            SaveImage(sfd.FileName);
        }
    }
}
