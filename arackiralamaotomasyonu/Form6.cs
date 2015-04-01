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
    public partial class Form6 : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.accdb");
        public Form6()
        {
            InitializeComponent();
        }
        public static int day;
        void dayCount() {
            conn.Open();
            OleDbCommand cmd2 = new OleDbCommand("Select * from received_autos where plaka='" + comboBox1.Text + "' and odeme_durum='H'", conn);
            OleDbDataReader read = cmd2.ExecuteReader();
            while(read.Read()){
            DateTime x = read.GetDateTime(6);
            DateTime y = read.GetDateTime(5);
            TimeSpan span = y - x;
            day = span.Days;
            }
            conn.Close();
        }
        void priceCalc() {
            dayCount();
            conn.Open();
            OleDbCommand cmd3 = new OleDbCommand("Select * from autos where plaka='" + comboBox1.Text + "'", conn);
            OleDbDataReader dr = cmd3.ExecuteReader();
            while (dr.Read())
            {
                int price = dr.GetInt32(6);
                int totalPrice = price * day;
                label3.Text = totalPrice.ToString();
            }
            conn.Close();
        }
        int dataCount = 0;
        private void Form6_Load(object sender, EventArgs e)
        {
            if (Form4.receivedAutoGetter != null)
            {
                comboBox1.Text = Form4.receivedAutoGetter;
                priceCalc();
                label2.Visible = true;
            }
            else {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from received_autos where odeme_durum='H'", conn);
                OleDbDataReader data = cmd.ExecuteReader();
                while(data.Read()){
                    dataCount += 1;
                    comboBox1.Items.Add(data["plaka"]);
                }
                if (dataCount > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ödeme Almak İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string plate = comboBox1.Text;
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("Update received_autos set odeme_durum='E' Where plaka='" + plate + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                comboBox1.Items.Remove(plate);
                comboBox1.Text = "";
                comboBox1.SelectedIndex = -1;
                MessageBox.Show("Ödeme Alındı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label2.Visible = true;
            priceCalc();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form6.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

    }
}
