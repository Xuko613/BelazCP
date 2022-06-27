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
        Auth auth = new Auth();
        public MainCP()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
           string query = $"UPDATE WorkTime SET [Закончил] = '{DateTime.Now}'," +
                $" [Отработал (часов)] = '{(DateTime.Now-Auth.StartWork).TotalHours}' where [ID] like '{Auth.WorkID}'";
            OleDbCommand com = new OleDbCommand(query, Auth.MyConn);
            com.ExecuteNonQuery();
            this.Close();
        }

        private void MainCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            auth.ID = null;
            auth.Show();
        }

        private void Cash_Click(object sender, EventArgs e)
        {
            CashCP cash = new CashCP();
            cash.ShowDialog();
        }

        private void Stock_Click(object sender, EventArgs e)
        {
            StockCP stock = new StockCP();
            stock.ShowDialog();
        }

        private void HR_Click(object sender, EventArgs e)
        {
            HRCP hrcp = new HRCP();
            hrcp.ShowDialog();
        }

        private void Settings_Click(object sender, EventArgs e)
        {

        }
    }
}
