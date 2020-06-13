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

        public Form2Brand()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
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
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
