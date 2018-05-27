using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCombiner
{
    public partial class MapCombiner : Form
    {
        public MapCombiner()
        {
            InitializeComponent();
            this.mapImage.MouseWheel += mapImage_MouseWheel;

            m_gridPos = new Point(0, 0);
            m_cellSize = 200;
            m_cellOutputSize = 400;
            m_grid = new Cell[4, 4];
            UpdateGridInfo();
            ResizeImage();
        }

        //--------------------------------------------------------------------------------------------------------------
        // Private Implementation

        struct Rotation
        {
            public Rotation(float angle, Point offset)
            {
                this.angle = angle;
                this.offset = offset;
            }
            public float angle;
            public Point offset;
        };

        static Rotation[] rotations = {
            new Rotation(0, new Point(0,0)),
            new Rotation(90, new Point(1,0)),
            new Rotation(180, new Point(1,1)),
            new Rotation(270, new Point(0,1))
        };

        struct Cell
        {
            public Image image;
            public int rotation;
        };

        private Cell[,] m_grid;
        private Point m_gridPos;
        private int m_cellSize;
        private int m_cellOutputSize;
        private Bitmap m_previewImage;

        private void UpdateGridInfo()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Count: {0},{1} Size: {2}px", 
                m_grid.GetLength(0), m_grid.GetLength(1), m_cellOutputSize);
            gridInfo.Text = builder.ToString();

        }

        private void UpdateTileCount(int countX, int countY)
        {
            if (countX < 1 || countX > 20 || countY < 1 || countY > 20)
            {
                MessageBox.Show("Tile count values must be between 1 and 20", 
                                "Invalid Tile Count", MessageBoxButtons.OK);
                return;
            }
            var newGrid = new Cell[countX, countY];
            foreach (int x in Enumerable.Range(1, Math.Min(countX, m_grid.GetLength(0)) - 1))
            {
                foreach (int y in Enumerable.Range(1, Math.Min(countY, m_grid.GetLength(1)) - 1))
                {
                    newGrid[x, y] = m_grid[x, y];
                }
            }
            m_grid = newGrid;
            UpdateGridInfo();
            ResizeImage();
        }

        private void ResizeImage()
        {
            while (m_grid.GetLength(0) * m_cellSize > 10000 || 
                   m_grid.GetLength(1) * m_cellSize > 10000)
            {
                m_cellSize = m_cellSize / 2;
            }
            if (m_previewImage != null)
            {
                m_previewImage.Dispose();
            }
            m_previewImage = new Bitmap(m_grid.GetLength(0) * m_cellSize, m_grid.GetLength(1) * m_cellSize);
            mapImage.Image = m_previewImage;
            UpdateImage();
        }

        private void UpdateImage()
        {
            using (var graphics = Graphics.FromImage(m_previewImage))
            {
                graphics.Clear(Color.Black);

                for (int x = 0; x < m_grid.GetLength(0); ++x)
                {
                    for (int y = 0; y < m_grid.GetLength(1); ++y)
                    {
                        if (m_grid[x,y].image != null)
                        {
                            graphics.ResetTransform();
                            var rot = rotations[m_grid[x, y].rotation];
                            graphics.TranslateTransform((x + rot.offset.X) * m_cellSize, 
                                                        (y + rot.offset.Y) * m_cellSize);
                            graphics.RotateTransform(rot.angle);
                            graphics.DrawImage(m_grid[x, y].image, 0, 0, 
                                m_cellSize + rot.offset.Y, m_cellSize + rot.offset.X);
                        }
                        if (x == m_gridPos.X && y == m_gridPos.Y)
                        {
                            graphics.ResetTransform();
                            graphics.DrawRectangle(Pens.Yellow, 
                                x * m_cellSize, y * m_cellSize, m_cellSize - 1, m_cellSize - 1);
                        }
                    }
                }
            }
            mapImage.Invalidate();
        }

        private static Result<Image> LoadImage(string filename)
        {
            Bitmap newImage = null;
            try
            {
                using (Image fileImage = Image.FromFile(filename))
                {
                    newImage = new Bitmap(fileImage);
                }
            }
            catch (OutOfMemoryException)
            {
                return Result<Image>.Error("Unsupported image format: " + filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                return Result<Image>.Error("File not found: " + filename);
            }
            catch
            {
                return Result<Image>.Error("Unknown error");
            }
            return Result<Image>.Success(newImage);
        }

        //--------------------------------------------------------------------------------------------------------------
        // Event handlers

        private void fileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fileOpenImages_Click(object sender, EventArgs e)
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
                var result = LoadImage(filename);
                if (result.Ok)
                {
                    imageList.Rows.Add(new object[] { result.Value });
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
            var image = imageList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as Image;
            if (image == null)
            {
                return;
            }

            using (var graphics = Graphics.FromImage(m_previewImage))
            {
                m_grid[m_gridPos.X, m_gridPos.Y].image = image;
                m_grid[m_gridPos.X, m_gridPos.Y].rotation = 0;
                UpdateImage();
            }
        }

        private void mapImage_MouseClick(object sender, MouseEventArgs e)
        {
            var newPos = new Point(e.Location.X / m_cellSize, e.Location.Y / m_cellSize);
            if (newPos.X < 0 || newPos.X >= m_grid.GetLength(0) || 
                newPos.Y < 0 || newPos.Y >= m_grid.GetLength(1))
            {
                return;
            }

            m_gridPos = newPos;
            if (e.Button == MouseButtons.Right)
            {
                m_grid[m_gridPos.X, m_gridPos.Y].rotation = 
                    (m_grid[m_gridPos.X, m_gridPos.Y].rotation + 1) % rotations.Length;
            }
            UpdateImage();
        }

        private void mapImage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0 && m_cellSize > 50)
            {
                m_cellSize /= 2;
                ResizeImage();
            }
            else if (e.Delta > 0 && m_cellSize < 500)
            {
                m_cellSize *= 2;
                ResizeImage();
            }
        }

        private void MapCombiner_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                case Keys.Back:
                    m_grid[m_gridPos.X, m_gridPos.Y].image = null;
                    UpdateImage();
                    break;
                case Keys.Up:
                    if (m_gridPos.Y > 0)
                    {
                        m_gridPos.Y -= 1;
                        UpdateImage();
                    }
                    break;
                case Keys.Down:
                    if (m_gridPos.Y < m_grid.GetLength(1) - 1)
                    {
                        m_gridPos.Y += 1;
                        UpdateImage();
                    }
                    break;
                case Keys.Left:
                    if (m_gridPos.X > 0)
                    {
                        m_gridPos.X -= 1;
                        UpdateImage();
                    }
                    break;
                case Keys.Right:
                    if (m_gridPos.X < m_grid.GetLength(0) - 1)
                    {
                        m_gridPos.X += 1;
                        UpdateImage();
                    }
                    break;
            }
        }

        private void editTileCount_Click(object sender, EventArgs e)
        {
            using (var editTileCount = new EditTileCount())
            {
                editTileCount.CountX = m_grid.GetLength(0);
                editTileCount.CountY = m_grid.GetLength(1);
                if (editTileCount.ShowDialog() == DialogResult.OK)
                {
                    UpdateTileCount(editTileCount.CountX, editTileCount.CountY);
                }
            }
        }
    }
}
