using Attendance.DataSet1TableAdapters;
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
    public partial class MainFrm : MetroFramework.Forms.MetroForm
    {

        public int loggedIn { get; set; }
        //passing userid to the form
        public int UserID { get; set; }
       // public int UserName { get; set; }
        public MainFrm()
        {
            InitializeComponent();
            loggedIn = 0;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

            
        }

        private void MainFrm_Activated(object sender, EventArgs e)
        {
            

            if (loggedIn == 0)
            {
                //open login form
                LoginFrm newlogin = new LoginFrm();
                newlogin.ShowDialog();

                if (newlogin.loginFlag == false)
                {

                    Close();
                }
                else
                {
                    //show user id in the status bar
                    UserID = newlogin.UserID;
                    
                    statLbUser.Text = UserID.ToString();
                    loggedIn = 1;

                    this.classesTBLTableAdapter.Fill(this.dataSet1.ClassesTBL);
                    classesTBLBindingSource.Filter = "UserID='" + UserID.ToString() + "'";

                }
            }
        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            //open add class form using userid
            FrmAddClass addClass = new FrmAddClass();
            addClass.UserID = this.UserID;
            addClass.ShowDialog();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            //open add students form
            StudentsFrm students = new StudentsFrm();
            //this code for retrive data from combobox of class name
            students.ClassName = metroComboBox1.Text;
            //retrive data  from combobox of class id and for int command because data of class id is in int datatype
            students.ClassID = (int)metroComboBox1.SelectedValue;

            students.ShowDialog();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            AttendanceREcordsTBTableAdapter ada = new AttendanceREcordsTBTableAdapter();
            DataTable dt = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

            if(dt.Rows.Count>0)
            {
                DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

                dataGridView1.DataSource = dt_new;


            }
            else
            {
                StudentsTBTableAdapter students_adapter = new StudentsTBTableAdapter();

                DataTable dt_students = students_adapter.GetDataByClassID((int)metroComboBox1.SelectedValue);

                foreach(DataRow row in dt_students.Rows)
                {

                    //insert a new record for the students
                    ada.InsertQuery((string)row[0], (int)metroComboBox1.SelectedValue, dateTimePicker1.Text, "", row[1].ToString(), metroComboBox1.Text);
                }
                DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

                dataGridView1.DataSource = dt_new;

            }

        
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            AttendanceREcordsTBTableAdapter ada = new AttendanceREcordsTBTableAdapter();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //if any box is empty this code will work

                if (row.Cells[1].Value != null)
                {
                    //update table for attendance
                    ada.UpdateQuery(row.Cells[2].Value.ToString(), row.Cells[1].Value.ToString(), (int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

                }
            }
            DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

            dataGridView1.DataSource = dt_new;

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            //get students
            StudentsTBTableAdapter students_adapter = new StudentsTBTableAdapter();

            DataTable dt_students = students_adapter.GetDataByClassID((int)metroComboBox2.SelectedValue);
            AttendanceREcordsTBTableAdapter ada = new AttendanceREcordsTBTableAdapter();

           
            int p = 0;
            int A = 0;
            int PA = 0;
            
            //loop through students and get the values
            foreach (DataRow row in dt_students.Rows)
            {
                

                //Presence count
                p = (int)ada.GetDataByReport(dateTimePicker2.Value.Month, row[1].ToString(), "p").Rows[0][6];

                // Absence
                A = (int)ada.GetDataByReport(dateTimePicker2.Value.Month, row[1].ToString(), "a").Rows[0][6];

                // percentage
                PA = (int)ada.GetDataByReport(dateTimePicker2.Value.Month, row[1].ToString(), "%").Rows[0][6];


              


                ListViewItem litem = new ListViewItem();
               
                litem.Text = row[1].ToString();

                litem.SubItems.Add(p.ToString());
                litem.SubItems.Add(A.ToString());
                litem.SubItems.Add(PA.ToString());
                
                listView1.Items.Add(litem);

            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            AttendanceREcordsTBTableAdapter ada = new AttendanceREcordsTBTableAdapter();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //if any box is empty this code will work

                if (row.Cells[1].Value != null)
                {
                    //update table for attendance
                    ada.UpdateQuery("", row.Cells[1].Value.ToString(), (int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

                }
            }
            DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

            dataGridView1.DataSource = dt_new;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterFrm reg = new RegisterFrm();
            reg.ShowDialog();
        }
    }
    }

