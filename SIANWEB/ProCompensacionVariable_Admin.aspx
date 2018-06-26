<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProCompensacionVariable_Admin.aspx.cs" Inherits="SIANWEB.ProCompensacionVariable_Admin" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function confirmCallBackFnVI(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {

                    ajaxManager.ajaxRequest('ok');
                }
                else {
                    ajaxManager.ajaxRequest('no');
                }
            }

            function winDetailClosing(sender, arg) {
                arg.set_cancel(true);
                function confirmCallback(args) {
                    if (args) {
                        sender.remove_beforeClose(winDetailClosing);
                        sender.close();
                        sender.add_beforeClose(winDetailClosing);
                    }
                }
                radconfirm("¿Seguro que desea cerrar sin guardar?", confirmCallback);
            }

            function Confirma() {
                radconfirm("¿Desea imprimir el contrato comodato?", confirmCallBackFnVI, 400, 160)
                return false;
            }
            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
          

            //*******************************
            function ToolBar_ClientClick2(sender, args) {
                //debugger;
                var button = args.get_item();
               switch (button.get_value()) {
                    case 'new':
                        AbrirVentana_CapturaComponentes(0, true, true, true, true);
                        args.set_cancel(true);
                        break;
                    case 'remisionPedido':
                        var oWnd2 = radopen("CapCompensacionVariable.aspx", "AbrirVentana_CapturaComponentes");
                        oWnd2.center();
                        break;
                }
            }



            function AbrirVentana_CapturaComponentes(Id, guardar, modificar, eliminar, imprimir) {
                var oWnd = radopen("CapCompensacionVariable.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir, "AbrirVentana_CapturaComponentes");
               
                oWnd.center();
            }


            function AbrirVentana_ComponentesModificar(id) {
                var oWnd = radopen("CapCompensacionVariable.aspx?Id=" + id, "AbrirVentana_ComponentesModificar");
               
                oWnd.center();
            }

            function AbrirVentana_ComponentesImprimir(id) {
                var oWnd = radopen("RepComisiones.aspx?Id=" + id, "AbrirVentana_ComponentesImprimir");

                oWnd.center();
            }


            //*******************************************************
            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                //obtener controles de formulario de inserión/edición de Grid

                return true;
            }
            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }

            function ModificaBanderaRebind(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }

          

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$rgPedido$ctl00$ctl02$ctl00$ImgExportar") != -1)
                    args.set_enableAjax(false);
            }

           
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RAM1_AjaxRequest" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgPedido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick2">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="remisionPedido" Value="remisionPedido" ToolTip="Remisiona pedido"
                CssClass="remPedido" ImageUrl="~/Imagenes/blank.png" />
              <%--   <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />--%>

        </Items>
    </telerik:RadToolBar>

     <telerik:radwindowmanager id="RadWindowManager1" runat="server">
        <Windows>
            <%--jfcv 18oct2016 que pregunte antes de salir control de cambio 9 OnClientBeforeClose="winDetailClosing" --%>
            <telerik:RadWindow ID="AbrirVentana_CapturaComponentes" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="1120px" Height="890px"
                Animation="Fade" KeepInScreenBounds="true" Overlay="True" Title="Captura Configuración de Sistema de Compensación Variable" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true" OnClientBeforeClose="winDetailClosing">
            </telerik:RadWindow>
             
            <telerik:RadWindow ID="AbrirVentana_ComponentesModificar" runat="server" Opacity="100"
                InitialBehaviors="Close,Move,Resize"   Behaviors="Move, Close, Maximize,Resize" VisibleStatusbar="False" Width="1120px" Height="890px"
                Animation="Fade" KeepInScreenBounds="true" Overlay="True" Title="Editar  Configuración de Sistema de Compensación Variable" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true" OnClientBeforeClose="winDetailClosing" DestroyOnClose="false">
            </telerik:RadWindow>

             <telerik:RadWindow ID="AbrirVentana_ComponentesImprimir" runat="server" Opacity="100"
                InitialBehaviors="Close,Move,Resize"   Behaviors="Move, Close, Maximize,Resize" VisibleStatusbar="False" Width="1120px" Height="890px"
                Animation="Fade" KeepInScreenBounds="true" Overlay="True" Title="Simulador de Reporte de Compensación Variable" Modal="True"
                ReloadOnShow="true"  DestroyOnClose="false">
            </telerik:RadWindow>

              <telerik:RadWindow ID="RWReporte" runat="server" Behaviors="Move,Close,Resize,Maximize"
                Opacity="100" VisibleStatusbar="False" Width="920px" Height="600px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Reporte" Modal="true" ShowContentDuringLoad="False"
                OnClientClose="CerrarWindow_Event">
            </telerik:RadWindow>


        </Windows>
    </telerik:radwindowmanager>


   <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_ClvPag" runat="server" />
                    <asp:HiddenField ID="HD_GridRebind_RemisionEspecial" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenId" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="hf_spo" runat="server" />
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" runat="server" MaxHeight="300px" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="div1" runat="server">
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td width="110">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td width="80">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                </td>
                                <td width="10">
                                </td>
                                <td width="45">
                                </td>
                                <td>
                                </td>
                            </tr>
                                   <tr>
                                    <td>
                                        <asp:Label ID="lblcdi" Text="CDI" runat="server" Visible="False">
                                        </asp:Label></td>
                                    <td>
                                <telerik:RadComboBox ID="CmbCDI" MaxHeight="300px" runat="server"  Width="250px" Visible="False" >
                                </telerik:RadComboBox>
                                </td>
                                <td>
                                     
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    
                                </td>
                            
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblNombreDel" runat="server" Text="Nombre"></asp:Label>
                                </td>
                                <td colspan="9">
                                    <telerik:RadTextBox ID="txtNombre" runat="server" Width="380px" MaxLength="70">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            
                                     <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                        <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                        <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="150px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                        ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
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
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server"  Width="900px">
                            <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" PageSize="15"
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                OnItemCreated="rg_ItemCreated" OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand="rgPedido_ItemCommand"
                                Width="1100px" OnItemDataBound="rgPago_ItemDataBound">
                                <MasterTableView ClientDataKeyNames="Id_Sistema" CommandItemDisplay="Top">
                                     <CommandItemSettings ShowAddNewRecordButton="false"  />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" UniqueName="Id_Cd"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                       
 
                                       <telerik:GridBoundColumn DataField="Id_Sistema" HeaderText="Sistema" UniqueName="Id_Sistema" Display="False">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NombreSistema" HeaderText="Nombre Sistema" UniqueName="NombreSistema">
                                            <HeaderStyle Width="400px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        
                                         
                                          <telerik:GridBoundColumn DataField="FechaInicio" HeaderText="Fecha Inicial Vigencia" UniqueName="FechaInicio"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="FechaFin" HeaderText="Fecha Final Vigencia" UniqueName="FechaFin"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>

                                         <%-- <telerik:GridBoundColumn DataField="Rec_Observaciones" HeaderText="Estatus" UniqueName="Rec_Observaciones">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>--%>

                                         <telerik:GridBoundColumn DataField="FechaUltimaMod" HeaderText="Fecha Ultima Actualización" UniqueName="FechaUltimaMod"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>

                       
                                         
                                        <%--jfcv 11 dic 2015 agregar botón para editar 17 dic cambiar icono a botón editar por lapiz --%>
                                       
                                        <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgSoporte" runat="server" ImageUrl="~/Imagenes/Lapiz.gif" ToolTip="Editar"
                                                    CommandName="Soporte" Visible='true' />
                                            </ItemTemplate>
                                       
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridTemplateColumn>

                                
                                        
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                            ConfirmText="¿Desea desactivar el Sistema?" Text="Desactivar" UniqueName="DeleteColumn"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" HeaderText="Cancelar" Display="false" >
                                            <HeaderStyle Width="25px" />
                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                                        </telerik:GridButtonColumn>

                                         <telerik:GridTemplateColumn HeaderText="Simular" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgSimular" runat="server" ImageUrl="~/Imagenes/simulador.png" ToolTip="Simular Sistema"
                                                    CommandName="Simular" Visible='true' />
                                            </ItemTemplate>
                                       
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridTemplateColumn>

                                         <telerik:GridTemplateColumn HeaderText="Copiar" HeaderStyle-HorizontalAlign="Center"
                                             ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgCopiar" runat="server" ImageUrl="~/Imagenes/iconos/document_edit.png" ToolTip="Copiar Sistema"
                                                    CommandName="Copiar" Visible='true' />
                                            </ItemTemplate>
                                       
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridTemplateColumn>


                                         <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                            Display="false">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="NombreEstatus" HeaderText="Estatus" UniqueName="NombreEstatus">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </telerik:GridBoundColumn>
                                       
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
