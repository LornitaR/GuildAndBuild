using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{
    public partial class Inbox : System.Web.UI.Page
    {

        
        string Username;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Users"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                string querySender = Request.QueryString["sender"];

                if (querySender != null)
                {
                    txtTo.Text = querySender;
                }

                Username = Request.Cookies["Users"]["Username"];
                grdMessages.DataSource = MessageAccess.GetAllMessages(Username);//Method in MessageAccess.cs
                grdMessages.DataBind();
                grdSent.DataSource = MessageAccess.GetSentMessages(Username);//Method in MessageAccess.cs
                grdSent.DataBind();

            }
        }
 

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string sendTo = txtTo.Text;
            DateTime sentAt = DateTime.Now;
            string senderName = Username;
            string messageText = txtMessageText.Text;
            bool valid = UserAccess.checkValidUser(txtTo.Text);//Method in User.cs
            if (valid)
            {
                MessageAccess.createMessage(sendTo, senderName, sentAt, messageText);//Method in MessageAccess.cs
                txtTo.Text = "";
                txtMessageText.Text = "";
                Response.Redirect("Inbox.aspx");
                lblSent.Text = "Message sent successfully!";
            }

            else
            {
               
                lblSent.Text = "Username not valid";
                lblSent.ForeColor = System.Drawing.Color.Red;
            }
            
        }

        protected void Inbox_DeleteMessage(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "DeleteRow")
            {
                MessageAccess.deleteMessage(Convert.ToInt32(e.CommandArgument));//Method in MessageAccess.cs
                grdMessages.DataBind();
                Response.Redirect("Inbox.aspx");
                
            }
            
        }

        protected void Sent_DeleteMessage(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteSentRow")
            {
                MessageAccess.deleteMessage(Convert.ToInt32(e.CommandArgument));//Method in MessageAccess.cs
                grdSent.DataBind();
                Response.Redirect("Inbox.aspx");
            }         
                       
        }

        public void Delete_row(object sender, GridViewDeleteEventArgs e)
        {
            
        
        }

        public void Delete2_row(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        
        //Takes username from the cookie
        //Loads messages that have been sent to them
        protected void ChangeInboxPage(object sender, GridViewPageEventArgs e)
        {
            grdMessages.DataSource = MessageAccess.GetAllMessages(Username);//Method in MessageAccess.cs
            grdMessages.PageIndex = e.NewPageIndex;
            grdMessages.DataBind();
        }

        //Takes username from the cookie
        //Loads messages they've sent to others
        protected void ChangeSentPage(object sender, GridViewPageEventArgs e)
        {
            grdSent.DataSource = MessageAccess.GetSentMessages(Username);//Method in MessageAccess.cs
            grdSent.PageIndex = e.NewPageIndex;
            grdSent.DataBind();
        }

    }
}