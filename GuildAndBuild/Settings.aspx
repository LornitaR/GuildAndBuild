<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="GuildAndBuild.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <p style="text-align:center; height: 25px;"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Settings"></asp:Label></p>
    <div style="float: left; margin-left: 350px; margin-right: 0px;" class="workArea">
        
            <asp:Label ID="lblFailure" runat="server"></asp:Label><br />
        
            <asp:Label ID="lblShopName" runat="server" Text="Shop Name" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtShopName" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" ViewStateMode="Enabled" Height="22px" style="margin-bottom: 1px"></asp:TextBox>
            <br />
            
            <asp:Label ID="lblArtisanDescription" runat="server" Text="Biography" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtDescription" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" ViewStateMode="Enabled" Rows ="3" TextMode="MultiLine" style="margin-bottom: 1px" Width="525px"></asp:TextBox><br />
            
            <asp:Label ID="lblFirstName" runat="server" Text="First Name" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px" style="margin-bottom: 1px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Invalid characters" ValidationExpression="[A-Z}{1}[a-z]{2,15}"></asp:RegularExpressionValidator>
            <br />
            
            <asp:Label ID="lblSurname" runat="server" Text="Surname" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtSurname" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px" style="margin-bottom: 1px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtSurname" ErrorMessage="Invalid characters" ValidationExpression="[A-Z}{1}[a-z]{2,15}"></asp:RegularExpressionValidator>
            <br />
            
            <asp:Label ID="lblUsername" runat="server" Text="Username" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtUsername" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px" style="margin-bottom: 1px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtUsername" ErrorMessage="RegularExpressionValidator" ValidationExpression="[A-Z}{1}[a-z]{2,15}">Invalid characters</asp:RegularExpressionValidator>
            <br />
           
            <asp:Label ID="lblNewPassword" runat="server" Text="New Password" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" ViewStateMode="Enabled" style="margin-bottom: 1px" Height="21px" TextMode="Password" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Must be between 8 and 32 alphanumeric characters in length" ValidationExpression="^[a-zA-Z0-9\s]{7,10}$" ControlToValidate="txtNewPassword"></asp:RegularExpressionValidator>
            <br />
            
            <asp:Label ID="lblReEnterNewPassword" runat="server" Text="Re-Enter Password" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtReEnterPassword" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" style="margin-bottom: 1px" Height="22px" TextMode="Password" ></asp:TextBox><br />
            <br />
            <asp:Label ID="lblLocation" runat="server" Text="Location" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:DropDownList ID="ddlLocation" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" ></asp:DropDownList><br />
            
            <asp:Label ID="lblDOB" runat="server" Text="Date of Birth" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtDOB" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" style="margin-bottom: 1px" Height="22px" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDOB" ErrorMessage="Must be in format DD/MM/YYYY" ValidationExpression="[0-9]{2}/[0-9]{2}/[0-9]{4}"></asp:RegularExpressionValidator>
            <br />
            
            <asp:Label ID="lblInterests" runat="server" Text="Interests" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:DropDownList ID="ddlInterests" runat="server"></asp:DropDownList> <br />

            <asp:Label ID="lblGender" runat="server" Text="Gender" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
             <asp:DropDownList ID="ddlGender" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS">
                <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                <asp:ListItem Text="Prefer not to say" Value="4"></asp:ListItem>
            </asp:DropDownList><br />

            <asp:Label ID="lblAddress" runat="server" Text="Address" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:TextBox ID="txtAddress" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" style="margin-bottom: 1px" Height="22px" ></asp:TextBox><br />
            
            <asp:Label ID="lblProfilePicture" runat="server" Text="Profile Picture" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />

            <asp:FileUpload ID="fuProfilePhoto" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px"/>
            <asp:Label ID="lblUploadStatus" runat="server"></asp:Label>
        <br />
        <p style="width:60%">
            <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" Font-Names="Bodoni MT" Font-Size="X-Large" Width="269px" OnClick="btnSaveChanges_Click"/>&nbsp&nbsp&nbsp&nbsp
            <asp:Button ID="btnDeleteProfile" runat="server" Text="Delete Profile" Font-Names="Bodoni MT" Font-Size="X-Large" Width="269px" OnClick="btnDeleteProfile_Click"/>
        </p><br />
    </div><br />
</asp:Content>
