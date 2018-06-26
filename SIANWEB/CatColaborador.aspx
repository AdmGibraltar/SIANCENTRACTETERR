<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatColaborador.aspx.cs" Inherits="SIANWEB.CatColaborador" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
 

        <script type="text/javascript">
            function Colaborador_Focus() {
                //debugger;
                var combo = $find("<%= cmbColaboradorsLista.ClientID %>");
                combo.clearSelection();
            }

            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';
            var arregloSubFamilias = '';


            //--------------------------------------------------------------------------------------------------
            //Limpiar controles del catalogo de Colaborador
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesColaborador() {
                //debugger;

                //Controles de la pestaña 'Valuacion de proyectos'                    
                var txtId_Colaborador = $find('<%= txtId_Colaborador.ClientID %>');
                var txtId_Empleado = $find('<%= txtId_Empleado.ClientID %>');
                
                var chkColaboradorNuevo = document.getElementById('<%= chkColaboradorNuevo.ClientID %>');
                var txtNum_Nomina = $find('<%= txtNum_Nomina.ClientID %>');
                var txtNombreEmpleado = $find('<%= txtNombreEmpleado.ClientID %>');
                var txtIdSucursal = $find('<%= txtIdSucursal.ClientID %>');

                var cmbUEN = $find('<%= cmbUEN.ClientID %>');
                var txtEmpresa = $find('<%= txtEmpresa.ClientID %>');
                var txtTipoSucursal = $find('<%= txtTipoSucursal.ClientID %>');


                var txtPuesto = $find('<%= txtTipoSucursal.ClientID %>');
                var txtCorreo = $find('<%= txtCorreo.ClientID %>');
                var CmbTipoUsuario = $find('<%= CmbTipoUsuario.ClientID %>');
                var txtIdTipoUsuario = $find('<%= txtIdTipoUsuario.ClientID %>');





                LimpiarTextBox(txtId_Colaborador);
                LimpiarTextBox(txtId_Empleado);
                
                LimpiarCheckBox(chkColaboradorNuevo, true);
                LimpiarTextBox(txtNum_Nomina);

                LimpiarTextBox(txtNombreEmpleado);

                LimpiarTextBox(txtIdSucursal);

                LimpiarTextBox(txtEmpresa);
                LimpiarTextBox(txtTipoSucursal);


                LimpiarTextBox(txtTipoSucursal);
                LimpiarTextBox(txtCorreo);
                LimpiarTextBox(txtIdTipoUsuario);
                LimpiarComboSelectIndex0(CmbTipoUsuario);

                var txtFechaInicio = $find('<%= txtFechaInicio.ClientID %>');
                var cmbFam = $find('<%= cmbFam.ClientID %>');
                LimpiarTextBox(txtFechaInicio);








                var txtSueldoVariable = $find('<%= txtSueldoVariable.ClientID %>');
                var txtContribucion = $find('<%= txtContribucion.ClientID %>');


                LimpiarTextBox(txtSueldoVariable);
                LimpiarTextBox(txtContribucion);

                alert('limpiar');
                cmbUEN.trackChanges();
                for (var i = 0; i < cmbUEN.get_items().get_count(); i++) {
                    cmbUEN.get_items().getItem(i).set_checked(false);
                }
                cmbUEN.commitChanges();
                alert('limpiar fin');


            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un Colaborador
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {

                var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
            }

            //Validar datos que son requeridos Siempre
            function ValidarControlesRequeridos() {
                //debugger;
                var validacionResult = true;


//                lbl_Val_txtId_Colaborador = document.getElementById('<%= lbl_Val_txtId_Colaborador.ClientID %>');

//                lbl_Val_txtNombreEmpleado = document.getElementById('<%= lbl_Val_txtNombreEmpleado.ClientID %>');




//                lbl_Val_txtCorreo = document.getElementById('<%= lbl_Val_txtCorreo.ClientID %>');


//                lbl_Val_txtId_Colaborador.innerHTML = '';
//                lbl_Val_txtNombreEmpleado.innerHTML = '';
//                lbl_Val_txtCorreo.innerHTML = '';

//                //                if (ValidaObjetoRequerido($find('<%= txtId_Colaborador.ClientID %>'), lbl_Val_txtId_Colaborador, 0) == false) validacionResult = false

//                if (ValidaObjetoRequerido($find('<%= txtNombreEmpleado.ClientID %>'), lbl_Val_txtNombreEmpleado, 0) == false) validacionResult = false


//                if (ValidaObjetoRequerido($find('<%= txtCorreo.ClientID %>'), lbl_Val_txtCorreo, 0) == false) validacionResult = false



                return validacionResult;
            }


            //Valida datos requeridos que dependen de la captura de otros datos al momento de insertar o actualizar un Colaborador
            function ValidarControlesEspeciales() {
                var validacionResult = true;

                //obtener objetos (Labels) para desplegar avisos de dato requerido...
                var lbl_Val_txtNum_Nomina = document.getElementById('<%= lbl_Val_txtNum_Nomina.ClientID %>');
//                var lbl_val_txtEmpresa = document.getElementById('<%= lbl_val_txtEmpresa.ClientID %>');
//                var lbl_val_txtTipoSucursal = document.getElementById('<%= lbl_val_txtTipoSucursal.ClientID %>');


//                var lbl_val_txtTipoSucursal = document.getElementById('<%= lbl_val_txtTipoSucursal.ClientID %>');

//                lbl_Val_txtNum_Nomina.innerHTML = '';
//                lbl_val_txtEmpresa.innerHTML = '';
//                lbl_val_txtTipoSucursal.innerHTML = '';





//                lbl_val_txtTipoSucursal.innerHTML = '';

//                var txtIdSucursal = $find('<%= txtIdSucursal.ClientID %>');
//                var txtEmpresa = $find('<%= txtEmpresa.ClientID %>');
//                var txtTipoSucursal = $find('<%= txtTipoSucursal.ClientID %>');

//                //si el tipo de Colaborador es tipo accesorios (Id_Ptp == 1) y elige una opcion del combo de sistemas propietarios
//                //el agrupado de equipos de sistemas propietarios es requerido
//                if (txtIdSucursal.get_textBoxValue() == '1' && txtEmpresa.get_textBoxValue() != '') {
//                    if (ValidaObjetoRequerido($find('<%= txtTipoSucursal.ClientID %>'), lbl_val_txtPuesto, 0) == false) validacionResult = false
//                }

//                if (txtIdSucursal.get_textBoxValue() == '1') {
//                    if (ValidaObjetoRequerido($find('<%= txtNum_Nomina.ClientID %>'), lbl_Val_txtNum_Nomina, 0) == false) validacionResult = false
//                    if (ValidaObjetoRequerido($find('<%= txtPuesto.ClientID %>'), lbl_val_txtPuesto, 0) == false) validacionResult = false
//                }



//                //validar que tiempo de entrega y de transporte sean multiplos de 7

//                var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

//                //                if (validacionResult == true) {
//                //                    if (txtTentrega.get_textBoxValue() != '') {
//                //                        var tiempoEntrega = parseFloat(txtTentrega.get_textBoxValue());
//                //                        if ((tiempoEntrega % 7) != 0) {
//                //                            var Alerta_tiempoEntrega = radalert('El tiempo de entrega debe estar en múltiplos de 7', 600, 10, tituloMensajes);
//                //                            validacionResult = false
//                //                            Alerta_tiempoEntrega.add_close(
//                //                            function () {
//                //                                radTabStrip.get_allTabs()[1].select();
//                //                                txtTentrega.focus();
//                //                            });
//                //                        }
//                //                    }
//                //                }

//                //                if (validacionResult == true) {
//                //                    if (txtTtransporte.get_textBoxValue() != '') {
//                //                        var tiempoTransporte = parseFloat(txtTtransporte.get_textBoxValue());
//                //                        if ((tiempoTransporte % 7) != 0) {
//                //                            var Alerta_tiempoTransporte = radalert('El tiempo de transporte debe estar en múltiplos de 7', 600, 10, tituloMensajes);
//                //                            validacionResult = false
//                //                            Alerta_tiempoTransporte.add_close(
//                //                            function () {
//                //                                radTabStrip.get_allTabs()[1].select();
//                //                                txtTtransporte.focus();
//                //                            });
//                //                        }
//                //                    }
//                //                }

                return validacionResult;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid de precios de Colaborador
            var datePickerFechaInicioClientId = '';
            var datePickerFechaFinClientId = '';
            var txtObjetivoClientId = '';

            //Validación del formulario de insercion/edición de registro en el RadGrid de precios de Colaborador
            function ValidaFormGridPrecioColaboradors(accion) {
                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find(datePickerFechaInicioClientId);
                var datePickerFechaFin = $find(datePickerFechaFinClientId);
                var txtObjetivo = $find(txtObjetivoClientId);

                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;

                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                //validar que se capture la la fecha inicio.
                if (fechaInicio == null) {
                    var mensage = 'Favor de capturar la fecha de inicio';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }

                //validar que se capture la la fecha fin.
                if (fechaFin == null) {
                    var mensage = 'Favor de capturar la fecha de fin';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaFin._dateInput.focus(); });
                    return false
                }

                //validar rango correcto de fechas.
                if (fechaInicio > fechaFin) {
                    var mensage = 'La fecha de inicio, no debe ser mayor a la fecha de fin';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaFin._dateInput.focus(); });
                    return false
                }

                //validar que se capture la cantidad de pesos
                if (txtObjetivo.get_textBoxValue() == '') {
                    var mensage = 'Favor de capturar el precio de Colaborador';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { txtObjetivo.focus(); });
                    return false
                }

                if (parseFloat(txtObjetivo.get_textBoxValue()) == 0) {
                    var mensage = 'El precio debe ser mayor a 0';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { txtObjetivo.focus(); });
                    return false
                }
                return true
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
                //                for (i = 0; i < Page_Validators.length; i++) {
                //                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                //                }

                //debugger;
                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();

                        continuarAccion = ValidarControlesRequeridos();
                        if (continuarAccion == true) {
                            continuarAccion = ValidarControlesEspeciales();
                        }
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            //--------------------------------------------------------------------------------------------------
            //Setea variable de pestaña del TabStrip es clickeada
            //--------------------------------------------------------------------------------------------------
            function OnClientTabSelectingHandler(sender, args) {
                tabSeleccionada = args.get_tab().get_text();
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgPrecios_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }


            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de conceptos dispara edición
            //--------------------------------------------------------------------------------------------------
            function grdConceptoNomina_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //--------------------------------------------------------------------------------------------------
            //Actualiza el número de registros en combo de Colaboradors.
            //--------------------------------------------------------------------------------------------------
            function cmbColaboradorsLista_UpdateItemCountField(sender, args) {
                //set the footer text
                sender.get_dropDownElement().lastChild.innerHTML = "Un total de " + (sender.get_items().get_count() - 1) + " Colaboradors";
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el combo cmbCentrosDist cambia el item seleccionado
            //--------------------------------------------------------------------------------------------------
            function cmbCentrosDist_ClientSelectedIndexChanged(sender, args) {
                LimpiarControlesColaborador();

                //select tab datos generales
                var RadTabStripPrincipal = $find('<%= RadTabStripPrincipal.ClientID %>');
                RadTabStripPrincipal.get_allTabs()[0].select();

                //registro nuevo -> se limpia bandera de actualización
                var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                hiddenId.value = '';

                //poner el doco en txtIdColaborador
                var txtId_Colaborador = $find('<%= txtId_Colaborador.ClientID %>');
                txtId_Colaborador.focus();
            }

            //--------------------------------------------------------------------------------------------------
            // Cuando txtId_Colaborador txtNombreEmpleado pierde el foco, establece el titulo del Colaborador
            //--------------------------------------------------------------------------------------------------
            function txtId_Colaborador_OnBlur(sender, args) {
                EstablecerLabelTituloColaborador();
            }

            function txtNombreEmpleado_OnBlur(sender, args) {
                EstablecerLabelTituloColaborador();
            }

            function EstablecerLabelTituloColaborador() {
                var label = document.getElementById('<%= lblTituloColaborador.ClientID %>');
                var txtId_Colaborador = $find('<%= txtId_Colaborador.ClientID %>');
                var txtNombreEmpleado = $find('<%= txtNombreEmpleado.ClientID %>');

                var string_variable = txtNombreEmpleado.get_value()

                var intIndexOfMatch = string_variable.indexOf("'");
                while (intIndexOfMatch != -1) {
                    string_variable = string_variable.replace("'", "")
                    intIndexOfMatch = string_variable.indexOf("'");
                }


                txtNombreEmpleado.set_value(string_variable);
                label.innerHTML = txtId_Colaborador.get_textBoxValue() + ' - ' + txtNombreEmpleado.get_textBoxValue();
            }

           
 


            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del CmbTipoUsuario
            //--------------------------------------------------------------------------------------------------
            function CmbTipoUsuario_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= txtIdTipoUsuario.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        textBox.set_value(item.get_value());
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtIdTipoUsuario pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtIdTipoUsuario_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= CmbTipoUsuario.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'el tipo de Usuario con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }



            //--------------------------------------------------------------------------------------------------
            //Cuando el txtFechaInicio pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtFechaInicio_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbFam.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'La familia con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }



            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbFam
            //--------------------------------------------------------------------------------------------------
            function cmbFam_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;

                var item = eventArgs.get_item();
                var textBox = $find('<%= txtFechaInicio.ClientID %>');


            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtFechaInicio pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtFechaInicio_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbFam.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'La familia con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }





            function KeyPress(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                //debugger;
                //                if (c == 39)
                //                    eventArgs.set_cancel(true);
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

        </script>



    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTabStripPrincipal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrecios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbColaboradorsLista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="formulario" id="divPrincipal" runat="server">
        <asp:HiddenField ID="hiddenId" runat="server" />
        <asp:HiddenField ID="hiddenRefrescapagina" runat="server" />

          

        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <style>
            tr.tablet
            {
                border-style: Solid;
                border-color: Black;
                border-width: 1px;
                padding: 25px;
                background-color:Aqua;
            }
        </style>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt; margin-left: 10px;
            margin-right: 10px;" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribucion" Visible="false"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True" Visible="false">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table id="TblEncabezado11" runat="server" width="100%">
            <tr>
                <td style="width: 70%; text-align: center">
                    <asp:Label ID="lblTituloColaborador" runat="server" CssClass="tituloColaborador"
                        Font-Size="16px" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="margin-left: 10px; margin-right: 10px;">
            <tr>
               <%-- <td>
                    <asp:Label ID="lblempresa" runat="server" Text="Empresa"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbEmpresa" runat="server" EnableCheckAllItemsCheckBox="false"
                        Width="255" Label="" AutoPostBack="True" OnSelectedIndexChanged="cmbColaboradorsLista_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem Text="Key Quimica" Value="1" />
                            <telerik:RadComboBoxItem Text="CNK" Value="2" />
                        </Items>
                    </telerik:RadComboBox>
                </td>--%>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Colaborador "></asp:Label>
                </td>
                <td colspan="2">
                    <%--     <telerik:RadComboBox runat="server" ID="cmbColaboradorsLista" Width="400px" HighlightTemplatedItems="true"
                        MaxHeight="200px" EnableLoadOnDemand="true" AutoPostBack="true" DataTextField="Prd_Descripcion"
                        DataValueField="Id_Prd" OnClientItemsRequested="cmbColaboradorsLista_UpdateItemCountField"
                        OnDataBound="cmbColaboradorsLista_DataBound" OnSelectedIndexChanged=""
                        EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True" ItemsPerRequest="10"
                        ShowMoreResultsBox="True" LoadingMessage="Cargando..." OnClientDropDownOpening="Colaborador_Focus">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 50px; text-align: center">
                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                    </td>
                                    <td style="width: 200px; text-align: left">
                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                            NoMatches="No hay coincidencias" />
                    </telerik:RadComboBox>--%>
                    <telerik:RadComboBox ID="cmbColaboradorsLista" runat="server" Width="350px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                        DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." AutoPostBack="True" OnSelectedIndexChanged="cmbColaboradorsLista_SelectedIndexChanged"
                        MaxHeight="250px" EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True"
                        ItemsPerRequest="10" ShowMoreResultsBox="True" OnClientDropDownOpening="Colaborador_Focus">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 50px; text-align: left">
                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                    </td>
                                    <td style="width: 200px; text-align: left">
                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                            NoMatches="No hay coincidencias" />
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <br />
        <div runat="server" id="formularioColaboradors" style="margin-left: 10px; margin-right: 10px;">
            <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                SelectedIndex="0" TabIndex="-1">
                <Tabs>
                    <telerik:RadTab PageViewID="RadPageViewDGrales" Text="Datos <u>g</u>enerales " AccessKey="G"
                        Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewCompensaciones" Text="<u>C</u>ompensación fija mensual"
                        AccessKey="F">
                    </telerik:RadTab>
                  <%--  <telerik:RadTab PageViewID="RadPageViewMetas" Text="<u>D</u>efinición de Metas" AccessKey="A" display="False">
                    </telerik:RadTab>
                  --%>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                Width="800px">
                <!-- Aqui empieza el contenido de los tabs--->
                <telerik:RadPageView ID="RadPageViewDGrales" runat="server" BorderStyle="Solid" BorderWidth="1px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Nombre "></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNombreEmpleado" runat="server" Width="306px" MaxLength="100" readonly="true">
                                    <ClientEvents OnKeyPress="SinComilla" OnBlur="txtNombreEmpleado_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkColaboradorNuevo" runat="server" Text="Colaborador nuevo" Visible="false" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lbl_Val_txtNombreEmpleado" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <%-- <table style="font-family: vernada; font-size: 8;">--%>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <!--Tab 1  Tabla 1-->
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="No. de Nómina"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtNum_Nomina" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0" ReadOnly="true">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="chkActivo" runat="server" Text="Activo" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Código del Colaborador" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtId_Colaborador" runat="server" Width="50px" MaxLength="6"
                                                MinValue="1" Visible="False">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtId_Colaborador_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                            <telerik:RadNumericTextBox ID="txtId_Empleado" runat="server" Width="50px" MaxLength="6"
                                                MinValue="1" Visible="False">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtId_Colaborador" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtNum_Nomina" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Fecha Alta en la Empresa"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtFechaAlta" runat="server" Width="150px" MaxLength="50"
                                                ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSucursal" runat="server" Text="Sucursal"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtIdSucursal" runat="server" Width="150px" MaxLength="50" ReadOnly="true">
                                                </telerik:RadTextBox>
                                                <%--<ClientEvents OnBlur="txtIdSucursal_OnBlur" OnKeyPress="handleClickEvent" />--%>
                                           
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Empresa"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtEmpresa" runat="server" Width="150px" MaxLength="50" ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Tipo Sucursal"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtTipoSucursal" runat="server" Width="150px" MaxLength="50"
                                                ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_val_txtEmpresa" runat="server" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lbl_val_txtTipoSucursal" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label45" runat="server" Text="Puesto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtPuesto" runat="server" Width="150px" ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="Label48" runat="server" Text="Tipo de usuario" Visible = "false"></asp:Label>--%>
                                        </td>
                                        <td>


                                         <telerik:RadComboBox ID="cmbFam" runat="server" Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" OnClientSelectedIndexChanged="cmbFam_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px"
                                                Visible="false">
                                            </telerik:RadComboBox>
                                           
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Correo Electrónico" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtCorreo" runat="server" MaxLength="9" MinValue="1" Width="150px" Visible="false">
                                            </telerik:RadTextBox>
                                        </td>
                                       
                                        <td>
                                           

                                            <telerik:RadNumericTextBox ID="txtIdTipoUsuario" runat="server" MaxLength="9" MinValue="1"
                                                Width="50px" Visible="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtIdTipoUsuario_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                            <telerik:RadComboBox ID="CmbTipoUsuario" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="CmbTipoUsuario_ClientSelectedIndexChanged"
                                                Width="201px" LoadingMessage="Cargando..." MaxHeight="200px" Visible="false">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Centro de Distribución" Value="1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Sucursal" Value="2" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Franquicia" Value="3" />
                                                </Items>
                                            </telerik:RadComboBox>




                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtCorreo" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                   
                                    <tr class="tablet" >
                                         
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Fecha alta en el puesto"></asp:Label>
                                        </td>
                                        <td >
                                            <telerik:RadDatePicker ID="txtFechaInicio" runat="server" Width="100px">
                                                <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                    ViewSelectorText="x">
                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                        TodayButtonCaption="Hoy" />
                                                </Calendar>
                                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" OnBlur="txtFechaInicio_OnBlur" />
                                                </DateInput>
                                                <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                            </telerik:RadDatePicker>
                                        </td>
                                        
                                         <td>
                                            <asp:Label ID="Label49" runat="server" Text="UEN"></asp:Label>
                                        </td>
                                        <td>
                                         <telerik:RadComboBox RenderMode="Lightweight" ID="cmbUEN" runat="server" CheckBoxes="true"
                                                EnableCheckAllItemsCheckBox="false" Width="255" Label="">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="INSTITUCIONAL BASICA" Value="1" />
                                                    <telerik:RadComboBoxItem Text="INSTITUCIONAL ESPECIALIZADA" Value="2" />
                                                    <telerik:RadComboBoxItem Text="INDUSTRIAL" Value="3" />
                                                    <telerik:RadComboBoxItem Text="ALIMENTARIA" Value="4" />
                                                    <telerik:RadComboBoxItem Text="COMERCIAL" Value="5" />
                                                    <telerik:RadComboBoxItem Text="VENTAS DIRECTAS" Value="6" />
                                                    <telerik:RadComboBoxItem Text="INTER CD" Value="7" />
                                                </Items>
                                            </telerik:RadComboBox>

                                        </td>
                                        
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                    
                              
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewCompensaciones" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <!-- Tabla principal--->
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Sueldo variable" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtSueldoVariable" runat="server" Width="70px" MaxLength="9"
                                                MinValue="0"  Visible="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label44" runat="server" Text="% Contribuci&oacute;n"  Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtContribucion" runat="server" Width="70px" MaxLength="9"
                                                MinValue="0" Visible="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                <telerik:RadGrid ID="grdConceptoNomina" runat="server" GridLines="None" DataMember="listaConceptosNomina"
                                                    PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                    OnNeedDataSource="grdConceptoNomina_NeedDataSource" OnUpdateCommand="grdConceptoNomina_UpdateCommand"
                                                    OnPreRender="grdConceptoNomina_PreRender" OnItemDataBound="grdConceptoNomina_ItemDataBound"
                                                    OnPageIndexChanged="grdConceptoNomina_PageIndexChanged">
                                                    <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_empleado, Id_Colaborador,Id_Compensacion,Id_Compensacion_Monto"
                                                        EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                        Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                        <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Colaborador" UniqueName="Id_Colaborador" DataField="Id_Colaborador"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Empleado" UniqueName="Id_empleado" DataField="Id_empleado"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Id_Compensacion" UniqueName="Id_Compensacion"
                                                                DataField="Id_Compensacion" Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Compensacion_Monto" DataField="Id_Compensacion_Monto"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                           <%-- <telerik:GridTemplateColumn HeaderText="Descripción Concepto" DataField="Compensacion_Descripcion"
                                                                UniqueName="Compensacion_Descripcion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompensacion_Descripcion" runat="server" Text='<%# Eval("Compensacion_Descripcion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>--%>
                                                            

                                                            <%--2--%>
                                                             <telerik:GridBoundColumn HeaderText="Id_Compensacion1" UniqueName="Id_Compensacion1"
                                                                DataField="Id_Compensacion1" Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Sueldo Nominal" DataField="Monto1" UniqueName="Monto1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMonto1" runat="server" Text='<%# Eval("Monto1") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtMonto1" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Monto1") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                             <telerik:GridBoundColumn HeaderText="Id_Compensacion2" UniqueName="Id_Compensacion2"
                                                                DataField="Id_Compensacion2" Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Cuenta de Gastos" DataField="Monto2" UniqueName="Monto2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMonto2" runat="server" Text='<%# Eval("Monto2") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtMonto2" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Monto2") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                             <telerik:GridBoundColumn HeaderText="Id_Compensacion3" UniqueName="Id_Compensacion3"
                                                                DataField="Id_Compensacion3" Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Gasolina" DataField="Monto3" UniqueName="Monto3">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMonto3" runat="server" Text='<%# Eval("Monto3") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtMonto3" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Monto3") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                              <telerik:GridBoundColumn HeaderText="Id_Compensacion4" UniqueName="Id_Compensacion4"
                                                                DataField="Id_Compensacion4" Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Despensa" DataField="Monto4" UniqueName="Monto4">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMonto4" runat="server" Text='<%# Eval("Monto4") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtMonto4" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Monto4") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>




                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                EditText="Editar" HeaderText="Editar">
                                                            </telerik:GridEditCommandColumn>
                                                        </Columns>
                                                        <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Colaborador" CaptionFormatString="Editar Montos de Conceptos del Empleado {0}"
                                                            InsertCaption="Agregar nuevo Monto de Empleado">
                                                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                BorderColor="#000000" BorderWidth="1" />
                                                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                            <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                            </EditColumn>
                                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings>
                                                        <ClientEvents OnRowDblClick="grdConceptoNomina_ClientRowDblClick" />
                                                        <Selecting AllowRowSelect="true" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewMetas" runat="server" BorderStyle="Solid" BorderWidth="1px" Visible = "false">
                    <table style="font-family: vernada; font-size: 8;">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                <telerik:RadGrid ID="rgPrecios" runat="server" GridLines="None" DataMember="listaPrecios"
                                                    PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                    OnNeedDataSource="grdPrecios_NeedDataSource" OnUpdateCommand="grdPrecios_UpdateCommand"
                                                    OnPreRender="grdPrecios_PreRender" OnItemDataBound="grdPrecios_ItemDataBound"
                                                    OnPageIndexChanged="grdPrecios_PageIndexChanged">
                                                    <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Colaborador,Id_ColaboradorObjetivo"
                                                        EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                        Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                        <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Colaborador" UniqueName="Id_Colaborador" DataField="Id_Colaborador"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_ColaboradorObjetivo" DataField="Id_ColaboradorObjetivo"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Año" DataField="Anio" UniqueName="Anio">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("Anio") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox onpaste="return false" ID="txtAnio" runat="server" Text='<%# Eval("Anio") %>'
                                                                        MaxLength="20">
                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Mes" DataField="Mes" UniqueName="Mes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoPrecio" runat="server" Text='<%# Eval("Mes") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblTipoPrecioEdit" runat="server" Text='<%# Eval("Mes") %>' Font-Bold="true" />
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Objetivo" DataField="Objetivo" UniqueName="Objetivo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblObjetivo" runat="server" Text='<%# Eval("Objetivo") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtObjetivo" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Objetivo") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                EditText="Editar" HeaderText="Editar">
                                                            </telerik:GridEditCommandColumn>
                                                        </Columns>
                                                        <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Colaborador" CaptionFormatString="Editar datos de Objetivos del Colaborador con clave {0}"
                                                            InsertCaption="Agregar nuevo Objetivo de Colaborador">
                                                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                BorderColor="#000000" BorderWidth="1" />
                                                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                            <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                            </EditColumn>
                                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings>
                                                        <ClientEvents OnRowDblClick="rgPrecios_ClientRowDblClick" />
                                                        <Selecting AllowRowSelect="true" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                       <asp:HiddenField ID="HiddenRebind" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
                                             <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />


                </telerik:RadPageView>

              

            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>


