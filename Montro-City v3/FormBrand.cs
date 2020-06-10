using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Montro_City_v3
{
    public partial class FormBrand : Form
    {
        public FormBrand()
        {
            InitializeComponent();
            for(int i=1;i<=10;++i)
            {
                dataGridView1.Rows.Add(i, "1", "BRAND1 " + i);
            }
        }
    }
}
