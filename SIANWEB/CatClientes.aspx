<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatClientes.aspx.cs" Inherits="SIANWEB.CatClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="chkRetencion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkPorcientoIVA">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting> 
            <telerik:AjaxSetting AjaxControlID="cmbCorporativa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="pnlGrid" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicked="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
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
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td valign="middle">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td valign="middle" style="text-align: right" width="150px">
                    <asp:Label ID="Label7" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td valign="middle" width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt" width="100%">
            <tr>
                <td valign="middle">
                </td>
                <td valign="middle">
                    <table>
                        <tr>
                            <td valign="middle" valign="middle">
                                <asp:Label ID="Label8" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td valign="middle">
                                <telerik:RadComboBox ID="cmbCliente" runat="server" Width="400px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCliente_SelectedIndexChanged" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                    MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." EnableAutomaticLoadOnDemand="True"
                                    EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True"
                                    MaxHeight="300px" EmptyMessage="-- Seleccionar --" OnClientDropDownOpening="Client_Focus"
                                    OnClientFocus="Client_Focus2">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td valign="middle" style="width: 50px; text-align: center">
                                                    <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                </td>
                                                <td valign="middle" style="width: 200px; text-align: left">
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
                        <tr>
                            <td valign="middle">
                            </td>
                            <td valign="middle">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="middle">
                </td>
                <td valign="middle">
                    <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                        SelectedIndex="0" TabIndex="-1" ValidationGroup="guardar" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab PageViewID="RadPageViewDGrales" Text="Datos <u>g</u>enerales " AccessKey="G">
                            </telerik:RadTab>
                            <telerik:RadTab PageViewID="RadPageViewDetalles" Text="Det<u>a</u>lles" AccessKey="A">
                            </telerik:RadTab>
                            <telerik:RadTab PageViewID="RadPageViewParametros" Text="<u>P</u>arámetros" AccessKey="P">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                        BorderStyle="Solid" BorderWidth="1px" Width="950px">
                        <!-- Aqui empieza el contenido de los tabs--->
                        <telerik:RadPageView ID="RadPageViewDGrales" runat="server">
                            <table style="font-family: vernada; font-size: 8;">
                                <!-- Tabla principal--->
                                <tr>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                        <table>
                                            <!--Tab 1  Tabla 1-->
                                            <tr>
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label9" runat="server" Text="Clave"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtClave" runat="server" MinValue="0" Width="70px"
                                                        MaxLength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="rdActivo" runat="server" Text="Activo" OnCheckedChanged="rdActivo_CheckedChanged"
                                                        AutoPostBack="True" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label10" runat="server" Text="Nombre"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="70" onpaste="return false"
                                                        Width="255px">
                                                       
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label99" runat="server" Text="Cuenta corporativa"></asp:Label>
                                                </td>
                                                <td colspan="2" valign="middle">
                                                    <telerik:RadNumericTextBox ID="numCorporativo" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="3" valign="middle">
                                                    <telerik:RadComboBox ID="cmbCorporativa" runat="server" DataTextField="Descripcion"
                                                        DataValueField="Id" EnableLoadOnDemand="True" Filter="Contains" HighlightTemplatedItems="True"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="True" OnClientBlur="Combo_ClientBlur"
                                                        OnClientSelectedIndexChanged="cmbCorporativa_ClientSelectedIndexChanged" Width="255px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" />
                                                        </Items>
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center" valign="middle">
                                                                        <asp:Label ID="LabelID0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left" valign="middle">
                                                                        <asp:Label ID="LabelDESC0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="numCorporativo"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label11" runat="server" Text="Contacto"></asp:Label>
                                                </td>
                                                <td colspan="5" valign="middle">
                                                    <telerik:RadTextBox ID="txtDcontacto" runat="server" onpaste="return false" Width="255px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label12" runat="server" Text="E-mail"></asp:Label>
                                                </td>
                                                <td colspan="4" valign="middle">
                                                    <telerik:RadTextBox ID="txtmail" runat="server" onpaste="return false" MaxLength="120"
                                                        Width="255px">
                                                        <ClientEvents OnKeyPress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr id="trReferencia" runat="server">
                                                <td valign="middle">
                                                    <asp:Label ID="Label62" runat="server" Text="Referencia"></asp:Label>
                                                </td>
                                                <td colspan="4" valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtReferencia" runat="server" MaxLength="8" MinValue="0"
                                                        onpaste="return false">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" colspan="2">
                                                    <strong>
                                                        <asp:Label ID="Label63" runat="server" Text="Correos para el envio de estados de cuenta"></asp:Label>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" width="120">
                                                    Correo 1
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCorreo1" runat="server"  MaxLength="120"
                                                        Width="255px">
                                                        <ClientEvents OnKeyPress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Correo 2
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCorreo2" runat="server" onpaste="return false" MaxLength="120"
                                                        Width="255px">
                                                        <ClientEvents OnKeyPress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Correo 3
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCorreo3" runat="server" onpaste="return false" MaxLength="120"
                                                        Width="255px">
                                                        <ClientEvents OnKeyPress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <!--Tab 1  Tabla 1-->
                                            <tr>
                                                <td valign="middle" colspan="2">
                                                    <strong>
                                                        <asp:Label ID="Label13" runat="server" Text="Datos fiscales"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle" class="style1">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <!--Datos Fiscales -->
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label14" runat="server" Text="Calle"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox onpaste="return false" ID="txtFcalle" runat="server" Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" OnBlur="txtFcalle_Blur" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFcalle"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label15" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td valign="middle" class="style1">
                                                    <telerik:RadTextBox onpaste="return false" ID="txtFnumero" runat="server">
                                                        <ClientEvents OnBlur="txtFnumero_Blur" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFnumero"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label16" runat="server" Text="CP"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtFcp" runat="server" Width="125px" MaxLength="6"
                                                        MinValue="0">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtFcp_Blur" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFcp"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label17" runat="server" Text="Colonia"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtFcolonia" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnBlur="txtFcolonia_Blur" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFcolonia"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label18" runat="server" Text="Municipio"></asp:Label>
                                                </td>
                                                <td colspan="4" valign="middle">
                                                    <telerik:RadTextBox ID="txtFmunicipio" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnBlur="txtFmunicipio_Blur" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFmunicipio"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label19" runat="server" Text="Estado "></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtFestado" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnBlur="txtFestado_Blur" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFestado"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td class="style1" valign="middle">
                                                    <telerik:RadTextBox ID="txtFciudad" runat="server" onpaste="return false" Visible="False">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td colspan="3" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label20" runat="server" Text="Teléfonos"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtFtelefono" runat="server" MaxLength="20">
                                                        <ClientEvents OnBlur="txtFtelefono_Blur" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td class="style1" valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label21" runat="server" Text="RFC"></asp:Label>
                                                </td>
                                                <td colspan="3" valign="middle">
                                                    <telerik:RadTextBox ID="txtFrfc" runat="server" MaxLength="14" onpaste="return false">
                                                        <ClientEvents OnBlur="txtFrfc_Blur" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="style1" valign="middle">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFrfc"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td colspan="2" valign="middle">
                                                    <strong>
                                                        <asp:Label ID="Label22" runat="server" Text="Datos de Consignación"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td class="style1" valign="middle">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" width="120">
                                                    <asp:Label ID="Label98" runat="server" Text="Nombre comercial"></asp:Label>
                                                </td>
                                                <td colspan="5" valign="middle">
                                                    <telerik:RadTextBox ID="txtNombreCorto" runat="server" MaxLength="50" onpaste="return false"
                                                        Width="255px">
                                                       
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label23" runat="server" Text="Calle"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtDcalle" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label24" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td class="style1" valign="middle">
                                                    <telerik:RadTextBox ID="txtDnumero" runat="server" onpaste="return false">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label25" runat="server" Text="CP"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtDcp" runat="server" MaxLength="6" MinValue="0"
                                                        Width="120px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label26" runat="server" Text="Colonia"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtDcolonia" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label27" runat="server" Text="Municipio"></asp:Label>
                                                </td>
                                                <td colspan="4" valign="middle">
                                                    <telerik:RadTextBox ID="txtDmunicipio" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label28" runat="server" Text="Estado"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtDestado" runat="server" onpaste="return false" Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td class="style1" valign="middle">
                                                    <telerik:RadTextBox ID="txtDciudad" runat="server" onpaste="return false" Visible="False">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label29" runat="server" Text="Teléfonos"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadTextBox ID="txtDtelefono" runat="server" MaxLength="20" onpaste="return false"
                                                        Width="125px">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="Label30" runat="server" Text="Fax"></asp:Label>
                                                </td>
                                                <td class="style1" valign="middle">
                                                    <telerik:RadTextBox ID="txtDfax" runat="server" MaxLength="20" Width="125px">
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label31" runat="server" Text="RFC"></asp:Label>
                                                </td>
                                                <td colspan="3" valign="middle">
                                                    <telerik:RadTextBox ID="txtDrfc" runat="server" MaxLength="14" onpaste="return false"
                                                        Width="125px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="style1" valign="middle">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle" width="200">
                                                    <asp:HiddenField ID="HF_ID" runat="server" />
                                                     <asp:HiddenField ID="HiddenCteNumCuentaContNal" runat="server" />
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td class="style1" valign="middle">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <table>
                                <tr>
                                    <td valign="middle">
                                        <asp:Panel runat="server" ID="pnlGrid" Width="944px" ScrollBars="Horizontal">
                                            <telerik:RadGrid ID="rgDetalles" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                OnNeedDataSource="rgDetalles_NeedDataSource" PageSize="15" AllowPaging="True"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgDetalles_ItemCommand"
                                                Height="420px">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="True" />
                                                </ClientSettings>
                                                <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Cons." UniqueName="Id_CteDet" Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label0" runat="server" Text='<%# Bind("Id_CteDet") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="Label0" runat="server" Text='<%# Bind("Id_CteDet") %>' />
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Activo">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk7" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Cte_Activo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Cte_Activo")) %>'
                                                                    ToolTip='<%# Eval("Id_CteDet", "Cons. {0}") %>' OnCheckedChanged="chkActivoDet_CheckedChanged"
                                                                    AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="chk8" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Cte_Activo") is DBNull?true:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Cte_Activo")) %>'
                                                                    OnCheckedChanged="chkActivoDet_CheckedChanged" AutoPostBack="true" />
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Ter_NombreNum">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label100" runat="server" Text='<%# Bind("Id_Ter") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Ter" runat="server" Text='<%# Bind("Id_Ter") %>'
                                                                    Width="60px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Editar") is DBNull ? true :  Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Editar"))%>'>
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="txtTerritorioPartida_OnBlur"
                                                                        OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbleditar" runat="server" Text='<%# Bind("Editar") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Territorio" UniqueName="Ter_Nombre">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="Label101" runat="server" Text=" - " />--%>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Ter_Nombre") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold1" runat="server" Text='<%# Bind("Id_Ter") %>' Visible="false" />
                                                                <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="300px" Filter="Contains"
                                                                    MaxHeight="300px" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                    OnClientLoad="cmbTerritorioPartida_OnLoad" OnClientSelectedIndexChanged="cmbTerritorioPartida_ClientSelectedIndexChanged"
                                                                    OnClientBlur="Combo_ClientBlur" OnDataBinding="cmbTerritorio_DataBinding" OnItemDataBound="Territorio_ItemDataBound"
                                                                    OnTextChanged="cmbTerritorioDet_TextChanged" AutoPostBack="true" Enabled='<%# DataBinder.Eval(Container.DataItem, "Editar") is DBNull ? true :  Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Editar"))%>'>
                                                                    <ItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td valign="middle" style="width: 20px; text-align: right; vertical-align: top">
                                                                                    <asp:Label ID="LabelID" runat="server" Width="20px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                                </td>
                                                                                <td valign="middle" style="width: 3px">
                                                                                </td>
                                                                                <td valign="middle" style="text-align: left">
                                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Seg">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121" runat="server" Text='<%# Bind("Id_Seg") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblId_Seg" runat="server" Text='<%# Bind("Id_Seg") %>' />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                     
                                                        <telerik:GridTemplateColumn HeaderText="Segmento" UniqueName="Seg_Descripcion">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="Label122" runat="server" Text=" - " />--%>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Seg_Descripcion") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold2" runat="server" Text='<%# Bind("Id_Seg") %>' Visible="false" />
                                                               <%-- <asp:Label ID="LabelSeg_Descripcion" runat="server" Text='<%# Bind("Seg_Descripcion") %>' />--%>
                                                                <telerik:RadComboBox ID="RadComboBox2" runat="server" OnDataBinding="cmbSegmento_DataBinding"
                                                                   MarkFirstMatch="true" OnItemDataBound="Segmento_ItemDataBound" EmptyMessage="Seleccione territorio"
                                                                    Width="200" OnTextChanged="cmbSegmento_TextChanged" AutoPostBack="true" 
                                                                   Enable="false" >
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="250px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <telerik:GridTemplateColumn UniqueName="IdUEN_Oculto" Display="false">
                                                            <ItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_UenIT" runat="server" Text='<%# Bind("Id_Uen") %>'>
                                                                </telerik:RadNumericTextBox>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Uen" runat="server">
                                                                    <ClientEvents OnLoad="OnUenLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Uen" DataField="Id_Uen">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblIDUEN" Text='<%# Bind("Id_Uen") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label runat="server" ID="lblIDUENEdit" Text='<%# Bind("Id_Uen") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="UEN" UniqueName="uen" DataField="uen">
                                                            <EditItemTemplate>
                                                                <asp:Label runat="server" ID="lblUENDetEdit" Text='<%# Bind("uen") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblUENDet" Text='<%# Bind("uen") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="250px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Rik">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelRik" runat="server" Text='<%# Bind("Id_Rik") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="LabelRikEdit" runat="server" Text='<%# Bind("Id_Rik") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Representante" UniqueName="rik" DataField="rik">
                                                            <EditItemTemplate>
                                                                <asp:Label runat="server" ID="lblRikDetEdit" Text='<%# Bind("rik") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRikDet" Text='<%# Bind("rik") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Unidades de dimensión" UniqueName="Cte_UnidadDim">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Cte_UnidadDim") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold3" runat="server" Text='<%# Bind("Cte_UnidadDim") %>' Visible="false" />
                                                                <asp:Label ID="RadTextBox3" runat="server" Text='<%# Bind("Cte_UnidadDim") %>'>
                                                                </asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="250px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Dimensión" UniqueName="Cte_Dimension">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Cte_Dimension") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold4" runat="server" Text='<%# Bind("Cte_Dimension", "N2") %>'
                                                                    Visible="false" />
                                                                <telerik:RadNumericTextBox ID="RadTextBox4" runat="server" Text='<%# Bind("Cte_Dimension") %>'
                                                                    MinValue="0" Width="70px" MaxLength="9">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="OnDimensionLoad" OnBlur="OnBlurDimension" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Pesos" UniqueName="Cte_Pesos">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Cte_Pesos", "{0:N2}") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold5" runat="server" Text='<%# Bind("Cte_Pesos") %>' Visible="false" />
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBox5" runat="server" Text='<%# Bind("Cte_Pesos") %>'
                                                                    MinValue="0" Width="70px" MaxLength="9" NumberFormat-DecimalDigits="2" ReadOnly="true">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="OnPesosLoad" OnBlur="OnBlurDimension" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Potencial" UniqueName="Cte_Potencial">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Cte_Potencial", "{0:N2}") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold6" runat="server" Text='<%# Bind("Cte_Potencial") %>' Visible="false" />
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBox6" runat="server" Text='<%# Bind("Cte_Potencial") %>'
                                                                    Width="70px" MaxLength="9">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="OnPotencialLoad" OnBlur="OnBlurPotencial" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Mano de obra en proyectos" UniqueName="Cte_ManoObra">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Cte_ManoObra", "{0:N2}") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold7" runat="server" Text='<%# Bind("Cte_ManoObra") %>' Visible="false" />
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBox7" runat="server" Text='<%# Bind("Cte_ManoObra") %>'
                                                                    Width="70px" MaxLength="9" MinValue="0">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Gastos territorio" UniqueName="Cte_GastoTerritorio">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Cte_GastoTerritorio", "{0:N2}") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold8" runat="server" Text='<%# Bind("Cte_GastoTerritorio") %>'
                                                                    Visible="false" />
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBox8" runat="server" Text='<%# Bind("Cte_GastoTerritorio") %>'
                                                                    Width="70px" MaxLength="9" MinValue="0">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Fletes pagados al cliente" UniqueName="Cte_FletePaga">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("Cte_FletePaga", "{0:N2}") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold9" runat="server" Text='<%# Bind("Cte_FletePaga") %>' Visible="false" />
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBox9" runat="server" Text='<%# Bind("Cte_FletePaga") %>'
                                                                    Width="70px" MaxLength="9" MinValue="0">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="120px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Porcentaje de comisión" UniqueName="Cte_PorcComision">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComision" runat="server" Text='<%# Bind("Cte_PorcComision", "{0:N2}") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblComisionold" runat="server" Text='<%# Bind("Cte_PorcComision") %>'
                                                                    Visible="false" />
                                                                <telerik:RadNumericTextBox ID="txtComision" runat="server" Text='<%# Bind("Cte_PorcComision") %>'
                                                                    Width="70px" MinValue="0" MaxLength="9">
                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="120px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Aceptar">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="70px" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                            UniqueName="DeleteColumn">
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <EditColumn UniqueName="EditCommandColumn1">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewParametros" runat="server">
                            <table style="font-family: vernada; font-size: 8;">
                                <!-- Tabla principal--->
                                <tr>
                                    <td valign="middle">
                                    </td>
                                    <td valign="middle">
                                        <table>
                                            <!--Tab 2 Tabla 1 -->
                                            <tr>
                                                <td valign="middle" width="130">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                    <asp:CheckBox ID="chkCredito" runat="server" Text="Crédito" />
                                                </td>
                                                <td valign="middle">
                                                    <asp:CheckBox ID="chkFacturar" runat="server" Text="Permitir facturar" />
                                                </td>
                                                <td valign="middle">
                                                    <asp:CheckBox ID="chkCredSuspendido" runat="server" Text="Crédito suspendido" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="130">
                                                    <asp:Label ID="Label34" runat="server" Text="Tipo de moneda"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbMoneda" runat="server" Width="200px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cmbMoneda"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label35" runat="server" Text="Límite de Crédito"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtCobranza" runat="server" MaxLength="9" MinValue="0">
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="130">
                                                    <asp:Label ID="Label44" runat="server" Text="Condiciones de pago"></asp:Label>
                                                </td>
                                                <td colspan="7" valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtDias1" runat="server" MaxLength="3" MinValue="0"
                                                        Width="30px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="7" valign="middle">
                                                    <asp:Label ID="Label60" runat="server" Text="días"></asp:Label>
                                                </td>
                                                <td width="20px">
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAutorizo" runat="server" Text="Autorizó"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtAutorizo" runat="server" Width="300">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="130">
                                                    <asp:Label ID="Label36" runat="server" Text="Horario de revisión"></asp:Label>
                                                </td>
                                                <td colspan="14" valign="middle">
                                                    <telerik:RadTimePicker ID="txtRHoraam1" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="es-MX" EndTime="11:59:59"
                                                            HeaderText="Horario de revisión">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinamRevision" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    <asp:Label ID="Label74" runat="server" Text="a"></asp:Label>
                                                    <telerik:RadTimePicker ID="txtRHoraam2" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="es-MX" EndTime="11:59:59"
                                                            HeaderText="Horario de revisión">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinamRevision" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    <asp:Label ID="Label75" runat="server" Text="y"></asp:Label>
                                                    <telerik:RadTimePicker ID="txtRHorapm1" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar3" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView3" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="Horario de revisión"
                                                            StartTime="12:00:00">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinpmRevision" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    <asp:Label ID="Label76" runat="server" Text="a"></asp:Label>
                                                    <telerik:RadTimePicker ID="txtRHorapm2" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar4" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView4" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="Horario de revisión"
                                                            StartTime="12:00:00">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinpmRevision" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label101" runat="server" Text="Semana de revisión"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtSemanaRevision" runat="server" MaxLength="9" MinValue="0"
                                                        Width="30px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle" colspan="2">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label37" runat="server" Text="Días de revisión"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label59" runat="server" Text="Lunes" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label38" runat="server" Text="Martes" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label39" runat="server" Text="Miércoles"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                    <asp:Label ID="Label40" runat="server" Text="Jueves"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label41" runat="server" Text="Viernes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label42" runat="server" Text="Sábado"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label43" runat="server" Text="Domingo"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label80" runat="server" Text="Revisión lunes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRLunes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label32" runat="server" Text="Revisión martes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRMartes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label33" runat="server" Text="Revisión miércoles" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRMiercoles" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                    <asp:Label ID="Label53" runat="server" Text="Revisión jueves" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRJueves" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label81" runat="server" Text="Revisión viernes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRViernes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label82" runat="server" Text="Revisión sabado" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRSabado" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label83" runat="server" Text="Revisión domingo" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRDomingo" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td colspan="14" valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label61" runat="server" Text="Horario de pago"></asp:Label>
                                                </td>
                                                <td colspan="14" valign="middle">
                                                    <telerik:RadTimePicker ID="txtPHoraam1" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar5" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView5" runat="server" CellSpacing="-1" Culture="es-MX" EndTime="11:59:59"
                                                            HeaderText="Horario de pago">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput5" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinamPago" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    <asp:Label ID="Label77" runat="server" Text="a"></asp:Label>
                                                    <telerik:RadTimePicker ID="txtPHoraam2" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar6" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView6" runat="server" CellSpacing="-1" Culture="es-MX" EndTime="11:59:59"
                                                            HeaderText="Horario de pago">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput6" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinamPago" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    <asp:Label ID="Label78" runat="server" Text="y"></asp:Label>
                                                    <telerik:RadTimePicker ID="txtPHorapm1" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar7" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView7" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="Horario de pago"
                                                            StartTime="12:00:00">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput7" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinpmPago" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    <asp:Label ID="Label79" runat="server" Text="a"></asp:Label>
                                                    <telerik:RadTimePicker ID="txtPHorapm2" runat="server" Culture="es-MX">
                                                        <Calendar ID="Calendar8" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView8" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="Horario de pago"
                                                            StartTime="12:00:00">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput8" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                            <ClientEvents OnBlur="MinpmPago" OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label64" runat="server" Text="Semana de pago"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtSemanaPago" runat="server" MaxLength="9" MinValue="0"
                                                        Width="30px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle" colspan="2">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label45" runat="server" Text="Días de pago"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label46" runat="server" Text="Lunes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label47" runat="server" Text="Martes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label48" runat="server" Text="Miércoles"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                    <asp:Label ID="Label49" runat="server" Text="Jueves"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label50" runat="server" Text="Viernes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label51" runat="server" Text="Sábado"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label52" runat="server" Text="Domingo"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label85" runat="server" Text="Pago lunes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPLunes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label84" runat="server" Text="Pago martes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPMartes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label86" runat="server" Text="Pago miércoles" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPMiercoles" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                    <asp:Label ID="Label87" runat="server" Text="Pago jueves" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPJueves" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label88" runat="server" Text="Pago viernes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPViernes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label89" runat="server" Text="Pago sábados" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPSabado" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label90" runat="server" Text="Pago domingo" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkPDomingo" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label65" runat="server" Text="Semana de recepción"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtSemana" runat="server" MaxLength="9" MinValue="0"
                                                        Width="30px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle" colspan="2">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label66" runat="server" Text="Días de recepción"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label67" runat="server" Text="Lunes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label68" runat="server" Text="Martes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label69" runat="server" Text="Miércoles"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                    <asp:Label ID="Label70" runat="server" Text="Jueves"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label71" runat="server" Text="Viernes"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label72" runat="server" Text="Sábado"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label73" runat="server" Text="Domingo"></asp:Label>
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label91" runat="server" Text="Recepción lunes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecLunes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label92" runat="server" Text="Recepción martes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecMartes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label93" runat="server" Text="Recepción miércoles" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecMiercoles" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle" colspan="2">
                                                    <asp:Label ID="Label94" runat="server" Text="Recepción jueves" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecJueves" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label95" runat="server" Text="Recepción viernes" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecViernes" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label96" runat="server" Text="Recepción sábado" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecSabado" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                    <asp:Label ID="Label97" runat="server" Text="Recepción domingo" Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chkRecDomingo" runat="server" />
                                                </td>
                                                <td style="text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" width="130">
                                                    <asp:Label ID="LbTelCobranza" runat="server" Text="Tels. de Cobranza"></asp:Label>
                                                </td>
                                                <td valign="middle" colspan="5">
                                                    <telerik:RadTextBox onpaste="return false" ID="txtTelCobranza1" runat="server" Width="250px"
                                                        MaxLength="255">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                    <td valign="middle" colspan="5">
                                                        <telerik:RadTextBox onpaste="return false" ID="txtTelCobranza2" runat="server" Width="250px"
                                                            MaxLength="255">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle" valign="top" width="360px">
                                                    <table>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:Label ID="Label102" runat="server" Text="Formas de pago" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <telerik:RadListBox ID="ListFPago" runat="server" CheckBoxes="True" BorderStyle="None">
                                                                </telerik:RadListBox>
                                                                <table runat="server" visible="false">
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <asp:CheckBox ID="chkEfectivo" runat="server" Text="Efectivo" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <asp:CheckBox ID="chkFactoraje" runat="server" Text="Factoraje" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <asp:CheckBox ID="chkTransferencia" runat="server" Text="Transferencia" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <asp:CheckBox ID="chkCheque" runat="server" Text="Cheque" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="middle" valign="top">
                                                                <asp:Label ID="LblCuenta" runat="server" Text="Últimos 4 dígitos de la cuenta"></asp:Label><br />
                                                                <telerik:RadTextBox ID="txtUDigitos" runat="server" MaxLength="50" Width="80px">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="middle" valign="top" width="200px">
                                                    <table>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:Label ID="Label103" runat="server" Font-Bold="True" Text="Comisiones"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:CheckBox ID="chkComisiones" runat="server" Text="Especiales" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="middle" valign="top" width="200px">
                                                    <table>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:Label ID="Label104" runat="server" Font-Bold="True" Text="Documentos"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:CheckBox ID="chkDesglose" runat="server" Text="Desglose de IVA" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:CheckBox ID="chkRetencion" runat="server" Text="Retención de IVA " OnCheckedChanged="chkRetencion_CheckedChanged" AutoPostBack="True" />                                                         
                                                            <telerik:RadNumericTextBox ID="txtPorcRetencion" runat="server" MinValue="0" Width="40px"
                                                            MaxLength="5" MaxValue="100"  >
                                                            <NumberFormat DecimalDigits="2" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox> %
                                                            </td>
                                                         </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <telerik:RadTextBox onpaste="return false" ID="txtDocumentos" runat="server" Width="250px">
                                                                    <%--    <ClientEvents OnKeyPress="SoloAlfanumerico" />--%>
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:CheckBox ID="ChkPorcientoIVA" runat="server" Text="Porcentaje de IVA del Cliente " OnCheckedChanged="chkPorcientoIVA_CheckedChanged" AutoPostBack="True"/>                                                        
                                                            <telerik:RadNumericTextBox ID="txtPorcientoIVA" runat="server" MinValue="0" Width="40px"
                                                                                MaxLength="5" MaxValue="100">
                                                            <NumberFormat DecimalDigits="2" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox> %
                                                            </td>
                                                         </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:CheckBox ID="chkOrdenCompra" runat="server" Text="Requiere orden de compra" />
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label54" runat="server" Text="Asignación de pedido"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbAsignacion" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="200px"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle" style="width: 20px">
                                                </td>
                                                <td valign="middle" colspan="2">
                                                    <asp:CheckBox ID="chkNotaCredFac" runat="server" Text="Crear nota de crédito al facturar por" />
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadNumericTextBox ID="txtPorcFacturar" runat="server" MinValue="0" Width="40px"
                                                        MaxLength="5" MaxValue="100">
                                                        <NumberFormat DecimalDigits="2" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                    %
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td valign="middle">
                                                    <asp:Label ID="LblRemisionElec" runat="server" Text="Remisión electrónica"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbRemisionElect" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="200px" Autopostback="True"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" OnSelectedIndexChanged="cmbRemisionElect_SelectedIndexChanged">
                                                        <Items>
                                                          
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label55" runat="server" Text="Serie de consecutivo"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbSerie" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label56" runat="server" Text="Serie de nota de crédito"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbNCredito" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label57" runat="server" Text="Serie de nota de cargo"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbNCargo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Label ID="Label58" runat="server" Text="Adenda"></asp:Label>
                                                </td>
                                                <td valign="middle">
                                                    <telerik:RadComboBox ID="cmbAdenda" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        Width="200px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="width: 50px; text-align: center">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td valign="middle" style="width: 200px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                                <td valign="middle" width="10">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirVentana_Autorizacion() {
                var oWnd = radopen("Ventana_Autorizacion.aspx", "AbrirVentana_Autorizacion");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }
            function autorizar(id_u, id_cd, nombre) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(id_u + "@" + id_cd + "@" + nombre);
            }
            var cmbter;
            var txtTerritorioPartida;
            function txtTerritorioPartida_OnBlur(sender, args) {
                //debugger; 
                OnBlur(sender, cmbTer);
            }
            function txtTerritorioPartida_OnLoad(sender, args) {
                txtTerritorioPartida = sender;
            }

            function cmbTerritorioPartida_OnLoad(sender, args) {
                cmbTer = sender;
            }
            function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida);
            }
            var focus;
            function Client_Focus() {
                var cid = "<%=cmbCliente.ClientID %>" + "_Input";
                var input = document.getElementById(cid);

                input.value = "";

            }
            function Client_Focus2() {
                var cid = "<%=cmbCliente.ClientID %>" + "_Input";
                var input = document.getElementById(cid);

                input.value = "";
            }
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;

                var button = args.get_item();
                var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

                var cmb = $find('<%= cmbMoneda.ClientID %>');
                var txtClave = $find('<%= txtClave.ClientID %>');
                var txtDescripcion = $find('<%= txtDescripcion.ClientID %>');
                var numCorporativo = $find('<%= numCorporativo.ClientID %>');
                var txtFcalle = $find('<%= txtFcalle.ClientID %>');
                var txtFnumero = $find('<%= txtFnumero.ClientID %>');
                var txtFcp = $find('<%= txtFcp.ClientID %>');
                var txtFcolonia = $find('<%= txtFcolonia.ClientID %>');
                var txtFmunicipio = $find('<%= txtFmunicipio.ClientID %>');
                var txtFestado = $find('<%= txtFestado.ClientID %>');
                var txtFrfc = $find('<%= txtFrfc.ClientID %>');

                if (txtClave.get_value() == "" || txtDescripcion.get_value() == "" || numCorporativo.get_value() == "" || txtFcalle.get_value() == "" || txtFnumero.get_value() == "" ||
                txtFcp.get_value() == "" || txtFcolonia.get_value() == "" || txtFmunicipio.get_value() == "" || txtFestado.get_value() == "" || txtFrfc.get_value() == "") {
                    radTabStrip.get_allTabs()[0].select();
                }
                else if (cmb.get_value() == "" || cmb.get_value() == "-1") {
                    radTabStrip.get_allTabs()[2].select();
                }
                else {
                    radTabStrip.get_allTabs()[0].select();
                }
            }

            //--------------------------------------------------------------------------------------------------
            //   controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= numCorporativo.ClientID %>'));
                LimpiarTextBox($find('<%= txtNombreCorto.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtDcontacto.ClientID %>'));
                LimpiarTextBox($find('<%= txtmail.ClientID %>'));
                LimpiarTextBox($find('<%= txtFcalle.ClientID %>'));
                LimpiarTextBox($find('<%= txtFnumero.ClientID %>'));
                LimpiarTextBox($find('<%= txtFcp.ClientID %>'));
                LimpiarTextBox($find('<%= txtFcolonia.ClientID %>'));
                LimpiarTextBox($find('<%= txtFmunicipio.ClientID %>'));
                LimpiarTextBox($find('<%= txtFestado.ClientID %>'));
                LimpiarTextBox($find('<%= txtFtelefono.ClientID %>'));
                LimpiarTextBox($find('<%= txtFrfc.ClientID %>'));
                LimpiarTextBox($find('<%= txtDcalle.ClientID %>'));
                LimpiarTextBox($find('<%= txtDnumero.ClientID %>'));
                LimpiarTextBox($find('<%= txtDcp.ClientID %>'));
                LimpiarTextBox($find('<%= txtDcolonia.ClientID %>'));
                LimpiarTextBox($find('<%= txtDmunicipio.ClientID %>'));
                LimpiarTextBox($find('<%= txtDestado.ClientID %>'));
                LimpiarTextBox($find('<%= txtDtelefono.ClientID %>'));
                LimpiarTextBox($find('<%= txtDfax.ClientID %>'));
                LimpiarTextBox($find('<%= txtDrfc.ClientID %>'));
                LimpiarTextBox($find('<%= txtDocumentos.ClientID %>'));

                LimpiarTextBox($find('<%= txtCobranza.ClientID %>'));
                LimpiarTextBox($find('<%= txtDias1.ClientID %>'));


                LimpiarComboSelectIndex0($find('<%= cmbCorporativa.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbMoneda.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbSerie.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbNCargo.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbNCredito.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbAsignacion.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbAdenda.ClientID %>'));



                LimpiarCheckBox(document.getElementById('<%= chkCredito.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkFacturar.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRLunes.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRMartes.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRMiercoles.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRJueves.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRViernes.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRSabado.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRDomingo.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPLunes.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPMartes.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPMiercoles.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPJueves.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPViernes.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPSabado.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkPDomingo.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkComisiones.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkDesglose.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkRetencion.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= ChkPorcientoIVA.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkEfectivo.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkFactoraje.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkTransferencia.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkCheque.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkOrdenCompra.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= rdActivo.ClientID %>'), true);

                LimpiarDatePicker($find('<%= txtRHoraam1.ClientID %>'));
                LimpiarDatePicker($find('<%= txtRHoraam2.ClientID %>'));
                LimpiarDatePicker($find('<%= txtRHorapm1.ClientID %>'));
                LimpiarDatePicker($find('<%= txtRHorapm2.ClientID %>'));

                LimpiarDatePicker($find('<%= txtPHoraam1.ClientID %>'));
                LimpiarDatePicker($find('<%= txtPHoraam2.ClientID %>'));
                LimpiarDatePicker($find('<%= txtPHorapm1.ClientID %>'));
                LimpiarDatePicker($find('<%= txtPHorapm2.ClientID %>'));
                //LimpiarComboSelectIndex0($find('<%= cmbCliente.ClientID %>'));
            }


            var txtDimension;
            var txtPesos;
            var txtPotencial;

            var txtUen;

            function OnPesosLoad(sender, args) {
                txtPesos = sender;
            }
            function OnDimensionLoad(sender, args) {
                txtDimension = sender;
            }
            function OnPotencialLoad(sender, args) {
                txtPotencial = sender;
            }
            function OnUenLoad(sender, args) {
                txtUen = sender;
            }

            function OnBlurPotencial(sender, args) {

                if (txtPesos.get_value() == 0) {
                    txtDimension.set_value(0);
                }
                else {
                    txtDimension.set_value(txtPotencial.get_value() / txtPesos.get_value());
                }
                if (txtPotencial.get_value() == 0) {
                    radalert('El potencial no debe ser cero', 330, 150);
                    //txtPotencial.set_value('');
                    //txtPotencial.focus();
                }
            }

            function OnBlurDimension(sender, args) {

                if (txtUen.get_value() == 1) {
                    txtPotencial.set_value(txtPesos.get_value() * txtDimension.get_value());
                }
                else if (txtUen.get_value() == 4) {
                    txtPotencial.set_value(txtDimension.get_value());
                }

                if (txtPotencial.get_value() == 0) {
                    radalert('El potencial no debe ser cero', 330, 150);
                    //txtDimension.set_value('');
                    //txtDimension.focus();
                }

            }
            //---------------------------------------------------------------------------
            //funciones para transportar los datos de default de facturación
            //---------------------------------------------------------------------------

            function txtFcalle_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo

                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDcalle = $find('<%= txtDcalle.ClientID %>');
                        txtDcalle.set_value(sender.get_value());
                    }
                }
            }



            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCorporativa.ClientID %>'));
            }

            function cmbCorporativa_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= numCorporativo.ClientID %>'));
            }




            function txtFcalle_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo

                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDcalle = $find('<%= txtDcalle.ClientID %>');
                        txtDcalle.set_value(sender.get_value());
                    }
                }
            }

            function txtFcalle_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDcalle = $find('<%= txtDcalle.ClientID %>');
                        txtDcalle.set_value(sender.get_value());
                    }
                }
            }

            function txtFnumero_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDnumero = $find('<%= txtDnumero.ClientID %>');
                        txtDnumero.set_value(sender.get_value());
                    }
                }
            }

            function txtFcp_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDcp = $find('<%= txtDcp.ClientID %>');
                        txtDcp.set_value(sender.get_value());
                    }
                }
            }

            function txtFcolonia_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDcolonia = $find('<%= txtDcolonia.ClientID %>');
                        txtDcolonia.set_value(sender.get_value());
                    }
                }
            }

            function txtFmunicipio_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDmunicipio = $find('<%= txtDmunicipio.ClientID %>');
                        txtDmunicipio.set_value(sender.get_value());
                    }
                }
            }

            function txtFestado_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDestado = $find('<%= txtDestado.ClientID %>');
                        txtDestado.set_value(sender.get_value());
                    }
                }
            }

            function txtFtelefono_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDtelefono = $find('<%= txtDtelefono.ClientID %>');
                        txtDtelefono.set_value(sender.get_value());
                    }
                }
            }

            function txtFrfc_Blur(sender, eventArgs) {
                var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                //el texto de default se pasa solo si es registro nuevo
                if (hiddenActualiza.value == '') {
                    if (sender.get_value() != '') {
                        var txtDrfc = $find('<%= txtDrfc.ClientID %>');
                        txtDrfc.set_value(sender.get_value());
                    }
                }
            }


            function MinamRevision(sender, eventArgs) {
                var min = $find('<%= txtRHoraam1.ClientID %>');
                var max = $find('<%= txtRHoraam2.ClientID %>');

                if (sender.get_selectedDate() != null) {
                    if (sender.get_selectedDate().format('HH:mm:ss') > '11:59:59') {
                        sender.set_value('11:59');
                    }
                }

                if (min.get_selectedDate() != null && max.get_selectedDate() != null) {
                    if (min.get_selectedDate().format('HH:mm:ss') > max.get_selectedDate().format('HH:mm:ss')) {
                        if ('ctl00_CPH_txtRHoraam2_dateInput' == sender._clientID) {
                            sender.set_value(min.get_selectedDate().format('HH:mm:ss'));
                        }
                        else {
                            sender.set_value(max.get_selectedDate().format('HH:mm:ss'));
                        }
                    }
                }
            }
            function MinamPago(sender, eventArgs) {

                var min = $find('<%= txtPHoraam1.ClientID %>');
                var max = $find('<%= txtPHoraam2.ClientID %>');

                if (sender.get_selectedDate() != null) {
                    if (sender.get_selectedDate().format('HH:mm:ss') > '11:59:59') {
                        sender.set_value('11:59');
                    }
                }

                if (min.get_selectedDate() != null && max.get_selectedDate() != null) {
                    if (min.get_selectedDate().format('HH:mm:ss') > max.get_selectedDate().format('HH:mm:ss')) {
                        if ('ctl00_CPH_txtPHoraam2_dateInput' == sender._clientID) {
                            sender.set_value(min.get_selectedDate().format('HH:mm:ss'));
                        }
                        else {
                            sender.set_value(max.get_selectedDate().format('HH:mm:ss'));
                        }
                    }
                }
            }

            function MinpmRevision(sender, eventArgs) {

                var min = $find('<%= txtRHorapm1.ClientID %>');
                var max = $find('<%= txtRHorapm2.ClientID %>');
                if (sender.get_selectedDate() != null) {
                    if (sender.get_selectedDate().format('HH:mm:ss') < '12:00') {
                        sender.set_value('12:00');
                    }
                }

                if (min.get_selectedDate() != null && max.get_selectedDate() != null) {
                    if (min.get_selectedDate().format('HH:mm:ss') > max.get_selectedDate().format('HH:mm:ss')) {
                        if ('ctl00_CPH_txtRHorapm2_dateInput' == sender._clientID) {
                            sender.set_value(min.get_selectedDate().format('HH:mm:ss'));
                        }
                        else {
                            sender.set_value(max.get_selectedDate().format('HH:mm:ss'));
                        }
                    }
                }
            }

            function MinpmPago(sender, eventArgs) {
                var min = $find('<%= txtPHorapm1.ClientID %>');
                var max = $find('<%= txtPHorapm2.ClientID %>');

                if (sender.get_selectedDate() != null) {
                    if (sender.get_selectedDate().format('HH:mm:ss') < '12:00:00') {
                        sender.set_value('12:00');
                    }
                }

                if (min.get_selectedDate() != null && max.get_selectedDate() != null) {
                    if (min.get_selectedDate().format('HH:mm:ss') > max.get_selectedDate().format('HH:mm:ss')) {
                        if ('ctl00_CPH_txtPHoraam2_dateInput' == sender._clientID) {
                            sender.set_value(min.get_selectedDate().format('HH:mm:ss'));
                        }
                        else {
                            sender.set_value(max.get_selectedDate().format('HH:mm:ss'));
                        }
                    }
                }
            }
            function cmbCorporativa_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= numCorporativo.ClientID %>'));
            }

            /*function cmbRemisionElect_SelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= numCorporativo.ClientID %>'));
            }*/

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
