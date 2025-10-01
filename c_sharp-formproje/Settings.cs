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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelpassc.Visible = true;
        }
        public string Username = "";
        private void Settings_Load(object sender, EventArgs e)
        {
            panelpassc.Visible = false;
            lbuser.Text = Username;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtOldPass.Text != "" & txtNewPass.Text != "")
            {
                string user = lbuser.Text;
                string Opass = txtOldPass.Text;
                string Npass = txtNewPass.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from loginTable where username = '" + user + "' and pass = '" + Opass + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    cmd.CommandText = "update loginTable set pass = '" + Npass + "' where username = '" + user + "'";
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    MessageBox.Show("İşlem başarılı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNewPass.Clear();
                    txtOldPass.Clear();

                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
