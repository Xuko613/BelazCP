using BelazCP.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace BelazCP
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Все данные будут потеряны", "Вы уверены?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Auth.MyConn.Close();
                File.Copy($"{System.Environment.SpecialFolder.ApplicationData}/BelazCP/DB.mdb", $"{System.Environment.SpecialFolder.ApplicationData}/BelazCP/DB.mdb.old [{DateTime.Now:dd.MM.yyyy}]");
                File.WriteAllBytes($"{System.Environment.SpecialFolder.ApplicationData}/BelazCP/DB.mdb", Resources.DB);
                foreach (var process in System.Diagnostics.Process.GetProcessesByName("BelazCP"))
                {
                    process.Kill();
                }
            }
            else { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Xuko613/BelazCP");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateReport report = new CreateReport();
            report.ShowDialog();
        }
    }
}
