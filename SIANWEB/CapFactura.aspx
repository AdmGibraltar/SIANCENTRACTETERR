<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapFactura.aspx.cs" Inherits="SIANWEB.CapFactura" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            function _PreValidarFechaEnPeriodo() {
                //debugger;
                if ('<%= FechaEnable %>' == '1') {
                    _ValidarFechaEnPeriodo();
                } 
            }

            function ClientTabSelecting(sender, args) {
                if ('<%= FechaEnable %>' == '1') {
                    continuarAccion = _ValidarFechaEnPeriodo();
                    args.set_cancel(!continuarAccion);
                }
                else {
                    continuarAccion = true;
                }
                args.set_cancel(!continuarAccion);
                if (continuarAccion) {
                    var Ser = $find('<%= txtId.ClientID %>');
                    var Mov = $find('<%= txtMov.ClientID %>');
                    var Cte = $find('<%= txtCliente.ClientID %>');
                    var Ter = $find('<%= txtTerritorio.ClientID %>');

                    if (Ser.get_value() == "") {
                        radalert('Por favor seleccione la serie del consecutivo antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Mov.get_value() == "") {
                        radalert('Por favor seleccione el tipo de movimiento antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Cte.get_value() == "") {
                        radalert('Por favor capture el cliente antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Ter.get_value() == "") {
                        radalert('Por favor capture el territorio antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                }
            }

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Orden de Compra
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesOrdenCompra() {
                //debugger;
                var txtId = $find('<%= txtId.ClientID %>');
                LimpiarTextBox(txtId);
            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {
                //debugger;
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lblFac_CantidadClientId = '';
            var txtFac_CantidadClientId = '';
            var lbl_cmbProductoClientId = '';
            var txtId_PrdClientId = '';
            var lbltxtTerritorioPartidaClientId = '';
            var txtTerritorioPartidaClientId = '';
            var txtId_PrdAdeClientId = '';
            var lblTxtClienteExternoClientId = '';
            var txtClienteExternoClientId = '';
            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {
                //debugger;
                var continuarAccion = true;
                //obtener controles de formulario de inserión/edición de Grid
                var lblFac_Cantidad = document.getElementById(lblFac_CantidadClientId);
                var txtFac_Cantidad = $find(txtFac_CantidadClientId);
                var lbl_cmbProducto = document.getElementById(lbl_cmbProductoClientId);
                var txtId_Prd = $find(txtId_PrdClientId);
                var lbltxtTerritorioPartida = document.getElementById(lbltxtTerritorioPartidaClientId);
                var txtTerritorioPartida = $find(txtTerritorioPartidaClientId);
                var lblTxtClienteExterno = document.getElementById(lblTxtClienteExternoClientId);
                var txtClienteExterno = $find(txtClienteExternoClientId);
                var txtId_PrdAde = $find(txtId_PrdAdeClientId);

                try {
                    //Limpiar contenedores de mensaje de validación
                    lblFac_Cantidad.innerHTML = '';
                    lbl_cmbProducto.innerHTML = '';
                    lbltxtTerritorioPartida.innerHTML = '';
                    if (lblTxtClienteExterno != null) {
                        lblTxtClienteExterno.innerHTML = '';
                    }
                } catch (e) {
                }

                //---------------------------------
                //inicia validaciones de formulario
                //---------------------------------
                //validar cliente
                //si el movimiento es 70
                var cmbMov = $find('<%= cmbMov.ClientID %>');
                if (cmbMov.get_value() == '70') {
                    if (txtClienteExterno != null) {
                        if (txtClienteExterno.get_textBoxValue() == '') {
                            lblTxtClienteExterno.innerHTML = '*Requerido';
                            continuarAccion = false
                        }
                        else {
                            //validar que no sea el mismo cliente que el de los datos generales de la factura
                            var txtCliente = $find('<%= txtCliente.ClientID %>');
                            if (txtCliente.get_textBoxValue() == txtClienteExterno.get_textBoxValue()) {
                                var alertCliente = radalert('El cliente no debe ser el mismo que el capturado en la pestaña de datos generales', 330, 150, tituloMensajes);
                                alertCliente.add_close(
                                    function () {
                                        txtClienteExterno.focus();
                                    });
                                continuarAccion = false
                            }
                        }
                    }
                    else
                        continuarAccion = false
                }

                //validar territorio
                if (txtTerritorioPartida != null) {
                    if ((txtTerritorioPartida.get_textBoxValue()) == '' && (txtId_Prd.get_textBoxValue() == '')) {
                        //                     lbltxtTerritorioPartida.innerHTML = '*Requerido';
                        alert("EL CAMPO TERRITORIO ES REQUERIDO");
                        continuarAccion = false
                    }
                    else
                        if (txtId_Prd.get_textBoxValue() == '') {
                            //                       lbl_cmbProducto.innerHTML = '*Requerido';
                            alert("EL CAMPO PRODUCTO ES REQUERIDO");
                            continuarAccion = false
                        }
                }
                else
                    continuarAccion = false



                //validar cantidad
                if (txtFac_Cantidad != null) {
                    if (txtFac_Cantidad.get_textBoxValue() == '') {
                        lblFac_Cantidad.innerHTML = '*Requerido';
                        continuarAccion = false
                    }
                    else
                        if (parseInt(txtFac_Cantidad.get_textBoxValue()) <= 0) {
                            lblFac_Cantidad.innerHTML = '*Requerido. El valor debe ser mayor a 0';
                            continuarAccion = false
                        }
                }
                else
                    continuarAccion = false
                return continuarAccion
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }

                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'new':
                        continuarAccion = true;
                        break;

                    case 'save':
                        if (Page_ClientValidate()) {
                            button.set_enabled(false);
                        }

                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        continuarAccion = _ValidarFechaEnPeriodo();
                        //                    if (continuarAccion == false) {
                        radTabStrip.get_allTabs()[0].select();
                        //                    }
                        break;

                    case 'facEspecial':
                        var radGrid = $find('<%= rgFacturaDet.ClientID %>');
                        var MasterTable = radGrid.get_masterTableView();
                        var length = MasterTable.get_dataItems().length;

                        if (length != '' && length > 0) {
                            AbrirVentana_FacturaEspecial();
                            continuarAccion = false;
                        }
                        else {
                            var alertaFEsp = radalert('No se ha agregado ningún producto', 330, 150, tituloMensajes);
                        }
                        break;
                }

                var HF = document.getElementById('<%= HF_VI.ClientID %>');
                if (continuarAccion == true && HF.value == 'false') {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }
                args.set_cancel(!continuarAccion);
            }

            function HabilitarGuardar() {
                var toolBar = $find("<%=RadToolBar1.ClientID %>");
                var button = toolBar.findItemByValue("save");
                button.set_enabled(true);
            }


            function ObtenerControlFecha() {
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                return txtFecha._dateInput;
            }
            //Oculta o visualiza la columna de cliente del Grid
            function rgFacturaDet_HiddeColumn(ocultar) {
                var radGrid = $find('<%= rgFacturaDet.ClientID %>');
                var table = radGrid.get_masterTableView();
                var column = table.getColumnByUniqueName("Id_CteExt");

                if (ocultar)
                    table.hideColumn(column.get_element().cellIndex);
                else
                    table.showColumn(column.get_element().cellIndex);
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

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura especial
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_FacturaEspecial() {
                ////debugger;
                var txtMonedaFE = $find('<%= txtMoneda.ClientID %>');
                var txtClienteFE = $find('<%= txtCliente.ClientID %>');
                var txtTotal = $find('<%= txtImporte.ClientID %>');
                var HD_IVAfacturacion = document.getElementById('<%= HD_IVAfacturacion.ClientID %>');
                var txtDescuento1 = $find('<%= txtDescuento1.ClientID %>');
                var txtDescuento2 = $find('<%= txtDescuento2.ClientID %>');

                var Folio = document.getElementById('<%= hiddenId.ClientID %>');
                var Id_FacSerie = document.getElementById('<%= HdId_FacSerie.ClientID %>');

                var oWnd = radopen("CapFactura_Especial.aspx?Id_Cte="
                + txtClienteFE.get_textBoxValue()
                + "&Id_Mon=" + txtMonedaFE.get_textBoxValue()
                + "&Fac_ImporteTotal=" + txtTotal.get_textBoxValue()
                + "&IVAfacturacion=" + HD_IVAfacturacion.value
                + "&Descuento1=" + txtDescuento1.get_textBoxValue()
                + "&Descuento2=" + txtDescuento2.get_textBoxValue()
                + "&Folio=" + Folio.value //clave de la factura
                + "&Id_FacSerie=" + Id_FacSerie.value 
                + "&Modificar=" + '<%= HabilitarGuardar %>'
                , "AbrirVentana_FacturaEspecial");
                oWnd.center();
                oWnd.Maximize();
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid_FacturaEspecial() {
                ////debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura especial se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent_FacturaEspecial(sender, eventArgs) {
            }

            function LimpiarBanderaRebind_FacturaEspecial(sender, eventArgs) {
                ////debugger;
                ModificaBanderaRebind_FacturaEspecial('0');
            }

            function ActivarBanderaRebind_FacturaEspecial() {
                ////debugger;
                ModificaBanderaRebind_FacturaEspecial('1');
            }
            function AjustarCentavos() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('AjustarCentavos');
            }
            function ModificaBanderaRebind_FacturaEspecial(valor) {
                ////debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_FacturaEspecial.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgFacturaDet_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //cuando el campo de texto pirde el foco
            function txtMov_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMov.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbMov_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMov.ClientID %>'));

                //si es movimiento 70, hace visible la columna 70 del grid
                var itemCombo = eventArgs.get_item();
                if (itemCombo != null)
                    if (itemCombo.get_value() == '70')
                        rgFacturaDet_HiddeColumn(false);
                    else
                        rgFacturaDet_HiddeColumn(true);
            }

            //cuando el campo de texto pirde el foco
            function txtCliente_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtRepresentante_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbRepresentante_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentante.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtMoneda_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMoneda.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbMoneda_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMoneda.ClientID %>'));
            }

            //Para el combo de Territorios dentro del Grid
            var txtTerritorioPartida;
            var cmbTerritorioPartida;

            function txtTerritorioPartida_OnLoad(sender, args) {
                txtTerritorioPartida = sender;
            }
            function cmbTerritorioPartida_OnLoad(sender, args) {
                cmbTerritorioPartida = sender;
            }

            //cuando el campo de texto de edición del Grid de TerritorioPartida pirde el foco
            function txtTerritorioPartida_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbTerritorioPartida);
            }


            //cuando el combo de edición del Grid de TerritorioPartida cambia de indice
            function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida);
            }

            //------------------------------------------------------------------------------------//
            //--PARA COMBOS DE ADENDA - EVITAR ESCRITURA------------------------------------------//            
            //------------------------------------------------------------------------------------//

            //Para el combo de ProductosAdenda dentro del Grid
            var txtId_PrdAde;
            var cmbProductoAde;

            function txtId_PrdAde_OnLoad(sender, args) {
                txtId_PrdAde = sender;
            }

            function cmbProductoAde_OnLoad(sender, args) {
                cmbProductoAde = sender;
                var input = cmbProductoAde.get_inputDomElement();
                input.onkeydown = onKeyDownHandler;
            }

            function onKeyDownHandler(e) {
                if (!e)
                    e = window.event;
                var code = e.keyCode || e.which;
                //do not allow any of these chars to be entered: !@#$%^&*()    
                if (code != 9) {
                    e.returnValue = false;
                    if (e.preventDefault) {
                        e.preventDefault();
                    }
                }
            }

            //cuando el campo de texto de ediciÃ³n del Grid de Adenda pierde el foco
            function txtId_PrdAde_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbProductoAde);
            }

            //cuando el combo de ediciÃ³n del Grid de ProductoAdenda cambia de indice
            function cmbProductoAde_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtId_PrdAde);
            }

            //Para el combo de Productos dentro del Grid
            var txtId_Prd;
            var cmbProducto;

            function txtId_Prd_OnLoad(sender, args) {
                txtId_Prd = sender;
            }
            function cmbProducto_OnLoad(sender, args) {
                cmbProducto = sender;
            }

            //cuando el campo de texto de edición del Grid de clave de producto pirde el foco
            function txtId_Prd_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbProducto);
            }

            //cuando el combo de edición del Grid de producto cambia de indice
            function cmbProducto_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtId_Prd);
            }

            //Para el combo de Cliente Externo dentro del Grid
            var txtClienteExterno;
            var cmbClienteExterno;

            function txtClienteExterno_OnLoad(sender, args) {
                txtClienteExterno = sender;
            }

            function cmbClienteExterno_OnLoad(sender, args) {
                cmbClienteExterno = sender;
            }

            //cuando el campo de texto de edición del Grid de ClienteExterno pirde el foco
            function txtClienteExterno_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbClienteExterno);
            }

            //cuando el combo de edición del Grid de ClienteExterno cambia de indice
            function cmbClienteExterno_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtClienteExterno);
            }

            function Cantidad_Blur() {
            }           

            function abrirBuscar() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Buscar.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
                        }

            function abrirEstadistica() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtCliente.ClientID%>');
                            var cteNom = $find('<%=txtClienteNombre.ClientID%>');
                            var oWnd = radopen("Ventana_Estadisticas.aspx?cte=" + cte.get_value() + "&cteNom=" + cteNom.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
            }
            function abrirIndicadores() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Indicadores.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarIndicadores");
                            oWnd.center();
                        }
                    }
                }
            }

            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }

            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }


            function OnClientItemsRequestedHandler(sender, eventArgs) {
                debugger;
                //set the max allowed height of the combo  
                var MAX_ALLOWED_HEIGHT = 220;
                //this is the single item's height  
                var SINGLE_ITEM_HEIGHT = 22;

                var calculatedHeight = sender.get_items().get_count() * SINGLE_ITEM_HEIGHT;

                var dropDownDiv = sender.get_dropDownElement();

                if (calculatedHeight > MAX_ALLOWED_HEIGHT) {
                    setTimeout(function () { dropDownDiv.firstChild.style.height = MAX_ALLOWED_HEIGHT + "px"; }, 20);
                }
                else {
                    setTimeout(function () { dropDownDiv.firstChild.style.height = calculatedHeight + "px"; }, 20);
                }
            }  
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <%-- Factura Especial --%>
            <telerik:RadWindow ID="AbrirVentana_FacturaEspecial" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="700px" Height="500px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Factura especial" Modal="True"
                OnClientClose="CerrarWindow_ClientEvent_FacturaEspecial" ShowOnTopWhenMaximized="true"
                OnClientPageLoad="LimpiarBanderaRebind_FacturaEspecial" Localization-Restore="Restaurar"
                Localization-Maximize="Maximizar" Localization-Close="Cerrar" InitialBehaviors="Maximize">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_Buscar" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="600px" Height="415px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Buscar" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_BuscarPrecio" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="750px" Height="415px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Buscar" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_BuscarIndicadores" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="750px" Height="515px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Buscar" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
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
            <telerik:AjaxSetting AjaxControlID="cmbConsFacEle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMov">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkDesgloce">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="formularioTotales" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRetencion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDescuento1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDescuento2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturaDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFacturaEspecial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Mail"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Cancelar"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="facEspecial" Value="facEspecial" CssClass="facEspecial"
                ToolTip="Capturar factura especial" ImageUrl="Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True" Value="DatosGenerales">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles"
                                Value="Detalles">
                            </telerik:RadTab>
                            
                            <telerik:RadTab runat="server" AccessKey="F" Text="Addenda de &lt;u&gt;f&lt;/u&gt;acturación"
                                PageViewID="rpvAdendaFacturacion" Visible="false">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" AccessKey="R" Text="Addenda de &lt;u&gt;r&lt;/u&gt;efacturación"
                                PageViewID="rpvAdendaRefacturacion" Visible="false">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>

                    <%--SE CREAN LAS PESTAÑAS DE LA PAGINA, CAB FACTURA, DETALLES, ADENDA Y REF_ADENDA --%>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="450px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                                    <%--<asp:Panel ID="Panel1" runat="server">--%>
                                    <div id="formularioDatosGenerales" runat="server">
                                        <table border="0">
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="lblNumero" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtId" runat="server" Width="70px" MaxLength="9" MinValue="1"
                                                        Enabled="false">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                    <br />
                                                </td>
                                                <td width="129">
                                                    <asp:Label ID="lblConsFacEle" runat="server" Text="Serie del consecutivo"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbConsFacEle" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" Width="167px" DataValueField="Id" Filter="Contains"
                                                        MarkFirstMatch="true" AutoPostBack="True" OnClientBlur="Combo_ClientBlur" OnSelectedIndexChanged="cmbConsFacEle_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_cmbConsFacEle" runat="server" ControlToValidate="cmbConsFacEle"
                                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px">
                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                        <Calendar ID="cal_txtFecha" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput MaxLength="10" runat="server">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="val_txtFecha" runat="server" ControlToValidate="txtFecha"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTMov" runat="server" Text="T. mov."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMov" runat="server" Width="70px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnBlur="txtMov_OnBlur" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadComboBox ID="cmbMov" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo"
                                                        OnClientSelectedIndexChanged="cmbMov_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbMov_SelectedIndexChanged"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                        Width="300px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtMov" runat="server" ControlToValidate="txtMov"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblReq" runat="server" Text="Req."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtReq" runat="server" MaxLength="50" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="lblNumeroPedido" runat="server" Text="Número de pedido"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPedido" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPedido" runat="server" Text="Pedido"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtPedidoDesc" runat="server" MaxLength="50" Width="250px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
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
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" AutoPostBack="true" MaxLength="9"
                                                        MinValue="1" OnTextChanged="txtCliente_TextChanged" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" ReadOnly="True" Width="295px">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtCliente"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                        ToolTip="Buscar" ValidationGroup="buscar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTerritorio_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="true" MaxHeight="300px" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_PreValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged" Width="300px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                        <div style="display: none">
                                                                            <asp:Label ID="lbl_Id_Rik" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Rik_Nombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtTerritorio" runat="server" ControlToValidate="txtTerritorio"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRepresentante" runat="server" Text="Representante"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRepresentanteStr" runat="server" Enabled="false" Width="295px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtRepresentante" runat="server" ControlToValidate="txtRepresentante"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblContacto" runat="server" Text="Contacto"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtContacto" runat="server" MaxLength="40" Width="325px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td style="height: 35px">
                                                    <asp:CheckBox ID="chkDesgloce" runat="server" Text="Desglose del I.V.A." OnCheckedChanged="chkDesgloceIva_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </td>
                                                <td style="height: 35px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRetencion" runat="server" Text="Retención del I.V.A. " OnCheckedChanged="chkRetencion_CheckedChanged"
                                                    AutoPostBack="true" />
                                                    <telerik:RadNumericTextBox ID="txtPorcRetencion" runat="server" MinValue="0" Width="40px"
                                                            MaxLength="5" MaxValue="100" ReadOnly="True" > 
                                                            <NumberFormat DecimalDigits="2" />                                                            
                                                            </telerik:RadNumericTextBox> %
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTipoMoneda" runat="server" Text="Tipo de moneda"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtMoneda" runat="server" MaxLength="9" MinValue="1"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnBlur="txtMoneda_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cmbMoneda" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                    OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbMoneda_ClientSelectedIndexChanged"
                                                                    Width="150px">
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="val_txtMoneda" runat="server" ControlToValidate="txtMoneda"
                                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="45">
                                                    <asp:Label ID="lblCalle" runat="server" Text="Calle"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCalle" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo"  />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalleNumero" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCalleNumero" runat="server" MaxLength="15" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCP" runat="server" Text="C.P."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCP" runat="server" MaxLength="10" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="45">
                                                    <asp:Label ID="lblColonia" runat="server" Text="Colonia"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtColonia" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo"  />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMunicipio" runat="server" Text="Municipio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtMunicipio" runat="server" MaxLength="40" Width="150px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtEstado" runat="server" MaxLength="20" Width="150px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="45">
                                                    <asp:Label ID="lblRFC" runat="server" Text="R.F.C."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRFC" runat="server" MaxLength="13" Width="200px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtRFC" runat="server" ControlToValidate="txtRFC"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTel" runat="server" Text="Tel."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtTelefono" runat="server" MaxLength="20" Width="120px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCondiciones" runat="server" Text="Condiciones de pago"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCondiciones" runat="server" MaxLength="70" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtCondiciones" runat="server" ControlToValidate="txtCondiciones"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCondiciones0" runat="server" Text="Forma de pago"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbFormaPago" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                        OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo" OnSelectedIndexChanged="cmbConsFacEle_SelectedIndexChanged"
                                                        Width="167px" EmptyMessage="Seleccione cliente" LoadingMessage="Cargando...">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCondiciones1" runat="server" Text="Últimos 4 dígitos de la cuenta"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtUDigitos" runat="server" MaxLength="50" Width="80px">   
                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />                                                                                                                                   
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="110">
                                                    <asp:Label ID="lblOrdenEntrega" runat="server" Text="Orden de entrega"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtOrden" runat="server" Width="70px" MaxLength="10">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloNumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumeroGuia" runat="server" Text="Número de guía"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNumeroGuia" runat="server" MaxLength="9" MinValue="0"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 55px;">
                                                                <asp:Label ID="lblConducto" runat="server" Text="Conducto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtConducto" runat="server" MaxLength="50" Width="120px">
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                                </telerik:RadTextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblNumEntrega" runat="server" Text="Núm. de entrega"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNumeroEntrega" runat="server" MaxLength="9" MinValue="0"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescuento1" runat="server" Text="Descuento 1"></asp:Label>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtDescuento1" runat="server" AutoPostBack="true"
                                                                    MaxLength="9" MinValue="0" MaxValue="100" OnTextChanged="txtDescuento1_TextChanged"
                                                                    Width="70px" Value="0.00">
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSigno1" runat="server" Text="%"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtDescPorc1" runat="server" MaxLength="50" Width="200px">
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                                                </td>
                                                <td rowspan="2" valign="top">
                                                    <telerik:RadTextBox ID="txtNotas" runat="server" Height="45px" MaxLength="500" TextMode="MultiLine"
                                                        Width="300px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescuento2" runat="server" Text="Descuento 2"></asp:Label>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtDescuento2" runat="server" AutoPostBack="true"
                                                                    MaxLength="9" MinValue="0" MaxValue="100" OnTextChanged="txtDescuento2_TextChanged"
                                                                    Width="70px" Value="0.00">
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSigno2" runat="server" Text="%"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtDescPorc2" runat="server" MaxLength="50" Width="200px">
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--</asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>

                        <%--CREAMOS LA PESTAÑA 2 DETALLES DE LA FACTURA--%>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <asp:HiddenField ID="HD_Prd_UniEmp" runat="server" />
                            <asp:HiddenField ID="HD_Prd_InvFinal" runat="server" />
                            <asp:HiddenField ID="HD_Prd_Asignado" runat="server" />
                            <asp:HiddenField ID="HD_Prd_Disponible" runat="server" />
                            <asp:HiddenField ID="HD_Amortizacion" runat="server" />
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="455px" OnClientResized="onResize"
                                    BorderStyle="None">
                                    <%--<asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="810px">--%>
                                    <telerik:RadGrid ID="rgFacturaDet" runat="server" GridLines="None" AllowPaging="false"
                                        AutoGenerateColumns="False" OnNeedDataSource="rgFacturaDet_NeedDataSource" OnInsertCommand="rgFacturaDet_InsertCommand"
                                        OnUpdateCommand="rgFacturaDet_UpdateCommand" OnDeleteCommand="rgFacturaDet_DeleteCommand"
                                        OnItemDataBound="rgFacturaDet_ItemDataBound" OnItemCommand="rgFacturaDet_ItemCommand"
                                        OnPageIndexChanged="rgFacturaDet_PageIndexChanged" BorderStyle="None" DataMember="listaOrdCompraDet"
                                        OnItemCreated="rgFacturaDet_ItemCreated">
                                        <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Fac,Id_FacDet,Id_Prd,Id_CteExt,Id_Ter,Id_Rem,Rem_Cant"
                                            EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                            NoMasterRecordsText="No se encontraron registros." PageSize="9">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Id_Fac" UniqueName="Id_Fac"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_FacDet" HeaderText="Id_FacDet" UniqueName="Id_FacDet"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_CteExtN" UniqueName="Id_CteExtN"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClienteExternoNum" runat="server" Text='<%# Eval("Id_CteExt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtClienteExterno" runat="server" Width="50px" MaxLength="9"
                                                            Text='<%# Eval("Id_CteExt") %>' OnTextChanged="txtClienteExterno_TextChanged"
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtClienteExterno_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cliente externo" DataField="Id_CteExt" UniqueName="Id_CteExt"
                                                    Display="false">
                                                    <HeaderStyle Width="280px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClienteExterno" runat="server" Text='<%# Eval("Id_CteExtStr") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="border-bottom-color: Transparent">
                                                                    <asp:Label ID="lblTxtClienteExterno" runat="server" ForeColor="#FF0000"></asp:Label>
                                                                </td>
                                                                <td style="border-bottom-color: Transparent">
                                                                    <telerik:RadTextBox ID="txtNombreCliente" runat="server" ReadOnly="true" Width="280px"
                                                                        Text='<%# Bind("Id_CteExtStr") %>'>
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Ter" UniqueName="Id_TerN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTerritorioPartidaNum" runat="server" Text='<%# Eval("Id_Ter")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtTerritorioPartida" runat="server" Width="50px"
                                                            MaxLength="9" Text='<%# Eval("Id_Ter") %>' AutoPostBack="true" OnTextChanged="txtTerritorio_TextChanged">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtTerritorioPartida_OnBlur" OnLoad="txtTerritorioPartida_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Territorio" DataField="Id_Ter" UniqueName="Id_Ter">
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritorioPartida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_TerStr") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbltxtTerritorioPartida" runat="server" ForeColor="#FF0000" Visible="false" />
                                                        <telerik:RadComboBox ID="cmbTerritorioPartida" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                            MarkFirstMatch="true" LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbTerritorioPartida_ClientSelectedIndexChanged"
                                                            OnClientLoad="cmbTerritorioPartida_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px"
                                                            Width="100%" EnableLoadOnDemand="true" OnClientBlur="Combo_ClientBlur"  >
                                                            <ExpandAnimation Type="none" />
                                                            <CollapseAnimation Type="none" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN"
                                                    DataType="System.Int32">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="txtProducto_TextChanged"
                                                            AutoPostBack="true" OnLoad="txtProducto_Load">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnLoad="txtId_Prd_OnLoad" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_Prd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000" Visible="false"></asp:Label>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="100%"
                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Prd_Descripcion"
                                                    UniqueName="Prd_Descripcion" Display="false">
                                                    <HeaderStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# Eval("Prd_Descripcion").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox Width="0px" ID="txtPrd_Descripcion" runat="server" ReadOnly="true"
                                                            Text='<%# Eval("Prd_Descripcion").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Pres." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# Eval("Prd_Presentacion").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox Width="100%" ID="txtPrd_Presentacion" runat="server" ReadOnly="true"
                                                            Text='<%# Eval("Prd_Presentacion").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Unidades" DataField="Prd_UniNe" UniqueName="Prd_UniNe">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrd_UniNe" runat="server" Text='<%# Eval("Prd_UniNe").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtPrd_UniNe" runat="server" Width="100%" ReadOnly="true"
                                                            Text='<%# Eval("Prd_UniNe").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cantidad" DataField="Fac_Cant" UniqueName="Fac_Cant">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrd_Cantidad" runat="server" Text='<%# Eval("Fac_Cant") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFac_Cantidad" runat="server" Width="100%" MaxLength="9"
                                                            Text='<%# Eval("Fac_Cant") %>' OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:Label ID="lblVal_txtFac_Cantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Rem_Cant" HeaderText="Rem_Cant" UniqueName="Rem_Cant"
                                                    Display="false" ReadOnly="true">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="Fac_Precio" HeaderText="Precio" UniqueName="Fac_Precio">
                                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFac_Precio" runat="server" Text='<%# Bind("Fac_Precio", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFac_Precio" runat="server" Width="100%" MaxLength="9"
                                                            MinValue="0" Text='<%# Eval("Fac_Precio") %>'>
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Fac_Importe" HeaderText="Importe" UniqueName="Fac_Importe">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFac_Importe" runat="server" Text='<%# Bind("Fac_Importe", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblFac_ImporteEdit" runat="server" Text='<%# Bind("Fac_Importe", "{0:N2}") %>' />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                            PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                            PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                            ShowPagerText="True" PageButtonCount="3" />
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    <div id="botonFacturaEspecial" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="btnFacturaEspecial" runat="server" Visible="false" Text="Capturar factura especial"
                                            OnClientClick="AbrirVentana_FacturaEspecial()" />
                                    </div>
                                    <%--</asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <%--CREAMOS LA PESTAÑA 3 ADENDA--%>
                        <telerik:RadPageView ID="rpvAdendaFacturacion" runat="server" Height="450px">
                            <telerik:RadGrid ID="rgAdendaFacturacion" runat="server" AutoGenerateColumns="False"
                                GridLines="None" OnNeedDataSource="rgAdendaFacturacion_NeedDataSource" Width="100%"
                                BorderStyle="None" Height="35%">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" ScrollHeight="100px" SaveScrollPosition="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_AdeDet" HeaderText="Id_AdeDet" UniqueName="Id_AdeDet"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="nodo" HeaderText="Nodo" UniqueName="nodo" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="campo" HeaderText="Campo" UniqueName="campo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="valor" HeaderText="Valor" UniqueName="valor">
                                            <HeaderStyle Width="250px" />
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="200px" Text='<%# Bind("valor") %>'
                                                    MaxLength='<%# Bind("Longitud") %>'>
                                                    <ClientEvents OnKeyPress="SinComillas" />
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <%--INSERTAMOS NUEVO GRID PARA DETALLES DE ADENDA--%>
                            <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane3" runat="server" Height="455px" OnClientResized="onResize"
                                    BorderStyle="None">   
                                    <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Auto" HorizontalAlign="Justify">                              
                                        <telerik:RadGrid ID="rgFacturaDetAde" runat="server" GridLines="None" AllowPaging="false" Width="100%" 
                                        AutoGenerateColumns="False" OnNeedDataSource="rgFacturaDetAde_NeedDataSource" OnInsertCommand="rgFacturaDetAde_InsertCommand"
                                        OnUpdateCommand="rgFacturaDetAde_UpdateCommand" OnDeleteCommand="rgFacturaDetAde_DeleteCommand"
                                        OnItemDataBound="rgFacturaDetAde_ItemDataBound" OnItemCommand="rgFacturaDetAde_ItemCommand" 
                                        OnPageIndexChanged="rgFacturaDetAde_PageIndexChanged" BorderStyle="None"
                                        OnItemCreated="rgFacturaDetAde_ItemCreated" >   
                                                                                                                                                        
                                            <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Cons_AdeDet,Id_Prd"
                                            EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                            NoMasterRecordsText="No se encontraron registros." PageSize="9">                                            
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>  
                                              <telerik:GridTemplateColumn HeaderText="Consecutivo" DataField="Id_Cons_AdeDet"  UniqueName="Id_Cons_AdeDet" Visible="false" >
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Id_Cons_AdeDet" runat="server" Text='<%# Eval("Id_Cons_AdeDet")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <telerik:RadTextBox ID="txtId_Cons_AdeDet" runat="server" Width="50px" 
                                                           Text='<%# Eval("Id_Cons_AdeDet").ToString() %>'  >                                                                                                                                                                               
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                                                                                                                                                                                                                                                                                                                                              
                                            									
													<telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblIdProducto" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_PrdAde" runat="server" Width="50px"
                                                            MaxLength="9" Text='<%# Eval("Id_Prd") %>'  >
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtId_PrdAde_OnBlur" OnLoad="txtId_PrdAde_OnLoad" />                                                            
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                               

                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcionProducto" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <telerik:RadComboBox ID="cmbProductoAde" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                             LoadingMessage="Cargando..."    OnClientSelectedIndexChanged="cmbProductoAde_ClientSelectedIndexChanged" 
                                                            OnClientLoad="cmbProductoAde_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="100%" 
                                                             MarkFirstMatch="true"  EnableLoadOnDemand="true"  OnClientBlur="Combo_ClientBlur">
                                                            <ExpandAnimation Type="none" />
                                                            <CollapseAnimation Type="none" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>                                                                    
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                            PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                            PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                            ShowPagerText="True" PageButtonCount="3" />
                                        <GroupingSettings CaseSensitive="False" />  
                                         <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                              <Scrolling AllowScroll="True" ScrollHeight="180px" SaveScrollPosition="true" UseStaticHeaders="true" 
                                               />
                                        </ClientSettings>                                        
                                    </telerik:RadGrid>
                                    <div id="Div1" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="Button1" runat="server" Visible="false" Text="Capturar factura especial"
                                            OnClientClick="AbrirVentana_FacturaEspecial()" />
                                    </div>
                            <%--TERMINAMOS NUEVO GRID PARA DETALLES DE ADENDA--%>
                            </asp:Panel> 
                            </telerik:RadPane>
                            </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <%--CREAMOS LA PESTAÑA 4 REFACT_ADENDA--%>                        
                        <telerik:RadPageView ID="rpvAdendaRefacturacion" runat="server" Height="450px">
                            <telerik:RadGrid ID="rgAdendaReFacturacion" runat="server" AutoGenerateColumns="False"
                                GridLines="None" OnNeedDataSource="rgAdendaReFacturacion_NeedDataSource" Width="100%"
                                BorderStyle="None" Height="100%">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" ScrollHeight="100px" SaveScrollPosition="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_AdeDet" HeaderText="Id_AdeDet" UniqueName="Id_AdeDet"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="nodo" HeaderText="Nodo" UniqueName="nodo" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="campo" HeaderText="Campo" UniqueName="campo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="valor" HeaderText="Valor" UniqueName="valor">
                                            <HeaderStyle Width="250px" />
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="200px" Text='<%# Bind("valor") %>'
                                                    MaxLength='<%# Bind("Longitud") %>'>
                                                    <ClientEvents OnKeyPress="SinComillas" />
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <div id="formularioTotales" runat="server">
                        <table width="99%">
                            <tr>
                                <td>
                                </td>
                                <td width="70%">
                                </td>
                                <td>
                                    <asp:Label ID="lblImporte" runat="server" Text="Importe"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtImporte" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSubtotal" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtSubTotal" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="IVA">
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblIVA" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIVA" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
                <asp:HiddenField ID="hiddenId" runat="server" />
                 <asp:HiddenField ID="HdId_FacSerie" runat="server" />
                <asp:HiddenField ID="HiddenIdRF" runat="server" />
                <asp:HiddenField ID="hiddenId_Es" runat="server" Value="" />
                <asp:HiddenField ID="HD_GridRebind_FacturaEspecial" runat="server" Value="0" />
                <asp:HiddenField ID="HF_VI" runat="server" Value="false" />
                <asp:HiddenField ID="HiddenHeight" runat="server" />
                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
            </tr>
        </table>
    </div>
</asp:Content>
