using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    public partial class LoginFrm :Form
    {
        //public property to get and set the value
        public bool loginFlag { get; set; }

        //passing userid to the form
        public int UserID { get; set; }
        public LoginFrm()
        {
            InitializeComponent();
            loginFlag = false;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //checking loging details from the dataset
            DataSet1TableAdapters.UsersTableAdapter UserAda = new DataSet1TableAdapters.UsersTableAdapter();
            DataTable dt = UserAda.GetDataByUserAndPass(metroTextBoxUser.Text, metroTextBoxPass.Text);


            if(dt.Rows.Count > 0)
            {
                //valid
                
                UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                loginFlag = true;
               
            }
            else
            {

                //not valid
                MessageBox.Show("Access Denied");
                UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                loginFlag = false;
                
            }
            Close();

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
