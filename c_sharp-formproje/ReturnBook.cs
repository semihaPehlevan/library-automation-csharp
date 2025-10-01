using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_sharp_formproje
{
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void txtSname_TextChanged(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        private void btnSearchStu_Click(object sender, EventArgs e)
        {
            
            cmd.Connection = con;
            // kitap adı çekilmediği için oraya geçirilmiyor
            cmd.CommandText = "select * from IRBook where std_no = '" + txtSearchS.Text + "' and book_return = 'Hayır'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Kayıtlı öğrenci ya da ödünç alınan kitap yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            txtSearchS.Clear();
        }

        Int64 bno;
        string bdate;
        Int64 rowid;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible = true;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                
                rowid = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bno = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                bdate = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtBName.Text = bno.ToString();
                txtIDate.Text = bdate;
            }
           
            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

            DateTime Rdate = dateTimePicker1.Value;

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "update IRBook set book_return_date= '" + Rdate.ToString("yyyy-MM-dd HH:mm:ss") + "', book_return = 'Evet' where std_no = '" + txtSearchS.Text + "' and id =" + rowid + "";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update NewBook set book_available = 'Evet' where bid = '" + bno + "'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("İade başarıyla gerçekleşti.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnBook_Load(this, null);

        }

        private void txtSearchS_TextChanged(object sender, EventArgs e)
        {
            if(txtSearchS.Text =="")
            {
                panel1.Visible = false;
                dataGridView1.DataSource = null;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchS.Clear();
        }

        private void brnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
