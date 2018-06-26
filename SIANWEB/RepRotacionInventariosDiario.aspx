<%@ Page Title="Rotación inventarios" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepRotacionInventariosDiario.aspx.cs" Inherits="SIANWEB.RepRotacionInventariosDiario" %>

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
                        continuarAccion = ValidacionesEspeciales();
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
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbanio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbmes">
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
                  <telerik:AjaxSetting AjaxControlID="RblTipoRep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
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
                            <td>
                                &nbsp;&nbsp;
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
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                        <td colspan= "3">
                         <asp:RadioButtonList runat= "Server" ID="RblTipoRep" RepeatDirection="Horizontal" OnSelectedIndexChanged="RblTipoRep_SelectedIndexChanged" AutoPostBack="True">
                         <asp:ListItem runat="Server" Value ="1" Text ="Actual" Selected="True" ></asp:ListItem>
                         <asp:ListItem runat="Server" Value ="2" Text ="Cierre"  ></asp:ListItem>
                         </asp:RadioButtonList>
                         
                        </td>
                        
                        </tr>
                           <td colspan= "3">
                           &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                         
                        </td>
                               <tr runat ="Server" id="trAnio" visible="False">
                            <td>
                                <asp:Label ID="Label3" Text="Año" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbanio" runat="server" Width="150px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                         <tr runat ="Server" id="trMes" visible="False">
                            <td>
                                <asp:Label ID="Label4" Text="Mes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbmes" runat="server" Width="150px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                         <tr runat ="Server" ID="trFecha">
                            <td>
                              <asp:Label ID="Label1" Text="Fecha" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                 <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="120px" DateInput-MaxLength="10">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                        </tr>
                             <tr>
                            <td>
                              
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                               <td>
                                 <asp:Label ID="Label2" Text="Tipo CD" runat="server"> </asp:Label>

                              </td>
                            <td colspan ="3">
                             <asp:RadioButtonList runat= "Server" ID="RblTipoCd" RepeatDirection="Vertical" >
                           <asp:ListItem runat="Server" Value ="1" Text ="CDI" Selected="True" ></asp:ListItem>
                             <asp:ListItem runat="Server" Value ="2" Text ="CDC"  ></asp:ListItem>
                         </asp:RadioButtonList>
                            </td>
                            
                        </tr>
                              <tr>
                                <td>
                               <asp:Label ID="Label5" Text="Tipo reporte" runat="server"> </asp:Label>

                              </td>
                             <td colspan ="2">
                             <asp:RadioButtonList runat= "Server" ID="RblTipoDesglose" RepeatDirection="Vertical">
                             <asp:ListItem runat="Server" Value ="1" Text ="General" Selected="True" ></asp:ListItem>
                             <asp:ListItem runat="Server" Value ="2" Text ="Detalle"  ></asp:ListItem>
                         </asp:RadioButtonList>
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
    </div>
</asp:Content>
