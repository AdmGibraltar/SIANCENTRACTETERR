 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProCargaRentabilidadExcepciones.aspx.cs" Inherits="SIANWEB.ProCargaRentabilidadExcepciones" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
 

        <script type="text/javascript">





            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }


            }


            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de conceptos dispara edición
            //--------------------------------------------------------------------------------------------------
            function grdConceptoNomina_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }


            function KeyPress(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                //debugger;
                //                if (c == 39)
                //                    eventArgs.set_cancel(true);
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

        </script>
 
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTabStripPrincipal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="formulario" id="divPrincipal" runat="server">
        <asp:HiddenField ID="hiddenId" runat="server" />
        <asp:HiddenField ID="hiddenRefrescapagina" runat="server" />

          

        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
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
         
       <div>
          <asp:Label ID="lblMensaje" runat="server"></asp:Label>
          </div>
        <div runat="server" id="formularioColaboradors" style="margin-left: 10px; margin-right: 10px;">
            <%--<telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                SelectedIndex="0" TabIndex="-1">
                <Tabs>
                    <telerik:RadTab runat="server" Text="C&lt;u&gt;a&lt;/u&gt;rga Archivo" AccessKey="A" PageViewID="RadPageViewCargaArchivo"> 
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>--%>
            <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                Width="800px">
                <!-- Aqui empieza el contenido de los tabs--->
             
                <telerik:RadPageView ID="RadPageViewCargaArchivo" runat="server" heigth="370px">
                            <telerik:RadSplitter ID="RadSplitter5" runat="server" Height="370px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                                <telerik:RadPane ID="RadPane5" runat="server" Height="370px" OnClientResized="onResize"
                                    BorderStyle="None" Scrolling="None">
                                    <div runat="server" id="CargaArchivo">
                                <table>
                                    <tr>
                                    <td colspan="3">
                                        <telerik:RadAsyncUpload runat="server" ID="RadUpload1" AllowedFileExtensions="xls,xlsx"
                                            Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                            ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30">
                                            <Localization Remove="Quitar" Select="Seleccionar" />
                                        </telerik:RadAsyncUpload>
                                        <asp:Panel ID="ValidFiles" runat="server">
                                       </asp:Panel>&nbsp;<a href="PlantillaCompensaciones.xlsx">Descarga Plantilla de Excel</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="buttonSubmit" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                            Style="margin-top: 6px" OnClick="btnImportar_Click" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>

                            <telerik:RadGrid ID="RgDet" runat="server" GridLines="None" DataMember="listaConceptosNomina"
                                                    PageSize="15" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"  OnPageIndexChanged="RgDet_PageIndexChanged_PageIndexChanged">
                                                 
                                                    <MasterTableView Name="Master" CommandItemDisplay="None" 
                                                        EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="15"
                                                        Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                        <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Actualizar" />  <%--DataKeyNames="Id_Emp,Id_Cd,Id_empleado, Id_Colaborador,Id_Compensacion,Id_Compensacion_Monto"--%>
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="Registro" UniqueName="RgDId" DataField="RgDId"
                                                                Display="true" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="EMPRESA" UniqueName="Id_Emp" DataField="Id_Emp" Display="true"
                                                                ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="CDI" UniqueName="Id_Cd" DataField="Id_Cd"
                                                                Display="true" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Rik" UniqueName="Id_Rik" DataField="Id_Rik"
                                                                Display="true" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Cliente" UniqueName="Id_Cte"
                                                                DataField="Id_Cte" Display="true" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Estatus" UniqueName="estatus_rentabilidad"
                                                                DataField="estatus_rentabilidad" Display="true" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Rentabilidad" UniqueName="rentabilidad"
                                                                DataField="rentabilidad" Display="true" ReadOnly="true">
                                                            </telerik:GridBoundColumn>

                                                          <%--<telerik:GridTemplateColumn HeaderText="Estatus" DataField="estatus_rentabilidad" UniqueName="estatus_rentabilidad">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSueldo" runat="server" Text='<%# Eval("rentabilidad") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtSueldo" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Sueldo") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>--%>

                                                        </Columns>
                                                       
                                                    </MasterTableView>
                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings>
                                                        <ClientEvents OnRowDblClick="grdConceptoNomina_ClientRowDblClick" />
                                                        <Selecting AllowRowSelect="true" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>


                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="370px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                            

                                    <asp:HiddenField ID="HiddenRebind" runat="server" />
                                    <asp:HiddenField ID="HF_ID" runat="server" />
                                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />

                            </telerik:RadSplitter>
                </telerik:RadPageView>

            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>



