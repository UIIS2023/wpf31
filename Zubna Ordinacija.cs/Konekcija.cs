using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zubna_Ordinacija.cs
{
    class Konekcija
    {
        public SqlConnection KreirajKonekciju()
        {
            {
                SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder();
                ccnSb.DataSource = @"M\SQLEXPRESS";
                ccnSb.InitialCatalog = @"Zubna ordinacija";
                ccnSb.IntegratedSecurity = true;

                string con = ccnSb.ToString();
                SqlConnection konekcija = new SqlConnection(con);
                return konekcija;
            }
        }
    }
}
