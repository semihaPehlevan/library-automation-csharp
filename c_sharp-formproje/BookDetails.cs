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
    public partial class BookDetails : Form
    {
        public BookDetails()
        {
            InitializeComponent();
        }

        private void BookDetails_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // simdilik book_return_date guncelleme yaparken teslim durumuna bakilacak cunku teslim tarihi otomatik 30 gun sonrası yazılacak
            cmd.CommandText = "select * from IRBook where book_return = 'Hayır' ";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dgvI.DataSource = ds.Tables[0];

            cmd.CommandText = "select * from IRBook where book_return = 'Evet' ";
            SqlDataAdapter da2 = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);

            dgvR.DataSource = ds2.Tables[0];


        }
    }
}
