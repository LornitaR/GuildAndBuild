<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="GuildAndBuild.ContactUs" MaintainScrollPositionOnPostBack="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Contact Us" ></asp:Label></p>
    <asp:Label ID="lblUnregistered" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Width="60%" style="margin-left: 160px;" Visible="false"
        Text="E-mail us with any queries at admin@guildNbuild.com. Alternatively you can call us on (+353)847896070. If you are registered, log in to contact us with any issues."></asp:Label>
    <p style="float: right; width:80%; margin-left: 20%;">
        <asp:Label ID="lblIssues" runat="server" Text="Issues:" Font-Size="X-Large" Font-Names="Bodoni MT" Width="200px" Visible="false"></asp:Label> 
        <asp:DropDownList ID="ddlIssues" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" AutoPostBack="True" Visible="false"></asp:DropDownList><br />
        <asp:Label ID="lblMessage" runat="server" Text="Message:" Font-Size="X-Large" Font-Names="Bodoni MT" Width="200px" Visible="false"></asp:Label>
        <asp:TextBox ID="txtMessage" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="168px" Width="55%" MaxLength="300" Visible="false" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p style="float: right; width:38%; margin-left: 62%;">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Size="X-Large" Font-Names="Bodoni MT" OnClick="btnSubmit_Click" Visible="false" />
    </p><br /><br />
    <asp:Label ID="lblResult" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Width="60%" style="margin-left: 160px;"></asp:Label>
</asp:Content>
