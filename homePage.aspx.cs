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
    public partial class homePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {



            if (password.Text == "$$$" && userName.Text == "Patient")
            {
                Response.Redirect("~/Patient/patient.aspx");
            }
            else if (password.Text == "%%%" && userName.Text == "Nurse")
            {
                Response.Redirect("~/Nurse/nurse.aspx");
            }
            else if (password.Text == "%%%" && userName.Text == "Staff")
            {
                Response.Redirect("~/Staff/staff.aspx");

            }
            else if (password.Text == "***" && userName.Text == "Physician")
            {
                Response.Redirect("~/Physician/physician.aspx");

            }
            else if (password.Text == "@@@" && userName.Text == "Administrator")
            {
                Response.Redirect("~/Admin/admin.aspx");

            }
            else
            {
                Response.Redirect("~/homePage.aspx");
            }
        }
    }
}