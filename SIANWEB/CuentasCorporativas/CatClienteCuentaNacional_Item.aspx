<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatClienteCuentaNacional_Item.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.CatClienteCuentaNacional_Item" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False" onajaxrequest="RAM1_AjaxRequest">
     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <h3 style="font-family: Verdana;">Matriz cuentas nacionales</h3>


<div style="font-family: Verdana; font-size: 8pt;">

 <h4> Datos Generales </h4>
    <table >
        <tr>
            <td>Nombre: </td>
            <td><telerik:RadTextBox ID="txtNombre" runat="server" ReadOnly="False" width="300px"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="r1" runat="server" Text="*" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
             </td>
        </tr>
          
         <tr>
            <td>Logo </td>
            <td> <input type="File" id="Logo" name="Logo" runat="server" style="width:300px" /></td>
        </tr>
                <tr>
            <td><asp:CheckBox ID="chkActivo" runat="server" Text="Activo"  Checked="true" /> </td>
            <td>  </td>
        </tr>


    
    </table>

    <br />

  <h4> Estructura </h4>
  <br />
    <table>
        <tr>
            <td><asp:CheckBox ID="chkNivel_1" runat="server" Text="Nivel 1" /> &nbsp;&nbsp;&nbsp; </td>
            <td><telerik:RadTextBox ID="txtDesc_Nivel_1" runat="server" ReadOnly="False" width="300"></telerik:RadTextBox></td>
        </tr>
                <tr>
            <td><asp:CheckBox ID="chkNivel_2" runat="server" Text="Nivel 2" /> </td>
            <td><telerik:RadTextBox ID="txtDesc_Nivel_2" runat="server" ReadOnly="False" width="300"></telerik:RadTextBox></td>
        </tr>
                <tr>
            <td><asp:CheckBox ID="chkNivel_3" runat="server" Text="Nivel 3" /> </td>
            <td><telerik:RadTextBox ID="txtDesc_Nivel_3" runat="server" ReadOnly="False" width="300"></telerik:RadTextBox></td>
        </tr>
                <tr>
            <td><asp:CheckBox ID="chkNivel_4" runat="server" Text="Nivel 4" /> </td>
            <td><telerik:RadTextBox ID="txtDesc_Nivel_4" runat="server" ReadOnly="False" width="300"></telerik:RadTextBox></td>
        </tr>
    </table>

    <br />
    <br />
  <h4> Datos para el E-commerce </h4>
  <br />
  <table>
          <tr>
            <td><asp:CheckBox ID="chkPresupuesto" runat="server" Text="Presupuesto" /> </td>
            <td>  </td>
        </tr>

         <tr>
            <td><asp:CheckBox ID="chkReqAutorizacion" runat="server" Text="Req. Autorización" /> </td>
            <td>  </td>
        </tr>
 </table>

 <br />
 <br />

    <asp:Button runat="server" ID="btnGuardar" text="Guardar" 
        onclick="btnGuardar_Click" />
</div>

 <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirPantallaPermisos(Id, Nombre) {
                oWnd = radopen('CatClienteMatriz_Permisos.aspx?Id=' + Id + '&Nombre=' + Nombre + '');
                //                oWnd.center();
                //                oWnd.Maximize();
            }

            document.getElementById("")

            function OpenAlert(mensaje, Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Acys2(Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir);
                    });
            }

            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }


            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }


            function CloseAlert(mensaje) {
                //                var cerrarWindow = radalert(mensaje, 330, 150);
                //                cerrarWindow.add_close(
                //                    function () {

                //alert(mensaje);
                CloseAndRebind();
                //                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

        </script>
    </telerik:RadCodeBlock>




</asp:Content>


