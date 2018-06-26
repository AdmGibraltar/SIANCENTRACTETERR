<%@ Page Title="Cancelación" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_BajaConvenio.aspx.cs"
    Inherits="SIANWEB.Ventana_BajaConvenio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head> 
<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest">
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
    <asp:Panel ID="PnlLogin" runat="server" DefaultButton="btnAceptar">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td align="center" width="15">
                    &nbsp;
                </td>
                <td align="center" width="110">
                    &nbsp;
                </td>
                <td align="left" width="180">
                    &nbsp;
                </td>
                <td align="center" width="150">
                    &nbsp;
                </td>
            </tr>
            <tr>
            <td>
             &nbsp;
            </td>
            <td colspan="3" >
            <asp:Label ID="Label3" runat="server" Text="¿Desea CANCELAR el convenio de precios especiales?"></asp:Label>          
              </td>
            </tr>
            <tr>
            <td colspan="4">
             &nbsp; &nbsp;
            </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Convenio:"></asp:Label>
                </td>
                <td>
                 <asp:Label ID="LblPC_NoConvenio" runat="server" ></asp:Label>
                </td>
                <td>
                   
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
                <td align="center">
                    &nbsp;
                </td>
                <td align="center" colspan="3">
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
                  <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center" colspan="3">
                    <asp:Label ID="Label5" runat="server" Text ="Se enviará correo electronico a los interesados indicando la cancelación."></asp:Label>
                    &nbsp; &nbsp; &nbsp;
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
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" 
                        Text="Aceptar" />
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HFId_PC"></asp:HiddenField>
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
         
            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                RefreshParentPage();
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
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
            }
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }


        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
