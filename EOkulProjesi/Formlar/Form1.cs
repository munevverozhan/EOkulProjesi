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

namespace EOkulProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //öğrenci girişi için girilen değerin  veritabanında olup olmadığının kontrol edilmesi.
            SqlCommand komut = new SqlCommand("select * from tblOgrenciler where ogrID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmOgrenciNotlar notlar = new frmOgrenciNotlar();
                notlar.numara = txtNumara.Text;
                notlar.Show();
                this.Hide();
            }
            else
            {
                 MessageBox.Show("Öğrenci bulunamadı.\nLütfen girilen numaranın doğru olduğundan emin olunuz !!");
            }
            bgl.baglanti().Close();


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //öğretmen girişi için girilen değerin veritabanında olup olmadığının kontrol edilmesi.
            SqlCommand komut = new SqlCommand("select * from tblOgretmenler where ogrtID=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p2", txtNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {

                frmOgretmen ogr = new frmOgretmen();
                ogr.ad = "HOŞ GELDİNİZ : "+dr[2];
                ogr.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Öğretmen bulunamadı.\nLütfen girilen numaranın doğru olduğundan emin olunuz !!");

            }
            bgl.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
