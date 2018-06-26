<%@ Page Title="Reporte comisiones" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="RepComisiones.aspx.cs" Inherits="SIANWEB.RepComisiones"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<style>
        .RadInput input[readonly]
        {
            background-color: #F7F7F7 !important;
        }
        
        #PopUpBackground
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
            z-index: 100000;
        }
        
        #PopUpProgress
        {
            position: fixed;
            font-size: 120%;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 100001;
            background-color: #FFFFFF;
            border: 1px solid Gray;
            background-image: url('images/loading1.gif');
            background-repeat: no-repeat;
            background-position: center;
            border-radius: .6em;
        }
      
     
}

    </style>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function TxtId_Rik_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRik.ClientID %>'));
            }
            function TxtTipo_Representante_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= CmbTipo_Representante.ClientID %>'));
            }

            function CmbId_Rik_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Rik.ClientID %>'));
            }
            function CmbTipo_Representante_SelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtTipo_Representante.ClientID %>'));
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= CmbTipo_Representante.ClientID %>'));
            }
            function cmbEmpresa_Representante_SelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtTipo_Representante.ClientID %>'));
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= CmbTipo_Representante.ClientID %>'));
            }



            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        alert("entra a validaciones especiales todo eliminar esta rutina ");
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

           

            function refreshGrid() {

            }

            

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

           
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">   <%--Skin="Default"--%>

    <div id="PopUpBackground"></div>
        <div id="PopUpProgress">
            <h6><p style="text-align:center;"><b>Favor de Esperar...</b></p></h6>
        </div>

    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" AsyncTimeout="600000" AsyncPostBackTimeout="600000" eventname="RadAjaxManager1_AjaxRequest"
        >

        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRiks" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="rgRiksPrevio" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                      <telerik:AjaxUpdatedControl ControlID="rgRiks" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="rgRiksPrevio" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbanio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEmpresa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensajeError" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensajeError" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbmes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensajeError" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="lblMensajeError" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbId_Cd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="lblMensajeError" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
 
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" >
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
                            <td>
                                <asp:Label ID="Label1" Text="Tipo de Reporte" runat="server">
                                
                                </asp:Label>&nbsp;
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbTipoReporte" runat="server" Width="150px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Comisiones" Value="" />
                                        <telerik:RadComboBoxItem runat="server" Text="Venta Incremental" Value="_venta_incremental" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                             <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" Text="Año" runat="server">
                                
                                </asp:Label>&nbsp;
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbanio" runat="server" Width="150px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="2016" Value="2016" />
                                        <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                        <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                        <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                        <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                        <telerik:RadComboBoxItem runat="server" Text="2021" Value="2021" />
                                        <telerik:RadComboBoxItem runat="server" Text="2022" Value="2022" />
                                        <telerik:RadComboBoxItem runat="server" Text="2014" Value="2023" />
                                        <telerik:RadComboBoxItem runat="server" Text="2015" Value="2024" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Mes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbmes" runat="server" Width="150px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" />
                                        <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" />
                                        <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" />
                                        <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" />
                                        <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" />
                                        <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" />
                                        <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" />
                                        <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" />
                                        <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" />
                                        <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblempresa" runat="server" Text="Empresa"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEmpresa" runat="server" EnableCheckAllItemsCheckBox="false"
                                    Width="255" Label="" AutoPostBack="True" OnSelectedIndexChanged="cmbEmpresa_Representante_SelectedIndexChanged"
                                    OnClientSelectedIndexChanged="cmbEmpresa_Representante_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Key Quimica" Value="1" />
                                        <telerik:RadComboBoxItem Text="CNK" Value="2" />
                                        <telerik:RadComboBoxItem Text="FRANQUICIAS" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblId_Cd" runat="server" Text="Sucursal"></asp:Label>
                            </td>
                            <td colspan="9">
                                <telerik:RadComboBox ID="CmbId_Cd" runat="server" AutoPostBack="true" CausesValidation="False"
                                    ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                    OnSelectedIndexChanged="CmbId_Cd_SelectedIndexChanged" EnableLoadOnDemand="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                    MarkFirstMatch="true" MaxHeight="200px" Width="300px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                        Width="50px" />
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
                            </td>
                        </tr>
                        <tr runat="server" id="tr1">
                            <td>
                                <asp:Label ID="Label2" Text="Tipo de Representante" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtTipo_Representante" runat="server" MaxLength="9"
                                    MinValue="0" Width="100px">
                                    <ClientEvents OnBlur="TxtTipo_Representante_OnBlur" OnKeyPress="handleClickEvent" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="CmbTipo_Representante" runat="server" AutoPostBack="true"
                                    CausesValidation="False" ChangeTextOnKeyBoardNavigation="true" Width="250px"
                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." OnSelectedIndexChanged="CmbTipo_Representante_SelectedIndexChanged"
                                    OnClientSelectedIndexChanged="CmbTipo_Representante_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="0" />
                                    </Items>
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="RSC" Value="1" />
                                    </Items>
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Asesor" Value="2" />
                                    </Items>
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Rik" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" id="trRik">
                            <td>
                                <asp:Label ID="LblId_Rik" Text="Representante" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtId_Rik" runat="server" MaxLength="10" MinValue="0"
                                    Width="100px">
                                    <ClientEvents OnBlur="TxtId_Rik_OnBlur" OnKeyPress="handleClickEvent" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbRik" runat="server" Width="250px" OnClientBlur="Combo_ClientBlur"
                                    OnClientSelectedIndexChanged="CmbId_Rik_ClientSelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblReporte" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="x">Previo</asp:ListItem>
                        <asp:ListItem Value="b">Reporte Concentrado</asp:ListItem>
                        <asp:ListItem Value="a">Generar PDF</asp:ListItem>
                    </asp:RadioButtonList>
 
                
                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgRiks" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="rgRiks_NeedDataSource" OnItemCommand="rgRiks_ItemCommand" OnItemDataBound="rgRiks_ItemDataBound"
                        PageSize="7" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnPageIndexChanged="rgRiks_PageIndexChanged">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Imprimir" HeaderText="Procesar"
                                    UniqueName="Enviacorreo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEnviarMail" runat="server" Checked="true" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkProcesarCabezera" AutoPostBack="true" Text="Procesar"
                                            HeaderText="Procesar" runat="server" OnCheckedChanged="chkProcesarCabezera_CheckedChanged" Checked="true" />
                                    </HeaderTemplate>
                                </telerik:GridTemplateColumn>
                               <%-- <telerik:GridBoundColumn DataField="Imprimir" HeaderText="Imprimir" UniqueName="Imprimir"
                                    Visible="false">
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <%-- nom_emp NombreEmpresa--%>
                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CDI" UniqueName="Id_Cd"
                                    Visible="true">
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="CdiNombre" HeaderText="Nombre CDI" UniqueName="CdiNombre">
                                    <HeaderStyle Width="100" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="id_tiporepresentante" HeaderText="Tipo Rep."
                                    UniqueName="id_tiporepresentante" Visible="true">
                                     <HeaderStyle Width="80" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Rik_Descripcion" HeaderText="Tipo Representante"
                                    UniqueName="Rik_Descripcion">
                                    <HeaderStyle Width="120" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id" HeaderText="Núm. Rik" UniqueName="Id">
                                    <HeaderStyle Width="80" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nombre_Empleado" HeaderText="Nombre Rik" UniqueName="Nombre_Empleado">
                                    <HeaderStyle Width="180" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus" Visible="false">
                                    <HeaderStyle Width="60" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                    UniqueName="PDF" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <%-- jfcv 5 oct 2015 Agregue la columna de soporte y validación si no tiene docs soporte no muestra el icono y el de comprobantes , si no tiene de soporte muestra el icono de comproante--%>
                                <telerik:GridTemplateColumn HeaderText="Soporte" DataField="PagElec_Soporte" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                    UniqueName="Soporte" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSoporte" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="edit"
                                            ToolTip="Archivo de Soporte" CommandName="Soporte" Enabled="true" Visible='<%#Eval("estatus") != null ? true : false  %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Comprobante" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" UniqueName="Comprobantes">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Reporte Generado en PDF" CommandName="Comprobantes" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <%--jfcv 11 dic 2015 agregar botón para editar 17 dic cambiar icono a botón editar por lapiz --%>
                                <telerik:GridTemplateColumn HeaderText="Estatus" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/lapiz.gif" ToolTip="Editar"
                                            CommandName="Modificar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                    <HeaderStyle Width="35px" />
                                </telerik:GridTemplateColumn>
                                <%-- <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea eliminar el archivo pdf?" Text="Eliminar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                ConfirmDialogWidth="350px" HeaderText="Cancelar">
                                <HeaderStyle Width="25px" />
                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                            </telerik:GridButtonColumn>--%>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
                <td>
                   
                     &nbsp;
                </td>
            </tr>
             <tr>
                <td>
                    <telerik:RadGrid ID="rgRiksPrevio" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="rgRiksPrevio_NeedDataSource" OnItemCommand="rgRiksPrevio_ItemCommand"  OnItemDataBound="rgRiksPrevio_ItemDataBound"
                        PageSize="7" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnPageIndexChanged="rgRiksPrevio_PageIndexChanged">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                    Visible="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id" HeaderText="Núm. Rik" UniqueName="Id">
                                    <HeaderStyle Width="80" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CdiNombre" HeaderText="CDI" UniqueName="CdiNombre">
                                    <HeaderStyle Width="180" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nombre_Empleado" HeaderText="Nombre Rik" UniqueName="Nombre_Empleado">
                                    <HeaderStyle Width="180" />
                                </telerik:GridBoundColumn>
                              
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="VtaCob" HeaderText="Total Venta" DataFormatString="{0:C}" UniqueName="VtaCob" 
                                 FooterText="Total: " ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn Aggregate="Sum" DataField="UP" HeaderText="Utilidad Prima" DataFormatString="{0:C}" UniqueName="UP" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn Aggregate="Sum" DataField="UB" HeaderText="Utilidad Bruta" DataFormatString="{0:C}" UniqueName="UB" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn  DataField="CP" HeaderText="Porc. Participacion" DataFormatString="{0:C}" UniqueName="CP" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="MVI" HeaderText="Mult. Vta. Incremental" DataFormatString="{0:C}" UniqueName="MVI"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="ComBaseAjustada" HeaderText="Comision base" DataFormatString="{0:C}" UniqueName="ComBaseAjustada" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="GtoAdmin" HeaderText="Gasto Adm" DataFormatString="{0:C}" UniqueName="GtoAdmin" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="ComisionNeta" HeaderText="Comision Neta" DataFormatString="{0:C}" UniqueName="ComisionNeta" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>


