using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SU_2_Anket
{
    class mahmut
    {
        SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=anket;integrated security=true");
        public void sorulari_getir(ComboBox cb)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from sorular order by soru asc", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb.DataSource = dt;
            //cb.DisplayMember = "soru";
            //cb.ValueMember = "soruno";
        }
        public void cevaplari_getir(string soruno,ListBox lb)
        {
            string sql = "select * from cevaplar where soruno="+soruno;
            SqlDataAdapter da = new SqlDataAdapter(sql,bag);
            da.SelectCommand.Parameters.AddWithValue("@soruno",soruno);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lb.DataSource = dt;
            //lb.DisplayMember = "cevap";//kod ile yapıldığından dolayı property dialog penceresinde ilgili propertylerrin boş olması gerekiyor
            //lb.ValueMember = "cevapno";
        }
        public void oy_ver(string cevapno)
        {
            SqlCommand komut = new SqlCommand("update cevaplar set oy=oy+1 where cevapno=@cevapno", bag);
            komut.Parameters.AddWithValue("@cevapno",cevapno);
            bag.Open();
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Oy kullanıldı");
        }
        public void grafik_ciz(string soruno,Chart c)
        {
            //soruya verilen toplam oy alias ile sanal tablo oluşturularak atandı.
            SqlDataAdapter da = new SqlDataAdapter("select SUM(oy) from cevaplar where soruno=@soruno", bag);
            da.SelectCommand.Parameters.AddWithValue("@soruno",soruno);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int toplam =Convert.ToInt32(dt.Rows[0][0]);

            //grafik çiziliyor
            //barların yükskeliklerini-değerlerini buldurup x ve y eksenlerine atama yapıyoruz:
            string sql = "select oy, cevap, (oy*100/" + toplam.ToString() + ") as yuzde from cevaplar where soruno=@soruno2";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, bag);
            da2.SelectCommand.Parameters.AddWithValue("@soruno2", soruno);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            c.DataSource = dt2;
            c.Series[0].XValueMember = "cevap";
            c.Series[0].YValueMembers = "yuzde";
            c.Series[0].Name = "Takımlar";
            //c.Series[0].ChartType = SeriesChartType.Pie;
            c.DataBind();
        }
    }
}
