<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="GuildAndBuild.Browse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
    <br />
    <div id="dropDownBrowse">
        <asp:DropDownList ID="guildDrop" runat="server" Height="20px" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="guildDrop_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <asp:DropDownList ID="typesDrop" runat="server" Height="20px" Width="150px" Visible="False" OnSelectedIndexChanged="typesDrop_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="materialsDrop" runat="server" Height="20px" Width="150px" Visible="False" OnSelectedIndexChanged="materialsDrop_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="guildDrop" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        
        
        <asp:DropDownList ID="orderByDrop" runat="server" Height="20px" Width="150px" OnSelectedIndexChanged="orderByDrop_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="0">Newest</asp:ListItem>
            <asp:ListItem Value="1">Oldest</asp:ListItem>
            <asp:ListItem Value="2">Cheapest</asp:ListItem>
            <asp:ListItem Value="3">Most Expensive</asp:ListItem>
            <asp:ListItem Value="5">Alpabetical Ascending</asp:ListItem>
            <asp:ListItem Value="6">Alphabetical Descending</asp:ListItem>
        </asp:DropDownList>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:GridView ID="browseGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="8" Width="100%" OnPageIndexChanging="browseGrid_PageIndexChanging" OnSelectedIndexChanged="browseGrid_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="typesDrop" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="materialsDrop" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="orderByDrop" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="browseGrid" EventName="PageIndexChanging" />
        </Triggers>
        </asp:UpdatePanel>
    <br />
    <br />

</asp:Content>
