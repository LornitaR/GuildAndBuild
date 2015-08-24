<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="GuildAndBuild.Admin" MaintainScrollPositionOnPostBack="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server" ViewStateMode="Inherit">

    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Administration"></asp:Label></p>
    <div id="dialog" style="display: none">
        <asp:Label ID="lblInvisibleUsername" runat="server" Visible="False"></asp:Label>
    </div> 

    <asp:GridView ID="GridViewComplaints" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="6" AutoGenerateSelectButton="True" SelectedIndex="0" DataSourceID="ComplaintsDataSource" DataKeyNames="Username" 
        OnPageIndexChanging="GridViewComplaints_PageIndexChanging" OnSelectedIndexChanged="GridViewComplaints_SelectedIndexChanged" OnSelectedIndexChanging="GridViewComplaints_SelectedIndexChanging" Width="100%" Font-Size="Medium" Font-Names="Trebuchet MS">
        <Columns>
            <asp:BoundField DataField="Username" HeaderText="Username" />
            <asp:BoundField DataField="ComplaintDate" HeaderText="Date" SortExpression="ComplaintDate" />
            <asp:BoundField DataField="ComplaintTypeName" HeaderText="Issue" SortExpression="ComplaintTypeName" />
            <asp:BoundField DataField="ComplaintText" HeaderText="Message" SortExpression="ComplaintText" ItemStyle-Width="50%" ><ItemStyle Width="50%"></ItemStyle></asp:BoundField>
        </Columns>
        <selectedrowstyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true"/>
    </asp:GridView><br />

    <asp:Label ID="lblSolveIssue" runat="server" Text="Solve Issue" Font-Size="Large" Font-Names="Trebuchet MS"></asp:Label><br />
    <asp:TextBox ID="txtSolveIssue" runat="server" TextMode="MultiLine" Width="50%" Font-Size="Medium" Font-Names="Trebuchet MS"></asp:TextBox><br />
    <asp:Button ID="btnSolveComplaint" runat="server" Text="Send" Font-Size="Medium" Font-Names="Trebuchet MS" OnClick="btnSolveComplaint_Click" />
    <asp:Label ID="lblStatus" runat="server" Font-Size="Large" Font-Names="Trebuchet MS" ForeColor="#009900"></asp:Label><br /><br />
  
    <asp:TextBox ID="txtBan" runat="server"></asp:TextBox><br />
    <asp:Button ID="btnBanUser" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" OnClick="btnBanUser_Click" Text="Ban User" />
    <asp:Label ID="lblDeletedUser" runat="server" Font-Size="Large" Font-Names="Trebuchet MS" ForeColor="#009900"></asp:Label><br />

    <asp:SqlDataSource ID="ComplaintsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT Username, ComplaintDate, ComplaintTypeName, ComplaintText FROM [Complaints] c, [Users] u, [ComplaintTypes] t WHERE c.UserID = u.UserID AND c.ComplaintID = t.ComplaintTypeID"></asp:SqlDataSource>

</asp:Content>
