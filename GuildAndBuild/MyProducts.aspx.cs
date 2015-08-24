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
    public partial class MyProducts : System.Web.UI.Page
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
            string artisan = Request.Cookies["Users"]["UserID"];
            int ArtisanID = Convert.ToInt32(artisan);
            ProductList.DataSource = ProductDataAccessLayer.GetAllProducts(ArtisanID);
            ProductList.AllowPaging = true;
            ProductList.AllowSorting = true;
            ProductList.PageSize = 5;
            ProductList.DataBind();
        }

        /*
         *      Contains the various commands that are fired by the grid view 
         */
        protected void ProductList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")                     // e.CommandName is a property of the GridViewCommandEventArgs
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;   // TypeCast the CommandSource to LinkButton. namingContainer will return the gridviewrow which was clicked as an object, which is then cast as a GridViewRow. Then .RowIndex will return the actual row. Tricky!
                ProductList.EditIndex = rowIndex;               // Edit index is a property of the grid view control that will set the row in edit mode
                //dropdownlist ddltype = new dropdownlist();            // the following lines were supposed to call a method to bind the dropdown list on row edit, but i couldn't get it to work.
                //ddltype = (dropdownlist)productlist.rows[rowindex].findcontrol("ddltype"); // you changed here
                //bindddltype(ddltype);
                //dropdownlist ddlmaterial = new dropdownlist();
                //ddlmaterial = (dropdownlist)productlist.rows[rowindex].findcontrol("ddlmaterial"); // and you changed here
                //BindddlMaterial(ddlMaterial);
                BindGridViewData();

            }
            else if (e.CommandName == "DeleteRow")
            {
                ProductDataAccessLayer.DeleteProduct(Convert.ToInt32(e.CommandArgument));
                BindGridViewData();
            }
            else if (e.CommandName == "CancelUpdate")
            {
                ProductList.EditIndex = -1;         // Set the EditIndex to -1 in order to remove the edit selection. This works because there is no row -1 to be put into edit mode.
                BindGridViewData();
            }
            else if (e.CommandName == "UpdateRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;       //Same as above

                int productID = Convert.ToInt32(e.CommandArgument);
                //string productPhoto = ;
                string productName = ((TextBox)ProductList.Rows[rowIndex].FindControl("TextBoxName")).Text;
                double productCost = Double.Parse(((TextBox)ProductList.Rows[rowIndex].FindControl("TextBoxCost")).Text); //Convert to double
                int productQuantity = Convert.ToInt32(((TextBox)ProductList.Rows[rowIndex].FindControl("TextBoxQuan")).Text); //Convert to int
                string productDescription = ((TextBox)ProductList.Rows[rowIndex].FindControl("TextBoxDesc")).Text;
                ProductDataAccessLayer.UpdateProduct(productID, productName, productCost, productQuantity, productDescription);

                ProductList.EditIndex = -1;
                BindGridViewData();
            }
        }

        protected void ProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string artisan = Request.Cookies["Users"]["UserID"];
            int ArtisanID = Convert.ToInt32(artisan);
            ProductList.DataSource = ProductDataAccessLayer.GetAllProducts(ArtisanID);
            ProductList.PageIndex = e.NewPageIndex;
            ProductList.DataBind();
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewProduct.aspx");
        }
        
    }
}
