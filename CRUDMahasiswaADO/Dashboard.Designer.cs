namespace CRUDMahasiswaADO
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cmbTipe = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnData = new System.Windows.Forms.Button();
            this.dtpTanggalMasuk = new System.Windows.Forms.DateTimePicker();
            this.chartProdi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartProdi)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tahun Masuk";
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.Lime;
            this.btnLoad.Location = new System.Drawing.Point(386, 95);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(83, 32);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Red;
            this.btnReset.Location = new System.Drawing.Point(475, 95);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(83, 32);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cmbTipe
            // 
            this.cmbTipe.FormattingEnabled = true;
            this.cmbTipe.Location = new System.Drawing.Point(761, 104);
            this.cmbTipe.Name = "cmbTipe";
            this.cmbTipe.Size = new System.Drawing.Size(121, 24);
            this.cmbTipe.TabIndex = 4;
            this.cmbTipe.SelectedIndexChanged += new System.EventHandler(this.cmbTipe_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(297, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "REKAP DATA MAHASISWA";
            // 
            // btnData
            // 
            this.btnData.Location = new System.Drawing.Point(715, 526);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(177, 32);
            this.btnData.TabIndex = 6;
            this.btnData.Text = "Data Mahasiswa";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // dtpTanggalMasuk
            // 
            this.dtpTanggalMasuk.Location = new System.Drawing.Point(147, 99);
            this.dtpTanggalMasuk.Name = "dtpTanggalMasuk";
            this.dtpTanggalMasuk.Size = new System.Drawing.Size(200, 22);
            this.dtpTanggalMasuk.TabIndex = 7;
            // 
            // chartProdi
            // 
            chartArea1.Name = "ChartArea1";
            this.chartProdi.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartProdi.Legends.Add(legend1);
            this.chartProdi.Location = new System.Drawing.Point(30, 153);
            this.chartProdi.Name = "chartProdi";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartProdi.Series.Add(series1);
            this.chartProdi.Size = new System.Drawing.Size(862, 367);
            this.chartProdi.TabIndex = 8;
            this.chartProdi.Text = "chart1";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 570);
            this.Controls.Add(this.chartProdi);
            this.Controls.Add(this.dtpTanggalMasuk);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTipe);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartProdi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cmbTipe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.DateTimePicker dtpTanggalMasuk;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProdi;
    }
}