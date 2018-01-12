using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking
{
    public static class Global
    {
        public static SqlCommand cmd;
        public static SqlConnection cs;
        public static SqlDataReader rdr;
        public static string con=  @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";




    }
}
