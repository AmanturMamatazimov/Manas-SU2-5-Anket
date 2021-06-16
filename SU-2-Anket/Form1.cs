using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SU_2_Anket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        mahmut ma = new mahmut();
        private void Form1_Load(object sender, EventArgs e)
        {
           ma.sorulari_getir(comboBox1);
        }

        string soruno;//farklı metodlarda kullanabilmek için global tanımladım.

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            soruno = comboBox1.SelectedValue.ToString();//seçilen sorunun value aldık.
            ma.cevaplari_getir(soruno,listBox1);
            ma.grafik_ciz(soruno, chart1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string cevapno = listBox1.SelectedValue.ToString();
                ma.oy_ver(cevapno);
                string gelen = soruno;
                ma.grafik_ciz(soruno, chart1);
            }
            else
            {
                MessageBox.Show("Bir cevap seçmelisiniz");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Emin misiniz?", "ÇIKIŞ", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
