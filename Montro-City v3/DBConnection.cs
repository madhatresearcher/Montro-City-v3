using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montro_City_v3
{
    class DBConnection
    {
        public string MyConnection()
        {
            string con = @"Data Source=LONDORIATH;Initial Catalog=montrocitydb;Integrated Security=True";
            return con;
        }
    }
}