using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    class SqlProcess
    {
        private string connectionString = "Data Source = NYANKIES; Initial Catalog = OgrenciDB; Integrated Security = True";
        public DataTable selectTable(string bolumIsmi)
        {
            //Problemsiz
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT Ad,Soyad From OgrenciBilgi as o inner join Bolum as b on b.BolumID = o.BolumID where b.BolumIsim = " + "'" + bolumIsmi + "'";
            var command = new SqlCommand(sqlString, connection);
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        public DataSet selectTable()
        {
            //Problemsiz
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT Ad, Soyad from OgrenciBilgi";
            var dataAdapter = new SqlDataAdapter(sqlString, connection);
            var dataSet = new DataSet();
            connection.Open();
            dataAdapter.Fill(dataSet, "OgrenciBilgi");
            connection.Close();
            return dataSet;
        }
        public DataTable selectInnerTable(string istenilen, string condition)
        {
            //Problemsiz
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT " + istenilen + " From OgrenciBilgi as o inner join Bolum as b on b.BolumID = o.BolumID where " + condition;
            var command = new SqlCommand(sqlString, connection);
            var dataTable = new DataTable();
            connection.Open();
            var dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public DataTable selectTable(string secilenSutun, string tabloIsmi)
        {
            //Problemsiz
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT " + secilenSutun + " from " + tabloIsmi;
            var dataAdapter = new SqlDataAdapter(sqlString, connection);
            var dataTable = new DataTable();
            connection.Open();
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public DataTable selectTable(string secilenSutun, string tabloIsmi, string arananSutun, string arananOzellik)
        {
            //Problemsiz    
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT " + secilenSutun + " from " + tabloIsmi + " where " + arananSutun + " = " + arananOzellik;
            var dataAdapter = new SqlDataAdapter(sqlString, connection);
            var dataTable = new DataTable();
            connection.Open();
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public DataTable selectLikeTable(string secilenSutun, string tabloIsmi, string arananSutun, string arananOzellik)
        {
            //Problemsiz    
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT " + secilenSutun + " from " + tabloIsmi + " where " + arananSutun + " like " + arananOzellik;
            var dataAdapter = new SqlDataAdapter(sqlString, connection);
            var dataTable = new DataTable();
            connection.Open();
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public DataTable selectTable(string secilenSutun, string tabloIsmi, string arananOzellik)
        {
            //Problemsiz    
            var connection = new SqlConnection(connectionString);
            string sqlString = "SELECT " + secilenSutun + " from " + tabloIsmi + " where " + arananOzellik;
            var dataAdapter = new SqlDataAdapter(sqlString, connection);
            var dataTable = new DataTable();
            connection.Open();
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public void updateTable(List<string> data)
        {
            //Problemsiz
            var connection = new SqlConnection(connectionString);
            string sqlString = "UPDATE dbo.OgrenciBilgi set Ad=@pAd ,Soyad=@pSoyad , BolumID = @pBolumID , KayitTarihi = @pKayitTarihi , DevamlilikDurumu = @pDevamlilikDurumu , OrtalamaNotu = @pOrtalamaNotu ,[Vize-1] = @pVize1,[Vize-2] = @pVize2, Final = @pFinal, Bütünleme = @pButunleme where OgrenciNo = @pOgrenciNo";
            var command = new SqlCommand(sqlString, connection);
            command.Parameters.AddWithValue("@pAd",data[0]);
            command.Parameters.AddWithValue("@pSoyad", data[1]);
            command.Parameters.AddWithValue("@pBolumID", data[2]);
            command.Parameters.AddWithValue("@pKayitTarihi", Convert.ToDateTime(data[3]));
            command.Parameters.AddWithValue("@pDevamlilikDurumu", Convert.ToChar(data[4]));
            command.Parameters.AddWithValue("@pOrtalamaNotu", data[5]);
            command.Parameters.AddWithValue("@pVize1", data[7]);
            command.Parameters.AddWithValue("@pVize2", data[8]);
            command.Parameters.AddWithValue("@pFinal", data[9]);
            command.Parameters.AddWithValue("@pButunleme", data[10]);
            command.Parameters.AddWithValue("@pOgrenciNo", data[6]);
            connection.Open();
            //try
            {
                command.ExecuteNonQuery();
            }
            /*catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Hatalı format girişi.");
            }*/
            connection.Close();
        }
        public void insertTable(List<string> data)
        {
            //Problemsiz
            //Ekleme
            var connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlString = "INSERT INTO OgrenciBilgi(Ad,Soyad,BolumID,KayitTarihi,OrtalamaNotu,DevamlilikDurumu ,[Vize-1],[Vize-2],Final,Bütünleme) VALUES (@pAd , @pSoyad , @pBolumID , @pKayitTarihi , @pOrtalamaNotu , @pDevamlilikDurumu, @pVize1, @pVize2, @pFinal, @pButunleme)";
            var command = new SqlCommand(sqlString, connection);
            SqlParameterCollection param = command.Parameters;
            param.AddWithValue("@pAd", data[0]);
            param.AddWithValue("@pSoyad", data[1]);
            param.AddWithValue("@pBolumID", data[2]);
            param.AddWithValue("@pKayitTarihi", Convert.ToDateTime(data[3]));
            param.AddWithValue("@pDevamlilikDurumu", data[4]);
            param.AddWithValue("@pOrtalamaNotu", data[5]);
            param.AddWithValue("@pVize1", data[7]);
            param.AddWithValue("@pVize2", data[8]);
            param.AddWithValue("@pFinal", data[9]);
            param.AddWithValue("@pButunleme", data[10]);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void deleteData(string deleteCondition)
        {
            var connection = new SqlConnection(connectionString);
            string sqlString = "DELETE from OgrenciBilgi where OgrenciNo = '" + deleteCondition +"'";
            SqlCommand cmd = new SqlCommand(sqlString, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
