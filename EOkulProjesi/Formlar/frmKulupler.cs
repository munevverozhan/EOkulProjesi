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
    public partial class frmKulupler : Form
    {
        public frmKulupler()
        {
            InitializeComponent();
        }
        void listele()
        {
            //veritabanında bulunan kulüp bilgilerini dataGridView1'de listeleme işlemi
            SqlCommand komut = new SqlCommand("select * from tblKulupler", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
        }
        sqlBaglantisi bgl = new sqlBaglantisi();

        private void frmKulupler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int secilen = dataGridView1.SelectedCells[0].RowIndex;
            //txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            //txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            //kulüp eklenmesi işlemi
            SqlCommand ekle = new SqlCommand("insert into tblKulupler(kulupAd) values(@p1)",bgl.baglanti());
            ekle.Parameters.AddWithValue("@p1", txtAd.Text);
            ekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("kulüp listeye eklendi.","bilgi",MessageBoxButtons.OK);
            temizle();
            listele();
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("delete from tblKulupler where kulupID=@p1",bgl.baglanti());
            sil.Parameters.AddWithValue("@p1", txtID.Text);
            sil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("kulüp listeden silindi.", "bilgi", MessageBoxButtons.OK);
            temizle();
            listele();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update tblKulupler set kulupAd=@p1 where kulupID=@p2", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            guncelle.Parameters.AddWithValue("@p2",txtID.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("kulüp güncellendi.", "bilgi", MessageBoxButtons.OK);
            temizle();
            listele();

        }

        private void btnCikis_MouseHover(object sender, EventArgs e) //mause buton üstüne geldiği zaman yapılacak işlemler:
        {
            btnCikis.BackColor = Color.Red;
        }

        private void btnCikis_MouseLeave(object sender, EventArgs e)//mause'u buton üstünden çekince yapılacak işlemler: 
        {
            btnCikis.BackColor = Color.Transparent; // ana renge geri döner
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmOgretmen frmOgretmen = new frmOgretmen();
            frmOgretmen.Show();
            this.Hide();
        }
    }
}
