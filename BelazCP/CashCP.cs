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
    public partial class CashCP : Form
    {
        AddBill addBill = new AddBill();
        string query;
        public CashCP()
        {
            InitializeComponent();
        } 
        private void Cash_Refresh()
        {
            query = "SELECT * FROM Cash";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Cash_table");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Cash_table";
            Cash_Resize();
            Cash_Calc();
        }
        private void Cash_Calc()
        {
            decimal cash = 0;
            decimal yearCash = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString().Trim() == "Пополнение")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    if (DateTime.Parse(row.Cells[3].Value.ToString()).Month == DateTime.Today.Month && DateTime.Parse(row.Cells[3].Value.ToString()).Year == DateTime.Today.Year)
                    {
                        cash += decimal.Parse(row.Cells[1].Value.ToString().Trim());
                    }
                    if (DateTime.Parse(row.Cells[3].Value.ToString()).Year == DateTime.Today.Year)
                    {
                        yearCash += decimal.Parse(row.Cells[1].Value.ToString().Trim());
                    }
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    if (DateTime.Parse(row.Cells[3].Value.ToString()).Month == DateTime.Today.Month && DateTime.Parse(row.Cells[3].Value.ToString()).Year == DateTime.Today.Year)
                    {
                        cash -= decimal.Parse(row.Cells[1].Value.ToString().Trim());
                    }
                    if (DateTime.Parse(row.Cells[3].Value.ToString()).Year == DateTime.Today.Year)
                    {
                        yearCash -= decimal.Parse(row.Cells[1].Value.ToString().Trim());
                    }
                }

                if (cash > 0)
                {
                    label1.ForeColor = Color.Green;
                }
                else if (cash < 0)
                {
                    label1.ForeColor = Color.Red;
                }
                else
                {
                    label1.ForeColor = Color.Black;
                }
                if (yearCash > 0)
                {
                    label2.ForeColor = Color.Green;
                }
                else if (yearCash < 0)
                {
                    label2.ForeColor = Color.Red;
                }
                else
                {
                    label2.ForeColor = Color.Black;
                }
                label1.Text = $"{cash.ToString("C").Trim()}";
                label2.Text = $"{yearCash.ToString("C").Trim()}";
            }
        }
        private void Cash_Resize()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count+1;
            }
        }
        private void CashCP_Activated(object sender, EventArgs e)
        {
            if (addBill.DialogResult == DialogResult.OK)
            {
                Cash_Refresh();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddBill addBill = new AddBill();
            addBill.ShowDialog();
        }
        private void CashCP_Resize(object sender, EventArgs e)
        {
            try
            {
                Cash_Resize();
            }
            catch
            {

            }
        }
        private void CashCP_Load(object sender, EventArgs e)
        {
            Cash_Refresh();
        }
        private void обновитьF5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cash_Refresh();
        }
        private void новаяЗаписьF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addBill.ShowDialog();
        }
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            Cash_Calc();
        }
    }
}