<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatClienteCuentaNacional_Lista.aspx.cs" 
 MasterPageFile="~/MasterPage/MasterPage01.master"
Inherits="SIANWEB.CuentasCorporativas.CatClienteCuentaNacional_Lista"  EnableEventValidation="false"%>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>


 <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicked="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
          </telerik:RadToolBar>

    <br />
    <h2>Catalogo Unico de Matrices de Cuentas Nacionales</h2>
    <br />

    <input type="button" value="Agregar Nuevo" onclick="AbrirPantallaItem()" />

    <br />
    <br />
    <br />

<table>
    <tr>
        <td><asp:Label ID="lblNombre" Text="Nombre Cliente: " runat="server"></asp:Label></td>
        <td><telerik:RadTextBox ID="txtNombre" runat="server" Width="300px"></telerik:RadTextBox></td>
        <td><asp:Button id="btnBuscar" runat="server" Text="Buscar" 
                onclick="btnBuscar_Click"/></td>
    </tr>
</table>
<br />

<table>
<tr>
     <asp:Repeater id="repClientes" runat="server" 
        onitemcommand="repClientes_ItemCommand" 
        onitemdatabound="repClientes_ItemDataBound">

         <HeaderTemplate>
     
         </HeaderTemplate>
         <ItemTemplate>
        <td w>
             <b><%# Eval("Nombre") %></b>
             <table>
                <tr>
                    <td rowspan="4"> <asp:Image ID="imgLogo" runat="server" Height="120px" Width="120px"  /></td>
                     <td><a href="javascript:AbrirPantallaDatosGen(<%# Eval("Id") %>);">Datos Generales Matriz</a>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>

                <tr><td><a href=""><a href="javascript:AbrirPantallaACYS(<%# Eval("Id") %>,'<%# Eval("Nombre") %>');">ACYS</a>&nbsp;&nbsp;&nbsp;&nbsp;</a></td></tr>
                <tr><td><a href="javascript:AbrirPantallaEstructura(<%# Eval("Id") %>,'<%# Eval("Nombre") %>');">Estructura</a></td></tr>
                <tr><td><a href="javascript:AbrirPantallaPermisos(<%# Eval("Id") %>,'<%# Eval("Nombre") %>');">Usuarios y Permisos</a></td></tr>
             </table>

             <br />
             <a href="javascript:AbrirPantallaItem(<%# Eval("Id") %>);">Editar</a>
             <br />
             <br />
         
         </td>
         <%# (int)Eval("Id") % 4==0 ? "<tr></tr>": ""%>
       

         </ItemTemplate>
     </asp:Repeater>

     </tr>
</table>


     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">



            function AbrirPantallaItem(Id) {
                oWnd = radopen('CatClienteCuentaNacional_Item.aspx?Id='+ Id);
                oWnd.setSize(800, 600);
                oWnd.center();
            }

            function AbrirPantallaDatosGen(Id) {
                oWnd = radopen('DatosGenMatriz.aspx?Id=' + Id);
                oWnd.setSize(800, 600);
                oWnd.center();
                oWnd.maximize();

            }


            function AbrirPantallaACYS(Id, Nombre) {
                oWnd = radopen('ACYS_Lista.aspx?Id=' + Id + '&Nombre=' + Nombre);
                oWnd.setSize(1000, 800);
                oWnd.center();
                oWnd.maximize();

            }


            function AbrirPantallaEstructura(Id, Nombre) {
                oWnd = radopen('Estructura.aspx?Id=' + Id + '&Nombre=' + Nombre);
                oWnd.setSize(800, 600);
                oWnd.center();
                oWnd.maximize();

            }


            function AbrirPantallaPermisos(Id, Nombre) {
                oWnd = radopen('Permisos.aspx?Id=' + Id + '&Nombre=' + Nombre);
                oWnd.setSize(800, 600);
                oWnd.center();
                oWnd.maximize();

            }


            function refreshGrid() {
                //debugger;
                location.reload();
            }

            

            function txt3_OnBlur(sender, args) {

            }



            function ToolBar_ClientClick(sender, args) {

                AbrirVentana_Acys(-1, 0);


            }


            function enviarSolicitudPorCorreo() {

                if (RowFolio == null) {
                    radalert("Se debe seleccionar una solicitud del grid antes de presionar este botón.", 330, 150);
                    return false;
                }

                alert("(<enviar solicitud terminada por correo>)");

            }



            function OpenAlert(mensaje, Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Acys2(Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir);
                    });
            }


            function AbrirVentana_Acys2(Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir) {
                //debugger;
                var oWnd = radopen("CapAcys.aspx?Id=" + Id + "&PermisoGuardar=" + PermisoGuardar + "&PermisoModificar=" + PermisoModificar + "&PermisoEliminar=" + PermisoEliminar + "&PermisoImprimir=" + PermisoImprimir, "AbrirVentana_Acys");
                oWnd.center();
                oWnd.Maximize();
            }


            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }


            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }


        </script>
    </telerik:RadCodeBlock>




</asp:Content>


