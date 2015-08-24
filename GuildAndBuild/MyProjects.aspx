<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MyProjects.aspx.cs" Inherits="GuildAndBuild.MyProjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>     
    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="My Projects"></asp:Label></p>
    <div id="Main" style="height: 1336px; width: 100%">
        <div id="Body" style="height: 100%; width:80%;">
            <p style="float:right; margin-right:2%; width:20%"><asp:Button ID="btnAddProject" runat="server" Text="Add a new Project" OnClick="btnAddProject_Click"/></p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
            <asp:GridView ID="ProjectList" runat="server"
                AutoGenerateColumns="False" 
                style="margin-right: 1px"
                OnRowCommand="ProjectList_RowCommand"
                OnPageIndexChanging="ProjectList_PageIndexChanging" 
                OnRowDeleting="ProjectList_RowDeleting">
                 <rowstyle Height="200px" Width="200px" />
                <Columns>
                    <asp:BoundField DataField="ProjectID" HeaderText="Project ID" Visible="False" />
                    <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" Visible="False" />
                    <asp:TemplateField HeaderText="Photo">
                        <ItemTemplate>                            
                            <asp:Image ID="Image1" Height="200px" Width="200px" runat="server" ImageUrl='<%# Bind("ProjectPhoto") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                    <asp:BoundField DataField="Guild" HeaderText="Guild" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Materials" HeaderText="Materials" />
                    <asp:BoundField DataField="ProjectDescription" HeaderText="Description" />
                    <asp:BoundField DataField="CompleteStatus" HeaderText="Status" />
                    <asp:BoundField DataField="MinPrice" HeaderText="Min Price" />
                    <asp:BoundField DataField="MaxPrice" HeaderText="Max Price" />
                    <asp:BoundField DataField="Deadline" HeaderText="Deadline" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDelete" CommandArgument='<%# Eval("ProjectID") %>' CommandName="Delete" runat="server" CausesValidation="False">Delete</asp:LinkButton>
                        </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
            </asp:GridView>
                    </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ProjectList"/>
            </Triggers>
        </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
