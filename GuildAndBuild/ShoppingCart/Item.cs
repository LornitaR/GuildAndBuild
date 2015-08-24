using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

    public class Item
    {
        public string itemName { get; set; }
        public decimal itemPrice { get; set; }
        public string itemArtisan { get; set; }
        public string itemPhoto { get; set; }

        public Item(int item)
        {
            string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
            string NameComm = "SELECT ProductName FROM Products WHERE productID = " + item;
            string Cost = "SELECT ProductCost FROM Products WHERE productID = " + item;
            string artisanID = "SELECT ArtisanID FROM Products WHERE productID = " + item;
            string Cover = "SELECT PhotoLocation FROM Products JOIN ProductPhotos ON Products.productID = ProductPhotos.ProductID WHERE Products.productID = " + item;


            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            SqlCommand nameCommand = new SqlCommand(NameComm, aSqlConnection);
            SqlCommand priceCommand = new SqlCommand(Cost, aSqlConnection);
            SqlCommand artisanIDCommand = new SqlCommand(artisanID, aSqlConnection);
            SqlCommand coverCommand = new SqlCommand(Cover, aSqlConnection);

            aSqlConnection.Open();

            this.itemName = Convert.ToString(nameCommand.ExecuteScalar());
            this.itemPrice = Convert.ToDecimal(priceCommand.ExecuteScalar());
            this.itemArtisan = Convert.ToString(artisanIDCommand.ExecuteScalar());
            this.itemPhoto = Convert.ToString(coverCommand.ExecuteScalar());

            aSqlConnection.Close();
        }
        public Item(string project)
        {
            string aConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
            string NameComm = "SELECT ProjectName FROM Projects WHERE ProjectID = " + project;
            string Cost = "SELECT BidPrice FROM Bids JOIN Projects ON Projects.ProjectID = Bids.ProjectID WHERE Bids.ProjectID = " + project + " AND Bids.BidStatus = 1";
            string artisanID = "SELECT ArtisanID FROM Bids JOIN Projects ON Projects.ProjectID = Bids.ProjectID WHERE Bids.ProjectID = " + project + " AND Bids.BidStatus = 1";
            string photo = "SELECT PhotoLocation FROM Projects JOIN ProjectPhotos ON Projects.ProjectID = ProjectPhotos.ProjectID WHERE Projects.ProjectID = " + project;

            SqlConnection aSqlConnection = new SqlConnection(aConnectionString);
            SqlCommand nameCommand = new SqlCommand(NameComm, aSqlConnection);
            SqlCommand priceCommand = new SqlCommand(Cost, aSqlConnection);
            SqlCommand artisanIDCommand = new SqlCommand(artisanID, aSqlConnection);
            SqlCommand photoIDCommand = new SqlCommand(photo, aSqlConnection);

            aSqlConnection.Open();

            this.itemName = Convert.ToString(nameCommand.ExecuteScalar());
            this.itemPrice = Convert.ToDecimal(priceCommand.ExecuteScalar());
            this.itemArtisan = Convert.ToString(artisanIDCommand.ExecuteScalar());
            this.itemPhoto = Convert.ToString(photoIDCommand.ExecuteScalar());

            aSqlConnection.Close();
        }


    }
   
