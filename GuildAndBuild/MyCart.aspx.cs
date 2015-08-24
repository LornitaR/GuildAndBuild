using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace GuildAndBuild
{
    public partial class MyCart : System.Web.UI.Page
    {

        public string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";


        protected void Page_Load(object sender, EventArgs e)
        {
            buyBtn.Enabled = true;
            if(!IsPostBack)
            {
                ViewState["GoBackTo"] = Request.UrlReferrer;
                DataBinding();

            }
        }

        protected void DataBinding()
        {
            gvCart.DataSource = ShoppingCartItems.Instance.Items; //binds two carts, one for products, one for projects. Product indentifiers and project indentifiers in the database are not unique when combined. Hence two carts.  

            projectsGV.DataSource = ShoppingCartProject.Instance.Items;

            projectsGV.DataBind();

            gvCart.DataBind();

        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Remove")   //RowCommand event to remove items from products.
            {
                try
                {
                    int productID = Convert.ToInt32(e.CommandArgument);
                    ShoppingCartItems.Instance.RemoveItem(productID);
                }
                catch(FormatException)
                { }
                Response.Redirect("MyCart.aspx");
            }
        }

        protected void gvCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total " + ShoppingCartItems.Instance.getSubtotal().ToString("C");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)  //update quantity in the cart.
        {
            foreach (GridViewRow row in gvCart.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        int productID = Convert.ToInt32(gvCart.DataKeys[row.RowIndex].Value);
                        int size = 0;
                        bool quantity = int.TryParse(((TextBox)row.Cells[2].FindControl("tbQuantity")).Text, out size);
                        ShoppingCartItems.Instance.SetItemQuantity(productID, size);
                    }
                    catch (FormatException)
                    {}
                }
                DataBinding();
            }
        }

        protected void buyBtn_Click(object sender, EventArgs e) //buy selected item in cart.
        {

            if (Request.Cookies["Users"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string CustomerID = emailToUserID(Request.Cookies["Users"]["UserEmail"].ToString());
                ViewState["UserID"] = CustomerID;
                string PurchaseID;

                SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
                aSqlConnection.Open();

                double fee;
                int count = 0;
                double cost = 0.0;

                foreach(ShoppingItems item in ShoppingCartItems.Instance.Items)     //there are two carts, one for products one for projects.
                {
                    buyBtn.Enabled = enoughQuantity(item.ItemID, item.quantity);
                }

                if (buyBtn.Enabled)
                {

                    foreach(ShoppingItems item in ShoppingCartItems.Instance.Items)     //each item in the products cart is checked for quantity.
                    {
                        string quantity = "SELECT Quantity FROM Products WHERE ProductID = " + item.ItemID;
                        SqlCommand quantityCommand = new SqlCommand(quantity, aSqlConnection);
                        int DBQuantity = Convert.ToInt32(quantityCommand.ExecuteScalar());
                        int newQuantity = DBQuantity - item.quantity;

                        string alter = "UPDATE Products SET Quantity = " + newQuantity + " WHERE ProductID = " + item.ItemID;

                        SqlCommand alterTable = new SqlCommand(alter, aSqlConnection);
                        alterTable.ExecuteNonQuery();
                    }



                    foreach (ShoppingItems item in ShoppingCartItems.Instance.Items)    //add all products to purchases table
                    {
                        count += item.quantity;
                        PurchaseID = getPurchaseID();
                        fee = decimal.ToDouble(item.TotalPrice) * 0.1;
                        cost += decimal.ToDouble(item.TotalPrice);
                        string aPurchase = "INSERT INTO [Purchases](PurchaseID, CustomerID, ArtisanID, ProductID, PurchaseDate, PaymentMethod, TotalCost, TransactionFee, Quantity, PromoCode) Values(" + PurchaseID + ", " + CustomerID + ", " + item.Artisan + ", " + item.ItemID + ", " + "@DatePar" + ", 'Card'" + ", " + item.TotalPrice + ", " + fee + ", " + item.quantity + ", NULL)";
                        SqlCommand purchaseCommand = new SqlCommand(aPurchase, aSqlConnection);
                        purchaseCommand.Parameters.AddWithValue("@DatePar", DateTime.Now);
                        purchaseCommand.ExecuteNonQuery();
                    }


                    foreach (ShoppingItems item in ShoppingCartItems.Instance.Items)    //sends message to artisan about the purchase.
                    {
                        string messageContent = "I have bought one of your products! "
                            + "It's " + item.itemName + " and it costs " + item.TotalPrice + " for " + item.quantity + " of them."
                            + " Send to this address: " + getAddress() + " Cheers!";

                        string messageID = getMessageID();
                        string aMessage = "INSERT INTO [Inboxes](UserID, SenderID, MessageID, DateReceived, MessageText) Values (" + item.Artisan + ", " + CustomerID + ", " + messageID + ", " + "@DatePar," + " @messageContent" + ")";
                        SqlCommand purchaseCommand = new SqlCommand(aMessage, aSqlConnection);
                        purchaseCommand.Parameters.AddWithValue("@DatePar", DateTime.Now);
                        purchaseCommand.Parameters.AddWithValue("@MessageContent", messageContent);
                        purchaseCommand.ExecuteNonQuery();
                    }

                    //Adds the products to the purchases table and sends a message to the artisan with the user's address and the item's name, price and quantity from the user's inbox.


                    ShoppingCartItems.Instance.Items.Clear();   //clears cart.

                    //Removes all items in the cart.


                    foreach (ShoppingItems item in ShoppingCartProject.Instance.Items)  //each project is put into purchases table.
                    {
                        count++;
                        PurchaseID = getPurchaseID();
                        fee = decimal.ToDouble(item.ProjectPrice) * 0.1;
                        cost += decimal.ToDouble(item.ProjectPrice);
                        string aPurchase = "INSERT INTO [Purchases](PurchaseID, CustomerID, ArtisanID, ProductID, PurchaseDate, PaymentMethod, TotalCost, TransactionFee, Quantity, PromoCode) Values(" + PurchaseID + ", " + CustomerID + ", " + item.ProjectArtisan + ", " + item.ProjectID + ", " + "@DatePar" + ", 'Card'" + ", " + item.ProjectPrice + ", " + fee + ", " + "1" + ", NULL)";
                        SqlCommand purchaseCommand = new SqlCommand(aPurchase, aSqlConnection);
                        purchaseCommand.Parameters.AddWithValue("@DatePar", DateTime.Now);
                        purchaseCommand.ExecuteNonQuery();
                    }

                    foreach (ShoppingItems item in ShoppingCartProject.Instance.Items)  //message sent to artisan.
                    {
                        string messageContent = "I have bought one of your products! "
                            + "It's " + item.ProjectName + " and it costs " + item.ProjectPrice + " for one project."
                            + " Send to this address: " + getAddress() + " Cheers!";

                        string messageID = getMessageID();
                        string aMessage = "INSERT INTO [Inboxes](UserID, SenderID, MessageID, DateReceived, MessageText) Values (" + item.ProjectArtisan + ", " + CustomerID + ", " + messageID + ", " + "@DatePar," + "@messageContent" + ")";
                        SqlCommand purchaseCommand = new SqlCommand(aMessage, aSqlConnection);
                        purchaseCommand.Parameters.AddWithValue("@DatePar", DateTime.Now);
                        purchaseCommand.Parameters.AddWithValue("@MessageContent", messageContent);
                        purchaseCommand.ExecuteNonQuery();
                    }

                    ShoppingCartProject.Instance.Items.Clear();
                    aSqlConnection.Close();

                    ScriptManager.RegisterStartupScript(this, typeof(string), "Purchase Complete", "alert('You have completed your purchase of " + count + " item(s). Costing €" + cost + "');", true);
                }
                aSqlConnection.Close();

                DataBinding();
            }
        }

        private string emailToUserID(string email)
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

        private string getPurchaseID()
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string purchaseID = "SELECT COALESCE(MAX(PurchaseID), 0) FROM Purchases";
            SqlCommand purchaseIDCommand = new SqlCommand(purchaseID, aSqlConnection);
            int purchaseInt = Convert.ToInt32(purchaseIDCommand.ExecuteScalar());
            aSqlConnection.Close();
            return Convert.ToString(purchaseInt + 1);
        }

        private string getMessageID()
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string MessageID = "SELECT COALESCE(MAX(MessageID), 0) FROM Inboxes";
            SqlCommand messageIDCommand = new SqlCommand(MessageID, aSqlConnection);
            int messageInt = Convert.ToInt32(NullToString(messageIDCommand.ExecuteScalar()));
            aSqlConnection.Close();
            return Convert.ToString(messageInt + 1);
        }

        private string getAddress()
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string Address = "SELECT Address FROM Users WHERE UserID = " + ViewState["UserID"].ToString();
            SqlCommand AddressCommand = new SqlCommand(Address, aSqlConnection);
            Address = NullToString(AddressCommand.ExecuteScalar());
            aSqlConnection.Close();
            return Address;
        }

        private string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

        private bool enoughQuantity(int ProductID, int objectQuantity)
        {
            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            aSqlConnection.Open();
            string quantity = "SELECT Quantity FROM Products WHERE ProductID = " + ProductID;
            SqlCommand quantityCommand = new SqlCommand(quantity, aSqlConnection);
            int DBQuantity = Convert.ToInt32(quantityCommand.ExecuteScalar());
            aSqlConnection.Close();
            if (DBQuantity >= objectQuantity) return true;
            else
            {
                quantityLabel.Text = "There is at least one item with more than the available quantity.";
                return false;
            }
        }

        protected void projectsGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")   //RowCommand event to remove items from projects.
            {
                try
                {
                    string productID = e.CommandArgument.ToString();
                    ShoppingCartProject.Instance.RemoveItem(productID);
                }
                catch (FormatException)
                { }
                Response.Redirect("MyCart.aspx");
                DataBinding();
            }
        }

        protected void returnBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["GoBackTo"].ToString());
        }



    }
}