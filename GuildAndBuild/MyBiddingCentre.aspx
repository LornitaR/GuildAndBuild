<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MyBiddingCentre.aspx.cs" Inherits="GuildAndBuild.MyBiddingCentre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="My Bids"></asp:Label></p>
    <div id="Main" style="height: 1590px; width: 100%">
        <div id="Body" style="height: 100%; width:80%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
            <asp:GridView ID="BidList" runat="server"
                AutoGenerateColumns="False" 
                AllowSorting="True"  
                style="margin-right: 1px" 
                OnRowCommand="BidList_RowCommand"
                OnPageIndexChanging="BidList_PageIndexChanging">
                <rowstyle Height="200px" Width="200px" />
                <Columns>
                    <asp:BoundField DataField="BidID" HeaderText="BidID" Visible="False" />
                    <asp:BoundField DataField="ArtisanID" HeaderText="ArtisanID" Visible="False" />
                    <asp:BoundField DataField="ProjectID" HeaderText="Project ID" Visible="False" />
                    <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" Visible="False" />
                    <asp:TemplateField HeaderText="Photo" Visible="False">
                        <ItemTemplate>                            
                            <asp:Image ID="Image1" Height="120px" Width="120px" runat="server" ImageUrl='<%# Bind("ProjectPhoto") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" />
                    <asp:BoundField DataField="ProjectDescription" HeaderText="Description" ReadOnly="True"/>
                    <asp:BoundField DataField="CompleteStatus" HeaderText="Status" ReadOnly="True"/>
                    <asp:BoundField DataField="MinPrice" HeaderText="Min Price" ReadOnly="True"/>
                    <asp:BoundField DataField="MaxPrice" HeaderText="Max Price" ReadOnly="True"/>
                    <asp:BoundField DataField="Deadline" HeaderText="Deadline" ReadOnly="True"/>
                    <asp:BoundField DataField ="ShopName" HeaderText="Shop Name" ReadOnly="true"/>
                    <asp:BoundField DataField="BidPrice" HeaderText="Proposed Price" />
                    <asp:BoundField DataField="BidText" HeaderText="Proposal" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblBid" CommandArgument='<%# Eval("ProjectID") + ";" +Eval("BidID") + ";" +Eval("ArtisanID") %>' CommandName="Accept" runat="server" CausesValidation="False">Finalise</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lblUpdate" CommandArgument='<%# Eval("ProjectID") %>' CommandName="Purchase" runat="server">Purchase</asp:LinkButton>                          
                        </EditItemTemplate>
                   </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BidList"/>
            </Triggers>
        </asp:UpdatePanel>
            <asp:ValidationSummary ID="ValidationSummary2" ForeColor="Red" runat="server" />
        </div>
    </div>
</asp:Content>
