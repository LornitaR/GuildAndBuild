using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;

namespace GuildAndBuild
{
    public partial class Browse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SqlConnection aConnection = new SqlConnection();
                SqlCommand aCommand = new SqlCommand();
                SqlDataReader aDataReader;
                string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
                string aQuery = "SELECT GuildTypeName, GuildTypeID FROM GuildTypes";
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(aQuery, aConnection);
                aConnection.Open();
                aDataReader = aCommand.ExecuteReader();
                guildDrop.DataSource = aDataReader;
                guildDrop.DataTextField = "GuildTypeName";
                guildDrop.DataValueField = "GuildTypeID";
                guildDrop.DataBind();
                aDataReader.Close();
                aConnection.Close();

                if (Request.QueryString["guild"] != null)
                {
                    guildDrop.SelectedValue = Request.QueryString["guild"];
                    bindDropDowns();
                }


                queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);
            }
        }

        protected void guildDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDropDowns();
        }
        protected void typesDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);
        }

        protected void materialsDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);
        }

        protected void orderByDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);
        }
        public void queryMakeAndDisplay(string guild, string material, string type, string order)
        {
            browseGrid.Columns.Clear();
            string aQuery = "SELECT Products.productID, Products.ProductName, Products.ProductDescription, Products.ProductCost, Artisans.ShopName, ProductPhotos.PhotoLocation FROM Products JOIN ProductPhotos ON Products.ProductID = ProductPhotos.ProductID JOIN Artisans ON Products.ArtisanID = Artisans.ArtisanID";
            string whereClause = " WHERE ";
            if (guild != "0")           //creates query and binds to grid.
            {
                whereClause += "Products.Guild = " + guild;
                if (type != "0")
                {
                    whereClause += " AND Products.ProductType = " + type;
                    if (material != "0")
                    {
                        whereClause += " AND Products.Materials = " + material;
                    }
                }
                else
                {
                    if (material != "0")
                    {
                        whereClause += " AND Products.Materials = " + material;
                    }
                }
                aQuery += whereClause;
            }
            string orderBy = "";
            switch (order)
            {
                case "0":
                    orderBy = " ORDER BY Products.DateAdded";
                    break;
                case "1":
                    orderBy = " ORDER BY Products.DateAdded DESC";
                    break;
                case "2":
                    orderBy = " ORDER BY Products.ProductCost";
                    break;
                case "3":
                    orderBy = " ORDER BY Products.ProductCost DESC";
                    break;
                case "4":
                    orderBy = " ORDER BY Products.ProductName";
                    break;
                case "5":
                    orderBy = " ORDER BY Products.ProductName DESC";
                    break;
            }
            aQuery += orderBy;

            try
            {
                SqlConnection aConnection = new SqlConnection();
                SqlCommand aCommand = new SqlCommand();
                string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
                DataSet aDataSet = new DataSet();
                SqlDataAdapter aDataAdapter = new SqlDataAdapter();
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(aQuery, aConnection);
                aDataAdapter.SelectCommand = aCommand;
                aDataAdapter.SelectCommand.Connection.Open();
                aDataAdapter.Fill(aDataSet, "Results");
                browseGrid.DataSource = aDataSet.Tables["Results"];


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
                browseGrid.DataSource = aDataSet.Tables["Results"];

                BoundField costField = new BoundField();
                costField.DataField = "ProductCost";
                costField.NullDisplayText = "Price Unknown";
                costField.DataFormatString = "{0:C0}";
                costField.HeaderText = "Price";
                browseGrid.DataSource = aDataSet.Tables["Results"];

                // Create an HyperlinkField object to redirect to a page.
                HyperLinkField productHyperLinkField = new HyperLinkField();
                productHyperLinkField.DataTextField = "ProductName";
                productHyperLinkField.DataNavigateUrlFields = new string[] { "productID" };
                productHyperLinkField.DataNavigateUrlFormatString = "ProductProfile.aspx?id={0}";//the name of the profile page to go to for example
                productHyperLinkField.HeaderText = "Product Name";
                browseGrid.DataSource = aDataSet.Tables["Results"];

                HyperLinkField artisanHyperLinkField = new HyperLinkField();
                artisanHyperLinkField.DataTextField = "ShopName";
                artisanHyperLinkField.DataNavigateUrlFields = new string[] { "productID" };
                artisanHyperLinkField.DataNavigateUrlFormatString = "Storefront.aspx?id={0}";
                artisanHyperLinkField.NavigateUrl = "#";//the name of the profile page to go to for example

                artisanHyperLinkField.HeaderText = "Artisan";
                browseGrid.DataSource = aDataSet.Tables["Results"];

                // Add the field columns to the Fields collection of the
                // GridView control.
                browseGrid.Columns.Add(photoImageField);
                browseGrid.Columns.Add(costField);
                browseGrid.Columns.Add(productDescriptionField);
                browseGrid.Columns.Add(artisanHyperLinkField);
                browseGrid.Columns.Add(productHyperLinkField);

                browseGrid.DataBind();
                aConnection.Close();
                browseGrid.AllowPaging = true;
            }
            catch (SqlException) { }
        }

        protected void browseGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            browseGrid.PageIndex = e.NewPageIndex;
            queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);
            browseGrid.DataBind();
        }

        protected void browseGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            materialsDrop.Visible = false;
        }

        private void bindDropDowns()
        {
            if (guildDrop.SelectedValue != "0")
            {
                SqlConnection aConnection = new SqlConnection();
                SqlCommand aCommand = new SqlCommand();
                SqlDataReader aDataReader;
                string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
                typesDrop.Visible = true;
                string typesQuery = "SELECT TypeName, TypeID FROM Types WHERE GuildTypeID = " + guildDrop.SelectedValue + " OR TypeID = 0";
                aConnection.ConnectionString = aConnectionString;
                aCommand = new SqlCommand(typesQuery, aConnection);  //Binds data to the types drop down menu relative to a valid selected guild i.e not "Please select guild".
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

                queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);

            }
            else
            {
                queryMakeAndDisplay(guildDrop.SelectedValue, typesDrop.SelectedValue, materialsDrop.SelectedValue, orderByDrop.SelectedValue);
                typesDrop.Visible = false;
                materialsDrop.Visible = false;   //If the user does not choose a guild, these two drop down boxes are hidden
            }
        }
    }
}