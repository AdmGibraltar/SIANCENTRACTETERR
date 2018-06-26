<%@ Page Title="Administrador para captación de pedidos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI_Admin.aspx.cs" Inherits="SIANWEB.ProAdminCapPedido_VentInst" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="edit" Value="edit" CssClass="modificar" ToolTip="Editar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Venta nueva" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
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
                                <asp:Label ID="Label3" runat="server" Text="Cliente inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCteIni" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Cliente final"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCteFin" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="cmbVencido" runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Territorio inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerIni" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Territorio final"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerFin" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Semana/Año actual"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtSem" runat="server" Width="50px" MaxLength="2"
                                    MinValue="0" Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtAnio" runat="server" Width="70px" MaxLength="4"
                                    MinValue="1990" Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                        </tr>
                        <tr>
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
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="80">
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="rg1_PageIndexChanged"
                                    OnItemCommand="rg1_ItemCommand" AllowPaging="True" PageSize="15" OnItemDataBound="rg1_ItemDataBound">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Cte_Credito" HeaderText="crédito" UniqueName="Cte_Credito"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Acs" HeaderText="Id_Acs" UniqueName="Id_Acs"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cte_Nom" HeaderText="Cliente" UniqueName="Cte_Nom">
                                                <HeaderStyle Width="400px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="Id_Ter">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Cantidad" HeaderText="Venta instalada" UniqueName="Acs_Cantidad"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Semana" HeaderText="Semana de entrega" UniqueName="Acs_Semana">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Anio" HeaderText="Año" UniqueName="Acs_Anio">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Id_Ped" UniqueName="Id_Ped"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Captar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Captar" CommandName="captar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Cancelar" Text="Cancelar"
                                                UniqueName="Cancelar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                ButtonCssClass="baja" Display="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="60px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                                ConfirmText="Se imprimirá el pago, tenga listo el formato en la impresora</br></br>"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir"
                                                Display="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label8" runat="server" Text="Total a captar"></asp:Label>
                                <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="70px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
        <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <%--Pedido Captado--%>
            <telerik:RadWindow ID="AbrirVentana_PedidoCaptado" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="350px" Height="200px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Pedido a editar" Modal="True"
                OnClientClose="CerrarWindow_ClientEvent_Pedido" OnClientPageLoad="LimpiarBanderaRebind_Pedido">
            </telerik:RadWindow>
            <%-- PEDIDOS VENTA INSTALADA --%>
            <telerik:RadWindow ID="AbrirVentana_PedidoVI" runat="server" Behaviors="Move,Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="910px" Height="610px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="" Modal="True" OnClientClose="CerrarWindow_Event"
                ShowContentDuringLoad="False">
            </telerik:RadWindow>
            <%--REPORTES--%>
            <telerik:RadWindow ID="RWReporte" runat="server" Behaviors="Move,Close,Resize,Maximize"
                Opacity="100" VisibleStatusbar="False" Width="920px" Height="600px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Reporte" Modal="true" ShowContentDuringLoad="False"
                OnClientClose="CerrarWindow_Event">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // ---------------------
            // Variables de permiso
            // ---------------------
            var permisoGuardar = '<%= _PermisoGuardar %>'
            var permisoModificar = '<%= _PermisoModificar %>'
            var permisoEliminar = '<%= _PermisoEliminar %>'
            var permisoImprimir = '<%= _PermisoImprimir %>'

            function AbrirVentana_ProPedidoVI(Id, guardar, modificar, eliminar, imprimir, Anio, semana) {
                //debugger;
                var oWnd = radopen("ProPedidoVI.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana, "AbrirVentana_PedidoVI");
                //oWnd.center();
                oWnd.Maximize();
            }

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
                var button = args.get_item();
                continuarAccion = true;

                switch (button.get_value()) {
                    case 'edit':
                        AbrirVentana_Pedido();
                        continuarAccion = false;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function AbrirVentana_Pedido() {
                var oWnd = radopen("CapPedidoCaptado.aspx", "AbrirVentana_PedidoCaptado");
                //oWnd.center();
                oWnd.Maximize();
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de Pedido se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent_Pedido(sender, eventArgs) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                if (HD_GridRebind.value == '1') {
                    ModificaBanderaRebind_Pedido('0');
                    //refreshGrid_FacturaPedido();  <---- se comentariza para abrir directamente la pantalla
                    //a diferencia de la facturacion de remisiones en la que si se requiere ir al servidor para validar
                    //la variable de sesion de facturacion de remisiones, aqui se invoca directamente la pantalla de 
                    //facturacion ya que en ella se valida directamente si el usuario eligio un pedido haciendo clic en el boton 'Aceptar'
                    //de la pantalla de 'factura pedido'. Si el pedido era válido, la variable de sesion de pedido trae un valor de lo cntrario en nula.
                    var oWnd = radopen("ProPedidoVI.aspx?IdVI=" + 0 + "&PermisoGuardar=" + true + "&PermisoModificar=" + true + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir=" + permisoImprimir + "&Anio=" + 0 + "&Semana=" + 0, "AbrirVentana_PedidoVI");
                    //oWnd.center();
                    oWnd.Maximize();
                }
            }
            function LimpiarBanderaRebind_Pedido(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind_Pedido('0');
            }

            function ActivarBanderaRebind_Pedido() {
                //debugger;
                ModificaBanderaRebind_Pedido('1');
            }

            function ModificaBanderaRebind_Pedido(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
