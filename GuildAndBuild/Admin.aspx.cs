using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace GuildAndBuild
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void GridViewComplaints_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = GridViewComplaints.SelectedRow;
            string Username = row.Cells[1].Text.Trim();
            lblInvisibleUsername.Text = Username;

            SqlConnection getFullName = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            getFullName.Open();
            SqlCommand fName = new SqlCommand("SELECT UserFirstname FROM Users WHERE Username = '" + Username + "'", getFullName);
            SqlCommand sName = new SqlCommand("SELECT UserSurname FROM Users WHERE Username = '" + Username + "'", getFullName);
            string FullName = fName.ExecuteScalar().ToString().Trim() + " " + sName.ExecuteScalar().ToString().Trim();
            getFullName.Close();

            string ComplaintTypeName = row.Cells[3].Text.Trim();
            txtSolveIssue.Text = "RE: " + ComplaintTypeName + " Issue\nDear " + FullName + ",\n";
        }

        protected void GridViewComplaints_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = GridViewComplaints.Rows[e.NewSelectedIndex];
        }

        protected void GridViewComplaints_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridViewComplaints.PageIndex = e.NewPageIndex;
            GridViewComplaints.DataBind();
        }

        protected void btnSolveComplaint_Click(Object sender, EventArgs e)
        {
            try
            {
                SqlConnection solveIssue = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                solveIssue.Open();

                string Username = lblInvisibleUsername.Text;
                string admin = Request.Cookies["Users"]["Username"];
                DateTime now = DateTime.Now;
                string message = txtSolveIssue.Text;
                MessageAccess.createMessage(Username, admin, now, message);

                solveIssue.Close();

                txtSolveIssue.Text = "";
                lblStatus.Text = "Message Sent!";
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        protected void btnBanUser_Click(object sender, EventArgs e)
        {
            string username = txtBan.Text.Trim();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand banUser = new SqlCommand("UPDATE Users SET Banned = @Truth WHERE Username = @Username;", conn);
                banUser.Parameters.AddWithValue("@Username", username);
                banUser.Parameters.AddWithValue("@truth", true);

                banUser.ExecuteNonQuery();

                conn.Close();
                lblDeletedUser.Text = "Successfully Banned";
                txtBan.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }
    }
}