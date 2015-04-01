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
    public partial class Form3 : Form
    {
         OleDbConnection conn;
         OleDbDataAdapter da;
         OleDbCommand cmd;
         DataSet ds;
        public Form3()
        {
            InitializeComponent();
        }
        void data()
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.accdb");
            da = new OleDbDataAdapter("Select * From autos", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "autos");
            dataGridView1.DataSource = ds.Tables["autos"];
            conn.Close();
        }
        void clrTxt() {
            txtPlate.Text = txtBrand.Text = txtType.Text = txtModel.Text = txtColor.Text = txtUcret.Text="";
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           data();
        }
        int recordCount;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPlate.Text == string.Empty || txtBrand.Text == string.Empty || txtType.Text == string.Empty || txtModel.Text == string.Empty || txtColor.Text == string.Empty || txtUcret.Text == string.Empty)
            {
                MessageBox.Show("Alanlar boş geçilemez","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                cmd = new OleDbCommand();
                conn.Open();
                OleDbCommand cont = new OleDbCommand("Select * from autos where plaka='" + txtPlate.Text + "'",conn);
                OleDbDataReader dr = cont.ExecuteReader();
                recordCount = 0;
                while (dr.Read()) {
                    recordCount += 1;
                }

                if (recordCount > 0)
                {
                    MessageBox.Show("Bu araç zaten kayıtlı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clrTxt();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Eklemek İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "insert into autos (plaka,marka,tip,model,renk,gunluk_ucret) values ('" + txtPlate.Text + "','" + txtBrand.Text + "','" + txtType.Text + "','" + txtModel.Text + "','" + txtColor.Text + "','" + txtUcret.Text + "')";
                        cmd.ExecuteNonQuery();
                        clrTxt();
                        MessageBox.Show("Araç Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
                data();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtPlate.Text = ds.Tables["autos"].Rows[e.RowIndex]["plaka"].ToString();
            txtBrand.Text = ds.Tables["autos"].Rows[e.RowIndex]["marka"].ToString();
            txtType.Text = ds.Tables["autos"].Rows[e.RowIndex]["tip"].ToString();
            txtModel.Text = ds.Tables["autos"].Rows[e.RowIndex]["model"].ToString();
            txtColor.Text = ds.Tables["autos"].Rows[e.RowIndex]["renk"].ToString();
            txtUcret.Text =  ds.Tables["autos"].Rows[e.RowIndex]["gunluk_ucret"].ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {

                cmd = new OleDbCommand();

                conn.Open();

                cmd.Connection = conn;

                cmd.CommandText = "Select * from autos";

                da.SelectCommand = cmd;

                da.Fill(ds.Tables["autos"]);

            }

            da.SelectCommand.CommandText = " Select * From autos" + " where(plaka like '%" + txtSearch.Text + "%' )";

            ds.Tables["autos"].Clear();

            da.Fill(ds.Tables["autos"]);

            conn.Close();
 
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Form3.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form3.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }
    }
}
