using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;

namespace GuildAndBuild
{
    public partial class Search : System.Web.UI.Page
    {
        SqlConnection aConnection = new SqlConnection();
        SqlCommand aCommand = new SqlCommand();
        SqlDataReader aDataReader;
        string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void advancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (advancedSearch.Checked == true)
            {
                guildDrop.Visible = true;
                priceRangeLbl.Visible = true;
                priceRangeLbl2.Visible = true;
                lowPriceRange.Visible = true;
                highPriceRange.Visible = true;   //if the advanced search check box is checked, items are shown.
                orderByDrop.Visible = true;
                locationDrop.Visible = true;
                locationLabel.Visible = true;
                artisanSearchTB.Visible = true;
                artisansLbl.Visible = true;


                string aQuery = "SELECT GuildTypeName, GuildTypeID FROM GuildTypes";
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(aQuery, aConnection);
                aConnection.Open();
                aDataReader = aCommand.ExecuteReader(); //Binds data to Guilds Drop Down Menu.
                guildDrop.DataSource = aDataReader;
                guildDrop.DataTextField = "GuildTypeName";
                guildDrop.DataValueField = "GuildTypeID";
                guildDrop.DataBind();
                aDataReader.Close();
                aConnection.Close();

                string locationQuery = "SELECT LocationName, LocationID FROM Locations";
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(locationQuery, aConnection);
                aConnection.Open();
                aDataReader = aCommand.ExecuteReader(); // Binds data to Location Drop Down Menu.
                locationDrop.DataSource = aDataReader;
                locationDrop.DataTextField = "LocationName";
                locationDrop.DataValueField = "LocationID";
                locationDrop.DataBind();
                aDataReader.Close();
                aConnection.Close();

            }
            else if (advancedSearch.Checked == false)
            {
                guildDrop.Visible = false;
                materialsDrop.Visible = false;
                typesDrop.Visible = false;
                priceRangeLbl.Visible = false;   //Hides items when the advanced search checkbox is not checked.
                priceRangeLbl2.Visible = false;
                lowPriceRange.Visible = false;
                highPriceRange.Visible = false;
                orderByDrop.Visible = false;
                locationDrop.Visible = false;
                locationLabel.Visible = false;
                artisanSearchTB.Visible = false;
                artisansLbl.Visible = false;
            }
            query();
        }

