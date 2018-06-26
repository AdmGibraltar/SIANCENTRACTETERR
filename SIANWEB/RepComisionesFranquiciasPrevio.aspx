<%@ Page Title="Reporte de Comisiones Franquicias" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="RepComisionesFranquiciasPrevio.aspx.cs" Inherits="SIANWEB.RepComisionesFranquiciasPrevio" %>

 <asp:Content ID="Content3" ContentPlaceHolderID="CPH" runat="server">
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

            function TabSelected(sender, args) {

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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbanio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbmes">
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
                  <telerik:AjaxSetting AjaxControlID="RblTipoRep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
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
					   
				        <tr runat ="Server" id="trAnio"  >
							<td>
								<asp:Label ID="lblanio" Text="Año" runat="server">
								</asp:Label>&nbsp;
							</td>
							<td>
								<telerik:RadComboBox ID="cmbanio" runat="server" Width="150px" >
								</telerik:RadComboBox>
							</td>
							<td>
							</td>
						</tr>
						<tr runat ="Server" id="trMes"  >
							<td>
								<asp:Label ID="Label4" Text="Mes" runat="server">
								</asp:Label>&nbsp;
							</td>
							<td>
								<telerik:RadComboBox ID="cmbmes" runat="server" Width="150px" >
								</telerik:RadComboBox>
							</td>
							<td>
							</td>
						</tr>

                       

                          <tr>
                            <td>
                                <asp:Label ID="LblId_Cd" runat="server" Text="Sucursal"></asp:Label>
                            </td>
                            <td colspan="9">
                            <telerik:RadNumericTextBox ID="txtCdi_Id" runat="server" MaxLength="9" MinValue="0"
			                                    Width="50px" >
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
 
                            </td>
                            <td>
                            <telerik:RadComboBox ID="CmbId_Cd" runat="server" AutoPostBack="true" CausesValidation="False"
                                    ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                    OnSelectedIndexChanged="CmbId_Cd_SelectedIndexChanged" EnableLoadOnDemand="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                    MarkFirstMatch="true" MaxHeight="200px" Width="300px">
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

                    </table>
				</td>
			</tr>
			<tr>
				<td>
					&nbsp;
				</td>
				<td>
					<asp:HiddenField ID="HF_Cve" runat="server" />
				</td>
			</tr>
		</table>
    </div>
 
</asp:Content>

