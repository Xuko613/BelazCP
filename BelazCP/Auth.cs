using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BelazCP
{
    public partial class Auth : Form
    {
        public static string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;";
        public static OleDbConnection MyConn;

        public  string ID;

        public Auth()
        {
            InitializeComponent();

            MyConn = new OleDbConnection(connStr);
            MyConn.Open();
        }

        private void Auth_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyConn.Close();
            Application.Exit();
        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            ID = IDText.Text.Trim();
            string Pass = PassText.Text.Trim();
            try
            {
                string query = $"SELECT Имя FROM Users WHERE (ID = {ID}) AND (Пароль = '{Pass}')";
                OleDbCommand command = new OleDbCommand(query, MyConn);
                if (command.ExecuteScalar() != null)
                {
                    MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!", "Приветсвие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MainCP mainCP = new MainCP();
                    mainCP.Show();
                }
                else
                {
                    MessageBox.Show("Неверный ID или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IDText_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) 
            {
                e.Handled = true;
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {

        }
    }
}
