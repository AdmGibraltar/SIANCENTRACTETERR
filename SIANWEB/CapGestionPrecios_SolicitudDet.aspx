﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapGestionPrecios_SolicitudDet.aspx.cs" Inherits="SIANWEB.CapGestionPrecios_SolicitudDet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
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
                    //debugger;
                    GetRadWindow().Close();
                }

                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind() {
                    //debugger;
                    GetRadWindow().Close();
                    //GetRadWindow().BrowserWindow.refreshGrid();
                }
                function CloseWindowA(mensaje) {
                    //debugger;
                    var cerrarWindow = radalert(mensaje, 330, 150);
                    cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
                }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RAM1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="CmbCentro">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ImageButton1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" >
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />
                    <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                        ImageUrl="~/Imagenes/blank.png" />
                </Items>
            </telerik:RadToolBar>
            <table id="TblEncabezado" 
                style="font-family: verdana; font-size: 8pt; visibility: hidden;" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                        <asp:HiddenField runat="server" ID="HFId_PC"/>
                        <asp:HiddenField runat="server" ID="HFCapUsuario"/>
                        <asp:HiddenField runat="server" ID="HFId_Sol"/>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                         
                            <tr>
                                <td>
                                   <asp:Label ID="Label3" runat="server" Text="Folio:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                  <asp:Label ID="LblId_Sol" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Nosol" runat="server" Text="Sucursal:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblCd_Nombre" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                                   <tr>
                                <td>
                                   <asp:Label ID="Label1" runat="server" Text="Solicitante:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblU_Nombre" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                                   <tr>
                                <td>
                                   <asp:Label ID="Label2" runat="server" Text="Correo:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblU_Correo" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                                   <tr>
                                <td>
                                   <asp:Label ID="Label4" runat="server" Text="Fecha solicitud:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblSol_Fecha" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                                        <tr>
                                <td>
                                   <asp:Label ID="Label5" runat="server" Text="Convenio:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblPC_NoConvenio" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                                    <tr>
                                <td>
                                   <asp:Label ID="Label6" runat="server" Text="Nombre convenio:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblPC_Nombre" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                                      <tr>
                                <td>
                                   <asp:Label ID="Label7" runat="server" Text="Categoría:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblId_CatStr" runat="server" ></asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                        </table>
                        <br />
                        <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="270px" BorderSize="0" >
                           <telerik:RadPane ID="RadPane3" runat="server"  Height="250px" width="850px"
                                                BorderStyle="None">
                        <telerik:RadGrid ID="rgSolicitudDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                               OnNeedDataSource="rgSolicitudDet_NeedDataSource" OnItemCommand="rgSolicitudDet_ItemCommand" OnInsertCommand="rgSolicitudDet_InsertCommand"
                                        OnUpdateCommand="rgSolicitudDet_UpdateCommand" OnDeleteCommand="rgSolicitudDet_DeleteCommand"
                                         OnItemCreated="rgSolicitudDet_ItemCreated">
                       
                              <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                 <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="Id_Unique" HeaderText="Id_Unique" UniqueName="Id_Unique"
                                                    Display="false">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="LblId_UniqueE" runat="server" Text='<%# Eval("Id_Unique") %>' /></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblId_Unique" runat="server" Text='<%# Eval("Id_Unique") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                               <telerik:GridTemplateColumn DataField="Id_Cte" HeaderText="Núm. Cliente" UniqueName="Id_Cte" >
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtId_Cte" runat="server" MaxLength="9" Width="70px"
                                                            MinValue="1"  Text='<%# Eval("Id_Cte") %>' style="text-align:right">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Id_Cte" runat="server" Text='<%# Eval("Id_Cte") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                      </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn DataField="Sol_CteNombre" HeaderText="Cliente" UniqueName="Sol_CteNombre" >
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="TxtSol_CteNombre" runat="server" MaxLength="400" Width="200px"
                                                          Text='<%# Eval("Sol_CteNombre") %>'  >
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSol_CteNombre" runat="server" Text='<%# Eval("Sol_CteNombre") %>'></asp:Label>
                                                     </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                      </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Id_Ter" HeaderText="Núm Terr." UniqueName="Id_Ter" >
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtId_Ter" runat="server" MaxLength="9" Width="70px"
                                                            MinValue="1"  Text='<%# Eval("Id_Ter") %>' style="text-align:right">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Id_Ter") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                      </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn DataField="SolTer_Nombre" HeaderText="Territorio" UniqueName="SolTer_Nombre" >
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="TxtSolTer_Nombre" runat="server" MaxLength="400" Width="200px"
                                                          Text='<%# Eval("SolTer_Nombre") %>'  >
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("SolTer_Nombre") %>'></asp:Label>
                                                     </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                      </telerik:GridTemplateColumn>
                                           <telerik:GridTemplateColumn DataField="Sol_UsuFinal" HeaderText="Usuario final" UniqueName="Sol_UsuFinal" >
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="TxtSol_UsuFinal" runat="server" MaxLength="400" Width="90px"
                                                          Text='<%# Eval("Sol_UsuFinal") %>' >
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSol_UsuFinal" runat="server" Text='<%# Eval("Sol_UsuFinal") %>'></asp:Label>
                                                     </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                      </telerik:GridTemplateColumn>
                                              <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                                            <HeaderStyle Width="70px" />
                                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                 </telerik:GridEditCommandColumn>
                                                  <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                                            UniqueName="DeleteColumn">
                                                                            <HeaderStyle Width="50px" />
                                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                   </telerik:GridButtonColumn>
                                 
                                </Columns>
                                    
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        </telerik:RadPane>
                          </telerik:RadSplitter>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
