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
using EOkulProjesi.Formlar;

namespace EOkulProjesi.Formlar
{
    public partial class frmOgretmenListele : Form
    {
        public frmOgretmenListele()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
       
        private void frmOgretmenListele_Load(object sender, EventArgs e)
        {
            //dataGridView içerisine tblOgretmenler tabloundan gelen verileri çekme işlemi
          
            SqlCommand listele = new SqlCommand("select ogrtID as ID ,dersAd as Branş,ogrtAdSoyad as AdSoyad from tblOgretmenler inner join tblDersler on tblDersler.dersID=tblOgretmenler.ogrtBrans", bgl.baglanti());
            DataTable list = new DataTable();
            SqlDataAdapter dalist = new SqlDataAdapter(listele);
            dalist.Fill(list);
            dataGridView1.DataSource = list;
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmOgretmen ogretmen = new frmOgretmen();
            ogretmen.Show();
            this.Hide();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
