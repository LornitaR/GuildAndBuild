<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Storefront.aspx.cs" Inherits="GuildAndBuild.Storefront" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div style="width:128%; height:1091px;">
    <div style="width:21%; height:321px; float:left; margin-left: 120px;">
        <div style="float:left; width: 270px; height: 314px;">
        <asp:Image ID="ProfileImage" runat="server" Height="232px" Width="235px" style="margin-left: 0px" />
        <br />
        <br />
        <asp:Button ID="contactBtn" runat="server" Text="Contact" OnClick="contactBtn_Click" />
&nbsp;&nbsp;
            <asp:Button ID="proposeBtn" runat="server" OnClick="proposeBtn_Click" Text="Propose Project" />
        <asp:Button ID="showReviews" runat="server" Text="Show/Hide Reviews" OnClick="showReviews_Click" Width="142px" />
&nbsp;&nbsp;
        <br />
        </div>

    </div>
    <div style="float:left; height: 321px; width: 669px; text-align:left;">

        <asp:Label ID="ShopNameLbl" runat="server" Font-Names="bodoni mt" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <asp:Label ID="guildType" runat="server" Font-Names="Trebuchet MS"></asp:Label>

        &nbsp;&nbsp;
        <asp:Label ID="Locationlbl" runat="server"></asp:Label>
        <br />

        <br />
        <br />

        <asp:Literal ID="Descriptionlbl" runat="server"></asp:Literal>

    </div>
    <div style="height: 445px; width: 634px;">



            <asp:DataList ID="productList" runat="server" BorderColor="#3366CC" BorderWidth="1px" GridLines="Both" style="margin-left: 192px; margin-right: 0px; margin-top: 30px;" Height="225px" Width="852px" OnItemCommand="productList_ItemCommand" BackColor="White" BorderStyle="None" CellPadding="4" OnItemDataBound="productList_ItemDataBound">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <ItemStyle BackColor="White" ForeColor="#003399" />
                <ItemTemplate>
                    <div style="height: 219px; width: 621px;">
                        <div style="float: left; height: 217px; width: 210px;">
                            <asp:Image ID="Image1" runat="server" Height="215px" ImageUrl='<%# Eval("PhotoLocation") %>' Width="215px" />
                        </div>
                        <div style="float: right; height: 215px; width: 403px;">
                            <asp:HyperLink ID="productName" runat="server" Font-Names="bodoni mt" Font-Size="X-Large" NavigateUrl='<%# Eval("productID", "ProductProfile.aspx?id={0}") %>' Text='<%# Eval("ProductName") %>'></asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="DateLbl" runat="server" Text='<%# Eval("DateAdded") %>'></asp:Label>
                            <br />
                            <asp:Label ID="ProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="ProDescription0" runat="server" Text='<%# Eval("ProductDescription") %>'></asp:Label>
                            <br />
                            <asp:Label ID="ProPrice0" runat="server" Text='<%# "€" + Eval("ProductCost") %>'></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Button ID="addToCart" runat="server" Text="Add to Cart" CommandName="AddToCart" CommandArgument='<%# Eval("ProductID") %>' />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="buyBtn" runat="server" Text="Buy" CommandName="Buy" CommandArgument='<%# Eval("ProductID") %>' OnClick="buyBtn_Click" />
                            <br />
                        </div>
                    </div>
                </ItemTemplate>
                <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            </asp:DataList>
                <div style="text-align: center; margin-top: 11px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="linkBtnPrevious" runat="server" OnClick="linkBtnPrevious_Click">Previous</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="linkBtnNext" runat="server" OnClick="linkBtnNext_Click">Next</asp:LinkButton>

                    
                    <asp:GridView ID="reviewGridview" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="reviewGrid" Height="29px" PageSize="3" style="margin-top: 9px; margin-left: 0px; margin-right: 0px;" Width="829px" Visible="False">
                <Columns>
                    <asp:ImageField DataImageUrlField="PhotoLocation">
                        <ControlStyle Height="120px" />
                    </asp:ImageField>
                    <asp:TemplateField HeaderText="Review Text">
                        <ItemTemplate>
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="productName" runat="server" Text='<%# "Product Name: " + Eval("ProductName") %>'></asp:Label>
&nbsp;
                            <asp:Label ID="title" runat="server" Text='<%# "Review Title: " + Eval("Reviewtitle") %>'></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="rating" runat="server" Text='<%# "Rating: " + Eval("OverallRating") + "/5" %>'></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="reviewID" runat="server" Text='<%# "Review ID: " + Eval("ReviewID") %>'></asp:Label>
                            <br />
                            <br />
                            <asp:Literal ID="text" runat="server" Mode="PassThrough" Text='<%# Eval("ReviewText") %>'></asp:Literal>
                        </ItemTemplate>
                        <ControlStyle Width="400px" />
                        <ItemStyle Width="800px" />
                    </asp:TemplateField>
                </Columns>
                        <RowStyle Width="400px" Wrap="True" />
            </asp:GridView>
            <asp:SqlDataSource ID="reviewGrid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Products.ProductName, Reviews.ReviewID, Reviews.Reviewtitle, Reviews.ReviewText, Reviews.OverallRating, Products.ProductID, ProductPhotos.PhotoLocation, Reviews.ReviewDate FROM Products INNER JOIN Reviews ON Products.ProductID = Reviews.ProductID INNER JOIN ProductPhotos ON Products.ProductID = ProductPhotos.ProductID WHERE (Products.ArtisanID = (SELECT ArtisanID FROM Products AS Products_1 WHERE (ProductID = @ProductID)))">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ProductID" QueryStringField="id" />
                </SelectParameters>
            </asp:SqlDataSource>



                    
        
            </div>
            <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
    </div>
    <div style="height: 69px; width: 741px">

   </div>

    </div>
</asp:Content>
