<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatMovimientos.aspx.cs" Inherits="SIANWEB.CatMovimientos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script language="javascript">
      function SoloNumeros(evt) {
          var charCode = (evt.which) ? evt.which : event.keyCode
          if (charCode > 31 && (charCode < 48 || charCode > 57))
              return false;

          return true;
      }
   </script>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbCobranza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmbTipo" />
                    <telerik:AjaxUpdatedControl ControlID="cmbNaturaleza" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbInventario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmbTipo" />
                    <telerik:AjaxUpdatedControl ControlID="cmbNaturaleza" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="txtInverso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="cmbInverso" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbInverso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtInverso" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgMovimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
   
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="True" dir="rtl"
            Width="100%" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" CssClass="mail" ToolTip="Correo" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false" />
                <telerik:RadToolBarButton CommandName="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="save" ToolTip="Guardar" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
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
                    <asp:Label ID="Label7" runat="server" Text="Centro de distribución" Visible = "false"></asp:Label>
                </td>
               <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" Visible= "false">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
      <div id="divPrincipal" runat="server">
         <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                SelectedIndex="0" TabIndex="-1">
                <Tabs>
                    <telerik:RadTab PageViewID="TdTM" Text="<u>T</u>ipos de movimiento " 
                        AccessKey="T" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="TdAC" Text="<u>A</u>plicación contable" 
                        AccessKey="A">
                    </telerik:RadTab>
     
                </Tabs>
            </telerik:RadTabStrip>
    

          <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                Width="800px">
                  <telerik:RadPageView ID="TdTM" runat="server" BorderStyle="Solid" 
                      BorderWidth="1px" Selected="True">
               
          <table> 
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: verdana; font-size: 8pt">
                        <tr>
                            <td colspan="5">
                                <asp:RadioButton ID="rbCobranza" runat="server" Checked="True" Text="Cobranza" AutoPostBack="True"
                                    OnCheckedChanged="rb_CheckedChanged" GroupName="MovNaturaleza" 
                                    Enabled="False" />
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="rbInventario" runat="server" Text="Inventario" AutoPostBack="True"
                                    OnCheckedChanged="rb_CheckedChanged" GroupName="MovNaturaleza" 
                                    Enabled="False" />
                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
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
                                <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="155px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    LoadingMessage="Cargando...">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RfvTipo" runat="server" ControlToValidate="cmbTipo"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="200px"
                                    MaxLength="40">
                                    <ClientEvents OnKeyPress="SoloAlfabetico" />
                                </telerik:RadTextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RfvNombre" runat="server" ControlToValidate="txtNombre"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="txtNumero" runat="server" MinValue="1" Width="70px"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RfvClave" runat="server" ControlToValidate="txtNumero"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Naturaleza"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbNaturaleza" runat="server" AllowCustomText="False" Width="150px"
                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RfvNaturaleza" runat="server" ControlToValidate="cmbNaturaleza"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Movimiento inverso"></asp:Label>
                            </td>
                            <td colspan="1">
                                <telerik:RadNumericTextBox ID="txtInverso" runat="server" MinValue="0" Width="70px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt_OnBlur" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="cmbInverso" runat="server" OnClientSelectedIndexChanged="cmb_ClientSelectedIndexChanged"
                                    Width="300px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
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
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trAfecta" runat="server" visible="false">
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Afecta"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbAfecta" runat="server" Width="150px" EmptyMessage="-- Seleccionar --"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RfvAfecta" runat="server" ControlToValidate="cmbAfecta"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="trCobranza">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkVenta" runat="server" Text="Afecta venta" />
                                <br />
                                <asp:CheckBox ID="chkIva" runat="server" Text="Afecta IVA" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="trInventario" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkOrden" runat="server" Text="Afecta orden de compra" />
                                <br />
                                <asp:CheckBox ID="chkReqRef" runat="server" Text="Requiere referencia" />
                                <br />
                                <asp:CheckBox ID="chkReqSpo" runat="server" Text="Requiere sistema de propietarios" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HFId_Mov" runat="server" />
                                <asp:HiddenField ID="HFNatMov" runat="server" />
                            </td>
                            <td width="70px">
                                &nbsp;
                            </td>
                            <td width="80px">
                                &nbsp;
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgMovimiento" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="rgMovimiento_ItemCommand"
                        PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnPageIndexChanged="rgMovimiento_PageIndexChanged">
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id" HeaderText="Clave" UniqueName="Id">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" UniqueName="Tipo" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Naturaleza" HeaderText="Naturaleza" UniqueName="Naturaleza"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Inverso" HeaderText="Inverso" UniqueName="Inverso"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AfeIVA" HeaderText="AfeIVA" UniqueName="AfeIVA"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AfeVta" HeaderText="AfeVta" UniqueName="AfeVta"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AfeOrdComp" HeaderText="AfeOrdComp" UniqueName="AfeOrdComp"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Afecta" HeaderText="Afecta" UniqueName="Afecta"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ReqSispropietario" HeaderText="ReqSispropietario"
                                    UniqueName="ReqSispropietario" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ReqReferencia" HeaderText="ReqReferencia" UniqueName="ReqReferencia"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NatMov" HeaderText="NatMov" UniqueName="NatMov"
                                    Visible="false">
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
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                    <br />
                </td>
            </tr>
        </table>
           </telerik:RadPageView>
             <telerik:RadPageView ID="TdAC" runat="server" BorderStyle="Solid" BorderWidth="1px">
              <table style="font-family: verdana; font-size: 8pt">
                        <tr>
                        <td> &nbsp;</td>
                            <td colspan="5" class="style1">
                                <asp:RadioButton ID="RbtnTAc_NatMov0" runat="server" Checked="True" 
                                    Text="Cobranza" AutoPostBack="True"
                                    GroupName="MovNaturaleza2" Enabled="False" />
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="RbtnTAc_NatMov1" runat="server" Text="Inventario" AutoPostBack="True"
                                  GroupName="MovNaturaleza2" Enabled="False" />
                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                        </tr>
                            <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
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
                         <td> &nbsp;</td>
                          <td> <asp:Label ID="Label8" runat="server" Text="Naturaleza"></asp:Label></td>
                         <td>
                               <telerik:RadComboBox ID="cmbNaturaleza2" runat="server" AllowCustomText="False" Width="150px"
                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" TabIndex="1">
                                </telerik:RadComboBox>
                                </td>
                        </tr>
                    
                        <tr>
                        <td> &nbsp;</td>
                            <td>
                                <asp:Label ID="LblId_TmAc" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadNumericTextBox ID="TxtId_TmAc" runat="server" MaxLength="9" 
                                    MinValue="1" Width="100px" TabIndex="2">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                  &nbsp;&nbsp;
                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                                                ToolTip="Buscar" ValidationGroup="buscar" onclick="imgBuscar_Click" 
                                   />
                                   &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="RfvTxtId_TmAc" runat="server" 
                                    ControlToValidate="TxtId_TmAc" Display="Static" ErrorMessage="*Requerido" 
                                    ForeColor="Red" ValidationGroup="guardarac"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                        <td> </td>
                            <td class="style2">
                                <asp:Label ID="LblTm_Nombre" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td colspan="4" class="style2">
                           <asp:Label ID="TxtTm_Nombre" runat="server" ></asp:Label></td>
                          
                        </tr>
                          <tr>
                          <td> &nbsp;</td>
                            <td>
                              
                                &nbsp;</td>
                            <td colspan="2">
                                  <asp:Label ID="Label10" runat="server" Text="Cargo" Width="100px" Font-Bold="True" ></asp:Label>
                                
                                 &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                <asp:Label ID="Label12" runat="server" Text="Crédito" Width="100px" Font-Bold="True"></asp:Label>

                                &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                   <asp:Label ID="Label13" runat="server" Text="Variación" Width="100px" Font-Bold="True"></asp:Label>
                                
                                </td>
                            <td >
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                          <td> &nbsp;</td>
                            <td>
                              <asp:Label ID="LblTAc_Cuenta" runat="server" Text="Cuenta"></asp:Label>
                                &nbsp;</td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="TxtTAc_Cuenta" runat="server" MaxLength="4" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="3">
                                
                                </telerik:RadTextBox> &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                <telerik:RadTextBox ID="TxtTAc_CuentaA" runat="server" MaxLength="4" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="6">
                                </telerik:RadTextBox>

                                &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                     <telerik:RadTextBox ID="TxtTAc_CuentaB" runat="server" MaxLength="4" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="9">
                                </telerik:RadTextBox>
                                
                                </td>
                            <td >
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                          <td> &nbsp;</td>
                            <td>
                            <asp:Label ID="LblAc_Subcuenta" runat="server" Text="Subcuenta"></asp:Label>
                               </td>
                            <td colspan="2">
                                    <telerik:RadTextBox ID="TxtAc_Subcuenta" runat="server" MaxLength="5" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="4">
                                
                                </telerik:RadTextBox>
                                &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                <telerik:RadTextBox ID="TxtAc_SubcuentaA" runat="server" MaxLength="5" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="7">
                                     </telerik:RadTextBox>

                                     &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                     <telerik:RadTextBox ID="TxtTAc_SubCuentaB" runat="server" MaxLength="5" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="10">
                                </telerik:RadTextBox>

                                    </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                          <tr>
                          <td> &nbsp;</td>
                            <td>
                            <asp:Label ID="LblAc_SubSubCuenta" runat="server" Text="Subsubcuenta"></asp:Label>
                               </td>
                            <td colspan="2">
                                    <telerik:RadTextBox ID="TxtAc_Subsubcuenta" runat="server" MaxLength="2" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="5">
                                
                                </telerik:RadTextBox>
                                  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                <telerik:RadTextBox ID="TxtAc_SubsubcuentaA" runat="server" MaxLength="2" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="8">
                                     </telerik:RadTextBox>

                                     &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;  &nbsp; &nbsp;&nbsp; 
                                     <telerik:RadTextBox ID="TxtTAc_SubsubCuentaB" runat="server" MaxLength="2" onkeypress="return SoloNumeros(event)"
                                    MinValue="1" Width="100px" TabIndex="11">
                                </telerik:RadTextBox>

                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                          <td> &nbsp;</td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Naturaleza"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="CmbAc_Naturaleza" runat="server" DataTextField="Descripcion" 
                                    Filter="Contains" LoadingMessage="Cargando..." MarkFirstMatch="True" 
                                    Width="150px" TabIndex="12">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Selected="True" 
                                            Text="-- Seleccionar --" Value="-1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Cargo" Value="0" />
                                        <telerik:RadComboBoxItem runat="server" Text="Crédito" Value="1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td >
                                <asp:RequiredFieldValidator ID="RfvCmbAc_Nauraleza" runat="server" 
                                    ControlToValidate="CmbAc_Naturaleza" Display="Dynamic" ErrorMessage="*Requerido" 
                                    ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardarac"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                            <tr>
                          <td> &nbsp;</td>
                            <td>
                              
                            </td>
                            <td colspan="2">
                       <asp:CheckBox ID="ChckApCC" runat="server" Text="Utiliza centro de costos" TabIndex="13"></asp:CheckBox>
                       
                            </td>
                            <td >
                                
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="tr1" runat="server" visible="false">
                          <td> &nbsp;</td>
                            <td>
                                <asp:HiddenField ID="HdId_TAc" runat="server" />
                            </td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>
                          <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>
                        <tr>
                        <td></td>
                        <td colspan ="4">
                         <telerik:RadGrid ID="GrdApContable" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                                    PageSize="15"  OnNeedDataSource="GrdApContable_NeedDataSource" MasterTableView-NoMasterRecordsText="No se encontraron registros"
                                    OnPageIndexChanged="GrdApContable_PageIndexChanged">
                                    <MasterTableView  TableLayout="Auto" AllowMultiColumnSorting="False"
                                        AllowNaturalSort="true" AllowSorting="true">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Tm" Visible="True" UniqueName="Id_Tm"
                                                HeaderText="Clave" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre"
                                                UniqueName="Tm_Nombre" HeaderStyle-HorizontalAlign="Center"
                                                AllowFiltering="false" HeaderStyle-Width="200px">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle ></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TAc_CuentaStr" Visible="True" UniqueName="TAc_CuentaStr"
                                                HeaderText="Cuenta" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                                AllowFiltering="false">
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TAc_SubcuentaStr" Visible="True" UniqueName="Tac_SubcuentaStr"
                                                HeaderText="Subcuenta" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                                AllowFiltering="false" ItemStyle-Width="150px" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="TAc_Subsubcuenta" Visible="True" UniqueName="Tac_Subsubcuenta"
                                                HeaderText="Subsubcuenta" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                                AllowFiltering="false" ItemStyle-Width="150px" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="TAc_Naturalezastr" Visible="True" UniqueName="TAc_Naturalezastr"
                                                HeaderText="Naturaleza contable" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                                AllowFiltering="false" ItemStyle-Width="150px" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="TAc_TipoStr" Visible="True" UniqueName="TAc_TipoStr"
                                                HeaderText="Naturaleza movimiento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                                AllowFiltering="false" ItemStyle-Width="100px" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_TAc" UniqueName="Id_TAc" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TAc_NatMov" Visible="False" UniqueName="TAc_NatMov">
                                               </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TAc_Naturaleza" Visible="False" UniqueName="TAc_Naturaleza">
                                               </telerik:GridBoundColumn>
                                        
                                        </Columns>
                                    </MasterTableView>
             
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
 
                        </td>
                        </tr>
                     
                       
                    </table>
             </telerik:RadPageView>
  </telerik:RadMultiPage>
   <asp:HiddenField id= "HF_ClvPag" runat ="server"/>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function txt_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbInverso.ClientID %>'));
            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtInverso.ClientID %>'));
            }


            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtNombre.ClientID %>'));
                LimpiarTextBox($find('<%= txtNumero.ClientID %>'));
                LimpiarTextBox($find('<%= txtInverso.ClientID %>'));
                LimpiarTextBox($find('<%= TxtId_TmAc.ClientID %>'));
                LimpiarTextBox($find('<%= TxtTAc_Cuenta.ClientID %>'));
                LimpiarTextBox($find('<%= TxtAc_Subcuenta.ClientID %>'));
                document.getElementById("TxtTm_Nombre").innerText = "[Nombre movimiento]"; 

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);

                if (document.getElementById('<%= chkVenta.ClientID %>') != null) {
                    LimpiarCheckBox(document.getElementById('<%= chkVenta.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkIva.ClientID %>'));
                } else {
                    LimpiarCheckBox(document.getElementById('<%= chkOrden.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkReqRef.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkReqSpo.ClientID %>'));
                }

                LimpiarComboSelectIndex0($find('<%= cmbTipo.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbNaturaleza.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbInverso.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= CmbAc_Naturaleza.ClientID %>'));

                if ($find('<%= cmbAfecta.ClientID %>') != null) {
                    LimpiarComboSelectIndex0($find('<%= cmbAfecta.ClientID %>'));
                }
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

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HFId_Mov.ClientID %>');
                        hiddenActualiza.value = '';

                        var radio = document.getElementById('<%= HFNatMov.ClientID %>');

                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtNumero.ClientID %>');
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatTMovimiento";
                        parametros = parametros + "&sp=spCatCentral_MaximoMov";
                        parametros = parametros + "&columna=Id_Tm";
                        parametros = parametros + "&naturaleza=" + radio.value;
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        txtId.enable();
                        txtId.focus();

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
        .style2
        {
            height: 17px;
        }
    </style>
</asp:Content>

