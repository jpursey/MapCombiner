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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public int CountX { get; set; }
        public int CountY { get; set; }
        public int OutputSize { get; set; }

        private void Settings_Load(object sender, EventArgs e)
        {
            editX.Text = CountX.ToString();
            editY.Text = CountY.ToString();
            editSize.Text = OutputSize.ToString();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            int value;
            if (int.TryParse(editX.Text, out value))
            {
                CountX = value;
            }
            if (int.TryParse(editY.Text, out value))
            {
                CountY = value;
            }
            if (int.TryParse(editSize.Text, out value))
            {
                OutputSize = value;
            }
        }
    }
}
