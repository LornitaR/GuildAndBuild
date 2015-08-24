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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Users"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            else
            {
                string type = Request.Cookies["Users"]["UserType"];
                int typeID = Convert.ToInt32(type);
                string Username = Request.Cookies["Users"]["Username"];

                if (typeID == 1)
                {
                    Button2.Visible = false;
                    Button4.Visible = false;
                }

                try
                {
                    lblUserName.Text = Username + "'s Profile";
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    conn.Open();
                    SqlCommand com = new SqlCommand("Select GuildTypeName from GuildTypes join Users on GuildTypeID = Interests where Username = @Username", conn);
                    com.Parameters.AddWithValue("@Username", Username);
                    string interests = com.ExecuteScalar().ToString();
                    lblInterests.Text = interests;
                    SqlCommand getPhoto = new SqlCommand("Select ProfilePhoto from Users where Username = @Username", conn);
                    getPhoto.Parameters.AddWithValue("@Username", Username);
                    imgProfilePhoto.Attributes["src"] = ResolveUrl(IsEmpty(getPhoto.ExecuteScalar()));
                }
                catch (SqlException) { }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
                Response.Redirect("MyProducts.aspx");            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
               Response.Redirect("BiddingCentre.aspx");            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyBiddingCentre.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Statistics.aspx");
        }

        private string IsEmpty(object anObject)
        {
            return anObject == null ? "" : anObject.ToString();
        }
    }
}