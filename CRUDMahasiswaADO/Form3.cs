using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class Form3 : Form
    {
        DAL dbLogic = new DAL();
        static string connectionString = "Data Source=JONNISHEREEE\\FADJAR;Initial Catalog=DBAkademikADO;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter da;
        DataTable dtMahasiswa;

        CrystalReport1 listMahasiswa = new CrystalReport1();

        string prodi { get; set; }
        DateTime tglmasuk { get; set; }

        public Form3(string Prodi, DateTime TglMasuk)
        {
            InitializeComponent();
            prodi = Prodi;
            tglmasuk = TglMasuk;

            try
            {
                DataTable dtMahasiswa = dbLogic.getRekapData(Prodi, TglMasuk);
                listMahasiswa.SetDataSource(dtMahasiswa);
                crystalReportViewer1.ReportSource = listMahasiswa;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }
    }
}