        protected void guildDrop_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (guildDrop.SelectedValue != "0")
            {
                typesDrop.Visible = true;
                string typesQuery = "SELECT TypeName, TypeID FROM Types WHERE GuildTypeID = " + guildDrop.SelectedValue + " OR TypeID = 0";
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(typesQuery, aConnection);
                SqlDataReader aDataReader;   //Binds data to the types drop down menu relative to a valid selected guild i.e not "Please select guild".
                aConnection.Open();
                aDataReader = aCommand.ExecuteReader();
                typesDrop.DataSource = aDataReader;
                typesDrop.DataTextField = "TypeName";
                typesDrop.DataValueField = "TypeID";
                typesDrop.DataBind();
                aConnection.Close();

                materialsDrop.Visible = true;
                string materialsQuery = "SELECT MaterialTypeName, MaterialTypeID FROM MaterialTypes WHERE GuildTypeID = " + guildDrop.SelectedValue + " OR MaterialTypeID = 0";
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(materialsQuery, aConnection);
                aConnection.Open();
                aDataReader = aCommand.ExecuteReader();  //Binds data to the materials drop down menu relative to a valid selected guild.
                materialsDrop.DataSource = aDataReader;
                materialsDrop.DataTextField = "MaterialTypeName";
                materialsDrop.DataValueField = "MaterialTypeID";
                materialsDrop.DataBind();
                aDataReader.Close();
                aConnection.Close();
            }
            else
            {
                typesDrop.Visible = false;
                materialsDrop.Visible = false;   //If the user does not choose a guild, these two drop down boxes are hidden
            }
            query();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            query();
        }

        private SqlCommand basicQuery(string searchItem)
        {
            Regex thing = new Regex("[' ']{2,}");
            searchItem = searchItem.Replace("\'","");
            string sQLCommand = "SELECT Products.ProductID, Products.ProductName, Products.ProductDescription, Products.ProductCost, Artisans.ShopName, ProductPhotos.PhotoLocation FROM Products JOIN ProductPhotos ON Products.ProductID = ProductPhotos.ProductID JOIN Artisans ON Products.ArtisanID = Artisans.ArtisanID JOIN Users ON Products.ArtisanID = Users.UserID";
            SqlCommand basicSearch = new SqlCommand(sQLCommand);
            if (searchItem.Length != 0 && !thing.IsMatch(searchItem))
            {
                string[] searchWords = searchItem.Split(' ');
                string whereClause = " WHERE (Products.ProductName LIKE @basicSearch  OR Products.ProductDescription LIKE @basicSearch ";
                basicSearch.Parameters.AddWithValue("@basicSearch", "%" + searchWords[0] + "%");
                for (int i = 1; i < searchWords.Length; i++)
                {
                        whereClause += " OR Products.ProductName LIKE @basicSearch" + i + "  OR Products.ProductDescription LIKE @basicSearch" + i + " ";
                        basicSearch.Parameters.AddWithValue("@basicSearch" + i, "%" + searchWords[i] + "%");
                }

                basicSearch.CommandText += whereClause + ")";
                return basicSearch;                              //Splits up all the words in the string. Word is something after which or before which there is a space.                                                  
            }                                                   //Each word is placed into a string array and added to the SQL query in the where clause.
            return basicSearch;                                  //Looks like SELECT ..... FROM ..... WHERE((Clause) OR (Clause) OR (Clause)) 
        }



        private SqlCommand advancedQuery(string searchItem, string artisanItem, string guild, string type, string materials, string lowPrice, string highPrice, string location, string order)
        {
            SqlCommand advancedQuery = basicQuery(searchItem);
            string advancedSearchQuery = advancedQuery.CommandText;
            Regex thing = new Regex("[' ']{2,}");
            Regex numbersRegex = new Regex("^[0-9]{1,3}([.,][0-9]{1,3})?$");
            Regex numbers2Regex = new Regex(@"^\d$");
            string whereClauseExtras = "";
            //shop term, guild etc.

            if (guild != "0" || artisanItem.Length != 0 || lowPrice.Length != 0 || highPrice.Length != 0 || location != "0")
            {
                if (!advancedSearchQuery.Contains("WHERE"))
                {
                    whereClauseExtras = " WHERE ";
                }
                else
                {
                    whereClauseExtras = " AND ";
                }

                if (artisanItem.Length != 0 && !thing.IsMatch(artisanItem))
                {
                    string[] artisanItemWords = artisanItem.Split(' ');
                    whereClauseExtras += "(Artisans.ShopName like @ArtisanItemWords ";      //same as above: splits up artisan search term into single words.
                    advancedQuery.Parameters.AddWithValue("@ArtisanItemWords", "%" + artisanItemWords[0] + "%");
                    for (int i = 1; i < artisanItemWords.Length; i++)
                    {
                        whereClauseExtras += " OR Artisans.ShopName like @ArtisanItemWords" + i + " ";
                        advancedQuery.Parameters.AddWithValue("@ArtisanItemWords" + i, "%" + artisanItemWords[i] + "%");
                    }
                    whereClauseExtras += ")";
                }


                    
                if (guild != "0")
                {
                    if (whereClauseExtras.Contains("Artisans.ShopName like")) whereClauseExtras += " AND ";
                    whereClauseExtras += "Products.Guild = " + guild;
                    if (type != "0")
                    {
                        whereClauseExtras += " AND Products.ProductType = " + type;
                        if (materials != "0")
                        {
                            whereClauseExtras += " AND Products.Materials = " + materials;
                        }
                    }
                    else
                    {
                        if (materials != "0")
                        {
                            whereClauseExtras += " AND Products.Materials = " + materials;
                        }
                    }
                }


                if (lowPrice.Length != 0 && (numbersRegex.IsMatch(lowPrice) || numbers2Regex.IsMatch(lowPrice)))    //Ignores if not numeric, either 20, or 20.00, or 20,00 accepted etc.
                {
                    double low = Convert.ToDouble(lowPrice);
                    if (whereClauseExtras.Contains("Artisans.ShopName like") || whereClauseExtras.Contains("Products.Guild = ")) whereClauseExtras += " AND ";
                    whereClauseExtras += "Products.ProductCost >= @LowPriceRange";
                    advancedQuery.Parameters.AddWithValue("@LowPriceRange", low);
                }

                if (highPrice.Length != 0 && (numbersRegex.IsMatch(highPrice) || numbers2Regex.IsMatch(highPrice)))
                {
                    double high = Convert.ToDouble(highPrice);
                    if (whereClauseExtras.Contains("Artisans.ShopName like") || whereClauseExtras.Contains("Products.Guild = ") || whereClauseExtras.Contains("ProductCost >=")) whereClauseExtras += " AND ";
                    whereClauseExtras += "Products.ProductCost <= @HighPriceRange";
                    advancedQuery.Parameters.AddWithValue("@HighPriceRange", high);
                }



                if(location != "0")
                {
                    if (whereClauseExtras.Contains("Artisans.ShopName like") || whereClauseExtras.Contains("Products.Guild = ") || whereClauseExtras.Contains("@HighPriceRange") || whereClauseExtras.Contains("@LowPriceRange"))
                    {
                        whereClauseExtras += " AND ";
                    }
                    whereClauseExtras += "Users.Location = " + location;
                }


            }

            switch (order)  //Order by any attribute.
            {
                case "0":
                    whereClauseExtras += " ORDER BY Products.DateAdded";
                    break;
                case "1":
                    whereClauseExtras += " ORDER BY Products.DateAdded DESC";
                    break;
                case "2":
                    whereClauseExtras += " ORDER BY Products.ProductCost";
                    break;
                case "3":
                    whereClauseExtras += " ORDER BY Products.ProductCost DESC";
                    break;
                case "4":
                    whereClauseExtras += " ORDER BY Products.ProductName";
                    break;
                case "5":
                    whereClauseExtras += " ORDER BY Products.ProductName DESC";
                    break;
            }
            advancedSearchQuery += whereClauseExtras;

            advancedQuery.CommandText = advancedSearchQuery;

            return advancedQuery;
        }

        protected void searchGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("ProductProfile.aspx?item=" + searchGrid.Rows.ToString());
        }

        private void query()    //creates grid, creates columns, binds columns.
        {
            searchGrid.Columns.Clear();
            string searchItem = searchTB.Text;
            DataSet aDataSet = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            aConnection.ConnectionString = aConnectionString;

            aCommand = new SqlCommand();

            if (advancedSearch.Checked == false)
            {                                    //Basic search.
                aCommand = basicQuery(searchItem);
                aCommand.CommandText += " ORDER BY Products.ProductName";
                aCommand.Connection = aConnection;
            }
            else
            {
                string shopTerm = artisanSearchTB.Text;
                aCommand = advancedQuery(searchItem, shopTerm, guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, lowPriceRange.Text, highPriceRange.Text, locationDrop.SelectedValue, orderByDrop.SelectedValue);
                aCommand.Connection = aConnection;
            }


            // Create an ImageField object to display a photo.
            ImageField photoImageField = new ImageField();
            photoImageField.DataImageUrlField = "PhotoLocation";//the name of the column in the database
            photoImageField.AlternateText = "Product Photos";
            photoImageField.NullDisplayText = "No image on file.";
            photoImageField.ControlStyle.Height = 120;
            photoImageField.HeaderText = "Product Photos";
            photoImageField.ReadOnly = true;

            // Create a BoundField object to display text.
            BoundField productDescriptionField = new BoundField();
            productDescriptionField.DataField = "ProductDescription";
            productDescriptionField.NullDisplayText = "No description.";
            productDescriptionField.HeaderText = "Description";
            searchGrid.DataSource = aDataSet.Tables["Results"];

            BoundField costField = new BoundField();
            costField.DataField = "ProductCost";
            costField.DataFormatString = "{0:C0}";
            costField.NullDisplayText = "Price Unknown";
            costField.HeaderText = "Price";
            searchGrid.DataSource = aDataSet.Tables["Results"];

            // Create an HyperlinkField object to redirect to a page.
            HyperLinkField productHyperLinkField = new HyperLinkField();
            productHyperLinkField.DataTextField = "ProductName";
            productHyperLinkField.DataNavigateUrlFields = new string[] { "ProductID" };
            productHyperLinkField.DataNavigateUrlFormatString = "ProductProfile.aspx?id={0}"; //the name of the profile page to go to for example
            productHyperLinkField.HeaderText = "Product Name";
            searchGrid.DataSource = aDataSet.Tables["Results"];

            HyperLinkField artisanHyperLinkField = new HyperLinkField();
            artisanHyperLinkField.DataTextField = "ShopName";
            artisanHyperLinkField.DataNavigateUrlFields = new string[] { "ProductID" };//the name of the profile page to go to for example
            artisanHyperLinkField.DataNavigateUrlFormatString = "Storefront.aspx?id={0}";
            artisanHyperLinkField.HeaderText = "Artisan";
            searchGrid.DataSource = aDataSet.Tables["Results"];

            // Add the field columns to the Fields collection of the
            // GridView control.
            searchGrid.Columns.Add(photoImageField);
            searchGrid.Columns.Add(costField);
            searchGrid.Columns.Add(productDescriptionField);
            searchGrid.Columns.Add(artisanHyperLinkField);
            searchGrid.Columns.Add(productHyperLinkField);

            try
            {
                da.SelectCommand = aCommand;
                da.SelectCommand.Connection.Open();   //if the user enters " " or something like "      John  " they will recieve as a reply to their query every item. 
                da.Fill(aDataSet, "Results");
                searchGrid.DataSource = aDataSet.Tables["Results"];
                searchGrid.DataBind();
                aConnection.Close();
            }
            catch (SqlException) { }
        }

        protected void searchGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            searchGrid.PageIndex = e.NewPageIndex;
            query();
        }


    }
}
