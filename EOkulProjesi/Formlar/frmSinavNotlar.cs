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
    public partial class frmSinavNotlar : Form
    {

        public frmSinavNotlar()
        {
            InitializeComponent();
        }

        void listele()
        {
            //seçilen ogrID değerine göre dataGridView1 içine ilgili değerlerin atanması işlemi
            SqlCommand komut = new SqlCommand(@"select notID,dersID,ogrAd,sinav1,sinav2,sinav3,proje,ortalama,durum from tblNotlar 
                                                inner join tblOgrenciler 
                                                on tblOgrenciler.ogrID=tblNotlar.ogrID where  tblNotlar.ogrID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbID.Text);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            txtSinav1.Text = "";
            txtSinav2.Text = "";
            txtSinav3.Text = "";
            txtProje.Text = "";
            txtDurum.Text = "";
            txtOrt.Text = "";

        }
        sqlBaglantisi bgl = new sqlBaglantisi();

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

        private void frmSinavNotlar_Load(object sender, EventArgs e)
        {

            // tblOgrenciler tablosundan öğrencilerin ID değerini comboBox'a çekme işlemi
            SqlCommand listeleID = new SqlCommand("select * from tblOgrenciler", bgl.baglanti());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(listeleID);
            da.Fill(dt);
            cmbID.DisplayMember = "ogrID";
            cmbID.DataSource = dt;

            //tblDersler tablosundan derslerin adını comboBox'a çekme işlemi
            SqlCommand listeleDersAd = new SqlCommand("select * from tblDersler", bgl.baglanti());
            DataTable ders = new DataTable();
            SqlDataAdapter daDers = new SqlDataAdapter(listeleDersAd);
            daDers.Fill(ders);
            cmbDers.DisplayMember = "dersAd";
            cmbDers.ValueMember = "dersID";
            cmbDers.DataSource = ders;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(@"select notID,dersID,ogrAd,sinav1,sinav2,sinav3,proje,ortalama,durum from tblNotlar 
                                                inner join tblOgrenciler 
                                                on tblOgrenciler.ogrID=tblNotlar.ogrID where  tblNotlar.notID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtNotID.Text);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNotID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtOrt.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            cmbDers.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            //notId' den gelen değere göre ders isminin comboBox'a atanması işlemi
            SqlCommand komut = new SqlCommand(@"select dersAd from tblNotlar
                                                inner join tblDersler
                                                on tblDersler.dersID=tblNotlar.dersID
                                                where  tblNotlar.notID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtNotID.Text);

            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDers.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        private void btnHesapla_Click(object sender, EventArgs e)
        {
            int s1, s2, s3, p1;
            double ort;
            string durum = "";
            try
            {
                s1 = Convert.ToInt32(txtSinav1.Text);
                s2 = Convert.ToInt32(txtSinav2.Text);
                s3 = Convert.ToInt32(txtSinav3.Text);
                p1 = Convert.ToInt32(txtProje.Text);
                ort = (double)(s1 + s2 + s3 + p1) / 4;
                txtOrt.Text = ort.ToString();

                if (ort >= 0 && ort < 55)
                {
                    txtDurum.Text = "KALDI";
                }
                else if (ort >= 55 && ort <= 100)
                {
                    txtDurum.Text = "GEÇTİ";
                }
                if (txtDurum.Text == "KALDI")
                {
                    durum = "false";
                }
                if (txtDurum.Text == "GEÇTİ")
                {
                    durum = "true";
                }

                SqlCommand ekle = new SqlCommand("update tblNotlar set sinav1=@p1,sinav2=@p2,sinav3=@p3,proje=@p4,ortalama=@p5,durum=@p6  where notID=@p7", bgl.baglanti());
                ekle.Parameters.AddWithValue("@p1", txtSinav1.Text);
                ekle.Parameters.AddWithValue("@p2", txtSinav2.Text);
                ekle.Parameters.AddWithValue("@p3", txtSinav3.Text);
                ekle.Parameters.AddWithValue("@p4", txtProje.Text);
                ekle.Parameters.AddWithValue("@p5", txtOrt.Text);
                ekle.Parameters.AddWithValue("@p6", durum);
                ekle.Parameters.AddWithValue("@p7", txtNotID.Text);
                ekle.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {            
                MessageBox.Show("lütfen tüm alanları doldurduğunuzdan emin olunuz!!");
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
