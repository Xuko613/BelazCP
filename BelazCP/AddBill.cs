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
    public partial class AddBill : Form
    {
        public AddBill()
        {
            InitializeComponent();
        }

        private void AddBill_Activated(object sender, EventArgs e)
        {
            Bill_Refresh();
        }
        public void Bill_Refresh()
        {
            dataGridView1.Columns[0].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[1].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[2].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[3].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[4].Width = dataGridView1.Width / 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index < dataGridView1.Rows.Count - 1)
                    {
                        double bmoney = double.Parse(row.Cells[0].Value.ToString().Trim());
                        string btype = row.Cells[1].Value.ToString().Trim();
                        DateTime bdate = DateTime.Parse(row.Cells[2].Value.ToString().Trim());
                        string botv = row.Cells[3].Value.ToString().Trim();
                        string breason = row.Cells[4].Value.ToString().Trim();
                        string query = $"insert into Cash ([Сумма], [Тип], [Время], [Ответственный], [Причина]) values ({bmoney},'{btype}','{bdate}','{botv}','{breason}')";
                        OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }

        private void AddBill_Resize(object sender, EventArgs e)
        {
            Bill_Refresh();
        }
    }
}