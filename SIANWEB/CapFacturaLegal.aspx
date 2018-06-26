<%@ Page Title="Documentos en proceso legal" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapFacturaLegal.aspx.cs" Inherits="SIANWEB.CapFacturaLegal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">
       function TxtId_Cd_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= CmbId_Cd.ClientID %>'));
       }
 
    
       function CmbId_Cd_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Cd.ClientID %>'));
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
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
   
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
                 <telerik:RadToolBarButton CommandName="Imprimir" Value="Imprimir" ToolTip="Imprimir" CssClass="print"
                    ImageUrl="Imagenes/blank.png"  />
                <telerik:RadToolBarButton CommandName="Guardar" Value="Guardar" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png"  />
            <telerik:RadToolBarButton CommandName="Nuevo" Value="Nuevo" ToolTip="Nuevo" CssClass="new"
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
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;" >
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td width="50">
                                </td>
                                <td width="50">
                                </td>
                                <td >
                                </td>
                                <td width="80" colspan= "9">
                                </td>
                            
                            </tr>
                            <tr>
                                <td>
                                   <asp:Label ID="LblId_Cd" runat="server" Text="CDI"></asp:Label></td>
                                <td >
                                   
                                    <telerik:RadNumericTextBox ID="TxtId_Cd" runat="server" MaxLength="9" MinValue="0" TabIndex="1"
                                                       Width="50px" >
                                                       <ClientEvents OnBlur="TxtId_Cd_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>
                                   </td>
                                   <td>
                                   </td>
                                   <td colspan = "9">
                                    <telerik:RadComboBox ID="CmbId_Cd" runat="server" autopostback="true" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px" OnClientBlur="Combo_ClientBlur" 
                                                         OnClientSelectedIndexChanged="CmbId_Cd_ClientSelectedIndexChanged"
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
                                <td >
                                     <asp:Label ID="Label2" runat="server" Text="Tipo documento"></asp:Label>
                                </td>
                                <td colspan ="11" >
                                <asp:RadioButtonList runat= "server" ID= "RblTipo" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Factura</asp:ListItem>
                                    <asp:ListItem Value="2">Nota de cargo</asp:ListItem>
                                </asp:RadioButtonList>
                                    &nbsp;</td>
                                              
                            </tr>
                                 
                                 <tr>
                                <td >
                                 <asp:Label ID="LblId_Fac" runat="server" Text="Documento"></asp:Label>
                                </td>
                                <td >
                                    <telerik:RadNumericTextBox ID="TxtId_Fac" runat="server" MaxLength="9"  TabIndex="2"
                                        MinValue="0" Width="50px">

                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                    </telerik:RadNumericTextBox>
                                     </td>  
                                <td >
                                </td>
                                <td colspan ="9" >
                                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" TabIndex="3"
                                     
                                        ToolTip="Buscar" onclick="btnBuscar_Click" />
                                     </td>
                                              
                            </tr>
                        
                       
                            <tr>
                                <td>
                                    <asp:Label ID="LblId_Cte" runat="server" Text="Cliente" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan = "9">
                                  <asp:Label ID="TxtId_Cte" runat="server" ></asp:Label></td>
                                
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblCte_Nombre" runat="server" Text="Nombre" Font-Bold="True"></asp:Label>
                                </td>
                                 <td colspan = "9">
                                    <asp:Label ID="Txt_Nombre" runat="server" ></asp:Label></td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="LblTotal" runat="server" Text="Total" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan = "9">
                                    <asp:Label ID="TxtTotal" runat="server" ></asp:Label></td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="LblPagado" runat="server" Text="Pagado" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan = "9">
                                    <asp:Label ID="TxtPagado" runat="server" ></asp:Label></td>
                             
                                
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="LblSaldo" runat="server" Text="Nombre" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan = "9">
                                    <asp:Label ID="TxtSaldo" runat="server" ></asp:Label></td>
                             
                              
                            </tr>
                                 <tr>
                                <td>
                                     </td>
                                <td colspan = "9">
                                   
                                    <asp:CheckBox  runat = "server" ID= "ChkLegal" Text ="Legal" TabIndex="4"/>
                                </td>
                                
                            </tr>
                               <tr>
                                <td>
                                   <asp:Label ID="Label1" runat="server" Text="Comentarios" Font-Bold="True"></asp:Label></td>
                                <td colspan = "9">
                                   
                                       <telerik:RadTextBox ID="TxtFacL_Comentarios" runat="server" MaxLength="800"  TabIndex="5"
                                      width="250px" height="50px" TextMode="multiline">
                                    </telerik:RadTextBox>
                                </td>
                                
                            </tr>
                        </table>
             
                    </td>
                </tr>
                 <asp:HiddenField ID="HF_Cve" runat="server" />
             
               
            </table>
            <br />
          
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    </asp:Content>

