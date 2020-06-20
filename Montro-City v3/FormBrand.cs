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
    public partial class FormBrand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        public FormBrand()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            //  for (int i=1;i<=10;++i)
            //  {
            //      dataGridView1.Rows.Add(i, "1", "BRAND1 " + i);
            //  }
            LoadRecords();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2Brand form2Brand = new Form2Brand(this);
            form2Brand.SaveButton.Enabled = true;
            form2Brand.UpdateButton.Enabled = false;
            form2Brand.ShowDialog();
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from BrandTable order by id", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i,dr["id"].ToString(),dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(ColName=="Edit")
            {
                Form2Brand FormTwoBrand = new Form2Brand(this);
                FormTwoBrand.LabelOfID.Text= dataGridView1[1, e.RowIndex].Value.ToString();
                FormTwoBrand.BrandTextBox.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                FormTwoBrand.SaveButton.Enabled = false;
                FormTwoBrand.UpdateButton.Enabled = true;
                FormTwoBrand.ShowDialog();
            }
            else if(ColName=="Delete")
            {
                if(MessageBox.Show("Delete this record?","Delete Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from BrandTable where id like '" + dataGridView1[1, e.RowIndex].Value.ToString()+"'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Done!","POS",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadRecords();
                }
            }
        }
    }
}
