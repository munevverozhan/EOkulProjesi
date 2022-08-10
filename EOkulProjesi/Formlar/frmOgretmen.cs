using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOkulProjesi.Formlar
{
    public partial class frmOgretmen : Form
    {
        public frmOgretmen()
        {
            InitializeComponent();
        }
        public string ad;
        private void frmOgretmen_Load(object sender, EventArgs e)
        {
            this.Text = ad;
        }

        private void btnKulup_Click(object sender, EventArgs e)
        {
            frmKulupler kulupler = new frmKulupler();
            kulupler.Show();
            this.Hide();
        }

        private void btnDers_Click(object sender, EventArgs e)
        {
            frmDersler ders = new frmDersler();
            ders.Show();
            this.Hide();
        }

        private void btnOgrenci_Click(object sender, EventArgs e)
        {
            frmOgrenci frmOgrenci = new frmOgrenci();
            frmOgrenci.Show();
            this.Hide();
        }

        private void btnSinav_Click(object sender, EventArgs e)
        {
            frmSinavNotlar frmSinavNotlar = new frmSinavNotlar();
            frmSinavNotlar.Show();
            this.Hide();
        }

        private void btnOgretmen_Click(object sender, EventArgs e)
        {
            frmOgretmenListele ogretmenListele = new frmOgretmenListele();
            ogretmenListele.Show();
            this.Hide();
        }
    }
}
