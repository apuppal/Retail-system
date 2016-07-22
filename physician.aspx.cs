using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Assignment_1
{
    public partial class physician : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie checkIfCookieIsSet = Request.Cookies["physicianCookie"];

            if (checkIfCookieIsSet == null || checkIfCookieIsSet.Value == "false" || checkIfCookieIsSet["type"] != "physician")
            {
                
                Response.Redirect("~/homePage.aspx");
            }
            else
            {
                Response.Cookies["physicianCookie"].Expires = DateTime.Now.AddDays(-1);
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
                    Response.Redirect("~/Staff/physician.aspx");
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

        protected void Button12_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 7;
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 8;
            string s = PatientRadioButtons.SelectedValue.ToString();
            Label14.Text =s +" HISTORY";
            if(s=="RAVI TEJA")
            {
                Label15.Text = " DATE : 04/06/2015,\t     TEMPERATURE : 96 F, \t  BLOOD PRESSURE : 120/79 mm HG, \t    PULSE RATE : 101 bpm ";

            }
            else if(s=="SHASHANK CHENJI")
            {
                Label15.Text = " DATE : 03/06/2015,\t     TEMPERATURE : 98 F, \t   BLOOD PRESSURE : 120/80 mm HG, \t    PULSE RATE : 100 bpm ";

            }
            else if(s=="PRANAV KUMAR")
            {
                Label15.Text = " DATE : 04/16/2015,\t     TEMPERATURE : 100 F, \t     BLOOD PRESSURE : 119/79 mm HG, \t   PULSE RATE : 99 bpm ";

            }

        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 9;
            Label20.Visible = false;
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(temperature.Text, @"^[0-9]*$") && Regex.IsMatch(pulseRate.Text, @"^[0-9]*$") && Regex.IsMatch(bloodPressure.Text, @"^\d{1,3}\/\d{1,3}$"))
            {
                DateTime thisDay = DateTime.Today;
                string date = thisDay.ToString("d");
                string s = " NEW MEASUREMENTS ARE DATE :  " + date + ",   TEMERATURE : " + temperature.Text.ToString() + " F,    BLOOD PRESSURE : " + bloodPressure.Text.ToString() + " mm HG,    PULSE RATE : " + pulseRate.Text.ToString() + " bpm ";
                Label20.Text = s;

            }
            else if (!Regex.IsMatch(temperature.Text, @"^[0-9]*$") || string.IsNullOrWhiteSpace(temperature.Text) || temperature.Text.Trim().Length == 0)
            {
                Label20.Text = " Pease give proper  value for temperature !!";

            }
            else if (!Regex.IsMatch(bloodPressure.Text, @"^\d{1,3}\/\d{1,3}$") || string.IsNullOrWhiteSpace(bloodPressure.Text) || bloodPressure.Text.Trim().Length == 0)
            {
                Label20.Text = " Pease give proper  value for blood presuure !!";

            }
            else if (!Regex.IsMatch(pulseRate.Text, @"^[0-9]*$") || string.IsNullOrWhiteSpace(pulseRate.Text) || pulseRate.Text.Trim().Length == 0)
            {
                Label20.Text = " Pease give proper  value for pulse rate !!";
            }

            Label20.Visible = true;
        }

        protected void Button16_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 10;
            commentsCheckList.Visible = false;
            Button18.Visible = false;
            Label22.Visible = false;
            Label23.Visible = false;
            Label24.Visible = false;
            commentsText.Visible = false;
        }

        protected void Button18_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<ListItem> selected = new List<ListItem>();
            string symptoms="";
            foreach (ListItem item in commentsCheckList.Items)
                if(item.Selected)
                {
                    if(count==0)
                    symptoms = symptoms + " " + item.Text.ToString();
                    else
                        symptoms = symptoms + ", " + item.Text.ToString();
                    count++;
                }

            Label23.Visible = true;
            Label24.Visible = true;
            Label23.Text = " Symptoms  for " + patientListForComments.SelectedValue.ToString() + " are " + symptoms + "</br>";
            Label24.Text = " Comments added  for " + patientListForComments.SelectedValue.ToString() + " are " + commentsText.Text + "</br>";

                
        }

        protected void Button17_Click(object sender, EventArgs e)
        {
            Label22.Text = Label22.Text + patientListForComments.SelectedValue.ToString();
            commentsCheckList.Visible = true;
            Button18.Visible = true;
            Label22.Visible = true;
            commentsText.Visible = true;
        }

        
    }
}