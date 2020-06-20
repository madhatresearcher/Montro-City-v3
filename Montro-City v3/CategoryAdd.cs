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
    public partial class CategoryAdd : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        CategoryListForm listForm;
        
        public CategoryAdd(CategoryListForm frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
           listForm = frm;
        }

        public CategoryAdd()
        {
            //this.categoryListForm = categoryListForm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ScrapButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            SaveButton.Enabled = true;
            UpdateButton.Enabled = false;
            CategoryTextBox.Clear();
            CategoryTextBox.Focus();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save this category?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT into CategoryTable(category) VALUES (@category)",cn);
                    cm.Parameters.AddWithValue("@category", CategoryTextBox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Done!");
                    Clear();
                    //listForm.LoadCategory();
                    listForm.LoadCategory();
                }
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoryAdd_Load(object sender, EventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

        }
    }
}
