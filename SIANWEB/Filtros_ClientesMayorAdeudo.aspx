<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Filtros_ClientesMayorAdeudo.aspx.cs" 
Inherits="SIANWEB.Filtros_ClientesMayorAdeudo" MasterPageFile="~/MasterPage/MasterPage01.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function ToolBar_ClientClick(sender, args) {
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

            function refreshGrid() {

            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" >
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
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl"   OnButtonClick = "rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" visible= "false"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                </td>
                <td width="150px" style="font-weight: bold">
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
                           
                            </td>
                            <td>
                       
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                               &nbsp;
                            </td>
                            <td>
                       
                            </td>
                        </tr>


                      <tr>
                            <td>
                                <asp:Label ID="Label1" Text="Días de plazo" runat="server">
                                </asp:Label></td>
                            <td>
                                   <telerik:RadNumericTextBox ID="TxtDias" runat="server" MaxLength="9" MinValue="0"
                                                       Width="100px" >
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                 </telerik:RadNumericTextBox>
                                </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                                <tr>
                            <td>
                                <asp:Label ID="Label2" Text="Días de revisión" runat="server">
                                </asp:Label></td>
                            <td>
                                   <telerik:RadNumericTextBox ID="TxtDiasRev" runat="server" MaxLength="9" MinValue="0"
                                                       Width="100px" >
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                 </telerik:RadNumericTextBox>
                                </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                          <tr>
                            <td>
                                <asp:Label ID="lblFIni" Text="Fecha Cierre de Mes" runat="server"></asp:Label>
                              </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="120px" DateInput-MaxLength="10"
                                    >
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                              </td>
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
                                <asp:Label ID="Label4" Text="Fecha Corte del reporte" runat="server"></asp:Label>
                              </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="120px" DateInput-MaxLength="10"
                                    >
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar" TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                              </td>
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
                            <td class="style1">
                                
                                <asp:Label ID="lblFIni0" Text="Clientes con saldo mayor a" runat="server">
                                </asp:Label>
                            </td>
                            <td class="style1">
                                   <telerik:RadNumericTextBox ID="TxtSaldoMinimo" runat="server" MaxLength="20" MinValue="0"
                                                       Width="100px" >
                                <NumberFormat DecimalDigits="2" GroupSeparator="." />
                                 </telerik:RadNumericTextBox>
                                     </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                        </tr>
                           <%-- <tr>
                            <td class="style1">
                                
                                <asp:Label ID="Label3" Text="Venta promedio de los últimos" runat="server">
                                </asp:Label>
                            </td>
                            <td class="style1">
                                   <telerik:RadNumericTextBox ID="TxtMeses" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" > 
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                 </telerik:RadNumericTextBox> &nbsp; meses
                                     </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkVencidos" runat="server" 
                                    Text="Vencido" />
                            </td>
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
                               <asp:CheckBox  runat= "server" ID= "ChkLegal" Text= "No considerar facturas en proceso legal"/>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
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
        .style1
        {
            height: 17px;
        }
    </style>
</asp:Content>

