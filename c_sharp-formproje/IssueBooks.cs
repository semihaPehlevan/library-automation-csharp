using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace c_sharp_formproje
{
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");

       
        private void IssueBooks_Load(object sender, EventArgs e)
        {
            comboBoxBooks.Items.Clear();
            comboBoxBooks.Text = "";
            cbbookno.Items.Clear();
            cbbookno.Text = "";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = "select book_name from NewBook where book_available = 'Evet'"; // ödünç alınmamış kitaplar gelicek bunun için ödünç işlemi sırasında book_available Hayır yapmalı
            SqlDataReader sdr = cmd.ExecuteReader();

            comboBoxBooks.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxBooks.AutoCompleteSource = AutoCompleteSource.ListItems;

            while (sdr.Read())
            {
                for(int i=0;i<sdr.FieldCount;i++)
                {
                    comboBoxBooks.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
            con.Close();


        }
        int count;
        private void btnSearchStu_Click(object sender, EventArgs e)
        {
            if (txtSearchS.Text != "")
            {
              
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                string searchno = txtSearchS.Text;

                cmd.CommandText = "select * from NewStudent where stu_no = '" + searchno + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                

                //----------------------------------------------------------------------------------
                
                // Aynı öğrenciye kaç kitap ödünç verildiğini saymak için
                cmd.CommandText = "select count(std_no) from IRBook where std_no = '" + searchno + "'and book_return = 'Hayır'";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                count = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
                //----------------------------------------------------------------------------------
                if (ds.Tables[0].Rows.Count != 0) // datasette hiç satır olmaması demek girdiğimiz numara öğrenci yok demektir.
                {
                    txtSname.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtlastname.Text = ds.Tables[0].Rows[0][2].ToString();
                    
                    txtScon.Text = ds.Tables[0].Rows[0][7].ToString();
                    txtSmail.Text = ds.Tables[0].Rows[0][8].ToString();
                }
                else
                {
                    txtSname.Clear();
                    
                    txtScon.Clear();
                    txtSmail.Clear();
                    MessageBox.Show("Geçersiz Öğrenci Numarası", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            
            if (txtSname.Text != "" & comboBoxBooks.Text != "" & cbbookno.Text !="")
            {
                if (comboBoxBooks.SelectedIndex != -1 & count<3)
                {
                    

                    string sno = txtSearchS.Text;
                    string sname = txtSname.Text;
                    string slant = txtlastname.Text;
                    
                    Int64 scon = Int64.Parse(txtScon.Text);
                    string smail = txtSmail.Text;
                    Int64 bno = Int64.Parse(cbbookno.Text);
                    DateTime Idate = dateTimePicker1.Value;

                   
                   
                    // book_no book_return
                    

                    cmd.CommandText = "insert into IRBook (std_no, std_tel, std_mail, book_no, book_issue_date) values ('" + sno + "', " + scon + ", '" + smail + "', " + bno + ", '" + Idate.ToString("yyyy-MM-dd HH:mm:ss") + "')";             
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select id from IRBook where std_no = '" + sno + "' and book_no = " + bno + " and book_issue_date = '" + Idate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    SqlDataAdapter daad = new SqlDataAdapter(cmd);
                    DataSet dset = new DataSet();
                    daad.Fill(dset);

                    cmd.CommandText = "update IRBook set book_return_date = DATEADD(day, 30, book_issue_date) where id = '" + dset.Tables[0].Rows[0][0].ToString() + "'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update NewBook set book_available = 'Hayır' where bid = '" + bno + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Kitap ödünç verildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IssueBooks_Load(this, null);
                    
                }
                else
                {
                    MessageBox.Show("Kitap seçilmedi ya da Maksimum ödünç kitap sayısında", "Ödünç verme işlemi gerçekleşmedi.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else
            {
                MessageBox.Show("Öğrenci numarası aranmadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchS.Clear();
            txtSname.Clear();
            txtScon.Clear();
            txtSmail.Clear();
        }

        private void brnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bname = comboBoxBooks.Text;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = "select bid from NewBook where book_name = '" + bname + "'and book_available = 'Evet'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            cbbookno.Items.Clear();

            for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
            {
                cbbookno.Items.Add(ds.Tables[0].Rows[i][0]);
            }
            
            con.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
