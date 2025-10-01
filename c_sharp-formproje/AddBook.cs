using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;

namespace c_sharp_formproje
{
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookName.Text!="" & cbWriter.Text != "" & cbPubl.Text != "" & cbType.Text !="" & txtPrice.Text != "" & txtQuantity.Text != "")
            {
                // Aynı kitaptan var ama stok yenilenicek ve eklme yapılıcaksa
                string bname = txtBookName.Text;
                string bwriter = cbWriter.Text;
                string bPublication = cbPubl.Text;
                string bdate = dateTimePicker1.Text;
                string btype = cbType.Text;
                Int64 bprice = Int64.Parse(txtPrice.Text);
                Int64 bquan = Int64.Parse(txtQuantity.Text);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "select id from Writer where wt_name = '" + bwriter + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmd.CommandText = "select id from Publ where publ_name = '" + bPublication + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                cmd.CommandText = "select typ_id from BType where typ_name = '" + btype + "'";
                SqlDataAdapter da3 = new SqlDataAdapter(cmd);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);

                for (int i = 0; bquan > i; i++)
                {

                    cmd.CommandText = "insert into NewBook (book_name,wrt_no,publ_no,book_date,book_price,book_quan) values ('" + bname + "','" + ds.Tables[0].Rows[0][0].ToString() + "','" + ds2.Tables[0].Rows[0][0].ToString() + "','" + bdate + "'," + bprice + "," + bquan + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select bid from Newbook order by bid desc";
                    SqlDataAdapter da4 = new SqlDataAdapter(cmd);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4);
                    cmd.CommandText = "insert into BookType (book_no, typ_no) values ('" + ds4.Tables[0].Rows[0][0].ToString() + "', '" + ds3.Tables[0].Rows[0][0].ToString() + "')";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBookName.Clear();
                    txtPrice.Clear();
                    txtQuantity.Clear();
                
            }
            else
            {
                MessageBox.Show("Hiçbir alan boş bırakılmaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bu işlem kaydedilmemiş verilerin silinmesine sebep olur.", "Çıkmak istediğinize emin misiniz?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void AddBooks_Load(object sender, EventArgs e)
        {
            cbWriter.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbWriter.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbPubl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbPubl.AutoCompleteSource = AutoCompleteSource.ListItems;
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
    
            cmd.CommandText = "select wt_name from Writer";
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    cbWriter.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();

            cmd.CommandText = "select publ_name from Publ";
            SqlDataReader sdr2 = cmd.ExecuteReader();

            while (sdr2.Read())
            {
                for (int i = 0; i < sdr2.FieldCount; i++)
                {
                    cbPubl.Items.Add(sdr2.GetString(i));
                }
            }
            sdr2.Close();

            cmd.CommandText = "select typ_name from BType";
            SqlDataReader sdr3 = cmd.ExecuteReader();

            while (sdr3.Read())
            {
                for (int i = 0; i < sdr3.FieldCount; i++)
                {
                    cbType.Items.Add(sdr3.GetString(i));
                }
            }
            sdr3.Close();



            con.Close();
        }

        private void btnWriterAdd_Click(object sender, EventArgs e)
        {
            AddWriter adw = new AddWriter();
            adw.Show();
        }

        private void btnPublAdd_Click(object sender, EventArgs e)
        {
            if (cbPubl.Text != "")
            {
                string pname = cbPubl.Text;


                SqlCommand cmd = new SqlCommand("select * from Publ where publ_name = '" + pname + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    con.Open();
                    cmd.CommandText = "insert into Publ (publ_name) values ('" + pname + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kayıt işlemi başarıyla gerçekleşmiştir", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Yayınevi zaten kayıtlı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
