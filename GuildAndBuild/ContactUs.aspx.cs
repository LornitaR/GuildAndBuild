using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.Cookies["Users"] != null)
            {
                lblIssues.Visible = true;
                ddlIssues.Visible = true;
                lblMessage.Visible = true;
                txtMessage.Visible = true;
                btnSubmit.Visible = true;
            }
            else
            {
                lblUnregistered.Visible = true;
            }
            
            String aConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            if (!IsPostBack)
            {
                SqlConnection aConnection = new SqlConnection();
                SqlCommand fillDropdown = new SqlCommand();
                SqlDataReader aDataReader;
                aConnection.ConnectionString = aConnectionString;
                fillDropdown = new SqlCommand("SELECT ComplaintTypeName, ComplaintTypeID FROM ComplaintTypes", aConnection);
                aConnection.Open();
                aDataReader = fillDropdown.ExecuteReader();

                ddlIssues.DataSource = aDataReader;
                ddlIssues.DataTextField = "ComplaintTypeName";
                ddlIssues.DataValueField = "ComplaintTypeID";
                ddlIssues.DataBind();
                aConnection.Close();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection newComplaint = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                newComplaint.Open();

                //Generate a Unique ComplaintID
                SqlCommand countComplaintID = new SqlCommand("SELECT COALESCE(MAX(ComplaintID), 0) FROM Complaints", newComplaint);
                int ComplaintID = (int)countComplaintID.ExecuteScalar() + 1;

                string UserEmail = Request.Cookies["Users"]["UserEmail"];
                SqlCommand getID = new SqlCommand("SELECT UserID FROM Users WHERE UserEmail = '" + UserEmail + "'", newComplaint);
                int UserID = (int)getID.ExecuteScalar();

                string insertQuery = "INSERT INTO [Complaints](ComplaintID, UserID, ComplaintDate, ComplaintTypeID, ComplaintText)" +
                    "VALUES (@ComplaintID, @UserID, @ComplaintDate, @ComplaintTypeID, @ComplaintText)";

                SqlCommand fillNewComplaint = new SqlCommand(insertQuery, newComplaint);
                fillNewComplaint.Parameters.AddWithValue("@ComplaintID", ComplaintID);
                fillNewComplaint.Parameters.AddWithValue("@UserID", UserID);
                fillNewComplaint.Parameters.AddWithValue("@ComplaintDate", DateTime.Now);
                fillNewComplaint.Parameters.AddWithValue("@ComplaintTypeID", ddlIssues.SelectedValue);
                fillNewComplaint.Parameters.AddWithValue("@ComplaintText", txtMessage.Text);

                fillNewComplaint.ExecuteNonQuery();
                newComplaint.Close();

                lblResult.Text = "Your message has been sent successfully. A reply will be sent to your inbox in the coming days.";
                txtMessage.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }
    }
}