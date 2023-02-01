using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entity
{
    class Ogrenci : IOgrenci
    {
        private string Ad;
        private string Soyad;
        private int Vize1;
        private int Vize2;
        private int Final;
        private int Bütünleme;
        private int OrtalamaNotu;

        public string getSetAd { get => Ad; set => Ad = value; }
        public string getSetSoyad { get => Soyad; set => Soyad = value; }
        public int getSetVize1 { get => Vize1; set => Vize1 = value; }
        public int getSetVize2 { get => Vize2; set => Vize2 = value; }
        public int getSetFinal { get => Final; set => Final = value; }
        public int getSetButunleme { get => Bütünleme; set => Bütünleme = value; }

        public Ogrenci(string ad, string soyad, int vize1 = 0, int vize2 = 0, int final = 0, int bütünleme = 0)
        {
            getSetAd = ad;
            getSetSoyad = soyad;
            getSetVize1 = sinavKontrolEt(vize1);
            getSetVize2 = sinavKontrolEt(vize2);
            getSetFinal = sinavKontrolEt(final);
            getSetButunleme = sinavKontrolEt(bütünleme);
            ortalamaHesapla();
        }
        public override int ortalamaHesapla()
        {
            OrtalamaNotu = (getSetVize1 / 10 * 2) + (getSetVize2 / 10 * 2) + (getSetFinal / 10 * 6);
            if (OrtalamaNotu < 50)
            {
                OrtalamaNotu = (getSetVize1 / 10 * 2) + (getSetVize2 / 10 * 2) + (getSetButunleme / 10 * 6);
            }
            return OrtalamaNotu;
        }

        public override int sinavKontrolEt(int sinav)
        {
            if (sinav >= 0 && sinav <= 100)
            {
                return sinav;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Girdiğiniz değer doğru değildir.");
                return 0;
            }
        }
    }
}
