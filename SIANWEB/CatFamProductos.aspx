﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatFamProductos.aspx.cs" Inherits="SIANWEB.CatFamProductos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario de Familias
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesFamilia() {

                var txtId = $find('<%= txtIdFam.ClientID %>');
                var txtDescripcion = $find('<%= txtDescripcionFam.ClientID %>');
                var chkActivo = document.getElementById('<%= chkActivo.ClientID %>');

                LimpiarTextBox(txtId);
                LimpiarTextBox(txtDescripcion);
                LimpiarCheckBox(chkActivo, true);
            }

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario de SubFamilias
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesSubFamilia() {

                var txtIdSubFam = $find('<%= txtIdSubFam.ClientID %>');
                var txtDescripcionSubFam = $find('<%= txtDescripcionSubFam.ClientID %>');
                var cmbFamilia = $find('<%= cmbFamilia.ClientID %>');
                var chkActivoSubFam = document.getElementById('<%= chkActivoSubFam.ClientID %>');

                LimpiarTextBox(txtIdSubFam);
                LimpiarTextBox(txtDescripcionSubFam);
                LimpiarComboSelectIndex0(cmbFamilia);
                LimpiarCheckBox(chkActivoSubFam, true);
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                //debugger;

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                var hiddenPanelActivo = document.getElementById('<%= hiddenPanelActivo.ClientID %>');

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save') {
                    if (hiddenPanelActivo.value == 'fam') {
                        for (i = 0; i < Page_Validators.length; i++) {
                            if (Page_Validators[i].id.indexOf('txtIdFam') != -1) ValidatorEnable(Page_Validators[i], true)
                            if (Page_Validators[i].id.indexOf('txtDescripcionFam') != -1) ValidatorEnable(Page_Validators[i], true);
                            if (Page_Validators[i].id.indexOf('txtIdSubFam') != -1) ValidatorEnable(Page_Validators[i], false)
                            if (Page_Validators[i].id.indexOf('txtDescripcionSubFam') != -1) ValidatorEnable(Page_Validators[i], false);
                            if (Page_Validators[i].id.indexOf('cmbFamilia') != -1) ValidatorEnable(Page_Validators[i], false);
                        }
                    }
                    else {
                        for (i = 0; i < Page_Validators.length; i++) {
                            if (Page_Validators[i].id.indexOf('txtIdFam') != -1) ValidatorEnable(Page_Validators[i], false)
                            if (Page_Validators[i].id.indexOf('txtDescripcionFam') != -1) ValidatorEnable(Page_Validators[i], false);
                            if (Page_Validators[i].id.indexOf('txtIdSubFam') != -1) ValidatorEnable(Page_Validators[i], true)
                            if (Page_Validators[i].id.indexOf('txtDescripcionSubFam') != -1) ValidatorEnable(Page_Validators[i], true);
                            if (Page_Validators[i].id.indexOf('cmbFamilia') != -1) ValidatorEnable(Page_Validators[i], true);
                        }
                    }
                }
                else {
                    for (i = 0; i < Page_Validators.length; i++) {
                        ValidatorEnable(Page_Validators[i], false);
                    }
                }



                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        if (hiddenPanelActivo.value == 'fam') {
                            LimpiarControlesFamilia();

                            //registro nuevo -> se limpia bandera de actualización
                            var hiddenActualiza = document.getElementById('<%= hiddenActualiza.ClientID %>');
                            hiddenActualiza.value = '';

                            //poner el foco en Descripcion
                            var txtId = $find('<%= txtIdFam.ClientID %>');
                            txtId.enable();

                            var urlArchivo = 'ObtenerMaximo.aspx';
                            parametros = "Catalogo=CatFamilia";
                            parametros = parametros + "&sp=spCatCentral_Maximo";
                            parametros = parametros + "&columna=Id_Fam";
                            var resultado = obtenerrequest(urlArchivo, parametros);
                            txtId.set_value(resultado);

                            txtId.focus();
                        }
                        else {
                            LimpiarControlesSubFamilia();

                            //registro nuevo -> se limpia bandera de actualización
                            var hiddenActualizaSub = document.getElementById('<%= hiddenActualizaSub.ClientID %>');
                            hiddenActualizaSub.value = '';

                            //poner el foco en Descripcion
                            var txtIdSubFam = $find('<%= txtIdSubFam.ClientID %>');
                            txtIdSubFam.enable();

                            var urlArchivo = 'ObtenerMaximo.aspx';
                            parametros = "Catalogo=CatSubfamilia";
                            parametros = parametros + "&sp=spCatCentral_Maximo";
                            parametros = parametros + "&columna=Id_Sub";
                            var resultado = obtenerrequest(urlArchivo, parametros);
                            txtIdSubFam.set_value(resultado);

                            txtIdSubFam.focus();
                        }

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
        
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="optOpciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFamilia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgSubFamilia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div class="formulario" id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
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
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenActualiza" runat="server" />
                    <asp:HiddenField ID="hiddenActualizaSub" runat="server" />
                    <asp:HiddenField ID="hiddenPanelActivo" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribuci&oacute;n"></asp:Label>
                </td>
              <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <br />
        <asp:RadioButtonList ID="optOpciones" runat="server" RepeatDirection="Horizontal"
            AutoPostBack="true" OnSelectedIndexChanged="optOpciones_SelectedIndexChanged">
            <asp:ListItem Text="Familias" Value="Familias" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Subfamilias" Value="SubFamilias"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:Panel ID="panelFamilias" runat="server">
            <table>
                <!-- Tabla principal--->
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Clave" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIdFam" runat="server" Width="50px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtIdFam" runat="server" 
                                        ControlToValidate="txtIdFam" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Descripción" />
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox onpaste="return false" ID="txtDescripcionFam" runat="server"
                                        Width="350px" MaxLength="250">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtDescripcionFam" runat="server" ControlToValidate="txtDescripcionFam"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td width="300">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <telerik:RadGrid ID="rgFamilia" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnItemCommand="rgFamilia_ItemCommand" OnNeedDataSource="rgFamilia_NeedDataSource"
                            OnPageIndexChanged="rgFamilia_PageIndexChanged">
                            <MasterTableView DataKeyNames="Id_Fam" DataMember="listFamProducto">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Fam" DataField="Id_Fam">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Descripci&oacute;n" UniqueName="Fam_Descripcion"
                                        DataField="Fam_Descripcion">
                                        <HeaderStyle Width="300px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fam_Activo" HeaderText="Estatus" UniqueName="Fam_Activo"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fam_ActivoStr" HeaderText="Estatus" UniqueName="Fam_ActivoStr">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelSubFamilias" runat="server">
            <table>
                <!-- Tabla principal--->
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Clave" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIdSubFam" runat="server" Width="50px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtIdSubFam" runat="server" 
                                        ControlToValidate="txtIdSubFam" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Descripción" />
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox onpaste="return false" ID="txtDescripcionSubFam" runat="server"
                                        Width="350px" MaxLength="250">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtDescripcionSubFam" runat="server" ControlToValidate="txtDescripcionSubFam"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Familia" />
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbFamilia" runat="server" Width="200px" Filter="Contains"
                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" EnableLoadOnDemand="true"
                                        DataTextField="Descripcion" DataValueField="Id" OnClientBlur="Combo_ClientBlur"
                                        LoadingMessage="Cargando...">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbFamilia" runat="server" 
                                        ControlToValidate="cmbFamilia" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkActivoSubFam" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged_SubFam"
                                        AutoPostBack="True" Checked="True" />
                                </td>
                                <td width="145">
                                    &nbsp;</td>
                                <td width="150">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <telerik:RadGrid ID="rgSubFamilia" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnItemCommand="rgSubFamilia_ItemCommand" OnNeedDataSource="rgSubFamilia_NeedDataSource"
                            OnPageIndexChanged="rgSubFamilia_PageIndexChanged">
                            <MasterTableView DataKeyNames="Id_Fam,Id_Sub" DataMember="listSubFamProducto">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Sub" DataField="Id_Sub">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Descripci&oacute;n" UniqueName="Sub_Descripcion"
                                        DataField="Sub_Descripcion">
                                        <HeaderStyle Width="300px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="IdFam" UniqueName="Id_Fam" DataField="Id_Fam"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Familia" UniqueName="Id_Fam_Str" DataField="Id_Fam_Str">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Sub_Activo" HeaderText="Estatus" UniqueName="Sub_Activo"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Sub_ActivoStr" HeaderText="Estatus" UniqueName="Sub_ActivoStr">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>