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
    </script>
</telerik:RadCodeBlock>
    
    <telerik:RadAjaxManager ID="RAM" runat="server">
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
      <telerik:RadToolBar  ID="RadToolBar1" runat="server" onbuttonclick="RadToolBar1_ButtonClick"  Width="100%" >
        <Items>
           <%-- <telerik:RadToolBarButton Text="" CommandName="Nuevo" ToolTip ="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png"/>--%>
        </Items>
     </telerik:RadToolBar> 

   
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="75%">
             <tr>
                <td class="style1" ><%--<asp:Label ID="lblSolicitud" runat="server" Text="Solicitud:"></asp:Label>--%></td>
                <td class="style5">
                                     
                    <%--<telerik:RadComboBox ID="CmbSolicitud" runat="server" Width = "250px" 
                        DropDownWidth="110px" AutoPostBack = "true"
                        onselectedindexchanged="CmbSolicitud_SelectedIndexChanged" 
                        ontextchanged="CmbSolicitud_TextChanged"></telerik:RadComboBox> --%>
      
                </td>
             </tr>
        </table> 

        <telerik:RadGrid ID="rgFolios" runat="server" GridLines="None"  AutoGenerateColumns="False" DataMember="lstSolicitud" style="margin-bottom: 0px"  PageSize = "10"
        onneeddatasource="rgFolios_NeedDataSource" AllowPaging = "True" onitemcommand="rgFolios_ItemCommand"  CellSpacing="0" onpageindexchanged="rgFolios_PageIndexChanged" 
        Width="1200px" MasterTableView-NoMasterRecordsText="No se encontraron registros." >

            <MasterTableView AllowFilteringByColumn="False" EditMode="InPlace"  AllowMultiColumnSorting="False" AutoGenerateColumns = "false" HorizontalAlign ="NotSet" >
                
                <CommandItemSettings ExportToPdfText="Export to Pdf"  RefreshText="Actualizar" ShowAddNewRecordButton="false" />
               
                <Columns>

               <%-- Columnas --%>
                    <telerik:GridBoundColumn DataField="Id_Solicitud" HeaderText="Solicitud" Display="true" UniqueName="Id_Solicitud" HeaderStyle-Width = "40px">
                            <HeaderStyle Width="40px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id Cliente" Display="true" UniqueName="Id_Cte" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Nom_Cliente" HeaderText="Nombre Cliente" Display="true" UniqueName="Nom_Cliente" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" Display="true" UniqueName="Id_Cd" HeaderStyle-Width = "70px">
                            <HeaderStyle Width="70px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Territorio" Display="true" UniqueName="Id_Ter" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Nom_Territorio" HeaderText="Nombre Territorio" Display="true" UniqueName="Nom_Territorio" HeaderStyle-Width = "100px">
                             <HeaderStyle Width="100px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Dimension" HeaderText="Dimensión" Display="true" UniqueName="Dimension"  HeaderStyle-Width = "30px">
                             <HeaderStyle Width="30px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Pesos" HeaderText="Pesos" Display="true" UniqueName="Pesos" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Potencial" HeaderText="Potencial" Display="true" UniqueName="Potencial" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="ManodeObra" HeaderText="Mano de obra en proyectos" Display="true" UniqueName="ManodeObra" HeaderStyle-Width = "70px">
                            <HeaderStyle Width="70px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="GastosTerritorio" HeaderText="Gastos de territorio" Display="true" UniqueName="GastosTerritorio" HeaderStyle-Width = "150px">
                            <HeaderStyle Width="150px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="FletesPagadoCliente" HeaderText="Fletes pagados al cliente" Display="true" UniqueName="FletesPagadoCliente" HeaderStyle-Width = "100px">
                             <HeaderStyle Width="100px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Porcentaje" HeaderText="Porcentaje de comisión" Display="true" UniqueName="Porcentaje"  HeaderStyle-Width = "30px">
                             <HeaderStyle Width="30px"></HeaderStyle>
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
              <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente"
                SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
          
          <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                ShowPagerText="True" PageButtonCount="3" />
        </telerik:RadGrid> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 64px;
        }
    </style>
</asp:Content>

