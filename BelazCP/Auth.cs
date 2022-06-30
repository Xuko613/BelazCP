using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class Auth : Form
    {

        public static OleDbConnection MyConn;
        string query;
        public static string WorkID;
        public static DateTime StartWork;
        public static string ID;
        public static string report;

        public Auth()
        {
            InitializeComponent();

            // MyConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = DB.mdb;");
            MyConn = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/BelazCP/DB.mdb");
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
                query = $"SELECT Имя FROM Users WHERE (ID = {ID}) AND (Пароль = '{Pass}')";
                OleDbCommand command = new OleDbCommand(query, MyConn);
                if (command.ExecuteScalar() != null)
                {
                    MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!", "Приветсвие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    WorkID = DateTime.Now.ToString("yyyyMMddhhss");
                    StartWork = DateTime.Now;
                    query = $"INSERT INTO WorkTime ([ID], [W_ID], [Приступил]) VALUES ('{WorkID}', '{ID}', '{StartWork}')";
                    command = new OleDbCommand(query, MyConn);
                    command.ExecuteNonQuery();
                    this.Hide();
                    MainCP mainCP = new MainCP();
                    mainCP.Show();
                }
                else
                {
                    MessageBox.Show("Неверный ID или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                report += ex.ToString();
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