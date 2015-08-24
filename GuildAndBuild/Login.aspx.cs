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
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Users"] != null)
            {
                Response.Redirect("Default.aspx");
            }

        }

        //uses email and password. Compares them with each other to make sure the details match.
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand com = new SqlCommand ("select count(*) from Users where UserEmail = @Email AND UserPassword = @Password", conn);
            com.Parameters.AddWithValue("@Email", txtEmail.Text);
            com.Parameters.AddWithValue("@Password", txtPassword.Text);
           
           

            string validUser = com.ExecuteScalar().ToString();
            
            if (validUser == "1"){
                SqlCommand checkBanned = new SqlCommand("Select Banned from Users where UserEmail = @Email AND UserPassword = @Password", conn);
                checkBanned.Parameters.AddWithValue("@Email", txtEmail.Text);
                checkBanned.Parameters.AddWithValue("@Password", txtPassword.Text);
                int banned = Convert.ToInt32(checkBanned.ExecuteScalar());

                if(banned == 0)//banned users are stored on the database so they can't register again
                { 

                    SqlCommand getName = new SqlCommand("Select Username from Users where UserEmail = @Email AND UserPassword = @Password", conn);
                
                    getName.Parameters.AddWithValue("@Email", txtEmail.Text);
                    getName.Parameters.AddWithValue("@Password", txtPassword.Text);

                    SqlCommand getID = new SqlCommand("Select UserID from Users where UserEmail = @Email AND UserPassword = @Password", conn);
                    getID.Parameters.AddWithValue("@Email", txtEmail.Text);
                    getID.Parameters.AddWithValue("@Password", txtPassword.Text);

                    SqlCommand getType = new SqlCommand("Select UserType from Users where UserEmail = @Email AND UserPassword = @Password", conn);
                    getType.Parameters.AddWithValue("@Email", txtEmail.Text);
                    getType.Parameters.AddWithValue("@Password", txtPassword.Text);

                    SqlCommand getInterest = new SqlCommand("Select Interests from Users where UserEmail = @Email AND UserPassword = @Password", conn);
                    getInterest.Parameters.AddWithValue("@Email", txtEmail.Text);
                    getInterest.Parameters.AddWithValue("@Password", txtPassword.Text);

                    string username = getName.ExecuteScalar().ToString();
                    string userType = getType.ExecuteScalar().ToString();
                    string userID = getID.ExecuteScalar().ToString();
                    string userInterest = getInterest.ExecuteScalar().ToString();

                    Response.Cookies["Users"]["UserEmail"] = txtEmail.Text;
                    Response.Cookies["Users"]["UserPassword"] = txtPassword.Text;
                    Response.Cookies["Users"]["Username"] = username;
                    Response.Cookies["Users"]["UserID"] = userID;   
                    Response.Cookies["Users"]["UserType"] = userType;
                    Response.Cookies["Users"]["Interest"] = userInterest;

                    Response.Cookies["Users"].Expires = DateTime.Now.AddDays(1);
                    string email = txtEmail.Text;
                    UserAccess.changeLoginTime(email);

                    if(userType.Equals("3")){
                        Response.Redirect("Admin.aspx");
                    }

                    else
                    {
                        Response.Redirect("ProfileCustomer.aspx");
                    }
                }
                    else
                    {
                        lblFailure.Text = "Sorry, you have been banned";
                    }
            }

            
            
            else 
            {
               lblFailure.Text = "Incorrect login details";
               
            }

                       

            conn.Close();
        }

        //displays textboxes to the user where they can enter their email address if they forgot their password
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            
            lblEmailAddress2.Visible = true;
            txtEmailAddress2.Visible = true;
            btnForgotPassword.Visible = false;
            txtEmail.Visible = false;
            lblEmail.Visible = false;
            lblPassword.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            
            btnEmail.Visible = true;
        }
        
              
        //takes the email address entered in the new textbox that has appeared
        //makes sure the email exists
        //shows the user's security question and a box to enter their answer
        protected void btnEmail_Click(object sender, EventArgs e)
        {
            lblQuestion.Visible = true;
            lblQuestion2.Visible = true;
            lblAnswer.Visible = true;
            txtAnswer.Visible = true;
            btnSubmit.Visible = true;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand checkEmail = new SqlCommand("select count(*) from Users where UserEmail = @Email", conn);
            checkEmail.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
            int emailCount = Convert.ToInt32(checkEmail.ExecuteScalar());

            if (emailCount == 1)
            {
                SqlCommand getQuestion = new SqlCommand("select SecurityQuestion from Users where UserEmail = @Email", conn);
                getQuestion.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);

                string question = getQuestion.ExecuteScalar().ToString();

                if (question != null)
                {
                    lblQuestion2.Text = question;
                }

                else
                {
                    lblFailure.Text = "Invalid email address";
                }
            }

            else
            {
                lblFailure.Text = "Invalid email address";
            }
            
        }

        //takes the email, question, and answer and checks to make sure they all match
        //if the details are correct, the user can reset their password
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand com = new SqlCommand("select count(*) from Users where UserEmail = @Email AND SecurityQuestion = @Question AND SecurityAnswer = @Answer", conn);
            com.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
            com.Parameters.AddWithValue("@Question", lblQuestion2.Text);
            com.Parameters.AddWithValue("@Answer", txtAnswer.Text);
            string validUser = com.ExecuteScalar().ToString();

            if (validUser == "1")
            {

                lblNewPassword.Visible = true;
                lblConfirmNewPassword.Visible = true;
                txtNewPassword.Visible = true;
                txtConfirmPassword.Visible = true;
                btnNewPassword.Visible = true;
                
            }

            else
            {
                lblFailure.Text = "Invalid details";
            }

        }

        //after resetting the password, the user is redirected to their profile page
        protected void btnNewPassword_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand getID = new SqlCommand("select UserID from Users where UserEmail = @Email", conn);
            getID.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
            int UserID = Convert.ToInt32(getID.ExecuteScalar());
            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                string password = txtNewPassword.Text;
                UserAccess.changePassword(password, UserID);//Method in User.cs
            
                SqlCommand com = new SqlCommand("select count(*) from Users where UserEmail = @Email AND SecurityQuestion = @Question AND SecurityAnswer = @Answer", conn);
                com.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
                com.Parameters.AddWithValue("@Question", lblQuestion2.Text);
                com.Parameters.AddWithValue("@Answer", txtAnswer.Text);

                string validUser = com.ExecuteScalar().ToString();

                if (validUser == "1")
                {
                

                    SqlCommand getUsername = new SqlCommand("Select Username from Users where UserEmail = @Email AND SecurityQuestion = @Question AND SecurityAnswer = @Answer", conn);
                    getUsername.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
                    getUsername.Parameters.AddWithValue("@Question", lblQuestion2.Text);
                    getUsername.Parameters.AddWithValue("@Answer", txtAnswer.Text);
                    string username = getUsername.ExecuteScalar().ToString();


                    SqlCommand getType = new SqlCommand("Select UserType from Users where UserEmail = @Email AND SecurityQuestion = @Question AND SecurityAnswer = @Answer", conn);
                    getType.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
                    getType.Parameters.AddWithValue("@Answer", txtAnswer.Text);
                    getType.Parameters.AddWithValue("@Question", lblQuestion2.Text);
                    
                    SqlCommand getInterest = new SqlCommand("Select Interests from Users where UserEmail = @Email AND SecurityQuestion = @Question AND SecurityAnswer = @Answer", conn);
                    getInterest.Parameters.AddWithValue("@Email", txtEmailAddress2.Text);
                    getInterest.Parameters.AddWithValue("@Answer", txtAnswer.Text);
                    getInterest.Parameters.AddWithValue("@Question", lblQuestion2.Text);

                    string userType = getType.ExecuteScalar().ToString();
                    string userID = getID.ExecuteScalar().ToString();
                    string userInterest = getInterest.ExecuteScalar().ToString();


                    Response.Cookies["Users"]["UserEmail"] = txtEmailAddress2.Text;
                    Response.Cookies["Users"]["UserPassword"] = txtNewPassword.Text;
                    Response.Cookies["Users"]["Username"] = username;
                    Response.Cookies["Users"]["UserID"] = userID;
                    Response.Cookies["Users"]["UserType"] = userType;
                    Response.Cookies["Users"]["Interest"] = userInterest;

                    Response.Cookies["Users"].Expires = DateTime.Now.AddDays(1);

                    string email = txtEmail.Text;
                    UserAccess.changeLoginTime(email);//Method in User.cs

                    Response.Redirect("ProfileCustomer.aspx");
                    conn.Close();
                }

                else
                {
                    lblFailure.Text = "Answer does not match";
                }
            }

            else
            {
                lblFailure.Text = "Passwords don't match";
            }

        }

        

    }
}