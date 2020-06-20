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
    public partial class Form2Brand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        FormBrand frmlist;
        

        public Form2Brand(FormBrand flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = flist;
        }

        public Form2Brand()
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2Brand form2Brand = new Form2Brand();
            form2Brand.ShowDialog();
        }

        private void Form2Brand_Load(object sender, EventArgs e)
        {

        }

        private void Clear() 
        {
            SaveButton.Enabled = true;
            UpdateButton.Enabled = false;
            BrandTextBox.Clear();
            BrandTextBox.Focus();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try 
            {
                if (MessageBox.Show("Save???", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTo BrandTable(Brand)VALUEs(@brand)", cn);
                    cm.Parameters.AddWithValue("@brand", BrandTextBox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Saved!!!");
                    Clear();
                    frmlist.LoadRecords();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Proceed with brand edit?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                {
                    cn.Open();
                    cm = new SqlCommand("update BrandTable set brand = @brand where id like '"+LabelOfID.Text+"'",cn);
                    cm.Parameters.AddWithValue("@brand", BrandTextBox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand updated.");
                    Clear();
                    frmlist.LoadRecords();
                    this.Dispose();
                }
            }
            catch(Exception ex) 
            {
                
            }
        }

        private void ScrapButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //MessageBox.Show("Operation Cancelled");
        }
    }
}
