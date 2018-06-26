<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos_Item.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.Permisos_Item" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
        <AjaxSettings>

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

         <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />

            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />

        </Items>
    </telerik:radtoolbar>


 <div id="divPrincipal" runat="server">

    <table width="100%">
    <thead>
        <tr>
            <th  style="font-family:verdana; font-size: 10pt;  border:1px solid black;  border-collapse:collapse;" colspan="9"    >Datos del Usuario</th>                                                         
        </tr>                                                        
    </thead> 
      <tr>

        <td style="vertical-align:top">
            <table width="100%" style="font-family:verdana;font-size: 9pt" >      
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>

                <tr>
                    <td>Nombre Completo:</td>
                    <td>
                        <telerik:RadTextBox ID="txtNombre" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>
                    
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" Text="*" 
                            ControlToValidate="txtNombre"
                            ValidationGroup="Guardar" 
                        ></asp:RequiredFieldValidator>

                    </td>


                </tr>
               <tr>
                    <td>Correo:</td>
                    <td><telerik:RadTextBox ID="txtCorreo" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>
                    
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*" Text="*" 
                            ControlToValidate="txtCorreo"
                            ValidationGroup="Guardar" 
                        ></asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>CDIK:</td>
                    <td><telerik:RadTextBox ID="txtCDIK" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>

                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*" Text="*" 
                            ControlToValidate="txtCDIK"
                            ValidationGroup="Guardar" 
                        ></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>Tel:</td>
                    <td><telerik:RadTextBox ID="txtTelefono" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>
                    
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ErrorMessage="*" Text="*" 
                            ControlToValidate="txtTelefono"
                            ValidationGroup="Guardar" 
                        ></asp:RequiredFieldValidator>

                    </td>
                </tr>

                <tr>
                    <td>Contraseña:</td>
                    <td>
                        <telerik:RadTextBox ID="txtContrasenia" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>
                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ErrorMessage="*" Text="*" 
                            ControlToValidate="txtTelefono"
                            ValidationGroup="Guardar" 
                        ></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>Confirmar Contraseña:</td>
                    <td><telerik:RadTextBox ID="txtContrasenia2" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>
                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ErrorMessage="*" Text="*" 
                            ControlToValidate="txtContrasenia2"
                            ValidationGroup="Guardar" 
                        ></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>Rol (Sistema Auditorias):</td>
                    <td>
                        <telerik:RadComboBox ID="cmbRol_Auditorias" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                            DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione..."
                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                            MarkFirstMatch="true" 
                                Width="370px" ReadOnly="True"
                            MaxHeight="250px">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                Width="50px" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
                                        </td>
                                    </tr>
                                    </table>
	                    </ItemTemplate>
                        </telerik:RadComboBox>

                     </td>
                </tr>

                <tr>
                    <td>Rol (E Commerce):</td>
                    <td>
       
                      <telerik:RadComboBox ID="cmbRol_Ecommerce" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                            DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione..."
                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                            MarkFirstMatch="true" 
                                Width="370px" ReadOnly="True"
                            MaxHeight="250px">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                Width="50px" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
                                        </td>
                                    </tr>
                                    </table>
	                    </ItemTemplate>
                        </telerik:RadComboBox>
        
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                    <tr>
                    <td></td>
                </tr>
                    <tr>
                    <td></td>
                </tr>
                    <tr>
                    <td></td>
                </tr>


                </table>
        </td>
        <td>
             <div style="overflow:auto;height:600px;">
              <telerik:RadTreeView  runat="Server" ID="treeEstructura" EnableDragAndDrop="true" 
                         EnableDragAndDropBetweenNodes="false" Skin="Vista" OnClientNodeChecked="clientNodeChecked" 
                         RenderMode="Lightweight"
                           AllowNodeEditing="false" ExpandAnimation-Type="Linear" CheckBoxes="true">
                
                             <ContextMenus>
                            </ContextMenus>

                            <Nodes>
                            </Nodes>
             </telerik:RadTreeView>
            </div>
    
        </td>
      </tr>
    </table>

 </div>

       

      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function UpdateAllChildren(nodes, checked) {
                var i;
                var test;
                for (i = 0; i < nodes.get_count(); i++) {
                    if (checked) {
                        nodes.getNode(i).check();
                    }
                    else {
                        nodes.getNode(i).set_checked(false);
                    }

                    if (nodes.getNode(i).get_nodes().get_count() > 0) {
                        UpdateAllChildren(nodes.getNode(i).get_nodes(), checked);
                    }
                }
            }
            function clientNodeChecked(sender, eventArgs) {
                var childNodes = eventArgs.get_node().get_nodes();
                var isChecked = eventArgs.get_node().get_checked();
                UpdateAllChildren(childNodes, isChecked);
            }



            function CloseAlert(mensaje) {
                //                var cerrarWindow = radalert(mensaje, 330, 150);
                //                cerrarWindow.add_close(
                //                    function () {

                //alert(mensaje);
                CloseAndRebind();
                //                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }




                    </script>
     </telerik:radcodeblock>

</asp:Content>