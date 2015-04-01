using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace arackiralamaotomasyonu
{
    public partial class Form4 : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.accdb");
        public Form4()
        {
            InitializeComponent();
        }
        public static string receivedAutoGetter;
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text != "")
            {
                receivedAutoGetter = comboBox1.Text;
                //DialogResult resultCont = MessageBox.Show("Teslim Almak İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
               // if (resultCont == DialogResult.Yes)
               // {
                    DateTime date = DateTime.Today;
                    conn.Open();
                    OleDbCommand getData = new OleDbCommand("Select * from contracts where plaka='" + comboBox1.Text + "'", conn);
                    OleDbDataReader readData = getData.ExecuteReader();
                    while (readData.Read())
                    {
                        OleDbCommand cmd = new OleDbCommand("insert into received_autos (plaka,mus_tckno,mus_ad,mus_soyad,teslim_tar,verilis_tar,odeme_durum) values ('" + comboBox1.Text + "','" + readData["musteri_tckno"] + "','" + readData["musteri_ad"] + "','" + readData["musteri_soyad"] + "','" + date + "','" + readData["verilis_trh"] + "','H')", conn);
                        cmd.ExecuteNonQuery();
                    }
                    OleDbCommand delData = new OleDbCommand("Delete from contracts where plaka='" + comboBox1.Text + "'", conn);
                    comboBox1.Items.Remove(comboBox1.Text);
                    comboBox1.Text = "";
                    delData.ExecuteNonQuery();
                    conn.Close();
                    DialogResult result = MessageBox.Show("Araç Teslim Alındı.Ödeme Raporu Almak İster misiniz?", "Araç Teslim Al", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Form4.ActiveForm.Dispose();
                        Form6 report = new Form6();
                        report.Show();
                    }
              //  }
            }
            else { MessageBox.Show("Lütfen Araç Seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from contracts",conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["plaka"]);
            }
            conn.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form4.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }
    }
}
