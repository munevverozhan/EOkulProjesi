using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EOkulProjesi
{
    class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source = DESKTOP-GBKS0E6; Initial Catalog = EOkul; Integrated Security = True");
            baglanti.Open();
            return baglanti;

        }
    }
}
