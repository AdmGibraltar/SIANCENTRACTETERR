﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatConceptos.aspx.cs" Inherits="SIANWEB.CatConceptos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="rtb1_ButtonClick">
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
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
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
                            <td width="80">
                                &nbsp;
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                            <td width="230">
                                &nbsp;
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblClave" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="300px"
                                    MaxLength="50">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HF_ID" runat="server" />
                            </td>
                            <td colspan="1">
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" AutoPostBack="True"
                                    OnCheckedChanged="chkActivo_CheckedChanged" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="rg1_NeedDataSource" OnItemCommand="rg1_ItemCommand" OnPageIndexChanged="rg1_PageIndexChanged"
                        PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Compensacion" HeaderText="Clave" UniqueName="Id_Compensacion">
                                    <HeaderStyle Width="80" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Compensacion_Descripcion" HeaderText="Descripción" UniqueName="Compensacion_Descripcion">
                                    <HeaderStyle Width="200" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Compensacion_Estatus" HeaderText="Estatus" UniqueName="Compensacion_Estatus"
                                    Visible="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                    <HeaderStyle Width="100px" />
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
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
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


                switch (button.get_value()) {
                    case 'new':
                        //debugger;

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                        hiddenActualiza.value = '';

                        var txtId = $find('<%= txtClave.ClientID %>');
                        txtId.enable();
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatCompensacion_Maximo";
                        parametros = parametros + "&sp=spCatCompensacion_Maximo";
                        parametros = parametros + "&columna=Id_Compensacion";
                        parametros = parametros + "&Naturaleza=0";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        txtId.focus();

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

           

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
