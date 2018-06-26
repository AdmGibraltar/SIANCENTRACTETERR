﻿<%@ Page Title="Antigüedad de saldos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_CobAntiguedadSaldos.aspx.cs" Inherits="SIANWEB.Rep_CobSaldos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                return true;
            }
            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />                   
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <%-- <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" ValidationGroup="print"
                    CssClass="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel" ValidationGroup="print"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="filtros" runat="server">
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
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtCliente" onpaste="return false" runat="server" Width="200px"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Territorio"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtTerritorio" onpaste="return false" runat="server" Width="200px"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Fecha de corte"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFechaCorte" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFechaCorte"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkDetalle" runat="server" Text="Detalle" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkTerritorio" runat="server" Text="Agrupar por territorio" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HF_ClvPag" runat="server" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
