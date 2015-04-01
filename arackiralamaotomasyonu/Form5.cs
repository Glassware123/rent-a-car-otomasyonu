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
    public partial class Form5 : Form
    {
        OleDbConnection conn=new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.accdb");
        OleDbDataAdapter da;
        DataSet ds;
        public Form5()
        {
            InitializeComponent();
        }
        void clrTxt() {
            comboBox1.Text = txtID.Text = "";
        }
        void data()
        {

            da = new OleDbDataAdapter("Select * From contracts", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "contracts");
            dataGridView1.DataSource = ds.Tables["contracts"];
            conn.Close();
        }
        string name;
        string surname;
        private void Form5_Load(object sender, EventArgs e)
        {
            data();
            conn.Open();
            OleDbCommand autoList = new OleDbCommand("SELECT * FROM autos where plaka NOT IN (SELECT plaka FROM contracts)", conn);
            OleDbDataReader autoRead = autoList.ExecuteReader();
            while(autoRead.Read()){
                comboBox1.Items.Add(autoRead["plaka"]);
            }
            conn.Close();
            DateTime date = DateTime.Today;
            dateTimePicker1.MaxDate = date;
           
        }
        int recordCount = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty || txtID.Text == string.Empty || dateTimePicker1.Text == string.Empty)
            {
                MessageBox.Show("Alanlar boş geçilemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Eklemek İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    conn.Open();
                    OleDbCommand cont = new OleDbCommand("Select * from contracts where plaka='" + comboBox1.Text + "'", conn);
                    OleDbDataReader dataCont = cont.ExecuteReader();
                    while (dataCont.Read())
                    {
                        recordCount += 1;
                    }
                    if (recordCount > 0)
                    {
                        MessageBox.Show("Zaten bu araca ait sözleşme bulnuyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        OleDbCommand cmdID = new OleDbCommand("Select * From customers where TCKimlikNo='" + txtID.Text + "'", conn);
                        OleDbDataReader dr = cmdID.ExecuteReader();
                        while (dr.Read())
                        {
                            name = dr.GetString(2);
                            surname = dr.GetString(3);
                        }
                        OleDbCommand cmd = new OleDbCommand("Insert into contracts (plaka,musteri_tckno,musteri_ad,musteri_soyad,verilis_trh) values ('" + comboBox1.Text + "','" + txtID.Text + "','" + name + "','" + surname + "','" + dateTimePicker1.Text + "')", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        clrTxt();
                        MessageBox.Show("Sözleşme eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form5.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
