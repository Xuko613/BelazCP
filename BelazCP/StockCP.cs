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
    public partial class StockCP : Form
    {
        string query;
        public StockCP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stock_Save();
        }

        private void Stock_Refresh()
        {
            query = "SELECT * FROM Stock";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Stock_table");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Stock_table";
            dataGridView1.Columns[0].ReadOnly = true;
            Stock_Resize();
        }
        private void Stock_Resize()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count + 1;
            }
        }
        private void Stock_Save()
        {
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                query = $"UPDATE Stock SET [Тип] = '{row.Cells[1].Value.ToString().Trim()}'," +
                    $" [Наименование] = '{row.Cells[2].Value.ToString().Trim()}'," +
                    $" [Вес (кг)] = '{row.Cells[3].Value.ToString().Trim()}'," +
                    $" [Размер (м)] = '{row.Cells[4].Value.ToString().Trim()}'," +
                    $" [Количество] = '{row.Cells[5].Value.ToString().Trim()}'," +
                    $" [Местоположение] = '{row.Cells[6].Value.ToString().Trim()}'," +
                    $" [Комментарий] = '{row.Cells[7].Value.ToString().Trim()}'" +
                    $" WHERE [Код объекта] = {row.Cells[0].Value.ToString().Trim()}";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();
            }
            button1.Enabled = false;
            сохранитьToolStripMenuItem.Enabled = false;
            Stock_Refresh();
        }

        private void StockCP_Load(object sender, EventArgs e)
        {
            Stock_Refresh();
        }

        private void StockCP_Resize(object sender, EventArgs e)
        {
            try
            {
                Stock_Resize();
            }
            catch (Exception ex) 
            {
                Auth.report += ex.ToString();
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_Refresh();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock_Save();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = $"Select * FROM Stock WHERE [Код объекта] like '%%{textBox1.Text.Trim()}%%' or [Тип] like '%%{textBox1.Text.Trim()}%%' or [Наименование] like '%%{textBox1.Text.Trim()}%%'" +
                $" or [Местоположение] like '%%{textBox1.Text.Trim()}%%' or [Комментарий] like '%%{textBox1.Text.Trim()}%%'";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet dss = new DataSet();
            dataadapter.Fill(dss, "Stock_table_src");
            dataGridView1.DataSource = dss;
            dataGridView1.DataMember = "Stock_table_src";
            dataGridView1.Columns[0].ReadOnly = true;
            Stock_Resize();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Удалить запись? Все изменения будут потеряны!", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                query = $"Delete from Stock where [Код объекта] like {dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value}";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();
                MessageBox.Show("Запись удалена", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Stock_Refresh();
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Все изменения будут потеряны! Продолжить?", "Новая запись", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                query = $"INSERT INTO Stock ([Тип]) VALUES ('')";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();
                Stock_Refresh();
            }
        }

        private void StockCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button1.Enabled == true)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Stock_Save();
                }
            }
        }
    }
}