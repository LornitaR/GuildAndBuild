using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace GuildAndBuild
{
    
    public partial class Settings : System.Web.UI.Page
    {
        
        public int UserID;
        public int UserType;
        string previousLocation, previousGender, previousInterest;
        public string UserEmail;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Users"] != null)
            {

                UserEmail = Request.Cookies["Users"]["UserEmail"];
                SqlConnection connID = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                connID.Open();
                SqlCommand getID = new SqlCommand("Select UserID from Users where UserEmail = @UserEmail;", connID);
                getID.Parameters.AddWithValue("@UserEmail", UserEmail);
                UserID = (int)getID.ExecuteScalar();
                SqlCommand getType = new SqlCommand("Select UserType from Users where UserEmail = @UserEmail;", connID);
                getType.Parameters.AddWithValue("@UserEmail", UserEmail);
                UserType = (int)getType.ExecuteScalar();
                connID.Close();

                if (UserType == 1)
                {
                    lblShopName.Visible = false;
                    txtShopName.Visible = false;
                    lblArtisanDescription.Visible = false;
                    txtDescription.Visible = false;
                }

                else if (UserType == 2)
                {
                    lblInterests.Visible = false;
                    ddlInterests.Visible = false;
                }

                if (!IsPostBack)
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    conn.Open();
                    SqlConnection connInterest = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    connInterest.Open();


                    SqlCommand Location = new SqlCommand("Select * from Locations;", conn);
                    SqlDataReader locDataReader;
                    locDataReader = Location.ExecuteReader();
                    ddlLocation.DataSource = locDataReader;
                    ddlLocation.DataTextField = "LocationName";
                    ddlLocation.DataValueField = "LocationID";
                    ddlLocation.DataBind();



                    SqlCommand Interest = new SqlCommand("Select * from GuildTypes;", connInterest);
                    SqlDataReader intDataReader;
                    intDataReader = Interest.ExecuteReader();
                    ddlInterests.DataSource = intDataReader;
                    ddlInterests.DataTextField = "GuildTypeName";
                    ddlInterests.DataValueField = "GuildTypeID";
                    ddlInterests.DataBind();


                    conn.Close();
                    connInterest.Close();

                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        //deletes user account and removes details from database
        protected void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            UserAccess.deleteAccount(UserEmail, UserType);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand checkDelete = new SqlCommand("Select count(*) from Users where UserEmail = @UserEmail", conn);
            checkDelete.Parameters.AddWithValue("@UserEmail", UserEmail);
            int count = (int) checkDelete.ExecuteScalar();

            if (count == 0)
            {
                Response.Cookies["Users"].Expires = DateTime.Now.AddDays(-1);
                Response.Redirect("DeletedProfile.aspx");
            }

            else
            {
                lblFailure.Text = "Problem deleting account!";
                lblFailure.ForeColor = System.Drawing.Color.Red;
            }
                
            conn.Close();
        }

        //changes user details if something is changed on this page.
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand getPrevious = new SqlCommand("Select Location, Interests, Gender from  User where UserEmail = @UserEmail", conn);
            getPrevious.Parameters.AddWithValue("@UserEmail", UserEmail);
            previousLocation = "Location";
            previousInterest = "Interests";
            previousGender = "Gender";
            

            User aUser = new User();

            if (txtShopName.Text != "")
            {
                UserAccess.changeShopName(txtShopName.Text, UserID);//Method in User.cs
            }

            if (txtDescription.Text != "")
            {
                UserAccess.changeBio(txtDescription.Text, UserID);//Method in User.cs
            }

            if (txtFirstName.Text != "")
            {
                UserAccess.changeFirstname(txtFirstName.Text, UserID);//Method in User.cs
            }


            if (txtSurname.Text != "")
            {
                UserAccess.changeSurname(txtSurname.Text, UserID);//Method in User.cs
            }

            if (txtUsername.Text != "")
            {
                UserAccess.changeUsername(txtUsername.Text, UserID);//Method in User.cs
            }

            if (txtNewPassword.Text != "" && txtNewPassword.Text == txtReEnterPassword.Text)
            {
                UserAccess.changePassword(txtNewPassword.Text, UserID);//Method in User.cs
            }
            

            if (ddlLocation.SelectedValue != previousLocation)
            {
                UserAccess.changeLocation(Convert.ToInt32(ddlLocation.SelectedValue), UserID);//Method in User.cs
            }

            if (txtDOB.Text != "")
            {
                DateTime DOB = Convert.ToDateTime(txtDOB.Text);
                UserAccess.changeDOB(DOB, UserID);//Method in User.cs
            }

            if (ddlInterests.SelectedValue != previousInterest)
            {
                UserAccess.changeInterests(Convert.ToInt32(ddlInterests.SelectedValue), UserID);//Method in User.cs
            }

            if (ddlGender.SelectedValue != previousGender)
            {
                UserAccess.changeGender(ddlGender.SelectedValue, UserID);//Method in User.cs
            }

            if (txtAddress.Text != "")
            {
                UserAccess.changeAddress(txtAddress.Text, UserID);//Method in User.cs
            }

            string saveDir = @"images\";
            string appPath = Request.PhysicalApplicationPath;
            string savePath, dbPath;

            if (fuProfilePhoto.HasFile)
            {
                if (checkFileType(fuProfilePhoto.FileName) == true)
                {
                    savePath = appPath + saveDir + fuProfilePhoto.FileName;
                    dbPath = "\\images\\" + fuProfilePhoto.FileName;
                    fuProfilePhoto.SaveAs(savePath);

                    UserAccess.changeProfilePicture(dbPath, UserID);//Method in User.cs


                  
                }
                else
                    lblUploadStatus.Text = "Upload status: No File Selected.";
            }

            lblFailure.Text = "Settings changed!";
            lblFailure.ForeColor = System.Drawing.Color.Green;
            txtDescription.Text = "";
            txtShopName.Text = "";
            txtFirstName.Text = "";
            txtSurname.Text = "";
            txtUsername.Text = "";
            txtNewPassword.Text = "";
            txtReEnterPassword.Text = "";
            txtDOB.Text = "";
            txtAddress.Text = "";
            conn.Close();

        }

        //takes in the file extension and makes sure it is compatible with our accected file types
        protected bool checkFileType(string file)
        {
            string extension = Path.GetExtension(file);
            extension.ToLower();
            switch (extension)
            {
                case ".gif":
                case ".png":
                case ".jpg":
                case ".jpeg": return true;
                default: return false;
            }
        }
    }
}