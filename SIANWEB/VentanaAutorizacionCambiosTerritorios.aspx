<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="VentanaAutorizacionCambiosTerritorios.aspx.cs" Inherits="SIANWEB.VentanaAutorizacionCambiosTerritorios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
   
     <asp:Panel ID="Panel1" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    Centro de distribucion
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt" width="100%">
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultipage1"
                                    TabIndex="-1" ValidationGroup="guardar" SelectedIndex="0">
                                    <Tabs>
                                        <telerik:RadTab AccessKey="Z" PageViewID="RadPageView1" Selected="True" Text="Pendientes">
                                        </telerik:RadTab>
                                        <telerik:RadTab AccessKey="C" PageViewID="RadPageView2" Text="Autorizadas">
                                        </telerik:RadTab>
                                        <telerik:RadTab AccessKey="A" PageViewID="RadPageView3" Text="Rechazadas">
                                        </telerik:RadTab>
                                       <%-- <telerik:RadTab AccessKey="N" PageViewID="RadPageView4" Text="Detalle">
                                        </telerik:RadTab>--%>
                                        
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                                    BorderWidth="1px">
                                    <telerik:RadPageView ID="RadPageView1" runat="server">

                                      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                                       <Windows>
                                        <telerik:RadWindow ID="RadWindow1" runat="server"  Modal="true">
                                        <ContentTemplate>
                                            <label>Motivo del rechazo de la solicitud:</label>
                                            </p>
                                            <asp:TextBox ID="txtComentarioRechazo" runat="server" TextMode="MultiLine" Columns ="10" Width="272" Height="73"></asp:TextBox>
                                            </p>
                                            <asp:Button ID="Button1" runat="server" Text="Continuar" OnClick="RechazarSolicitudComentarios"/>
                                         
                                        </ContentTemplate>
                                        </telerik:RadWindow>
                                        </Windows>
                                        </telerik:RadWindowManager>


                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                     <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg1_NeedDataSource" OnItemCommand="rg1_ItemCommand" OnPageIndexChanged="rg1_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" 
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                    CellSpacing="0" Culture="es-ES">
                                    <MasterTableView>
                                        <Columns>
                                        <telerik:GridBoundColumn DataField="BdName" HeaderText="Origen" UniqueName="BdName"
                                                Visible="true" Display="true">
                                                <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IdAutorizacion" HeaderText="#.Autorizacion" UniqueName="IdAutorizacion"
                                                Visible="true">
                                                <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaSolicitud" HeaderText="Fecha De Solicitud" UniqueName="FechaSolicitud"
                                                Visible="true">
                                                 <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="ClaveTerritorio" HeaderText="Territorio" UniqueName="ClaveTerritorio"
                                                Visible="true">
                                                 <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="NombreRepresentanteActual" HeaderText="RIK Actual" UniqueName="RIKActual"
                                                Visible="true">
                                                 <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NombreRepresentante" HeaderText="RIK Cambio" UniqueName="NombreRepresentante">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Territorio" HeaderText="Territorio Actual" UniqueName="Territorio">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TerritorioCambio" HeaderText="Territorio Cambio" 
                                                UniqueName="TerritorioCambio">
                                            </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn DataField="Activo" HeaderText="Estatus" UniqueName="Activo">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#  Convert.ToBoolean(Eval("Activo")) == true ? "Activo" : "InActivo" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>




                                            <telerik:GridBoundColumn DataField="NombreSolicitante" 
                                                HeaderText="Solicitante" 
                                                UniqueName="NombreSolicitante">
                                            </telerik:GridBoundColumn>
                                           
                                            <telerik:GridTemplateColumn HeaderText="Aprobar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/check.png"
                                                        CssClass="" ToolTip="Aprobar" CommandName="Aprobar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>

                                             <telerik:GridTemplateColumn HeaderText="Rechazar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/delete2.png"
                                                        CssClass="" ToolTip="Rechazar" CommandName="Rechazar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                                                </td>
                                               
                                            </tr>
                                           
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView2" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                     <telerik:RadGrid ID="rg2" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg2_NeedDataSource" OnItemCommand="rg2_ItemCommand" OnPageIndexChanged="rg2_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" 
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                    CellSpacing="0" Culture="es-ES">
                                    <MasterTableView>
                                        <Columns>

                                        <telerik:GridBoundColumn DataField="BdName" HeaderText="Origen" UniqueName="BdName"
                                                Visible="true" Display="true">
                                                <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="IdAutorizacion" HeaderText="#.Autorizacion" UniqueName="IdAutorizacion"
                                                Visible="true">
                                                <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaSolicitud" HeaderText="Fecha De Solicitud" UniqueName="FechaSolicitud"
                                                Visible="true">
                                                 <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="ClaveTerritorio" HeaderText="Territorio" UniqueName="ClaveTerritorio"
                                                Visible="true">
                                                 <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="NombreRepresentanteActual" HeaderText="RIK Actual" UniqueName="RIKActual"
                                                Visible="true">
                                                 <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Territorio" HeaderText="Territorio Actual" UniqueName="Territorio">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NombreRepresentante" HeaderText="RIK Cambio" UniqueName="NombreRepresentante">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TerritorioCambio" HeaderText="Territorio Cambio" 
                                                UniqueName="TerritorioCambio">
                                            </telerik:GridBoundColumn>

                         <telerik:GridTemplateColumn DataField="Activo" HeaderText="Estatus" UniqueName="Activo">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#  Convert.ToBoolean(Eval("Activo")) == true ? "Activo" : "InActivo" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn DataField="NombreAprobador" 
                                                HeaderText="Autorizo" 
                                                UniqueName="NombreAprobo">
                                            </telerik:GridBoundColumn>
                                           
                                            <telerik:GridTemplateColumn HeaderText="Aprobar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"  Visible="True" Display="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/check.png"
                                                        CssClass="" ToolTip="Aprobar" CommandName="Aprobar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>

                                             <telerik:GridTemplateColumn HeaderText="Rechazar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"  Visible="True" Display="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/delete2.png"
                                                        CssClass="" ToolTip="Rechazar" CommandName="Rechazar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                                                </td>
                                               
                                            </tr>
                                           
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView3" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                     <telerik:RadGrid ID="rg3" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg3_NeedDataSource" OnItemCommand="rg3_ItemCommand" OnPageIndexChanged="rg3_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" 
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                    CellSpacing="0" Culture="es-ES">
                                    <MasterTableView>
                                        <Columns>
                                        <telerik:GridBoundColumn DataField="BdName" HeaderText="Origen" UniqueName="BdName"
                                                Visible="true" Display="true">
                                                <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IdAutorizacion" HeaderText="#.Autorizacion" UniqueName="IdAutorizacion"
                                                Visible="true">
                                                <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaSolicitud" HeaderText="Fecha De Solicitud" UniqueName="FechaSolicitud"
                                                Visible="true">
                                                 <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="ClaveTerritorio" HeaderText="Territorio" UniqueName="ClaveTerritorio"
                                                Visible="true">
                                                 <HeaderStyle Width="50" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="NombreRepresentanteActual" HeaderText="RIK Actual" UniqueName="RIKActual"
                                                Visible="true">
                                                 <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Territorio" HeaderText="Territorio Actual" UniqueName="Territorio">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NombreRepresentante" HeaderText="RIK Cambio" UniqueName="NombreRepresentante">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TerritorioCambio" HeaderText="Territorio Cambio" 
                                                UniqueName="TerritorioCambio">
                                            </telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="Comentario" 
                                                HeaderText="Comentario" 
                                                UniqueName="Comentarios">
                                            </telerik:GridBoundColumn>


                         <telerik:GridTemplateColumn DataField="Activo" HeaderText="Estatus" UniqueName="Activo">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#  Convert.ToBoolean(Eval("Activo")) == true ? "Activo" : "InActivo" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn DataField="NombreAprobador" 
                                                HeaderText="Rechazo" 
                                                UniqueName="Rechazo">
                                            </telerik:GridBoundColumn>
                                           
                                            <telerik:GridTemplateColumn HeaderText="Aprobar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" Visible="True" Display="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/check.png"
                                                        CssClass="" ToolTip="Aprobar" CommandName="Aprobar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>

                                             <telerik:GridTemplateColumn HeaderText="Rechazar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" Visible="True" Display="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/delete2.png"
                                                        CssClass="" ToolTip="Rechazar" CommandName="Rechazar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                                                </td>
                                               
                                            </tr>
                                           
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                   <%-- <telerik:RadPageView ID="RadPageView4" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    Tiempo de vida(días)
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtContTVida" runat="server" MaxLength="2" 
                                                        MinValue="0" TabIndex="1" Width="15px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Longitud mínima
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtContLong" runat="server" MaxLength="2" 
                                                        MinValue="0" TabIndex="2" Width="15px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </telerik:RadPageView>--%>
                                    

                                   

                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadInputManager ID="RadInputManager1" runat="server">
        <telerik:TextBoxSetting BehaviorID="Texto" Validation-ValidationGroup="guardar" ErrorMessage="Requerido"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailContraseña" />
            </TargetControls>
        </telerik:TextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="Numeric6" Validation-ValidationGroup="guardar"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit" MinValue="1"
            MaxValue="999999" DecimalDigits="0" GroupSizes="6">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailPuerto" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="Numeric2" Validation-ValidationGroup="guardar"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit" MinValue="1"
            MaxValue="99" DecimalDigits="0">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtLoginIntentos" />
                <telerik:TargetInput ControlID="TxtLoginTiempoBloqueo" />
                <telerik:TargetInput ControlID="TxtContTVida" />
                <telerik:TargetInput ControlID="TxtContLong" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="Numeric3" Validation-ValidationGroup="guardar"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit" MinValue="1"
            MaxValue="999" DecimalDigits="0">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtOtrosInfo" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
        <telerik:RegExpTextBoxSetting BehaviorID="Correos" Validation-ValidationGroup="guardar"
            ErrorMessage="Correo incorrecto" Validation-IsRequired="True" Validation-ValidateOnEvent="Submit"
            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailUsuario" />
                <telerik:TargetInput ControlID="TxtMailRemitente" />
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
        <telerik:RegExpTextBoxSetting BehaviorID="SMTP" Validation-ValidationGroup="guardar"
            ErrorMessage="SMTP incorrecto" Validation-IsRequired="True" Validation-ValidateOnEvent="Submit"
            ValidationExpression="^(\w+\.)+(\w+)$">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailServidor" />
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    </telerik:RadInputManager>

 <telerik:RadCodeBlock runat="server" ID="rdbScripts">
       <script type="text/javascript">
           function openwin() {

               window.radopen(null, "RadWindow1");
           }
</script>
    </telerik:RadCodeBlock>

</asp:Content>
