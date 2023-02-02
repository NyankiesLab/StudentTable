using FinalProject.Data;
using FinalProject.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Controller
{
    class StudentController
    {
        public DataTable matchBolumSqlResult(string a)
        {
            //3 farklı tablo birleştirmek için bu kod satırı gerekli eğer ihtiyaç duyarsak diye
            List<int> ogrID = new List<int>();
            SqlProcess sp = new SqlProcess();
            var dataFirst = sp.selectTable("BolumID", "OgrenciBilgi");
            var dataSecond = sp.selectLikeTable("BolumID", "Bolum", "BolumIsim", "like '" +a+"%'");
            int i = 0;
            foreach (DataRow data in dataFirst.Rows)
            {
                foreach (DataRow item in dataSecond.Rows)
                {
                    if (data.ItemArray.GetValue(0).ToString().Equals(item.ItemArray.GetValue(0).ToString()))
                    {
                        ogrID.Add(i);
                    }
                }

                i++;
            }
            for (int j = 0; j < ogrID.Count; j++)
            {
                var dataOgr = sp.selectTable("OgrenciID", "OgrenciBilgi","OgrenciNo", "'" + ogrID[j] + "'");
                ogrID[j] = Convert.ToInt32(dataOgr.Rows[0].ItemArray.GetValue(0).ToString());
            }
            string sql = "";
            for (int j = 0; j < ogrID.Count; j++)
            {
                sql += "OgrenciID = " + "'" + ogrID[j] + "'";
                if (j != ogrID.Count-1)
                {
                    sql += " or ";
                }
            }
            DataTable dataThird = sp.selectTable("*", "OgrenciBilgi", sql);
            return dataThird;
        }
        public double ogrenciOrtalamaHesapla(DataTable data)
        {
            double sum = 0;
            foreach (DataRow item in data.Rows)
            {
                sum += (Convert.ToDouble(item.ItemArray.GetValue(0).ToString()));
            }
            sum /= data.Rows.Count;
            return sum;
        }
        public double ogrenciOrtalamaHesapla(List<string> data)
        {
            double sum = 0;
            for (int i = 5; i < data.Count; i+=6)
            {
                sum += Convert.ToInt32(data[i]);
            }
            sum /= (data.Count/6);
            return sum;
        }
        public int ogrenciSayiHesapla()
        {
            SqlProcess sp = new SqlProcess();
            DataTable data = sp.selectTable("OgrenciNo", "OgrenciBilgi");
            return data.Rows.Count;
        }
        public int ogrenciSayiHesapla(DataTable data)
        {
            return data.Rows.Count;
        }
        public float ogrenciDevamHesapla(DataTable data)
        {
            double ogrenciSayisi = Convert.ToDouble(ogrenciSayiHesapla());
            var yuzde = ((100 / ogrenciSayisi) * data.Rows.Count);
            return (float)yuzde;
        }
        public float ogrenciDevamHesapla(List<string> data)
        {
            int count = 0;
            foreach (var item in data)
            {
                if (item == "G")
                {
                    count++;
                }
            }
            double ogrenciSayisi = Convert.ToDouble(data.Count / 6);
            var yuzde = ((100 / ogrenciSayisi) * count);
            return (float)yuzde;
        }
        public char changeEnum(string target)
        {
            if (target == "DerseGirer")
            {
                return 'G';
            }
            else if (target == "DevamdanMuaf")
            {
                return 'M';
            }
            else if(target == "Devamsız")
            {
                return 'D';
            }
            else
            {
                return ' ';
            }
        }
        public Enum.Devamsizlik changeEnumtoString(string target)
        {
            if (target == "G")
            {
                return Enum.Devamsizlik.DerseGirer;
            }
            else if (target == "M")
            {
                return Enum.Devamsizlik.DevamdanMuaf;
            }
            else
            {
                return Enum.Devamsizlik.Devamsız;
            }
        }
        public string ortalamaGridHesaplama(string vize1 = "0", string vize2 = "0" , string final = "0" , string but = "0")
        {
            var sum = (Convert.ToDouble(vize1) / 10 * 2) + (Convert.ToDouble(vize2) / 10 * 2) + (Convert.ToDouble(final) / 10 * 6);
            if ((sum < 50 || (Convert.ToInt32(final) < 50)) || (Convert.ToInt32(but) != 0))
            {
                sum = (Convert.ToDouble(vize1) / 10 * 2) + (Convert.ToDouble(vize2) / 10 * 2) + (Convert.ToDouble(but) / 10 * 6);
            }
            return sum.ToString();
        }
        public string nullExp(string target)
        {
            if (target == "")
            {
                target = "0";
            }
            return target;
        }
        public List<Ogrenci> fetchEntity()
        {
            SqlProcess sp = new SqlProcess();
            List<Ogrenci> ogrenci = new List<Ogrenci>();
            List<string> dataString = new List<string>();
            foreach (DataRow data in sp.selectTable("Ad , Soyad , [Vize-1] ,[Vize-2] ,Final ,Bütünleme", "OgrenciBilgi").Rows)
            {
                foreach (var item in data.ItemArray)
                {
                        dataString.Add(item.ToString());
                }
            }
            for (int i = 0; i < dataString.Count; i += 6)
            {
                ogrenci.Add(new Ogrenci(dataString[0], dataString[1], Convert.ToInt32(dataString[2]), Convert.ToInt32(dataString[3]), Convert.ToInt32(dataString[4]), Convert.ToInt32(dataString[5])));
            }
            return ogrenci;
        }
    }
}
