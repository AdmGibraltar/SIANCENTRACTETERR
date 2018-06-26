<%@ Page Title="Generación de póliza" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapGenerarPoliza.aspx.cs" Inherits="SIANWEB.GenerarPoliza" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">
       function TxtId_Alm_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= CmbId_Alm.ClientID %>'));
       }
 
    
       function CmbId_Alm_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Alm.ClientID %>'));
       }

       function TxtId_Tm_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= cmbTipoMovimiento.ClientID %>'));
       }
       function CmbId_Tm_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Tm.ClientID %>'));
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

       function refreshGrid() {
           var ajaxManager = $find("<%= RAM1.ClientID %>");
           ajaxManager.ajaxRequest('RebindGrid');
       }

   </script>
     
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server"  OnAjaxRequest="RAM1_AjaxRequest"
         EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1" >
                <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
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
            <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgMovimientos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMmovimientos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="Generar" Value="Generar" ToolTip="Generar póliza" CssClass="aceptarToolbar"
                ImageUrl="Imagenes/blank.png"  />
            <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir vista previa" CssClass="print"
                    ImageUrl="Imagenes/blank.png"  />
                
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
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;" cellspacing = "8">
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
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                </td>
                                              
                            </tr>
                        
                       
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px" >
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
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final" ></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px" >
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
                                    &nbsp;</td>
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
                                 <asp:Label ID="Label3" runat="server" Text="Tipo póliza"></asp:Label>
                                  
                                </td>
                                <td colspan = "4">
                                <asp:RadioButtonList ID="RdblTipo" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">Resumen</asp:ListItem>
                                    <asp:ListItem Value="1">Detalle</asp:ListItem>
                                </asp:RadioButtonList>
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
             
                    </td>
                </tr>

             
               
            </table>
            <br />
            <table width="900px" cellspacing="3"   style="font-family: Verdana; font-size: 10pt;">
              <tr>
              <td>
              </td>

               <td width="90%">
             <asp:Label ID="Label4" runat="server" Text="Histórico de polizas" 
                       Font-Bold="True"></asp:Label>
               </td>
               <td align ="left" >
                  </td>
               <td align ="right">
                   &nbsp;</td>
               <td>
               </td>
               </tr>
               <tr>
                <td>
              </td>
                 <td width="90%">
                 <asp:Label ID="Label10" runat="server" Text="Filtros" ></asp:Label>
                  </td
                   <td align ="left" >
                 
                  </td>
               <td align ="right">
                   &nbsp;</td>
               </tr>
                  <tr>
                <td>
              </td>
                 <td width="90%">
                 <asp:Label ID="Label5" runat="server" Text="Año" ></asp:Label>
                 &nbsp; &nbsp; &nbsp; &nbsp;
                    <telerik:radcombobox id="cmbAnio" runat="server" width="120px" filter="Contains" changetextonkeyboardnavigation="true" markfirstmatch="true" loadingmessage="Cargando..."
                        highlighttemplateditems="true" datatextfield="Descripcion" datavaluefield="Id"  MaxHeight="250px"
                         >                           
                        </telerik:radcombobox >
                  </td
                   <td align ="left" >
                 
                  </td>
               <td align ="right">
                   &nbsp;</td>
               </tr>
                       <tr>
                <td>
              </td>
                 <td width="90%">
                 <asp:Label ID="Label6" runat="server" Text="Mes" ></asp:Label>
                 &nbsp; &nbsp; &nbsp; &nbsp;
                    <telerik:radcombobox id="cmbMes" runat="server" width="120px" filter="Contains"
                            changetextonkeyboardnavigation="true" markfirstmatch="true" loadingmessage="Cargando..."
                            highlighttemplateditems="true" datatextfield="Descripcion" datavaluefield="Id" MaxHeight="250px"
                            >                           
                            </telerik:radcombobox>
        &nbsp; &nbsp; &nbsp; &nbsp;
                               <asp:ImageButton ID="BtnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="BtnBuscar_Click1"  />
                  </td
                   <td align ="left" >
                 
                  </td>
               <td align ="right">
                   &nbsp;</td>
               </tr>
                
                  <tr>
               <td>
             
               </td>
               <td  align="left">
                       <telerik:RadGrid ID="rgPolizas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" 
                                PageSize="10" 
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand = "rg_ItemCommand"
                                Width="500px"   >
                                <MasterTableView ClientDataKeyNames="Pol_Version">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Visible="false">
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pol_Ano" HeaderText="Año" UniqueName="Pol_Ano">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pol_Mes" HeaderText="Mes" UniqueName="Pol_Mes" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pol_Version" HeaderText="Versión" UniqueName="Pol_Version">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pol_Tipo" HeaderText="Tipo" UniqueName="Pol_Tipo">
                                            </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pol_Cargo" HeaderText="Cargo"  UniqueName="Cargo"
                                            DataFormatString="{0:N2}" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pol_Abono"  HeaderText="Abono" UniqueName="Abono"
                                            DataFormatString="{0:N2}" > 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" 
                                           Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
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
                            </telerik:RadGrid></td>
               <td align ="right">
                   &nbsp;</td>
               <td>
               </td>
               </tr>
                 <tr>
               <td>
             
               </td>
               <td align ="right" >
                   &nbsp;</td>
               <td align ="right">
                   &nbsp;</td>
               <td>
               </td>
               </tr>
            </table>
        </div>
    </div>
</asp:Content>
