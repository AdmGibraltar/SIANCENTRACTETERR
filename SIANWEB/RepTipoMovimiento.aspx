<%@ Page Title="Reporte tipos de movimiento" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="RepTipoMovimiento.aspx.cs" Inherits="SIANWEB.RepTipoMovimiento" %>

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
    <telerik:RadAjaxManager ID="RAM1" runat="server"  
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
            <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" CssClass="print"
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
                                 <asp:Label ID="Label4" runat="server" Text="Movimiento"></asp:Label>
                                </td>
                                <td colspan ="2">
                               <telerik:RadNumericTextBox ID="TxtId_MovC" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                </td>  
                                <td colspan ="9">
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
                                     <asp:Label ID="Label3" runat="server" Text="Tipo precio"></asp:Label>
                                </td>       
                                <td colspan = "4">
                                   <telerik:RadComboBox ID="cmbTPrecio" runat="server" Width="150px">
                                </telerik:RadComboBox></td>
                             
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                               <tr>
                                <td>
                                 <asp:Label ID="Label5" runat="server" Text="Tipo reporte"></asp:Label>
                                  
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
            
        </div>
    </div>
</asp:Content>
