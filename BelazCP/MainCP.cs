using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class MainCP : Form
    {
        Auth auth = new Auth();
        public MainCP()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            auth.ID = null;
            auth.Show();
        }
    }
}
