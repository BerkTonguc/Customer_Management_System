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

namespace Müsteri_Kayit_Sistemi
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
        }

        public void MusteriGetir()
        {
            conn = new SqlConnection("Server=.;Database=Musteri_DB;Trusted_Connection=True;");
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM musteri",conn);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();         
            txtMail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
           
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO musteri(ad,soyad,dogum_tarihi,telefon,mail_adresi) VALUES (@ad,@soyad,@dogum_tarihi,@telefon,@mail_adresi)";

            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@dogum_tarihi", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@mail_adresi", txtMail.Text);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            MusteriGetir();
            MessageBox.Show("Customer has been added to the system.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM musteri WHERE musteri_Id=@musteri_Id";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@musteri_Id", Convert.ToInt32(txtNo.Text));
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            MusteriGetir();
            MessageBox.Show("The customer has been deleted from the system!");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE musteri SET ad=@ad, soyad=@soyad, dogum_tarihi=@dogum_tarihi, telefon=@telefon, mail_adresi=@mail_adresi WHERE musteri_Id=@musteri_Id";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@musteri_Id", Convert.ToInt32(txtNo.Text));
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@dogum_tarihi", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@mail_adresi", txtMail.Text);
            if (conn.State==ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            MusteriGetir();
            MessageBox.Show("Customer information has been updated.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
