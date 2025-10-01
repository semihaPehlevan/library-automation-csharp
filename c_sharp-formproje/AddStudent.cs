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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu işlem kaydedilmemiş verilerin silinmesine sebep olur.", "Çıkmak istediğinize emin misiniz?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSName.Clear();
            txtlname.Clear();
            txtSNo.Clear();
            txttc.Clear();
            txtClass.Clear();
            txtContact.Clear();
            txtMail.Clear();
        }

        SqlConnection con = new SqlConnection("Data Source = DESKTOP-UPQILDP; Initial Catalog = PROJE; Integrated Security = True");

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSName.Text != "" & txtSNo.Text != "" & comboBox1.Text != "" & txtClass.Text != "" & txtContact.Text != "" & txtMail.Text != "")
            {
                string sname = txtSName.Text;
                string lname = txtlname.Text;
                string sno = txtSNo.Text;
                string stc = txttc.Text;
                string sdep = comboBox1.Text;
                string sclass = txtClass.Text;
                Int64 stel = Int64.Parse(txtContact.Text);
                string smail = txtMail.Text;

                con.Open();
                SqlCommand cmd = new SqlCommand("select id from Department where dep_name = '" + sdep + "'",con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmd.CommandText = "insert into NewStudent (stu_name, stu_lastname, stu_no, stu_tc, dep_no, stu_class,stu_tel,stu_mail) values ('" + sname + "', '"+lname+"','" + sno + "','"+stc+"' ,'" + ds.Tables[0].Rows[0][0].ToString() + "', '" + sclass + "', " + stel + ", '" + smail + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Veri kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tüm alanların doldurulmuş olması gerekmektedir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            con.Open();
            SqlCommand cmd = new SqlCommand("select dep_name  from Department", con);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    comboBox1.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
            con.Close();

        }


    }
}
