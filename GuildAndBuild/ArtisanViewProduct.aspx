<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ArtisanViewProduct.aspx.cs" Inherits="GuildAndBuild.ArtisanViewProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div style:"height:100%; width:100%" style="height: 855px; width: 735px">
        <div style="height: 390px; width: 426px; float:right; margin-left: 0px;">
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
            <asp:Label ID="NoFavs" runat="server"></asp:Label>
        </div>
        <div style="height: 389px; width: 306px; float:right;">

            <img alt="" runat="server" src="item.jpg" style="height: 300px; width: 272px" id="ProductPhoto" /><br />
            <br />
            <asp:SqlDataSource ID="reviewSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [ReviewID], [ReviewDate], [Reviewtitle], [ReviewText], [OverallRating] FROM [Reviews] WHERE ([ProductID] = @ProductID)">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ProductID" QueryStringField="id" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

            </div>
        <div style="height: 187px; width: 670px;">
            <asp:GridView ID="ReviewDetail" runat="server" Height="16px" Width="817px" AutoGenerateColumns="False" DataSourceID="reviewSource" AllowPaging="True" PageSize="3" style="margin-right: 0px; margin-top: 3px" >
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
    </div>
</asp:Content>