<%--                                   <telerik:GridBoundColumn Aggregate="Sum" DataField="Total_Venta" HeaderText="Total Venta" DataFormatString="{0:C}" UniqueName="Total_Venta" 
                                 FooterText="Total: " ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn Aggregate="Sum" DataField="Utilidad_Prima" HeaderText="Utilidad Prima" DataFormatString="{0:C}" UniqueName="Utilidad_Prima" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn Aggregate="Sum" DataField="Utilidad_Bruta" HeaderText="Utilidad Bruta" DataFormatString="{0:C}" UniqueName="Utilidad_Bruta" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn  DataField="Porc_Participacion" HeaderText="Porc. Participacion" DataFormatString="{0:C}" UniqueName="Porc_Participacion" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="Mult_Vta_Incremental" HeaderText="Mult. Vta. Incremental" DataFormatString="{0:C}" UniqueName="Mult_Vta_Incremental"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="Comision_base" HeaderText="Comision base" DataFormatString="{0:C}" UniqueName="Comision_base" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="Gasto_Adm" HeaderText="Gasto Adm" DataFormatString="{0:C}" UniqueName="Gasto_Adm" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn Aggregate="Sum" DataField="Comision_Neta" HeaderText="Comision Neta" DataFormatString="{0:C}" UniqueName="Comision_Neta" 
                                  ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
--%>


                                <%-- <telerik:GridTemplateColumn HeaderText="Soporte" DataField="PagElec_Soporte" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                    UniqueName="Soporte" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSoporte" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="edit"
                                            ToolTip="Archivo de Soporte" CommandName="Soporte" Enabled="true" Visible='<%#Eval("estatus") != null ? true : false  %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>--%>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
                <td>
                   
                     &nbsp;
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
    </div>
</asp:Content>
