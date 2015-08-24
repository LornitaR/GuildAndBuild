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
    public partial class Register : System.Web.UI.Page
    {
        //String aConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Users"] != null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();



                SqlCommand Location = new SqlCommand("Select LocationID, LocationName from Locations;", conn);
                SqlDataReader locDataReader;
                locDataReader = Location.ExecuteReader();
                ddlLocation.DataSource = locDataReader;
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataValueField = "LocationID";
                ddlLocation.DataBind();
                conn.Close();


                SqlConnection connUser = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                connUser.Open();

                SqlCommand User = new SqlCommand("Select UserTypeName, UserTypeID from UserTypes where UserTypeID = 1 or UserTypeID = 2;", connUser);
                SqlDataReader userDataReader;
                userDataReader = User.ExecuteReader();
                ddlUserType.DataSource = userDataReader;
                ddlUserType.DataTextField = "UserTypeName";
                ddlUserType.DataValueField = "UserTypeID";
                ddlUserType.DataBind();
                connUser.Close();

                SqlConnection connInterest = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                connInterest.Open();

                SqlCommand Interest = new SqlCommand("Select GuildTypeID, GuildTypeName from GuildTypes;", connInterest);
                SqlDataReader intDataReader;
                intDataReader = Interest.ExecuteReader();
                ddlInterests.DataSource = intDataReader;
                ddlInterests.DataTextField = "GuildTypeName";
                ddlInterests.DataValueField = "GuildTypeID";
                ddlInterests.DataBind();

                connInterest.Close();
            }

        }


        protected void btnCreateUser_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand count = new SqlCommand("SELECT coalesce (MAX(UserID), 0) FROM Users", conn);
                int UserID;
                int banned = 0;
                DateTime LastLogin = DateTime.Now;
                int count1 = (int)count.ExecuteScalar();

                if (count1 == 0)
                {
                    UserID = 1;

                }
                else
                {

                    UserID = count1 + 1;

                    bool IDExists = checkIDExists(UserID);
                    

                    if (!IDExists)
                    {
                        UserID = count1 + 1;
                    }

                    else
                    {
                        UserID += 10;
                        checkIDExists(UserID);
                    }

                }

                SqlCommand checkUsername = new SqlCommand("Select count(Username) from Users where Username = '" + txtUsername.Text + "'", conn);
                int checkUsernameCount = (int)checkUsername.ExecuteScalar();
                if (checkUsernameCount == 0)
                {

                    bool validDate = CheckDate(txtDOB.Text);

                    if (validDate)
                    {

                        bool validAge = checkAge(txtDOB.Text, LastLogin);

                        if (validAge)
                        {

                            string insertQuery = "insert into [Users](UserID, UserFirstname, UserSurname, Username, UserEmail, UserPassword, Location, UserType, DateOfBirth, Interests, Gender, SecurityQuestion, SecurityAnswer, LastLoginTime, Banned) values (@UserID,@txtFirstName, @txtSurname,@txtUsername,@txtEmail,@Password,@ddlLocation,@ddlUserType,@txtDOB, @ddlInterests ,@txtGender,@Question,@Answer, @LastLoginTime, @Banned)";

                            SqlCommand com = new SqlCommand(insertQuery, conn);
                            com.Parameters.AddWithValue("@UserID", UserID);
                            com.Parameters.AddWithValue("@txtFirstName", txtFirstName.Text);
                            com.Parameters.AddWithValue("@txtSurname", txtSurname.Text);
                            com.Parameters.AddWithValue("@txtUsername", txtUsername.Text);
                            com.Parameters.AddWithValue("@txtEmail", txtEmail.Text);
                            com.Parameters.AddWithValue("@Password", Password.Text);
                            com.Parameters.AddWithValue("@ddlLocation", ddlLocation.SelectedValue);
                            com.Parameters.AddWithValue("@ddlUserType", ddlUserType.SelectedValue);
                            com.Parameters.AddWithValue("@txtDOB", DateTime.Parse(txtDOB.Text));
                            com.Parameters.AddWithValue("@ddlInterests", ddlInterests.SelectedValue);
                            com.Parameters.AddWithValue("@txtGender", ddlGender.SelectedValue);
                            com.Parameters.AddWithValue("@Question", Question.SelectedValue);
                            com.Parameters.AddWithValue("@Answer", Answer.Text);
                            com.Parameters.AddWithValue("@LastLoginTime", LastLogin);
                            com.Parameters.AddWithValue("@Banned", banned);


                            com.ExecuteNonQuery();

                            Response.Cookies["Users"]["UserEmail"] = txtEmail.Text;
                            Response.Cookies["Users"]["UserPassword"] = Password.Text;
                            Response.Cookies["Users"]["Username"] = txtUsername.Text;
                            Response.Cookies["Users"]["UserID"] = UserID.ToString();
                            Response.Cookies["Users"]["UserType"] = ddlUserType.SelectedValue.ToString();
                            Response.Cookies["Users"]["Interest"] = ddlInterests.SelectedValue.ToString();

                            Response.Redirect("ProfileCustomer.aspx");
                            
                        }

                        else
                        {
                            lblDOBFail.Text = "You must be over 18!";
                        }
                    }

                    else
                    {
                        lblFailure.Text = "Invalid date entered";
                    }

                }

                else
                {
                    lblFailure.Text = "Username already exists";
                }

                if (ddlUserType.SelectedValue == "1")
                {
                    Response.Redirect("ProfileCustomer.aspx");
                }

                if (ddlUserType.SelectedValue == "2")
                {
                    Response.Redirect("profileArtisan.aspx");
                }
                conn.Close();
            }

            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }



        public bool checkAge(String DOB, DateTime systemDate)
        {
            DateTime DOBirth = DateTime.Parse(DOB);
            int age;
            age = (int)systemDate.Year - (int)DOBirth.Year;
            if (age > 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool checkIDExists(int userID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand userIDExists = new SqlCommand("SELECT COUNT(*) FROM Users where UserID = @UserID", conn);
            userIDExists.Parameters.AddWithValue("@UserID", userID);
            int exists = (int)userIDExists.ExecuteScalar();
            conn.Close();

            if (exists == 0)
            {
                return false;
            }


            else
            {
                return true;
            }

        }

    }
}


