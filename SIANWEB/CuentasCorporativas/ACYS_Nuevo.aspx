<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACYS_Nuevo.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.ACYS_Nuevo" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>
  

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
       <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
          </AjaxSettings>

     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick"
        onclientbuttonclicked="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
        </Items>
    </telerik:radtoolbar>


 <div id="divPrincipal" runat="server"  runat="server" style="font-family: Verdana; font-size: 8pt;">
      <br />
    <h4>Datos del ACYS</h4>

    <br />
    <br />

    
    <table>

            <tr>
                <td>Nombre Acys: </td>
                <td><asp:TextBox runat="server" ID="txtNombre"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" Text="*" 
                    ControlToValidate="txtNombre"
                    ValidationGroup="Guardar" 
                    ></asp:RequiredFieldValidator>
                
                </td>
                <td>Ejecutivo Cuenta:</td>
                <td><asp:TextBox runat="server" ID="txtEjecCuenta"></asp:TextBox>
                
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ErrorMessage="*" Text="*" 
                    ControlToValidate="txtEjecCuenta"
                    ValidationGroup="Guardar" 
                    ></asp:RequiredFieldValidator>
                
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>Lider Técnico</td>
                <td><asp:TextBox runat="server" ID="txtLiderTecnico"></asp:TextBox>
                
                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*" Text="*" 
                    ControlToValidate="txtLiderTecnico"
                    ValidationGroup="Guardar" 
                    ></asp:RequiredFieldValidator>

                </td>
            </tr>
    
    </table>

    <br />
    <br />

    <table>
        <tr style="background-color:#b3c6ff">
            <td>Dias Crédito</td>
            <td>Acuerdo Económico Productos</td>
            <td>Datos Fiscales</td>
            <td>Asignado a RIK</td>
            <td>MOV 80</td>
            <td>Nivel de ACYS</td>
            <td>Tipo de Cuenta</td>
        </tr>
         <tr>
            <td><asp:CheckBox id="chkDiasCredito" runat="server"/></td>
            <td><asp:CheckBox id="chkAcuerdoEcon" runat="server"/></td>
            <td><asp:CheckBox id="chkDatosFisc" runat="server"/></td>
            <td><asp:CheckBox id="chkAsigRIK" runat="server"/></td>
            <td><asp:CheckBox id="chkMOV80" runat="server"/></td>
            <td>
                      <telerik:RadComboBox ID="cmbNivelAcys" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="120px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                        </table>
	                                        </ItemTemplate>
                      </telerik:RadComboBox>
                      </td>
                        <td>

                      <telerik:RadComboBox ID="cmbTipoCuenta" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="120px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                        </table>
	                                        </ItemTemplate>
                      </telerik:RadComboBox>
            </td>

        </tr>
    </table>
</div>


      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">



            function jsFunction() {}




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
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                //                var cerrarWindow = radalert(mensaje, 330, 150);
                //                cerrarWindow.add_close(
                //                    function () {

                alert(mensaje);
                CloseWindow();
                //                    });
            }

             //---------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;


            }



        </script>
     </telerik:radcodeblock>
 </asp:Content>