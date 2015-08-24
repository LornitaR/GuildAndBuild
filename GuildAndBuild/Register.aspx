<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GuildAndBuild.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    
                    <table  style="position: relative; top: -79px; left: 308px; height: 396px; width: 566px;">
                        <tr>
                            <td align="center" colspan="2">Sign Up for Your New Account</td>
                        </tr>

                        <tr>
                            <td align="right">
                                <asp:Label ID="lblFirstname" runat="server" Text="First name:"></asp:Label>
                                <br />
                                </td>
                            <td>
                                <asp:TextBox ID="txtFirstName" runat="server" ></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid characters" ControlToValidate="txtFirstName" ValidationExpression="[A-Z}{1}[a-z]{2,15}"></asp:RegularExpressionValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ErrorMessage="You must enter your First name" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                 </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblSurname" runat="server" Text="Surname:"></asp:Label>
                            <br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtSurname" runat="server" ></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid characters" ControlToValidate="txtSurname" ValidationExpression="[A-Z}{1}[a-z]{2,15}"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSurname" ErrorMessage="You must enter your Surname" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                </td>
                        </tr>


                        <tr>
                            <td align="right">
                                <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                                <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsername" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUsername" ErrorMessage="You must enter a username" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                    <tr>
                                <td align="right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email address:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Must be valid email address" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="You must enter your email address" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                           
                                <td align="right">
                                    <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                                    <br />
                                </td>
                                <td style="margin-left: 80px">
                                    <asp:DropDownList ID="ddlLocation" runat="server" style="position: relative" AutoPostBack="true">
                                        <asp:ListItem>Select province</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlLocation" ErrorMessage="You must select a province" InitialValue="0" style="position: right" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUserType" runat="server" Text="User type:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlUserType" ErrorMessage="You must select an account type" InitialValue="0" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDOB" runat="server" Text="Date of birth"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Must be in format DD/MM/YYYY" ControlToValidate="txtDOB" ValidationExpression="[0-9]{2}/[0-9]{2}/[0-9]{4}"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDOB" ErrorMessage="You must enter your date of birth" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblDOBFail" runat="server" ForeColor="#CC0000"></asp:Label>
                                    <br /> 
                                     </td>
                            </tr>
                                    <tr>
                                <td align="right">
                                    <asp:Label ID="lblInterests" runat="server" Text="Interest:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlInterests" runat="server" ValidationGroup="btnCreateUser" AutoPostBack="true" >
                                    </asp:DropDownList>
                                     
                                     </td>
                            </tr>
                         <tr>
                                <td align="right">
                                    <asp:Label ID="lblPassword" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" ></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Must be between 8 and 32 alphanumeric characters in length" ControlToValidate="Password" ValidationExpression="^[a-zA-Z0-9\s]{7,10}$"></asp:RegularExpressionValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Password" ErrorMessage="You must enter your password" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" ></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="ConfirmPassword" ControlToValidate="Password" ErrorMessage="Passwords don't match"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="You must re-enter your password" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGender" runat="server">
                                        <asp:ListItem>Prefer not to say</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                            </tr>
    </tr>
    
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="Question" runat="server">
                                    <asp:ListItem>In which city were you born?</asp:ListItem>
                                    <asp:ListItem>What was the name of your first pet?</asp:ListItem>
                                    <asp:ListItem>What is the name of your primary school?</asp:ListItem>
                                    <asp:ListItem>What&#39;s the first thing you learned to cook?</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="Group1">Security question is required.</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server" ></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Security answer is required." ToolTip="Security answer is required." ValidationGroup="Group1">Security answer is required.</asp:RequiredFieldValidator>
                            <br />
                            </td>
                            
                        </tr>
                        <tr>
                            <td> </td>
                            <td>
                                <asp:Button ID="btnCreateUser" runat="server" Text="Create User" Height="27px" OnClick="btnCreateUser_Click" ValidationGroup="Group1" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><asp:Label ID="lblFailure" runat="server" ForeColor="#990000"></asp:Label></td>
                        </tr>

                    </table>
                
    </asp:Content>


                  