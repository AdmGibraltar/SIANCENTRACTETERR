<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACYS_Lista.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.ACYS_Lista" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>

   
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
               <AjaxSettings>

                    <telerik:AjaxSetting AjaxControlID="RAM1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                            </UpdatedControls>
                    </telerik:AjaxSetting>

                     <telerik:AjaxSetting AjaxControlID="dgACYS">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                                    UpdatePanelHeight="" />
                            </UpdatedControls>
                     </telerik:AjaxSetting>

             </AjaxSettings>


     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" 
        onclientbuttonclicked="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" CssClass="new" ToolTip="Nuevo"
                ImageUrl="~/Imagenes/blank.png" />
 
        </Items>
    </telerik:radtoolbar>

 <div id="divPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt;">

      <br />
    <h1><%= Request.QueryString["Nombre"] %></h1>
      <h4>Filtros</h4>

      <table>
      <tr>
        <td><asp:Label ID="lblNombre" Text="Nombre Acys: " runat="server"></asp:Label></td>
        <td><telerik:RadTextBox ID="txtNombre" runat="server" Width="300px"></telerik:RadTextBox></td>
        <td><asp:Button id="btnBuscar" runat="server" Text="Buscar" 
                onclick="btnBuscar_Click"/></td>
      </tr>
      </table>

    <br />
    <h4>Acys Existentes</h4>
    <br />
    <br />




     <asp:GridView ID="dgACYS" runat="server" AutoGenerateColumns="false" 
          onrowcommand="dgACYS_RowCommand" >

        <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Matriz" DataField="Id" Visible="false" />

            <asp:BoundField ReadOnly="True" HeaderText="Cliente Matriz" DataField="Nombre" HeaderStyle-Width="300"/>
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Ult Act." DataField="FechaUltimaAct" DataFormatString="{0:d}"  HeaderStyle-Width="100" />
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Vencimiento" DataField="FechaVencimiento" DataFormatString="{0:d}" HeaderStyle-Width="100"/>

            <asp:TemplateField HeaderText="Activo">
                    <ItemTemplate>
                         <asp:CheckBox ID="chkActivoB" runat="server" Checked='<%#Convert.ToBoolean(Eval("Activo"))%>' Enabled="false" />
                    </ItemTemplate>
           </asp:TemplateField>

           <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                         <img src="../Img/ic_edit.jpg" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaItem(<%# DataBinder.Eval(Container.DataItem, "Id") %>,<%# Request.QueryString["Id"]%> );" />
                    </ItemTemplate>
           </asp:TemplateField>

            <asp:TemplateField HeaderText="Detalle ACYS">
                     <ItemTemplate>
                           <img src="../Img/ic_2.jpg" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaACYS(<%# DataBinder.Eval(Container.DataItem, "Id") %>,<%# Request.QueryString["Id"]%> );" />
                     </ItemTemplate>
            </asp:TemplateField>


         <asp:TemplateField HeaderText="Duplicar ACYS">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="../Img/ic_1.gif" ID="imgDuplicar" runat="server" CommandName="Duplicar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClientClick ="return confirm('¿Desea duplicar este ACYS?');" />
                     </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Deshabilitar">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="../Img/quitar1.png" ID="imgDeshabilitar" runat="server" CommandName="Deshabilitar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClientClick ="return confirm('¿Esta seguro que desea deshabilitar el ACYS?');"  />
                     </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                           <asp:ImageButton ImageUrl="../Img/x.gif" ID="imgEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClientClick ="return confirm('¿Esta seguro que desea eliminar el ACYS?');"  />
                     </ItemTemplate>
            </asp:TemplateField>

                
        </Columns>
    </asp:GridView>

</div>



      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">




            function AbrirPantallaItem(Id, IdMatriz) {
                oWnd = radopen('ACYS_Nuevo.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz);
                oWnd.setSize(800, 600);
                oWnd.center();
            }


            function AbrirPantallaACYS(Id, IdMatriz) {
                oWnd = radopen('CatClienteMatriz_ACYS.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz);
                oWnd.maximize();
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
                AbrirPantallaItem(undefined,idmatriz);

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