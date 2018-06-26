<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitudes.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.Solicitudes" 
  MasterPageFile="~/MasterPage/MasterPage01.master" EnableEventValidation="false" %>

   
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>


     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False" onajaxrequest="RAM1_AjaxRequest">
            <AjaxSettings>
                 <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="dgSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>

            </telerik:AjaxSetting>
            </AjaxSettings>

     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" CssClass="new" ToolTip="Nuevo"
                ImageUrl="~/Imagenes/blank.png" />
 
        </Items>
    </telerik:radtoolbar>

 <div id="divPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt;">

      <br />
    <h1>Solicitudes Pendientes</h1>
      <h4>Filtros</h4>

      <table>
          <tr>
            <td><asp:Label ID="lblNombre" Text="Nombre Matriz: " runat="server"></asp:Label></td>
            <td><telerik:RadTextBox ID="txtNombre" runat="server" Width="300px"></telerik:RadTextBox></td>
             <td><asp:Button id="btnBuscar" runat="server" Text="Buscar" 
                     onclick="btnBuscar_Click" /></td>
            </tr>

            <tr>
            <td><asp:Label ID="lblEstatus" Text="Estatus: " runat="server"></asp:Label></td>
            <td>
                   <telerik:RadComboBox ID="cmbEstatus" runat="server" 
                        DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione..."
                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                        MarkFirstMatch="true" 
                            Width="300px" ReadOnly="True"
                        MaxHeight="250px" >
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                        <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                            Width="50px" />
                                    </td>

                                    <td style="text-align: left">
                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
                                    </td>
                                </tr>
                                </table>
	                        </ItemTemplate>
                   </telerik:RadComboBox>
            
            </td>


           
          </tr>
      </table>

    <br />
     <h4>Solicitudes</h4>
    <br />
    <br />

     <asp:GridView ID="dgSolicitudes" runat="server" AutoGenerateColumns="false" >
        <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="Id" Visible="True" />
            <asp:BoundField ReadOnly="True" HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:d}"   HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Sucursal" DataField="SucursalNombre"   HeaderStyle-Width="150"/>
            <asp:BoundField ReadOnly="True" HeaderText="#Cliente" DataField="ClienteSIAN" HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Terr" DataField="Territorio" HeaderStyle-Width="30"/>
            <asp:BoundField ReadOnly="True" HeaderText="Razon social" DataField="RazonSocial" HeaderStyle-Width="150"/>
            <asp:BoundField ReadOnly="True" HeaderText="Matriz" DataField="Matriz" HeaderStyle-Width="150" />
            <asp:BoundField ReadOnly="True" HeaderText="Estructura" DataField="Estructura" HeaderStyle-Width="200"/>
            <asp:BoundField ReadOnly="True" HeaderText="Usuario" DataField="Usuario" HeaderStyle-Width="100"/>


           <asp:TemplateField HeaderText="Ver">
                    <ItemTemplate>
                         <img src="../Img/ic_edit.jpg" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaItem(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "Sucursal") %> );" />
                    </ItemTemplate>
           </asp:TemplateField>
    
            <asp:BoundField ReadOnly="True" HeaderText="Estatus" DataField="EstatusNombre" HeaderStyle-Width="100"/>
                
        </Columns>
    </asp:GridView>

</div>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function AbrirPantallaItem(Id, Sucursal) {
                oWnd = radopen('SolicitudesItem.aspx?Id=' + Id + '&Sucursal=' + Sucursal);
              
                oWnd.center();
                oWnd.maximize();

            }


            function refreshGrid() {
                //debugger;
                location.reload();
            }


            function ToolBar_ClientClick(sender, args) {
                AbrirVentana_Acys(-1, 0);
            }




            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }


            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }


            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

        </script>
    </telerik:RadCodeBlock>




</asp:Content>