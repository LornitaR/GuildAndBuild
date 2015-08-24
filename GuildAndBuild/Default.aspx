<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GuildAndBuild.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
     <div class="pictureWall">
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
</asp:Content>
