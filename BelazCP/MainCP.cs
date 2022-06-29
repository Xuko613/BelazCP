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
    public partial class MainCP : Form
    {

        public MainCP()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            string query = $"UPDATE WorkTime SET [Закончил] = '{DateTime.Now}'," +
                 $" [Отработал (часов)] = '{(DateTime.Now - Auth.StartWork).TotalHours}' where [ID] like '{Auth.WorkID}'";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            com.ExecuteNonQuery();
            this.Close();
        }

        private void MainCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            Auth auth = new Auth();
            Auth.ID = null;
            auth.Show();
        }

        private void Cash_Click(object sender, EventArgs e)
        {
            string query = $"SELECT ДКБ FROM Rights WHERE ID like '{Auth.ID}'";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            if (bool.Parse(com.ExecuteScalar().ToString()))
            {
                CashCP cash = new CashCP();
                cash.ShowDialog();

            }
            else
            {
                MessageBox.Show("Нет прав для просмотра этого раздела", "Нет прав", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Stock_Click(object sender, EventArgs e)
        {
            string query = $"SELECT ДКС FROM Rights WHERE ID like '{Auth.ID}'";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            if (bool.Parse(com.ExecuteScalar().ToString()))
            {
                StockCP stock = new StockCP();
                stock.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нет прав для просмотра этого раздела", "Нет прав", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void HR_Click(object sender, EventArgs e)
        {
            string query = $"SELECT ДКК FROM Rights WHERE ID like '{Auth.ID}'";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            if (bool.Parse(com.ExecuteScalar().ToString()))
            {
                HRCP hrcp = new HRCP();
                hrcp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нет прав для просмотра этого раздела", "Нет прав", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            string query = $"SELECT ДКН FROM Rights WHERE ID like '{Auth.ID}'";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            if (bool.Parse(com.ExecuteScalar().ToString()))
            {
                Settings set = new Settings();
                set.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нет прав для просмотра этого раздела", "Нет прав", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}