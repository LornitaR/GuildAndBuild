using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{


    public class User
    {

        
        //string firstname = Settings.firstname;
        public string UserFirstname { get; set; }
        public string UserSurname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserPassword{ get; set; }
        public int Location{ get; set; }
        public DateTime DateOfBirth{ get; set; }
        public int Interests{ get; set; }
        public string Gender{ get; set; }
        public string ProfilePhoto{ get; set; }
        public string Address{ get; set; }

                
    }

    public partial class UserAccess
    {
        
        public static void changeSettings(User aUser)
        {
            string UserFirstname = aUser.UserFirstname;
            string UserSurname = aUser.UserSurname;
            string Username = aUser.Username;
            string password = aUser.UserPassword;
            int location = aUser.Location;
            DateTime DOB = aUser.DateOfBirth;
            int interests = aUser.Interests;
            string gender = aUser.Gender;
            string address = aUser.Address;

        }

       

        //takes username and checks to see if it exists in the database
        public static bool checkValidUser(string username)
        {
            bool valid;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            
            SqlCommand getValidUser = new SqlCommand("SELECT count(Username) from Users where Username = CAST(@name AS varchar)", conn);
            getValidUser.Parameters.AddWithValue("@name", username);
            if (getValidUser != null)
            {
                int User = Convert.ToInt32(getValidUser.ExecuteScalar());
                if (User == 1)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }

                conn.Close();
                
            }
            else
            {
                valid = false;
            }
            return valid;
        }

        //takes in the email address of the user
        //updates last login time for that user to the current system time when they log in
        public static void changeLoginTime(string email)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand loginTime = new SqlCommand("Update Users set LastLoginTime = @Login where UserEmail = @UserEmail", conn);
            loginTime.Parameters.AddWithValue("@Login", DateTime.Now);
            loginTime.Parameters.AddWithValue("@UserEmail", email);
            int rows = loginTime.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new shop name
        //updates the shop name using the userID
        public static void changeShopName(string name, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Artisans SET ShopName = @name WHERE ArtisanID = @UserID", conn);
            insert.Parameters.AddWithValue("@name", name);
            insert.Parameters.AddWithValue("@ArtisanID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new bio
        //updates the bio using userID
        public static void changeBio(string bio, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Artisans SET ArtisanDescription = @bio WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@bio", bio);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new first name
        //updates the first name using userID
        public static void changeFirstname(string fName, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET UserFirstname = @UserFirstname WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@UserFirstname", fName);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new surname
        //updates the surname using userID
        public static void changeSurname(string sName, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET UserSurname = @UserSurname WHERE UserID = @UserID ", conn);
            insert.Parameters.AddWithValue("@UserSurname", sName);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new username
        //updates the username using userID
        public static void changeUsername(string uName, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET Username = @Username WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@Username", uName);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new password
        //updates the password using userID
        public static void changePassword(string password, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET UserPassword = @UserPassword WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@UserPassword", password);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new location
        //updates the location using userID
        public static void changeLocation(int location, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET Location = @Location WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@Location", location);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new date of birth
        //updates the date of birth using userID
        public static void changeDOB(DateTime DOB, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET DateOfBirth = @DOB WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@DOB", DOB);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new interest
        //updates the interest using userID
        public static void changeInterests(int interests, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET Interests = @Interests WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@Interests", interests);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new username
        //updates the username using userID
        public static void changeGender(string gender, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET Gender = @Gender WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@Gender", gender);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new address
        //updates the address using userID
        public static void changeAddress(string address, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET Address = @Address WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@Address", address);
            insert.Parameters.AddWithValue("@UserID", UserID);
            int rows = insert.ExecuteNonQuery();
            conn.Close();
        }

        //takes in the userID and new profile photo
        //updates the username using profile photo
        public static void changeProfilePicture(string location, int UserID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand insert = new SqlCommand("UPDATE Users SET ProfilePhoto = @PhotoLocation WHERE UserID = @UserID", conn);
            insert.Parameters.AddWithValue("@UserID", UserID);
            insert.Parameters.AddWithValue("@PhotoLocation", location);
            int rows = insert.ExecuteNonQuery();              

            conn.Close();
        }

        //takes in the email address and user type
        //deletes this user from the database
        //user type it needed to see how many tables are affected
        public static void deleteAccount(string email, int UserType)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand deleteUser = new SqlCommand("Delete from Users where UserEmail = @Email", conn);
            deleteUser.Parameters.AddWithValue("@Email", email);
            deleteUser.ExecuteNonQuery();
            SqlCommand getID = new SqlCommand("Select UserID from Users where UserEmail = @Email", conn);
            getID.Parameters.AddWithValue("@Email", email);
            int id = Convert.ToInt32(getID.ExecuteScalar());

            if (UserType == 2)
            {
                SqlCommand deleteArtisan = new SqlCommand("Delete from Artisans where ArtisanID = @id", conn);
                deleteArtisan.Parameters.AddWithValue("@id", id);
                deleteArtisan.ExecuteNonQuery();

            }

            conn.Close();
        }


    }
    
    
}


