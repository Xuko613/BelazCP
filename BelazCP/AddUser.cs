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
    public partial class AddUser : Form
    {
        string query;
        DateTimePicker oDateTimePicker;
        int SR = 0;
        public static string pass = "qwerty";
        public AddUser()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetPass setPass = new SetPass();
            setPass.ShowDialog();
            Save_User();
            MessageBox.Show("Упешно добавлен", "Упех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        private void Save_User()
        {

            string[] fio = textBox2.Text.Trim().Split(' ');
            query = $"INSERT INTO Users ([ID], [Фамилия], [Имя], [Отчество], [Дата рождения], [Пол], [Должность], [Пароль]) " +
                   $"VALUES ('{textBox4.Text}', '{fio[0]}', '{fio[1]}', '{fio[2]}', '{dateTimePicker1.Value}', '{textBox3.Text.ToUpper()}','{textBox5.Text}', '{pass}')";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            com.ExecuteNonQuery();
            query = $"INSERT INTO Rights ([ID], [ДКК], [ДКБ], [ДКС]) " +
                  $"VALUES ('{textBox4.Text}', {checkBox1.Checked}, {checkBox3.Checked}, {checkBox2.Checked})";
            OleDbCommand com2 = new OleDbCommand(query, Auth.MyConn);
            com2.ExecuteNonQuery();

            Child_Save();
        }
        private void Child_Save()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                query = $"INSERT INTO Childs ([W_ID], [Фамилия], [Имя], [Отчество], [Дата рождения], [Пол]) " +
                    $"VALUES ('{textBox4.Text}', '{row.Cells[0].Value}', '{row.Cells[1].Value}', '{row.Cells[2].Value}', '{row.Cells[3].Value}','{row.Cells[4].Value}')";
                OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                com.ExecuteNonQuery();
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }
        private void AddUser_Resize(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].Width = dataGridView2.Width / dataGridView2.Columns.Count + 1;
                }
            }
            catch (Exception ex)
            {
                Auth.report += ex.ToString();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex == dataGridView2.CurrentCell.RowIndex)
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
            dataGridView2.Rows[SR].Cells[3].Value = oDateTimePicker.Text.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}