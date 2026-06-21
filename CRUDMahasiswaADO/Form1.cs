using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class Form1 : Form
    {
        DAL dbLogic = new DAL();
        private BindingSource bindingSource = new BindingSource();
        private DataTable dtMahasiswa = new DataTable();
        static string connectionString = "Data Source=JONNISHEREEE\\FADJAR; Initial Catalog=DBAkademikADO; Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbLogic.GetConnectionString()))
                {
                    conn.Open();
                    MessageBox.Show("Koneksi Berhasil");
                }
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("SQL Error:" + ex.Message);
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("General Error:" + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] ConvertImageToBytes(PictureBox pb)
                {
                    if (pb.Image == null)
                    {
                        return null;
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pb.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }

                byte[] imgBytes = ConvertImageToBytes(fotoMhs);
                dbLogic.InsertMhs(txtnim.Text, txtnama.Text, txtAlamat.Text, cmbJK.Text, dtpTanggalLahir.Value.Date, txtKodeProdi.Text, imgBytes);
                MessageBox.Show("Data mahasiswa berhasil ditambahkan");

                ClearForm();
                LoadData();
            }
            catch (SqlException ex)
            {
                SimpanLog("Rollback Insert : " + ex.Message);
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                SimpanLog("General Error: " + ex.Message);
                MessageBox.Show("General Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] ConvertImageToBytes(PictureBox pb)
                {
                    if (pb.Image == null) return null;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pb.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }

                byte[] imgBytes = ConvertImageToBytes(fotoMhs);
                dbLogic.UpdateMhs(txtnim.Text, txtnama.Text, txtAlamat.Text, cmbJK.Text, dtpTanggalLahir.Value.Date, txtKodeProdi.Text, imgBytes);
                MessageBox.Show("Data mahasiswa berhasil diubah");

                ClearForm();
                btnLoad.PerformClick();
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("General Error: " + ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dg = MessageBox.Show(
                    "Yakin ingin menghapus data?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dg == DialogResult.Yes)
                {
                    dbLogic.DeleteMhs(txtnim.Text);
                    MessageBox.Show("Data mahasiswa berhasil dihapus");
                    ClearForm();
                    btnLoad.PerformClick();
                }
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("SQL Error:" + ex.Message);
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("General Error:" + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRow row = ((DataRowView)bindingSource[e.RowIndex]).Row;
                txtnim.Text = row[0].ToString();
                txtnama.Text = row[1].ToString();
                cmbJK.Text = row[2].ToString();
                dtpTanggalLahir.Value = Convert.ToDateTime(row[3]);
                txtAlamat.Text = row[4].ToString();
                txtKodeProdi.Text = row[7].ToString();

                if (row[5] != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])row[5];
                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        using (Image tempImage = Image.FromStream(ms))
                        {
                            fotoMhs.Image = new Bitmap(tempImage);
                        }
                        fotoMhs.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

                else
                {
                    fotoMhs.Image = null;
                }
                txtnim.Enabled = false;
            }
        }

        byte[] ConvertImageToBytes(PictureBox pb)
        {
            if (pb.Image == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                pb.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void Formm1_Load(object sender, EventArgs e)
        {
            bindingNavigator1.BindingSource = bindingSource;

            cmbJK.DataSource = new string[] { "L", "P" };
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadData();
        }

            private void LoadData()
            {
                try
                {
                    bindingSource.DataSource = dbLogic.GetMhs();
                    dataGridView1.DataSource = bindingSource;

                    DataGridViewImageColumn fotoColumn = (DataGridViewImageColumn)dataGridView1.Columns["Foto"];
                    fotoColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                    HitungTotal();

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        Console.WriteLine("Name: " + col.Name + " DataPropertyName: " + col.DataPropertyName);
                    }

                    dataGridView1.Enabled = true;
                    btnImportdb.Enabled = false;
                    btninsert.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                    btnLoad.Enabled = true;
                    btnResetData.Enabled = true;
                    btnTestInjection.Enabled = true;
                }
                catch (Exception ex)
                {
                    SimpanLog(ex.Message);
                    MessageBox.Show("Gagal load data: " + ex.Message);
                }
            }

        private void btnResetData_Click(object sender, EventArgs e)
        {
            try
            {
                dbLogic.resetData();
                MessageBox.Show("Data berhasil direset");
                LoadData();
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("SQL Error:" + ex.Message);
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("General Error:" + ex.Message);
            }
        }

        private void btnTestInjection_Click(object sender, EventArgs e)
        {
            try
            {
                dbLogic.testInjection(txtnim.Text);
                LoadData();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("safe"))
                {
                    SimpanLog(ex.Message);
                    MessageBox.Show("SQL Error: Unsafe UPDATE operation not allowed");
                }
                else
                {
                    SimpanLog(ex.Message);
                    MessageBox.Show("SQL Error:" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("General Error:" + ex.Message);
            }
        }

        private void HitungTotal()
        {
            try
            {
                int total = (dbLogic.CountMhs().Equals(DBNull.Value)) ? 0 : dbLogic.CountMhs();
                lblTotal.Text = "Total Mahasiswa:" + total;
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }


        private void SimpanLog(string pesan)
        {
            dbLogic.InsertLog(pesan);
        }

        private void btnRekap_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Show();
            this.Hide();
        }

        private void ClearForm()
        {
            txtnim.Enabled = true;
            txtnim.Clear();
            txtnama.Clear();
            cmbJK.SelectedIndex = -1;
            txtAlamat.Clear();
            txtKodeProdi.Clear();
            dtpTanggalLahir.Value = DateTime.Now;
            fotoMhs.Image = null;
            txtnim.Focus();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var tempImage = Image.FromFile(ofd.FileName))
                {
                    fotoMhs.Image = new Bitmap(tempImage);
                }
                fotoMhs.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });

                            DataTable dt = result.Tables[0];
                            dataGridView1.DataSource = dt;
                            dataGridView1.Enabled = false;
                            btnImportdb.Enabled = true;
                            btninsert.Enabled = false;
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;
                            btnSearch.Enabled = false;
                            btnLoad.Enabled = false;
                            btnResetData.Enabled = false;
                            btnTestInjection.Enabled = false;
                        }
                    }
                }
            }
        }

        private void btnImportdb_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data untuk diimport.");
                    return;
                }
                int sukses = 0;
                int gagalKosong = 0;
                int gagalTanggal = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string nim = row["NIM"].ToString().Trim();
                    string nama = row["Nama"].ToString().Trim();
                    string jk = row["JenisKelamin"].ToString().Trim();
                    string alamat = row["Alamat"].ToString().Trim();
                    string kodeProdi = row["NamaProdi"].ToString().Trim();

                    if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(nama))
                    {
                        gagalKosong++;
                        continue;
                    }

                    DateTime tglLahir;
                    if (!DateTime.TryParse(row["TanggalLahir"].ToString(), out tglLahir))
                    {
                        gagalTanggal++;
                        continue;
                    }

                    bool hasFotoPath = row.Table.Columns.Contains("FotoPath");
                    string rawFotoPath = hasFotoPath ? row["FotoPath"].ToString() : "(kolom tidak ditemukan)";
                    MessageBox.Show($"Has FotoPath column: {hasFotoPath}\nRaw value: '{rawFotoPath}'");

                    string fotoPath = row.Table.Columns.Contains("FotoPath")
                        ? row["FotoPath"].ToString().Trim()
                        : string.Empty;

                    byte[] ConvertImageFromPath(string path)
                    {
                        if (!File.Exists(path))
                        {
                            MessageBox.Show(
                                "File TIDAK ditemukan!\n" +
                                "Path: '" + path + "'\n" +
                                "Panjang string: " + path.Length + "\n" +
                                "Drive ada?: " + Directory.Exists(Path.GetDirectoryName(path))
                            );
                            return null;
                        }
                        MessageBox.Show("File DITEMUKAN: '" + path + "', size: " + new FileInfo(path).Length + " bytes");
                        return File.ReadAllBytes(path);
                    }

                    byte[] fotoBytes = ConvertImageFromPath(fotoPath);
                    dbLogic.InsertMhs(nim, nama, alamat, jk, tglLahir, kodeProdi, fotoBytes);
                    sukses++;
                }

                MessageBox.Show($"Sukses: {sukses}\nGagal (NIM/Nama kosong): {gagalKosong}\nGagal (Tanggal invalid): {gagalTanggal}");
                ClearForm();
                LoadData();
            }
            catch (SqlException ex)
            {
                SimpanLog("Rollback Insert :" + ex.Message);
                MessageBox.Show("SQL Error:" + ex.Message);
            }
            catch (Exception ex)
            {
                SimpanLog("General Error:" + ex.Message);
                MessageBox.Show("General Error:" + ex.Message);
            }
        }
    }
}
