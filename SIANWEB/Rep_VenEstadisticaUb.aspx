<%@ Page Title="Estadística de utilidad bruta" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_VenEstadisticaUb.aspx.cs" Inherits="SIANWEB.Rep_VenEstadisticaUb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                        //continuarAccion = ValidacionesEspeciales();
                        break;
                }
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

            function refreshGrid() {

            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td valign="middle" width="140">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td rowspan="1" valign="top">
                                Por
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td rowspan="5" valign="top">
                                &nbsp;<table style="width: 100%;">
                                    <tr runat="server" id="RowTerritorio">
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Territorio "></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtTerritorio" runat="server" GroupName="porrb"
                                                MaxLength="200">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr runat="server" id="RowCliente">
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Cliente "></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtCliente" runat="server" GroupName="porrb"
                                                MaxLength="200">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr runat="server" id="RowProducto">
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Producto "></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtProducto" runat="server" GroupName="porrb"
                                                MaxLength="200">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr runat="server" id="RowAño">
                                        <td width="60">
                                            <asp:Label ID="Label10" runat="server" Text="Año "></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbAño" runat="server" Width="130px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td rowspan="4">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbTerritorio" runat="server" Text="Territorio" AutoPostBack="true"
                                                GroupName="porrb" OnCheckedChanged="rbTerritorio_CheckedChanged" Checked="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbCliente" runat="server" Text="Cliente" AutoPostBack="true"
                                                GroupName="porrb" OnCheckedChanged="rbCliente_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbProducto" runat="server" Text="Producto" AutoPostBack="true"
                                                GroupName="porrb" OnCheckedChanged="rbProducto_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCliente" runat="server" Text="Cliente" />
                                <asp:CheckBox ID="chkProducto" runat="server" Text="Producto" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
    
</asp:Content>
