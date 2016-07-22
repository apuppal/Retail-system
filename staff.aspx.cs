using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Assignment_1
{
    public partial class staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie checkIfCookieIsSet = Request.Cookies["staffCookie"];

            if (checkIfCookieIsSet == null || checkIfCookieIsSet.Value == "false" || checkIfCookieIsSet["type"] != "staff")
            {
                
                Response.Redirect("~/homePage.aspx");
            }
            else
            {
                Response.Cookies["staffCookie"].Expires = DateTime.Now.AddDays(-1);
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            
            
            
            MultiView1.ActiveViewIndex = 4;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            Details.Text = "Thomas S Abott is from Senatara Medical School";
            universityLink.Text = "www.senatara.com";
            universityLink.NavigateUrl = "http://www.sentara.com/hampton-roads-virginia";

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            Details.Text = "Alfred Z Fernandes is from EVMC Medical School";
            universityLink.Text = "www.evmc.com";
            universityLink.NavigateUrl = "http://www.sentara.com/hampton-roads-virginia";
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            Details.Text = "Yasir Ahmed is from Fernandes Medical School";
            universityLink.Text = "www.fernandes.com";
            universityLink.NavigateUrl = "http://www.sentara.com/hampton-roads-virginia";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            
            string pName=patientName.SelectedValue.ToString();
            string pEmail = patientEmail.Text.ToString();
            string phyName=physicianName.SelectedValue.ToString();
            string date=Calendar2.SelectedDate.ToShortDateString();
            //string cs = "data source=(localdb)\\DotNet; database=MyDotNetApplications; integrated security =SSPI";
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection myConnection = new SqlConnection(cs))
            {
                myConnection.Open();
                //string sql = "CREATE TABLE Appointments(PhysicianName varchar(255),PatientName varchar(255),patientEmail varchar(255), AppointmentTime varchar(255));";
                //string sql = "drop table Appointments;";
                string sql = "select * from Appointments where PhysicianName='" + phyName + "' and AppointmentTime ='" + date + "';" ;
                SqlCommand command = new SqlCommand(sql, myConnection);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {

                    messageForAppointment.Text = "Date is already scheduled. Please Select different date !!";

                }
                else
                {
                    sql = "insert into Appointments values('" + phyName + "','" + pName + "','" + pEmail + "','" + date + "');";
                    command = new SqlCommand(sql, myConnection);

                    dataReader.Close();
                    dataReader = command.ExecuteReader();
                    messageForAppointment.Text = "Appointment Scheduled";
                    Response.Redirect("~/Staff/staff.aspx");
                }

                dataReader.Close();
                command.Dispose();
                myConnection.Close();

            }
        }

        protected void logOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/homePage.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 5;
           
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
           
            MultiView1.ActiveViewIndex = 6;

        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            int i = physiciansListRadioButtons.SelectedIndex;
            string phyName="";
            string date = Calendar3.SelectedDate.ToShortDateString();

            if(i==0)
            {
                phyName = "Thomas S Abott";

            }
            else if (i == 1)
            {
                phyName = "Alfred Z Fernandes";

            }
            else if (i == 2)
            {
                phyName = "Yasir Ahmed";

            }

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection myConnection = new SqlConnection(cs))
            {
                myConnection.Open();
                //string sql = "CREATE TABLE Appointments(PhysicianName varchar(255),PatientName varchar(255),patientEmail varchar(255), AppointmentTime varchar(255));";
                //string sql = "drop table Appointments;";
                string sql = "select * from Appointments where PhysicianName='" + phyName + "' and AppointmentTime ='" + date + "';";
                SqlCommand command = new SqlCommand(sql, myConnection);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    Label12.Text = "APPOINTMENTS ARE:";

                    GridView1.DataSource = dataReader;
                    GridView1.DataBind();
                    GridView1.Visible = true;

                }
                else
                {
                    
                    Label12.Text = "NO APPOINTMENTS ON THIS DAY !!";
                    GridView1.Visible = false;
                }

                dataReader.Close();
                command.Dispose();
                myConnection.Close();

            }
            

        }

        
    }
}