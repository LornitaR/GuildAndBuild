using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{
    public class Project
    {
        string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public int ProjectID { get; set; }
        public int CustomerID { get; set; }
        public int BidID { get; set; }
        public int ArtisanID { get; set; }
        public string ShopName { get; set; }
        public string ProjectPhoto { get; set; }
        public string ProjectName { get; set; }
        public double MaxPrice { get; set;}
        public double MinPrice { get; set;}
        public string ProjectDescription { get; set; }
        public string Guild { get; set; }
        public string Type { get; set; }
        public string Materials { get; set; }
        public string Deadline { get; set; }
        public string CompleteStatus { get; set; }
        public string PublicProject { get; set; }
        public string BidStatus { get; set; }
        public double BidPrice { get; set; }
        public string BidText { get; set; }

    }

    public class ProjectDataAccessLayer
    {

        /*  
         *  Input: Interests: Is a string containing the value of the users interest; UserID: Is an int containg the UserID from the cookies
         *  Processing: Reads the relevant details from the database and passes them to a list of Project type
         *  Output: Returns a list of Project type to be used in the population of the relevant grid view   
         */
        public static List<Project> GetAllProjects(string interests, int UserID)
        {
            List<Project> listProjects = new List<Project>();
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                
                try
                {
                    using (SqlConnection conn = new SqlConnection(CS))
                    {
                        int interestID = Convert.ToInt32(interests);
                        SqlCommand cmd = new SqlCommand("SELECT ProjectID, CustomerID, PhotoLocation, ProjectName, ProjectDescription, GuildTypeName, TypeName, MaterialTypeName, Deadline, PublicProject, MaxPrice, MinPrice, StatusType from ProjectInfo where (GuildTypeID = @Interest or PrivateArtisan = @ArtisanID); ", conn);
                        cmd.Parameters.AddWithValue("@Interest", interestID);
                        cmd.Parameters.AddWithValue("@ArtisanID", UserID);
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Project project = new Project();
                            project.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                            project.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                            project.ProjectPhoto = rdr["PhotoLocation"].ToString();
                            project.ProjectName = rdr["ProjectName"].ToString();
                            project.ProjectDescription = rdr["ProjectDescription"].ToString();
                            project.Guild = rdr["GuildTypeName"].ToString();
                            project.Type = rdr["TypeName"].ToString();
                            project.Materials = rdr["MaterialTypeName"].ToString();
                            project.PublicProject = rdr["StatusType"].ToString();
                            project.Deadline = rdr["Deadline"].ToString();
                            project.MaxPrice = Convert.ToDouble(rdr["MaxPrice"]);
                            project.MinPrice = Convert.ToDouble(rdr["MinPrice"]);
                            project.CompleteStatus = rdr["StatusType"].ToString();

                            listProjects.Add(project);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return listProjects;
            }

        /*  
         *  Input: UserID: Is an int containg the UserID from the cookies
         *  Processing: Reads the relevant details from the database, i.e the details of a specific users projects, and passes them to a list of Project type
         *  Output: Returns a list of Project type to be used in the population of the relevant grid view   
         */
        public static List<Project> GetMyProjects(string UserID)
        {
            List<Project> listProjects = new List<Project>();
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    int userID = Convert.ToInt32(UserID);
                    SqlCommand cmd = new SqlCommand("SELECT ProjectID, CustomerID, PhotoLocation, ProjectName, ProjectDescription, GuildTypeName, TypeName, MaterialTypeName, Deadline, PublicProject, MaxPrice, MinPrice, StatusType from ProjectInfo where CustomerID = @UserID and StatusType = 'Up for tender';", conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Project project = new Project();
                        project.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                        project.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                        //project.ArtisanID = Convert.ToInt32(rdr["ArtisanID"]);
                        project.ProjectPhoto = rdr["PhotoLocation"].ToString();
                        project.ProjectName = rdr["ProjectName"].ToString();
                        project.ProjectDescription = rdr["ProjectDescription"].ToString();
                        project.Guild = rdr["GuildTypeName"].ToString();
                        project.Type = rdr["TypeName"].ToString();
                        project.Materials = rdr["MaterialTypeName"].ToString();
                        project.PublicProject = rdr["StatusType"].ToString();
                        project.Deadline = rdr["Deadline"].ToString();
                        project.MaxPrice = Convert.ToDouble(rdr["MaxPrice"]);
                        project.MinPrice = Convert.ToDouble(rdr["MinPrice"]);
                        project.CompleteStatus = rdr["StatusType"].ToString();

                        listProjects.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return listProjects;
        }

        /*  
         *  Input: UserID: Is an int containg the UserID from the cookies
         *  Processing: Reads the relevant details from the database, 
         *  i.e the details of a users projects that have active bids, 
         *  and passes them to a list of Project type
         *  Output: Returns a list of Project type to be used in the population of the relevant grid view   
         */
        public static List<Project> GetMyBids(string UserID)
        {
            List<Project> listProjects = new List<Project>();
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    int userID = Convert.ToInt32(UserID);
                    SqlCommand cmd = new SqlCommand("SELECT BidID, ArtisanID, ProjectID, CustomerID, ProjectName, ProjectDescription, Deadline,  BidStatusName, StatusType, MaxPrice, MinPrice, ShopName, BidPrice, BidText from ProjectBids where CustomerID = @UserID and BidStatusName != 'Accepted';", conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Project project = new Project();
                        project.BidID = Convert.ToInt32(rdr["BidID"]);
                        project.ArtisanID = Convert.ToInt32(rdr["ArtisanID"]);
                        project.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                        project.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                        project.ProjectName = rdr["ProjectName"].ToString();
                        project.ProjectDescription = rdr["ProjectDescription"].ToString();
                        project.BidStatus = rdr["BidStatusName"].ToString();
                        project.CompleteStatus = rdr["StatusType"].ToString();
                        project.Deadline = rdr["Deadline"].ToString();
                        project.MaxPrice = Convert.ToDouble(rdr["MaxPrice"]);
                        project.MinPrice = Convert.ToDouble(rdr["MinPrice"]);
                        project.ShopName = rdr["ShopName"].ToString();
                        project.BidPrice = Convert.ToDouble(rdr["BidPrice"]);
                        project.BidText = rdr["BidText"].ToString();

                        listProjects.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return listProjects;
        }

        /*  
         *  Input: ProjectID: Is an int containg the ProjectID from the selected project
         *  Processing: Passes an SQL command to the database that deletes the selet project details.
         *  Output: void   
         */
        public static void DeleteProject(int ProjectID)
        {
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Projects WHERE (ProjectID = @projectID);", conn);
                SqlParameter param = new SqlParameter("@projectID", ProjectID);
                cmd.Parameters.Add(param);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*  
         *  Input: BidID: Is an int containg the BidID for the selected project; ArtisanID: an int containing the ArtisanId of the proposer of the bid;
         *  BidText: a string containing the text of the proposed bid; BidPrice: an int containing the price of the proposed bid;
         *  BidStatus: an int containing the bidStatus of the proposed bid; ProjectID: an int contining the ID of the selected project
         *  Processing: Passes an SQL command to the database that updates the bid table with a new entry containing the relevant details.
         *  Output: void   
         */
        public static void MakeBid(int BidID, int ArtisanID, string BidText, double BidPrice, int BidStatus, int ProjectID)
        {
            // Will have to add the rest of the parameters once i figure out how to add from a ddl.
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string updateQuery = "insert into [Bids] (BidID, ArtisanID, BidText, BidPrice, BidStatus, ProjectID)" +
                                        " values (@BidID, @ArtisanID, @BidTxt, @BidPrice, @BidStatus, @ProjectID)";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@BidID", BidID);
                cmd.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                cmd.Parameters.AddWithValue("@BidTxt", BidText);
                cmd.Parameters.AddWithValue("@BidPrice", BidPrice);
                cmd.Parameters.AddWithValue("@BidStatus", BidStatus);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*  
         *  Input: BidID: Is an int containg the BidID for the selected project; 
         *  ArtisanID: an int containing the ArtisanId of the proposer of the bid;
         *  ProjectID: an int contining the ID of the selected project
         *  Processing: Passes an SQL command to the database that updates the bid status to accepted and simultaniously removes all other bids on the project
         *  Output: void   
         */
        public static void ChangeBidStatus(int BidID, int ArtisanID, int ProjectID )
        {
            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string updateQuery = "UPDATE Bids SET BidStatus = 1 WHERE BidID = @BidID AND @ArtisanID = @ArtisanID AND ProjectID = @ProjectID";
                string updateCompleteQuery = "UPDATE Projects SET CompleteStatus = 1 WHERE ProjectID = @ProjectID;";
                string updateQuery2 = "Delete from Bids WHERE (NOT BidID = @BidID AND ProjectID = @ProjectID)";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                SqlCommand cmdComplete = new SqlCommand(updateCompleteQuery, conn);
                SqlCommand cmd2 = new SqlCommand(updateQuery2, conn);
                cmd.Parameters.AddWithValue("@BidID", BidID);
                cmd.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmdComplete.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd2.Parameters.AddWithValue("@BidID", BidID);
                cmd2.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                cmd2.Parameters.AddWithValue("@ProjectID", ProjectID);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmdComplete.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
        }

        /*  
         *  Input: BidID: Is an int containg the BidID for the selected project; 
         *  Processing: Creates a new BidID for the new bid.
         *  Output: An int containing the new BidID
         */
        public static int newBidID()
        {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand count = new SqlCommand("SELECT coalesce (MAX(BidID), 0) FROM Bids", conn);
                int BidID;
                int count1 = (int)count.ExecuteScalar();

                if (count1 == 0)
                {
                    BidID = 1;

                }
                else
                {
                    BidID = count1 + 1;
                }

                return BidID;
        }
    }
}