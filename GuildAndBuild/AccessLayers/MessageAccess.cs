using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace GuildAndBuild
{
                 
    public class Message 
    {
        string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public string userName { get; set; }
        public string SenderName { get; set; }
        public int MessageID { get; set; }
        public DateTime DateReceived { get; set; }
        public string MessageText { get; set; }
    }

    public partial class MessageAccess
    {
        //takes in the user name and gets all messages sent to this person
        public static List<Message> GetAllMessages(string Username)
        {
            
            List<Message> listMessages = new List<Message>();

            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try{
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * from MessageDetails where Username = CAST(@Username AS varchar) ORDER BY DateReceived desc;", con);
                    cmd.Parameters.AddWithValue("@Username", Username);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Message message = new Message();
                        message.userName = rdr["Username"].ToString();
                        message.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                        message.MessageText = rdr["MessageText"].ToString();
                        message.SenderName = rdr["Sender"].ToString();
                        message.MessageID = Convert.ToInt32(rdr["MessageID"]);

                        listMessages.Add(message);
                    }
                    
                    con.Close();
                }

                
            }

            
    
            catch (Exception ex)
            {
                
            }

            
                return listMessages;
           
        }

        //takes in the username and gets all messages they have sent
        public static List<Message> GetSentMessages(string Username)
        { 
        List<Message> listMessages = new List<Message>();

            string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * from MessageDetails where Sender = CAST(@Username AS varchar) ORDER BY DateReceived desc;", con);
                    cmd.Parameters.AddWithValue("@Username", Username);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Message message = new Message();
                        message.userName = rdr["Username"].ToString();
                        message.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                        message.MessageText = rdr["MessageText"].ToString();
                        message.SenderName = rdr["Sender"].ToString();
                        message.MessageID = Convert.ToInt32(rdr["MessageID"]);


                        listMessages.Add(message);
                    }

                    con.Close();
                }
            }

            catch (Exception ex)
            {
                
            }

            
           return listMessages;
        }

        //takes in the receiver name, sender name, time the message was sent, and the message text
        //enters these in to the inbox table
        public static void createMessage(string receiverName, string senderName, DateTime sentAt, string messageText)
        {
            
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand countMessages = new SqlCommand("Select coalesce (max(MessageID), 0) from Inboxes", conn);
                SqlCommand rName = new SqlCommand("Select UserID from Users where Username = '" + receiverName + "'", conn);
                SqlCommand sName = new SqlCommand("Select UserID from Users where Username = '" + senderName + "'", conn);
                int rID = (int)rName.ExecuteScalar();
                int sID = (int)sName.ExecuteScalar();

                int count;

                if (countMessages == null)
                {
                    count = 1;

                }
                else
                {
                    count = (int)countMessages.ExecuteScalar();

                }
               
                int messageID = count + 1;

                SqlCommand com = new SqlCommand("insert into [Inboxes](UserID, SenderID, MessageID, DateReceived, MessageText) values (@UserID,@SenderID, @MessageID,@DateReceived,@MessageText)", conn);
                com.Parameters.AddWithValue("@UserID", rID);
                com.Parameters.AddWithValue("@SenderID", sID);
                com.Parameters.AddWithValue("@MessageID", messageID);
                com.Parameters.AddWithValue("@DateReceived", sentAt);
                com.Parameters.AddWithValue("@MessageText", messageText);

                com.ExecuteNonQuery();

                conn.Close();

        }
    
        //takes in the messageID and uses this to delete the message from the database
        public static void deleteMessage(int messageID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            
            SqlCommand cmd = new SqlCommand("Delete from Inboxes where MessageID = @MessageID;", conn);
            cmd.Parameters.AddWithValue("@MessageID", messageID);
            cmd.ExecuteNonQuery();
            
            conn.Close();
            
        }
    }
}