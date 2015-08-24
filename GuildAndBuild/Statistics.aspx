<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="GuildAndBuild.WebForm2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div style="height: 607px">
        <div id="Main" style="height: 100%; width: 100%">
            <div id="Profile" style="height: 100%; width: 18%; float: left;">
                <div id="profilePicture" style="padding-bottom: 10%;">
                    &nbsp;</div>
            </div>
            <div id="Body" style="height: 100%; width:80%; float: right;">
                <div id="button">

                            <asp:Button ID="btnPie" OnClick ="btnPie_Click" runat="server" Text="Pie" />
                            <asp:Button ID="btnBar" OnClick ="btnBar_Click" runat="server" Text="Bar" />
                            <asp:Button ID="btnGraph" OnClick ="btnGraph_Click" runat="server" Text="Graph" />
                            <br />
                            <asp:DropDownList ID="ddlUserInfo1"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserInfo1_SelectedIndexChanged">
                                <asp:ListItem Text="Please Make a Selection" Value="0" />
                                <asp:ListItem Value="1">Gender</asp:ListItem>
                                <asp:ListItem Value="2">Location</asp:ListItem>
                                <asp:ListItem Value="3">Material</asp:ListItem>
                                <asp:ListItem Value="4">Type</asp:ListItem>
                            </asp:DropDownList>
                </div>
                <div id="chart">
                    <asp:Chart ID="Chart1" runat="server" Width="569px">
                        <Series>
                            <asp:Series Name="Series1">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
