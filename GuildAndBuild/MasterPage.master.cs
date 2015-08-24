using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnCart.Visible = true;
        if (this.Request.Cookies["Users"] != null)
        {
            string Username = Request.Cookies["Users"]["Username"];

            SqlConnection getUserType = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand type = new SqlCommand("SELECT UserType FROM Users WHERE Username = @Username", getUserType);
            getUserType.Open();
            type.Parameters.AddWithValue("@Username", Username);

            
            int UserType = (int)type.ExecuteScalar();
            

            getUserType.Close();

            lblUser.Text = Username;
            btnInbox.Visible = true;
            btnLogout.Visible = true;
            btnMyAccount.Visible = true;
            btnSettings.Visible = true;

            if (UserType == 1)                  //Customer
            {
                btnProject.Visible = true;
            }

            else if (UserType == 2)             //Artisan
            {
                btnCart.Visible = true;
                btnProduct.Visible = true;
                btnProject.Visible = true;
            }

            else                                //Admin      
            {
                btnIssues.Visible = true;
                btnMyAccount.Visible = true;
            }
        }

        else
        {
            btnRegister.Visible = true;
            btnLogin.Visible = true;
            btnMyAccount.Visible = false;
        }

    }
    protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void btnProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyProjects.aspx");
    }
    protected void btnProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyProducts.aspx");
    }
    protected void btnCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyCart.aspx");
    }
    protected void btnInbox_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inbox.aspx");
    }
    protected void btnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("Settings.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Cookies["Users"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("Logout.aspx");
    }

    protected void btnIssues_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin.aspx");
    }

    protected void btnMyAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfileCustomer.aspx");
    }
    
}