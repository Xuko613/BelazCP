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
using System.Globalization;

namespace BelazCP
{
    public partial class AddBill : Form
    {
        DateTimePicker oDateTimePicker;
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
                        if (row.Cells[1].Value != null && row.Cells[3].Value != null && row.Cells[4].Value != null)
                        {
                            dataGridView1.Sort(dataGridView1.Columns["Column3"], ListSortDirection.Ascending);
                            decimal bmoney = decimal.Parse(row.Cells[0].Value.ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("be-BY"));
                            string btype = row.Cells[1].Value.ToString().Trim();
                            DateTime bdate = DateTime.Parse(row.Cells[2].Value.ToString().Trim());
                            string botv = row.Cells[3].Value.ToString().Trim();
                            string breason = row.Cells[4].Value.ToString().Trim();
                            string query = $"insert into Cash ([Сумма], [Тип], [Дата], [Ответственный], [Комментрарий]) values ('{bmoney}','{btype}','{bdate}','{botv}','{breason}')";
                            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
                            com.ExecuteNonQuery();
                        }
                        else { throw new Exception(); }
                    }
                }
                MessageBox.Show("Все записи внесены в базу данных.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Не корректно введены данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBill_Resize(object sender, EventArgs e)
        {
            Bill_Refresh();
        }

        private void AddBill_Load(object sender, EventArgs e)
        {
            string query = "select ID from Users";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            OleDbDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                Column4.Items.Add(reader[0].ToString().Trim());
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            oDateTimePicker.Visible = false;
        }
        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = oDateTimePicker.Text.ToString();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == Column1.Index)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Column3.Index)
            {
                oDateTimePicker = new DateTimePicker();
                dataGridView1.Controls.Add(oDateTimePicker);
                oDateTimePicker.Format = DateTimePickerFormat.Short;
                Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);
                oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);
                oDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);
                oDateTimePicker.TextChanged += new EventHandler(dateTimePicker_OnTextChange);
                oDateTimePicker.Visible = true;
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Column3"].Value = DateTime.Today.ToShortDateString();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Неверное значение";
            e.ThrowException = false;
        }
    }
}