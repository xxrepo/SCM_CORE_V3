<%@ Page Title="No-Show Report" Language="C#" MasterPageFile="~/SmartCargoMaster.Master" AutoEventWireup="true" CodeBehind="rptAWBNoShow.aspx.cs" Inherits="ProjectSmartCargoManager.rptAWBNoShow" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="DataDynamics.Reports.Web, Version=1.6.2084.14, Culture=neutral, PublicKeyToken=d557f2f30a260da2"
    Namespace="DataDynamics.Reports.Web" TagPrefix="dd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="TSM0" runat="server">
 
 </asp:ToolkitScriptManager>
     
 <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(callShow);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(callclose);

        function callShow() {
            document.getElementById('msglight').style.display = 'block';
            document.getElementById('msgfade').style.display = 'block';
            //document.getElementById("<%= msgshow.ClientID %>").innerHTML = "Loading Data .......";

        }
        function callclose() {
            document.getElementById('msglight').style.display = 'none';
            document.getElementById('msgfade').style.display = 'none';
        }
     </script>
    
 <style type="text/css">
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 1000px;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: 0.8;
            filter: alpha(opacity=80);
        }
        .white_content
        {
            margin: 0 auto;
            display: none;
            position: absolute;
            top: 30%;
            left: 35%;
            width: 30%;
            height: 45%;
            padding: 16px;
            border: 16px solid #ccdce3;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        .black_overlaymsg
        {
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: White;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }
        .white_contentmsg
        {
            display: none;
            position: fixed;
            top: 45%;
            left: 45%;
            width: 5%;
            height: 5%;
            padding: 16px;
            background-color: Transparent;
            z-index: 1002;
        }
     .style1
     {
         height: 30px;
     }
    </style>
    
 <%--<asp:UpdatePanel runat="server" ID="updtPnl">
 <ContentTemplate>--%>
     
 <div id="contentarea">
 <div class="msg">
<asp:Label ID="lblStatus" runat="server" BackColor="White" Font-Bold="True" Font-Size="Large"></asp:Label>
</div>
 <h1>No-Show Report</h1>

<div class="botline">
<table width="100%">
<tr>
<td>Origin</td>
<td>
    <asp:DropDownList ID="ddlStn" runat="server">
    </asp:DropDownList>
</td>
<td>Destination</td>
<td>
    <asp:DropDownList ID="ddldest" runat="server">
    </asp:DropDownList>
</td>

<td>CommodityCode</td>
<td>
    <asp:DropDownList ID="ddlCC" runat="server">
    </asp:DropDownList>
    </td>
<td>
    
    
</td>
    <td>
    </td>
</tr>
<tr>
<td class="style1">From Date</td>
<td class="style1">
    <asp:TextBox ID="txtfrmdate" runat="server" Width="100px"></asp:TextBox>
    <asp:CalendarExtender ID="C1" runat="server" TargetControlID="txtfrmdate" Format="dd/MM/yyyy" Enabled="true" PopupButtonID="imgFromDate"></asp:CalendarExtender>
                    <asp:ImageButton ID="imgFromDate" runat="server" ImageAlign="AbsMiddle" 
                     ImageUrl="~/Images/calendar_2.png" />
</td>
<td class="style1">ToDate</td>
<td class="style1">
    <asp:TextBox ID="txttodate" runat="server" Width="100px"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttodate" Format="dd/MM/yyyy" Enabled="true" PopupButtonID="imgToDate"></asp:CalendarExtender>
                    <asp:ImageButton ID="imgToDate" runat="server" ImageAlign="AbsMiddle" 
                     ImageUrl="~/Images/calendar_2.png" />
</td>

<td class="style1"></td>
<td class="style1">
    </td>

    <td class="style1">
        </td>
    <td class="style1">
        </td>
</tr>
<tr>
<td colspan="8">
        <asp:Button ID="btnList" runat="server" Text="List" CssClass="button" 
        onclick="btnList_Click" />
        &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="button"  
                        Text="Export"  onclick="btnExport_Click" />
    &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" onclick="btnClear_Click" 
        /></td>
</tr>
<tr>
<td colspan="3">
    <%--<asp:Label ID="lblStatus" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>--%>
    </td>
    </tr></table></div><br />  

<div style="border: thin solid #000000; float:left">
<%--<dd:WebReportViewer ID="rptNNoSHow" runat="server" Height="500px" Width="1000px" Visible="false"/>
--%>  

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1022px">
    </rsweb:ReportViewer>
</div>  

</div>
      
<div id="msglight" class="white_contentmsg">
<table>
<tr>
<td width="5%" align="center">
<br />
<img src="Images/loading.gif" />
<br />
<asp:Label ID="msgshow" runat="server"></asp:Label></td></tr></table></div><div id="msgfade" class="black_overlaymsg">
 </div>
    
 <%--</ContentTemplate>
 </asp:UpdatePanel>--%>
</asp:Content>
