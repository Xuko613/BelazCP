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
        public StockCP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stock_Refresh();
        }
        
        private void Stock_Refresh()
        {
            string query = "SELECT * FROM Stock";
            OleDbDataAdapter dataadapter = new OleDbDataAdapter(query, Auth.MyConn);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Stock_table");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Stock_table";
        }
    }
}
