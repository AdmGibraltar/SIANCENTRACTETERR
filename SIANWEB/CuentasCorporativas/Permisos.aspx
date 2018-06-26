<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.Permisos" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onclientbuttonclicked="ToolBar_ClientClick" >
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" CssClass="new" ToolTip="Nuevo"
                ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:radtoolbar>


     <div id="divPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt;">

      <br />
    <h1>Usuarios y Permisos</h1>
    <br />
    <h4>Usuarios existentes</h4>
    <br />
    <br />

     <asp:GridView ID="dgUsuarios" runat="server" AutoGenerateColumns="false" 
               onrowcommand="dgUsuarios_RowCommand"
             >
         
        <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="Id" Visible="false" />
            <asp:BoundField ReadOnly="True" HeaderText="IdMatriz" DataField="IdMatriz" Visible="false" />

            <asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="Nombre" HeaderStyle-Width="300"/>
            <asp:BoundField ReadOnly="True" HeaderText="Correo" DataField="Correo" HeaderStyle-Width="200"/>
            <asp:BoundField ReadOnly="True" HeaderText="Telefono" DataField="Telefono" HeaderStyle-Width="150"/>
            <asp:BoundField ReadOnly="True" HeaderText="CDIK" DataField="CDIK" HeaderStyle-Width="150"/>
         
           <asp:TemplateField HeaderText="Admin. Cliente">
                    <ItemTemplate>
                         <asp:CheckBox ID="chkAdminCliente" runat="server" Checked='<%#Convert.ToBoolean(Eval("AdminCliente"))%>' Enabled="false" />
                    </ItemTemplate>
           </asp:TemplateField>

           <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                         <img src="../Img/ic_edit.jpg" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaItem(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# Request.QueryString["Id"]%>,'<%# Request.QueryString["Nombre"]%>');" />
                    </ItemTemplate>
           </asp:TemplateField>
    


            <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                           <asp:ImageButton ImageUrl="../Img/x.gif" ID="imgEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClientClick ="return confirm('¿Esta seguro que desea eliminar el usuario?');"  />
                     </ItemTemplate>
            </asp:TemplateField>



        </Columns>
    </asp:GridView>

</div>



      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirPantallaItem(Id, IdMatriz, Nombre) {
                oWnd = radopen('Permisos_Item.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz + '&Nombre=' + Nombre+'');
                oWnd.setSize(800, 600);
                oWnd.center();
            }

            function onResize(sender, eventArgs) {
            }

            function refreshGrid() {
                //debugger;
                location.reload();
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                // debugger;
                idmatriz = getParameterByName("Id");
                nombre = getParameterByName("Nombre");
                AbrirPantallaItem(0, idmatriz,nombre);

            }



            function getParameterByName(name, url) {
                if (!url) {
                    url = window.location.href;
                }
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }




        </script>
     </telerik:radcodeblock>
 </asp:Content>

