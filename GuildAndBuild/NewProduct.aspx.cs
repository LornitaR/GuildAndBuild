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
    public partial class NewProduct : System.Web.UI.Page
    {
        String aConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection aConnection = new SqlConnection();
                SqlCommand CommandA = new SqlCommand();
                SqlDataReader DataReaderA;
                aConnection.ConnectionString = aConnectionString; 
                CommandA = new SqlCommand("SELECT GuildTypeName, GuildTypeID FROM GuildTypes", aConnection);
                aConnection.Open();
                DataReaderA = CommandA.ExecuteReader();

                ddlGuild.DataSource = DataReaderA;
                ddlGuild.DataTextField = "GuildTypeName";
                ddlGuild.DataValueField = "GuildTypeID";
                ddlGuild.DataBind();
                aConnection.Close();

                //Load Artisan's guild into dropdown
                SqlConnection conn = new SqlConnection();
                SqlCommand getID = new SqlCommand("SELECT Interests FROM Users WHERE Username = @Username", conn);
                conn.ConnectionString = aConnectionString; 
                conn.Open();
                getID.Parameters.AddWithValue("@Username", Request.Cookies["Users"]["Username"]);
                string GuildID = getID.ExecuteScalar().ToString();
                ddlGuild.SelectedValue = GuildID;
                conn.Close();

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection newProd = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                newProd.Open();

                //Generate a Unique ProductID
                SqlCommand lastProductID = new SqlCommand("SELECT COALESCE(MAX(productID), 0) FROM Products", newProd);
                int productID = (int)lastProductID.ExecuteScalar() + 1;

                string requestUserID = Request.Cookies["Users"]["Username"];
                SqlCommand UserID = new SqlCommand("SELECT UserID FROM Users WHERE Username = @Username", newProd);
                UserID.Parameters.AddWithValue("@Username", requestUserID);
                int ArtisanID = (int)UserID.ExecuteScalar();

                string insertQuery = "INSERT INTO [Products](ProductID, ArtisanID, ProductName, Guild, Materials, ProductType, ProductCost, ProductDescription, Quantity, DateAdded)" +
                    "VALUES (@ProductID, @ArtisanID, @txtProductName, @ddlGuild, @ddlMaterial, @ddlType, @txtCost, @txtDescription, @txtQuantity, @dateAdded)";

                SqlCommand fillNewProj = new SqlCommand(insertQuery, newProd);
                fillNewProj.Parameters.AddWithValue("@ProductID", productID);
                fillNewProj.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                fillNewProj.Parameters.AddWithValue("@txtProductName", txtProductName.Text);
                fillNewProj.Parameters.AddWithValue("@ddlGuild", ddlGuild.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@ddlMaterial", ddlMaterial.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@ddlType", ddlType.SelectedValue);
                fillNewProj.Parameters.AddWithValue("@txtCost", decimal.Parse(txtCost.Text));
                fillNewProj.Parameters.AddWithValue("@txtDescription", txtDescription.Text);
                fillNewProj.Parameters.AddWithValue("@txtQuantity", Int32.Parse(txtQuantity.Text));
                fillNewProj.Parameters.AddWithValue("@dateAdded", DateTime.Now);

                fillNewProj.ExecuteNonQuery();
                newProd.Close();
                
                string saveDir = @"Images\";
                string appPath = Request.PhysicalApplicationPath;
                string savePath, dbPath;
                if (fuImageUpload.HasFile)
                {
                    if (checkFileType(fuImageUpload.FileName) == true)
                    {
                        savePath = appPath + saveDir + fuImageUpload.FileName;
                        dbPath = @"\images\" + fuImageUpload.FileName;
                        fuImageUpload.SaveAs(savePath);

                        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            //Insert savePath into database
                            string query = "INSERT INTO ProductPhotos(ProductID, PhotoLocation)" +
                                " VALUES (@ProductID, @PhotoLocation)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@ProductID", productID);
                                cmd.Parameters.AddWithValue("@PhotoLocation", dbPath);
                                lblUploadStatus.ForeColor = System.Drawing.Color.Green;
                                lblUploadStatus.Text = "Upload status: File uploaded!";
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
                        string query = "INSERT INTO ProductPhotos(ProductID, PhotoLocation)" +
                                       " VALUES (@ProductID, @PhotoLocation)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@ProductID", productID);
                            cmd.Parameters.AddWithValue("@PhotoLocation", dbPath);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                lblStatus.Text = "Successfully uploaded!";
                Response.Redirect("ProductProfile.aspx?id=" + productID);
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
    