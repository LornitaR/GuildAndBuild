using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildAndBuild
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( Request.Cookies["Users"] == null || Convert.ToInt32(Request.Cookies["Users"]["UserType"]) == 1)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    btnPie.Click += new EventHandler(this.btnPie_Click);
                    btnBar.Click += new EventHandler(this.btnBar_Click);
                    btnGraph.Click += new EventHandler(this.btnGraph_Click);
                }
            }
        }

        public void btnPie_Click(object sender, EventArgs e)
        {
                this.Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
                chartBind();
        }

        public void btnBar_Click(object sender, EventArgs e)
        {
            this.Chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
            chartBind();
        }

        public void btnGraph_Click(object sender, EventArgs e)
        {
            this.Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            chartBind();
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* The following method passes in the sql required to populate the dropdown menus */

        protected void ddlUserInfo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartBind();

        }

        private void chartBind()
        {
            string artisan = Request.Cookies["Users"]["UserID"];
            int ArtisanID = Convert.ToInt32(artisan);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            if (ddlUserInfo1.SelectedValue == "1")
            {
                SqlCommand Gender = new SqlCommand("select gender, sum(Quantity) AS TotalSales from PurchaseInfo Where ArtisanID = @ArtisanID group by Gender;", conn);
                Series series = Chart1.Series["Series1"];
                Gender.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                SqlDataReader rdr = Gender.ExecuteReader();
                while (rdr.Read())
                {
                    series.Points.AddXY(rdr["gender"].ToString(), rdr["TotalSales"]);
                }
                Chart1.DataBind();
            }

            else if (ddlUserInfo1.SelectedValue == "2")
            {
                SqlCommand Location = new SqlCommand("select LocationName, sum(Quantity) AS TotalSales from PurchaseInfo Where ArtisanID = @ArtisanID group by LocationName;", conn);
                Series series = Chart1.Series["Series1"];
                Location.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                SqlDataReader rdr = Location.ExecuteReader();
                while (rdr.Read())
                {
                    series.Points.AddXY(rdr["LocationName"].ToString(), rdr["TotalSales"]);
                }
                Chart1.DataBind();
            }

            else if (ddlUserInfo1.SelectedValue == "3")
            {
                SqlCommand Guild = new SqlCommand("select MaterialTypeName, sum(Quantity) AS TotalSales from PurchaseInfo Where ArtisanID = @ArtisanID group by MaterialTypeName;", conn);
                Guild.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                Series series = Chart1.Series["Series1"];
                SqlDataReader rdr = Guild.ExecuteReader();
                while (rdr.Read())
                {
                    series.Points.AddXY(rdr["MaterialTypeName"].ToString(), rdr["TotalSales"]);
                }
                Chart1.DataBind();
            }

            else if (ddlUserInfo1.SelectedValue == "4")
            {
                SqlCommand Materals = new SqlCommand("select TypeName, sum(Quantity) AS TotalSales from PurchaseInfo Where ArtisanID = @ArtisanID group by TypeName;", conn);
                Materals.Parameters.AddWithValue("@ArtisanID", ArtisanID);
                Series series = Chart1.Series["Series1"];
                SqlDataReader rdr = Materals.ExecuteReader();
                while (rdr.Read())
                {
                    series.Points.AddXY(rdr["TypeName"].ToString(), rdr["TotalSales"]);
                }
                Chart1.DataBind();
            }

            conn.Close();
        }
    }
}

