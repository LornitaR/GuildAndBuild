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
    public partial class MyProjects : System.Web.UI.Page
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
            ProjectList.DataSource = ProjectDataAccessLayer.GetMyProjects(userID);
            ProjectList.AllowPaging = true;
            ProjectList.AllowSorting = true;
            ProjectList.PageSize = 4;
            ProjectList.DataBind();
            ProjectList.DataBind();
        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewProject.aspx");
        }

        /*
         *      Contains the various commands that are fired by the grid view 
         */
        protected void ProjectList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Delete")
            {
                ProjectDataAccessLayer.DeleteProject(Convert.ToInt32(e.CommandArgument.ToString()));
                BindGridViewData();
            }
        }

        public void ProjectList_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string user = Request.Cookies["Users"]["UserID"];
            ProjectList.DataSource = ProjectDataAccessLayer.GetMyProjects(user);
            ProjectList.PageIndex = e.NewPageIndex;
            ProjectList.DataBind();
        }

    }
}