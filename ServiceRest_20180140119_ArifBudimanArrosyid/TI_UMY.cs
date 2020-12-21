using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.SqlClient;
using System.Text;

namespace ServiceRest_20180140119_ArifBudimanArrosyid
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            SqlConnection sqlcon = new SqlConnection("Data Source=ARIFBUDIMANAR;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=Arrosyid");
            string query = string.Format("Insert into dbo.Mahasiswa values ('{1}', '{0}', '{2}', '{3}')", mhs.nama, mhs.nim, mhs.prodi, mhs.angkatan);
            //NIM = '{0}'", nim)
            // string query = "Insert into dbo.Mahasiswa values ('"+mhs.nama+"', '"+mhs.nim+"', '"+mhs.prodi+"', '"+mhs.angkatan+"')";
            SqlCommand sqlcom = new SqlCommand(query, sqlcon); //yg dikirim ke sql

            try
            {
                sqlcon.Open(); //membuka connection sql
                Console.WriteLine(query);
                sqlcom.ExecuteNonQuery(); //mengeksekusi tuk memasukkan data
                sqlcon.Close();
                msg = "SUKSES";
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }

            return msg;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();

            SqlConnection con = new SqlConnection("Data Source=ARIFBUDIMANAR;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=Arrosyid");
            string query = "Select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa";
            SqlCommand com = new SqlCommand(query, con); //yg dikirim ke sql

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader(); //mendapatkan data yg telah dieksekusi, dari select hasil query ditaruh di reader

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0); //e itu array pertama // ini diambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }

            return mahas; //output
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection con = new SqlConnection("Data Source=ARIFBUDIMANAR;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=Arrosyid");
            string query = string.Format("Select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0); // 0 itu array pertama // ini di ambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;
        }
    }
}
