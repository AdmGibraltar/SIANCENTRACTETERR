<%@ Page Title="Vincular sucursal" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_VinculaSucursal.aspx.cs"
    Inherits="SIANWEB.Ventana_VinculaSucursal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
      <link href="../SIANCENTRAL/Styles/Toolbar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 17px;
        }
    </style>
</head> 
<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PnlLogin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="PnlLogin" runat="server" >
        <table style="font-family: Verdana; font-size: 8pt; >
        <tr>
        <td colspan ="4">
                <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
            Width="100%"  OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
         
            </Items>
        </telerik:RadToolBar>
        </td>
        </tr>
       
     
            <tr>
            <td colspan="4">
             &nbsp; &nbsp;
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" Text="Convenio:"></asp:Label>
                </td>
                <td class="style1">
                 <asp:Label ID="LblPC_NoConvenio" runat="server" ></asp:Label>
                </td>
                <td class="style1">
             
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nombre: "></asp:Label>
                </td>
                <td>
                 <asp:Label ID="LblPC_Nombre" runat="server" ></asp:Label>
                </td>
                <td>
                   
                </td>
            </tr>
                 <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Categoría: "></asp:Label>
                </td>
                <td>
                 <asp:Label ID="LblId_CatStr" runat="server" ></asp:Label>
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;
                </td>
                <td   colspan="3">
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
                  <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td  colspan="3">
                   <telerik:RadGrid ID="rgVincular" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" Width="450px" Height="400px" Visible="true" >
                                    <ClientSettings EnableRowHoverStyle="false">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <MasterTableView NoMasterRecordsText="No existen registros que mostrar">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                          <telerik:GridBoundColumn DataField="Id_PC" HeaderText="Id_PC" Display="false"
                                                UniqueName="Id_PC">
                                            </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Tipo" DataField="Id_Tipo" UniqueName="Id_Tipo" Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblId_Tipo" runat="server" Text='<%# Eval("Id_Tipo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn HeaderText="CD" DataField="Id_CD" UniqueName="Id_CD" Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblId_CD" runat="server" Text='<%# Eval("Id_CD")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            
                                            <telerik:GridBoundColumn DataField="Cd_Nombre" HeaderText="CDI" ItemStyle-HorizontalAlign="Left"
                                                UniqueName="CD_Nombre">
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </telerik:GridBoundColumn>
                                
                                            <telerik:GridTemplateColumn UniqueName="Ver">
                                                <HeaderTemplate>
                                                               <input onclick="CheckAllVer(this);" type="checkbox" id="ChkVerHeader" runat="server" />Ver
                                              </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkVer" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PCD_Ver") %>'
                                                        Style="cursor: hand" oncheckedchanged="ChkVer_CheckedChanged" autopostback="true"  />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Usar">
                                                <HeaderTemplate>
                                                    <input onclick="CheckAllUsar(this);" type="checkbox" id="ChkUsarHeader" runat="server" />Utilizar
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkUsar" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PCD_Usar") %>'
                                                        Style="cursor: hand"  oncheckedchanged="ChkUsar_CheckedChanged" autopostback="true"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;
                
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HFId_PC"></asp:HiddenField>
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function SoloAlfabetico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if ((c < 32) || (c > 32 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250))
                    eventArgs.set_cancel(true);
            }

            function SoloAlfanumerico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (event.keyCode == 13) {
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest(null);
                } else
                    if ((c < 32) || (c > 32 && c < 46) || (c > 46 && c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250))
                        eventArgs.set_cancel(true);

            }

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }
            

            function AlertaFocus(mensaje, control) {

                var oWnd = radalert(mensaje, 340, 150);
                //oWnd.add_close(foco(control));
                oWnd.add_close(function () {
                    var target = $find(control);
                    if (target != null) {
                        target.focus();
                    }
                });
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

            function CloseAndRebind() {
                ////debugger;
                GetRadWindow().Close();
            }

            function CheckAllVer(sender) {
                //debugger;
                var grid = $find('<%=rgVincular.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkVer")).checked = sender.checked;
                }
            }
            function CheckAllUsar(sender) {
                //debugger;
                var grid = $find('<%=rgVincular.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkUsar")).checked = sender.checked;
                }
            }

        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
