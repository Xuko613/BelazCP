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
    public partial class HRCP : Form
    {
        string query;
        public HRCP()
        {
            InitializeComponent();
        }

        private void Stuff_Refresh()
        {
            query = "SELECT [ID], [Фамилия], [Имя], [Отчество], [Дата рождения], [Пол] FROM Users";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Users_table");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Users_table";
            Stuff_Resize();
        }
        private void Stuff_Resize()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count + 1;
            }
        }

        private void HRCP_Load(object sender, EventArgs e)
        {
            Stuff_Refresh();
        }

        private void HRCP_Resize(object sender, EventArgs e)
        {
            try
            {
                Stuff_Resize();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            query = $"Select [ID], [Фамилия], [Имя], [Отчество], [Дата рождения], [Пол] FROM Users WHERE [ID] like '%%{textBox1.Text.Trim()}%%' or [Фамилия] like '%%{textBox1.Text.Trim()}%%' or [Имя] like '%%{textBox1.Text.Trim()}%%'" +
    $" or [Отчество] like '%%{textBox1.Text.Trim()}%%' or [Дата рождения] like '%%{textBox1.Text.Trim()}%%'";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet dss = new DataSet();
            dataadapter.Fill(dss, "Stuff_table_src");
            dataGridView1.DataSource = dss;
            dataGridView1.DataMember = "Stuff_table_src";
            Stuff_Resize();
        }
    }
}
