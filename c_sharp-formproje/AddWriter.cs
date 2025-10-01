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
    public partial class AddWriter : Form
    {
        public AddWriter()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtWName.Text != "" & txtWcv.Text != "")
            {
                string wname = txtWName.Text;
                string wcv = txtWcv.Text;
               

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "select * from Writer where wt_name = '" + wname + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    cmd.CommandText = "insert into Writer (wt_name, wt_cv) values ('" + wname + "','" + wcv + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtWName.Clear();
                    txtWcv.Clear();
                }
               

            }
            else
            {
                MessageBox.Show("Hiçbir alan boş bırakılmaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtWcv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtWName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
