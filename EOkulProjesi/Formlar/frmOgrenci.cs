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

namespace EOkulProjesi.Formlar
{
    public partial class frmOgrenci : Form
    {
        public frmOgrenci()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblOgrenciler",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbKulup.Text = "";
            
        }

        private void frmOgrenci_Load(object sender, EventArgs e)
        {
            listele();

            //kulupleri veritabanından comboBox' çekme işlemi
           
            SqlCommand kulupListele = new SqlCommand("select * from tblKulupler", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(kulupListele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKulup.DisplayMember = "kulupAd"; // gözüken değer
            cmbKulup.ValueMember = "kulupID"; //arka planda tutulan değer
            cmbKulup.DataSource = dt;

        }


        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmOgretmen frmOgretmen = new frmOgretmen();
            frmOgretmen.Show();
            this.Hide();
        }
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbKulup.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            //öğrencinin kulubünü gösteren sorgu
            SqlCommand kulup = new SqlCommand(@"select kulupAd from tblOgrenciler 
                                              inner join  tblKulupler
                                              on tblKulupler.kulupID = tblOgrenciler.ogrKulup
                                              where ogrID =@p1", bgl.baglanti());
            kulup.Parameters.AddWithValue("@p1", txtID.Text);
            SqlDataReader dr = kulup.ExecuteReader();
            while (dr.Read())
            {
                cmbKulup.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();



            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() == "Kız")
            {
                rbtnKiz.Checked=true;
            }
            else
            {
                rbtnErkek.Checked = true;
            }
          

        }

        
        private void rbtnKiz_CheckedChanged(object sender, EventArgs e)
        {
            rbtnKiz.Text = "Kız";
        }

        private void rbtnErkek_CheckedChanged(object sender, EventArgs e)
        {
            rbtnErkek.Text = "Erkek";
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            string cinsiyet="";
            if (rbtnKiz.Checked == true)
            {
                cinsiyet = "Kız";
            }
            if(rbtnErkek.Checked==true)
            {
                cinsiyet = "Erkek";
            }
            SqlCommand ekle = new SqlCommand("insert into tblOgrenciler (ogrAd,ogrSoyad,ogrKulup,ogrCinsiyet) values(@p1,@p2,@p3,@p4) ", bgl.baglanti());
            ekle.Parameters.AddWithValue("@p1", txtAd.Text);
            ekle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            ekle.Parameters.AddWithValue("@p3",cmbKulup.SelectedValue.ToString()); // value değerin tutar.
            ekle.Parameters.AddWithValue("@p4",cinsiyet);
            if (txtAd.Text == "" || txtAd.Text == " ")
            {
                MessageBox.Show("Lütfen bir isim giriniz..");
                
            }
            if (txtSoyad.Text == "" || txtSoyad.Text == " ")
            {
                MessageBox.Show("Lütfen soyad giriniz..");

            }
            if (cmbKulup.Text == "" || cmbKulup.Text == " ")
            {
                MessageBox.Show("Lütfen kulüp seçiniz..");

            }
            if (rbtnKiz.Checked == false && rbtnErkek.Checked == false)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz..");

            }
            else
            {
                ekle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("öğrenci eklendi.");
                listele();
                temizle();
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("delete from tblOgrenciler where ogrID=@p1", bgl.baglanti());
            sil.Parameters.AddWithValue("@p1", txtID.Text);
            sil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("öğrenci kaydı silindi.");
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (rbtnKiz.Checked == true)
            {
                cinsiyet = "Kız";
            }
            if (rbtnErkek.Checked == true)
            {
                cinsiyet = "Erkek";
            }
            SqlCommand guncelle = new SqlCommand("update tblOgrenciler set ogrAd=@p1,ogrSoyad=@p2,ogrKulup=@p3,ogrCinsiyet=@p4 where ogrID=@p5", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            guncelle.Parameters.AddWithValue("@p2",txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@p3",cmbKulup.SelectedValue.ToString());
            guncelle.Parameters.AddWithValue("@p4",cinsiyet);
            guncelle.Parameters.AddWithValue("@p5",txtID.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("öğrenci kaydınız güncellendi.");
            listele();
            temizle();

        }
    }
}
