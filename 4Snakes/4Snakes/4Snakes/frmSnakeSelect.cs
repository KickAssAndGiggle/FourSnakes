using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4Snakes
{
    public partial class frmSnakeSelect : Form
    {

        public int Selected = -1;

        public frmSnakeSelect()
        {
            InitializeComponent();
        }

        private void lblPeter_Click(object sender, EventArgs e)
        {
            Selected = 0;
            this.Hide();
        }

        private void lblGreg_Click(object sender, EventArgs e)
        {
            Selected = 1;
            this.Hide();
        }

        private void lblYuri_Click(object sender, EventArgs e)
        {
            Selected = 2;
            this.Hide();
        }

        private void lblTom_Click(object sender, EventArgs e)
        {
            Selected = 3;
            this.Hide();
        }
    }
}
