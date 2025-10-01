using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_sharp_formproje
{
    public partial class ViewStudentInformation : Form
    {
        public ViewStudentInformation()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");

        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select stu_id,stu_name, stu_lastname,stu_no, stu_tc, dep_name, stu_class, stu_tel, stu_mail from NewStudent, Department where NewStudent.dep_no=Department.id";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];




            

            cbdep.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbdep.AutoCompleteSource = AutoCompleteSource.ListItems;

            con.Open();
            cmd.Connection = con;

            cmd.CommandText = "select dep_name from Department";
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    cbdep.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();

            con.Close();

        }

        int sid;
        string sdep;
        Int64 rowid;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value!=null)
            {
                sid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                sdep = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            panel4.Visible = true;
            
            

            cmd.CommandText = "select * from NewStudent where stu_id =" + sid + "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            txtSName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtSLast.Text = ds.Tables[0].Rows[0][2].ToString();
            txtSNo.Text = ds.Tables[0].Rows[0][3].ToString();
            txtTc.Text = ds.Tables[0].Rows[0][4].ToString();
            cbdep.Text = sdep;
            txtClass.Text = ds.Tables[0].Rows[0][6].ToString();
            txtContact.Text = ds.Tables[0].Rows[0][7].ToString();
            txtMail.Text = ds.Tables[0].Rows[0][8].ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text !="")
            {
                panel4.Visible = true;

                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select stu_id,stu_name, stu_lastname,stu_no, stu_tc, dep_name, stu_class, stu_tel, stu_mail from NewStudent, Department where NewStudent.dep_no=Department.id and stu_no LIKE '" + txtSearch.Text+ "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else 
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select stu_id,stu_name, stu_lastname,stu_no, stu_tc, dep_name, stu_class, stu_tel, stu_mail from NewStudent, Department where NewStudent.dep_no=Department.id";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

            }
           
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewStudentInformation_Load(this, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Veri güncellensin mi?", "Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string sname = txtSName.Text;
                string slast = txtSLast.Text;
                string sno = txtSNo.Text;
                string stc = txtTc.Text;
                string sdep = cbdep.Text;
                string sclass = txtClass.Text;
                Int64 scon = Int64.Parse(txtContact.Text);
                string smail = txtMail.Text;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select id from Department where dep_name = '" + sdep + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                cmd.CommandText = "update NewStudent set stu_name = '" + sname + "', stu_lastname = '"+slast+"' ,stu_no = '" + sno + "', stu_tc = '"+stc+"', dep_no = '" + ds1.Tables[0].Rows[0][0].ToString() + "', stu_class = '" + sclass + "', stu_tel = " + scon + ", stu_mail = '" + smail + "' where stu_id = " + rowid + "";
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                ViewStudentInformation_Load(this, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Veri silinsin mi?", "Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "delete from NewStudent where stu_id = " + rowid + "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
