using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Montro_City_v3
{
    public partial class CategoryListForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader sdr;

        public CategoryListForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCategory()
        {
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM CategoryTable order by category", cn);
            sdr = cm.ExecuteReader();
            while(sdr.Read())
            {
                dataGridView1.Rows.Add(sdr[1].ToString());
            }
            sdr.Close();
            cn.Close();
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            CategoryAdd cadd = new CategoryAdd(this);
            cadd.SaveButton.Enabled = true;
            cadd.UpdateButton.Enabled = false;
            cadd.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
