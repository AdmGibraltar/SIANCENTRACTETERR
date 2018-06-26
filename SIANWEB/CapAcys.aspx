<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="True" CodeBehind="CapAcys.aspx.cs" Inherits="SIANWEB.CapAcys"  EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
               <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ContactoRepVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbRepresentante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="ChkServAsesoria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="ChkServTecnicoRelleno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkServMantenimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="rdVigenciaIni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdFechaInicioDocumento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdFechaFinDocumento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAcuerdos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAsesoria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsesoria" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgServicios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgServicios" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgMantPrevRev">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMantPrevRev" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>        
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick"
        onclientbuttonclicked="ToolBar_ClientClick">
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
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
        </Items>
    </telerik:radtoolbar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" style="font-family: verdana; font-size: 9pt" runat="server">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label102" runat="server" Text="Folio" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtFolio" runat="server" enabled="False" maxlength="9"
                                    minvalue="1" width="70px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" /> 
                                <ClientEvents OnKeyPress="handleClickEvent" /> 
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha
                            </td>
                            <td>
                                <telerik:RadDatePicker id="rdFecha" runat="server" width="90px" culture="es-MX">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                    <ClientEvents OnDateClick="Calendar_Click" />
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdFecha"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha Inicio
                            </td>
                            <td>                               
                                <telerik:RadDatePicker ID="rdFechaInicioDocumento" runat="server" Width="100px" OnSelectedDateChanged="rdFechaInicioDocumento_SelectedDateChanged"
                                AutoPostBack="True" Culture="es-MX">
                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x" ShowRowHeaders="false">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy" />
                                </Calendar>
                                <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                            </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rdFechaInicioDocumento"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha Fin
                            </td>
                            <td>
                             <telerik:RadDatePicker ID="rdFechaFinDocumento" runat="server" Width="100px" OnSelectedDateChanged="rdFechaFinDocumento_SelectedDateChanged"
                                AutoPostBack="True" Culture="es-MX">
                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x" ShowRowHeaders="false">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy" />
                                </Calendar>
                                <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                            </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rdFechaFinDocumento"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>                           
                            </td>
                            <td width="250">
                            </td>
                            <td>
                                <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" 
                                ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" 
                                ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:radtabstrip id="RadTabStrip1" runat="server" multipageid="RadMultiPage1"
                        selectedindex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" AccessKey="C" PageViewID="RadPageCliente" Text="1.-&lt;u&gt;C&lt;/u&gt;liente" ClientIDMode="Inherit"
                                 Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="R" PageViewID="RPVRecepcionPedido" Text="2.-&lt;u&gt;R&lt;/u&gt;ecepción de Pedidos">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="A" PageViewID="RPVAcuerdosEconomicos" Text="3.-&lt;u&gt;A&lt;/u&gt;cuerdo Económico de Producto">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="P" PageViewID="RPVCondicionesPago" Text="4.-Condiciones de &lt;u&gt;p&lt;/u&gt;ago">
                            </telerik:RadTab>                                                     
                            <telerik:RadTab runat="server" AccessKey="S" PageViewID="RPVServicio" Text="5.-&lt;u&gt;S&lt;/u&gt;ervicios de Valor">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="O" PageViewID="RPVOtrosApoyos" Text="6.-<u>O</u>tros Apoyos">
                            </telerik:RadTab>                            
                        </Tabs>
                    </telerik:radtabstrip>
                    <div id="div1" runat="server">
                        <telerik:radmultipage id="RadMultiPage1" runat="server" borderstyle="Solid" borderwidth="1px"
                            scrollbars="Auto" selectedindex="0">
                            <%-- Height="415px" Width="880px">--%>
                            <telerik:RadPageView ID="RadPageCliente" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="99%" Height="415px"
                                    ResizeMode="AdjacentPane" BorderSize="0" BorderStyle="Solid" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane1" runat="server" Width="99%" Height="410px" OnClientResized="onResize"
                                        BorderColor="Red">
                                        <div runat="server" id="divGenerales" style="font-family: verdana; font-size: 8pt">                                                                                                                                                                                                                              
                                            <table style="border:1px solid black;"  >                                                                                                                                 
                                              <thead >
                                                <tr>
                                                <th  style="font-family:  verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="21"    >  CLIENTE</th>                                                         
                                                </tr>
                                                <tr>
                                                <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  Información Fiscal</th>                                                         
                                                </tr>
                                              </thead> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                           
                                                <tr> 
                                                    <td>
                                                    </td>
                                                    <td> 
                                                        <asp:Label ID="Label5" runat="server" Text="Cliente"/>
                                                    </td>
                                                    <td width="70">
                                                        <telerik:RadNumericTextBox ID="txtCliente" runat="server" AutoPostBack="True" MaxLength="9"
                                                            MinValue="1" OnTextChanged="txtCliente_TextChanged" Width="70px">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator=""/>
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>    
                                                    </td>                                                                                                          
                                                    <td colspan="10">
                                                        <telerik:RadTextBox ID="txtClienteNombre" runat="server" ReadOnly="True" Width="428px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td Width="20px" >
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCliente"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                        <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                            ToolTip="Buscar" ValidationGroup="buscar" />
                                                    </td>
                                                    <td> </td>                                                     
                                                     <td> </td>                                                                                                         
                                                     <td width="50"> </td>
                                                     <td  width="50"> </td>
                                                     <td> </td>
                                                     <td></td>
                                                    <td></td>
                                                </tr>
                                                 <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label59" runat="server" Text="Dirección"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txtClienteDireccion" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label60" runat="server" Text="Colonia"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txtClienteColonia" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>                                                        </td>     
                                                     <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtClienteColonia"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>                                                                                                  
                                                   <td colspan="1">  <asp:Label ID="Label61" runat="server" Text="Municipio"></asp:Label>         </td> 
                                                      <td colspan="6">
                                                        <telerik:RadTextBox ID="txtClienteMunicipio" runat="server" ReadOnly="False" width="410">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                     
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label62" runat="server" Text="Estado"/></td>                                                  
                                                    <td colspan="2">
                                                        <telerik:RadTextBox ID="txtClienteEstado" runat="server" ReadOnly="False" width="185" >
                                                        </telerik:RadTextBox>                       </td>                                                                                                                                                              
                                                    <td> <asp:Label ID="Label63" runat="server" Text="C.P." style="text-align:center;"  width="30" ></asp:Label> </td> 
                                                    <td colspan="1">
                                                        <telerik:RadTextBox ID="txtClienteCodPost" runat="server" ReadOnly="False" width="47">
                                                        </telerik:RadTextBox> </td>
                                                    <td colspan="1">  <asp:Label ID="Label64" runat="server" Text="RFC" width="40" style="text-align:center;" ></asp:Label> </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtClienteRFC" runat="server" ReadOnly="False" width="175" >
                                                        </telerik:RadTextBox> </td> 
                                                    <td></td>   
                                                    <td> 
                                                        <asp:Label ID="Label65" runat="server" Text="Addenda"></asp:Label>
                                                    </td>
                                                    <td> <asp:CheckBox ID="ChkbAdendaSI" runat="server" Text="SI" ReadOnly="True" /> </td>                                                    
                                                    <td> </td>                                                        
                                                    <td> </td>   
                                                    <td> </td>                                                     
                                                     <td> </td>                                                    
                                                     <td> </td>  
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="Label106" runat="server" Text="E-mail de recibo de factura electrónica"></asp:Label>
                                                    </td>
                                                    <td colspan="3" >
                                                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="231px" MaxLength="50" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" />
                                                        </telerik:RadTextBox>
                                                       <asp:RegularExpressionValidator
                                                                ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtEmail"
                                                                Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                                                    </td>
                                                     <td colspan="9">&#160; </td>                                                      
                                                     <td> <asp:Label ID="Label9" runat="server" Text="Cuenta Corporativa"></asp:Label> </td>
                                                    <td> <asp:CheckBox ID="CheckCuentaCorporativa" runat="server" Text="SI" ReadOnly="True"/> </td>  
                                                </tr>
                                                  <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                                        
                                                <tr>
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  Información Comercial</th>                                                         
                                                </tr> 
                                                 <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                                    
                                                <tr > 
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label26" runat="server" Text="Nombre comercial" /> </td>
                                                    <td colspan="4" style="margin-left: 40px">
                                                        <telerik:RadTextBox ID="txtComercial" runat="server" Width="275px" MaxLength="250" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox> </td>                                                                                                                                                
                                                     <td colspan="7">
                                                        <asp:Label ID="Label67" runat="server" Text="Dirección de Entrega Producto"></asp:Label>
                                                    </td>
                                                    <td> </td>                                                   
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtDireccionEntrega" runat="server" ReadOnly="True" Width="400px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td> </td>                                                                                                          
                                                </tr>
                                                  <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label68" runat="server" Text="Colonia"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txtClienteColoniaE" runat="server" ReadOnly="True" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label69" runat="server" Text="Municipio"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txtClienteMunicipioE" runat="server"  width="220">
                                                        </telerik:RadTextBox>     
                                                       </td>     
                                                        <td></td>
                                                     <td colspan="1">
                                                       <asp:Label ID="Label70" runat="server" Text="Estado" ReadOnly="True"></asp:Label> 
                                                    </td>                                                                                                                                                     
                                                      <td colspan="2">
                                                        <telerik:RadTextBox ID="txtClienteEstadoE" runat="server" Enabled="False" width="122">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                     <td colspan="1">  <asp:Label ID="Label71" runat="server" Text="C.P."></asp:Label>   </td>   
                                                     <td colspan="1"  >
                                                        <telerik:RadTextBox ID="txtClienteCPE" runat="server" ReadOnly="True" width="60">
                                                        </telerik:RadTextBox>
                                                    </td>  
                                                    <td colspan="1"> <asp:Label ID="Label25" runat="server" Text="No proveedor" /> </td>
                                                    <td colspan="2"> <telerik:RadTextBox ID="txtProveedor" runat="server" Width="70px" MaxLength="9" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox> </td>                                                                                                                                                                                                                                                    
                                                </tr>
                                                <tr> 
                                                    <td> </td>
                                                     <td>  <asp:Label ID="Label3" runat="server" Text="Contacto principal" /> </td>
                                                    <td  colspan="4">
                                                        <telerik:RadTextBox ID="txtContacto" runat="server" Width="275px" MaxLength="100" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>                                                    
                                                    <td>   <asp:Label ID="Label4" runat="server" Text="Puesto" />  </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtPuesto" runat="server" Width="175px" MaxLength="50" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        
                                                    </td>                                                    
                                                     <td >
                                                        <asp:Label ID="Label6" runat="server" Text="Teléfono" />
                                                    </td>
                                                    <td colspan="2" >
                                                        <telerik:RadTextBox ID="txtTelefono" runat="server"  Width="122px"  MaxLength="9" ReadOnly="True">                                                         
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />                                                            
                                                        </telerik:RadTextBox>
                                                    </td>
                                                      <td >
                                                        
                                                    </td>
                                                    <td colspan="3" >                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td><asp:Label ID="Label8" runat="server" Text="Territorio" />    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                            Width="70px" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>                                                     
                                                    <td colspan="8">
                                                        <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            MaxHeight="250px" OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbTer_SelectedIndexChanged" Width="370px" ReadOnly="True">
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
                                                     <td Width="20px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTerritorio"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"  ></asp:RequiredFieldValidator>
                                                    </td>   
                                                     <td  Width="20px" ></td>                                                   
                                                    <td  >   </td> 
                                                    <td colspan="1" >   
                                                        <asp:Label ID="Label1" runat="server" Text="Ruta de servicio"></asp:Label> 
                                                    </td>
                                                    <td  colspan="1">
                                                        <telerik:RadNumericTextBox ID="txtRutaServicio" runat="server" Width="70px" MinValue="1"
                                                            MaxLength="9" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnBlur="txt4_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                     <td  colspan="6">
                                                        <telerik:RadComboBox ID="cmbRutaServicio" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb4_ClientSelectedIndexChanged"
                                                            Width="340px" OnClientFocus="pre_validarfecha" MaxHeight="250px" ReadOnly="True">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="Label76" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>                                                                                                                                                                                                                                                                                                                                                                                                                            
                                                </tr>
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label7" runat="server" Text="Representante de Ventas" />   </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" MinValue="1" Width="70px"
                                                            MaxLength="9" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txt2_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td colspan="8">
                                                        <telerik:RadComboBox ID="cmbRepresentante" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione un territorio"
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbRepresentante_SelectedIndexChanged" Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
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
                                                    <td Width="20px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRepresentante"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>  
                                                    <td></td>    
                                                    <td></td> 
                                                    <td><asp:Label ID="Label2" runat="server" Text="Ruta de entrega"></asp:Label> </td>
                                                    <td colspan="1">  <telerik:RadNumericTextBox ID="txtRutaEntrega" runat="server" Width="70px" MinValue="1"
                                                            MaxLength="9" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnBlur="txt5_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>                  </td>
                                                    <td colspan="6">
                                                        <telerik:RadComboBox ID="cmbRutaEntrega" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true" 
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb5_ClientSelectedIndexChanged"
                                                            Width="340px" OnClientFocus="pre_validarfecha" MaxHeight="250px" ReadOnly="True">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>                                                                                                                                                            
                                                </tr>
                                                 <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>  
                                            </table>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RPVRecepcionPedido" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane2" runat="server" OnClientResized="onResize">
                                    <div runat="server" id="div2" style="font-family: verdana; font-size: 8pt">                                    
                                        <table style="border:1px solid black; margin-right:0" width="100%" >                                                                                                                                 
                                              <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > 2.- EL CLIENTE ENVIARA LOS PEDIDOS DE PRODUCTO VIA:</th>                                                         
                                                </tr>                                                        
                                              </thead>  
                                                <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                            
                                                <tr> 
                                                    <td></td>                                                   
                                                    <td> <asp:CheckBox ID="ChkbEmail" runat="server" Text="Email" />  </td>
                                                     <td width="100px"> </td>
                                                    <td ><asp:CheckBox ID="ChkbFax" runat="server" Text="Fax"/> </td>
                                                    <td><asp:CheckBox ID="ChkbTelefono" runat="server" Text="Teléfono"/> </td>    
                                                    <td colspan="2">&#160;</td>                                                
                                                    <td colspan="3"><asp:CheckBox ID="CheckRepVenta" runat="server" Text="Recolectado por el Rep. de Ventas"/> </td>
                                                    
                                                </tr>
                                                 <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label97" runat="server" Text="Otro:"/></td>
                                                    <td colspan="7"  >
                                                        <telerik:RadTextBox ID="txtPedidoOtro" runat="server" ReadOnly="False" width="600">
                                                        </telerik:RadTextBox> 
                                                     </td>                                                                                                                                                                                                                                                                                                                                    
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td  colspan="1"> <asp:Label ID="Label100" runat="server" Text="Nombre de la Persona Encargada de Enviar el Pedido:"/></td>                                                  
                                                    <td colspan="5">
                                                        <telerik:RadTextBox ID="txtPedidoEncargadoEnviar" runat="server" ReadOnly="False" width="600" >
                                                        </telerik:RadTextBox>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtPedidoEncargadoEnviar"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                     </td>                                                                                                                                                                                                                  
                                                </tr>                                                                                                                                                                                                                                                     
                                                <tr> 
                                                    <td></td>
                                                    <td> <asp:Label ID="Label104" runat="server" Text="Puesto:" /> </td>
                                                    <td colspan="2" >
                                                        <telerik:RadTextBox ID="txtpedidoPuesto" runat="server" Width="245px" MaxLength="250">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtpedidoPuesto"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                     </td>                                                                                                                                                
                                                     <td colspan="1">
                                                        <asp:Label ID="Label105" runat="server" Text="Teléfono:"></asp:Label>
                                                    </td>                                                    
                                                    <td colspan="2">
                                                    <telerik:RadTextBox ID="txtpedidotelefono" runat="server" Width="180px">
                                                        </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtpedidotelefono"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>  
                                                     <td colspan="1"  width="50px">
                                                        <asp:Label ID="Label96" runat="server" Text="Email:"></asp:Label>
                                                    </td>                                                    
                                                    <td colspan="1" Width="264px">
                                                        <telerik:RadTextBox ID="txtpedidoEmail" runat="server"  Width="257px">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtpedidoEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>                                                   
                                                </tr> 
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">                                                        
                                                    </td>
                                                    <td colspan="4">                                                     
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr> 
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr> 
                                                <tr>
                                                    <td>
                                                    </td>
                                                  <td>Documentación requerida para entrega  </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkRecDocOrdenCompra" runat="server" Text="Orden de compra / Release" />
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                         <asp:CheckBox ID="chkRecDocReposicion" runat="server" Text="Orden de Reposición" />
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                       <asp:CheckBox ID="ChkRecDocFolio" runat="server" Text="Folio" />
                                                    </td>                                                    
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label55" runat="server" Text="Otro:"/></td>
                                                    <td colspan="7"  >
                                                        <telerik:RadTextBox ID="txtRecDocOtro" runat="server" ReadOnly="False" width="600">
                                                        </telerik:RadTextBox> 
                                                     </td>                                                                                                                                                                                                                                                                                                                                    
                                                </tr>
                                                 <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                     
                                                    </td>                                                    
                                                </tr>  
                                                 <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                     
                                                    </td>                                                    
                                                </tr>  
                                                 <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                     
                                                    </td>                                                    
                                                </tr>                                                                                             
                                            </table>                                                                                 
                                            <table>                                               
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:HiddenField ID="HF_ID" runat="server" />   
                                                        <asp:HiddenField ID="HF_Sustituye" runat="server" />   
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                        <td></td>
                                                        <td>
                                                       
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                       
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                      </tr> 
                                                      <tr>
                                                        <td></td>
                                                      </tr> 
                                                      <tr>
                                                        <td></td>
                                              </tr>        
                                            </table>
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RPVAcuerdosEconomicos" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter3" runat="server" Width="100.5%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane3" runat="server" Width="100%" OnClientResized="onResize">                                     
                                        <div runat="server" id="divAcuerdosE" style="font-family: verdana; font-size: 8pt">
                                            <table >
                                                <tr>
                                                    <td width="10">
                                                    </td>
                                                    <td width="130">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td width="10">
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td width="150">
                                                    <asp:Label ID="Label99" runat="server" Text="ANEXO A" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="20">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="7">
                                                        
                                                    </td>
                                                     <a<td colspan="3">
                                                       &#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label28" runat="server" Text="Vigencia a partir de"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <telerik:RadDatePicker ID="rdVigenciaIni" runat="server" Width="100px" OnSelectedDateChanged="RadDatePicker2_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX">
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rdVigenciaIni"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                            &#160;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label29" runat="server" Text="Semana inicial"></asp:Label>
                                                    </td>
                                                    <td colspan="5">
                                                        <telerik:RadNumericTextBox ID="txtSemana" runat="server" MinValue="1" Width="70px"
                                                            Enabled="False" MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                            &#160;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                            &#160;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="7">
                                                       
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                            &#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="6">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                           &#160;
                                                    </td>
                                                    <td>
                                                            &#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="6">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td>
                                                    <telerik:RadGrid ID="rgAcuerdos" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        OnNeedDataSource="rgAcuerdos_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_ItemDataBound" OnItemCreated="rgAcuerdos_ItemCreated"
                                                        BorderStyle="None">
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdPrd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Id_Prd") %>'
                                                                            MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del producto"
                                                                    UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="170px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="170px"
                                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="Equivalencias">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="hlEquivalencias" runat="server" Width="65px">Equivalencia</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle Width="80px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentacion" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblPresentacionEd" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniNom" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblUniEd" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Precio" HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>                                                                
                                                                <telerik:GridTemplateColumn DataField="Acys_Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Acys_Frecuencia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acys_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acys_Frecuencia") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLunes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMartes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMiercoles" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJueves" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkViernes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSabado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_DocStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                                            Width="80px">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Modalidad" HeaderText="Modalidad" UniqueName="Acs_Modalidad">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("Acs_ModalidadStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtModalidad" runat="server" Text='<%# Bind("Acs_Modalidad") %>'
                                                                            Width="140px">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Visita del representante" Value="A" />
                                                                                <telerik:RadComboBoxItem Text="Confirmación Tel." Value="B" />
                                                                                <telerik:RadComboBoxItem Text="Confirmación/Con consignación" Value="C" />
                                                                                <telerik:RadComboBoxItem Text="Orden Abierta/Con reposición" Value="D" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltSCtp" DataField="Acys_UltSCtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltACtp" DataField="Acys_UltACtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                        </table>
                                          </div>                                       
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RPVCondicionesPago" runat="server" Enabled="False">
                                <telerik:RadSplitter ID="RadSplitter4" BorderSize="0" runat="server" Width="99%"
                                    Height="415px" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane4" runat="server" Width="99%" Height="415px" OnClientResized="onResize">
                                    <div runat="server" id="divCondionesPago" style="font-family: verdana; font-size: 8pt">                                    
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
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
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkCredito" runat="server" Text="Crédito" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    Días
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtDias" runat="server" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>                                               
                                                <td>
                                                 Naturales
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
                                                <td>
                                                    <asp:Label ID="Label30" runat="server" Text="Límite de crédito"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtLimite" runat="server" Width="80px">
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkContado" runat="server" Text="Contado" />
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
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td colspan="2" width="100">
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                            </tr>                                                       
                                                        
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                </tr>
                                            </table>                                                
                                            <table width="100%">
                                                <tr>
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  4.1 Formas de Pago</th>                                                         
                                                    </tr>
                                                <tr>
                                                    <td width="10">
                                                    </td>
                                                    <td width="20">
                                                    </td>
                                                    <td width="120">
                                                    </td>
                                                </tr>                                                        
                                                <tr>
                                                    <td>
                                                    </td>                                                            
                                                    <td>
                                                        <asp:CheckBox ID="chkEfectivo" runat="server" Text="Efectivo" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="ChkDeposito" runat="server" Text="Deposito" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>                                                            
                                                    <td>
                                                        <asp:CheckBox ID="chkFactoraje" runat="server" Text="Factoraje" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="ChkTarjetaDebito" runat="server" Text="Tarjeta de Debito" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>                                                            
                                                    <td>
                                                        <asp:CheckBox ID="chkTransferencia" runat="server" Text="Transferencia" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="ChkTarjetaCredito" runat="server" Text="Tarjeta de Crédito" />
                                                    </td>
                                                </tr>
                                                <tr>                                                            
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkCheque" runat="server" Text="Cheque" />
                                                    </td>
                                                </tr>                                                        
                                            </table>
                                        <table width="100%">
                                        <tr>
                                            <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  4.2 Revisión de Facturas</th>                                                         
                                         </tr>  
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td height="10" width="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label32" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label34" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label35" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label36" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label37" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label38" runat="server" Text="Sábado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                                <asp:Label ID="Label33" runat="server" Text="Días de revisión"></asp:Label>
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionLunes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionMartes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionMiercoles" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionJueves" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionViernes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionSabado" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>                                                        
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="120">
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
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label51" runat="server" Text="Horarios de revisión"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionMañanaInicio" runat="server" Culture="es-MX"
                                                                    Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="cabezera">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionMañanafin" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                y
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionTardeinicio" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionTardefin" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                        <tr> 
                                                         <td width="10">
                                                            </td>                                                           
                                                             <td valign="top" width="100">
                                                                <asp:Label ID="Label54" runat="server" Text="Documentos Adicionales"></asp:Label>
                                                              </td>   
                                                              <td colspan="5"></td>                                                                                                                   
                                                                                                                                                               
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td valign="top" width="100">
                                                            </td>                                                            
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkrevfolio" runat="server" Text="Folio" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkreventrada" runat="server" Text="Entrada de Almacén"/>
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkrevorden" runat="server" Text="Orden de Compra" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkrevreporte" runat="server" Text="Reporte de Consumo" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkrevcopia" runat="server" Text="Copia de Factura" />
                                                            </td>                                                                                                                       
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td valign="top" width="100">
                                                                <asp:Label ID="Label27" runat="server" Text="Otro"></asp:Label>
                                                              </td>  
                                                            <td colspan="4">
                                                             <telerik:RadTextBox ID="txtrevOtro" runat="server"  width="600">
                                                             </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                    <tr>
                                                        <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  4.3 Pago de Facturas</th>                                                         
                                                     </tr>
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
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
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label39" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label40" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label41" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label43" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label45" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label47" runat="server" Text="Sábado"></asp:Label>
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
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label52" runat="server" Text="Días de pago"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoLunes" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoMartes" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoMiercoles" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoJueves" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoViernes" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoSabado" runat="server" />
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
                                                    <table >
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="115">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label53" runat="server" Text="Horarios de pago"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoMañanaInicio" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton runat="server" CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton runat="server" CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label58" runat="server" Text="a"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoMañanafin" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label57" runat="server" Text="y"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoTardeinicio" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label56" runat="server" Text="a"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoTardefin" runat="server" Width="100px">
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                </td>
                                            </tr>
                                        </table>                                        
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>                          
         
                            <telerik:RadPageView ID="RPVServicio" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter5" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true" Height="615px">                                 
                                    <telerik:RadPane ID="RadPane5" runat="server" OnClientResized="onResize">
                                    <div runat="server" id="divServicio" style="font-family: verdana; font-size: 8pt">                                    
                                        <table>
                                            <tr>
                                                <td width="10"></td>
                                                <td width="50">&nbsp;&nbsp;</td>
                                                <td valign="middle"></td>
                                                <td width="10"></td>
                                                <td width="50"></td>
                                                <td></td>
                                                <td width="70"></td>
                                                <td width="70"></td>
                                            </tr>
                                            <tr>
                                                
                                                <td colspan="5">
                                                     <asp:Label ID="Label48" runat="server" Text="5.1 Visita del Representante" Font-Bold="true"></asp:Label>    
                                                 </td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                             <td>
                                                Frecuencia
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="Vis_Frecuencia" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="0">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="4">
                                                    Vez(Veces) al mes
                                                </td>
                                                <td colspan="3"></td>
                                                  <td><asp:Label ID="Label42" runat="server" Text="Otro:"/></td>
                                                    <td colspan="2"  >
                                                        <telerik:RadTextBox ID="txtVisitaOtro" runat="server" ReadOnly="False" width="300">
                                                        </telerik:RadTextBox> 
                                                     </td> 
                                            </tr>
                                            </table>
                                             <table>
                                            <tr>
                                               <td colspan="5">
                                                <asp:Label ID="Label50" runat="server" Text="5.2 Servicio de Asesoria" Font-Bold="true"></asp:Label>    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>                                                            
                                                <td>
                                                    <asp:CheckBox ID="ChkServAsesoria" runat="server" OnCheckedChanged="ChkServAsesoria_CheckedChanged" Text="Requiere Servicio de Asesoria" 
                                                        AutoPostBack="true" Checked="True" />
                                                </td>                                                            
                                             </tr>
                                             <tr>
                                                <td width="10"></td>                                           
                                              </tr>
                                                <tr id="AsesoriaListado" runat="server">
                                                    <td width="10">  </td>
                                                    <td>
                                                        <telerik:RadGrid ID="rgAsesoria" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                            OnNeedDataSource="rgAsesoria_NeedDataSource" PageSize="15">
                                                            <MasterTableView>
                                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="Id_Ase" HeaderText="Núm." UniqueName="Id_Ase">
                                                                        <HeaderStyle Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Ase_Descripcion" HeaderText="Descripción" UniqueName="Ase_Descripcion">
                                                                        <HeaderStyle Width="500px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                        <HeaderTemplate>
                                                          <table id="Table6" cellspacing="1" cellpadding="1" width="600" >
                                                            <tr>
                                                              <td colspan="6" align="center">
                                                                <b>Frecuencia</b>
                                                              </td>
                                                            </tr>
                                                            <tr>
                                                              <td width="12%">
                                                                <b>Mensual</b>
                                                              </td>
                                                              <td width="21%">
                                                                <b>Fecha Inicio</b>
                                                              </td>
                                                               <td width="12%">
                                                                <b>Bimestral</b>
                                                              </td>
                                                              <td width="21%">
                                                                <b>Fecha Inicio</b>
                                                              </td>
                                                               <td width="12%">
                                                                <b>Trimestral</b>
                                                              </td>
                                                              <td width="21%">
                                                                <b>Fecha Inicio</b>
                                                              </td>
                                                            </tr>
                                                          </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                          <table id="Table7" cellspacing="1" cellpadding="1" width="600" border="1">
                                                            <tr>
                                                              <td width="12%">                                                          
                                                                  <asp:RadioButton ID="ServAsesoriaMensual" GroupName="FrecuenciaServAsesoria" runat="server"  OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual")) %>'/>
                                                              </td>
                                                              <td width="21%">
                                                                 <telerik:RadDatePicker ID="ServAsesoriaMensualfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                    AutoPostBack="True" Culture="es-MX" Enabled='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual")) %>'  MinnDate="01/01/0001" 
                                                                     DbSelectedDate ='<%# Eval("Ase_ServAsesoriaMensualfechaIni") %>'>
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x" ShowRowHeaders="false">
                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                            TodayButtonCaption="Hoy" />
                                                                    </Calendar>
                                                                    <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                </telerik:RadDatePicker>
                                                              </td>
                                                              <td width="12%"> 
                                                                <asp:RadioButton ID="ServAsesoriaBimestral" GroupName="FrecuenciaServAsesoria" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral")) %>'/>
                                                              </td>
                                                              <td width="21%">
                                                                <telerik:RadDatePicker ID="ServAsesoriaBimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged" 
                                                                    AutoPostBack="True" Culture="es-MX"  Enabled='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral")) %>' MinDate="01/01/0001" 
                                                                    DbSelectedDate ='<%# Eval("Ase_ServAsesoriaBimestralfechaIni") %>'>
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x" ShowRowHeaders="false">
                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                            TodayButtonCaption="Hoy" />
                                                                    </Calendar>
                                                                    <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal"  />
                                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                </telerik:RadDatePicker>
                                                              </td>
                                                               <td width="12%">
                                                                <asp:RadioButton ID="ServAsesoriaTrimestral" GroupName="FrecuenciaServAsesoria" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral")) %>'/>
                                                              </td>
                                                              <td width="21%">
                                                                <telerik:RadDatePicker ID="ServAsesoriaTrimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged" 
                                                                    AutoPostBack="True" Culture="es-MX" Enabled='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral")) %>' MinDate="01/01/0001" DbSelectedDate ='<%# Eval("Ase_ServAsesoriaTrimestralfechaIni") %>'>
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x" ShowRowHeaders="false">
                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                            TodayButtonCaption="Hoy" />
                                                                    </Calendar>
                                                                    <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                </telerik:RadDatePicker>
                                                              </td>                                                       
                                                            </tr>
                                                          </table>
                                                        </ItemTemplate>
                                                      </telerik:GridTemplateColumn>
                                                      </Columns>
                                                </MasterTableView>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                                NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                                PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>                                           
                                                  <tr>
                                                    <td> <asp:Label ID="Label107" runat="server" Text="  "></asp:Label></td>
                                                    <td></td>
                                                 </tr>                                              
                                        </table>
                                        <table width="95%">    <%--INICIO--%>
                                        <tr>
                                           <td colspan="5">  
                                            <asp:Label ID="Label49" runat="server" Text="5.3 Servicio Técnico" Font-Bold="true"></asp:Label>
                                           </td>
                                        </tr>                                         
                                         <tr>
                                            <td width="10"></td>
                                            <td>    <asp:Label ID="Label103" runat="server" Text="A) Equipos de Servicio (Relleno)"></asp:Label>    </td>
                                          </tr>
                                          <tr>
                                            <td>
                                            </td>                                                            
                                            <td>
                                                <asp:CheckBox ID="ChkServTecnicoRelleno" runat="server" Text="Requiere Servicio a equipos(Relleno) " OnCheckedChanged="ChkServTecnicoRelleno_CheckedChanged" AutoPostBack="True"  Checked="True" />
                                            </td>                                                            
                                         </tr>                                            
                                            <tr id="EquipoRellenoListado" runat="Server">
                                                <td width="10">       </td>
                                                <td>
                                                    <telerik:RadGrid ID="rgServicios" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."                                                      
                                                        OnItemDataBound="rgServicios_ItemDataBound" OnNeedDataSource="rgServicios_NeedDataSource"
                                                        OnItemCommand="rgServicios_ItemCommand" PageSize="15">
                                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Prd,Prd_AgrupadoSpo" EditMode="InPlace" >
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Código" UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCodigoEdit" runat="server" Text='<%# Bind("Id_Prd") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <ClientEvents OnBlur="ObtenerNombreInicio" OnLoad="onLoadIdPrd" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProducto" runat="server" Text='<%# Bind("Prd_Descripcion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoEdit" runat="server" Enabled="false" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="onLoadPrdDescr" />
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="400px" />
                                                                </telerik:GridTemplateColumn>                                                                
                                                                <telerik:GridTemplateColumn DataField="Prd_AgrupadoSpo" Display="false" HeaderText="Prd_AgrupadoSpo"
                                                                    UniqueName="Prd_AgrupadoSpo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAgrupadoSpo" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtAgrupadoSpoEdit" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_InvInicial" HeaderText="Cantidad" UniqueName="Prd_InvInicial">
                                                                    <ItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%"  Enabled="false">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidadEdit" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                                <HeaderTemplate>
                                                                  <table id="Table2" cellspacing="1" cellpadding="1" width="600" >
                                                                    <tr>
                                                                      <td colspan="6" align="center">
                                                                        <b>Frecuencia</b>
                                                                      </td>
                                                                    </tr>
                                                                    <tr>
                                                                      <td width="12%">
                                                                        <b>Bimestral</b>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <b>Fecha Inicio</b>
                                                                      </td>
                                                                       <td width="12%">
                                                                        <b>Trimestral</b>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <b>Fecha Inicio</b>
                                                                      </td>                                                       
                                                                    </tr>
                                                                  </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                  <table id="Table3" cellspacing="1" cellpadding="1" width="600" border="1">
                                                                    <tr>
                                                                      <td width="12%">                                                          
                                                                          <asp:RadioButton ID="ServTecnicoRellenoBimestral"  Enabled="false"  GroupName="FrecuenciaServRelleno" runat="server"  
                                                                          Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                         <telerik:RadDatePicker ID="ServTecnicoRellenoBimestralfechaIni" runat="server" Width="100px"
                                                                             Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoBimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>
                                                                      <td width="12%">
                                                                        <asp:RadioButton ID="ServTecnicoRellenoTrimestral" GroupName="FrecuenciaServRelleno" runat="server"   Enabled="false"
                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <telerik:RadDatePicker ID="ServTecnicoRellenoTrimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                             Culture="es-MX" Enabled="false" MinDate="01/01/0001"  
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoTrimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>                                                     
                                                                    </tr>
                                                                  </table>
                                                                </ItemTemplate>
                                                                 <EditItemTemplate>
                                                                 <table id="Table8" cellspacing="1" cellpadding="1" width="600" border="1">
                                                                    <tr>
                                                                      <td width="12%">                                                          
                                                                          <asp:RadioButton ID="ServTecnicoRellenoBimestralEdit" GroupName="FrecuenciaServRellenoEdit" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"
                                                                          Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                         <telerik:RadDatePicker ID="ServTecnicoRellenoBimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoBimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>
                                                                      <td width="12%">
                                                                        <asp:RadioButton ID="ServTecnicoRellenoTrimestralEdit" GroupName="FrecuenciaServRellenoEdit" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True" 
                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <telerik:RadDatePicker ID="ServTecnicoRellenoTrimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoTrimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>                                                     
                                                                    </tr>
                                                                  </table>     
                                                                  </EditItemTemplate>                                                                 
                                                              </telerik:GridTemplateColumn>
                                                              <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px"
                                                                ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Borrar este detalle?"
                                                                Text="Borrar" UniqueName="DeleteColumn">
                                                                <HeaderStyle Width="30px" />
                                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                    Width="30px" />
                                                            </telerik:GridButtonColumn>
                                                    </Columns>
                                                   
                                                </MasterTableView>
                                                <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                    NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                    PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>
                                                </td>                                         
                                            </tr>
                                        </table>        <%--FIN--%>
                                             <br />
                                        <table  width="95%">    <%--INICIO--%>
                                         <tr>
                                            <td width="10">  </td>
                                            <td>    <asp:Label ID="Label108" runat="server" Text="B) Mantenimiento Preventivo/Revisión"></asp:Label>    </td>
                                             </tr>
                                        <tr>                                          
                                            <td width="10">  </td>
                                            <td> <asp:CheckBox ID="ChkServMantenimiento" runat="server" Text="Requiere Servicio Mantenimiento Preventivo/Revisión"   OnCheckedChanged="ChkServMantenimiento_CheckedChanged" AutoPostBack="True" Checked="True" /></td>
                                        </tr>                                              
                                         <tr id="MantenimientoPreventivoListado" runat="Server">
                                                <td width="10"></td>
                                                <td>
                                                    <telerik:RadGrid ID="rgMantPrevRev" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."                                                        
                                                        OnItemDataBound="rgMantPrevRev_ItemDataBound" OnNeedDataSource="rgMantPrevRev_NeedDataSource"
                                                        PageSize="15"  OnItemCommand="rgMantPrevRev_ItemCommand" >
                                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Prd" EditMode="InPlace">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />                                                                
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Código" UniqueName="Id_Prd" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCodigoEdit" runat="server" Text='<%# Bind("Id_Prd") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <ClientEvents OnBlur="ObtenerNombreInicio" OnLoad="onLoadIdPrd" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label101" runat="server" Text='<%# Bind("Prd_Descripcion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoEdit" runat="server" Enabled="false" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="onLoadPrdDescr" />
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="400px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_AgrupadoSpo" Display="false" HeaderText="Prd_AgrupadoSpo"
                                                                    UniqueName="Prd_AgrupadoSpo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label82" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtAgrupadoSpoEdit" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Prd_InvInicial" HeaderText="Cantidad" UniqueName="Prd_InvInicial">
                                                                    <ItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%" Enabled="false">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidadEdit" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                <HeaderTemplate>
                                                  <table id="Table4" cellspacing="1" cellpadding="1" width="600" >
                                                    <tr>
                                                      <td colspan="6" align="center">
                                                        <b>Frecuencia</b>
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td width="12%">
                                                        <b>Mensual</b>
                                                      </td>
                                                      <td width="21%">
                                                        <b>Fecha Inicio</b>
                                                      </td>
                                                       <td width="12%">
                                                        <b>Bimestral</b>
                                                      </td>
                                                      <td width="21%">
                                                        <b>Fecha Inicio</b>
                                                      </td>     
                                                      <td width="12%">
                                                        <b>Trimestral</b>
                                                      </td>
                                                      <td width="21%">
                                                        <b>Fecha Inicio</b>
                                                      </td>                                                       
                                                    </tr>
                                                  </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                  <table id="Table5" cellspacing="1" cellpadding="1" width="600" border="1">
                                                    <tr>
                                                      <td width="12%">
                                                         <asp:RadioButton ID="ServMantenimientoMensual" runat="server" Text="" GroupName="FrecuenciaMantenimiento" Enabled = "false"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                         <telerik:RadDatePicker ID="ServMantenimientoMensualfechaIni" runat="server" Width="100px" 
                                                            Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoMensualfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoBimestral" runat="server" Text=""  GroupName="FrecuenciaMantenimiento"  Enabled = "false"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoBimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoBimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoTrimestral" runat="server" Text="" GroupName="FrecuenciaMantenimiento"  Enabled = "false"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoTrimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoTrimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                    </tr>
                                                  </table>
                                                </ItemTemplate>
                                                 <EditItemTemplate>
                                                  <table id="Table9" cellspacing="1" cellpadding="1" width="600" border="1">
                                                    <tr>
                                                      <td width="12%">
                                                         <asp:RadioButton ID="ServMantenimientoMensualEdit" runat="server" Text="" GroupName="FrecuenciaMantenimiento" AutoPostBack="True" OnCheckedChanged="Rb_CheckedChanged" 
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                         <telerik:RadDatePicker ID="ServMantenimientoMensualEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoMensualfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoBimestralEdit" runat="server" Text=""  GroupName="FrecuenciaMantenimiento" AutoPostBack="True" OnCheckedChanged="Rb_CheckedChanged"
                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoBimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoBimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoTrimestralEdit" runat="server" Text="" GroupName="FrecuenciaMantenimiento" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoTrimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoTrimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                    </tr>
                                                  </table>
                                                </EditItemTemplate>
                                              </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px"
                                                        ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Borrar este detalle?"
                                                        Text="Borrar" UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="30px" />
                                                    </telerik:GridButtonColumn>
                                                    </Columns>
                                                           
                                                        </MasterTableView>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                            NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                            PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                    </telerik:RadGrid>
                                                </td>                                         
                                            </tr>
                                             <tr>
                                                <td>&#160;</td>
                                            </tr>
                                        </table>
                                            <br />
                                        <br />
                                        <br />
                                        <br />
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>                     

                            <telerik:RadPageView ID="RPVOtrosApoyos" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter6" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane6" runat="server" OnClientResized="onResize">
                                    <div runat="server" id="divOtrosApoyos" style="font-family: verdana; font-size: 8pt">  
                                            <table width="100%">                                                
                                                <tr>
                                                    <td width="10">  </td>
                                                    <td>Notas:  </td>
                                                    <td colspan = "6">
                                                        <asp:TextBox id="txtNotas" TextMode="multiline" Columns="170" Rows="8" runat="server" />
                                                    </td>
                                                </tr>                                                  
                                            </table>
                                              <table width="100%"> 
                                            <thead >                                                        
                                                 <th style="font-family:  verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;"   >  PERSONAL DE KEY QUE LO PUEDE ANTENDER </th>  
                                            </thead> 
                                            </table>

                                            <table>                                               
                                                <tr>
                                                    <td width="10">  </td>
                                                    <td> <asp:Label ID="Label31" runat="server" Text="Representante de Ventas" /></td>
                                                    <td>
                                                        <telerik:RadComboBox ID="ContactoRepVenta" runat="server" Width="250px" AutoPostBack="True"
                                                        Filter="Contains" Style="cursor: hand"  OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator40" ControlToValidate="ContactoRepVenta"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />                                                    
                                                     </td>
                                                    <td></td>
                                                    <td> <asp:Label ID="Label66" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadNumericTextBox ID="ContactoRepVentaTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="ContactoRepVentaTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                    <td> </td>
                                                    <td><asp:Label ID="Label72" runat="server" Text="E-mail" /> </td>
                                                    <td>  <telerik:RadTextBox ID="ContactoRepVentaEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox>  </td>
                                                    <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ContactoRepVentaEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ContactoRepVentaEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label73" runat="server" Text="Representante de Servicio al Cliente" /> </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="ContactoRepServ" runat="server" Width="250px" AutoPostBack="True"
                                                        Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator35" ControlToValidate="ContactoRepServ"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />         
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label74" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadNumericTextBox ID="ContactoRepServTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>  
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ContactoRepServTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label77" runat="server" Text="E-mail" /> </td>
                                                    <td> <telerik:RadTextBox ID="ContactoRepServEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox>  
                                                      </td>
                                                    <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ContactoRepServEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ContactoRepServEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label78" runat="server" Text="Jefe Servicio a Cliente" />
                                                    </td> <td>
                                                        <telerik:RadComboBox ID="ContactoJefServ" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator32" ControlToValidate="ContactoJefServ"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />  
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label79" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadNumericTextBox ID="ContactoJefServTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="ContactoJefServTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                    <td></td>
                                                    <td> <asp:Label ID="Label80" runat="server" Text="E-mail" /> </td>
                                                    <td> <telerik:RadTextBox ID="ContactoJefServEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> </td>
                                                    <td> 
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="ContactoJefServEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ContactoJefServEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>   
                                                    </td>
                                                </tr>                                               
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label84" runat="server" Text="Asesor de Servicio" /> </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="ContactoAseServ" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29" ControlToValidate="ContactoJefServ"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />  
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label85" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoAseServTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>  
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ContactoAseServTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label86" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoAseServEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="ContactoAseServEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ContactoAseServEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label87" runat="server" Text="Jefe de Operaciones" /> </td>
                                                    <td> 
                                                         <telerik:RadComboBox ID="ContactoJefOper" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand"  OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" ControlToValidate="ContactoJefOper"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />  
                                                    </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label88" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoJefOperTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ContactoJefOperTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                     </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label89" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoJefOperEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> 
                                                    </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="ContactoJefOperEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ContactoJefOperEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label90" runat="server" Text="Coordinador de Almacén Y Rep." /> </td>
                                                    <td> 
                                                           <telerik:RadComboBox ID="ContactoCAlmRep" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" ControlToValidate="ContactoCAlmRep"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />  
                                                     </td>
                                                    <td> </td>
                                                    <td><asp:Label ID="Label91" runat="server" Text="Teléfono" />
                                                    </td><td>
                                                        <telerik:RadNumericTextBox ID="ContactoCAlmRepTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ContactoCAlmRepTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>    
                                                    </td>
                                                    <td></td>
                                                    <td>  <asp:Label ID="Label92" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoCAlmRepEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> 
                                                     </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="ContactoCAlmRepEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ContactoCAlmRepEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>  
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label93" runat="server" Text="Coordinador de Servicio Téncnico" /> </td>
                                                    <td> 
                                                         <telerik:RadComboBox ID="ContactoCServTec" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" ControlToValidate="ContactoCServTec"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />                                                          
                                                     </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label94" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoCServTecTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>  
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ContactoCServTecTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                    </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label95" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoCServTecEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> 
                                                         
                                                    </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="ContactoCServTecEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>  
                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ContactoCServTecEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                </tr>

                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label44" runat="server" Text="Coordinador de Crédito y Cobranza" /> </td>
                                                    <td> 
                                                         <telerik:RadComboBox ID="ContactoCCreCob" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand"  OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                     </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label46" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoCCreCobTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ContactoCCreCobTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
                                                     </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label81" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoCCreCobEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="ContactoCCreCobEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>  
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ContactoCCreCobEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td width="180"></td>
                                                    <td width="257"></td>
                                                    <td width="5"></td>
                                                    <td width="60"></td>
                                                    <td width="100"></td>
                                                    <td width="5"></td>
                                                    <td width="40"></td>
                                                    <td width="100"></td>
                                                    <td></td>
                                                </tr>
                                            </table>                                            
                                            <table width="100%"> 
                                            <thead>  
                                                <tr>                                                      
                                                <th style="font-family:  verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;"   >  CONTACTOS DEL CLIENTE </th>
                                                </tr>  
                                            </thead> 
                                            <tbody>
                                            <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                            </tr>
                                            </tbody>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="10">   </td>
                                                <td>  </td>
                                                <td>  </td>
                                                    
                                                <td width="10">
                                                </td>
                                                    <td>  </td>
                                                <td>  </td>
                                                   
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="10">  </td>
                                                <td> <asp:Label ID="Label19" runat="server" Text="Pagos" /></td>
                                                <td><telerik:RadTextBox ID="txtContactoClientePagos" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox></td>
                                                <td>                                                    </td>
                                                <td> <asp:Label ID="Label11" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadNumericTextBox ID="txtContactoClientePagosTel" runat="server" Width="100px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>  </td>
                                                <td> </td>
                                                <td><asp:Label ID="Label12" runat="server" Text="E-mail" /> </td>
                                                <td>  <telerik:RadTextBox ID="txtContactoClientePagosEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox>  </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtContactoClientePagosEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator>  </td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label10" runat="server" Text="Compras" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientecompra" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label14" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadNumericTextBox ID="txtContactoClientecompraTel" runat="server" Width="100px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>  </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label15" runat="server" Text="E-mail" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientecompraEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox>   </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtContactoClientecompraEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label13" runat="server" Text="Almacén" />
                                                </td> <td>
                                                    <telerik:RadTextBox ID="txtContactoClientealmacen" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label17" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadNumericTextBox ID="txtContactoClientealmacenTel" runat="server" Width="100px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox> </td>
                                                <td></td>
                                                <td> <asp:Label ID="Label18" runat="server" Text="E-mail" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientealmacenEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox> </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactoClientealmacenEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label16" runat="server" Text="Mantenimiento" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClienteMantenimiento" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td><asp:Label ID="Label20" runat="server" Text="Teléfono" />
                                                </td> <td>
                                                    <telerik:RadNumericTextBox ID="txtContactoClienteMantenimientoTel" runat="server" Width="100px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox></td>
                                                <td></td>
                                                <td> <asp:Label ID="Label21" runat="server" Text="E-mail" /> </td>
                                                <td><telerik:RadTextBox ID="txtContactoClienteMantenimientoEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox> </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtContactoClienteMantenimientoEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator> </td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label22" runat="server" Text="Otros" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClienteOtro" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>  </td>
                                                <td> </td>
                                                <td>   <asp:Label ID="Label23" runat="server" Text="Teléfono" />
                                                </td> <td>
                                                    <telerik:RadNumericTextBox ID="txtContactoClienteOtroTel" runat="server" Width="100px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>  </td>
                                                <td>  </td>
                                                <td>  <asp:Label ID="Label24" runat="server" Text="E-mail" />   </td>
                                                <td>    <telerik:RadTextBox ID="txtContactoClienteOtroEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox> </td>
                                                <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtContactoClienteOtroEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator>  
                                                         
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td width="180"></td>
                                                <td width="257"></td>
                                                <td width="5"></td>
                                                <td width="60"></td>
                                                <td width="100"></td>
                                                <td width="5"></td>
                                                <td width="40"></td>
                                                <td width="100"></td>
                                                <td></td>
                                            </tr>
                                        </table>                                        
  
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                        </telerik:radmultipage>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                </td>
            </tr>
        </table>
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;

                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }


            function pre_validarfecha() {
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                if (txtFecha.get_enabled()) {
                    _ValidarFechaEnPeriodo();
                }
            }
            var txtproductoId;
            var cmbproductoDesc;

            function Id_OnLoad(sender, args) {
                txtproductoId = sender;
            }

            function Desc_OnLoad(sender, args) {
                cmbproductoDesc = sender;
            }
            function txtId_OnBlur(sender, args) {
                OnBlur(sender, cmbproductoDesc);
            }

            function cmbDesc_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtproductoId);
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRepresentante.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentante.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }

            function txt4_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRutaServicio.ClientID %>'));
            }

            function cmb4_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRutaServicio.ClientID %>'));
            }

            function txt5_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRutaEntrega.ClientID %>'));
            }

            function cmb5_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRutaEntrega.ClientID %>'));
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }

            function TabSelected(sender, args) {
                var idcte = $find('<%= txtCliente.ClientID %>');
                var idrik = $find('<%= txtRepresentante.ClientID %>');
                var idter = $find('<%= txtTerritorio.ClientID %>');
                var cte = $find('<%= txtClienteNombre.ClientID %>');
                var fol = $find('<%= txtFolio.ClientID %>');
                var rik = $find('<%= cmbRepresentante.ClientID %>');
                //debugger;
                /* if (idcte.get_value() == '' || idrik.get_value() == '') {
                // radalert('Favor de seleccionar territorio, representante y cliente</br></br>', 330, 150);
                //args.set_cancel(true);
                }
                else {*/
                var ter_Asesorias = $find('<%= txtTerritorio.ClientID %>');
                var idrik_Asesorias = $find('<%= txtRepresentante.ClientID %>');
                var rik_Asesorias = $find('<%= txtRepresentante.ClientID %>');
                var idcte_Asesorias = $find('<%= txtCliente.ClientID %>');
                var cte_Asesorias = $find('<%= txtCliente.ClientID %>');
                var fol_Asesorias = $find('<%= txtFolio.ClientID %>');


                ter_Asesorias.set_value(idter.get_value());
                idrik_Asesorias.set_value(idrik.get_value());
                rik_Asesorias.set_value(rik.get_text());
                idcte_Asesorias.set_value(idcte.get_value());
                cte_Asesorias.set_value(cte.get_value());
                fol_Asesorias.set_value(fol.get_value());


                //}
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;

                var button = args.get_item();
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                if (button.get_value() == 'save') {
                    for (i = 0; i < Page_Validators.length; i++) {
                        if (!Page_Validators[i].isvalid) {
                            if ('CPH_RequiredFieldValidator15' != Page_Validators[i].id) {
                                radTabStrip.get_allTabs()[0].select()
                            }
                            else {
                                radTabStrip.get_allTabs()[1].select()
                            }
                        }
                    }
                }
            }
            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function popup2(Id_Prd, Id_Acs) {
                var oWnd = radopen("Ventana_PrdAcys.aspx?Id_Prd=" + Id_Prd + "&Id_Acs=" + Id_Acs, "AbrirVentana_PrdAcys");
                oWnd.center();
            }
            function ClienteSeleccionado() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('cliente');
            }
            function ProductosSeleccionados() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('productos');
            }
            function ObtenerNombre(prd) {
                var urlArchivo = 'ObtenerNombre.aspx';
                parametros = "prd=" + prd;
                parametros = parametros + "&Bi=true";
                return obtenerrequest(urlArchivo, parametros);
            }
            var txtId;
            var txtDes;

            function ObtenerNombreInicio() {
                //debugger;

                var actual;

                if (txtId.get_value() == '') {
                    actual = '';
                }
                else {
                    actual = ObtenerNombre(txtId.get_value());
                    if (actual == "-0") {
                        window.location.href("Login.aspx");
                    }
                    else if (actual == "-2") {
                        actual = '';
                        txtId.set_value(actual);
                        AlertaFocus('El producto debe ser un aparato de sistema propietario', txtId._clientID);
                        //radalert('El producto debe ser un aparato de sistema propietario', 330, 150);
                    }
                }
                txtDes.set_value(actual);
            }
            function onLoadIdPrd(sender)
            { txtId = sender; }
            function onLoadPrdDescr(sender)
            { txtDes = sender; }         
        </script>
    </telerik:radcodeblock>
</asp:Content>
