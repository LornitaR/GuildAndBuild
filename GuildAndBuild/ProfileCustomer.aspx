<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProfileCustomer.aspx.cs" Inherits="GuildAndBuild.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <p style="text-align:center"><asp:Label ID="lblUserName" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT"></asp:Label></p>
    <div id="profileLayout">
        <div id="left" class="column" style="width: 20%">
                <asp:Image ID="imgProfilePhoto" Width= "160px" Height = "160px" runat="server" ImageUrl='<%# Bind("photo") %>'/>
&nbsp;<div style="height: 100%; width: 60%;"height: 30%; width: 10%;">
                
                <p>Interests:
                    <asp:Label ID="lblInterests" runat="server"></asp:Label>
                    </p>
            </div>

        </div>
        <div id="right" class="column pictureWall" width: 40%;>
            <div style="padding-bottom: 5%;">
                <ul style="width: 100%;">
                    <a href=Browse.aspx?guild=1><li><p>Art</p><img src="/images/byanyOtherName.jpg"></li></a>
                    <a href=Browse.aspx?guild=2><li><p>Woodwork</p><img src="/images/table.jpg"></li></a>
                    <a href=Browse.aspx?guild=3><li><p>Metalwork</p><img src="/images/fire-pits.jpg"></li></a>
                    <a href=Browse.aspx?guild=4><li><p>Textiles</p><img src="/images/textile.jpg"></li></a>
                    <a href=Browse.aspx?guild=5><li><p>Jewellery</p><img src="/images/jewellery.jpg"></li></a>
                    <a href=Browse.aspx?guild=6><li><p>Masonry</p><img src="/images/sculpt.jpg"></li></a>
                    <a href=Browse.aspx?guild=7><li><p>Pottery</p><img src="/images/cool.jpg"></li></a>
                    <a href=Browse.aspx?guild=8><li><p>Glass</p><img src="/images/swirl.jpg"></li></a>
                </ul>
            </div>
        </div>
        <div style="width: 195px; height: 190px; padding-top:10%; float:right;">
        </div>
        <div style="width: 206px; height: 188px">
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Bidding Centre" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="My Bids" />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Statistics" />
        </div>
    </div>
</asp:Content>
