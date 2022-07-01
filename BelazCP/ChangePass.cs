using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"SELECT Имя FROM Users WHERE ([ID] like '{HRCP.SID}') AND ([Пароль] like '{textBox1.Text}')";
            OleDbCommand command = new OleDbCommand(query, Auth.MyConn);
            if (command.ExecuteScalar() != null && textBox2.Text == textBox3.Text)
            {
                query = $"UPDATE Users SET [Пароль] = '{textBox2.Text}' where [ID] like {HRCP.SID}";
                OleDbCommand cmd = new OleDbCommand(query, Auth.MyConn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пароль успешно изменен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ошибка смены пароля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}