using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_sharp_formproje
{
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");

        private void ViewBook_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select bid,book_name, wt_name,publ_name,typ_name,book_date ,book_price, book_quan, book_available from NewBook, Publ,Writer,BType,BookType where NewBook.wrt_no=Writer.id and Newbook.publ_no=Publ.id and BType.typ_id=BookType.typ_no and NewBook.bid=BookType.book_no " ;

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);


            dataGridView1.DataSource = ds1.Tables[0];

            
            con.Open();
            cmd.Connection = con;

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

            cbWriter.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbWriter.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbPubl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbPubl.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        int bid;
        string bname;
        string bwt;
        string bpbl; 
        Int64 bquan;
        string btype;
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Hangi hucrenin secildiginin kontrol edilmesi gerekiyor.
            // e tıklanan hucrenin indexsini ifade eder
            // e.RowIndex -> e'nin satir indexsi
            // e.ColumnIndex -> e'nin sutun indexsi
            // if komutu tıklanılan hucrenin icinde veri olup olmadiginin kontrolunu yapiyor.

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                // bir hücreye tıklaması sonucu, tıklanan hücreye ait tüm satır verilerini temsil eder
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

               
                bid = Convert.ToInt32(row.Cells["bid"].Value);
                bwt = row.Cells["wt_name"].Value.ToString();
                bpbl = row.Cells["publ_name"].Value.ToString();
                btype = row.Cells["typ_name"].Value.ToString();
              
                panel4.Visible = true;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewBook where bid = " + bid;
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                

               bname = ds2.Tables[0].Rows[0]["book_name"].ToString();
               bquan = Int64.Parse(ds2.Tables[0].Rows[0]["book_quan"].ToString());

                txtBName.Text = bname;
                cbWriter.Text = bwt;
                cbPubl.Text = bpbl;
                dateTimePicker1.Value = Convert.ToDateTime(ds2.Tables[0].Rows[0]["book_date"]);
                cbType.Text = btype;
                txtPrice.Text = ds2.Tables[0].Rows[0]["book_price"].ToString();
                txtQuantity.Text = bquan.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if(txtBookName.Text != "")
            {
                // Arama alani bos degilse kitap ismine gore arama yap

                SqlCommand cmd = new SqlCommand("select bid,book_name, wt_name,publ_name,book_date, typ_name ,book_price, book_quan, book_available from NewBook, Publ,Writer, BookType, BType where NewBook.wrt_no=Writer.id and Newbook.publ_no=Publ.id and NewBook.bid=BookType.book_no and BookType.typ_no=BType.typ_id and book_name LIKE '" + txtBookName.Text + "%'",con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                // Arama alani bos ise tum verileri goster

                SqlCommand cmd = new SqlCommand("select bid,book_name, wt_name,publ_name,book_date, typ_name ,book_price, book_quan, book_available from NewBook, Publ,Writer, BookType, BType where NewBook.wrt_no=Writer.id and Newbook.publ_no=Publ.id and NewBook.bid=BookType.book_no and BookType.typ_no=BType.typ_id",con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Veri silinsin mi?", "Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // kitap silinince miktar güncellemesi
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                

                cmd.CommandText = "select bid from NewBook, Writer, Publ, BookType, BType where NewBook.wrt_no=Writer.id and NewBook.publ_no=Publ.id and NewBook.bid=BookType.book_no and BookType.typ_no=BType.typ_id and book_name = '"+bname+"' and wt_name = '" + bwt + "' and publ_name = '" + bpbl + "' and typ_name = '" + btype + "'";
                SqlDataAdapter dataA = new SqlDataAdapter(cmd);
                DataSet dataS = new DataSet();
                dataA.Fill(dataS);

                if (dataS.Tables[0].Rows.Count != 0)
                {
                    
                    cmd.CommandText = "update NewBook set book_quan = book_quan - 1 where book_name = '" + bname+ "' and wrt_no IN (select id from Writer where wt_name = '"+bwt+ "') and publ_no IN (select id from Publ where publ_name = '"+bpbl+ "') and bid IN (select book_no from BookType where typ_no IN (select typ_id from BType where typ_name = '"+btype+"'))";
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "delete from NewBook where bid = " + bid + "";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from BookType where book_no = " + bid + "";
                cmd.ExecuteNonQuery();
                
                con.Close();

            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Veri güncellensin mi?", "Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string bname = txtBName.Text;
                string bwriter = cbWriter.Text;
                string bpub = cbPubl.Text;
                string bdate = dateTimePicker1.Text;
                string btype = cbType.Text;
                Int64 bprice = Int64.Parse(txtPrice.Text);
                Int64 pquan = Int64.Parse(txtQuantity.Text);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

               
                // id verir
                cmd.CommandText = "select id from Writer where wt_name = '" + bwriter + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                cmd.CommandText = "select id from Publ where publ_name = '" + bpub + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                cmd.CommandText = "select typ_id from BType where typ_name = '" + btype + "'";
                SqlDataAdapter da3 = new SqlDataAdapter(cmd);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);

                cmd.CommandText = "select * from NewBook where book_name = '" + bname + "' and wrt_no = '" + ds1.Tables[0].Rows[0][0].ToString() + "'and publ_no = '" + ds2.Tables[0].Rows[0][0] + "'";
                SqlDataAdapter da4 = new SqlDataAdapter(cmd);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4);

                for (int i = 0; ds4.Tables[0].Rows.Count > i; i++)
                {
                    con.Open();


                    cmd.CommandText = "update NewBook set book_name ='" + bname + "', wrt_no ='" + ds1.Tables[0].Rows[0][0].ToString() + "', publ_no ='" + ds2.Tables[0].Rows[0][0].ToString() + "', book_date = '" + bdate + "', book_price =" + bprice + ", book_quan =" + pquan + " where bid =" + ds4.Tables[0].Rows[i][0].ToString() + "";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update BookType set typ_no = '" + ds3.Tables[0].Rows[0][0].ToString() + "' where book_no = " + bid + "";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

           
        }
    }
}
