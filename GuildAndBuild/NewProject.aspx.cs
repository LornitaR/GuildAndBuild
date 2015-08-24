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
    public partial class NewProject : System.Web.UI.Page
    {
        String aConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection aConnection = new SqlConnection();
                SqlCommand aCommand = new SqlCommand();
                SqlDataReader aDataReader;
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand("SELECT GuildTypeName, GuildTypeID FROM GuildTypes", aConnection);
                aConnection.Open();
                aDataReader = aCommand.ExecuteReader();

                ddlGuild.DataSource = aDataReader;
                ddlGuild.DataTextField = "GuildTypeName";
                ddlGuild.DataValueField = "GuildTypeID";
                ddlGuild.DataBind();
                aConnection.Close();

                cvDeadline.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");

                if (Request.QueryString["set"] != null)
                {
                    rblPrivacy.SelectedValue = "0";
                    txtArtisan.Visible = true;
                    txtArtisan.Text = Request.QueryString["set"];
                }
            }
        }

        protected void ddlGuild_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection materialConnect = new SqlConnection();
            SqlCommand aCommand = new SqlCommand();
            SqlDataReader aDataReader;
            materialConnect.ConnectionString = aConnectionString;
            aCommand = new SqlCommand("SELECT MaterialTypeName, MaterialTypeID FROM MaterialTypes m JOIN GuildTypes g ON m.GuildTypeID = g.GuildTypeID WHERE GuildTypeName = @Guild" +
                " UNION SELECT MaterialTypeName, MaterialTypeID FROM MaterialTypes WHERE MaterialTypeID = 0 OR MaterialTypeID = 47 ORDER BY MaterialTypeID", materialConnect);

            materialConnect.Open();
            aCommand.Parameters.AddWithValue("@Guild", ddlGuild.SelectedItem.Text);
            aDataReader = aCommand.ExecuteReader();

            ddlMaterial.DataSource = aDataReader;
            ddlMaterial.DataTextField = "MaterialTypeName";
            ddlMaterial.DataValueField = "MaterialTypeID";
            ddlMaterial.DataBind();
            materialConnect.Close();

            SqlConnection typeConnect = new SqlConnection();
            SqlCommand bCommand = new SqlCommand();
            SqlDataReader bDataReader;
            typeConnect.ConnectionString = aConnectionString;
            bCommand = new SqlCommand("SELECT TypeName, TypeID FROM Types t JOIN GuildTypes g ON t.GuildTypeID = g.GuildTypeID WHERE GuildTypeName = @Guild" +
                " UNION SELECT TypeName, TypeID FROM Types WHERE TypeID = 0 OR TypeID = 29 ORDER BY TypeID", typeConnect);

            typeConnect.Open();
            bCommand.Parameters.AddWithValue("@Guild", ddlGuild.SelectedItem.Text);
            bDataReader = bCommand.ExecuteReader();

            ddlType.DataSource = bDataReader;
            ddlType.DataTextField = "TypeName";
            ddlType.DataValueField = "TypeID";
            ddlType.DataBind();
            typeConnect.Close();
        }

        protected void CalendarDeadline_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = CalendarDeadline.SelectedDate.ToShortDateString();
        }

        protected void rblPrivacy_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblPrivacy.SelectedValue == "0")
            {
                txtArtisan.Visible = true;
            }
            else
            {
                txtArtisan.Visible = false;
            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {                                
                SqlConnection newProj = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                newProj.Open();

                //Generate a Unique ProjectID
                SqlCommand lastProjectID = new SqlCommand("SELECT COALESCE(MAX(ProjectID), 0) FROM Projects", newProj);
                int ProjectID = (int)lastProjectID.ExecuteScalar() + 1;

                string Username = Request.Cookies["Users"]["Username"];
                SqlCommand UserID = new SqlCommand("SELECT UserID FROM Users WHERE Username = @Username", newProj);
                UserID.Parameters.AddWithValue("@Username", Username);
                int CustomerID = (int)UserID.ExecuteScalar();

                string insertQuery = "INSERT INTO [Projects](ProjectID, CustomerID, ProjectName, ProjectDescription, Deadline, GuildTypeID, MaterialsTypeID, TypeID, PublicProject, MaxPrice, MinPrice, PrivateArtisan)" +
                "VALUES (@ProjectID, @CustomerID, @txtProjectName, @txtDescription, @txtDeadline, @ddlGuild, @ddlMaterial, @ddlType, @rblPrivacy, @txtMax, @txtMin, @PrivateArtisan)";

                SqlCommand fillNewProj = new SqlCommand(insertQuery, newProj);
                fillNewProj.Parameters.AddWithValue("@ProjectID", ProjectID);
                fillNewProj.Parameters.AddWithValue("@CustomerID", CustomerID);
                fillNewProj.Parameters.AddWithValue("@txtProjectName", txtProjectName.Text);
                fillNewProj.Parameters.AddWithValue("@txtDescription", txtDescription.Text);
                fillNewProj.Parameters.AddWithValue("@txtDeadline", CalendarDeadline.SelectedDate);
                fillNewProj.Parameters.AddWithValue("@ddlGuild", ddlGuild.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@ddlMaterial", ddlMaterial.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@ddlType", ddlType.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@rblPrivacy", rblPrivacy.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@txtMax", decimal.Parse(txtMax.Text));
                fillNewProj.Parameters.AddWithValue("@txtMin", decimal.Parse(txtMin.Text));
                if (txtArtisan.Text != string.Empty)
                {
                    SqlCommand getArtisanID = new SqlCommand("SELECT UserID FROM [Users] WHERE Username = @ArtisanName", newProj);
                    getArtisanID.Parameters.AddWithValue("@ArtisanName", txtArtisan.Text.Trim());
                    int PrivateArtisanID = (int)getArtisanID.ExecuteScalar();
                    fillNewProj.Parameters.AddWithValue("@PrivateArtisan", PrivateArtisanID);
                }
                else
                {
                    fillNewProj.Parameters.AddWithValue("@PrivateArtisan", DBNull.Value);
                }
                fillNewProj.ExecuteNonQuery();
                newProj.Close();

                string saveDir = @"Images\";
                string appPath = Request.PhysicalApplicationPath;
                string savePath, dbPath;
                if (fuImageUpload.HasFile)
                {
                    if (checkFileType(fuImageUpload.FileName) == true)
                    {
                        savePath = appPath + saveDir + fuImageUpload.FileName;
                        fuImageUpload.SaveAs(savePath);
                        dbPath = @"\images\" + fuImageUpload.FileName;

                        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            //Insert dbPath into database
                            string query = "INSERT INTO ProjectPhotos(ProjectID, PhotoLocation)" +
                                " VALUES (@ProjectID, @PhotoLocation)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                                cmd.Parameters.AddWithValue("@PhotoLocation", dbPath);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        lblUploadStatus.ForeColor = System.Drawing.Color.Green;
                        lblUploadStatus.Text = "Upload status: File uploaded!";
                    }
                    else
                        lblUploadStatus.Text = "Upload status: Only image files are accepted!";
                }
                else
                {
                    lblUploadStatus.Text = "Upload status: No File Selected.";
                    dbPath = @"\images\default-no-image.png";

                    string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        string query = "INSERT INTO ProjectPhotos(ProjectID, PhotoLocation)" +
                                       " VALUES (@ProjectID, @PhotoLocation)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                            cmd.Parameters.AddWithValue("@PhotoLocation", dbPath);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                lblUploadStatus.ForeColor = System.Drawing.Color.Green;
                lblUploadStatus.Text = "Project Upload Successful.";
                Response.Redirect("MyProjects.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

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