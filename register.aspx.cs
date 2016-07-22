using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace Assignment_1.Registration
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (password.Text == confirmPassword.Text)
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection myConnection = new SqlConnection(cs))
                {
                    myConnection.Open();
                    string user = userName.Text;
                    string pass = password.Text;
                    string encryptedUserName=FormsAuthentication.HashPasswordForStoringInConfigFile(user,"SHA1");

                    string encryptedPassword=FormsAuthentication.HashPasswordForStoringInConfigFile(pass,"SHA1");
                    //string sql = "CREATE TABLE Appointments(PhysicianName varchar(255),PatientName varchar(255),patientEmail varchar(255), AppointmentTime varchar(255));";
                    //string sql = "drop table Appointments;";
                    string sql = "select * from users where userName='" + encryptedUserName +  "';";
                    SqlCommand command = new SqlCommand(sql, myConnection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        registerStatus.Text = "UserName  already exists. Please Select different UserName !!";

                    }
                    else
                    {
                        sql = "insert into users values('" + encryptedUserName + "','" + encryptedPassword + "');";
                        command = new SqlCommand(sql, myConnection);

                        dataReader.Close();
                        dataReader = command.ExecuteReader();
                        registerStatus.Text = "User Registered  Successfully !!";
                        Response.Redirect("~/homePage.aspx");
                    }

                    dataReader.Close();
                    command.Dispose();
                    myConnection.Close();

                }
            }
            else
            {
                registerStatus.Text = "Password and Confirm Password did not match !!";
            }
        }
    }
}