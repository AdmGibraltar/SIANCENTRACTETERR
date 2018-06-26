<%@ Page Title="Reporte productos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="RepProductos.aspx.cs" Inherits="SIANWEB.RepProductos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">

       function refreshGrid() {
           var ajaxManager = $find("<%= RAM1.ClientID %>");
           ajaxManager.ajaxRequest('RebindGrid');
       }

       function TxtId_Pvd_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= cmbProveedor.ClientID %>'));
       }


       function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Pvd.ClientID %>'));
       }


       function TxtId_Ptp_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= cmbTipoProducto.ClientID %>'));
       }


       function cmbTipoProducto_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Ptp.ClientID %>'));
       }


       function TxtId_Cpr_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= cmbCategoriaProducto.ClientID %>'));
       }


       function cmbCategoriaProducto_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Cpr.ClientID %>'));
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
                                   <asp:Label ID="LblId_Prd" runat="server" Text="Producto"></asp:Label></td>
                                <td >
                                     <telerik:RadNumericTextBox ID="TxtId_Prd" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>

                                   </td>
                                   <td colspan = "9">
                                        
                                   </td>

                            </tr>
                                <tr>
                                <td>
                                 <asp:Label ID="LblId_Pvd" runat="server" Text="Proveedor"></asp:Label>
                                </td>
                                <td>
                                              <telerik:RadNumericTextBox ID="TxtId_Pvd" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                                       <ClientEvents OnBlur="TxtId_Pvd_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>
                                </td>  
   
                                <td colspan ="9">
                                 <telerik:RadComboBox ID="cmbProveedor" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px"   
                                                        Width="300px" OnClientBlur="Combo_ClientBlur"
                                                         OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged">
                                                           <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                </td>
                                              
                            </tr>
                                 <tr>
                                <td>
                                 <asp:Label ID="LblId_Tp" runat="server" Text="Tipo de producto"></asp:Label>
                                </td>
                                <td>
                                  <telerik:RadNumericTextBox ID="TxtId_Ptp" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                                         <ClientEvents OnBlur="TxtId_Ptp_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>
                                </td>  
                                <td colspan ="9">
                                 <telerik:RadComboBox ID="cmbTipoProducto" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px"   
                                                        Width="300px" OnClientBlur="Combo_ClientBlur"
                                                         OnClientSelectedIndexChanged="cmbTipoProducto_ClientSelectedIndexChanged">
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
                                 <asp:Label ID="LblId_Cpr" runat="server" Text="Categoría de producto"></asp:Label>
                                </td>
                                <td>  <telerik:RadNumericTextBox ID="TxtId_Cpr" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                                         <ClientEvents OnBlur="TxtId_Cpr_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>

                                </td>  
                             <td colspan ="9">
                                 <telerik:RadComboBox ID="cmbCategoriaProducto" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px"   
                                                        Width="300px" OnClientBlur="Combo_ClientBlur"
                                                         OnClientSelectedIndexChanged="cmbCategoriaProducto_ClientSelectedIndexChanged">
                                                           <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                </td>
                                              
                            </tr>
                       
                        
               
                        </table>
             
                    </td>
                </tr>

             
               
            </table>

        </div>
    </div>
</asp:Content>
