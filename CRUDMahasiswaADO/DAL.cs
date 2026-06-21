using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class DAL : Form
    {
        public string GetConnectionString()
        {
            return connectionString;
        }

        static string connectionString = $"Data Source={GetLocalIPAddress()},1433;Initial Catalog=DBAkademikADO;Integrated Security=True;";

        public static string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting local IP address: " + ex.Message);
            }
            return localIP;
        }

        SqlConnection conn = new SqlConnection(connectionString);

        SqlDataAdapter da;
        DataTable dtProdi;
        DataTable dtMahasiswa;

        public int CountMhs()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_CountMahasiswa", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter OutputParam = new SqlParameter("@Total", SqlDbType.Int);
            OutputParam.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OutputParam);
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(OutputParam.Value);
        }

        public DataTable GetMhs()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_GetMahasiswa", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);

            dtMahasiswa = new DataTable();
            da.Fill(dtMahasiswa);

            return dtMahasiswa;
        }

        public void InsertMhs(string nim, string nama, string alamat, string jeniskelamin, DateTime TanggalLahir, string kodeprodi, byte[] foto)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_InsertMahasiswa", conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pNIM", nim);
                cmd.Parameters.AddWithValue("@pNama", nama);
                cmd.Parameters.AddWithValue("@pAlamat", alamat);
                cmd.Parameters.AddWithValue("@pTanggalLahir", TanggalLahir);
                cmd.Parameters.AddWithValue("@pJenisKelamin", jeniskelamin);
                cmd.Parameters.AddWithValue("@pKodeProdi", kodeprodi);

                SqlParameter fotoParam = new SqlParameter("@pFoto", SqlDbType.VarBinary, -1); 
                fotoParam.Value = (foto == null || foto.Length == 0) ? (object)DBNull.Value : foto;
                cmd.Parameters.Add(fotoParam);

                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateMhs(string nim, string nama, string alamat, string jeniskelamin, DateTime TanggalLahir, string kodeprodi, byte[] foto)
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand command = new SqlCommand("sp_UpdateMahasiswa", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@pNIM", nim);
            command.Parameters.AddWithValue("@pNama", nama);
            command.Parameters.AddWithValue("@pAlamat", alamat);
            command.Parameters.AddWithValue("@pJenisKelamin", jeniskelamin);
            command.Parameters.AddWithValue("@pTanggalLahir", TanggalLahir);
            command.Parameters.AddWithValue("@pKodeProdi", kodeprodi);

 
            SqlParameter fotoParam = new SqlParameter("@pFoto", SqlDbType.VarBinary, -1);
            fotoParam.Value = (foto == null || foto.Length == 0) ? (object)DBNull.Value : foto;
            command.Parameters.Add(fotoParam);

            command.ExecuteNonQuery();
        }

        public void DeleteMhs(string nim)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand command = new SqlCommand("sp_DeleteMahasiswa", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NIM", nim);

            command.ExecuteNonQuery();
        }

        public void resetData()
        {
     if (conn.State == ConnectionState.Closed)
        conn.Open();

    string deleteQuery = "DELETE FROM mahasiswa";
    SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn);
    cmdDelete.ExecuteNonQuery();

    string queryInsert = @"
        INSERT INTO Mahasiswa (NIM, Nama, JenisKelamin, TanggalLahir, Alamat, KodeProdi, TanggalDaftar, Foto)
        SELECT NIM, Nama, JenisKelamin, TanggalLahir, Alamat, KodeProdi, TanggalDaftar, Foto
        FROM Mahasiswa_Backup;";
    SqlCommand cmdInsert = new SqlCommand(queryInsert, conn);
    cmdInsert.ExecuteNonQuery();
        }

        public void testInjection(string nim)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "UPDATE Mahasiswa SET Nama   = 'HACKED' WHERE NIM = " + nim;
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        public DataTable GetMhsByNIM(string nim)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_GetMahasiswaByNIM", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pNIM", nim);
            da = new SqlDataAdapter(cmd);

            dtMahasiswa = new DataTable();
            da.Fill(dtMahasiswa);

            return dtMahasiswa;
        }

        public void InsertLog(string message)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_LogMessage", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("psn", message);
            cmd.ExecuteNonQuery();
        }

        public DataTable getProdi()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select namaprodi from prodi", conn);
            cmd.CommandType = CommandType.Text;
            dtProdi = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtProdi);

            return dtProdi;
        }

        public DataTable getRekapData(string prodi, DateTime TanggalMasuk)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_Report", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inProdi", prodi);
            cmd.Parameters.AddWithValue("@inTglMsuk", TanggalMasuk.Year.ToString());

            da = new SqlDataAdapter(cmd);

            dtMahasiswa = new DataTable();
            da.Fill(dtMahasiswa);
            return dtMahasiswa;
        }

        public DataTable GetAllDataChart()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_DashBoard", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dtMahasiswa = new DataTable();
            da.Fill(dtMahasiswa);
            return dtMahasiswa;
        }

        public DataTable GetDataChartByTahun(DateTime thMasuk)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_DashBoardByTahun", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inTglMsuk", thMasuk.Year.ToString());
            da = new SqlDataAdapter(cmd);
            dtMahasiswa = new DataTable();
            da.Fill(dtMahasiswa);
            return dtMahasiswa;
        }
    }
}
