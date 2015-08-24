using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GuildAndBuild
{
    public partial class ArtisanViewProduct : System.Web.UI.Page
    {
        public string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
        public string UserID;
        public string ProductID;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            string iD = Request.QueryString["id"];


            
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();

            if (!IsPostBack)
            {
                if (iD != null)
                {
                    if (validQueryString(iD))
                    {
                        ProductID = iD;
                        ViewState["ProductID"] = ProductID;
                            if (Request.Cookies["Users"] != null)
                            {
                                if(Request.Cookies["Users"]["UserEmail"] != null)
                                {
                                    UserID = emailToUserID(Request.Cookies["Users"]["UserEmail"]);
                                    ViewState["UserID"] = UserID;
                                }
                            }

                            if (!notArtisan())
                            {


                                try
                                {
                                    string NameComm = "SELECT ProductName FROM Products WHERE productID = @ProductIDPar";
                                    string Cost = "SELECT ProductCost FROM Products WHERE productID = @ProductIDPar";
                                    string Description = "SELECT ProductDescription FROM Products WHERE productID = @ProductIDPar";
                                    string Photo = "SELECT PhotoLocation FROM ProductPhotos WHERE ProductPhotos.productID = @ProductIDPar";
                                    string quantity = "SELECT Quantity FROM Products WHERE productID = @ProductIDPar";


                                    SqlCommand nameCommand = new SqlCommand(NameComm, aSqlConnection);
                                    nameCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    SqlCommand priceCommand = new SqlCommand(Cost, aSqlConnection);
                                    priceCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    SqlCommand descriptionCommand = new SqlCommand(Description, aSqlConnection);
                                    descriptionCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    SqlCommand PhotoCommand = new SqlCommand(Photo, aSqlConnection);
                                    PhotoCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    SqlCommand quantityCommand = new SqlCommand(quantity, aSqlConnection);
                                    quantityCommand.Parameters.AddWithValue("ProductIDPar", ProductID);


                                    string guildName = "SELECT GuildTypes.GuildTypeName FROM GuildTypes JOIN Products ON GuildTypes.GuildTypeID = Products.Guild WHERE Products.productID = @ProductIDPar";
                                    SqlCommand guildCommand = new SqlCommand(guildName, aSqlConnection);
                                    guildCommand.Parameters.AddWithValue("ProductIDPar", ProductID);


                                    string materialName = "SELECT MaterialTypes.MaterialTypeName FROM MaterialTypes JOIN Products ON MaterialTypes.MaterialTypeID = Products.Materials WHERE Products.productID = @ProductIDPar";
                                    SqlCommand materialCommand = new SqlCommand(materialName, aSqlConnection);
                                    materialCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    string typeName = "SELECT Types.TypeName FROM Types JOIN Products ON Types.TypeID = Products.ProductType WHERE Products.productID = @ProductIDPar";
                                    SqlCommand typeCommand = new SqlCommand(typeName, aSqlConnection);
                                    typeCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    string favCount = "SELECT COUNT(UserID) FROM Favourites WHERE ProductID = @ProductIDPar";
                                    SqlCommand favCountCommand = new SqlCommand(favCount, aSqlConnection);
                                    favCountCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    string revCount = "SELECT COUNT(ReviewID) FROM Reviews WHERE ProductID = @ProductIDPar";
                                    SqlCommand revCountCommand = new SqlCommand(revCount, aSqlConnection);
                                    revCountCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    string shopName = "SELECT ShopName FROM Artisans JOIN Products ON Artisans.ArtisanID = Products.ArtisanID WHERE Products.ProductID = @ProductIDPar";
                                    SqlCommand shopNameCommand = new SqlCommand(shopName, aSqlConnection);
                                    shopNameCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                                    string avgRating = "SELECT AVG(OverallRating) FROM Reviews WHERE Reviews.ProductID = @ProductIDPar";
                                    SqlCommand avgRatingCommand = new SqlCommand(avgRating, aSqlConnection);
                                    avgRatingCommand.Parameters.AddWithValue("ProductIDPar", ProductID);


                                    guild.Text = NullToString(guildCommand.ExecuteScalar());
                                    material.Text = NullToString(materialCommand.ExecuteScalar());
                                    type.Text = NullToString(typeCommand.ExecuteScalar());
                                    NoFavs.Text = "No. of Favourites: " + NullToString(favCountCommand.ExecuteScalar());

                                    ShopLink.NavigateUrl = "Storefront.aspx?id=" + ProductID;
                                    ShopLink.Text = NullToString(shopNameCommand.ExecuteScalar());

                                    reviewCount.Text = NullToString(revCountCommand.ExecuteScalar()) + " Review(s)";
                                    productTitle.Text = NullToString(nameCommand.ExecuteScalar());
                                    productPrice.Text = "€" + NullToString(priceCommand.ExecuteScalar());
                                    productDescription.Text = NullToString(descriptionCommand.ExecuteScalar());
                                    Quantitylbl.Text = "Available Quantity: " + NullToString(quantityCommand.ExecuteScalar());
                                    ProductPhoto.Attributes["src"] = ResolveUrl(NullToString(PhotoCommand.ExecuteScalar()));

                                    starImage.Attributes["src"] = "/StarRating/0star.PNG";
                                    string rating = NullToString(avgRatingCommand.ExecuteScalar());

                                    switch (rating)
                                    {
                                        case "1": starImage.Attributes["src"] = "/StarRating/1star.PNG"; break;
                                        case "2": starImage.Attributes["src"] = "/StarRating/2star.PNG"; break;
                                        case "3": starImage.Attributes["src"] = "/StarRating/3star.PNG"; break;
                                        case "4": starImage.Attributes["src"] = "/StarRating/4star.PNG"; break;
                                        case "5": starImage.Attributes["src"] = "/StarRating/5star.PNG"; break;
                                    }

                                }
                                catch (SqlException) { }
                            }
                            else
                            {
                                if (Request.UrlReferrer == null)
                                {
                                    Response.Redirect("Default.aspx");
                                }
                                else Response.Redirect("MyProducts.aspx");
                            }

                        }

                        else
                        {
                            if (Request.UrlReferrer == null)
                            {
                                Response.Redirect("Default.aspx");
                            }
                            else Response.Redirect(Request.UrlReferrer.ToString());
                        }
                }
            else
            {
                if (Request.UrlReferrer == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else Response.Redirect(Request.UrlReferrer.ToString()); 
            }

             aSqlConnection.Close();
            }
        
        }

        private bool validQueryString(string iD)
        {
            Regex numbers2Regex = new Regex("^[0-9]+$");

            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);

            aSqlConnection.Open();
            string countOfProducts = "SELECT Count(productID) FROM Products WHERE ProductID = " + iD;
            SqlCommand countCommand = new SqlCommand(countOfProducts, aSqlConnection);
            int Number = Int32.Parse(countCommand.ExecuteScalar().ToString());
            aSqlConnection.Close();

            return numbers2Regex.IsMatch(iD) && Number != 0;
        }

        private string emailToUserID(string email)
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string UserID = "";
            string emailAddressQuery = "SELECT UserID FROM Users WHERE UserEmail = @EmailPar";
            SqlCommand emailAddressCommand = new SqlCommand(emailAddressQuery, aSqlConnection);
            emailAddressCommand.Parameters.AddWithValue("EmailPar", email);
            UserID = emailAddressCommand.ExecuteScalar().ToString();
            aSqlConnection.Close();
            return UserID;
        }

        private bool notArtisan()
        {
            if (ViewState["UserID"] == null) return true;

            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string artisanID = "SELECT ArtisanID FROM Products WHERE ProductID = " + ViewState["ProductID"].ToString();
            SqlCommand artisanIDCommand = new SqlCommand(artisanID, aSqlConnection);
            if (ViewState["UserID"].ToString() == artisanIDCommand.ExecuteScalar().ToString()) return false;
            else return true;
        }

        private string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

    }
}