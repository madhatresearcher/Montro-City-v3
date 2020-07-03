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
using System.Security.Cryptography;

namespace Montro_City_v3
{
    public partial class ProductAdd : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader sdr;
        ProductListForm flist;

        public ProductAdd()
        { }

        public ProductAdd(ProductListForm frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            flist = frm;
        }

        private void ClosePictureBox_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ScrapButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCategory()
        {
            CategoryComboBox.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select category from CategoryTable", cn);
            sdr = cm.ExecuteReader();
            while(sdr.Read())
            {
                CategoryComboBox.Items.Add(sdr[0].ToString());
            }sdr.Close();
            cn.Close();
        }

        public void LoadBrand()
        {
            CategoryComboBox.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select brand from BrandTable", cn);
            sdr = cm.ExecuteReader();
            while (sdr.Read())
            {
                BrandComboBox.Items.Add(sdr[0].ToString());
            }
            sdr.Close();
            cn.Close();
        }

        private void ProductAdd_Load(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Save this product?","Save Product",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    string bid="", cid="";
                    cn.Open();
                    cm = new SqlCommand("Select id from BrandTable where brand like '" + BrandComboBox.Text + "'", cn);
                    sdr = cm.ExecuteReader();
                    sdr.Read();
                    if(sdr.HasRows){bid = sdr[0].ToString();}
                    sdr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("Select id from CategoryTable where category like '" + CategoryComboBox.Text + "'", cn);
                    sdr = cm.ExecuteReader();
                    sdr.Read();
                    if (sdr.HasRows) { cid = sdr[0].ToString(); }
                    sdr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("INSERT INTO ProductTable (pcode, pdesc, bid, cid, price) VALUES (@pcode, @pdesc, @bid, @cid, @price)", cn);
                    cm.Parameters.AddWithValue("@pcode", PCODETextBox.Text);
                    cm.Parameters.AddWithValue("@pdesc", PDescTextBox.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", PriceTextBox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Saved.");
                    Clear();
                    flist.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            PCODETextBox.Clear();
            PDescTextBox.Clear();
            BrandComboBox.Text = "";
            CategoryComboBox.Text = "";
            PriceTextBox.Clear();
            PCODETextBox.Focus();
            SaveButton.Enabled = true;
            UpdateButton.Enabled = false;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Update Content?", "Update Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "", cid = "";
                    cn.Open();
                    cm = new SqlCommand("Select id from BrandTable where brand like '" + BrandComboBox.Text + "'", cn);
                    sdr = cm.ExecuteReader();
                    sdr.Read();
                    if (sdr.HasRows) { bid = sdr[0].ToString(); }
                    sdr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("Select id from CategoryTable where category like '" + CategoryComboBox.Text + "'", cn);
                    sdr = cm.ExecuteReader();
                    sdr.Read();
                    if (sdr.HasRows) { cid = sdr[0].ToString(); }
                    sdr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("UPDATE ProductTable SET pdesc=@pdesc, bid=@bid, cid=@cid, price=@price where pcode like @pcode", cn);
                    cm.Parameters.AddWithValue("@pcode", PCODETextBox.Text);
                    cm.Parameters.AddWithValue("@pdesc", PDescTextBox.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", PriceTextBox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Done.");
                    Clear();
                    flist.LoadRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}