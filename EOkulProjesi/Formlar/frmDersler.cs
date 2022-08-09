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
    public partial class frmDersler : Form
    {
        public frmDersler()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
       
        void listele()
        {
            dataGridView1.DataSource = ds.DersListesi();

        }
        void temizle()
        {
            txtAd.Text = "";
            txtID.Text = "";
        }
        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmOgretmen frmOgretmen = new frmOgretmen();
            frmOgretmen.Show();
            this.Hide();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        DataSet1TableAdapters.tblDerslerTableAdapter ds = new DataSet1TableAdapters.tblDerslerTableAdapter();

        private void frmDersler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
           ds.dersEkle(txtAd.Text);
           MessageBox.Show("Ders ekleme işlemi yapılmıştır.");
            listele();
            temizle();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            ds.dersSil(byte.Parse(txtID.Text));
            MessageBox.Show("ders listeden kaldırıldı");
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            ds.UpdateQuery(txtAd.Text, byte.Parse(txtID.Text));
            MessageBox.Show("ders güncellendi");
            listele();
            temizle();

        }
    }
}
