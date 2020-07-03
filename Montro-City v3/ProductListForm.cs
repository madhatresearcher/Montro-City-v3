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
    public partial class ProductListForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader sdr;

        public ProductListForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void ClosePictureBox_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            ProductAdd pad = new ProductAdd(this);
            pad.SaveButton.Enabled = true;
            pad.UpdateButton.Enabled = false;
            pad.LoadBrand();
            pad.LoadCategory();
            pad.ShowDialog();
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select p.pcode, p.pdesc, b.brand, c.category, p.price, p.qty from ProductTable as p inner join BrandTable as b on b.id = p.bid inner join CategoryTable as c on c.id = p.cid where p.pdesc like '"+TextSearchBox.Text+"%'",cn);
            //cm = new SqlCommand("SELECT * FROM ProductTable order by pcode", cn);
            sdr = cm.ExecuteReader();
            while(sdr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString(), sdr[3].ToString(), sdr[4].ToString(), sdr[5].ToString());
            }sdr.Close();
            cn.Close();
        }

        private void TextSearchBox_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(ColName=="Edit")
            {
                ProductAdd padlst = new ProductAdd(this);
                padlst.SaveButton.Enabled = false;
                padlst.UpdateButton.Enabled = true;
                padlst.PCODETextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                padlst.PDescTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                padlst.BrandComboBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                padlst.CategoryComboBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                padlst.PriceTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                padlst.ShowDialog();
            }
            else 
            {
                if (MessageBox.Show("Delete Record?","Delete?",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes) 
                {
                    cn.Open();
                    cm = new SqlCommand("delete from ProductTable where pcode like '"+ dataGridView1[1, e.RowIndex].Value.ToString() + "'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecords();
                }
            }
        }

        private void TextSearchBox_Click(object sender, EventArgs e)
        {

        }
    }
}