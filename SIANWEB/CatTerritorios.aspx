<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatTerritorios.aspx.cs" Inherits="SIANWEB.CatTerritorios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbUen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                       />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
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
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label5" runat="server" Text="Centro de distribución"></asp:Label>
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
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talles" AccessKey="E" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="250px"
                        BorderStyle="Solid" BorderWidth="1px" Width="610px">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td width="70">
                                    </td>
                                    <td width="123">
                                        &nbsp;
                                    </td>
                                    <td width="75">
                                        &nbsp;
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
                                        <asp:Label ID="Label6" runat="server" Text="Clave"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="197px"
                                            MaxLength="50">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="Label8" runat="server" Text="UEN"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtUen" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt1_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbUen" runat="server" OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." 
                                            AutoPostBack="True" onselectedindexchanged="cmbUen_SelectedIndexChanged">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbUen"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                            ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Representante"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRik" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt2_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbRik" runat="server" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando...">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
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
                                        <asp:Label ID="Label10" runat="server" Text="Segmento"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtSegmento" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt3_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbSegmento" runat="server" OnClientSelectedIndexChanged="cmb3_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando...">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
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
                                        <asp:HiddenField ID="HF_ID" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" AutoPostBack="True"
                                            OnCheckedChanged="chkActivo_CheckedChanged" />
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
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <table>
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
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            OnNeedDataSource="rgDet_NeedDataSource" OnItemCommand="rgDet_ItemCommand" OnPageIndexChanged="rgDet_PageIndexChanged"
                                            Height="206px" PageSize="5" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" />
                                            </ClientSettings>
                                            <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No se encontraron registros.">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Año" UniqueName="Anyo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Anyo") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold1" runat="server" Text='<%# Bind("Anyo") %>' Visible="false" />
                                                            <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" MaxLength="4" Text='<%# Bind("Anyo") %>'
                                                                MinValue="1900">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Mes" UniqueName="Mes">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("MesStr") %>' />
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Mes") %>' Visible="false" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold2" runat="server" Text='<%# Bind("Mes") %>' Visible="false" />
                                                            <telerik:RadComboBox ID="RadComboBox1" runat="server" OnDataBinding="RadComboBox_DataBinding"
                                                                SelectedValue='<%# Bind("Mes") %>' />
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="180px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Presupuesto" UniqueName="Presupuesto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Presupuesto","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold3" runat="server" Text='<%# Bind("Presupuesto") %>' Visible="false" />
                                                            <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Text='<%# Bind("Presupuesto") %>'
                                                                MinValue="0" MaxLength="9">             
                                                                 <NumberFormat DecimalDigits="2" GroupSeparator="" />                                                   
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UpdateText="Aceptar" EditText="Editar"
                                                        UniqueName="EditCommandColumn" CancelText="Cancelar" InsertText="Aceptar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="30px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg1_NeedDataSource" OnItemCommand="rg1_ItemCommand" OnPageIndexChanged="rg1_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Clave" UniqueName="Id_Ter">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="Descripcion">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_UEN" HeaderText="Id_UEN" UniqueName="Id_UEN"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Rik" HeaderText="Id_Rik" UniqueName="Id_Rik"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Seg" HeaderText="Id_Seg" UniqueName="Id_Seg"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                                Visible="false">
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
                LimpiarTextBox($find('<%= txtUen.ClientID %>'));
                LimpiarTextBox($find('<%= txtRik.ClientID %>'));
                LimpiarTextBox($find('<%= txtSegmento.ClientID %>'));

                LimpiarComboSelectIndex0($find('<%= cmbUen.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbRik.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbSegmento.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {



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


                        var txtIdPrecio = $find('<%= txtClave.ClientID %>');
                        txtIdPrecio.enable();
                        txtIdPrecio.focus();
                        txtIdPrecio.set_value('<%= Valor %>');
                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbUen.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtUen.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRik.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRik.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbSegmento.ClientID %>'));
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtSegmento.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
