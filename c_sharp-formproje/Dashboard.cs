using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace c_sharp_formproje
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void çkişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                Application.Exit();
            }

            
        }


        private void button5_Click(object sender, EventArgs e)
        {
            AddBooks abs = new AddBooks();
            abs.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewBook vb = new ViewBook();
            vb.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddStudent ast = new AddStudent();
            ast.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewStudentInformation vsi = new ViewStudentInformation();
            vsi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IssueBooks isb = new IssueBooks();
            isb.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnBook rtnb = new ReturnBook();
            rtnb.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?","Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BookDetails bd = new BookDetails();
            bd.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Settings stn = new Settings();  
            stn.Username = lbuser.Text;
            stn.ShowDialog();
        }
        public string Username = "";
        
        private void Dashboard_Load(object sender, EventArgs e)
        {
            lbuser.Text = Username;

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UPQILDP;Initial Catalog=PROJE;Integrated Security=True");
           
            
            con.Open();

            string query = "SELECT typ_no, COUNT(*) AS type_count FROM BookType WHERE typ_no IN (1, 2, 3, 4, 5) GROUP BY typ_no";
            

        }

        private void grafik_Click(object sender, EventArgs e)
        {

        }
    }
}
