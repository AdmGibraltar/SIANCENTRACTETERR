<%@ Page Title="Gestión precios" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosD.aspx.cs" Inherits="SIANWEB.CapGestionPreciosD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }


            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
               // debugger;
               var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
            //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid();
            }

            function CloseWindowA(mensaje) {
                debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                GetRadWindow();
                            });
                        }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function refreshGrid() {

            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" ScrollBars="Vertical">
    </telerik:RadAjaxLoadingPanel>
          <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" 
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="True">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="rgConvenioDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal"  style=" overflow:scroll; ">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="120%" dir="rtl" OnButtonClick="rtb1_ButtonClick" style="margin-right: 0">
            <Items>
                <telerik:RadToolBarButton CommandName="guardar" Value="guardar" ToolTip="Guardar" CssClass="save"  ValidationGroup="guardar"
                ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <table id="TblEncabezado" 
            style="font-family: verdana; font-size: 8pt; width: 99%;" runat="server">
            <tr>
                <td class="style2">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 7pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                     <tr>
                            <td>
                           
                            
                           
                                &nbsp;</td>
                            <td>
                       
                                &nbsp;</td>
                            <td>
                              
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            <td>
                               <asp:Label ID="LblConvAnt" Text="Convenio ant." runat="server"  Visible="false">
                                </asp:Label>
                               </td>
                               <td>
                                <asp:Label ID="TxtConvAnt" Text="LblConvAnterior" runat="server" Visible="false">
                                </asp:Label>
                               </td>
                            <td >
                       
                            </td>
                        </tr>


                      <tr>
                            <td class="style1">
                           
                            
                           
                               <asp:Label ID="Label4" Text="Categoría" runat="server">
                                </asp:Label>
                           
                            </td>
                            <td class="style1">
                                 <telerik:RadComboBox ID="CmbId_Cat" MaxHeight="300px" runat="server" 
                        Width="150px" autopostback="true" 
                                     onselectedindexchanged="CmbId_Cat_SelectedIndexChanged" >
                    </telerik:RadComboBox>
                            </td>
                            <td class="style1">
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="CmbId_Cat"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                 </td>
                            <td class="style1">
                               <asp:Label ID="Label5" Text="Convenio" runat="server">
                                </asp:Label>
                              </td>
                            
                            <td class="style1">
                               <telerik:RadTextbox runat="server"  ID="TxtPC_NoConvenio" MaxHeight="400px"   Width="150px" >
                            </telerik:RadTextbox>
                         
                              </td>
                  
                              <td  >
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                        ControlToValidate="TxtPC_NoConvenio" Display="Dynamic" ErrorMessage="*Requerido"
                                                        ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                                <tr>
                            <td>
                            <asp:Label ID="LblLider" Text="Líder Técnico" runat="server">
                                </asp:Label>
                               </td>
                            <td>
                                   <telerik:RadComboBox ID="CmbId_ULider" MaxHeight="400px" runat="server" 
                        Width="150px" >
                    </telerik:RadComboBox>
                                 </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CmbId_ULider"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label1" Text="Nombre" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                  <telerik:RadTextbox runat="server"  ID="TxtPC_Nombre" MaxHeight="300px"   Width="150px" >
                            </telerik:RadTextbox>
                              </td>
                                  <td  width="50%">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="TxtPC_Nombre" Display="Dynamic" ErrorMessage="*Requerido"
                                                        ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                          <tr>
                            <td >
                            <asp:Label ID="LblEjecutivo" Text="Ejecutivo de cuenta" runat="server">
                                </asp:Label>
                              </td>
                            <td>
                                <telerik:RadComboBox ID="CmbId_UEjecutivo" MaxHeight="400px" runat="server" 
                        Width="150px" >
                    </telerik:RadComboBox>
                             
                             </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CmbId_UEjecutivo"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
   <td>
                                  <asp:Label ID="Label2" Text="Margen de convenio %" runat="server" 
                                      Visible="False"></asp:Label>
                            </td>
                            <td>
                                   <telerik:RadNumericTextbox runat="server"  ID="TxtPC_Margen" 
                                       MaxHeight="300px"   Width="70px" Visible="False" >
                                    <NumberFormat  allowrounding="False" decimaldigits="2"></NumberFormat>
                            </telerik:RadNumericTextbox>
                            </td>
                        </tr>
                               <tr>
                            <td  >
                            <asp:Label ID="Label3" Text="Última modificación" runat="server">
                                </asp:Label>
                            </td>
                          <td>
                                  <asp:Label ID="TxtPC_FechaCreo"  runat="server">
                                </asp:Label>
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
                       
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" Text="Notas:" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                              <td >
                       
                            </td>
                        </tr>
                        <tr>
                        <td colspan ="4">
                           <telerik:RadTextbox runat="server"  ID="TxtPC_Notas" Height="100px"   
                                Width="500px" MaxLength="500" TextMode="MultiLine" >
                            </telerik:RadTextbox>
                        </td>
                        <td colspan="2">
                    
                        </td>
                        </tr>
                        <tr>
                        <td colspan = "6">
                              
                              
                <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" 
                                LoadingPanelID="RadAjaxLoadingPanel1" width="978px" height ="400px" 
                                HorizontalAlign="NotSet" >
                <asp:Label ID="Label8" runat="server"></asp:Label>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="buttonSubmit"  >
                    <table>
                        <tr>

                            <td colspan="3">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload1" AllowedFileExtensions="xls,xlsx"
                                    Height="25px" Width="200px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30">
                                    <Localization Remove="Quitar" Select="Examinar.." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="ValidFiles" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                            <tr>
                         <td> &nbsp;</td>
                            <td colspan="2">
                                <asp:Button ID="buttonSubmit" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    Style="margin-top: 6px" OnClick="btnImportar_Click" />
                            </td>
                            <td>
                             <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                   
                       <table>
                         <tr>
                            <td>
                               <telerik:RadGrid ID="rgConvenioDet" runat="server" AutoGenerateColumns ="false" GridLines="None"
                                EnableLinqExpressions="False"  autopostback="True"  OnItemCommand="rgConvenioDet_ItemCommand" 
                                PageSize="15"  width="100%" OnPageIndexChanged = "rgConvenioDet_PageIndexChanged"
                                AllowPaging="True"   MasterTableView-NoMasterRecordsText="No se encontraron registros.">

                                <MasterTableView ClientDataKeyNames="Id_Prd">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                      <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Cancelar"  ConfirmDialogType="RadWindow"
                                           ConfirmText="¿Está seguro que desea eliminar el producto?" ConfirmDialogHeight="150px"
                                            ConfirmDialogWidth="350px"
                                           Text="Cancelar" UniqueName="Cancelar" Display="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>

                        
                                      <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Clave Key" UniqueName="Id_Prd" >
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="PCD_ClaveProv" HeaderText="Clave Proveedor" UniqueName="PC_NoConvenio">
                                            <HeaderStyle Width="80px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Descripción" UniqueName="Prd_Descripción">
                                            <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_PrecioVtaMin" HeaderText="P. Venta Min." UniqueName="PCD_PrecioVtaMin">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_PrecioVtaMax" HeaderText="P. Venta Max." UniqueName="PCD_PrecioVtaMax">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="PCD_CantidadMax" HeaderText="Cantidad max." UniqueName="PCD_CantidadMax">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="Id_Moneda" HeaderText="Id_Moneda" UniqueName="Id_Moneda" Display ="False">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_MonedaStr" HeaderText="Moneda" UniqueName="Id_MonedaStr" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="PCD_CatDesp" HeaderText="Cat. Despachador" UniqueName="PCD_CatDesp" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn DataField="PCD_PrecioAAAEsp" HeaderText="<b>Anterior</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEsp" Display = "false"
                                               DataFormatString="{0:N2}" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicio" HeaderText="<b>Anterior</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicio"  Display = "false"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspA" HeaderText="<b>Anterior</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEspA" 
                                           DataFormatString="{0:N2}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicioA" HeaderText="<b>Anterior</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicioA"  
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="PCD_FechaFinA"   UniqueName="PCD_FechaFinA" Display ="false"  
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEsp" HeaderText="<b>Actual</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEspB"  
                                                DataFormatString="{0:N2}" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicio" HeaderText="<b>Actual</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicioB" 
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="PCD_FechaFin"  UniqueName="PCD_FechaFin" Display="false" 
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspC" HeaderText="<b>Futuro</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEspC"  
                                                DataFormatString="{0:N2}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicioC" HeaderText="<b>Futuro</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicioC"  
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="PCD_FechaFinC"  UniqueName="PCD_FechaFinC" Display="false"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PCD_FechaFinVer" HeaderText="Fecha fin" UniqueName="PCD_FechaFinVer"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                    <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>                                   
                            </td>
                       
                        </tr>
                    </table>
                   

                </asp:Panel>
            </telerik:RadAjaxPanel>
                        </td>
                        </tr>
                    </table>
                 
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style3">
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                    <asp:HiddenField ID="HFCat_Consecutivo" runat="server" />
                    <asp:HiddenField ID="HFId_PC" runat="server" />
                    <asp:HiddenField ID="HFTipoOp" runat="server" />


                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
    
        .style1
        {
            width: 1057px;
        }
        .style2
        {
            width: 839px;
        }
        .style3
        {
            height: 8px;
        }
    
    </style>
</asp:Content>

