<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CapAutClienteTerr_Lista.aspx.cs" Inherits="SIANWEB.CapAutClienteTerr_Lista" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        //--------------------------------------------------------------------------------------------------
        //Abre la ventana de edición de Quejas
        //--------------------------------------------------------------------------------------------------
        function AbrirVentana_Solicitud(Id_Solicitud, Id_Cd, Id_Cte, Id_Ter) {
            var oWnd = radopen("CapAutClienteTerr.aspx?Id_Solicitud=" + Id_Solicitud
                                + "&Id_Cd=" + Id_Cd
                                + "&Id_Cte=" + Id_Cte
                                + "&Id_Ter=" + Id_Ter
                        , "AbrirVentana_Solicitud");
            oWnd.set_showOnTopWhenMaximized(false);
            oWnd.maximize();
            oWnd.center();
        }

        function CloseAlert(mensaje) {
            var cerrarWindow = radalert(mensaje, 330, 150);
            var oWindow = null;
            RefreshGrid();
            if (window.radWindow)
                oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
            return oWindow;
        }

        function refreshGrid() {
            var ajaxManager = $find("<%= RAM1.ClientID %>");
            ajaxManager.ajaxRequest('RebindGrid');
        }

        function RefreshGrid() {
            var rgPendientes = $find("<%= rgPendientes.ClientID %>").get_masterTableView();
            rgPendientes.rebind();
            var rgAutorizados = $find("<%= rgAutorizados.ClientID %>").get_masterTableView();
            rgAutorizados.rebind();
            var rgRechazados = $find("<%= rgRechazados.ClientID %>").get_masterTableView();
            rgRechazados.rebind();
        }
    </script>
