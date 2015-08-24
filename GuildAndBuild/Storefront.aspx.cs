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
    public partial class Storefront : System.Web.UI.Page
    {

        private string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";


        public int CurrentPage
        {

            get
            {
                if (this.ViewState["CurrentPage"] == null)
                    return 0;
                else
                    return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
            }

            set
            {
                this.ViewState["CurrentPage"] = value;
            }

        }


        PagedDataSource aPagedDataSource = new PagedDataSource();









        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid() 
        { 
            string iD = Request.QueryString["id"];
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();

            if(Request.Cookies["Users"] != null)
            {
                ViewState["UserID"] = emailToUserID(Request.Cookies["Users"]["UserEmail"].ToString());
            }

            if (iD != null)
            {
                if (validQueryString(iD))   //is an actual product.
                {
                    ViewState["ProductID"] = iD;
                    if (notArtisan())   //So long as they aren't the artisan they can take advantage of buying products etc.
                    {
                        string storefront = "SELECT Products.productID, Products.ProductName, Products.ProductCost, Products.ProductDescription, Products.DateAdded, ProductPhotos.PhotoLocation FROM ProductPhotos INNER JOIN Products ON ProductPhotos.ProductID = Products.productID WHERE (Products.ArtisanID = (SELECT ArtisanID FROM Products AS Products_1 WHERE (productID = @ProductIDPar)))";



                        SqlCommand storefrontCommand = new SqlCommand(storefront, aSqlConnection);
                        storefrontCommand.Parameters.AddWithValue("@ProductIDPar", Convert.ToInt32(iD));
                        SqlDataAdapter aDataAdapter = new SqlDataAdapter();
                        aDataAdapter.SelectCommand = storefrontCommand;

                        DataTable aDataTable = new DataTable();
                        aDataAdapter.Fill(aDataTable);

                        aPagedDataSource.DataSource = aDataTable.DefaultView;   //Setting up the PagedDataSource object with attributes.
                        aPagedDataSource.AllowPaging = true;
                        aPagedDataSource.PageSize = 3;
                        aPagedDataSource.CurrentPageIndex = CurrentPage;
                        linkBtnNext.Enabled = !aPagedDataSource.IsLastPage;
                        linkBtnPrevious.Enabled = !aPagedDataSource.IsFirstPage;

                        productList.DataSource = aPagedDataSource;
                        productList.DataBind();


                        string shopName = "SELECT ShopName FROM Artisans JOIN Products ON Artisans.ArtisanID = Products.ArtisanID WHERE Products.ProductID = @ProductIDPar";
                        SqlCommand shopNameCommand = new SqlCommand(shopName, aSqlConnection);
                        shopNameCommand.Parameters.AddWithValue("ProductIDPar", Convert.ToInt32(iD));
                        //Add to labels the various attributes of the storefront.
                        string shopDescription = "SELECT ArtisanDescription FROM Artisans JOIN Products ON Artisans.ArtisanID = Products.ArtisanID WHERE Products.ProductID = @ProductIDPar";
                        SqlCommand shopDescriptionCommand = new SqlCommand(shopDescription, aSqlConnection);
                        shopDescriptionCommand.Parameters.AddWithValue("ProductIDPar", Convert.ToInt32(iD));

                        string guildName = "SELECT GuildTypes.GuildTypeName FROM GuildTypes JOIN Products ON GuildTypes.GuildTypeID = Products.Guild WHERE Products.productID = @ProductIDPar";
                        SqlCommand guildCommand = new SqlCommand(guildName, aSqlConnection);
                        guildCommand.Parameters.AddWithValue("ProductIDPar", Convert.ToInt32(iD));

                        string shopPhoto = "SELECT Users.ProfilePhoto FROM Artisans INNER JOIN Users ON Artisans.ArtisanID = Users.UserID WHERE (Artisans.ArtisanID IN (SELECT ArtisanID FROM Products WHERE (productID = @ProductIDPar)))";
                        SqlCommand shopPhotoCommand = new SqlCommand(shopPhoto, aSqlConnection);
                        shopPhotoCommand.Parameters.AddWithValue("ProductIDPar", Convert.ToInt32(iD));


                        string Location = "SELECT Location FROM Products JOIN Users ON Users.UserID = Products.ArtisanID WHERE Products.ProductID = @ProductIDPar";
                        SqlCommand LocationCommand = new SqlCommand(Location, aSqlConnection);
                        LocationCommand.Parameters.AddWithValue("ProductIDPar", Convert.ToInt32(iD));

                        switch(NullToString(LocationCommand.ExecuteScalar()))
                        {
                            case "1": Locationlbl.Text = "Location: Munster"; break;
                            case "2": Locationlbl.Text = "Location: Connaught"; break;
                            case "3": Locationlbl.Text = "Location: Ulster"; break;
                            case "4": Locationlbl.Text = "Location: Leinster"; break;
                        }

                        guildType.Text = "Guild Type: " + NullToString(guildCommand.ExecuteScalar());
                        Descriptionlbl.Text = NullToString(shopDescriptionCommand.ExecuteScalar());
                        ShopNameLbl.Text = NullToString(shopNameCommand.ExecuteScalar());
                        ProfileImage.Attributes["src"] = ResolveUrl(NullToString(shopPhotoCommand.ExecuteScalar()));
                    }
                    else
                    {
                        if(Request.UrlReferrer == null)
                        {
                            Response.Redirect("Default.aspx");
                        }
                        else Response.Redirect("ProfileCustomer.aspx");
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
                if(Request.UrlReferrer == null)
                        {
                            Response.Redirect("Default.aspx");
                        }
                        else Response.Redirect(Request.UrlReferrer.ToString());
            }
        }

        protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("lnkbtnPaging"))   //Listview is saved in viewstate as a PagedDataSource, this functiion pages the list.
            {
                CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
                BindGrid();
            }
        }

        protected void linkBtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindGrid();
        }

        protected void linkBtnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindGrid();
        }

        protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
            if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkbtnPage.Enabled = false;
                lnkbtnPage.Font.Bold = true;
            }
        }

        private string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

        protected void showReviews_Click(object sender, EventArgs e)
        {
            if(productList.Visible)
            {
                reviewGridview.Visible = true;
                productList.Visible = false;
                linkBtnNext.Visible = false;
                linkBtnPrevious.Visible = false;
                showReviews.Text = "Show Products";
            }
            else
            {
                linkBtnNext.Visible = true;
                linkBtnPrevious.Visible = true;
                reviewGridview.Visible = false;
                productList.Visible = true;
                showReviews.Text = "Show Reviews";
            }
           
        }

        protected void buyBtn_Click(object sender, EventArgs e)
        {

        }

        protected void productList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "Buy")
            {

                if (Request.Cookies["Users"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    string CustomerID = emailToUserID(Request.Cookies["Users"]["UserEmail"].ToString());    //adds item to list.
                    ViewState["UserID"] = CustomerID;

                    ShoppingCartItems.Instance.AddItem(Convert.ToInt32(e.CommandArgument));

                    Response.Redirect("MyCart.aspx");
                }
            }
            else if(e.CommandName == "AddToCart")
            {
                string ProductID = e.CommandArgument.ToString();
                ShoppingCartItems.Instance.AddItem(Convert.ToInt32(ProductID));
            }
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
            UserID = Convert.ToString(emailAddressCommand.ExecuteScalar());
            aSqlConnection.Close();
            return UserID;
        }

        protected void contactBtn_Click(object sender, EventArgs e) //redirects to inbox if logged in and the sender's name is in the sender textbox
        {
            if(Request.Cookies["Users"] != null)
            {
                if(Request.Cookies["Users"]["UserEmail"] != null)
                {
                    Response.Redirect("Inbox.aspx?sender=" + artisanUsername());
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private bool notArtisan()   //isn't the artisan. Cannot buy own products.
        {
            if (ViewState["UserID"] == null) return true;

            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string artisanID = "SELECT ArtisanID FROM Products WHERE ProductID = " + ViewState["ProductID"].ToString();
            SqlCommand artisanIDCommand = new SqlCommand(artisanID, aSqlConnection);
            if (ViewState["UserID"].ToString() == artisanIDCommand.ExecuteScalar().ToString()) return false;
            else return true;
        }

        private bool enoughQuantity(int ProductID)  //enough of quantity of a product.
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string quantity = "SELECT Quantity FROM Products WHERE ProductID = " + ProductID;
            SqlCommand quantityCommand = new SqlCommand(quantity, aSqlConnection);
            ViewState["Quantity"] = Convert.ToInt32(quantityCommand.ExecuteScalar());
            aSqlConnection.Close();
            if (Convert.ToInt32(ViewState["Quantity"]) > 0) return true;
            else return false;
        }

        protected void productList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) //checks if each item in list is in stock. Allows buy button if in stock.
            {
                Button BT = e.Item.FindControl("buyBtn") as Button;
                Label lbl = e.Item.FindControl("ProductID") as Label;
                BT.Enabled = enoughQuantity(Convert.ToInt32(lbl.Text));

                   
            }
        }

        protected void proposeBtn_Click(object sender, EventArgs e)
        {
            if(Request.Cookies["Users"] != null)
            {
                if(Request.Cookies["Users"]["UserEmail"] != null)
                {

                    Response.Redirect("NewProject.aspx?set=" + artisanUsername());
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private string artisanUsername()    //finds artisan username to add to querystring when redirecting to the inbox page.
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string artisanUserName = "SELECT Username FROM Users JOIN Products ON Users.UserID = Products.ArtisanID WHERE Products.ProductID = @ProductIDPar";
            SqlCommand artisanIDCommand = new SqlCommand(artisanUserName, aSqlConnection);
            artisanIDCommand.Parameters.AddWithValue("ProductIDPar", ViewState["ProductID"].ToString());
            return artisanIDCommand.ExecuteScalar().ToString();
        }
    }
}