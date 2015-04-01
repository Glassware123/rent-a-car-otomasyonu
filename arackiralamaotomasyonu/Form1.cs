using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arackiralamaotomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 addCustomer = new Form2();
            this.Hide();
            addCustomer.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 addAuto = new Form3();
            this.Hide();
            addAuto.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 receivedAuto = new Form4();
            this.Hide();
            receivedAuto.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 report = new Form6();
            this.Hide();
            report.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 contractForm = new Form5();
            this.Hide();
            contractForm.Show();
        }



        private void button7_Click_1(object sender, EventArgs e)
        {
            Form7 earnings = new Form7();
            this.Hide();
            earnings.Show();
        }
    }
}
