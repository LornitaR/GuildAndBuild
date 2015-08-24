<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GuildAndBuild.Login" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="mainContent" >
    
   <div style="float: left; margin-left: 350px; margin-right: 0px; height: 932px;" > 
    <table style="width: 50%" >
        Log-in here!
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email Address:"></asp:Label>  
                
            </td>
            <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email address required" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPassword" runat="server">Password:</asp:Label>
                </td>
            <td>
                 <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
         <tr>
             <td>               
            <asp:Label ID="lblFailure" runat="server" ForeColor="Red"></asp:Label>
            </td>
             <td>                           
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="LoginGroup" />
                 </td> 
        </tr>
        <tr><td>
               <asp:Button ID="btnForgotPassword" runat="server"  Text="Forgotten password?" OnClick="btnForgotPassword_Click" />
             </td></tr>     
             
        <tr>
            <td>
               <asp:Label ID="lblEmailAddress2" runat="server" Visible="False" >Email Address:</asp:Label>
            </td>
            <td>
                 <asp:TextBox ID="txtEmailAddress2" runat="server" Visible="False"></asp:TextBox>
                 <asp:Button ID="btnEmail" runat="server" OnClick="btnEmail_Click" Text="Submit Email" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuestion" runat="server" Visible="False" >Security Question:</asp:Label>
             </td>
            <td>
                 <asp:Label ID="lblQuestion2" runat="server" Visible="False" ></asp:Label>
            
             </td>
            <tr>

            <td>
                 <asp:Label ID="lblAnswer" Text="Answer:" runat="server" Visible="False" ></asp:Label>
            
             </td>
                <td>
                    <asp:TextBox ID="txtAnswer" runat="server" Visible="False"></asp:TextBox>
                </td>
                </tr>
              </tr> 
        <tr>
            <td>
                 <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click1" Text="Submit" Visible="False" />
                 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNewPassword" runat="server" Text="New password:" Visible="False"></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="txtNewPassword" runat="server" Visible="False" TextMode="Password"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Label ID="lblConfirmNewPassword" runat="server" Text="Confirm Password" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" Visible="False" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNewPassword" runat="server" Text="Submit new password" Visible="False" OnClick="btnNewPassword_Click" />
            </td>
        </tr>
        
    </table>
   </div>
<div style="height: 150px">
    <br />
</div>
    
</asp:Content>

