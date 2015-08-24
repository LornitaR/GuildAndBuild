<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="NewProduct.aspx.cs" Inherits="GuildAndBuild.NewProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server" ViewStateMode="Enabled">
    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Add New Product"></asp:Label></p>
    <div class="workArea">
        <p id="projectLabels">
            <asp:Label ID="lblProductName" runat="server" Text="Product Name" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblGuild" runat="server" Text="Guild" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblMaterial" runat="server" Text="Material" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblType" runat="server" Text="Type" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblCost" runat="server" Text="Price" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label>&nbsp;<br />
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblDescription" runat="server" Text="Product Description" Font-Size="X-Large" Font-Names="Bodoni MT"></asp:Label><br /><br /><br /><br />
            <asp:Label ID="lblImages" runat="server" Text="Upload Images" Font-Size="X-Large" Font-Names="Bodoni MT"></asp:Label><br />
        </p>
        <p id="projectFields">
            <asp:TextBox ID="txtProductName" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" ViewStateMode="Enabled" Height="22px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="txtProductName" ForeColor="#CC0000" ErrorMessage="You must enter a Product Name." ValidationGroup="GroupNewProduct"></asp:RequiredFieldValidator><br />
            <asp:DropDownList ID="ddlGuild" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" OnSelectedIndexChanged="ddlGuild_SelectedIndexChanged" ViewStateMode="Enabled" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvGuild" runat="server" ControlToValidate="ddlGuild" ForeColor="#CC0000" ErrorMessage="You must choose a Guild." ValidationGroup="GroupNewProduct" InitialValue="0"></asp:RequiredFieldValidator><br />
            <asp:DropDownList ID="ddlMaterial" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvMaterial" runat="server" ControlToValidate="ddlMaterial" ForeColor="#CC0000" ErrorMessage="You must choose a Material." ValidationGroup="GroupNewProduct" InitialValue="0"></asp:RequiredFieldValidator><br />
            <asp:DropDownList ID="ddlType" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" ForeColor="#CC0000" ErrorMessage="You must choose a Type." ValidationGroup="GroupNewProduct" InitialValue="0"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtCost" runat="server" MaxLength="18" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCost" runat="server" ControlToValidate="txtCost" ForeColor="#CC0000" ErrorMessage="Enter the price of your product." ValidationGroup="GroupNewProduct" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revCost" runat="server" ForeColor="#CC0000" ErrorMessage="Enter numeric with no more than 2 decimal places." ControlToValidate="txtCost" ValidationExpression="^(\d{0,9}\.\d{0,2}|\d{0,9})$" Display="Dynamic" ValidationGroup="GroupNewProduct"></asp:RegularExpressionValidator><br />
            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="4" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ForeColor="#CC0000" ErrorMessage="Enter the product quantity in stock." ValidationGroup="GroupNewProduct" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="revQuantity" runat="server" ForeColor="#CC0000" ErrorMessage="Value must be a whole number." Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ControlToValidate="txtQuantity" ValidationGroup="GroupNewProduct"></asp:CompareValidator><br />
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="90%" Font-Size="Medium" Font-Names="Trebuchet MS" PlaceHolder="Describe your product (colour, dimensions, etc.) ..." Rows="4"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ForeColor="#CC0000" ErrorMessage="You must enter a detailed description of your product." ValidationGroup="GroupNewProduct" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:FileUpload ID="fuImageUpload" runat="server" Font-Size="Medium" Font-Names="Bodoni MT" Height="29px" AutoPostback="true" /><br />
            <asp:Label ID="lblUploadStatus" runat="server" Text="Upload status: " Font-Size="Medium" Font-Names="Bodoni MT" Height="29px" />
        </p>
        <p style="text-align:center"><asp:Label ID="lblStatus" runat="server" ForeColor="#009933" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label></p>
    </div><br /><br />
    <p style="float: right; width:40%">
        <asp:Button ID="btnSubmit" runat="server" CssClass="commandButton" Font-Names="Bodoni MT" Font-Size="X-Large" Text="Submit" OnClick="btnSubmit_Click" Width="269px" ValidationGroup="GroupNewProduct"/>
    </p>
</asp:Content>