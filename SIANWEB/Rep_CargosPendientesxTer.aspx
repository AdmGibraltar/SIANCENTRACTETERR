<%@ Page Title="Cargos pendientes de pago" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_CargosPendientesxTer.aspx.cs" Inherits="SIANWEB.Rep_CargosPendientesxTer" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':
                        var txtCliente = $find("<%= txtCte.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtTerr = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        break;
                    case 'excel':
                        var txtCliente = $find("<%= txtCte.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtTerr = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
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

            function refreshGrid() {
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RBAnalisis">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbNivel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>    
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            TabIndex="10">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
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
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
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
                    <table style="font-family: verdana; font-size: 8pt" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="LblCte" runat="server" Text="Cliente" />
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtCte" runat="server" Width="125px" MaxLength="19" onpaste="return false"
                                    TabIndex="1">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblTerritorio" runat="server" Text="Territorio" />
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtTerritorio" runat="server" Width="125px" MaxLength="19"
                                    onpaste="return false" TabIndex="6">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="LblFecha" runat="server" Text="Dias vencidos inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="diasIni" runat="server" MaxLength="9" MinValue="0"
                                    Width="70px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LblFecha0" runat="server" Text="Dias vencidos final" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="DiasFin" runat="server" MaxLength="9" MinValue="0"
                                    Width="70px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="RBCliente" runat="server" Text="Agrupar por cliente" GroupName="venta"
                                    Checked="true" />
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="RBTerritorio" runat="server" Text="Agrupar por territorio" GroupName="venta" />
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="HF_ClvPag" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
