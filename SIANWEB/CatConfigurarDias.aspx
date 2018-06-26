<%@ Page Title="Configuración días" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatConfigurarDias.aspx.cs" Inherits="SIANWEB.CatConfigurarDias" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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
            function CloseAndRebind(param) {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.FisicoTerminado(param);
            }

        </script>
        <style type="text/css">
            .ruBrowse
            {
                background-position: 0 -23px !important;
                width: 80px !important;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbNivel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="CmbCDI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="rgDias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="formulario">
         <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" >
            <Items>
                 <telerik:RadToolBarButton Width="20px" Enabled="False" />
                 <telerik:RadToolBarButton CommandName="Guardar" Value="Guardar"  CssClass="save"
                ToolTip="Guardar" ImageUrl="Imagenes/blank.png" />
        
            </Items>
        </telerik:RadToolBar>
        <div runat="server" id="divPrincipal" style="margin-left: 10px; margin-right: 10px;
            margin-top: 10px;">
           <asp:Label ID="lblMensaje" runat="server"></asp:Label>
         <br />  <br />
            <table>
            <tbody>
          
            <tr>
            <td>
              <asp:Label ID="Label1" Text="Aplica a" runat="server"  >
                                </asp:Label>
            </td>
            <td>
              <telerik:RadComboBox ID="CmbNivel" runat="server" Width="150px" OnSelectedIndexChanged="CmbNivel_SelectedIndexChanged" autopostback="true" >
                 <Items>
                                   <telerik:RadComboBoxItem runat="server" Text="--Todos--" Value="1" Selected= "True" />
                                   <telerik:RadComboBoxItem runat="server" Text="Sucursal" Value="2"  />
                                   <telerik:RadComboBoxItem runat="server" Text="Representante" Value="3"  />
                  </Items>
               </telerik:RadComboBox>
            </td>
            <td>
            </td>
            </tr>
             <tr>
                <td>
                <asp:Label ID="Label3" Text="CDI" runat="server"  >
                </asp:Label>&nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="CmbCdi" runat="server" Width="150px" autopostback="true" OnSelectedIndexChanged="CmbCDI_SelectedIndexChanged"  >
                </telerik:RadComboBox>
            </td>
            <td>
                            </td>
              </tr>
                               <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Representante" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="CmbRepresentante" runat="server" Width="150px" autopostback="true"  >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                            <tr>
                            <td>
                                <asp:Label ID="Label2" Text="Tipo de justificación" runat="server" Enabled = "false">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="CmbTipo" runat="server" Width="150px"   >
                                <items>
                                   <telerik:RadComboBoxItem runat="server" Text="--Seleccionar--" Value="-1" Selected= "True" />
                                   <telerik:RadComboBoxItem runat="server" Text="Asueto" Value="1"  />
                                   <telerik:RadComboBoxItem runat="server" Text="Curso" Value="2"  />
                                   <telerik:RadComboBoxItem runat="server" Text="Incapacidad" Value="3"  />
                                   <telerik:RadComboBoxItem runat="server" Text="Permiso" Value="4"  />
                                   </items>
                                </telerik:RadComboBox>

                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                                <asp:Label ID="LblFechaIni" Text="Fecha inicio" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                        <telerik:RadDatePicker ID="rdFechaIni" runat="server" Width="100px" Culture="es-MX">
                                    <Calendar ID="Calendar1" runat="server">
                                        <ClientEvents />
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                        </tr>
                                  <tr>
                            <td>
                                <asp:Label ID="Label5" Text="Fecha fin" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                        <telerik:RadDatePicker ID="rdFechaFin" runat="server" Width="100px" Culture="es-MX">
                                    <Calendar ID="Calendar1" runat="server">
                                        <ClientEvents />
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                                <asp:Label ID="LblComentario" Text="Comentarios" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <asp:TextBox runat="Server" ID="TxtDF_Comentario" TextMode = "Multiline"  Width = "230px"
                                height = "80px" MaxLength = "400"
                                >
                                </asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
     </tbody>
            </table>
            <table>
            <br />
            <br />
            <tbody>
            <tr>
            <td >
                   <telerik:RadGrid ID="rgDias" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" 
                                PageSize="10" 
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand = "rg_ItemCommand"
                                Width="900px"   >
                                <MasterTableView ClientDataKeyNames="Id_DF">
                           
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_DF" HeaderText="Id_DF" UniqueName="Id_DF"
                                            Display="false">
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DF_FechaIni" HeaderText="Fecha ini." UniqueName="DF_FechaIni"  DataFormatString="{0:dd/MM/yy}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DF_FechaFin" HeaderText="Fecha fin" UniqueName="DF_FechaFin"  DataFormatString="{0:dd/MM/yy}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DF_NivelStr" HeaderText="Nivel" UniqueName="DF_NivelStr" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CD_Nombre" HeaderText="CDI" UniqueName="CD_Nombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="DF_RepNombre" HeaderText="Representante" UniqueName="DF_RepNombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DF_TipoStr" HeaderText="Tipo" UniqueName="DF_TipoStr">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="DF_Comentario" HeaderText="Tipo" UniqueName="DF_Comentario">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Eliminar" 
                                           Text="Eliminar" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
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
            </td>
            </tr>
            </tbody>
            </table>
        </div>
    </div>
</asp:Content>
