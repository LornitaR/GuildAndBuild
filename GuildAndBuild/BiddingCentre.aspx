<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="BiddingCentre.aspx.cs" Inherits="GuildAndBuild.BiddingCentre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Bidding Centre"></asp:Label></p>
    <div id="Main" style="height: 1439px; width: 100%">       
        <div id="Body" style="height: 100%; width:80%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
            <asp:GridView ID="BidList" runat="server"
                AutoGenerateColumns="False" 
                style="margin-right: 1px" 
                OnPageIndexChanging="BidList_PageIndexChanging" 
                OnRowCommand="BidList_RowCommand">
                 <rowstyle Height="200px" Width="200px" />
                <Columns>
                    <asp:BoundField DataField="ProjectID" HeaderText="Project ID" Visible="False" />
                    <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" Visible="False" />
                    <asp:TemplateField HeaderText="Photo">
                        <ItemTemplate>                            
                            <asp:Image ID="Image1" Height="120px" Width="120px" runat="server" ImageUrl='<%# Bind("ProjectPhoto") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" />
                    <asp:BoundField DataField="Guild" HeaderText="Guild" ReadOnly="True" />
                    <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True"/>
                    <asp:BoundField DataField="Materials" HeaderText="Materials" ReadOnly="True"/>
                    <asp:BoundField DataField="ProjectDescription" HeaderText="Description" ReadOnly="True"/>
                    <asp:BoundField DataField="CompleteStatus" HeaderText="Status" ReadOnly="True"/>
                    <asp:BoundField DataField="MinPrice" HeaderText="Min Price" ReadOnly="True"/>
                    <asp:BoundField DataField="MaxPrice" HeaderText="Max Price" ReadOnly="True"/>
                    <asp:BoundField DataField="Deadline" HeaderText="Deadline" ReadOnly="True"/>
                    <asp:TemplateField HeaderText="Price">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPrice" Width="60px" runat="server" Text=""></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditPrice" runat="server" ErrorMessage="You must enter a project proposal"
                                ControlToValidate="TextBoxPrice" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>    
                            <asp:CompareValidator ID="cmprValidatorDoubleType" ControlToValidate="TextBoxPrice" Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="*Not a valid number." runat="server">
                            </asp:CompareValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelQuan" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proposal">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxProposal" wrap="true" TextMode="MultiLine" runat="server" Text=""></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditProposal" runat="server" ErrorMessage="You must enter a project proposal"
                                ControlToValidate="TextBoxProposal" Text="*" ForeColor="Red">                   
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelDesc" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblBid" CommandArgument='<%# Eval("ProjectID") %>' CommandName="EditRow" runat="server" CausesValidation="False">MakeBid</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lblUpdate" CommandArgument='<%# Eval("ProjectID") %>' CommandName="UpdateRow" runat="server">Propose</asp:LinkButton>
                            <asp:LinkButton ID="lblCancel" CommandArgument='<%# Eval("ProjectID") %>' CommandName="CancelUpdate" runat="server" CausesValidation="False">Cancel</asp:LinkButton>                            
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
