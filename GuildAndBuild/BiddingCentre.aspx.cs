using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{
    public partial class BiddingCentre : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Users"] == null || Convert.ToInt32(Request.Cookies["Users"]["UserType"]) == 1)
            {
                Response.Redirect("Default.aspx");
            }

            else
            {

                if (!IsPostBack)
                {
                    BindGridViewData();

                }
            }
        }

        private void BindGridViewData()
        {
            string interests = Request.Cookies["Users"]["Interest"];
            string user = Request.Cookies["Users"]["UserID"];
            int userID = Convert.ToInt32(user);
            BidList.DataSource = ProjectDataAccessLayer.GetAllProjects(interests, userID);
            BidList.AllowPaging = true;
            BidList.AllowSorting = true;
            BidList.PageSize = 4;
            BidList.DataBind();
        }

        /*
         *      Contains the various commands that are fired by the grid view 
         */
        protected void BidList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")                     // e.CommandName is a property of the GridViewCommandEventArgs
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;   // TypeCast the CommandSource to LinkButton. namingContainer will return the gridviewrow which was clicked as an object, which is then cast as a GridViewRow. Then .RowIndex will return the actual row. Tricky!
                BidList.EditIndex = rowIndex;               // Edit index is a property of the grid view control that will set the row in edit mode
                BindGridViewData();

            }

            else if (e.CommandName == "CancelUpdate")
            {
                BidList.EditIndex = -1;         // Set the EditIndex to -1 in order to remove the edit selection. This works because there is no row -1 to be put into edit mode.
                BindGridViewData();
            }

            else if (e.CommandName == "UpdateRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;       //Same as above

                int ProjectID = Convert.ToInt32(e.CommandArgument);
                double BidPrice = Double.Parse(((TextBox)BidList.Rows[rowIndex].FindControl("TextBoxPrice")).Text); //Convert to double
                string BidText = ((TextBox)BidList.Rows[rowIndex].FindControl("TextBoxProposal")).Text;
                int BidStatus = 3;  // sets the bid status to pending
                string UserID = Request.Cookies["Users"]["UserID"]; // Have to get artisanID(UserID)
                int ArtisanID = Convert.ToInt32(UserID);
                int BidID = ProjectDataAccessLayer.newBidID(); // This will return an int for BidID (it's the old BidID + 1)
                ProjectDataAccessLayer.MakeBid(BidID, ArtisanID, BidText, BidPrice, BidStatus, ProjectID);

                BidList.EditIndex = -1;
                BindGridViewData();
            }
        }

       protected void BidList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string interests = Request.Cookies["Users"]["Interest"];
            string user = Request.Cookies["Users"]["UserID"];
            int userID = Convert.ToInt32(user);
            BidList.DataSource = ProjectDataAccessLayer.GetAllProjects(interests, userID);
            BidList.PageIndex = e.NewPageIndex;
            BidList.DataBind();
        }
    }
}
