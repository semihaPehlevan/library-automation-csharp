using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using System.Xml.Linq;

namespace c_sharp_formproje
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
             if(txtUsername.Text !="" & txtPassword.Text != "")
            {
                string user = txtUsername.Text;
                string pass = txtPassword.Text;
                
                
                SqlCommand cmd = new SqlCommand("select * from loginTable where username = '" + user + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    con.Open();
                    cmd.CommandText = "insert into loginTable (username, pass) values ('" + user + "','" + pass + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kayıt işlemi başarıyla gerçekleşmiştir", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kulanıcı adı kullanılmakta. Lütfen farklı kullanıcı adı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtUsername.Text == "Kullanıcı Adı")
            {
                txtUsername.Clear();
            }
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text == "Şifre")
            {
                txtPassword.PasswordChar = '*';
                txtPassword.Clear();

            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
