using System;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class AddBill : Form
    {
        DateTimePicker oDateTimePicker;
        int SR;
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
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[0].Width = dataGridView1.Width / dataGridView1.Columns.Count - 1;
            }

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
            catch (Exception ex)
            {
                MessageBox.Show("Не корректно введены данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Auth.report += ex.ToString();
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
            dataGridView1.Rows[SR].Cells[Column3.Index].Value = oDateTimePicker.Text.ToString();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == Column1.Index)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
            else { e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Controls.Contains(oDateTimePicker) != false)
            {
                dataGridView1.Controls.Remove(oDateTimePicker);
            }
            if (e.ColumnIndex == Column3.Index && e.RowIndex == dataGridView1.CurrentCell.RowIndex)
            {
                SR = dataGridView1.CurrentCell.RowIndex;
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
            Auth.report += e.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Неверное значение";
            e.ThrowException = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}