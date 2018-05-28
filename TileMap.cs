using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapCombiner
{
    struct Rotation
    {
        public Rotation(float angle, Point offset)
        {
            this.angle = angle;
            this.offset = offset;
        }
        public float angle;
        public Point offset;

        static public Rotation[] Values = {
            new Rotation(0, new Point(0,0)),
            new Rotation(90, new Point(1,0)),
            new Rotation(180, new Point(1,1)),
            new Rotation(270, new Point(0,1))
        };

    };

    [DataContract]
    class Tile
    {
        public Tile() { Reset(); }
        public Tile(string filename) { Reset(filename); }
        public Tile(string filename, int rotation) { Reset(filename, rotation); }

        //--------------------------------------------------------------------------------------------------------------
        // Attributes

        [DataMember(Name = "File")]
        public string Filename {
            get { return m_filename; }
            set
            {
                m_filename = (value == null ? "" : value);
                m_image = (Filename.Length != 0 ? ImageDatabase.Load(Filename).Value : null);
            }
        }
        public Image Image { get { return m_image; } }
        [DataMember(Name = "Rotation")]
        public int Rotation { get; set; }

        public void Reset() { Filename = null; Rotation = 0; }
        public void Reset(string filename) { Filename = filename; Rotation = 0; }
        public void Reset(string filename, int rotation) { Filename = filename; Rotation = rotation; }

        //--------------------------------------------------------------------------------------------------------------
        // Data

        private string m_filename;
        private Image m_image;
    };

    [DataContract]
    class TileMap
    {
        public TileMap(int countX, int countY, int outputSize)
        {
            CountX = countX;
            CountY = countY;
            m_tiles = new Tile[countX * countY];
            TileSize = outputSize;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Attributes

        [DataMember(Name = "CountX")]
        public int CountX { get; private set; }

        [DataMember(Name = "CountY")]
        public int CountY { get; private set; }

        [DataMember(Name = "TileSize")]
        public int TileSize { get; set; }

        public Tile this[int x, int y]
        {
            get
            {
                if (m_tiles[x + y * CountX] == null)
                    m_tiles[x + y * CountX] = new Tile();
                return m_tiles[x + y * CountX];
            }
            set { m_tiles[x + y * CountX] = value; }
        }

        //--------------------------------------------------------------------------------------------------------------
        // Operations

        public void Resize(int countX, int countY)
        {
            var newTiles = new Tile[countX * countY];
            foreach (int x in Enumerable.Range(1, Math.Min(countX, CountX) - 1))
            {
                foreach (int y in Enumerable.Range(1, Math.Min(countY, CountY) - 1))
                {
                    newTiles[x + y * countX] = m_tiles[x + y * CountX];
                }
            }
            CountX = countX;
            CountY = countY;
            m_tiles = newTiles;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Data

        [DataMember(Name = "Tiles")]
        private Tile[] m_tiles;
    }
}
