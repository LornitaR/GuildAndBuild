<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="GuildAndBuild.Inbox" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="mainContent">
  <div style="height: 1211px; width: 890px">

   <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Inbox"></asp:Label></p>
   
     
       <asp:Label ID="lblInbox" runat="server" Font-Bold="True" Font-Names="Bodoni MT" Font-Size="Large">Received Items</asp:Label>
    <asp:GridView ID="grdMessages" runat="server" 
        AutoGenerateColumns="False" 
        OnRowCommand="Inbox_DeleteMessage"
        OnPageIndexChanging="ChangeInboxPage" 
        OnRowDeleting="Delete_row" AllowPaging="True" Width="663px" PageSize="5" style="margin-right: 1px" EmptyDataText="No messages" >
        <Columns>
            <asp:BoundField DataField="SenderName" HeaderText="From" />
            <asp:BoundField DataField="MessageText" HeaderText="Message" />
            <asp:BoundField DataField="DateReceived" HeaderText="Date Received" />
            <asp:BoundField DataField="MessageID" HeaderText="MessageID" Visible="False" />
        
         <asp:TemplateField>   
            <ItemTemplate>
                <asp:LinkButton ID ="lblDeleteInbox" CommandArgument='<%# Eval("MessageID") %>' CommandName="DeleteRow" runat="server" CausesValidation="False">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
      <br />
      <br />

     <asp:Label ID="lblTo" runat="server" Text="To: "></asp:Label>
        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="NameRequired" runat="server" ErrorMessage="Must enter username" ControlToValidate="txtTo" ValidationGroup="SendMessage"></asp:RequiredFieldValidator>
         <br />
        <asp:Label ID="lblSent" runat="server" ForeColor="#006600"></asp:Label>
         <asp:Label ID="lblInvalidName" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblMessageText" runat="server" Text="Message"></asp:Label><br />
        <asp:TextBox ID="txtMessageText" runat="server" Height="130px" Width="483px" ValidationGroup="Group1" MaxLength="400" TextMode="MultiLine"></asp:TextBox><br />
        <asp:Button ID="btnCreate" runat="server" Text="Send Message" OnClick="btnCreate_Click" ValidationGroup="Group1" />
         <asp:RequiredFieldValidator ID="TextRequired" runat="server" ErrorMessage="Must enter message text" ValidationGroup="Group1" ControlToValidate="txtMessageText"></asp:RequiredFieldValidator>
      <br />
      <br />
    
    
     <asp:Label ID="lblSentItems" runat="server" Font-Bold="True" Font-Names="Bodoni MT" Font-Size="Large">Sent Items</asp:Label>
    <asp:GridView ID="grdSent" runat="server" 
        AutoGenerateColumns="False" 
        OnPageIndexChanging="ChangeSentPage" 
        OnRowCommand="Sent_DeleteMessage" 
        OnRowDeleting="Delete2_row" AllowPaging="True" PageSize="5" Width="675px" EmptyDataText="No sent messages">
        <Columns>
            <asp:BoundField DataField="Username" HeaderText="Sent To" />
            <asp:BoundField DataField="MessageText" HeaderText="Message" />
            <asp:BoundField DataField="DateReceived" HeaderText="Date Received" />
            <asp:BoundField DataField="MessageID" HeaderText="MessageID" Visible="False" />
        
        <asp:TemplateField>   
            <ItemTemplate>
                <asp:LinkButton ID ="lblDeleteSent" CommandArgument='<%# Eval("MessageID") %>' CommandName="DeleteSentRow" runat="server" CausesValidation="False">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
         
    </asp:GridView>
    </div>
       
    </asp:Content>