</telerik:RadCodeBlock>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"  EnablePageHeadUpdate="False">
    </telerik:RadAjaxManager>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
             <telerik:RadWindow ID="AbrirVentana_Solicitud" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Autorización de Cliente-Territorio"
                Modal="True" Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
               InitialBehaviors="Maximize"> 
            </telerik:RadWindow>  
        </Windows>
    </telerik:RadWindowManager>
     <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
     
    <telerik:RadToolBar  ID="RadToolBar1" runat="server"  Width="100%" >
        <Items>
           <%-- <telerik:RadToolBarButton Text="" CommandName="Nuevo" ToolTip ="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png"/>--%>
        </Items>
     </telerik:RadToolBar> 

     <div class="formulario" id="divPrincipal" runat="server"> 
        
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

        <table style="font-family: verdana; font-size: 8pt; height: 100%" width="100%">
        <tr>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0" >
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Pendientes" AccessKey="P" PageViewID="RadPageViewPendientes" Value="Pendientes" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Autorizados" AccessKey="A" PageViewID="RadPageViewAutorizados" Value="Autorizados">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Rechazados" AccessKey="R" PageViewID="RadPageViewRechazados" Value="Rechazados">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>


                <%--SE CREAN LAS PESTAÑAS CLIENTE-TERRITORIO PENDIENTES, AUTORIZADOS Y RECHAZADOS --%>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid" BorderWidth="0px" ScrollBars="Hidden">

                    <%--CREAMOS LA PESTAÑA PENDIENTES--%>
                    <telerik:RadPageView ID="RadPageViewPendientes" runat="server">
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0">
                                <telerik:RadPane ID="RadPane1" runat="server" Width = "1400px">

                                 <telerik:RadGrid ID="rgPendientes" runat="server" GridLines="None"  AutoGenerateColumns="False" style="margin-bottom: 0px"  PageSize = "10"
                                onneeddatasource="rgPendientes_NeedDataSource" AllowPaging = "True" onitemcommand="rgPendientes_ItemCommand"  CellSpacing="0" onpageindexchanged="rgPendientes_PageIndexChanged" 
                                MasterTableView-NoMasterRecordsText="No se encontraron registros.">

                                    <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" >
               
                                        <Columns>

                                       <%-- Columnas --%>
                                            <telerik:GridBoundColumn DataField="Id_Solicitud" HeaderText="Solicitud" Display="true" UniqueName="Id_Solicitud" HeaderStyle-Width = "40px">
                                                    <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" Display="true" UniqueName="Id_Cd" HeaderStyle-Width = "70px">
                                                    <HeaderStyle Width="70px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Sucursal" HeaderText="Nombre Sucursal" Display="true" UniqueName="Nom_Sucursal" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id Cliente" Display="true" UniqueName="Id_Cte" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Cliente" HeaderText="Nombre Cliente" Display="true" UniqueName="Nom_Cliente" HeaderStyle-Width = "300px">
                                                    <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Territorio" Display="true" UniqueName="Id_Ter" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Territorio" HeaderText="Nombre Territorio" Display="true" UniqueName="Nom_Territorio" HeaderStyle-Width = "300px">
                                                     <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn DataField="Fec_Solicitud" HeaderText="Fecha Solicitud" Display="true" UniqueName="Fec_Solicitud" HeaderStyle-Width = "100px">
                                                     <HeaderStyle Width="100px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Comentarios" HeaderText="Comentarios" Display="true" UniqueName="Comentarios" HeaderStyle-Width = "300px">
                                                    <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn UniqueName = "Activo" HeaderText="Activo">
                                                <ItemTemplate>
                                                        <asp:CheckBox ID="chkActivo"  runat="server" Enabled = "false" Checked='<%# DataBinder.Eval(Container.DataItem, "Activo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Activo")) %>'
                                                        AutoPostBack="true" />
                                                </ItemTemplate>

                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>

                                           <telerik:GridTemplateColumn UniqueName = "Nuevo" HeaderText="Nuevo">
                                                <ItemTemplate>
                                                       <asp:CheckBox ID="chkNuevo" runat="server" Enabled = "false" Checked='<%# DataBinder.Eval(Container.DataItem, "Nuevo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Nuevo")) %>'
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>

                                        <%--Botones --%> 
               
                                           <telerik:GridTemplateColumn HeaderText="Autorizar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="35px" UniqueName = "Autorizar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnAutorizar" runat="server"  CssClass="edit" ToolTip="Autorizar" CommandName="Autorizar" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                           </telerik:GridTemplateColumn>

                                           <telerik:GridTemplateColumn HeaderText="Rechazar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="35px" UniqueName = "Rechazar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnRechar" runat="server"  CssClass="edit" ToolTip="Rechazar" CommandName="Rechazar" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                           </telerik:GridTemplateColumn>

                                           <telerik:GridTemplateColumn HeaderText="Ver Detalle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="35px" UniqueName = "Detalle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnDetalle" runat="server"  CssClass="edit" ToolTip="Ver Detalle" CommandName="Detalle" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                           </telerik:GridTemplateColumn>
                                               

                                     </Columns>     
                     
                                     </MasterTableView>
                                            
                                  <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid> 

                               </telerik:RadPane>
                         </telerik:RadSplitter>
                    </telerik:RadPageView>

                    <%--CREAMOS LA PESTAÑA AUTORIZADOS--%>
                    <telerik:RadPageView ID="RadPageViewAutorizados" runat="server">
                        <telerik:RadSplitter ID="RadSplitter2" runat="server" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0" >
                            <telerik:RadPane ID="RadPane2" runat="server" Width = "1400px">

                              <telerik:RadGrid ID="rgAutorizados" runat="server" GridLines="None" AutoGenerateColumns="False"  style="margin-bottom: 0px"  PageSize = "10"
                                onneeddatasource="rgAutorizados_NeedDataSource" AllowPaging = "True" CellSpacing="0" onpageindexchanged="rgAutorizados_PageIndexChanged"  
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." onitemcommand="rgAutorizados_ItemCommand" >

                                <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" >
                
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
               
                                        <Columns>

                                       <%-- Columnas --%>
                                            <telerik:GridBoundColumn DataField="Id_Solicitud" HeaderText="Solicitud" Display="true" UniqueName="Id_Solicitud" HeaderStyle-Width = "40px">
                                                    <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" Display="true" UniqueName="Id_Cd" HeaderStyle-Width = "70px">
                                                    <HeaderStyle Width="70px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Sucursal" HeaderText="Nombre Sucursal" Display="true" UniqueName="Nom_Sucursal" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id Cliente" Display="true" UniqueName="Id_Cte" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Cliente" HeaderText="Nombre Cliente" Display="true" UniqueName="Nom_Cliente" HeaderStyle-Width = "300px">
                                                    <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Territorio" Display="true" UniqueName="Id_Ter" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Territorio" HeaderText="Nombre Territorio" Display="true" UniqueName="Nom_Territorio" HeaderStyle-Width = "300px">
                                                     <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn DataField="Fec_Solicitud" HeaderText="Fecha Solicitud" Display="true" UniqueName="Fec_Solicitud" HeaderStyle-Width = "100px">
                                                     <HeaderStyle Width="100px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Fec_Atendido" HeaderText="Fecha Atendida" Display="true" UniqueName="Fec_Atendido" HeaderStyle-Width = "100px">
                                                     <HeaderStyle Width="100px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Comentarios" HeaderText="Comentarios" Display="true" UniqueName="Comentarios" HeaderStyle-Width = "300px">
                                                    <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn UniqueName = "Activo" HeaderText="Activo">
                                                <ItemTemplate>
                                                        <asp:CheckBox ID="chkActivo"  runat="server" Enabled = "false" Checked='<%# DataBinder.Eval(Container.DataItem, "Activo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Activo")) %>'
                                                        AutoPostBack="true" />
                                                </ItemTemplate>

                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>

                                           <telerik:GridTemplateColumn UniqueName = "Nuevo" HeaderText="Nuevo">
                                                <ItemTemplate>
                                                       <asp:CheckBox ID="chkNuevo" runat="server" Enabled = "false" Checked='<%# DataBinder.Eval(Container.DataItem, "Nuevo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Nuevo")) %>'
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>

                                        <%--Botones --%> 
               
                                           <telerik:GridTemplateColumn HeaderText="Ver Detalle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="35px" UniqueName = "Detalle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnDetalle" runat="server"  CssClass="edit" ToolTip="Ver Detalle" CommandName="Detalle" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                           </telerik:GridTemplateColumn>
                                               

                                     </Columns>     
                     
                                     </MasterTableView>
          
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />

                                </telerik:RadGrid> 

                            </telerik:RadPane>
                         </telerik:RadSplitter>
                    </telerik:RadPageView>

                    <%--CREAMOS LA PESTAÑA RECHAZADOS--%>
                    <telerik:RadPageView ID="RadPageViewRechazados" runat="server" >
                        <telerik:RadSplitter ID="RadSplitter3" runat="server"  ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0">
                            <telerik:RadPane ID="RadPane3" runat="server"  Width = "1400px">

                              <telerik:RadGrid ID="rgRechazados" runat="server" GridLines="None" AutoGenerateColumns="False"  style="margin-bottom: 0px"  PageSize = "10" onneeddatasource="rgRechazados_NeedDataSource" AllowPaging = "True" 
                                    CellSpacing="0" onpageindexchanged="rgRechazados_PageIndexChanged" Width="1200px" MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                    onitemcommand="rgRechazados_ItemCommand" >

                                    <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" >
                
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
               
                                        <Columns>

                                       <%-- Columnas --%>
                                            <telerik:GridBoundColumn DataField="Id_Solicitud" HeaderText="Solicitud" Display="true" UniqueName="Id_Solicitud" HeaderStyle-Width = "40px">
                                                    <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" Display="true" UniqueName="Id_Cd" HeaderStyle-Width = "70px">
                                                    <HeaderStyle Width="70px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Sucursal" HeaderText="Nombre Sucursal" Display="true" UniqueName="Nom_Sucursal" HeaderStyle-Width = "120px">
                                                    <HeaderStyle Width="70px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id Cliente" Display="true" UniqueName="Id_Cte" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Cliente" HeaderText="Nombre Cliente" Display="true" UniqueName="Nom_Cliente" HeaderStyle-Width = "300px">
                                                    <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Territorio" Display="true" UniqueName="Id_Ter" HeaderStyle-Width = "150px">
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nom_Territorio" HeaderText="Nombre Territorio" Display="true" UniqueName="Nom_Territorio" HeaderStyle-Width = "300px">
                                                     <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn DataField="Fec_Solicitud" HeaderText="Fecha Solicitud" Display="true" UniqueName="Fec_Solicitud" HeaderStyle-Width = "100px">
                                                     <HeaderStyle Width="100px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Fec_Atendido" HeaderText="Fecha Atendida" Display="true" UniqueName="Fec_Atendido" HeaderStyle-Width = "100px">
                                                     <HeaderStyle Width="100px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Comentarios" HeaderText="Comentarios" Display="true" UniqueName="Comentarios" HeaderStyle-Width = "300px">
                                                    <HeaderStyle Width="300px"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn UniqueName = "Activo" HeaderText="Activo">
                                                <ItemTemplate>
                                                        <asp:CheckBox ID="chkActivo"  runat="server" Enabled = "false" Checked='<%# DataBinder.Eval(Container.DataItem, "Activo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Activo")) %>'
                                                        AutoPostBack="true" />
                                                </ItemTemplate>

                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>

                                           <telerik:GridTemplateColumn UniqueName = "Nuevo" HeaderText="Nuevo">
                                                <ItemTemplate>
                                                       <asp:CheckBox ID="chkNuevo" runat="server" Enabled = "false" Checked='<%# DataBinder.Eval(Container.DataItem, "Nuevo") is DBNull?false:Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Nuevo")) %>'
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>

                                        <%--Botones --%> 
               
                                           <telerik:GridTemplateColumn HeaderText="Ver Detalle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="35px" UniqueName = "Detalle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnDetalle" runat="server"  CssClass="edit" ToolTip="Ver Detalle" CommandName="Detalle" ImageUrl="~/Imagenes/blank.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                           </telerik:GridTemplateColumn>
                                               

                                     </Columns>     
                     
                                     </MasterTableView>
                                              
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />

                                </telerik:RadGrid> 

                            </telerik:RadPane>
                         </telerik:RadSplitter>
                    </telerik:RadPageView>

                </telerik:RadMultiPage>


            </td>
        </tr>

        </table>

  </div>

</asp:Content>
