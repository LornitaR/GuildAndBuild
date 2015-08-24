<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MyCart.aspx.cs" Inherits="GuildAndBuild.MyCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div>
       <div>
           <h1>Your Shopping Items</h1>
           <p>The list below contains your shopping items:</p>
       </div>
     <div>
    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemID" OnRowCommand="gvCart_RowCommand" OnRowDataBound="gvCart_RowDataBound" Width="680px" ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:ImageField DataImageUrlField="Photo" HeaderText="Photo">
                <ControlStyle Height="100px" />
            </asp:ImageField>
            <asp:HyperLinkField DataNavigateUrlFields="itemID" DataNavigateUrlFormatString="ProductProfile.aspx?id={0}" DataTextField="itemName" HeaderText="Item Name" />
            <asp:BoundField DataField="item.itemPrice" DataFormatString="{0:c}" HeaderText="Price" SortExpression="itemPrice" />
            <asp:BoundField DataField="quantity" HeaderText="Current Quantity" />
            <asp:TemplateField HeaderText="Update/Remove">
                <ItemTemplate>
                    <asp:TextBox ID="tbQuantity" runat="server" MaxLength="2" text='<%# Bind("quantity") %>' Width="26px"></asp:TextBox>
                    &nbsp;<asp:LinkButton ID="linkRemove" autopostback="true" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ItemID") %>'>Remove</asp:LinkButton>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbQuantity" ErrorMessage="Must be a valid integer." ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalPrice" DataFormatString="{0:c}" HeaderText="Total Price" />
        </Columns>
    </asp:GridView>
         <p>
             <asp:Label ID="projListLbl" runat="server" Text="The list below contains your project items:"></asp:Label>
         </p>
         <br />
         <asp:GridView ID="projectsGV" runat="server" AutoGenerateColumns="False" Width="680px" OnRowCommand="projectsGV_RowCommand" ShowHeaderWhenEmpty="True" DataKeyNames="ProjectID" style="margin-top: 0px">
             <Columns>
                 <asp:BoundField DataField="project.itemName" HeaderText="Project Name" />
                 <asp:BoundField DataField="project.itemPrice" HeaderText="Price" DataFormatString="{0:c}" />
                 <asp:TemplateField HeaderText="Remove">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ProjectID") %>'>Remove</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
         <br />
         <asp:Label ID="quantityLabel" runat="server"></asp:Label>
         <br />
         <br />
         <br />
         <br />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="buyBtn" runat="server" OnClick="buyBtn_Click" Text="Buy" Width="63px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="returnBtn" align="center" runat="server" Text="Return" OnClick="returnBtn_Click" />
    </div>
    </div>
</asp:Content>
