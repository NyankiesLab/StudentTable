using FinalProject.Controller;
using FinalProject.Data;
using FinalProject.Enum;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        SqlProcess sq = new SqlProcess();
        StudentController sc = new StudentController();

        private void button3_Click(object sender, EventArgs e)
        {

            var girilenDeger = textBox1.Text;
            try
            {
                if (girilenDeger != "" && girilenDeger != null)
                {
                    dataGridView1.DataSource = sq.selectInnerTable("Ad,Soyad, b.BolumIsim ,KayitTarihi ,DevamlilikDurumu,OrtalamaNotu,[Vize-1], [Vize-2],Final,Bütünleme", "b.BolumIsim like '" + girilenDeger + "%'");
                }
                else if (girilenDeger == "")
                {
                    dataGridView1.DataSource = sq.selectInnerTable("Ad,Soyad, b.BolumIsim ,KayitTarihi ,DevamlilikDurumu, OrtalamaNotu,[Vize-1], [Vize-2],Final,Bütünleme", "b.BolumId = o.BolumId");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz kelime ile alakalı bölüm yoktur.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlProcess sq = new SqlProcess();
            dataGridView1.DataSource = sq.selectInnerTable("Ad,Soyad, b.BolumIsim ,KayitTarihi ,DevamlilikDurumu, OrtalamaNotu,[Vize-1], [Vize-2],Final,Bütünleme", "b.BolumId = o.BolumId");
            comboBox1.Items.Add(Devamsizlik.DerseGirer);
            comboBox1.Items.Add(Devamsizlik.DevamdanMuaf);
            comboBox1.Items.Add(Devamsizlik.Devamsız);
            foreach (DataRow item in sq.selectTable("BolumIsim","Bolum").Rows)
            {
                comboBox2.Items.Add(item.ItemArray.GetValue(0).ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            try
            {
                data.Add(textBox2.Text);
                data.Add(textBox3.Text);
                data.Add(sq.selectInnerTable("distinct b.BolumID", "b.BolumIsim = '" + comboBox2.Text + "'").Rows[0].ItemArray.GetValue(0).ToString());
                data.Add(dateTimePicker1.Text);
                data.Add(sc.changeEnum(comboBox1.Text).ToString());
                data.Add(sc.ortalamaGridHesaplama(textBox4.Text,textBox5.Text, textBox6.Text , textBox8.Text));
                data.Add(sq.selectTable("*", "OgrenciBilgi").Rows.Count.ToString());
                data.Add(textBox4.Text);
                data.Add(textBox5.Text);
                data.Add(textBox6.Text);
                data.Add(textBox8.Text);
                sq.insertTable(data);
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.Text = "";
                textBox7.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox8.Text = "";
                MessageBox.Show("Kayıt Başarıyla Eklendi.");
                string temp = textBox1.Text;
                textBox1.Text = "tas";
                textBox1.Text = temp;
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt Formatı Hatalı");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
                textBox7.Text = "";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = sc.changeEnumtoString(dataGridView1.SelectedRows[0].Cells[4].Value.ToString()).ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            try
            {
                data.Add(textBox2.Text);
                data.Add(textBox3.Text);
                data.Add(sq.selectInnerTable("distinct b.BolumID", "b.BolumIsim = '" + comboBox2.Text + "'").Rows[0].ItemArray.GetValue(0).ToString());
                data.Add(dateTimePicker1.Text);
                data.Add(sc.changeEnum(comboBox1.Text).ToString());
                data.Add(sc.ortalamaGridHesaplama(textBox4.Text, textBox5.Text, textBox6.Text, textBox8.Text));
                data.Add(sq.selectTable("OgrenciNo", "OgrenciBilgi", "Ad = '" + textBox2.Text + "'").Rows[0].ItemArray.GetValue(0).ToString());
                data.Add(textBox4.Text);
                data.Add(textBox5.Text);
                data.Add(textBox6.Text);
                data.Add(textBox8.Text);
                sq.updateTable(data);
                MessageBox.Show("Güncelleme işlemi başarılı.");
                string temp = textBox1.Text;
                textBox1.Text = "tas";
                textBox1.Text = temp;
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.Text = "";
                textBox7.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox8.Text = "";
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Güncellemek için kayıt seçiniz.");
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.Text = "";
                textBox7.Text = "";
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                sq.deleteData(sq.selectTable("OgrenciNo", "OgrenciBilgi", "Ad = '" + textBox2.Text + "'").Rows[0].ItemArray.GetValue(0).ToString());
                MessageBox.Show("Kayıt başarıyla silindi.");
                string temp = textBox1.Text;
                textBox1.Text = "tas";
                textBox1.Text = temp;
            }
            catch (Exception)
            {

                MessageBox.Show("Silmek için kayıt seçiniz.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void textBoxs_TextChanged(object sender, EventArgs e)
        {
            sc.ortalamaGridHesaplama(textBox4.Text, textBox5.Text, textBox6.Text, textBox8.Text);
        }
    }
}
