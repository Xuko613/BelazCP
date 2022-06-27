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
    public partial class SetPass : Form
    {
        public SetPass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == textBox3.Text.Trim() && textBox2.Text.Trim()!="")
            {
                AddUser.pass = textBox2.Text.Trim();
                MessageBox.Show("Пароль успешно задан", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
