<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatClienteMatriz_Lista.aspx.cs" 
 MasterPageFile="~/MasterPage/MasterPage01.master"
Inherits="SIANWEB.CuentasCorporativas.CatClienteMatriz_Lista"  EnableEventValidation="false"%>



<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

 <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicked="ToolBar_ClientClick">
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

    <br />
    <h2>Catalogo Cliente Matriz</h2>
    <br />

    <asp:GridView ID="dgClienteMatriz" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Matriz" DataField="Id" />
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Inicio" DataField="FechaInicio" DataFormatString="{0:d}"  HeaderStyle-Width="100" />
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Fin" DataField="FechaFin" DataFormatString="{0:d}" HeaderStyle-Width="100"/>

            <asp:TemplateField HeaderText="Estatus" HeaderStyle-Width="75">
                <ItemTemplate>
                    <%#      (Boolean.Parse(Eval("Estatus").ToString())) ? "Activo" : "Inactivo"%>
                </ItemTemplate>
                
            </asp:TemplateField>

            <asp:BoundField ReadOnly="True" HeaderText="Cliente Matriz" DataField="Nombre" HeaderStyle-Width="300"/>
            
            

           <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                         <img src="../Img/ic_edit.jpg" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaACYS(<%# DataBinder.Eval(Container.DataItem, "Id") %>);" />
                    </ItemTemplate>
           </asp:TemplateField>

            <asp:ButtonField ButtonType="Image" HeaderText="Cancelar" ImageUrl="../Img/quitar1.png" />
    
            

            <asp:TemplateField HeaderText="PERMISOS">
                    <ItemTemplate>
                         <img src="../Img/ic_conts.gif" id="imgPermisos" style="cursor:pointer" onclick="AbrirPantallaPermisos(<%# DataBinder.Eval(Container.DataItem, "Id") %>,'<%# DataBinder.Eval(Container.DataItem, "Nombre") %>')" />
                     </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Consultar Afiliaciones">
                    <ItemTemplate>
                         <img src="../Img/find16.png" id="imgAfiliaciones" style="cursor:pointer" onclick="AbrirPantallaAfiliaciones(<%# DataBinder.Eval(Container.DataItem, "Id") %>);" />
                     </ItemTemplate>
            </asp:TemplateField>

                
        </Columns>
    </asp:GridView>

     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function AbrirPantallaPermisos(Id, Nombre) {
                oWnd = radopen('CatClienteMatriz_Permisos.aspx?Id='+ Id +'&Nombre='+ Nombre +'');
//                oWnd.center();
//                oWnd.Maximize();
            }



            function AbrirPantallaAfiliaciones(Id) {
                oWnd = radopen('CatClienteMatriz_Afiliaciones.aspx?Id=' + Id);
                                oWnd.center();
                                oWnd.Maximize();
                              
            }

            function AbrirPantallaACYS(Id) {
                oWnd = radopen('CatClienteMatriz_ACYS.aspx?Id=' + Id);
                oWnd.center();
                oWnd.Maximize();
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



            function OpenAlert(mensaje, Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir) 
            {
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


