<%@ Page Title="Autorización Gasto" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProAutorizacion_PagoElectronico.aspx.cs" Inherits="SIANWEB.ProAutorizacion_PagoElectronico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
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

    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbSubTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmdCtaGastos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divCtaGastos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbAcreedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="inlinePanel"></telerik:AjaxUpdatedControl>
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
                         <telerik:AjaxSetting AjaxControlID="imgBoton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="inlinePanel"></telerik:AjaxUpdatedControl>
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server">
        <div id="PopUpBackground"></div>
        <div id="PopUpProgress">
            <h6><p style="text-align:center;"><b>Favor de Esperar...</b></p></h6>
        </div>
    </telerik:radajaxloadingpanel>
   <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelImport" runat="server" Transparency="30" MinDisplayTime="300">
            <div class="loading">
                <asp:Image ID="Image1" runat="server" ImageUrl="images/loading1.gif" AlternateText="loading"></asp:Image>
            </div>
        </telerik:RadAjaxLoadingPanel>--%>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_Gastos" runat="server" Opacity="100" Behaviors="Move, Close, Maximize"
                VisibleStatusbar="False" Width="900px" Height="600px" Animation="Fade" KeepInScreenBounds="True"
                Overlay="True" Title="Captura Gastos" Modal="True" OnClientClose="refreshGrid"
                ReloadOnShow="true">
            </telerik:RadWindow>
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_LstComprobantes" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="900px" Height="600px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Listado de Comprobantes"
                Modal="True" OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div id="divPrincipal" runat="server">
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
        <%--JFCV 24 oct que pueda filtrar punto 13--%>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="LblId_Cd" runat="server" Text="CDI"></asp:Label>
                </td>
                
                <td colspan="9">
                    <telerik:RadComboBox ID="CmbId_Cd" runat="server" AutoPostBack="true" CausesValidation="False"
                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
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
                        <asp:Label ID="lblid" runat="server" Text="Núm. Solicitud"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtidPagoElectronico" runat="server" Width="70px">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="CmbTipo" runat="server" OnSelectedIndexChanged="CmbTipo_SelectedIndexChanged"
                            AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />
                            </Items>
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Solicitud de Cheque" Value="1" />
                            </Items>
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Reposicion de Caja" Value="2" />
                            </Items>
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Gastos de Viaje" Value="3" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="LblSubTipo" runat="server" Text="SubTipo"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="CmbSubTipo" runat="server" AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />
                                <telerik:RadComboBoxItem runat="server" Text="Flete" Value="1" />
                                <telerik:RadComboBoxItem runat="server" Text="No Inventariable" Value="2" />
                                <telerik:RadComboBoxItem runat="server" Text="Compra Local" Value="3" />
                                <telerik:RadComboBoxItem runat="server" Text="Pagos de Servicios" Value="4" />
                                <telerik:RadComboBoxItem runat="server" Text="Otros" Value="5" />
                                <telerik:RadComboBoxItem runat="server" Text="Honorarios" Value="6" />
                                <telerik:RadComboBoxItem runat="server" Text="Arrendamientos" Value="7" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <%--<asp:Label ID="LblAcreedor" runat="server" Text="Acreedor"></asp:Label>--%>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="CmbAcreedor" runat="server" Visible="false">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <%-- <asp:Label ID="LblCuenta" runat="server" Text="Cuenta"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Panel ID="divCtaGastos" runat="server" Visible="false">
                            <telerik:RadComboBox ID="cmdCtaGastos" runat="server" Width="425px" OnClientFocus="cmdClient_Focus"
                                Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True" ItemsPerRequest="12"
                                ShowMoreResultsBox="True" MaxHeight="300px" EmptyMessage="-- Seleccionar --"
                                AutoPostBack="True">
                                <HeaderTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td valign="middle" style="width: 263px; text-align: left">
                                                <b>Descripcion</b>
                                            </td>
                                            <td valign="middle" style="width: 80px; text-align: left">
                                                <b>Sub Cuenta</b>
                                            </td>
                                            <td valign="middle" style="width: 80px; text-align: left">
                                                <b>SubSub Cuenta</b>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td valign="middle" style="width: 263px; text-align: left">
                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_Descripcion") %>' />
                                            </td>
                                            <td valign="middle" style="width: 80px; text-align: left">
                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubCuenta") %>' />
                                            </td>
                                            <td valign="middle" style="width: 80px; text-align: left">
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubSubCuenta") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                    NoMatches="No hay coincidencias" />
                            </telerik:RadComboBox>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
                    </td> <td>
                                    <asp:ImageButton ID="imgBoton" runat="server" ImageUrl="~/images/actualizar.jpg" OnClick="imgBoton_Click"
                                        ToolTip="Actualizar" MaxHeight="50px"   Width="50px"   />
                                </td>
            </tr>
        </table>
        <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="RadPanelBar1" Height="430px"
            Width="100%" ExpandMode="FullExpandedItem" >
            <Items>
                <telerik:RadPanelItem runat="server" Text="Autorización de Gastos" Id="paneldegastos">
                    <Items>
                        <telerik:RadPanelItem runat="server" Value="PanelItem1">
                            <ItemTemplate>
                                <telerik:RadGrid ID="rgPago" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rgPago_NeedDataSource" OnItemCommand="rgPago_ItemCommand" OnPageIndexChanged="rgPago_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."  >
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CD" UniqueName="Id_Cd" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElecTipo_Descrpcion" HeaderText="Tipo" UniqueName="PagElecTipo_Descrpcion">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_PagElecTipo" HeaderText="Id_PagElecTipo" UniqueName="Id_PagElecTipo"
                                                Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--jfcv agregar sub tipo Id_PagElecSubTipo--%>
                                            <telerik:GridBoundColumn DataField="Id_PagElecSubTipo" HeaderText="Id_PagElecSubTipo"
                                                UniqueName="Id_PagElecSubTipo" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_SubTipoDescripcion" HeaderText="SubTipo"
                                                UniqueName="PagElec_SubTipoDescripcion" Display="true">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElecConcepto_Descripcion" HeaderText="Concepto"
                                                UniqueName="PagElecConcepto_Descripcion" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Acr" HeaderText="Id_Acr" UniqueName="Id_Acr"
                                                Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--se comenta la columna de acreedor y dirá cta pago y será la clave del proveedor --%>
                                            <%--//jfcv 24oct2016  inicio punto 13--%>
                                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado">
                                                <HeaderStyle Width="60" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Nombre" HeaderText="Proveedor/Acreedor" UniqueName="Acr_Nombre">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_Solicitante" HeaderText="Solicitante"
                                                UniqueName="PagElec_Solicitante" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--//jfcv 24oct2016  fin punto 13--%>
                                            <telerik:GridBoundColumn DataField="PagElec_FechaRequiere" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}"
                                                UniqueName="PagElec_FechaRequiere">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_Cuenta" HeaderText="Número" UniqueName="PagElec_Cuenta"
                                                Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_Cc" HeaderText="C.C." UniqueName="PagElec_Cc"
                                                Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_SubCuenta" HeaderText="Sub Cuenta" UniqueName="PagElec_SubCuenta"
                                                Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_SubSubCuenta" HeaderText="Sub Sub-Cta"
                                                UniqueName="PagElec_SubSubCuenta" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%-- ocultar la columna de cuenta pago y poner en su lugar la de Monto que tendrá el importe de la solicitud --%>
                                            <telerik:GridBoundColumn DataField="PagElec_CuentaPago" HeaderText="Cta Pago." UniqueName="PagElec_CuentaPago"
                                                Display="false">
                                                <HeaderStyle Width="10" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_Importe" HeaderText="Monto" DataFormatString="{0:C}"
                                                UniqueName="PagElec_Importe" ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--JFCV 30 oct para poder enviar el archivo de soporte a macola necesito agregar estas dos columnas ocultas --%>
                                            <telerik:GridBoundColumn DataField="PagElec_Importe" HeaderText="montocalcular" UniqueName="PagElec_ImporteSumar"
                                                Display="false">
                                                <HeaderStyle Width="0" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PagElec_Soporte" HeaderText="soporte" UniqueName="PagElec_Soporte"
                                                Display="false">
                                                <HeaderStyle Width="10" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                UniqueName="XML" Display="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                UniqueName="PDF" Display="false">
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
                                                UniqueName="Soporte" Visible="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgSoporte" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="edit"
                                                        ToolTip="Archivo de Soporte" CommandName="Soporte" Enabled="true" Visible='<%#Eval("PagElec_Soporte") != null ? true : false  %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px" UniqueName="Comprobantes">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px"
                                UniqueName="Comprobantes">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" Visible ='<%#Eval("PagElec_Soporte") != null ? false : true  %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>--%>
                                            <telerik:GridTemplateColumn HeaderText="Número Referencia" UniqueName="Acr_NumeroGenerado">
                                                <ItemTemplate>
                                                    <%-- JFCV cambiar en lugar numerico que sea texto<telerik:RadNumericTextBox ID="TxtNumeroAcreedor" runat="server"><NumberFormat DecimalDigits="0" AllowRounding="false" /></telerik:RadNumericTextBox>--%>
                                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" runat="server" MaxLength="30">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea autorizar el gasto?</br></br>" Text="Autorizar" ConfirmDialogHeight="150px"
                                                ConfirmDialogWidth="350px" UniqueName="Autorizar" Visible="True" ButtonType="ImageButton"
                                                ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridButtonColumn>
                                            <%-- JFCV 18 dic 2015 agregar botón de rechazar y Motivo de Rechazo --%>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea rechazar la solicitud?" Text="Cancelar" UniqueName="DeleteColumn"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" HeaderText="Rechazar">
                                                <HeaderStyle Width="25px" />
                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn HeaderText="Motivo Rechazo" UniqueName="Acr_MotivoRechazo">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="TxtMotivoRechazo" runat="server" MaxLength="100">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Autorización de Gastos de Viaje">
                    <Items>
                        <telerik:RadPanelItem runat="server" Value="PanelGastoViaje">
                            <ItemTemplate>
                                <telerik:RadGrid ID="rgPagoGastoViaje" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" OnNeedDataSource="rgPagoGastoViaje_NeedDataSource" OnItemCommand="rgPagoGastoViaje_ItemCommand"
                                    OnPageIndexChanged="rgPagoGastoViaje_PageIndexChanged" PageSize="15" AllowPaging="True"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros."  >
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CD" UniqueName="Id_Cd" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--jfcv agregar sub tipo Id_PagElecSubTipo--%>
                                            <telerik:GridBoundColumn DataField="Id_PagElecSubTipo" HeaderText="Id_PagElecSubTipo"
                                                UniqueName="Id_PagElecSubTipo" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_GV" HeaderText="Id" UniqueName="Id_GV" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_GVEst" HeaderText="Tipo" UniqueName="Id_GVEst"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GVEst_Descripcion" HeaderText="Estatus" UniqueName="GVEst_Descripcion">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Motivo" HeaderText="Motivo" UniqueName="GV_Motivo">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Solicitante" HeaderText="Solicitante" UniqueName="GV_Solicitante">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--JFCV 29ene2016 agregar nombre del acreedor , observaciones y columna de fecha de elaboracion cuando sea pago acreedores --%>
                                            <telerik:GridBoundColumn DataField="GV_Acr_Nombre" HeaderText="Acreedor" UniqueName="GV_Acr_Nombre">
                                                <HeaderStyle Width="130" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_PagElec_Observaciones" HeaderText="Observaciones"
                                                UniqueName="GV_PagElec_Observaciones">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_FechaElaboracion" HeaderText="Fecha Elaboración"
                                                DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaElaboracion">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_FechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yy}"
                                                UniqueName="GV_FechaSalida">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_FechaRegreso" HeaderText="Fecha Regreso" DataFormatString="{0:dd/MM/yy}"
                                                UniqueName="GV_FechaRegreso">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_PagElec_Destino" HeaderText="Destino" UniqueName="GV_PagElec_Destino">
                                                <HeaderStyle Width="100" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Importe" HeaderText="Importe Solicitado" UniqueName="GV_Importe">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Saldo_Comprobar" HeaderText="Saldo Comprobar"
                                                UniqueName="GV_Saldo_Comprobar">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado"
                                                Display="false">
                                                <HeaderStyle Width="60" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Número Referencia" UniqueName="Acr_NumeroGenerado">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" runat="server" MaxLength="30">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%-- jfcv 5 oct 2015 Agregue la columna de soporte y validación si no tiene docs soporte no muestra el icono y el de comprobantes , si no tiene de soporte muestra el icono de comproante--%>
                                            <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px" UniqueName="Comprobantes">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea autorizar el gasto?</br></br>" Text="Autorizar" ConfirmDialogHeight="150px"
                                                ConfirmDialogWidth="350px" UniqueName="Autorizar" Display="True" ButtonType="ImageButton"
                                                ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridButtonColumn>
                                            <%-- JFCV 12 ene 2015 agregar botón de rechazar y Motivo de Rechazo --%>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea rechazar la solicitud?" Text="Cancelar" UniqueName="DeleteColumn"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" HeaderText="Rechazar">
                                                <HeaderStyle Width="25px" />
                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn HeaderText="Motivo Rechazo" UniqueName="Acr_MotivoRechazo">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="TxtMotivoRechazo" runat="server" MaxLength="100">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%-- JFCV 02 feb 2016 agregar campos de cuenta subcuenta etc ocultos para enviar al movimiento de CXP --%>
                                            <telerik:GridBoundColumn DataField="GV_Cuenta" HeaderText="GV_Cuenta" UniqueName="GV_Cuenta"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Cc" HeaderText="GV_Cc" UniqueName="GV_Cc"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Numero" HeaderText="GV_Numero" UniqueName="GV_Numero"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_SubCuenta" HeaderText="GV_SubCuenta" UniqueName="GV_SubCuenta"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_SubSubCuenta" HeaderText="GV_SubSubCuenta"
                                                UniqueName="GV_SubSubCuenta" Display="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Autorización de Comp. de Acreedores y Proveedores">
                    <Items>
                        <telerik:RadPanelItem runat="server" Value="PanelCompAcreedor">
                            <ItemTemplate>
                                <telerik:RadGrid ID="rgPagoCompAcreedor" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" OnNeedDataSource="rgPagoCompAcreedor_NeedDataSource" OnItemCommand="rgPagoCompAcreedor_ItemCommand"
                                    OnPageIndexChanged="rgPagoCompAcreedor_PageIndexChanged" PageSize="15" AllowPaging="True"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros."  >
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CD" UniqueName="Id_Cd" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--jfcv agregar sub tipo Id_PagElecSubTipo--%>
                                            <telerik:GridBoundColumn DataField="Id_PagElecSubTipo" HeaderText="Id_PagElecSubTipo"
                                                UniqueName="Id_PagElecSubTipo" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_GV" HeaderText="Id" UniqueName="Id_GV" Display="false">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_GVEst" HeaderText="Tipo" UniqueName="Id_GVEst"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GVEst_Descripcion" HeaderText="Estatus" UniqueName="GVEst_Descripcion">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Motivo" HeaderText="Motivo" UniqueName="GV_Motivo">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Solicitante" HeaderText="Solicitante" UniqueName="GV_Solicitante">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <%--JFCV 29ene2016 agregar nombre del acreedor , observaciones y columna de fecha de elaboracion cuando sea pago acreedores --%>
                                            <telerik:GridBoundColumn DataField="GV_Acr_Nombre" HeaderText="Acreedor" UniqueName="GV_Acr_Nombre">
                                                <HeaderStyle Width="130" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_PagElec_Observaciones" HeaderText="Observaciones"
                                                UniqueName="GV_PagElec_Observaciones">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_FechaElaboracion" HeaderText="Fecha Elaboración"
                                                DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaElaboracion">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_FechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yy}"
                                                UniqueName="GV_FechaSalida">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_FechaRegreso" HeaderText="Fecha Regreso" DataFormatString="{0:dd/MM/yy}"
                                                UniqueName="GV_FechaRegreso">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_PagElec_Destino" HeaderText="Destino" UniqueName="GV_PagElec_Destino">
                                                <HeaderStyle Width="100" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Importe" HeaderText="Importe Solicitado" UniqueName="GV_Importe">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Saldo_Comprobar" HeaderText="Saldo Comprobar"
                                                UniqueName="GV_Saldo_Comprobar">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado"
                                                Display="false">
                                                <HeaderStyle Width="60" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Número Referencia" UniqueName="Acr_NumeroGenerado">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" runat="server" MaxLength="30">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%-- jfcv 5 oct 2015 Agregue la columna de soporte y validación si no tiene docs soporte no muestra el icono y el de comprobantes , si no tiene de soporte muestra el icono de comproante--%>
                                            <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px" UniqueName="Comprobantes">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea autorizar el gasto?</br></br>" Text="Autorizar" ConfirmDialogHeight="150px"
                                                ConfirmDialogWidth="350px" UniqueName="Autorizar" Display="True" ButtonType="ImageButton"
                                                ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridButtonColumn>
                                            <%-- JFCV 12 ene 2015 agregar botón de rechazar y Motivo de Rechazo --%>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea rechazar la solicitud?" Text="Cancelar" UniqueName="DeleteColumn"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" HeaderText="Rechazar">
                                                <HeaderStyle Width="25px" />
                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn HeaderText="Motivo Rechazo" UniqueName="Acr_MotivoRechazo">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="TxtMotivoRechazo" runat="server" MaxLength="100">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%-- JFCV 02 feb 2016 agregar campos de cuenta subcuenta etc ocultos para enviar al movimiento de CXP --%>
                                            <telerik:GridBoundColumn DataField="GV_Cuenta" HeaderText="GV_Cuenta" UniqueName="GV_Cuenta"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Cc" HeaderText="GV_Cc" UniqueName="GV_Cc"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_Numero" HeaderText="GV_Numero" UniqueName="GV_Numero"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_SubCuenta" HeaderText="GV_SubCuenta" UniqueName="GV_SubCuenta"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GV_SubSubCuenta" HeaderText="GV_SubSubCuenta"
                                                UniqueName="GV_SubSubCuenta" Display="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Autorización de Nuevos Acreedores">
                    <Items>
                        <telerik:RadPanelItem runat="server" Value="PanelAcreedor">
                            <ItemTemplate>
                                <telerik:RadGrid ID="rgAcreedor" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rgAcreedor_NeedDataSource" OnItemCommand="rgAcreedor_ItemCommand"
                                    OnPageIndexChanged="rgAcreedor_PageIndexChanged" PageSize="15" AllowPaging="True"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros."    >
                                    <MasterTableView    Font-Size="8"  >
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CD" UniqueName="Id_Cd" Display="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Acr" HeaderText="Clave" UniqueName="Id_Acr">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Nombre" HeaderText="Nombre" UniqueName="Acr_Nombre">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Calle" HeaderText="Calle" UniqueName="Acr_Calle">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Numero" HeaderText="Numero" UniqueName="Acr_Numero">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_NumInterior" HeaderText="Num. Int." UniqueName="Acr_NumInterior">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_CP" HeaderText="CP" UniqueName="Acr_CP">
                                                <HeaderStyle Width="70" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Colonia" HeaderText="Colonia" UniqueName="Acr_Colonia">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Municipio" HeaderText="Municipio" UniqueName="Acr_Municipio">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Estado" HeaderText="Estado" UniqueName="Acr_Estado">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                           
                                             <telerik:GridBoundColumn DataField="Acr_RFC" HeaderText="RFC" UniqueName="Acr_RFC">
                                                <HeaderStyle Width="60" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Correo" HeaderText="Correo" UniqueName="Acr_Correo">
                                                <HeaderStyle Width="60" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Acr_Banco" HeaderText="Banco" UniqueName="Acr_Banco">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_Cuenta" HeaderText="Cuenta Bancaria" UniqueName="Acr_Cuenta">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acr_CondPago" HeaderText="Días de Pago" UniqueName="Acr_CondPago">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridTemplateColumn HeaderText="Numero Acreedor" UniqueName="Acr_NumeroGenerado">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" CausesValidation="true" MaxLength="40"
                                                        runat="server" AutoPostBack="true" Style="text-transform: uppercase;">
                                                        <ClientEvents OnKeyPress="OnKeyPress" />
                                                    </telerik:RadTextBox>
                                                    <asp:RegularExpressionValidator ID="RfvTxtNumeroAcreedor" runat="server" Display="Dynamic"
                                                        ErrorMessage="*Invalido" ForeColor="Red" ValidationExpression="^([a-zñA-ZÑ\x26]{3}([0-9]{4}))?$"
                                                        ControlToValidate="TxtNumeroAcreedor">
                                                    </asp:RegularExpressionValidator>
                                                    <%--<telerik:RadNumericTextBox ID="TxtNumeroAcreedor" runat="server"></telerik:RadNumericTextBox>--%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea autorizar el acreedor?</br></br>" Text="Autorizar" ConfirmDialogHeight="150px"
                                                ConfirmDialogWidth="350px" UniqueName="Autorizar" Visible="True" ButtonType="ImageButton"
                                                ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirVentana_LstComprobantes(Id, Id_Cd) {
                var oWnd = radopen("CapPagosElectronicos_Listado.aspx?Id=" + Id + "&Id_Cd=" + Id_Cd, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }
            //JFCV abrir pantalla de Gastos de Viaje listado 
            function AbrirVentana_LstComprobantesGV(Id, Id_Emp, Id_Cd) {
                var oWnd = radopen("CapGastosViajeComprobantes_Listado.aspx?Id=" + Id + "&Id_Emp=" + Id_Emp + "&Id_Cd=" + Id_Cd, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }

            //jfcv 24oct2016  punto 13
            function cmdClient_Focus(sender, args) {
                var input = sender.get_inputDomElement();
                sender.highlightAllMatches(input.defaultValue);
                sender.showDropDown();
            }
        </script>
        <script type="text/javascript">
            function OnKeyPress(sender, args) {
                args.set_newValue(args.get_newValue().toUpperCase());
            }  
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
