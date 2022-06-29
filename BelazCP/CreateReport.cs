using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BelazCP
{
    public partial class CreateReport : Form
    {
        public CreateReport()
        {
            InitializeComponent();
        }

        private void CreateReport_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (Auth.report == "" || Auth.report == null)
            { richTextBox1.Text = "Ошибок не обнаруженно."; }
            else
            {
                richTextBox1.Text = Auth.report;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = $"{System.Environment.CurrentDirectory}/Reports/report [{DateTime.Now:dd.MM.yyyy (HH-mm-ss)}].txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(richTextBox1.Text);
                }
            }
        }
    }
}
