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
using System.Text.RegularExpressions;
namespace arackiralamaotomasyonu
{
    public partial class Form2 : Form
    {
        OleDbConnection conn;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        public Form2()
        {
            InitializeComponent();
        }
        bool contMail;
          void data()
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.accdb");
            da = new OleDbDataAdapter("Select * From customers", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "customers");
            dataGridView1.DataSource = ds.Tables["customers"];
            conn.Close();
        }
        string updRef;
        void updKeeper(string c) {
            updRef = c;
        }
        void clearTxt() {
            txtID.Text =  txtCustomerName.Text = txtCustomerSurname.Text = cmb1.Text = dateTimePicker1.Text = cmbPoB.Text = txtPhoneNumber.Text = txtMail.Text = txtAddress.Text = txtLicenseNumber.Text = dateTimePicker2.Text = cmbLicencePlace.Text = "";
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtLicenseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            dateTimePicker2.MaxDate = DateTime.Today;
            data();
            cmb1.SelectedIndex = 0;
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from countries", conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbPoB.Items.Add(dr["sehir"]);
                cmbLicencePlace.Items.Add(dr["sehir"]);
            }
            conn.Close();
            clearTxt();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }
        int dataCont = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text == string.Empty || txtCustomerName.Text == string.Empty || txtCustomerSurname.Text == string.Empty || cmb1.Text == string.Empty || dateTimePicker1.Text == string.Empty || cmbPoB.Text == string.Empty || txtPhoneNumber.Text == string.Empty || txtMail.Text == string.Empty || txtAddress.Text == string.Empty || txtLicenseNumber.Text == string.Empty || dateTimePicker2.Text == string.Empty || cmbLicencePlace.Text == string.Empty)
            {
                MessageBox.Show("Alanlar boş geçilemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else{
                if (contMail==false)
                {
                    MessageBox.Show("E-Mail Geçersiz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new OleDbCommand();
                    conn.Open();
                    OleDbCommand cont = new OleDbCommand("select * from customers where TCKimlikNo='" + txtID.Text + "'", conn);
                    OleDbDataReader drCont = cont.ExecuteReader();
                    while (drCont.Read())
                    {
                        dataCont += 1;
                    }
                    if (dataCont > 0)
                    {
                        MessageBox.Show("Bu müşteri zaten kayıtlı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }else{
                        DialogResult result = MessageBox.Show("Eklemek İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "insert into customers (TCKimlikNo,ad,soyad,cinsiyet,dogumtar,dogumyer,tel,mail,adres,ehliyetno,ehliyettar,ehliyetyer) values ('" + txtID.Text + "','" + txtCustomerName.Text + "','" + txtCustomerSurname.Text + "','" + cmb1.Text + "','" + dateTimePicker1.Text + "','" + cmbPoB.Text + "','" + txtPhoneNumber.Text + "','" + txtMail.Text + "','" + txtAddress.Text + "','" + txtLicenseNumber.Text + "','" + dateTimePicker2.Text + "','" + cmbLicencePlace.Text + "')";
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            data();
                            clearTxt();
                            MessageBox.Show("Müşteri Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
                txtID.Text = ds.Tables["customers"].Rows[e.RowIndex]["TCKimlikNo"].ToString();
                txtCustomerName.Text = ds.Tables["customers"].Rows[e.RowIndex]["ad"].ToString();
                txtCustomerSurname.Text = ds.Tables["customers"].Rows[e.RowIndex]["soyad"].ToString();
                cmb1.Text = ds.Tables["customers"].Rows[e.RowIndex]["cinsiyet"].ToString();
                dateTimePicker1.Text = ds.Tables["customers"].Rows[e.RowIndex]["dogumtar"].ToString();
                cmbPoB.Text = ds.Tables["customers"].Rows[e.RowIndex]["dogumyer"].ToString();
                txtPhoneNumber.Text = ds.Tables["customers"].Rows[e.RowIndex]["tel"].ToString();
                txtMail.Text = ds.Tables["customers"].Rows[e.RowIndex]["mail"].ToString();
                txtAddress.Text = ds.Tables["customers"].Rows[e.RowIndex]["adres"].ToString();
                txtLicenseNumber.Text = ds.Tables["customers"].Rows[e.RowIndex]["ehliyetno"].ToString();
                dateTimePicker2.Text = ds.Tables["customers"].Rows[e.RowIndex]["ehliyettar"].ToString();
                cmbLicencePlace.Text = ds.Tables["customers"].Rows[e.RowIndex]["ehliyetyer"].ToString();
                string updRef = ds.Tables["customers"].Rows[e.RowIndex]["TCKimlikNo"].ToString();
                updKeeper(updRef);
        }

        public void button3_Click(object sender, EventArgs e)
        {
            if (txtID.Text == string.Empty || txtCustomerName.Text == string.Empty || txtCustomerSurname.Text == string.Empty || cmb1.Text == string.Empty || dateTimePicker1.Text == string.Empty || cmbPoB.Text == string.Empty || txtPhoneNumber.Text == string.Empty || txtMail.Text == string.Empty || txtAddress.Text == string.Empty || txtLicenseNumber.Text == string.Empty || dateTimePicker2.Text == string.Empty || cmbLicencePlace.Text == string.Empty)
            {
                MessageBox.Show("Alanlar boş geçilemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Gücellemek İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd = new OleDbCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "update customers set TCKimlikNo='" + txtID.Text + "',ad='" + txtCustomerName.Text + "',soyad='" + txtCustomerSurname.Text + "',cinsiyet='" + cmb1.Text + "',dogumtar='" + dateTimePicker1.Text + "',dogumyer='" + cmbPoB.Text + "',tel='" + txtPhoneNumber.Text + "',mail='" + txtMail.Text + "',adres='" + txtAddress.Text + "',ehliyetno='" + txtLicenseNumber.Text + "',ehliyettar='" + dateTimePicker2.Text + "',ehliyetyer='" + cmbLicencePlace.Text + "' Where TcKimlikNo='" + updRef + "'";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    clearTxt();
                    data();
                    MessageBox.Show("Müşteri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtID.Text == string.Empty || txtCustomerName.Text == string.Empty || txtCustomerSurname.Text == string.Empty || cmb1.Text == string.Empty || dateTimePicker1.Text == string.Empty || cmbPoB.Text == string.Empty || txtPhoneNumber.Text == string.Empty || txtMail.Text == string.Empty || txtAddress.Text == string.Empty || txtLicenseNumber.Text == string.Empty || dateTimePicker2.Text == string.Empty || cmbLicencePlace.Text == string.Empty)
            {
                MessageBox.Show("Alanlar boş geçilemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd = new OleDbCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from customers where TcKimlikNo='" + txtID.Text + "' ";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    data();
                    clearTxt();
                    MessageBox.Show("Müşteri Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2.ActiveForm.Dispose();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Today.AddDays(-(18*365+4));
            dateTimePicker1.MaxDate = date;
        }



        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void txtCustomerSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match cont = regex.Match(txtMail.Text);
            if (cont.Success)
            {
                mailValidation.Visible = true;
                mailValidation.ForeColor = Color.Green;
                mailValidation.Text = "E-Mail Geçerli";
                contMail = true;
            }
            else
            {
                mailValidation.Visible = true;
                mailValidation.ForeColor = Color.Red;
                mailValidation.Text = "E-Mail Geçersiz";
                contMail = false;
            }
        }
    }
}
