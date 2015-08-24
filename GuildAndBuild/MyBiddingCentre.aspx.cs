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
    public partial class MyBiddingCentre : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Cookies["Users"] == null)
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
            string userID = Request.Cookies["Users"]["UserID"];
            BidList.DataSource = ProjectDataAccessLayer.GetMyBids(userID);
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

            if (e.CommandName == "Accept")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;       //Same as above
                string[] arg = new string[3];
                arg = e.CommandArgument.ToString().Split(';');
                int ProjectIDvalue = Convert.ToInt32(arg[0]);
                int ArtisanID = Convert.ToInt32(arg[2]);
                int BidID = Convert.ToInt32(arg[1]);
                string ProjectID = arg[0].ToString();                           
                ProjectDataAccessLayer.ChangeBidStatus(BidID, ArtisanID, ProjectIDvalue);
                ShoppingCartProject.Instance.AddItem(ProjectID);
                Response.Redirect("MyCart.aspx");
                
            }
        }

        protected void BidList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string userID = Request.Cookies["Users"]["UserID"];
            BidList.DataSource = ProjectDataAccessLayer.GetMyBids(userID);
            BidList.PageIndex = e.NewPageIndex;
            BidList.DataBind();
        }
    }
}