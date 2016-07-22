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
    public partial class patient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie checkIfCookieIsSet = Request.Cookies["patientCookie"];

            if (checkIfCookieIsSet == null || checkIfCookieIsSet.Value == "false" || checkIfCookieIsSet["type"] != "patient")
            {
                Response.Write("Success");
                
                Response.Redirect("~/homePage.aspx");
            }
            else
            {
                Response.Cookies["patientCookie"].Expires = DateTime.Now.AddDays(-1);
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
                    Response.Redirect("~/Patient/patient.aspx");
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

        
    }
}