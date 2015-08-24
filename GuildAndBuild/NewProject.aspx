<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="NewProject.aspx.cs" Inherits="GuildAndBuild.NewProject" MaintainScrollPositionOnPostBack="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server" ViewStateMode="Enabled">
    <p style="text-align:center"><asp:Label ID="lblTitle" runat="server" Font-Size="XX-Large" Font-Names="Bodoni MT" Text="Add New Project"></asp:Label></p>
    <div class="workArea">
        <div id="projectLabels">
            <asp:Label ID="lblProjectName" runat="server" Text="Project Name" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblGuild" runat="server" Text="Guild" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblMaterial" runat="server" Text="Material" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblType" runat="server" Text="Type" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br />
            <asp:Label ID="lblPriceRange" runat="server" Text="Price Range €" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br /><br />
            <asp:Label ID="lblDeadline" runat="server" Text="Deadline" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            <asp:Label ID="lblPrivacy" runat="server" Text="Project Privacy" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label><br /><br /><br />
            <asp:Label ID="lblDescription" runat="server" Text="Project Description" Font-Size="X-Large" Font-Names="Bodoni MT"></asp:Label><br /><br /><br /><br /><br />
            <asp:Label ID="lblImages" runat="server" Text="Upload Images" Font-Size="X-Large" Font-Names="Bodoni MT"></asp:Label><br /><br /><br />
        </div>
        <div id="projectFields" style="font-family: 'Trebuchet MS'">
            <div style="height: 40%";>
                <asp:TextBox ID="txtProjectName" runat="server" MaxLength="50" Font-Size="Medium" Font-Names="Trebuchet MS" ViewStateMode="Enabled" Height="22px" style="margin-bottom: 0px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="You must enter a Project Name." ForeColor="#CC0000" ValidationGroup="GroupNewProject"></asp:RequiredFieldValidator><br />
                <asp:DropDownList ID="ddlGuild" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" OnSelectedIndexChanged="ddlGuild_SelectedIndexChanged" ViewStateMode="Enabled" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvGuild" runat="server" ControlToValidate="ddlGuild" ErrorMessage="You must choose a Guild." ValidationGroup="GroupNewProject" InitialValue="0" ForeColor="#CC0000"></asp:RequiredFieldValidator><br />
                <asp:DropDownList ID="ddlMaterial" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvMaterial" runat="server" ControlToValidate="ddlMaterial" ErrorMessage="You must choose a Material." ForeColor="#CC0000" ValidationGroup="GroupNewProject" InitialValue="0"></asp:RequiredFieldValidator><br />
                <asp:DropDownList ID="ddlType" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="29px" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" ErrorMessage="You must choose a Type." ForeColor="#CC0000" ValidationGroup="GroupNewProject" InitialValue="0"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtMin" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px" PlaceHolder="Minimum"></asp:TextBox>&nbsp;
                <asp:Label ID="lblTo" runat="server" Text="to" Font-Size="X-Large" Font-Names="Bodoni MT" Height="29px"></asp:Label>&nbsp;
                <asp:TextBox ID="txtMax" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" Height="22px" PlaceHolder="Maximum"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMin" runat="server" ControlToValidate="txtMin" ErrorMessage="Enter Minimum." ForeColor="#CC0000" ValidationGroup="GroupNewProject" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvMax" runat="server" ControlToValidate="txtMax" ErrorMessage="Enter Maximum." ForeColor="#CC0000" ValidationGroup="GroupNewProject" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revMin" runat="server" ErrorMessage="Enter numeric with no more than 2 decimal places." ControlToValidate="txtMin" ValidationExpression="^(\d{0,9}\.\d{0,2}|\d{0,9})$" ForeColor="#CC0000" Display="Dynamic" ValidationGroup="GroupNewProject"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="revMax" runat="server" ErrorMessage="Enter numeric with no more than 2 decimal places." ControlToValidate="txtMax" ValidationExpression="^(\d{0,9}\.\d{0,2}|\d{0,9})$" ForeColor="#CC0000" Display="Dynamic" ValidationGroup="GroupNewProject"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="cvMinMax" runat="server" ErrorMessage="Maximum needs to be greater than Minimum." ControlToValidate="txtMax" ControlToCompare="txtMin" Operator="GreaterThan" Display="Dynamic" ValidationGroup="GroupNewProject" ForeColor="#CC0000" Type="Double"></asp:CompareValidator><br /><br />
            </div>
            <div>
                <asp:Calendar ID="CalendarDeadline" runat="server" FirstDayOfWeek="Monday" SelectionMode="DayWeek" OnSelectionChanged="CalendarDeadline_SelectionChanged" Height="190px" Width="330px"></asp:Calendar>
                <asp:TextBox ID="txtDate" runat="server" Font-Names="Trebuchet MS"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDeadline" runat="server" ControlToValidate="txtDate" ErrorMessage="You must enter a deadline." Display="Dynamic" ValidationGroup="GroupNewProject" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvDeadline" runat="server" ControlToValidate="txtDate" ErrorMessage="You must choose a date in the future." Display="Dynamic" ValidationGroup="GroupNewProject" ForeColor="#CC0000" Operator="GreaterThan" Type="Date"></asp:CompareValidator><br />

                <asp:RadioButtonList ID="rblPrivacy" runat="server" Font-Size="X-Large" Font-Names="Bodoni MT" Height="62px" RepeatLayout="Flow" OnSelectedIndexChanged="rblPrivacy_OnSelectedIndexChanged" Width="213px" AutoPostBack="True">
                    <asp:ListItem Text="Public" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Private" Value ="0"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="txtArtisan" runat="server" Font-Size="Medium" Font-Names="Trebuchet MS" PlaceHolder="Artisan for this project" Width ="40%" Visible="false" Display="Dynamic"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPrivacy" runat="server" ControlToValidate="rblPrivacy" ErrorMessage="You must choose a privacy setting for your project." ValidationGroup="GroupNewProject" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <br />
                
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="90%" Font-Size="Medium" Font-Names="Trebuchet MS" Height="93px" PlaceHolder="Describe what you want created in detail (colour, dimensions, etc.)..."></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Give a detailed description to your project." ValidationGroup="GroupNewProject" ForeColor="#CC0000"></asp:RequiredFieldValidator><br />
                <asp:FileUpload ID="fuImageUpload" runat="server" Font-Size="Medium" Font-Names="Bodoni MT" Height="29px" AutoPostback="true"/><br />
                <asp:Label runat="server" id="lblUploadStatus" text="Upload status: Choose an image to help illustrate your idea." Font-Size="Medium" Font-Names="Bodoni MT" Height="29px" />
            </div>
         </div>
        <div style="width:65%"; >
            <asp:Button ID="btnSubmit" runat="server" CssClass="commandButton" Font-Names="Bodoni MT" Font-Size="X-Large" Text="Submit" OnClick="btnSubmit_Click" Width="269px" ValidationGroup="GroupNewProject" />
        </div><br />
    </div><br />
</asp:Content>