<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="GuildAndBuild.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
        <div class="searchDiv" style="height: 230px; margin-left: 20%">
             <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
            Search by Product Name: <asp:TextBox runat="server" ID="searchTB" AutoCompleteType="Search" MaxLength="25"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        Advanced Search: <asp:CheckBox runat="server" ID="advancedSearch" OnCheckedChanged="advancedSearch_CheckedChanged" AutoPostBack="True" />
        <br />
        <br />
        <asp:Label ID="artisansLbl" runat="server" Text="Search by Shop-Names:" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="artisanSearchTB" runat="server" MaxLength="25" Visible="False"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="searchBtn" runat="server" Text="Search!" OnClick="searchBtn_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:DropDownList ID="guildDrop" runat="server" Height="20px" Visible="False" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="guildDrop_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="typesDrop" runat="server" Height="20px" Visible="False" Width="150px">
        </asp:DropDownList>
        <asp:DropDownList ID="materialsDrop" runat="server" Height="20px" Visible="False" Width="146px">
        </asp:DropDownList>
            <asp:DropDownList ID="orderByDrop" runat="server" Height="20px" Visible="False" Width="147px">
            <asp:ListItem Value="0">Newest</asp:ListItem>
            <asp:ListItem Value="1">Oldest</asp:ListItem>
            <asp:ListItem Value="2">Cheapest</asp:ListItem>
            <asp:ListItem Value="3">Most Expensive</asp:ListItem>
            <asp:ListItem Value="4">Alpabetical Ascending</asp:ListItem>
            <asp:ListItem Value="5">Alphabetical Descending</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="priceRangeLbl" runat="server" Text="Price range: Between" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="lowPriceRange" runat="server" Visible="False" Width="41px" MaxLength="5"></asp:TextBox>
&nbsp;<asp:Label ID="priceRangeLbl2" runat="server" Text="and" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="highPriceRange" runat="server" Visible="False" Width="47px" MaxLength="6"></asp:TextBox>
        <asp:Label ID="priceRangeLbl3" runat="server" Text="euros" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="locationLabel" runat="server" Text="Location:" Visible="False"></asp:Label>
&nbsp;<asp:DropDownList ID="locationDrop" runat="server" Height="20px" Visible="False" Width="152px" ViewStateMode="Enabled">
        </asp:DropDownList>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID ="guildDrop" EventName ="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID ="advancedSearch" EventName="CheckedChanged" />
                </Triggers>
        </asp:UpdatePanel>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <div class="grid" style="margin-top: 0px">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
        <asp:GridView ID="searchGrid" runat="server" Width="100%" style="margin-top: 0px" AutoGenerateColumns="False" OnPageIndexChanging="searchGrid_PageIndexChanging" AllowPaging="True" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
            </Columns>
        </asp:GridView>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID ="searchBtn" EventName ="Click" />
            </Triggers>
            </asp:UpdatePanel>

    </div>
</asp:Content>
