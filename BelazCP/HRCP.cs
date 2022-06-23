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
        DateTimePicker oDateTimePicker;
        public HRCP()
        {
            InitializeComponent();
        }

        private void Stuff_Refresh()
        {
            query = "SELECT [ID], [Фамилия], [Имя], [Отчество], [Должность],[Дата рождения], [Пол] FROM Users";
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
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dataGridView2.Columns[i].Width = dataGridView2.Width / dataGridView2.Columns.Count + 1;
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


        private void button1_Click(object sender, EventArgs e) //Поиск
        {
            query = $"Select [ID], [Фамилия], [Имя], [Отчество], [Дата рождения], [Пол] FROM Users WHERE [ID] like '%%{textBox1.Text.Trim()}%%' or [Фамилия] like '%%{textBox1.Text.Trim()}%%' or [Имя] like '%%{textBox1.Text.Trim()}%%'" +
    $" or [Отчество] like '%%{textBox1.Text.Trim()}%%' or [Дата рождения] like '%%{textBox1.Text.Trim()}%%'or [Должность] like '%%{textBox1.Text.Trim()}%%'";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet dss = new DataSet();
            dataadapter.Fill(dss, "Stuff_table_src");
            dataGridView1.DataSource = dss;
            dataGridView1.DataMember = "Stuff_table_src";
            Stuff_Resize();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                OleDbCommand com;
                textBox2.Text = $"{dataGridView1.SelectedCells[1].Value} {dataGridView1.SelectedCells[2].Value} {dataGridView1.SelectedCells[3].Value}"; //ФИО
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedCells[5].Value.ToString()); //Дата рождения
                textBox3.Text = $"{dataGridView1.SelectedCells[6].Value}"; //Пол
                textBox4.Text = $"{dataGridView1.SelectedCells[0].Value}"; //ID
                textBox5.Text = $"{dataGridView1.SelectedCells[4].Value}"; //Должность  
                //Доступ к кадрам (ДКК)
                query = $"Select ДКК FROM Rights where ID like {dataGridView1.SelectedCells[0].Value}";
                com = new OleDbCommand(query, Auth.MyConn);
                checkBox1.Checked = bool.Parse(com.ExecuteScalar().ToString());
                //Доступ к складу (ДКС)
                query = $"Select ДКС FROM Rights where ID like {dataGridView1.SelectedCells[0].Value}";
                com = new OleDbCommand(query, Auth.MyConn);
                checkBox2.Checked = bool.Parse(com.ExecuteScalar().ToString());
                //Доступ к банку (ДКБ)
                query = $"Select ДКБ FROM Rights where ID like {dataGridView1.SelectedCells[0].Value}";
                com = new OleDbCommand(query, Auth.MyConn);
                checkBox3.Checked = bool.Parse(com.ExecuteScalar().ToString());
                Child_Refresh();
            }
        }

        private void Child_Refresh()
        {
            query = $"SELECT [ID], [Фамилия], [Имя], [Отчество], [Дата рождения], [Пол] FROM Childs Where W_ID like {dataGridView1.SelectedCells[0].Value}";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet dsc = new DataSet();
            dataadapter.Fill(dsc, "Child_table");
            dataGridView2.DataSource = dsc;
            dataGridView2.DataMember = "Child_table";
            dataGridView2.Columns[0].ReadOnly = true;
            Stuff_Resize();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stuff_Refresh();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) //Режим редактирования
        {
            сохранитьToolStripMenuItem.Enabled = true;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            dateTimePicker1.Enabled = true;
            textBox5.ReadOnly = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            query = $"INSERT INTO Childs ([W_ID]) VALUES ('{dataGridView1.SelectedCells[0].Value}')";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            com.ExecuteNonQuery();
        }

        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Удалить запись? Все изменения будут потеряны!", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                query = $"Delete from Childs where [ID] like {dataGridView2.SelectedCells[0].Value}";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();
                MessageBox.Show("Запись удалена", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dataGridView2_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[4].Value = DateTime.Today.ToShortDateString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            oDateTimePicker.Visible = false;
        }
        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell.Value = oDateTimePicker.Text.ToString();
        }

        private void save_changes()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                query = $"UPDATE Childs SET [Фамилия] = '{row.Cells[1].Value}'," +
            $" [Имя] = '{row.Cells[2].Value}'," +
            $" [Отчество] = '{row.Cells[3].Value}'," +
            $" [Дата рождения] = '{row.Cells[4].Value}'," +
            $" [Пол]= '{row.Cells[5].Value}' Where [ID] like {row.Cells[0].Value}";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            save_changes();
        }
    }
}