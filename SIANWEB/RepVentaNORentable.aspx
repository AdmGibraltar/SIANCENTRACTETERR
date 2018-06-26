<%@ Page Title="Venta NO Rentable" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepVentaNORentable.aspx.cs" Inherits="SIANWEB.RepVentaNORentable" %>


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

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$BtnExportarExcel") != -1)
                    args.set_enableAjax(false);
            }
        </script>
                <style type="text/css"> 
       .button {
           border-top: 1px solid #96d1f8;
           background: #65a9d7;
           background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
           background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
           background: -moz-linear-gradient(top, #3e779d, #65a9d7);
           background: -ms-linear-gradient(top, #3e779d, #65a9d7);
           background: -o-linear-gradient(top, #3e779d, #65a9d7);
           padding: 10.5px 21px;
           cursor:pointer;
           -webkit-border-radius: 10px;
           -moz-border-radius: 10px;
           border-radius: 10px;
           -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
           -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
           box-shadow: rgba(0,0,0,1) 0 1px 0;
           text-shadow: rgba(0,0,0,.4) 0 1px 0;
           color: #f0f0f0;
           font-size: 14px;
           font-family: 'Lucida Grande', Helvetica, Arial, Sans-Serif;
           text-decoration: none;
           vertical-align: middle;
           }
        .button:hover {
           border-top-color: #305f7d;
           background: #305f7d;
           cursor:pointer;
           color: #ccc;
           }
        .button:active {
           border-top-color: #68abba;
           background: #68abba;
             cursor:pointer;
           }
        </style> 

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
               <telerik:AjaxSetting AjaxControlID="BtnExportarExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="RgConsulta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnBuscar">
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
    <div runat="server" id="divPrincipal" >
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
                            <td colspan ="2"  >
                              <asp:RadioButtonList runat= "server" ID = "RblTipoCd" RepeatDirection="Horizontal" Enabled = "false">
                              <asp:ListItem Selected="True" Text="CDI" Value="1"></asp:ListItem>
                               <asp:ListItem  Text="CDC" Value="2"></asp:ListItem>
                              </asp:RadioButtonList>
                            </td>
                          
                            <td >
                            </td>
                        </tr>




                              <tr>
                            <td>
                               <asp:Label ID="Label1" Text="Region" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                              <asp:RadioButtonList runat= "server" ID = "RadioButtonList1" RepeatDirection="Horizontal" >
                                <asp:ListItem Selected="True" Text="Todos" Value="0"></asp:ListItem>
                                <asp:ListItem  Text="NORTE" Value="1"></asp:ListItem>
                                <asp:ListItem  Text="SURESTE" Value="2"></asp:ListItem>
                                <asp:ListItem  Text="OCCIDENTE" Value="3"></asp:ListItem>
                                <asp:ListItem  Text="CENTRO" Value="4"></asp:ListItem>
                              </asp:RadioButtonList>
                            </td>

                              <%--   <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" 
                                        CssClass="button" onclick="BtnBuscar_Click"   />--%>
                         
                            <td>
                            </td>
                        </tr>                 






                               <tr>
                            <td class="style1">
                                <asp:Label ID="Label3" Text="Mes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td class="style1">
                                <telerik:RadComboBox ID="cmbmesini" runat="server" Width="150px" >
                                </telerik:RadComboBox>
                            </td>
                            <td class="style1">
                            </td>
                        </tr>
                               <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Año" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbanioini" runat="server" Width="150px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                               <asp:Label ID="Label5" Text="Nivel" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                              <asp:RadioButtonList runat= "server" ID = "RblTipoReporte" RepeatDirection="Horizontal" >
                                <asp:ListItem Selected="True" Text="Región" Value="1"></asp:ListItem>
                                <asp:ListItem  Text="Región-CD" Value="2"></asp:ListItem>
                                <asp:ListItem  Text="Representante" Value="3"></asp:ListItem>
                                <asp:ListItem  Text="Territorio" Value="4"></asp:ListItem>
                                <asp:ListItem  Text="Cliente" Value="5"></asp:ListItem>
                                <asp:ListItem  Text="CD" Value="6"></asp:ListItem>
                              </asp:RadioButtonList>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                               <asp:Label ID="Label2" Text="CD" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCD" runat="server" MaxLength="256">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>


                              <tr>
                            <td>
                               <asp:Label ID="Label7" Text="RIK" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtRIK" runat="server" MaxLength="256">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>





                        <tr>
                            <td>
                               <asp:Label ID="Label8" Text="Territorio" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtTERR" runat="server" MaxLength="256">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>



                        <tr>
                            <td>
                               <asp:Label ID="Label9" Text="Cliente" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCte" runat="server" MaxLength="256">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>


                           <tr>
                            <td>
                              
                            </td>
                            <td>
                      
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                        <td colspan ="3">
                     
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
        <table width = "1400px">
        <tr>
        <td>
    <%--     <asp:Button ID="BtnExportarExcel" runat="server" Text="Exportar" 
                                        CssClass="button" onclick="BtnExportarExcel_Click"   />--%>
        </td>
        </tr>
        <tr>
        <td>
            <%-- <telerik:RadPivotGrid ID="RgConsulta" runat="server"  AllowSorting="true" PageSize="20"
                                EnableConfigurationPanel="true" 
                 ConfigurationPanelSettings-Position="Left" Width="1400px"
                                ShowFilterHeaderZone="false" AllowPaging="true" 
                 Skin="Metro" onneeddatasource="RadPivotGrid1_NeedDataSource" 
                 OnPivotGridCellExporting="RadPivotGrid1_PivotGridCellExporting"
                 OnPivotGridBiffExporting="RadPivotGrid1_PivotGridBiffExporting"
                 RowTableLayout="Compact" 
                >
                                <Fields>
                                    <telerik:PivotGridRowField DataField="Region"></telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField DataField="CDI"></telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField DataField="Representante"></telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField DataField="Cliente"></telerik:PivotGridRowField>
                                    <telerik:PivotGridAggregateField DataField="MetaUBImporte"  DataFormatString="{0:N2}" Caption="Meta UB" ></telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="MetaUBPorc"  DataFormatString="{0:N2}" Caption="Meta UB %" Aggregate="Average"></telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="ProyUBImporte"  DataFormatString="{0:N2}" ></telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="ProyUBPorc"  DataFormatString="{0:N2}"></telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="RealUBImporte"   DataFormatString="{0:N2}"></telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="RealUBPorc" DataFormatString="{0:N2}" ></telerik:PivotGridAggregateField>
                                </Fields>


                </telerik:RadPivotGrid>--%>
        </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</asp:Content>

