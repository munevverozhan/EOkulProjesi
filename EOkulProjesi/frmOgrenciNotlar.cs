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

namespace EOkulProjesi
{
    public partial class frmOgrenciNotlar : Form
    {
        public frmOgrenciNotlar()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();

        public string numara;
        private void frmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            //dataGridView1 içerisinde; girilen numara değerine göre notların listelenmesi
            SqlCommand komut = new SqlCommand(@"select dersAd,sinav1,sinav2,sinav3,proje,ortalama,durum from tblNotlar
                                              inner join tblDersler on tblDersler.dersID = tblNotlar.dersID  where ogrID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            // Formun name kısmına öğrencinin ad-soyad değerinin atanması
            SqlCommand komut2 = new SqlCommand("select ogrAd,ogrSoyad from tblOgrenciler where ogrID=@p2",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p2", numara);
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                this.Text = "Öğrenci : "+dr[0] +" "+dr[1];
            }
            bgl.baglanti().Close();
            

        }
    }
}
