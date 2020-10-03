using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graal.Library.Common.GUI
{
    public partial class InfoWindow : Form
    {
        public InfoWindow()
        {
            InitializeComponent();
        }

        public InfoWindow(string title, string text, int width = 500, int height = 600)
        {
            InitializeComponent();

            this.Text = title;
            this.Width = width;
            this.Height = height;

            TextBox.Text = text;
        }
    }
}
