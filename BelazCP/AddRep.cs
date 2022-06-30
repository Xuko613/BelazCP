using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class AddRep : Form
    {
        DateTime dt;
        public AddRep()
        {
            InitializeComponent();
        }

        private void AddRep_Load(object sender, EventArgs e)
        {
            label4.Text = HRCP.FIO;
            dt = DateTime.Now;
            label3.Text = dt.ToString("f");
            dateTimePicker1.MinDate = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"INSERT INTO Reprimands ([W_ID], [Причина], [Дата], [Действует до]) VALUES ('{HRCP.SID}', '{textBox1.Text}', '{dt}','{dateTimePicker1.Value}')";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            com.ExecuteNonQuery();
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
