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
    public partial class EditTileCount : Form
    {
        public EditTileCount()
        {
            InitializeComponent();
        }

        public int CountX { get; set; }
        public int CountY { get; set; }

        private void editX_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(editX.Text, out value))
            {
                CountX = value;
            }
        }

        private void editY_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(editY.Text, out value))
            {
                CountY = value;
            }
        }

        private void EditTileCount_Load(object sender, EventArgs e)
        {
            editX.Text = CountX.ToString();
            editY.Text = CountY.ToString();
        }
    }
}
