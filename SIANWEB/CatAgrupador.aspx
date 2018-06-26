<%@ Page Title="Agrupadores" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatAgrupador.aspx.cs" Inherits="SIANWEB.CatAgrupador" %>

<%--rm--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
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
            <telerik:AjaxSetting AjaxControlID="CheckBox1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server" style="font-family: verdana; font-size: 8pt;">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
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
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución" />
                </td>
             <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server"
                        Width="150px">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hiddenActualiza" runat="server" />
        <asp:HiddenField ID="HdId_Agp" runat="server" />
        <table style="width: 518px">
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <!--Tab 1  Tabla 1-->
                        <tr>
                            <td style="width: 70px">
                                &nbsp;</td>
                            <td class="style1">
                                <%--<asp:TextBox ID="txtRegion" Width="46px" runat="server" ></asp:TextBox>--%>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="Label3" runat="server" Text="Descripción" />
                            </td>
                            <td colspan="2">
                                <!-- Pendiente-->
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion2" runat="server" Width="300px"
                                    AutoPostBack="false" MaxLength="400">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorReg2" runat="server" ErrorMessage="RequiredFieldValidator"
                                    ControlToValidate="txtDescripcion2" Display="Static" ValidationGroup="guardar"
                                    ForeColor="#FF0000" Text="*Requerido">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                <telerik:RadGrid ID="rgAgrupador" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rgAgrupador_NeedDataSource" EnableLinqExpressions="False" 
                                PageSize="10" 
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                OnPageIndexChanged="rgAgrupador_PageIndexChanged" OnItemCommand = "rgAgrupador_ItemCommand"
                                Width="500px"   >
                                <MasterTableView ClientDataKeyNames="Id_Agp">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Agp" HeaderText="Id_Agp" UniqueName="Id_Agp"
                                            Visible="false">
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                           
                                        <telerik:GridBoundColumn DataField="Ag_Descripcion" HeaderText="Agrupador" UniqueName="Ag_Descripcion">
                                            </telerik:GridBoundColumn>
                                        
                                        <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" 
                                           Text="Editar" UniqueName="Editar" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
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
                            </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
