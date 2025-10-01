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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand("select * from loginTable where username = '" + txtUsername.Text + "' and pass = '" + txtPassword.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           
           
            // DataSet.Tables[0] DataSet icindeki ilk tabloya erisimi saglar
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.Hide();
                Dashboard dh = new Dashboard();
                dh.Username = txtUsername.Text;
                dh.ShowDialog();
                
                
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register rgs = new Register();
            rgs.Show();

        }
    }
}
