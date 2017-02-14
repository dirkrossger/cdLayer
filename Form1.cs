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
            //DataTable dt = cData.ConvertListToDataTable(file.List);
            //this.dataGridView1.DataSource = dt;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 200;

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = file.List;

            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[1];
            //    if (chk.Selected == true)
            //    {
            //        chk.Selected = false;
            //    }
            //    else
            //    {
            //        chk.Selected = true;
            //    }
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DataGridViewButtonCell IsOff = new DataGridViewButtonCell();
            DataGridViewButtonCell IsFrozen = new DataGridViewButtonCell();
        }
    }
}
