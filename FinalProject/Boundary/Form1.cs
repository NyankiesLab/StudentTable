using FinalProject.Controller;
using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SqlProcess sq = new SqlProcess();
            dataGridView1.DataSource = sq.selectTable().Tables["OgrenciBilgi"];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlProcess sq = new SqlProcess();
            StudentController sc = new StudentController();
            var entity = sc.fetchEntity();
            dataGridView1.DataSource = sq.selectTable().Tables["OgrenciBilgi"];
            label5.Text = sc.ogrenciOrtalamaHesapla(sq.selectTable("OrtalamaNotu" , "OgrenciBilgi")).ToString();
            label2.Text = Convert.ToString(sc.ogrenciSayiHesapla());
            label6.Text = "%" + Convert.ToString(sc.ogrenciDevamHesapla(sq.selectTable("DevamlilikDurumu", "OgrenciBilgi", "DevamlilikDurumu", "'G'")));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlProcess sq = new SqlProcess();
            StudentController sc = new StudentController();
            var girilenDeger = textBox1.Text;
            try
            {
                if (girilenDeger != "" && girilenDeger != null)
                {
                    dataGridView1.DataSource = sq.selectInnerTable("Ad,Soyad, b.BolumIsim ,KayitTarihi ,DevamlilikDurumu, OrtalamaNotu", "b.BolumIsim like '" + girilenDeger + "%'");
                    label2.Text = sc.ogrenciSayiHesapla((DataTable)dataGridView1.DataSource).ToString();
                    label5.Text = sc.ogrenciOrtalamaHesapla(fetchDataFromGrid()).ToString();
                    label6.Text = "%" + Convert.ToString(sc.ogrenciDevamHesapla(fetchDataFromGrid()));
                }
                else if (girilenDeger == "")
                {
                    dataGridView1.DataSource = sq.selectInnerTable("Ad,Soyad, b.BolumIsim ,KayitTarihi ,DevamlilikDurumu, OrtalamaNotu", "b.BolumId = o.BolumId");
                    label5.Text = sc.ogrenciOrtalamaHesapla(sq.selectTable("OrtalamaNotu", "OgrenciBilgi")).ToString();
                    label2.Text = Convert.ToString(sc.ogrenciSayiHesapla());
                    label6.Text = "%" + Convert.ToString(sc.ogrenciDevamHesapla(sq.selectTable("DevamlilikDurumu", "OgrenciBilgi", "DevamlilikDurumu", "'G'")));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz kelime ile alakalı bölüm yoktur.");
            }
        }
        public List<string> fetchDataFromGrid()
        {
            List<string> data = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    data.Add(cell.Value.ToString());
                }
            }
            return data;
        }

    }
}
