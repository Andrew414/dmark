using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eventz
{
    public partial class AnalyzerForm : Form
    {
        public AnalyzerForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e) { }


            var reader = new System.IO.StreamReader(@"C:\Users\andre_000\Desktop\LogFile.CSV");
            string line;
            while (!String.IsNullOrWhiteSpace(line = reader.ReadLine()))
            {
                string[] str = line.Split(new char[] { ',' });
                betterListView1.Items.Add(new string[] { str[0], str[1], str[2], str[3], str[4]});
            }
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
