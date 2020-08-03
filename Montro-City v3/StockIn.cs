using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Montro_City_v3
{
    public partial class StockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader sdr;
        string stitle = "Point Of Sale";

        public StockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select pcode, pdesc, qty from ProductTable where pdesc like '%"+TextSearchBox.Text+"%' order by pdesc",cn);
            sdr = cm.ExecuteReader();
            while (sdr.Read()) { i++; dataGridView1.Rows.Add(sdr[0].ToString(),sdr[1].ToString(),sdr[2].ToString()); }
            sdr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(ColName=="ColSelect")
            {
                if(MessageBox.Show("Add this Item?","Affirmation?",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("select * from ProductTable where pcode like'" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cn.Close();
                }
             }
        }
    }
}