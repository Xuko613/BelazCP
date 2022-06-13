﻿using System;
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
        private OleDbConnection MyConn;
        public CashCP()
        {
            InitializeComponent();
        }

        private void Cash_Refresh()
        {
            MyConn = new OleDbConnection(Auth.connStr);
            string query = "SELECT * FROM Cash";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, MyConn);
            DataSet ds = new DataSet();
            MyConn.Open();
            dataadapter.Fill(ds, "Cash_table");
            MyConn.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Cash_table";
            dataGridView1.Columns[0].Width = dataGridView1.Width / 6;
            dataGridView1.Columns[1].Width = dataGridView1.Width / 6;
            dataGridView1.Columns[2].Width = dataGridView1.Width / 6;
            dataGridView1.Columns[3].Width = dataGridView1.Width / 6;
            dataGridView1.Columns[4].Width = dataGridView1.Width / 6;
            dataGridView1.Columns[5].Width = dataGridView1.Width / 6;
            decimal cash = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString().Trim() == "Пополнение")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    cash += decimal.Parse(row.Cells[1].Value.ToString().Trim());

                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    cash -= decimal.Parse(row.Cells[1].Value.ToString().Trim());
                }
                if (cash > 0)
                {
                    label1.ForeColor = Color.Green;
                }
                else if(cash<0)
                {
                    label1.ForeColor= Color.Red;
                }
                else
                {
                    label1.ForeColor = Color.Black;
                }
                label1.Text = $"{cash.ToString().Trim()} BYN";
            }
        }

        private void CashCP_Activated(object sender, EventArgs e)
        {
            Cash_Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void CashCP_Resize(object sender, EventArgs e)
        {
            Cash_Refresh();
        }
    }
}