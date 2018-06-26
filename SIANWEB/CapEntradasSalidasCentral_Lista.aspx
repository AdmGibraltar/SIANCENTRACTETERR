<%@ Page Title="Entradas y salidas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapEntradasSalidasCentral_Lista.aspx.cs" Inherits="SIANWEB.CapEntradasSalidasCentral_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">
       function TxtId_Alm_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= CmbId_Alm.ClientID %>'));
       }
       function TxtId_Tm_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= cmbTipoMovimiento.ClientID %>'));
       }
       function CmbId_Tm_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Tm.ClientID %>'));
       }
       function CmbId_Alm_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Alm.ClientID %>'));
       }

       function OpenWindow(Id_Emp, Id_Alm, Id_MovC, Id_Tm, MovC_Naturaleza) {
         
               AbrirVentana_Detalle(Id_Emp, Id_Alm, Id_MovC, Id_Tm, MovC_Naturaleza);
       }
       function AbrirVentana_Detalle(Id_Emp, Id_Alm, Id_MovC, Id_Tm, MovC_Naturaleza) 
       {
           //debugger;
           var oWnd = radopen("CapEntSalCentral.aspx?Id_Emp=" + Id_Emp + "&Id_Alm=" + Id_Alm + "&Id_MovC=" + Id_MovC + "&Id_Tm=" + Id_Tm + "&Nat=" + MovC_Naturaleza, "AbrirVentana_EntSalCentral");
           oWnd.center();
           oWnd.Maximize();
       }

       //********************************
       //refrescar grid
       //********************************
       function refreshGrid() {
           //debugger;
           var ajaxManager = $find("<%= RAM1.ClientID %>");
           ajaxManager.ajaxRequest('RebindGrid');
       }

       function confirmCallBackFnVI(arg) {
           var ajaxManager = $find("<%= RAM1.ClientID %>");
           if (arg) {

               ajaxManager.ajaxRequest('ok');
           }
           else {
               ajaxManager.ajaxRequest('no');
           }
       }


   </script>
     
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EventName="RAM1_AjaxRequest" 
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbId_Alm" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                 <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTipoMovimiento" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMovimientos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtTotalFac" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtTotalEst" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtTotVariacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
          <telerik:AjaxSetting AjaxControlID="rgMovimientos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMovimientos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMovimientos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
          
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick" >
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="print" CssClass="print" ToolTip="Imprimir listado"
                    ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <div>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
            
                </td>
                <td width="150px" style="font-weight: bold">
                <asp:HiddenField id= "HF_ClvPag" runat ="server"/>
             
                </td>
            </tr>
            <tr>
            <td>
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            </td>
            </tr>
        </table>
        <div id="divPrincipal" runat="server">
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td width="110">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td width="80">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                </td>
                                <td width="10">
                                </td>
                                <td width="45">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <asp:Label ID="LblId_Alm" runat="server" Text="Almacén"></asp:Label></td>
                                <td >
                                   
                                    <telerik:RadNumericTextBox ID="TxtId_Alm" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                                       <ClientEvents OnBlur="TxtId_Alm_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>
                                   </td>
                                   <td>
                                   </td>
                                   <td colspan = "9">
                                    <telerik:RadComboBox ID="CmbId_Alm" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px" OnClientBlur="Combo_ClientBlur" 
                                                         OnClientSelectedIndexChanged="CmbId_Alm_ClientSelectedIndexChanged"
                                                        Width="300px">
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

                            </tr>
                            <tr>
                                <td>
                                 <asp:Label ID="LblId_Tm" runat="server" Text="Tipo de movimiento"></asp:Label>
                                </td>
                                <td>
                               <telerik:RadNumericTextBox ID="TxtId_Tm" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                 <ClientEvents OnBlur="TxtId_Tm_OnBlur"  OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                                </td>  
                                <td>
                                </td>
                                <td colspan ="9">
                                 <telerik:RadComboBox ID="cmbTipoMovimiento" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px"   OnClientBlur="Combo_ClientBlur"
                                                         OnClientSelectedIndexChanged="CmbId_Tm_ClientSelectedIndexChanged"
                                                        Width="300px">
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
                                              
                            </tr>
                            <tr>
                                <td>
                                     <asp:Label ID="LblReferencia" runat="server" Text="Referencia"></asp:Label></td>
                                <td>
                                    <telerik:RadTextBox ID="TxtReferencia" runat="server" Width="70px"
                                        MaxLength="9">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LblMovC_Naturaleza" runat="server" Text="Naturaleza"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="CmbMovC_Naturaleza" runat="server" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Selected="True" 
                                                Text=" -- Seleccionar --" Value="-1" />
                                                 <telerik:RadComboBoxItem runat="server" Selected="True" 
                                                Text="Entrada" Value="0" />
                                                 <telerik:RadComboBoxItem runat="server" Selected="True" 
                                                Text="Salida" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblId_MovCIni" runat="server" Text="Movimiento inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="TxtId_MovCIni" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9" >
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                     
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LblId_MovCFin" runat="server" Text="Movimiento final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="TxtId_MovCFin" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:ImageButton ID="BtnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="BtnBuscar_Click"  />
                                </td>
                                <td colspan="2">
                                    &nbsp;</td>
                                 <td>
                                </td>
                                 <td>
                                     &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server"  Width="900px" Font-Size="Smaller">
                            <telerik:RadGrid ID="rgMovimientos" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" 
                                PageSize="10" 
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand = "rg_ItemCommand"
                                Width="900px"  onitemdatabound="rgMovimientos_ItemDataBound"  >
                                <MasterTableView ClientDataKeyNames="Id_MovC">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Alm" HeaderText="Id_Alm" UniqueName="Id_Alm"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="Id_Tm" UniqueName="Id_Tm"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MovC_Naturaleza" HeaderText="MovC_Naturaleza" UniqueName="MovC_Naturaleza"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_MovC" HeaderText="Número" UniqueName="Id_MovC"
                                            Display="true">
                                            </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Almacen" HeaderText="Almacén" UniqueName="Almacen">
                                            <HeaderStyle Width="270px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TipoMov" HeaderText="Tipo movimiento" UniqueName="TipoMov">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MovC_Fecha" HeaderText="Fecha" UniqueName="MovC_Fecha"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MovC_Referencia" HeaderText="Referencia" UniqueName="MovC_Referencia">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalFac" HeaderText="Total"  UniqueName="TotalFac"
                                            >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalCostoEst"  HeaderText="Acum. recepciones" UniqueName="TotalCostoEst"
                                            DataFormatString="{0:N2}" > 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Variacion" HeaderText="Variación compra" UniqueName="Variacion"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                               
                                        <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" Text="Editar" 
                                            UniqueName="Editar" Display="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" 
                                          
                                           Text="Imprimir" UniqueName="Imprimir" Display="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                   
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                       
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table width="900px" cellspacing="3"   style="font-family: Verdana; font-size: 10pt;">
              <tr>
               <td>
             
               </td>
               <td align ="right" width= "80%">
         <asp:Label ID="LblTotalFac" Font-Bold = "true" runat="server" Text="Total"></asp:Label>
               </td>
               <td align ="right">
                   <asp:Label ID= "TxtTotalFac" runat ="Server" ></asp:Label>
               </td>
               <td>
               </td>
               </tr>
                  <tr>
               <td>
             
               </td>
               <td align ="right" width= "80%">
         <asp:Label ID="LblTotalEst" Font-Bold = "true" runat="server" Text="Total acum. recepciónes"></asp:Label>
               </td>
               <td align ="right">
                   <asp:Label ID= "TxtTotalEst" runat ="Server" ></asp:Label>
               </td>
               <td>
               </td>
               </tr>
                 <tr>
               <td>
             
               </td>
               <td align ="right" width= "80%">
         <asp:Label ID="LblTotVariacion" Font-Bold = "true" runat="server" Text="Total variación compra"></asp:Label>
               </td>
               <td align ="right">
                   <asp:Label ID= "TxtTotVariacion" runat ="Server" ></asp:Label>
               </td>
               <td>
               </td>
               </tr>
            </table>
        </div>
    </div>
</asp:Content>
