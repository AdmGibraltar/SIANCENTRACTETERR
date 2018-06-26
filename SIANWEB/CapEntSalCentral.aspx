<%@ Page Title="Entradas y salidas" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapEntSalCentral.aspx.cs" Inherits="SIANWEB.CapEntSalCentral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .cssOcultar
        {
            display: none;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbNaturaleza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipoMovimento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntradaSalida">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divtotales" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            >
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png"  />
            
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 9pt;"  width="100%">
     
            <tr>
                <td>
                </td>
                <td>
                <table style="font-family: Verdana; font-size: 9pt;" width="100%" cellspacing = "5">
                   <tr>
                <td>
                &nbsp;
                </td>
                </tr>
                <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblId_MovC"  Text = "Número" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtId_MovC">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="LblMovC_Fecha"  Text = "Fecha" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="txtMovC_Fecha" >
                </asp:Label>
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                       <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblAlmacen"  Text = "Almacén" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtAlmacen">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="LblTipoMov"  Text = "Tipo movimiento" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="TxtTipoMov" >
                </asp:Label>
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                          <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblReferencia"  Text = "Referencia" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtMovC_Referencia">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="LblFechaRef"  Text = "Fecha referencia" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="TxtFechaRef" >
                </asp:Label>
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                          <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblRemitente"  Text = "Remitente" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtRemitente">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="LblAplContable"  Text = "Aplicación contable" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="TxtAplContable" >
                </asp:Label>
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                          <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblDestinatario"  Text = "Destinatario" Font-Bold ="true">
                </asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtDestinatario">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                 
                </td>
                <td>
                 
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                <tr>
                <td>
                &nbsp;
                </td>
                </tr>
                </table>
          
                    <table width="99%" runat="server" id="divtotales">
                    <tr>
                    <td  colspan="4">
                          
                                    <telerik:RadGrid ID="rgMovimentosDet" runat="server" OnNeedDataSource="rg_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None" OnItemDataBound="rg_ItemDataBound"
                                        OnItemCommand="rg_ItemCommand" OnInsertCommand="rg_InsertCommand"  OnItemCreated="rg_ItemCreated"
                                        OnUpdateCommand= "rg_UpdateCommand" 
                                       >
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                                        </ClientSettings>
                                        <MasterTableView EditMode="InPlace" NoMasterRecordsText="No se encontraron registros."
                                             DataKeyNames="Id_MovC">
                                        
                                            <Columns>
                                             <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Id_Alm" HeaderText="Id_Alm" UniqueName="Id_Alm"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Id_MovC" HeaderText="Id_MovC" UniqueName="Id_MovC"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Id_MovCDet" HeaderText="Id_MovCDet" UniqueName="Id_MovCDet"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="Id_Tm" UniqueName="Id_Tm"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MovC_Naturaleza" HeaderText="MovC_Naturaleza" UniqueName="MovC_Naturaleza"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_Prd"
                                                    DataType="System.Int32">
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblId_Prd" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                           ReadOnly="true" MinValue="1" Text='<%# Eval("Id_Prd") %>' BackColor="Transparent" 
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn HeaderText="Producto" DataField="Prd_Descripcion" UniqueName="Prd_Descripcion">
                                                    <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPrd_Descripcion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        
                                                        <telerik:RadTextBox ID="TxtPrd_Descripcion" runat="server" BackColor="Transparent"  ReadOnly="true" Width="100%"
                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn HeaderText="Pres." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPrd_Presentacion" runat="server" Text='<%# Eval("Prd_Presentacion").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox Width="100%" ID="TxtPrd_Presentacion" BackColor="Transparent"  runat="server" ReadOnly="true"
                                                            Text='<%# Eval("Prd_Presentacion").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn DataField="MovC_Cant" HeaderText="Cantid." 
                                                    UniqueName="MovC_Cant">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtMovC_Cant" runat="server" MinValue="1" BackColor="Transparent" 
                                                            MaxLength="9" Width="50px" Text='<%# Eval("MovC_Cant") %>' ReadOnly = "true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblMovC_Cant" runat="server" Text='<%# Eval("MovC_Cant") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                  <telerik:GridTemplateColumn DataField="MovC_CostoEst" HeaderText="Costo est." 
                                                    UniqueName="CostoEst">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtCostoEst" runat="server" MinValue="0" BackColor="Transparent" 
                                                            MaxLength="9" Width="50px" Text='<%# Eval("MovC_CostoEst") %>' ReadOnly = "true">
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblCostoEst" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("MovC_CostoEst")).ToString("N") %>' /></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn DataField="MovC_CostoFac" HeaderText="Costo fac." 
                                                    UniqueName="CostoFac">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtCostoFac" runat="server" MinValue="0"
                                                            MaxLength="9" Width="50px" Text='<%# Eval("MovC_CostoFac") %>' ontextchanged="TxtTotalFac_TextChanged" > 
                                                        </telerik:RadNumericTextBox ></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblCostoFac" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("MovC_CostoFac")).ToString("N") %>' /></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                  <telerik:GridTemplateColumn DataField="TotalEst" HeaderText="Total est." 
                                                    UniqueName="TotalEst">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTotalEst" runat="server" MinValue="0" MaxLength="9"
                                                            Width="50px" Text='<%# Bind("TotalEst") %>' BackColor="Transparent" ReadOnly="true"
                                                            CssClass="AlignRight">
                                                           
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTotalEst" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("TotalEst")).ToString("N") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="TotalFac" HeaderText="Total fac." 
                                                    UniqueName="TotalFac">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTotalFac" runat="server" MinValue="0" MaxLength="9"
                                                            Width="50px" Text='<%# Bind("TotalFac") %>' BackColor="Transparent" ReadOnly="true"
                                                            CssClass="AlignRight">
                                                           
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTotalFac" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("TotalFac")).ToString("N") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn DataField="Variación" HeaderText="Variación" 
                                                    UniqueName="Variacion">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtVariación" runat="server" MinValue="0" MaxLength="9"
                                                            Width="50px" Text='<%# Bind("Variacion") %>' BackColor="Transparent" ReadOnly="true"
                                                            CssClass="AlignRight">
                                                            
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblVariacion" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("Variacion")).ToString("N") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                             <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                    EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" HeaderText="" UpdateText="Actualizar">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </telerik:GridEditCommandColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                    </td>
                    <td>
                    </td>
                    </tr>
                        <tr ID="trTotal" runat="server" style="font-family: verdana; font-size: 8pt">
                        <td  width="50%">
                        </td>
                        <td align="right" width= "20%">
                                <asp:Label ID="LblTotal" runat="server" Text="Total" Font-Bold = "True" 
                                   ></asp:Label>
                        </td>
                            <td align="right" width= "10%">
                           <asp:Label ID="TxtTotal" runat="server" ></asp:Label>
                            </td>
                            <td width="20%" align="right">
                                </td>
                        </tr>
                        <tr ID="trAcumulado" runat="server" style="font-family: verdana; font-size: 8pt">
                        <td width="50%">
                        </td>
                        <td align="right"  width= "20%">
                                <asp:Label ID="LblAcumRecepciones" runat="server" Text="Acumulación en recepciones" Font-Bold = "true"></asp:Label>
                        </td>
                            <td align="right"  width= "10%">
                            <asp:Label ID="TxtAcumRecepciones" runat="server" ></asp:Label>
                            </td>
                            <td align="right" width="20%">
                                </td>
                        </tr>
                        <tr ID="trVariacion" runat="server" style="font-family: verdana; font-size: 8pt">
                        <td width="50%">
                        </td>
                        <td align="right" width= "20%">
                                <asp:Label ID="LblVariacion" runat="server" Text="Variación en compras" Font-Bold = "true"></asp:Label>
                        </td>
                            <td align="right"  width= "10%">
                            <asp:Label ID="TxtVariacion" runat="server" ></asp:Label>
                            </td>
                            <td align="right" width="20%">
              
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                <asp:HiddenField ID="HiddenHeight" runat="server" />
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function initDialog() {

            }




            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                    case 'delete':
                        continuarAccion = Confirma();
                        break;
                    case 'save':
                        button.set_enabled(false);
                        continuarAccion = _ValidarFechaEnPeriodo();
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
                else {
                    if (window.frameElement != null) {
                        if (window.frameElement.radWindow)
                            oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                    }
                    else
                        window.open("login.aspx");
                }
                return oWindow;
            }

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
            }

            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }



            function CloseWindow() {
                var cerrarWindow = radalert('El campo de referencia se encuentra vacío', 330, 150, '');
                cerrarWindow.add_close(
                function () {
                    //debugger;                        
                });
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }



            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }















            var mostrando_mensaje = false;
            function AlertaFocus_Mostrado(mensaje, control) {
                if (mostrando_mensaje) {
                    return;
                }
                var oWnd = radalert(mensaje, 340, 150);
                mostrando_mensaje = true;

                oWnd.add_close(function () {
                    mostrando_mensaje = false;
                    var target = $find(control);
                    if (target != null && (target.enabled || target._enabled)) {
                        target.focus();
                    }
                });
            }
           

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
