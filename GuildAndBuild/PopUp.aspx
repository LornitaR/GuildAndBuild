<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="GuildAndBuild.PopUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <script type="text/javascript" src="js/jquery-1.11.0.min.js"></script>
<script type="text/javascript" src="js/jquery.leanModal.min.js"></script>
<link rel="StyleSheetPopUp" href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" />
<link type="text/css" rel="stylesheet" href="css/style.css" />

    <div id="modal" class="popupContainer" style="display:none;">
	<header class="popupHeader">
		<span class="header_title">Login</span>
		<span class="modal_close" ><i class="fa fa-times"></i></span>
	</header>

    <section class="popupBody">
     <asp:SqlDataSource ID="SqlDataSourceUserTypes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [UserTypes]"></asp:SqlDataSource>
    
                                     <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
    
                                     <asp:SqlDataSource ID="SqlDataSourceLocation" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Locations]"></asp:SqlDataSource>
    
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
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstname" ErrorMessage="You must enter your First name"></asp:RequiredFieldValidator>
                                 </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblSurname" runat="server" Text="Surname:"></asp:Label>
                            <br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtSurname" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSurname" ErrorMessage="You must enter your Surname"></asp:RequiredFieldValidator>
                                </td>
                        </tr>


                        <tr>
                            <td align="right">
                                <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                                <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsername" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUsername" ErrorMessage="You must enter a username"></asp:RequiredFieldValidator>
                                    <tr>
                                <td align="right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email address:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="You must enter your email address"></asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPassword" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" ></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Password" ErrorMessage="You must enter your password"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="You must re-enter your password"></asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                                    <br />
                                </td>
                                <td style="margin-left: 80px">
                                    <asp:DropDownList ID="ddlLocation" runat="server"  DataSourceID="SqlDataSourceLocation" DataTextField="LocationName" DataValueField="LocationID" style="position: relative">
                                        <asp:ListItem>Select province</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlLocation" ErrorMessage="You must select a province" InitialValue="0" style="position: right"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUserType" runat="server" Text="User type:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUserType" runat="server" DataSourceID="SqlDataSourceUserTypes" DataTextField="UserTypeName" DataValueField="UserTypeID">
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlUserType" ErrorMessage="You must select an account type" InitialValue="0"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDOB" runat="server" Text="Date of birth"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDOB" ErrorMessage="You must enter your date of birth"></asp:RequiredFieldValidator>
                                    <br /> 
                                     </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGender" runat="server" >
                                    </asp:TextBox>
                                    </td>
                            </tr>
    </tr>
    
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server" ></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Security answer is required." ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            <br />
                            </td>
                            
                        </tr>
                        <tr>
                            <td> </td>
                            <td>
                                <asp:Button ID="btnCreateUser" runat="server" Text="Create User" Height="27px" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><asp:Label ID="Label1" runat="server"></asp:Label></td>
                        </tr>

                    </table>
    </section>
</div>

    <div class="social_login">
    <div class="clearfix">
        <a class="social_box fb" href="#"><span class="icon_title">Connect with
        Facebook</span></a> <a class="social_box google" href="#"><span class=
        "icon_title">Connect with Google</span></a>
    </div>

    <div class="centeredText">
        <span>Or use your Email address</span>
    </div>

    <div class="action_btns">
        <div class="one_half">
            <a class="btn" href="#" id="login_form" name="login_form">Login</a>
        </div>

        <div class="one_half last">
            <a class="btn" href="#" id="register_form" name=
            "register_form">Sign up</a>
        </div>
    </div>
</div>

    <div class="user_login">
    <form>
        <label>Email / Username</label> <input type="text"><br>
        <label>Password</label> <input type="password"><br>

        <div class="checkbox">
            <input id="remember" type="checkbox"> <label for=
            "remember">Remember me on this computer</label>
        </div>

        <div class="action_btns">
            <div class="one_half">
                <a class="btn back_btn" href="#">Back</a>
            </div>

            <div class="one_half last">
                <a class="btn btn_red" href="#">Login</a>
            </div>
        </div>
    </form>
    
    <a class="forgot_password" href="#">Forgot password?</a>
</div>


    <div class="user_register">
    <form>
        <label>Full Name</label> <input type="text"><br>
        <label>Email Address</label> <input type="email"><br>
        <label>Password</label> <input type="password"><br>

        <div class="checkbox">
            <input id="send_updates" type="checkbox"> <label for=
            "send_updates">Send me occasional email updates</label>
        </div>

        <div class="action_btns">
            <div class="one_half">
                <a class="btn back_btn" href="#">Back</a>
            </div>

            <div class="one_half last">
                <a class="btn btn_red" href="#">Register</a>
            </div>
        </div>
    </form>
</div>


</asp:Content>
