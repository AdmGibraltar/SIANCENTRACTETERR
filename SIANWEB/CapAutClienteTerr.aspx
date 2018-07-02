<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master" AutoEventWireup="true" CodeBehind="CapAutClienteTerr.aspx.cs" Inherits="SIANWEB.CapAutClienteTerr" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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

            function CloseWindow(mensaje) {
                ////debugger;
                var cerrarWindow = radalert(mensaje, 350, 150, tituloMensajes);
                cerrarWindow.add_close(
                    function () {
                        ////debugger;
                        CloseAndRebind();
                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                ////debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
     </script>       
</telerik:RadCodeBlock>


<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>


    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RAM1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadToolBar1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" 
        OnButtonClick="RadToolBar1_ButtonClick1">
        <Items>
            
        </Items>
    </telerik:RadToolBar>


    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                <td>
                        <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="false" />
                </td>
                <td>
                        <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="false" />
                </td>
            </tr>
        </table>
        <table border = "0">
        <tr>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                            PageViewID="RadPageViewDGenerales" Selected="True" Value="DatosGenerales">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

               <%-- <telerik:RadMultiPage ID="RadMultiPage1" Runat="server">
                    <telerik:RadPageView ID="RadPageView1" runat="server" Height="300px">
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="450px" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Height="450px" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">--%>
                                
                                 <div id="formularioDatosGenerales" runat="server">
                                   <table>
                                    <tr>
                                    <td>
                                        <asp:Label ID="lblIdSolicitud" runat="server" Text="Id Solicitud"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIdSolicitud" runat="server" Width="70px"  MinValue="1"
                                            Enabled="false">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <br />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    </tr>
                                    <tr>
                                    <td>
                                        <asp:Label ID="lblId_Cd" runat="server" Text="Sucursal"></asp:Label>
                                    </td>
                                    <td> 
                                        <telerik:RadNumericTextBox ID="txtId_Cd" runat="server" Width="70px"  MinValue="1"
                                        Enabled="false">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                       
                                        </telerik:RadNumericTextBox>
                                        <br />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    </tr>
                                    <tr>
                                    <td>
                                        <asp:Label ID="lblId_Cliente" runat="server" Text="Id Cliente"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtId_Cliente" runat="server" Width="70px" MinValue="1"
                                        Enabled="false">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <br />
                                    </td>
                                    <td></td>
                                    <td></td>

                                    </tr>
                                    <tr>
                                    <td>
                                        <asp:Label ID="lblnomCliente" runat="server" Text="Nombre Cliente"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtNom_Cliente" runat="server" Width="450px"  MinValue="1"
                                        Enabled="false">
                                        </telerik:RadTextBox>
                                        <br />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    </tr>
                                    
                                    </table>
                                    <table>
                                    <tr>
                                    <td>
                                        
                                        <asp:Panel ID="PnlSolicitud" runat="server" GroupingText = "Solicitud" >
                                             <table border="0">
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lbltipo" runat="server" Text="Tipo de solicitud"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkNuevo" Text = "Nuevo" runat="server" Enabled = "false" />
                                                        <asp:CheckBox ID="chkActivar"  Text = "Activar" runat="server" Enabled = "false" />
                                                        <asp:CheckBox ID="chkDesactivar" Text = "Desactivar" runat="server" Enabled = "false" />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblclave" runat="server" Text="Id Territorio"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblNom_Territorio" runat="server" Text="Nombre Territorio"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadTextBox ID="txtNom_Territorio" runat="server" Width="350px" Enabled="false">
                                                        </telerik:RadTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblDimension" runat="server" Text="Dimensión"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtDimension" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblPesos" runat="server" Text="Pesos"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtPesos" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblPotencial" runat="server" Text="Potencial"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtPotencial" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblManoObra" runat="server" Text="Mano de Obra"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtManoObra" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblGastos" runat="server" Text="Gastos Territorio"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtGastos" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblFletes" runat="server" Text="Fletes pagados al cliente"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtFletes" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblPorcentaje" runat="server" Text="Porcentaje"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtPorcentaje" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                 </table>     
                                        </asp:Panel>
                                        
                                    </td>
                                    <td colspan = "2">
                                        <asp:Panel ID="PnlAutorizado" runat="server" GroupingText = "Autorizado">
                                             <table border="0">
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lbltipo1" runat="server" Text="Tipo de solicitud"></asp:Label>
                                                    </td>
                                                    <td> 
                                                         <asp:CheckBox ID="chkNuevo1" Text = "Nuevo" runat="server" Enabled = "false"/>
                                                        <asp:CheckBox ID="chkActivar1"  Text = "Activar" runat="server" Enabled = "false"/>
                                                        <asp:CheckBox ID="chkDesactivar1" Text = "Desactivar" runat="server" Enabled = "false"/>
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblClave1" runat="server" Text="Id Territorio"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtClave1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblNom_Territorio1" runat="server" Text="Nombre Territorio"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadTextBox ID="txtNom_Territorio1" runat="server" Width="350px"  
                                                            Enabled="false">
                                                        </telerik:RadTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblDimension1" runat="server" Text="Dimensión"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtDimension1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblPesos1" runat="server" Text="Pesos"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtPesos1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblPotencial1" runat="server" Text="Potencial"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtPotencial1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblManoObra1" runat="server" Text="Mano de Obra"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtManoObra1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblGastos1" runat="server" Text="Gastos Territorio"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtGastos1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblFletes1" runat="server" Text="Fletes pagados al cliente"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtFletes1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="lblPorcentaje1" runat="server" Text="Porcentaje"></asp:Label>
                                                    </td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtPorcentaje1" runat="server" Width="70px"  MinValue="1"
                                                            Enabled="false">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <br />
                                                    </td>   
                                                  </tr>
                                                 </table>  
                                        </asp:Panel>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td colspan = "2">
                                            <asp:Panel ID="PnlComentarios" runat="server" GroupingText = "Comentarios">
                                           
                                            <telerik:RadEditor RenderMode="Lightweight" runat="server" SkinID="BasicSetOfTools" ID="RadEditorComentarios" EditModes="Design" Width="1498px" Height="150px">
                                                 <Modules>
                                                            <telerik:EditorModule Name="RadEditorHtmlInspector" Enabled="true" Visible="false" />
                                                            <telerik:EditorModule Name="RadEditorNodeInspector" Enabled="true" Visible="false" />
                                                            <telerik:EditorModule Name="RadEditorDomInspector" Enabled="false" />
                                                            <telerik:EditorModule Name="RadEditorStatistics" Enabled="false" />
                                                        </Modules>
                                            </telerik:RadEditor>
                                             </asp:Panel>
                                        </td>
                                    </tr>
                            </table>
                                </div>
       <%--                  </telerik:RadPane>
                         </telerik:RadSplitter>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>--%>
            </td>
        </tr>

        </table>
    </div>
</asp:Content>
