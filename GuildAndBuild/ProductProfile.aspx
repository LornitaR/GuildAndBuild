<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProductProfile.aspx.cs" Inherits="GuildAndBuild.ProductProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div style:"height:100%; width:100%" style="height: 1252px; width: 735px">
        <div style="height: 518px; width: 426px; float:right; margin-left: 0px;">
            <asp:Label ID="productTitle" runat="server" Font-Bold="True" Font-Names="bodoni mt" Font-Size="X-Large"></asp:Label>
            <br />
            <br />
            <asp:Label ID="guild" runat="server" Font-Names="Trebuchet MS" Font-Size="Large"></asp:Label>
&nbsp;&gt;
            <asp:Label ID="type" runat="server" Font-Names="Trebuchet MS" Font-Size="Large"></asp:Label>
&nbsp;&gt;
            <asp:Label ID="material" runat="server" Font-Names="Trebuchet MS" Font-Size="Large"></asp:Label>
            <br />
            <asp:HyperLink ID="ShopLink" runat="server">[ShopLink]</asp:HyperLink>
            <br />
            <br />
            <asp:Literal ID="productDescription" runat="server" Mode="PassThrough"></asp:Literal>
            <br />
            <br />
            <asp:Label ID="starlbl" runat="server" Text="Star Rating:"></asp:Label>
            <asp:Image ID="starImage" runat="server" Height="20px" Width="111px" />
            <br />
            <asp:Label ID="reviewCount" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="productPrice" runat="server" Font-Names="Trebuchet MS"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Quantitylbl" runat="server"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="quanBox" runat="server" MaxLength="2" Width="16px"></asp:TextBox>
            <asp:Label ID="quanLbl" runat="server" Text="Quantity"></asp:Label>
            <br />
            <br />
            <asp:Label ID="NoFavs" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="cartBtn" runat="server" Text="Add to Cart" OnClick="cartBtn_Click" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="buyBtn" runat="server" Text="Buy" OnClick="buyBtn_Click" Height="25px" Width="64px" />
            <br />
            <br />
            <asp:Button ID="reviewBtn" runat="server" Text="Review Me" OnClick="reviewBtn_Click" Visible="False" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="FavouriteBtn" runat="server" Text="Favourite" OnClick="FavouriteBtn_Click" Visible="False" />
            <br />
            <br />
            <br />
            <asp:Label ID="TitleLabel" runat="server" Font-Bold="True" Font-Size="Larger" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Literal ID="TextLabel" runat="server" Mode="PassThrough"></asp:Literal>
            <br />
            <br />
            <br />
            <asp:Button ID="ReviewDelete" runat="server" OnClick="ReviewDelete_Click" Text="Delete Review" Visible="False" />
        &nbsp;
            <asp:Button ID="editReview" runat="server" Text="Edit Review" Visible="False" OnClick="editReview_Click" />
        </div>

        <div style:"height:100%; width:100%" style="height: 425px; float:left; width: 276px; margin-left: 0px; margin-right: 0px;">

            <img alt="" runat="server" src="item.jpg" style="height: 300px; width: 272px" id="ProductPhoto" /></div>

        
        <div style="height: 364px; width: 735px; margin-left: 0px;">
            <asp:SqlDataSource ID="reviewSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ReviewID, ReviewDate, Reviewtitle, ReviewText, OverallRating FROM Reviews WHERE (ProductID = @ProductID) ORDER BY ReviewDate DESC">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ProductID" QueryStringField="id" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:GridView ID="ReviewDetail" runat="server" Height="142px" Width="817px" AutoGenerateColumns="False" DataSourceID="reviewSource" AllowPaging="True" PageSize="3" style="margin-right: 0px; margin-top: 2px" >
                <Columns>
                    <asp:TemplateField SortExpression="ReviewText" HeaderText="Review Text">
                        <ItemTemplate>
                            <div style="text-align:center;">
                            <asp:Label ID="Date" runat="server" Text='<%# "Review Date: " + Eval("ReviewDate") %>'></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="Title" runat="server" Text='<%# "Title: " + Eval("Reviewtitle") %>'></asp:Label>
                            &nbsp;&nbsp;<asp:Label ID="rating" runat="server" Text='<%# "Rating: " + Eval("OverallRating") + "/5" %>'></asp:Label>
                                <br />
                                <br />
                            <br />
                                <asp:Literal ID="textLit" runat="server" Mode="PassThrough" Text='<%# Eval("ReviewText") %>'></asp:Literal>
                            <br />
                            </div>
                        </ItemTemplate>
                        <ControlStyle Width="200px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div style="height: 317px; width: 739px">

            <br />
            <asp:TextBox ID="TitleTextBox" runat="server" Visible="False" MaxLength="50"></asp:TextBox>
            <br />
            <br />
            <textarea id="ReviewTextBox" Visible="False" placeholder="Enter a review here. No longer than 400 characters." runat="server" name="S1" style="height: 228px; width: 708px; margin-top: 62px;" maxlength="385"></textarea><br />
            <asp:Button ID="ClearAll" runat="server" Text="Clear All" OnClick="ClearAll_Click" />

        &nbsp; <asp:Button ID="SubReview" runat="server" Text="Submit Review" OnClick="SubReview_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="RatingLabel" runat="server" Text="Overall Rating" Visible="False"></asp:Label>
&nbsp;<asp:DropDownList ID="RatingDDL" runat="server" Visible="False">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <br />
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
        </div>
</div>
</asp:Content>
