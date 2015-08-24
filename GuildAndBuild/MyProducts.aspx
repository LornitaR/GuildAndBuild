<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MyProducts.aspx.cs" Inherits="GuildAndBuild.MyProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="My Products"></asp:Label></p>
   <div id="Main" style="height: 1336px; width: 100%">
        <div id="Body" style="height: 100%; width:80%;">
            <p style="float:right; margin-right:2%; width:20%"><asp:Button ID="btnAddProduct" runat="server" Text="Add a new Product" OnClick="btnAddProduct_Click"/></p>
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
            <asp:GridView Width="90%"  ID="ProductList" runat="server" 
                          AutoGenerateColumns="False" 
                          style="margin-right: 1px" 
                          OnPageIndexChanging="ProductList_PageIndexChanging"
                          OnRowCommand="ProductList_RowCommand">
                <rowstyle Height="200px" Width="200px" />
                <Columns>
                    <asp:TemplateField HeaderText="ProductID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProdID" runat="server" Text='<%# Bind("ProductID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Photo">
                        <ItemTemplate>                            
                            <asp:Image ID="Image1" Height="120px" Width="120px" runat="server" ImageUrl='<%# Bind("ProductPhoto") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="Name" DataNavigateUrlFields="ProductID" DataNavigateUrlFormatString="ProductProfile.aspx?id={0}" DataTextField="ProductName" />
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxName" Width="100px" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditName" runat="server" ErrorMessage="You must enter a Name"
                                ControlToValidate="TextBoxName" Text="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelName" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxCost" Width="60px" runat="server" Text='<%# Bind("ProductCost") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditCost" runat="server" ErrorMessage="You must enter a price"
                                ControlToValidate="TextBoxCost" Text="*" ForeColor="Red">
                             </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmprValidatorDoubleType" ControlToValidate="TextBoxCost" Type="Double" 
                                 Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="*Not a valid number." runat="server">
                            </asp:CompareValidator>                          
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelCost" runat="server" Text='<%# Bind("ProductCost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxQuan" Width="60px" runat="server" Text='<%# Bind("ProductQuantity") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditQuan" runat="server" ErrorMessage="You must enter a Quantity"
                                ControlToValidate="TextBoxQuan" Text="*" ForeColor="Red">
                            </asp:RequiredFieldValidator> 
                            <asp:CompareValidator ID="cmprValidatorIntType" ControlToValidate="TextBoxQuan" Type="Integer" 
                                 Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="*Not a valid number." runat="server">
                            </asp:CompareValidator>                                                       
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelQuan" runat="server" Text='<%# Bind("ProductQuantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guild">
                        <ItemTemplate>
                            <asp:Label ID="LabelGuild" runat="server" Text='<%# Bind("Guild") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="Labeltype"  runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material">
                        <ItemTemplate>
                            <asp:Label ID="LabelMat" runat="server" Text='<%# Bind("Materials") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxDesc" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditDesc" runat="server" ErrorMessage="You must enter a description"
                                ControlToValidate="TextBoxDesc" Text="*" ForeColor="Red">                   
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelDesc" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblEdit" CommandArgument='<%# Eval("ProductID") %>' CommandName="EditRow" runat="server">Edit</asp:LinkButton>
                            <asp:LinkButton ID="lblDelete" CommandArgument='<%# Eval("ProductID") %>' CommandName="DeleteRow" runat="server" CausesValidation="False">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lblUpdate" CommandArgument='<%# Eval("ProductID") %>' CommandName="UpdateRow" runat="server">Update</asp:LinkButton>
                            <asp:LinkButton ID="lblCancel" CommandArgument='<%# Eval("ProductID") %>' CommandName="CancelUpdate" runat="server" CausesValidation="False">Cancel</asp:LinkButton>                            
                        </EditItemTemplate>
                   </asp:TemplateField>
                </Columns>
            </asp:GridView>
                    </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ProductList"/>
            </Triggers>
        </asp:UpdatePanel>
            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" runat="server" />
        </div>
    </div>
</asp:Content>
