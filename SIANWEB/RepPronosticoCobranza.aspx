﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepPronosticoCobranza.aspx.cs" MasterPageFile="~/MasterPage/MasterPage01.master" Inherits="SIANWEB.RepPronosticoCobranza" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        break;                        
                }
                args.set_cancel(!continuarAccion);
            }


            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            function TabSelected(sender, args) {

            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function refreshGrid() { }

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                        args.set_enableAjax(false); 
                }
           
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        
        </AjaxSettings>
    </telerik:RadAjaxManager>
      
   <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl"  OnButtonClick = "rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"  ImageUrl="~/Imagenes/blank.png" />
               <%-- <telerik:RadToolBarButton CommandName="Descargar" Value="Descargar formato"  CssClass="facPedido" ToolTip="Descargar formato" ImageUrl="Imagenes/blank.png"/>--%>
            </Items>
        </telerik:RadToolBar>
        <br />
         <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
           <tr>
           <td> &nbsp;</td>
                <td >
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td class="style2" >
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server"
                        Width="248px" AutoPostBack="True" Height="19px" style="margin-left: 47px" 
                        onselectedindexchanged="CmbCentro_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
           <%-- <tr>
            <td> &nbsp;</td>
                <td>Venta promedio de los últimos</td>
                <td><telerik:RadTextBox ID="txtmeses" runat="server" Height="17px" Width="120px"></telerik:RadTextBox></td>
                <td>meses</td>
            </tr>--%>
            <tr>
            <td> &nbsp;</td>
                <td>Fecha corte</td>
                <td><telerik:RadDatePicker ID="dpFecha1" runat="server" Height="17px" Width="120px"></telerik:RadDatePicker></td>
                <td></td>
            </tr>
            <tr>
            <td> &nbsp;</td>
                <td><asp:Label ID="lbldias" runat="server" Text="Días de rotación deseada" 
                        Visible="False"></asp:Label></td>
                <td><telerik:RadTextBox ID="txtrotacion" runat="server" Height="17px" Width="120px" 
                        Visible="False"></telerik:RadTextBox></td>
                <td></td>
            </tr>
              <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                </td>
            </tr>
        </table>

    </div>


</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style2
        {
            height: 20px;
        }
    </style>
</asp:Content>