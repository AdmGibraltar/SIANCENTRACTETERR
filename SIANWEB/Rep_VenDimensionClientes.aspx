<%@ Page Title="Dimensión de clientes" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_VenDimensionClientes.aspx.cs" Inherits="SIANWEB.Rep_VenDimensionClientes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function refreshGrid() {

            }
            
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="imprimir" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="imprimir" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
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
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblUEN" runat="server" Text="UEN"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtUen" runat="server" MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtTerritorio" runat="server" MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente "></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCliente" runat="server" MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSegmento" runat="server" Text="Segmento"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtSegmento" runat="server" MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>
