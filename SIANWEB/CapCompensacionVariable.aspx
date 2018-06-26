<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapCompensacionVariable.aspx.cs" Inherits="SIANWEB.CapCompensacionVariable" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
            background-image: url('images/loading.gif');
            background-repeat: no-repeat;
            background-position: center;
            border-radius: .6em;
        }
    </style>
    <style>
        #example .demo-container
        {
            width: 860px;
        }
        
        
        .demo-container .wrapper
        {
            float: left;
        }
        
        .demo-container .treeViewWrapper
        {
            width: 220px;
        }
        
        
        .demo-container span.label
        {
            color: #4888A3;
            padding: 4px 4px;
            display: block;
            margin-bottom: 7px;
        }
    </style>
    <style type="text/css">
        .mytable
        {
            border-collapse: collapse;
            width: 100%;
            background-color: white;
        }
        .mytable-head
        {
            margin-bottom: 1;
            padding-bottom: 1;
        }
        
        .mytable-body
        {
            border-top: 0;
            margin-top: 0;
            padding-top: 0;
            margin-bottom: 1;
            padding-bottom: 1;
        }
        .mytable-body td
        {
            border-top: 0;
        }
        
        .rbText {
    padding-left: 5px;
}


        div.qsf-right-content .qsf-col-wrap 
        {
            position: static;
        }
 
        .rgEditForm {
            width: auto !important;
        }
 
        * + html .rgEditForm.popUpEditForm {
            width: 800px !important;
        }
 
        .rgEditForm > div + div,
        .RadGrid .rgEditForm {
            height: auto !important;
        }
 
        .rgEditForm > div > table{
            height: 100%;
     
        }
 
        .rgEditForm > div > table > tbody > tr > td {
            padding: 4px 10px;
        }
 
        .rfdSelectBoxDropDown {
            z-index: 100011;
        }

    </style>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        <div id="PopUpBackground">
        </div>
        <div id="PopUpProgress">
            <h6>
                <p style="text-align: center;">
                    <b>Favor de Esperar...</b></p>
            </h6>
        </div>
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
            <telerik:AjaxSetting AjaxControlID="txtProveedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkConComprobante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--JFCV agregue este botón --%>
            <telerik:AjaxSetting AjaxControlID="BtnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRaya">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="buttonSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="UploadButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divUpload" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="FileUploadControl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divUpload" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grVariables">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

     
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="true" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Grabar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_FormulaPaso" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenId" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="divCtaGastos" runat="server">
            <table class="mytable mytable-head">
                <tr>
                    <td width="10%">
                        <asp:Label ID="lblNombreDel" runat="server" Text="Nombre del sistema"></asp:Label>
                    </td>
                    <td width="30%">
                        <telerik:RadTextBox ID="txtNombreSistema" runat="server" Width="300px" MaxLength="100">
                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                        </telerik:RadTextBox>
                    </td>
                    <td width="13%">
                        <asp:Label ID="Label2" runat="server" Text="Folio Sistema"></asp:Label>
                    </td>
                    <td width="52%">
                        <telerik:RadTextBox ID="txtFolioSistema" runat="server" Width="60px" MaxLength="6"
                            ReadOnly="true">
                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                        </telerik:RadTextBox>
                    </td>
                    <td width="27%">
                    </td>
                </tr>
            </table>
            <table class="mytable mytable-body">
                <tr>
                    <td width="10%">
                        <asp:Label ID="lblPerfil" runat="server" Text="Perfil" Visible="true"></asp:Label>
                    </td>
                    <td width="30%">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="CmbPerfil" runat="server" CheckBoxes="true"
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
                    <td width="17%">
                    </td>
                    <td width="33%">
                    </td>
                    <td width="10%">
                    </td>
                </tr>
            </table>
            <table class="mytable mytable-body">
                <tr>
                    <td width="10%">
                        <asp:Label ID="Label7" runat="server" Text="Fecha Inicial"></asp:Label>
                    </td>
                    <td width="15%">
                        <telerik:RadDatePicker ID="txtFechaInicio" runat="server" Width="100px">
                            <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                ViewSelectorText="x">
                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy" />
                            </Calendar>
                            <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                    <td width="13%">
                    </td>
                    <td width="20%">
                    </td>
                    <td width="42%">
                        
                    </td>
                </tr>
                <tr>
                    <td width="10%">
                        <asp:Label ID="Label1" runat="server" Text="Fecha Final"></asp:Label>
                    </td>
                    <td width="15%">
                        <telerik:RadDatePicker ID="txtFechaFin" runat="server" Width="100px">
                            <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                ViewSelectorText="x">
                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy" />
                            </Calendar>
                            <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                     <td width="13%">
                     <asp:CheckBox ID="chkEstatus" runat="server" Text="Sistema Activo" Checked="true"
                            Visible="true" />
                    </td>
                    <td width="20%">
                    </td>
                    <td width="42%">
                       
                    </td>
                </tr>
            </table>
            <br />
            <%--Captura de Variables de usuario --%>


             </asp:Panel>

             <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                SelectedIndex="0" TabIndex="-1">
                <Tabs>
                    <telerik:RadTab PageViewID="RadPageViewDGrales" Text="Conceptos Predefinidos " AccessKey="G"
                        Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewCompensaciones" Text="Estado Resultados Consolidado"
                        AccessKey="F">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewDetalle" Text="Detalle" Visible=false
                        AccessKey="D">
                    </telerik:RadTab>
                    

                  
                </Tabs>
            </telerik:RadTabStrip>
              <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                Width="100%">
                <!-- Aqui empieza el contenido de los tabs--->
                <telerik:RadPageView ID="RadPageViewDGrales" runat="server" BorderStyle="Solid" BorderWidth="1px">
                    <table >
                        <%-- Encabezados--%>
                        <tr>
                            <td style="width: 10%;">
                                <asp:Label ID="lblConcepto" runat="server" Text="Concepto" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 12%;">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" Visible="false"></asp:Label>
                            </td>
                             <td style="width: 12%;">
                                <asp:Label ID="lblComentarios" runat="server" Text="Comentarios" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 8%;">
                                <asp:Label ID="lblOperador" runat="server" Text="Operador" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 26%;">
                                <asp:Label ID="lblVariable" runat="server" Text="Variable" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 5%;">
                                <asp:Label ID="Label3" runat="server" Text="Valor" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:Button ID="imgBoton" runat="server" Text="Agregar" ToolTip="Actualizar" MaxHeight="50px"
                                    OnClick="btnAgregar_Click" Visible="false" />
                                       

                            </td>
                            <td style="width: 12%;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                <telerik:RadTextBox ID="txtConcepto" runat="server" Width="100px" MaxLength="30" Visible="false">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td style="width: 12%;">
                                <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="100px" MaxLength="100" Visible="false">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td style="width: 12%;">
                                <telerik:RadTextBox ID="txtComentarios" runat="server" Width="100px" MaxLength="150" Visible="false">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td style="width: 8%;">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="cmbOperador" runat="server" Width="50"
                                    Label="" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="+" Value="1" />
                                        <telerik:RadComboBoxItem Text="-" Value="2" />
                                        <telerik:RadComboBoxItem Text="*" Value="3" />
                                        <telerik:RadComboBoxItem Text="/" Value="4" />
                                        <telerik:RadComboBoxItem Text="%" Value="5" />
                                        <telerik:RadComboBoxItem Text="(" Value="6" />
                                        <telerik:RadComboBoxItem Text=")" Value="7" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 26%;">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="cmbVariables" runat="server" Width="300"
                                    Label="" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text=" " Value="0" />
                                        <telerik:RadComboBoxItem Text="(IVC) Importe de venta cobrada" Value="1" />
                                        <telerik:RadComboBoxItem Text="(UP) Utilidad prima" Value="2" />
                                        <telerik:RadComboBoxItem Text="(ASP) Amortización de sistemas propietarios" Value="3" />
                                        <telerik:RadComboBoxItem Text="(GTS) Gastos de tecnico de servicio" Value="4" />
                                        <telerik:RadComboBoxItem Text="(AAER) Amortización anticipada de sistemas propietarios"
                                            Value="5" />
                                        <telerik:RadComboBoxItem Text="(MO) Mano de obra en proyectos" Value="6" />
                                        <telerik:RadComboBoxItem Text="(AEA) Amortización de equipos arrendados" Value="7" />
                                        <telerik:RadComboBoxItem Text="(FC) Factor por cobranza" Value="8" />
                                        <telerik:RadComboBoxItem Text="(UBC) Utilidad bruta del cliente ajustada por cobranza"
                                            Value="9" />
                                        <telerik:RadComboBoxItem Text="(FPPP) Factor por porcentaje de participacion ponderado"
                                            Value="10" />
                                        <%--<telerik:RadComboBoxItem Text="(CP) Comisión preliminar" Value="11" />--%>
                                        <telerik:RadComboBoxItem Text="(MVI) Multiplicador por venta incremental" Value="11" />
                                        <telerik:RadComboBoxItem Text="(CND) Comisiónes no devengadas (Factor 0)" Value="12" />
                                        <telerik:RadComboBoxItem Text="(SF) Sueldo fijo" Value="13" />
                                        <telerik:RadComboBoxItem Text="(TSF) Tope del sueldo fijo ( 40%)" Value="14" />
                                        <%--<telerik:RadComboBoxItem Text="(MVI) Multiplicador por venta incremental" Value="15" />--%>
                                        <telerik:RadComboBoxItem Text="(GA) Gasto administrativo" Value="16" />
                                        <telerik:RadComboBoxItem Text="(CB) Comision bruta (pago por nomina)" Value="17" />
                                        <telerik:RadComboBoxItem Text="Constante de Usuario" Value="15" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 5%;">
                                <telerik:RadTextBox ID="txtValor" runat="server" Width="50px" MaxLength="5" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                            <td style="width: 15%;">
                                <asp:Button ID="btnTerminar" runat="server" Text="Terminar" ToolTip="Terminar" MaxHeight="50px"
                                    OnClick="btnTerminar_Click"  Visible="false"/>
                            </td>
                            <td style="width: 12%;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                            </td>
                            <td style="width: 12%;">
                            </td>
                            <td style="width: 12%;">
                            </td>
                            <td style="width: 8%;">
                            </td>
                            <td style="width: 26%;">
                            </td>
                            <td style="width: 5%;">
                            </td>
                            <td style="width: 15%;">
                            </td>
                            <td style="width: 12%;">
                            </td>
                        </tr>
                    </table>
                    <br />
            <div class="wrapper treeViewWrapper" style="width: 850px; float: left;">
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="rgGrid" runat="server" CommandItemDisplay="Top" GridLines="None"
                                AutoGenerateColumns="False" OnNeedDataSource="rgGrid_NeedDataSource" OnInsertCommand="rgGrid_InsertCommand"
                                OnItemCommand="rgGrid_ItemCommand" EditMode="InPlace" OnItemDataBound="rgGrid_ItemDataBound"
                                OnUpdateCommand="rgGrid_UpdateCommand" PageSize="12" AllowPaging="True" DataMember="listaOrdCompraDet">
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_ConceptoVariable,Concepto_Descripcion,Concepto_Operador,Concepto_TipoVariable"
                                    EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                    NoMasterRecordsText="No se encontraron registros.">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"
                                        RefreshText="Actualizar" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Renglón" DataField="Id_ConceptoVariable"
                                            UniqueName="Id_ConceptoVariable" ReadOnly="true" Display="true">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblId_GVComprobante" runat="server" Text='<%# Eval("Id_ConceptoVariable").ToString() %>'
                                                    ReadOnly="true" Display="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Concepto" DataField="Concepto_Descripcion"
                                            UniqueName="Concepto_Descripcion" Display="true">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_Descripcion" runat="server" Text='<%# Eval("Concepto_Descripcion").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Concepto_Observaciones"
                                            UniqueName="Concepto_Observaciones" Display="true">
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_Observaciones" runat="server" Text='<%# Eval("Concepto_Observaciones").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Operador" DataField="Concepto_Operador" UniqueName="Concepto_Operador">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_Operador" runat="server" Text='<%# Eval("Concepto_Operador").ToString() %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtConcepto_Operador" runat="server" Width="65px" MaxLength="100"
                                                    AutoPostBack="true" Text='<%# Eval("Concepto_Operador") %>'>
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtConcepto_Operador" runat="server" ForeColor="#FF0000" Text='<%# Eval("Concepto_Operador").ToString() %>'
                                                    Visible="false"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Concepto_TipoVariable" DataField="Concepto_TipoVariable"
                                            UniqueName="Concepto_TipoVariable" Display="false">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_TipoVariable" runat="server" Text='<%# Eval("Concepto_TipoVariable").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Concepto_IdVariable" DataField="Concepto_IdVariable"
                                            UniqueName="Concepto_IdVariable" Display="false">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_IdVariable" runat="server" Text='<%# Eval("Concepto_IdVariable").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Variable" DataField="Concepto_VariableDescripcion"
                                            UniqueName="Concepto_VariableDescripcion">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_VariableDescripcion" runat="server" Text='<%# Eval("Concepto_VariableDescripcion").ToString() %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtConcepto_VariableDescripcion" runat="server" Width="65px"
                                                    MaxLength="100" AutoPostBack="true" Text='<%# Eval("Concepto_VariableDescripcion") %>'>
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtConcepto_VariableDescripcion" runat="server" ForeColor="#FF0000"
                                                    Text='<%# Eval("Concepto_VariableDescripcion").ToString() %>' Visible="false"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar"
                                            HeaderText="Editar">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Eliminar" CommandName="Delete"
                                            ConfirmDialogType="RadWindow" ConfirmText="¿Desea eliminar el comprobante?" Text="Cancelar"
                                            UniqueName="DeleteColumn" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, reg. <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="15" />
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grVariables" runat="server" CommandItemDisplay="Top" GridLines="None"
                                AutoGenerateColumns="False" OnNeedDataSource="grVariables_NeedDataSource" OnInsertCommand="grVariables_InsertCommand"
                                OnItemCommand="grVariables_ItemCommand" EditMode="PopUp" OnItemDataBound="grVariables_ItemDataBound"
                                OnUpdateCommand="grVariables_UpdateCommand" PageSize="6" AllowPaging="True" DataMember="listaOrdCompraDet"
                                Display="true" >
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_VariableLocal,sVariable_Local,sVariable_Descripcion"
                                    DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                    NoMasterRecordsText="" EditMode="PopUp"> 
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="true"  AddNewRecordText="Agregar"/>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Variable" DataField="Id_VariableLocal" UniqueName="Id_VariableLocal"
                                            ReadOnly="true" Display="false">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblId_VariableLocal" runat="server" Text='<%# Eval("Id_VariableLocal").ToString() %>'
                                                    ReadOnly="true" Display="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Variable Desc" DataField="sVariable_Local"
                                            UniqueName="sVariable_Local" Display="true">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsVariable_Local" runat="server" Text='<%# Eval("sVariable_Local").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Descripción" DataField="sVariable_Descripcion"
                                            UniqueName="sVariable_Descripcion" Display="true">
                                            <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsVariable_Descripcion" runat="server" Text='<%# Eval("sVariable_Descripcion").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Comentarios" DataField="sVariable_Comentarios"
                                            UniqueName="sVariable_Comentarios" Display="true">
                                            <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsVariable_Comentarios" runat="server" Text='<%# Eval("sVariable_Comentarios").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Formula" DataField="sVariable_Formula"
                                            UniqueName="sVariable_Formula" Display="false">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsVariable_Formula" runat="server" Text='<%# Eval("sVariable_Formula").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar"
                                            HeaderText="Editar">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Eliminar" CommandName="Delete"
                                            ConfirmDialogType="RadWindow" ConfirmText="¿Desea eliminar la formula?" Text="Cancelar"
                                            UniqueName="DeleteColumn" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                        </telerik:GridButtonColumn>
                                    </Columns>

                                    <%--jfcv agregar edición on popup--%>
                                     <EditFormSettings UserControlName="CapCompensacionVariableDet.ascx" EditFormType="WebUserControl">
                                        <EditColumn UniqueName="EditCommandColumn1">
                                        </EditColumn>
                                    </EditFormSettings>
                                     </MasterTableView>

                                      <ClientSettings>
                                        <ClientEvents OnRowDblClick="RowDblClick" OnPopUpShowing="onPopUpShowing" />
                                    </ClientSettings>


                               

                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="15" />
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="wrapper treeViewWrapper" style="width: 230px; float: left;">
                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtArea" Width="250px"
                    EmptyMessage="Formulas" TextMode="MultiLine" Height="340px" Resize="None" ReadOnly="true">
                </telerik:RadTextBox><br />
                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtFormula" Width="150px"
                    EmptyMessage="Formulas" TextMode="MultiLine" Height="140px" Resize="None" ReadOnly="true">
                </telerik:RadTextBox><br />

               
            </div>
             </telerik:RadPageView>
             <telerik:RadPageView ID="RadPageViewCompensaciones" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                     <asp:Panel runat="server" ID="Panel1" Style="width: 990px; float: left;">
                        <div style="overflow: auto; height: 600px; width: 440px; float: left;">
                <%--<h3>Componentes</h3>--%>
                <telerik:RadTreeView RenderMode="Lightweight" ID="RadTreeView1" runat="server" EnableDragAndDrop="True"
                    OnNodeDrop="RadTreeView1_HandleDrop" OnClientNodeDropping="onNodeDropping" OnClientNodeDragging="onNodeDragging"
                    MultipleSelect="true" EnableDragAndDropBetweenNodes="true">
                    <Nodes>
                        <telerik:RadTreeNode runat="server" Text="Componentes" Expanded="true" AllowDrag="false"
                            AllowDrop="false">
                            <Nodes>
                                <telerik:RadTreeNode runat="server" Text="Conceptos Predefinidos" AllowDrag="false">
                                    <Nodes>
                                        <telerik:RadTreeNode runat="server" Text="(IVC) Importe de venta cobrada" AllowDrop="false"
                                            Value="1">
                                        </telerik:RadTreeNode>
                                        <telerik:RadTreeNode runat="server" Text="(UP) Utilidad prima" AllowDrop="false"
                                            Value="2">
                                        </telerik:RadTreeNode>
                                        <telerik:RadTreeNode runat="server" Text="(ASP) Amortización de sistemas propietarios"
                                            AllowDrop="false" Value="3">
                                        </telerik:RadTreeNode>
                                        <telerik:RadTreeNode runat="server" Text="(GTS) Gastos de tecnico de servicio" AllowDrop="false"
                                            Value="4" />
                                        <telerik:RadTreeNode runat="server" Text="(AAER) Amortización anticipada de sistemas propietarios"
                                            AllowDrop="false" Value="5" />
                                        <telerik:RadTreeNode runat="server" Text="(MO) Mano de obra en proyectos" AllowDrop="false"
                                            Value="6" />
                                        <telerik:RadTreeNode runat="server" Text="(AEA) Amortización de equipos arrendados"
                                            AllowDrop="false" Value="7" />
                                        <telerik:RadTreeNode runat="server" Text="(FC) Factor por cobranza" AllowDrop="false"
                                            Value="8" />
                                        <telerik:RadTreeNode runat="server" Text="(UBC) Utilidad bruta del cliente ajustada por cobranza"
                                            AllowDrop="false" Value="9" />
                                        <telerik:RadTreeNode runat="server" Text="(FPPP) Factor por porcentaje de participacion ponderado"
                                            AllowDrop="false" Value="10" />
                                       <%-- <telerik:RadTreeNode runat="server" Text="(CP) Comisión preliminar" AllowDrop="false"
                                            Value="11" />--%>
                                         <telerik:RadTreeNode runat="server" Text="(MVI) Multiplicador por venta incremental" AllowDrop="false"
                                            Value="11" />
                                        <telerik:RadTreeNode runat="server" Text="(CND) Comisiónes no devengadas (Factor 0)"
                                            AllowDrop="false" Value="12" />
                                        <telerik:RadTreeNode runat="server" Text="(SF) Sueldo fijo" AllowDrop="false" Value="13" />
                                        <telerik:RadTreeNode runat="server" Text="(TSF) Tope del sueldo fijo ( 40%)" AllowDrop="false"
                                            Value="14" />
                                        <telerik:RadTreeNode runat="server" Text="(GA) Gasto administrativo" AllowDrop="false"
                                            Value="16" />
                                        <telerik:RadTreeNode runat="server" Text="(CB) Comision bruta (pago por nomina)"
                                            AllowDrop="false" Value="17" />
                                        <telerik:RadTreeNode runat="server" Text="Constante de Usuario" AllowDrop="false"
                                            Value="15" />
                                    </Nodes>
                                </telerik:RadTreeNode>
                                <telerik:RadTreeNode runat="server" Text="Formulas" AllowDrag="false">
                                    <Nodes>
                                    </Nodes>
                                </telerik:RadTreeNode>
                            </Nodes>
                        </telerik:RadTreeNode>
                    </Nodes>
                </telerik:RadTreeView>
            </div>
                        <div style="overflow: auto; height: 600px; width: 440px; float: left;">

                         
                 
                            <asp:Button ID="btnRaya" runat="server" Text="__" ToolTip="Agregar Raya" MaxHeight="50px"
                                OnClick="btnRaya_Click" />
                            <asp:Button ID="btnNegrita" runat="server" Text="N" ToolTip="Negrita" MaxHeight="50px"
                                OnClick="btnNegrita_Click" Visible="false" />
                            <asp:Button ID="Button1" runat="server" Text="Agregar" ToolTip="Agregar un renglón"
                                OnClick="btnRenglon_Click" />
                            <telerik:RadTextBox ID="txtRenglon" runat="server" Width="130px" MaxLength="25">
                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                            </telerik:RadTextBox>
            

                <%-- <h3>Resultado</h3>--%>
                <telerik:RadTreeView RenderMode="Lightweight" ID="RadTreeView2" runat="server" EnableDragAndDrop="True"
                    OnNodeDrop="RadTreeView1_HandleDrop" OnClientNodeDropping="onNodeDropping" OnClientNodeDragging="onNodeDragging"
                    MultipleSelect="true" EnableDragAndDropBetweenNodes="true" ShowLineImages="False">
                    <Nodes>
                        <telerik:RadTreeNode runat="server" Text="Estado de Resultados Consolidado" Expanded="true"
                            AllowDrag="false" AllowDrop="false">
                            <Nodes>
                            </Nodes>
                        </telerik:RadTreeNode>
                    </Nodes>
                </telerik:RadTreeView>
            </div>
                    </asp:Panel>

           </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageViewDetalle" runat="server" BorderStyle="Solid"
                    BorderWidth="1px" Visible=false>
                     <asp:Panel runat="server" ID="Panel2" Style="width: 890px; float: left;">
                         
                         <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server"  AutoGenerateColumns="false"
            AllowSorting="true" GroupingEnabled="false"
            EnableHeaderContextMenu="true" AllowPaging="true" PageSize="10" OnNeedDataSource="RadGrid1_NeedDataSource">
            <MasterTableView TableLayout="Fixed">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="Pag_Referencia" HeaderText="Factura" HeaderStyle-Width="70px" DataField="Pag_Referencia" Display="true"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Id_Cte" HeaderText="Cliente" HeaderStyle-Width="50px" DataField="Id_Cte" Display="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Id_Ter" HeaderText="Territorio" HeaderStyle-Width="50px" DataField="Id_Ter" Display="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Id_RIK" HeaderText="RIK" HeaderStyle-Width="50px" DataField="Id_RIK" Display="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Vencimiento" HeaderText="Fecha Venc." HeaderStyle-Width="150px" DataField="Vencimiento"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="FechaPago" HeaderText="Fecha-Pago" HeaderStyle-Width="150" DataField="FechaPago"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Dias" HeaderText="Días" HeaderStyle-Width="50px" DataField="Dias"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Importe" HeaderText="Importe" HeaderStyle-Width="100px" DataField="Importe" Display="true"> <ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="UP" HeaderText="Utilidad Prima" HeaderStyle-Width="100px" DataField="UP" Display="true"> <ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Mult_Porc" HeaderText="Multiplicador de ajuste por cobranza" HeaderStyle-Width="100px" DataField="Mult_Porc" Display="true"> <ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="AjCobranza" HeaderText="Ajuste Por Cobranza " HeaderStyle-Width="100px" DataField="AjCobranza" Display="true"> <ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings ReorderColumnsOnClient="true" AllowColumnsReorder="true" ColumnsReorderMethod="Reorder">
                 
                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
        </telerik:RadGrid>
             
                    </asp:Panel>

           </telerik:RadPageView>
            
         </telerik:RadMultiPage>

    </div>
    <div class="demo-container no-bg size-wide" style="width: 860px;">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
            <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
            <script type="text/javascript">                jQuery.noConflict();</script>
            <script type="text/javascript">


                function CloseAlert(mensaje) {
                    var cerrarWindow = radalert(mensaje, 330, 150);
                    cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
                }

                function CloseWindow() {
                    GetRadWindow().Close();
                    //JFCV 18oct2016 que cierre la pantalla al grabar y si no graba que pregunte si desea salir control cambio 9
                    var oWnd = GetRadWindow();
                    oWnd.close();
                    top.location.href = top.location.href;
                }
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                    return oWindow;
                }



                function cmdClient_Focus(sender, args) {
                    var input = sender.get_inputDomElement();
                    sender.highlightAllMatches(input.defaultValue);
                    sender.showDropDown();
                }

                function RowDblClick(sender, eventArgs) {
                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                }

                function onPopUpShowing(sender, args) {
                    args.get_popUp().className += " popUpEditForm";
                }


            </script>
            <script type="text/javascript">
                (function () {
                    var demo = window.demo = window.demo || {};

                    function dropOnHtmlElement(args) {
                        if (droppedOnInput(args))
                            return;


                    }

                    function droppedOnInput(args) {
                        var target = args.get_htmlElement();
                        if (target.tagName === "INPUT") {
                            target.style.cursor = "default";
                            target.value = args.get_sourceNode().get_text();
                            args.set_cancel(true);
                            return true;
                        }
                    }

                    function dropOnTree(args) {
                        var text = "";

                        if (args.get_sourceNodes().length) {
                            var i;
                            for (i = 0; i < args.get_sourceNodes().length; i++) {
                                var node = args.get_sourceNodes()[i];
                                text = text + ', ' + node.get_text();
                            }
                        }
                    }

                    function clientSideEdit(sender, args) {
                        var destinationNode = args.get_destNode();

                        if (destinationNode) {
                            firstTreeView = demo.firstTreeView;
                            secondTreeView = demo.secondTreeView;

                            firstTreeView.trackChanges();
                            secondTreeView.trackChanges();
                            var sourceNodes = args.get_sourceNodes();
                            var dropPosition = args.get_dropPosition();

                            //Needed to preserve the order of the dragged items
                            if (dropPosition == "below") {
                                for (var i = sourceNodes.length - 1; i >= 0; i--) {
                                    var sourceNode = sourceNodes[i];
                                    sourceNode.get_parent().get_nodes().remove(sourceNode);

                                    insertAfter(destinationNode, sourceNode);
                                }
                            }
                            else {
                                for (var j = 0; j < sourceNodes.length; j++) {
                                    sourceNode = sourceNodes[j];
                                    sourceNode.get_parent().get_nodes().remove(sourceNode);

                                    if (dropPosition == "over")
                                        destinationNode.get_nodes().add(sourceNode);
                                    if (dropPosition == "above")
                                        insertBefore(destinationNode, sourceNode);
                                }
                            }
                            destinationNode.set_expanded(true);
                            firstTreeView.commitChanges();
                            secondTreeView.commitChanges();
                        }
                    }

                    function insertBefore(destinationNode, sourceNode) {
                        var destinationParent = destinationNode.get_parent();
                        var index = destinationParent.get_nodes().indexOf(destinationNode);
                        destinationParent.get_nodes().insert(index, sourceNode);
                    }

                    function insertAfter(destinationNode, sourceNode) {
                        var destinationParent = destinationNode.get_parent();
                        var index = destinationParent.get_nodes().indexOf(destinationNode);
                        destinationParent.get_nodes().insert(index + 1, sourceNode);
                    }

                    window.onNodeDragging = function (sender, args) {
                        var target = args.get_htmlElement();

                        if (!target) return;

                        if (target.tagName == "INPUT") {
                            target.style.cursor = "hand";
                        }


                    };

                    window.onNodeDropping = function (sender, args) {
                        var dest = args.get_destNode();
                        if (dest) {

                            clientSideEdit(sender, args);
                            args.set_cancel(true);
                            return;

                            dropOnTree(args);
                        }
                        else {
                            dropOnHtmlElement(args);
                        }
                    };
                } ());

            </script>
            <script type="text/javascript">
                Sys.Application.add_load(function () {

                    demo.firstTreeView = $find('<%= RadTreeView1.ClientID %>');
                    demo.secondTreeView = $find('<%= RadTreeView2.ClientID %>');
                });
            </script>
        </telerik:RadCodeBlock>
    </div>
</asp:Content>
