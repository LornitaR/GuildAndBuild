using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{
    public class Product
    {
        string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public int ProductID { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductName { get; set; }
        public double ProductCost { get; set; }
        public int ProductQuantity { get; set; }
        public string Guild { get; set; }
        public string Type { get; set; }
        public string Materials { get; set; }
        public string ProductDescription { get; set; }

    }
    public class ProductDataAccessLayer
    {
        /*  
         *  Input: ArtisanID: Is an int containg the artisans UserID from the cookies
         *  Processing: Reads the relevant details from the database and passes them to a list of Product type
         *  Output: Returns a list of Product type to be used in the population of the relevant grid view   
         */
        public static List<Product> GetAllProducts(int ArtisanID)
        {
            List<Product> listProducts = new List<Product>();

            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("select ProductID, PhotoLocation, ProductName, ProductCost, Quantity, GuildTypeName, TypeName, MaterialTypeName, ProductDescription from ProductInfo where ArtisanId = @ArtisanID;", conn);
                    cmd.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Product product = new Product();
                        product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                        product.ProductPhoto = rdr["PhotoLocation"].ToString();
                        product.ProductName = rdr["ProductName"].ToString();
                        product.ProductCost = Convert.ToDouble(rdr["ProductCost"]);
                        product.ProductQuantity = Convert.ToInt32(rdr["Quantity"]);
                        product.Guild = rdr["GuildTypeName"].ToString();
                        product.Type = rdr["TypeName"].ToString();
                        product.Materials = rdr["MaterialTypeName"].ToString();
                        product.ProductDescription = rdr["ProductDescription"].ToString();

                        listProducts.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return listProducts;
        }

        /*  
         *  Input: ProductID: an int containing the productID of the row to be deleted
         *  Processing: Passes an sql ommand back to the server to delete the specfied row
         *  Output: Row will be deleted                                                                     
         */
        public static void DeleteProduct(int ProductID)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Delete from Products where (ProductID = @productID);", conn);
                    cmd.Parameters.AddWithValue("@productID", ProductID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
        }
        
        /*  
         *  Input: ProductID: As above; ProductName: string containing the product name of the relevant product; 
         *  ProductCost: A double containing the cost of the product; ProductQuantity: An int containing the quantity of the product
         *  ; ProductDescription: a string containing a description of the product.
         *  Processing: Sends an sql command to update the relevant areas of the item specified by the productID
         *  Output: Returns the number of rows affected by the command. This is a leftover from an idea I had, but never got the time to finish.
         */
        public static int UpdateProduct(int ProductID, string ProductName, double ProductCost, int ProductQuantity, string ProductDescription)
        {
            // Will have to add the rest of the parameters once i figure out how to add from a ddl.
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string updateQuery = "Update Products SET ProductName = @ProductName, " +
                    "ProductCost = @ProductCost, Quantity = @Quantity, ProductDescription = @ProductDescription WHERE ProductID = @productID";             
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@productID", ProductID);
                cmd.Parameters.AddWithValue("@ProductName", ProductName);
                cmd.Parameters.AddWithValue("@ProductCost", ProductCost);
                cmd.Parameters.AddWithValue("@Quantity", ProductQuantity);
                cmd.Parameters.AddWithValue("@ProductDescription", ProductDescription);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
