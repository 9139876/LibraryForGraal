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
    public partial class OperationWaitWindow : Form
    {
        readonly string text;
        readonly Action action;

        public OperationWaitWindow(string _text, Action _action)
        {
            InitializeComponent();
            action = _action;
            text = _text;
        }
        
        private void OperationWaitWindow_Shown(object sender, EventArgs e)
        {
            LblText.Text = text;
            LblText.Left = this.Width / 2 - LblText.Width / 2;
            LblText.Top -= (int)((text.Split("\r\n".ToCharArray()).Length - 1) * LblText.Font.Size);

            Refresh();

            action();

            Close();
        }
    }
}
