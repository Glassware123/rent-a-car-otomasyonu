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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.accdb");
        int dailyPrice = 0;
        void findDailyPrice(string x)
        {
            OleDbCommand dailyPriceCmd = new OleDbCommand("Select * from autos where plaka ='" + x + "'", conn);
            OleDbDataReader drDailyPrice = dailyPriceCmd.ExecuteReader();
            while(drDailyPrice.Read()){
            dailyPrice = drDailyPrice.GetInt32(6);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            int totalPrice = 0;
            string sql = "Select * from received_autos where odeme_durum='E' and teslim_tar between @D1 and @D2 ";
            OleDbCommand cmd = new OleDbCommand(sql,conn);
            cmd.Parameters.AddWithValue("@D1", DateTime.Parse(dateTimePicker1.Text));
            cmd.Parameters.AddWithValue("@D2", DateTime.Parse(dateTimePicker2.Text));
            OleDbDataReader dr = cmd.ExecuteReader();
            while(dr.Read()){
                string plate = dr.GetString(1);
                DateTime x = dr.GetDateTime(6);
                DateTime y = dr.GetDateTime(5);
                TimeSpan span = y - x;
                findDailyPrice(plate);
                totalPrice = totalPrice + (dailyPrice*span.Days);
            }
            conn.Close();
            label4.Text = totalPrice.ToString();
            label3.Visible = true;
            label4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form7.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker2.MaxDate = DateTime.Today;
        }
    }
}
