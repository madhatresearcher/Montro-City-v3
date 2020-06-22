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
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM CategoryTable order by id", cn);
            sdr = cm.ExecuteReader();
            while(sdr.Read())
            {
                i+=1;
                dataGridView1.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString());
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
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColName == "Edit")
            {
                CategoryAdd cade = new CategoryAdd(this);
                cade.LabelOfID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cade.CategoryTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cade.SaveButton.Enabled = false;
                cade.UpdateButton.Enabled = true;
                cade.ShowDialog();
            }
            else if (ColName == "Delete")
            {
                if (MessageBox.Show("Delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from CategoryTable where id like '" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Done!", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategory();
                }
            }
        }
    }
}
