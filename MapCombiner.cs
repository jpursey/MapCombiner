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
            m_gridPos = new Point(0, 0);
            m_gridSize = new Size(3, 3);
            m_cellSize = 250;
            m_grid = new Cell[m_gridSize.Width, m_gridSize.Height];
            m_previewImage = new Bitmap(m_gridSize.Width * m_cellSize, m_gridSize.Height * m_cellSize);
            UpdateImage();
            mapImage.Image = m_previewImage;
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
        private Size m_gridSize;
        private int m_cellSize;
        private Bitmap m_previewImage;

        private void UpdateImage()
        {
            using (var graphics = Graphics.FromImage(m_previewImage))
            {
                graphics.Clear(Color.Black);

                for (int x = 0; x < m_gridSize.Width; ++x)
                {
                    for (int y = 0; y < m_gridSize.Height; ++y)
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

        private void editGridSize_Click(object sender, EventArgs e)
        {
            // TODO
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
            if (newPos.X < 0 || newPos.X >= m_gridSize.Width || 
                newPos.Y < 0 || newPos.Y >= m_gridSize.Height)
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

        private void MapCombiner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                m_grid[m_gridPos.X, m_gridPos.Y].image = null;
                UpdateImage();
            }
        }
    }
}
