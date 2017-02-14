using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cdLAYER
{
    public partial class Form1 : Form
    {
        private cFile file;
        public Form1()
        {
            InitializeComponent();
            file = new cFile();
        }

        private void button1_Add_Click(object sender, EventArgs e)
        {
            file.Add();
            file.Read(file.Filename);
            DataTable dt = Data.ConvertListToDataTable(file.List);
            this.dataGridView1.DataSource = dt;
        }
    }
}
