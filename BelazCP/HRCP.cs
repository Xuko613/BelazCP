using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class HRCP : Form
    {
        string query;
        DateTimePicker oDateTimePicker;
        int SR = 0;
        public static string FIO;
        public static string SID;
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
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                dataGridView3.Columns[i].Width = dataGridView3.Width / dataGridView3.Columns.Count + 1;
            }
            for (int i = 0; i < dataGridView4.Columns.Count; i++)
            {
                dataGridView4.Columns[i].Width = dataGridView4.Width / dataGridView4.Columns.Count + 1;
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
            catch (Exception ex)
            {
                Auth.report += ex.ToString();
            }
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
                try
                {
                    OleDbCommand com;
                    textBox2.Text = $"{dataGridView1.SelectedCells[1].Value} {dataGridView1.SelectedCells[2].Value} {dataGridView1.SelectedCells[3].Value}"; //ФИО
                    FIO = textBox2.Text;
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedCells[5].Value.ToString()); //Дата рождения
                    textBox3.Text = $"{dataGridView1.SelectedCells[6].Value}"; //Пол
                    textBox4.Text = $"{dataGridView1.SelectedCells[0].Value}"; //ID
                    SID = textBox4.Text;
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
                    WorkTime_Refresh();
                    Reprimands_Refresh();
                }
                catch (Exception ex)
                {
                    Auth.report += ex.ToString();
                }
            }
        }

        private void WorkTime_Refresh()
        {
            query = $"SELECT [ID], [Приступил], [Закончил], [Отработал (часов)], [Комментарий] FROM WorkTime Where W_ID like '{dataGridView1.SelectedCells[0].Value}'";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet dsWT = new DataSet();
            dataadapter.Fill(dsWT, "WorkTime_table");
            dataGridView3.DataSource = dsWT;
            dataGridView3.DataMember = "WorkTime_table";
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[2].ReadOnly = true;
            dataGridView3.Columns[3].ReadOnly = true;
            Stuff_Resize();
        }
        private void Reprimands_Refresh()
        {
            query = $"SELECT [Причина], [Дата], [Действует до] FROM Reprimands Where W_ID like '{dataGridView1.SelectedCells[0].Value}'";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet dsRT = new DataSet();
            dataadapter.Fill(dsRT, "Reprimands_table");
            dataGridView4.DataSource = dsRT;
            dataGridView4.DataMember = "Reprimands_table";
            Stuff_Resize();
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (DateTime.Parse(row.Cells[2].Value.ToString()) > DateTime.Now)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
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
            dataGridView2.Columns[4].ReadOnly = true;
            Stuff_Resize();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stuff_Refresh();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) //Режим редактирования
        {
            button6.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            dateTimePicker1.Enabled = true;
            textBox5.ReadOnly = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            dataGridView2.Enabled = true;
            dataGridView3.Enabled = true;
            изменитьToolStripMenuItem.Enabled = false;
            button5.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
        }


        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            query = $"INSERT INTO Childs ([W_ID], [Фамилия], [Дата рождения]) VALUES ('{dataGridView1.SelectedCells[0].Value}', '{dataGridView2.SelectedCells[1].Value}', '{dataGridView2.SelectedCells[4].Value}')";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            com.ExecuteNonQuery();
            Child_Save();
            Child_Refresh();
            dataGridView2.ClearSelection();
            dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[0];

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
            e.Row.Cells[1].Value = dataGridView1.SelectedCells[1].Value;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Controls.Contains(oDateTimePicker) != false)
            {
                dataGridView2.Controls.Remove(oDateTimePicker);
            }
            if (e.ColumnIndex == 4 && e.RowIndex == dataGridView2.CurrentCell.RowIndex)
            {
                SR = e.RowIndex;
                oDateTimePicker = new DateTimePicker();
                dataGridView2.Controls.Add(oDateTimePicker);
                oDateTimePicker.Format = DateTimePickerFormat.Short;
                Rectangle oRectangle = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);
                oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);
                oDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);
                oDateTimePicker.TextChanged += new EventHandler(dateTimePicker_OnTextChange);
                oDateTimePicker.Visible = true;
            }
        }
        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            oDateTimePicker.Visible = false;
        }
        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            dataGridView2.Rows[SR].Cells[4].Value = oDateTimePicker.Text.ToString();
        }

        private void Child_Save()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                try
                {
                    query = $"UPDATE Childs SET [Фамилия] = '{row.Cells[1].Value}'," +
                $" [Имя] = '{row.Cells[2].Value}'," +
                $" [Отчество] = '{row.Cells[3].Value}'," +
                $" [Дата рождения] = '{row.Cells[4].Value}'," +
                $" [Пол]= '{row.Cells[5].Value}' Where [ID] like {row.Cells[0].Value}";
                    OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Auth.report += ex.ToString();
                }
            }
        }
        private void Edit_Save()
        {
            try
            {
                string[] fio = textBox2.Text.Trim().Split(' ');
                query = $"UPDATE Users SET [Фамилия] = '{fio[0]}'," +
                    $" [Имя] = '{fio[1]}'," +
                    $" [Отчество] = '{fio[2]}'," +
                    $" [Дата рождения] = '{dateTimePicker1.Value}'," +
                    $" [Пол]= '{textBox3.Text.ToUpper()}'," +
                    $" [Должность] = '{textBox5.Text}' where [ID] like {textBox4.Text}";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();

                query = $"UPDATE Rights SET [ДКК] = {checkBox1.Checked}," +
                    $" [ДКС] = {checkBox2.Checked}," +
                    $" [ДКБ] = {checkBox3.Checked} where [ID] like {textBox4.Text}";
                OleDbCommand com2 = new OleDbCommand(query, Auth.MyConn);
                com2.ExecuteNonQuery();

                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    query = $"UPDATE WorkTime SET [Комментарий] = '{row.Cells[4].Value}' where [ID] like {row.Cells[0].Value}";
                    OleDbCommand com3 = new OleDbCommand(query, Auth.MyConn);
                    com3.ExecuteNonQuery();
                }

                button6.Enabled = false;
                сохранитьToolStripMenuItem.Enabled = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                dateTimePicker1.Enabled = false;
                textBox5.ReadOnly = true;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                dataGridView2.Enabled = false;
                dataGridView3.Enabled = false;
                изменитьToolStripMenuItem.Enabled = true;
                button5.Enabled = false;

            }
            catch (Exception ex)
            {
                Auth.report += ex.ToString();
            }
            Child_Save();
            Stuff_Refresh();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Edit_Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddRep addRep = new AddRep();
            addRep.ShowDialog();
            if (addRep.DialogResult == DialogResult.OK)
            {
                Reprimands_Refresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangePass changePass = new ChangePass();
            changePass.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0].Value.ToString() != Auth.ID.ToString())
            {
                DialogResult result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    query = $"Delete from Users where [ID] like {dataGridView1.SelectedCells[0].Value}";
                    OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Запись удалена", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Вы не можете удалить себя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}