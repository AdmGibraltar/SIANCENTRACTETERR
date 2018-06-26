<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatMovimientosCentral.aspx.cs" Inherits="SIANWEB.CatMovimientosCentral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
   <telerik:RadAjaxManager ID="RAM1" runat="server"  EnablePageHeadUpdate="False">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick2">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
          
        </Items>
    </telerik:RadToolBar>
<table width="100%" >
<tr>
<td>
  
</td>
<td>
    
Almacén
</td>
<td>
   <telerik:RadTextBox ID="TxtId_Alm" runat="server">
    </telerik:RadTextBox>
</td>
<td>
 
</td>
</tr>
</table>
</asp:Content>
