<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepCob_CostoFinyAho.aspx.cs" Inherits="SIANWEB.RepCob_CostoFinyAho" MasterPageFile="~/MasterPage/MasterPage01.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
            }

            function rtb_ButtonClick(sender, args) {
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

        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar" runat="server" Width="100%" dir="rtl"  OnButtonClick = "rtb_ButtonClick">
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
                        Width="248px" AutoPostBack="True" Height="19px" style="margin-left: 20px">
                    </telerik:RadComboBox>
                </td>
           </tr>
           <tr>
                <td>&nbsp;</td>
                <td>
                     <asp:Label ID="Label1" runat="server" Text="Fecha de Cierre"></asp:Label>
                </td>
                <td class="style2">
                    <telerik:RadDatePicker ID="dpFecha" runat="server" Width="120px" DateInput-MaxLength="10" style="margin-left: 20px">
                        <Calendar ID="Calendar" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" runat="server">
                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                TodayButtonCaption="Hoy">
                            </FastNavigationSettings>
                        </Calendar>
                        <DateInput ID="DateInput" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                        </DateInput>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                    </telerik:RadDatePicker>
                </td>
            </tr>
           <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblTasa" runat ="server" Text="Tasa porcentual (%)"></asp:Label>
                </td>
                <td class="style2">
                    <telerik:RadNumericTextBox  ID="txtTasa" runat="server" MaxLength ="3" Width="100px" style="margin-left: 20px" >
                    <NumberFormat DecimalDigits="2" GroupSeparator="." />
                    </telerik:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td> &nbsp;</td>
           </tr> 
           <tr>
               <td> &nbsp;</td>
               <td>
                    <label>Tipo de Reporte</label>
               </td>
               <td>
                    <asp:Label ID="lbltrimestre" runat="server" Text="Trimestres" Visible="false" ></asp:Label>
               </td>
           </tr>
           <tr>
               <td> &nbsp;</td>
               <td>
                   <asp:RadioButtonList ID="rblReportes" runat="server" BorderStyle ="Double" Width = "150" Height = "106" OnSelectedIndexChanged="Index_Changed"  AutoPostBack="true">
                   <asp:ListItem Text="General" Value = "1" Selected = "True" Enabled = "true"> </asp:ListItem>
                   <asp:ListItem Text="Trimestral" Value = "2" Enabled = "true"></asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td>
                   <asp:RadioButtonList ID="rblTrimestres" runat="server" BorderStyle ="Double" Width = "260" Visible ="false">
                   <asp:ListItem Text="Enero - Marzo " Value = "1" Selected = "True" Enabled = "true"> </asp:ListItem>
                   <asp:ListItem Text="Abril - Junio" Value = "2" Enabled = "true"></asp:ListItem>
                   <asp:ListItem Text="Julio - Septiembre" Value = "3" Enabled = "true"></asp:ListItem>
                   <asp:ListItem Text="Octubre - Diciembre" Value = "4" Enabled = "true"></asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>


        </table>
        <table>
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
    


    </telerik:RadCodeBlock>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    </asp:Content>
