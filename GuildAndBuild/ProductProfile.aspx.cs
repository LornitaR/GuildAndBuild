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
    public partial class ProductProfile : System.Web.UI.Page
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
                quanBox.Text = "1";
                ReviewTextBox.Visible = false;
                SubReview.Visible = false;
                ClearAll.Visible = false;

                if (iD != null) 
                {
                    if (validQueryString(iD))   //is an actual product.
                    {
                        ProductID = iD;
                        ViewState["ProductID"] = ProductID; //ProductID saved.
                        buyBtn.Enabled = enoughQuantity(Convert.ToInt32(ProductID));   //Product is in stock.
                            if (Request.Cookies["Users"] != null)   //user is logged in.
                            {
                                if(Request.Cookies["Users"]["UserEmail"] != null)
                                {
                                    FavouriteBtn.Visible = true;
                                    UserID = emailToUserID(Request.Cookies["Users"]["UserEmail"]);
                                    ViewState["UserID"] = UserID;
                                    reviewBtn.Visible = haveBought(UserID);
                                    FavouriteBtn.Visible = true;
                                    FavouriteBtn.Enabled = showFavourite(UserID, ProductID);
                                }
                            }

                            if (notArtisan())   //So long as they aren't the artisan they can take advantage of buying products etc.
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

                                    SqlCommand priceCommand = new SqlCommand(Cost, aSqlConnection);     //Add to labels the various attributes of the product.
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
                                    type.Text = NullToString(typeCommand.ExecuteScalar());                                  //All queries are placed in labels, literals etc.
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

                                    switch (rating) //The rating is averaged and the stars are displayed.
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
                                else Response.Redirect("ArtisanViewProduct.aspx?id=" + ProductID);
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

        private string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

        protected void buyBtn_Click(object sender, EventArgs e) //Buy button clicked, if logged in, the product is added to cart and the user is redirected. Else, they're asked to log in.
        {
            if (Request.Cookies["Users"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string ProductID = ViewState["ProductID"].ToString();
                string CustomerID = ViewState["UserID"].ToString();

                ShoppingCartItems.Instance.AddItem(Convert.ToInt32(ProductID));
                int quantity = 0;
                bool quantityBool = int.TryParse(quanBox.Text, out quantity);
                if(quantityBool)
                {
                    ShoppingCartItems.Instance.SetItemQuantity(Convert.ToInt32(ProductID), quantity);   //add to cart.
                }

                Response.Redirect("MyCart.aspx");

            }
        }

        protected void FavouriteBtn_Click(object sender, EventArgs e)   //If user is logged in, they can favourite. So long as they aren't the product creator.
        {
            try
            {
                SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
                aSqlConnection.Open();
                ProductID = ViewState["ProductID"].ToString();
                UserID = ViewState["UserID"].ToString();
                string FavID = "INSERT INTO [Favourites](ProductID, UserID) Values (@ProductIDPar, @UserIDPar)";
                SqlCommand FavCommand = new SqlCommand(FavID, aSqlConnection);
                FavCommand.Parameters.AddWithValue("ProductIDPar", ProductID);
                FavCommand.Parameters.AddWithValue("UserIDPar", UserID);
                FavCommand.ExecuteNonQuery();
                FavouriteBtn.Enabled = false;
                aSqlConnection.Close();
                Response.Redirect("ProductProfile.aspx?id=" + ProductID);
            }
            catch (SqlException)
            { }
        }

        protected void reviewBtn_Click(object sender, EventArgs e) //"Review Me" button is clicked.
        {
            if (ReviewDetail.Visible) //If the grid for all reviews in shown, then check if the user has already reviewed.
            {
                string emailAddress = Request.Cookies["Users"]["UserEmail"];
                
                SqlConnection aSqlConnection = new SqlConnection(aConnectionString);

                aSqlConnection.Open();


                UserID = ViewState["UserID"].ToString();
                ProductID = ViewState["ProductID"].ToString();

                string countQuery = "SELECT Count(ReviewID) FROM Reviews WHERE CustomerID = @UserIDPar AND ProductID = @ProductIDPar";
                SqlCommand countCommand = new SqlCommand(countQuery, aSqlConnection);
                countCommand.Parameters.AddWithValue("UserIDPar", UserID);
                countCommand.Parameters.AddWithValue("ProductIDPar", ProductID);
                if (NullToString(countCommand.ExecuteScalar()) == "1") //If they have reviewed, hide the grid with all reviews and show there review text and title.
                {
                    string displayTextQuery = "SELECT ReviewText FROM Reviews WHERE CustomerID = @UserIDPar AND ProductID = @ProductIDPar";
                    string displayTitleQuery = "SELECT ReviewTitle FROM Reviews WHERE CustomerID = @UserIDPar AND ProductID = @ProductIDPar";
                    string ratingQuery = "SELECT OverallRating FROM Reviews WHERE CustomerID = @UserIDPar AND ProductID = @ProductIDPar";

                    SqlCommand ratingCommand = new SqlCommand(ratingQuery, aSqlConnection);
                    SqlCommand displayTextCommand = new SqlCommand(displayTextQuery, aSqlConnection);
                    SqlCommand displayTitleCommand = new SqlCommand(displayTitleQuery, aSqlConnection);
                    displayTextCommand.Parameters.AddWithValue("UserIDPar", UserID);
                    displayTextCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                    displayTitleCommand.Parameters.AddWithValue("UserIDPar", UserID);
                    displayTitleCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                    ratingCommand.Parameters.AddWithValue("UserIDPar", UserID);
                    ratingCommand.Parameters.AddWithValue("ProductIDPar", ProductID);

                    ReviewDelete.Visible = true;
                    editReview.Visible = true;
                    TitleLabel.Visible = true;
                    TextLabel.Visible = true;
                    TitleTextBox.Visible = false;
                    RatingLabel.Visible = false;
                    RatingDDL.Visible = false;
                    ReviewTextBox.Visible = false;
                    SubReview.Visible = false;
                    ClearAll.Visible = false;
                    ReviewDetail.Visible = false;
                    reviewBtn.Text = "Hide Your Review";
                    TitleLabel.Text = NullToString(displayTitleCommand.ExecuteScalar());
                    TextLabel.Text = NullToString(displayTextCommand.ExecuteScalar());
                    ViewState["Title"] = TitleLabel.Text;   //save text and title of the review, incase they want to edit there review.
                    ViewState["Text"] = TextLabel.Text;
                    ViewState["Rating"] = NullToString(ratingCommand.ExecuteScalar());
                }


                else
                {
                    TitleLabel.Visible = false;
                    TextLabel.Visible = false;
                    ReviewDelete.Visible = false;
                    editReview.Visible = false;
                    TitleTextBox.Text = "Enter a Title Here";
                    RatingLabel.Visible = true;
                    RatingDDL.Visible = true;
                    TitleTextBox.Visible = true;
                    ReviewTextBox.Visible = true;
                    SubReview.Visible = true;
                    ClearAll.Visible = true;
                    ReviewDetail.Visible = false;
                    reviewBtn.Text = "Exit Review";
                }
                aSqlConnection.Close();
            }
            else //Close the review (textarea, etc) option and the display the review grid.
            {
                TitleLabel.Visible = false;
                TextLabel.Visible = false;
                ReviewDelete.Visible = false;
                editReview.Visible = false;
                TitleTextBox.Visible = false;
                RatingLabel.Visible = false;
                RatingDDL.Visible = false;
                reviewBtn.Text = "Review Me";
                ReviewTextBox.Visible = false;
                SubReview.Visible = false;
                ClearAll.Visible = false;
                ReviewDetail.Visible = true;
            }
            
        }

        protected void ClearAll_Click(object sender, EventArgs e)
        {
            ReviewTextBox.InnerText = "";
        }

        protected void SubReview_Click(object sender, EventArgs e)
        {
            TitleLabel.Visible = false;
            if(!(ReviewTextBox.InnerText.Length == 0 || TitleTextBox.Text.Length == 0))
            {
                SqlConnection aSqlConnection = new SqlConnection(aConnectionString);    //submit the review.

                string inputText = ReviewTextBox.InnerText;
                string inputTitle = TitleTextBox.Text;
                inputText = Server.HtmlEncode(inputText);
                inputTitle = Server.HtmlEncode(inputTitle);

                aSqlConnection.Open();

                ProductID = ViewState["ProductID"].ToString();
                UserID = ViewState["UserID"].ToString();

                SqlCommand PurchaseIDCommand = new SqlCommand("SELECT PurchaseID FROM Purchases WHERE CustomerID = " + UserID + " AND ProductID = " + ProductID, aSqlConnection);
                string PurchaseID = NullToString(PurchaseIDCommand.ExecuteScalar());
                SqlCommand ReviewIDCommand = new SqlCommand("Select COALESCE(MAX(ReviewID), 0) From Reviews", aSqlConnection);
                int ReviewID = Int32.Parse(NullToString(ReviewIDCommand.ExecuteScalar()));
                ReviewID++;
                try{

                    string query = "INSERT INTO [Reviews](PurchaseId, ReviewID, ProductID, CustomerID, ReviewDate, Reviewtitle, ReviewText, OverallRating) Values(@PurchaseIDPar, @ReviewIDPar, @ProductIDPar, @CustomerIDPar, @ReviewDatePar, @ReviewTitlePar, @ReviewTextPar, @OverallRatingPar)";
                    SqlCommand reviewCommand = new SqlCommand(query, aSqlConnection);
                    reviewCommand.Parameters.AddWithValue("PurchaseIDPar", PurchaseID);
                    reviewCommand.Parameters.AddWithValue("ReviewIDPar", ReviewID);
                    reviewCommand.Parameters.AddWithValue("ProductIDPar", ProductID);
                    reviewCommand.Parameters.AddWithValue("CustomerIDPar", UserID);
                    reviewCommand.Parameters.AddWithValue("ReviewDatePar", DateTime.Now);
                    reviewCommand.Parameters.AddWithValue("ReviewTitlePar", inputTitle);
                    reviewCommand.Parameters.AddWithValue("ReviewTextPar", inputText);
                    reviewCommand.Parameters.AddWithValue("OverallRatingPar", RatingDDL.SelectedValue);

                    reviewCommand.ExecuteNonQuery();
                }
                catch(SqlException) {}
                aSqlConnection.Close();

                Response.Redirect("ProductProfile.aspx?id=" + ProductID);

            }
            else
            {
                TitleLabel.Visible = true;
                TitleLabel.Text = "Please Enter a Title/Text.";
            }

        }


        private bool haveBought(string UserID)          //checks if they've bought the item. They can only review an item they've bought.
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            string productID = Request.QueryString["id"];
            string haveBoughtQuery = "SELECT Count(PurchaseID) FROM Purchases WHERE CustomerID = @UserIDPar AND ProductID = @ProductIDPar";
            SqlCommand haveBoughtCommand = new SqlCommand(haveBoughtQuery, aSqlConnection);
            haveBoughtCommand.Parameters.AddWithValue("UserIDPar", UserID);
            haveBoughtCommand.Parameters.AddWithValue("ProductIDPar", productID);
            aSqlConnection.Open();
            string haveBoughtCount = NullToString(haveBoughtCommand.ExecuteScalar());
            aSqlConnection.Close();
            return haveBoughtCount != "0";
        }

        private bool validQueryString(string iD)    //that the querystring is a valid querystring.
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

        private string emailToUserID(string email)  //Originally the cookie contained the user's email, but that was updated much later, I've just continued with this method.
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string UserID = "";
            string emailAddressQuery = "SELECT UserID FROM Users WHERE UserEmail = @EmailPar";
            SqlCommand emailAddressCommand = new SqlCommand(emailAddressQuery, aSqlConnection);
            emailAddressCommand.Parameters.AddWithValue("EmailPar", email);
            UserID = NullToString(emailAddressCommand.ExecuteScalar());
            aSqlConnection.Close();
            return UserID;
        }

        private bool showFavourite(string UserID, string ProductID) //Show favourite if they haven't already "favourited".
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string FavouriteCount = "SELECT Count(UserID) FROM Favourites WHERE UserID = @UserIDPar AND ProductID = @ProductIDPar";
            SqlCommand FavouriteCountCommand = new SqlCommand(FavouriteCount, aSqlConnection);
            FavouriteCountCommand.Parameters.AddWithValue("UserIDPar", UserID);
            FavouriteCountCommand.Parameters.AddWithValue("ProductIDPar", ProductID);
            string count = NullToString(FavouriteCountCommand.ExecuteScalar());
            aSqlConnection.Close();
            if (count == "0")
            {
                return true;
            }
            else
                return false;
        }

        protected void cartBtn_Click(object sender, EventArgs e)    //add to cart function
        {
            ProductID = ViewState["ProductID"].ToString();

            ShoppingCartItems.Instance.AddItem(Convert.ToInt32(ProductID));
            int quantity = 0;
            bool quantityBool = int.TryParse(quanBox.Text, out quantity);
            if(quantityBool)
            {
                ShoppingCartItems.Instance.SetItemQuantity(Convert.ToInt32(ProductID), quantity);
            }
        }

        private bool notArtisan()   //isn't the artisan. Cannot buy own products.
        {
            if (ViewState["UserID"] == null) return true;

            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string artisanID = "SELECT ArtisanID FROM Products WHERE ProductID = " + ViewState["ProductID"].ToString();
            SqlCommand artisanIDCommand = new SqlCommand(artisanID, aSqlConnection);
            if(ViewState["UserID"].ToString() == artisanIDCommand.ExecuteScalar().ToString()) return false;
            else return true;
        }

        private bool enoughQuantity(int ProductID)  //enough of quantity of a product.
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string quantity = "SELECT Quantity FROM Products WHERE ProductID = " + ProductID;
            SqlCommand quantityCommand = new SqlCommand(quantity, aSqlConnection);
            int DBQuantity = Convert.ToInt32(quantityCommand.ExecuteScalar());
            aSqlConnection.Close();
            if (DBQuantity > 0) return true;
            else return false;
        }

        protected void ReviewDelete_Click(object sender, EventArgs e)
        {
            deleteReview(); //delete review and refresh page.
            Response.Redirect("ProductProfile.aspx?id=" + ViewState["ProductID"].ToString());
        }

        protected void editReview_Click(object sender, EventArgs e)
        {   //Edit review option.
            TitleLabel.Visible = false;
            TextLabel.Visible = true;

            TextLabel.Text = "Your Review has been deleted. End edit by clicking submit.";

            ReviewDelete.Visible = false;
            editReview.Visible = false;
            reviewBtn.Visible = false;
            TitleTextBox.Visible = true;
            ReviewTextBox.Visible = true;

            TitleTextBox.Text = Server.HtmlDecode(ViewState["Title"].ToString());
            ReviewTextBox.InnerText = Server.HtmlDecode(ViewState["Text"].ToString());
            RatingDDL.SelectedValue = ViewState["Rating"].ToString();

            deleteReview();
            ReviewDetail.Visible = false;
            RatingLabel.Visible = true;
            RatingDDL.Visible = true;
            SubReview.Visible = true;
            ClearAll.Visible = true;
        }

        private void deleteReview() //delete review function.
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string findPurchaseID = "SELECT PurchaseID FROM Purchases WHERE CustomerID = " + ViewState["UserID"].ToString() + " AND ProductID = " + ViewState["ProductID"].ToString();
            SqlCommand purchaseIDCommand = new SqlCommand(findPurchaseID, aSqlConnection);
            string purchaseID = purchaseIDCommand.ExecuteScalar().ToString();
            purchaseID = "DELETE FROM Reviews WHERE PurchaseID = " + purchaseID + " AND CustomerID = " + ViewState["UserID"].ToString() + " AND ProductID = " + ViewState["ProductID"].ToString();
            purchaseIDCommand.CommandText = purchaseID;
            purchaseIDCommand.ExecuteNonQuery();
            aSqlConnection.Close();
        }


    }
}