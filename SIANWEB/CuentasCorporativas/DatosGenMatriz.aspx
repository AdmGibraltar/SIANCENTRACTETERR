<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatosGenMatriz.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.DatosGenMatriz" 
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

                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rgFranquicias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFranquicias" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

      <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick"
        onclientbuttonclicked="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
        </Items>
    </telerik:radtoolbar>


 <br /> &nbsp;
      <div id="divPrincipal" runat="server">
       <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
            SelectedIndex="0" >
                    <Tabs>
                        <telerik:RadTab runat="server" AccessKey="D" PageViewID="RadDatMaccola" Text="1.- Datos Maccola para la Matriz" ClientIDMode="Inherit"
                                Selected="True">
                        </telerik:RadTab>
                        
                        <telerik:RadTab runat="server" AccessKey="F" PageViewID="RadDatFiscales" Text="2.- Datos Fiscales Adicionales del Cliente ">
                        </telerik:RadTab>

                        <telerik:RadTab runat="server" AccessKey="I" PageViewID="RadIntranet" Text="3.- Intranet Franquicias">
                        </telerik:RadTab>
                    </Tabs>
        </telerik:RadTabStrip>



       <telerik:radmultipage id="RadMultiPage1" runat="server" borderstyle="Solid" borderwidth="1px"
                            scrollbars="Auto" selectedindex="0">
                            <%-- Height="415px" Width="880px">--%>

                        <telerik:RadPageView ID="RadDatMaccola" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="99%" Height="415px"
                                    ResizeMode="AdjacentPane" BorderSize="0" BorderStyle="Solid" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane1" runat="server" Width="99%" Height="410px" OnClientResized="onResize"
                                        BorderColor="Red">

                                    <table width="100%" >      
                                             <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > 1.- Datos Maccola para la Matriz:</th>                                                         
                                                </tr>                                                        
                                              </thead>                                                                                                                             
                                               <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 

                                              <tr>
                                                <td>No. Cliente</td>
                                                <td>
                                                      <telerik:RadNumericTextBox ID="txtNumClienteMac" runat="server" Width="50px">
                                                      <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>

                                                        <asp:RequiredFieldValidator runat="server" ID="Validator1" ErrorMessage="*" Text="*" ControlToValidate="txtNumClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                </td>

                                                 <td>Nombre</td>
                                                <td>
                                                      <telerik:RadTextBox ID="txtNombreClienteMac" runat="server" Width="300px">
                                                        </telerik:RadTextBox>

                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" Text="*" ControlToValidate="txtNombreClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                </td>
                                                    <td>Condiciones </td>
                                                    <td>

                                                       <telerik:RadComboBox ID="csbStrCondicionesClienteMac" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px" >
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                         </ItemTemplate>
                                                        </telerik:RadComboBox>

                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*" Text="*" ControlToValidate="csbStrCondicionesClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                   </td>

                                                   
                                              </tr>
                                              <tr>
                                                  <td>
                                              
                                                  </td>
                                              </tr>
                                              <tr>
                                                <td>Datos de Facturación</td>
                                              </tr>
                                              <tr>
                                                    <td>Calle</td>
                                                    <td> <telerik:RadTextBox ID="txtCalleClienteMac" runat="server" Width="300px">
                                                        </telerik:RadTextBox>
                                                      
                                                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*" Text="*" ControlToValidate="txtCalleClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>Ciudad</td>
                                                    <td>
                                                         <telerik:RadTextBox ID="txtCiudadClienteMac" runat="server" Width="150px">
                                                        </telerik:RadTextBox>

                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ErrorMessage="*" Text="*" ControlToValidate="txtCiudadClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        Zona Postal
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtZonaPostalClienteMac" runat="server" Width="100px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                              </tr>
                                              <tr>
                                                <td>País</td>
                                                <td>    
                                                        <telerik:RadTextBox ID="txtPaisClienteMac" runat="server" Width="150px">
                                                        </telerik:RadTextBox> 

                                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ErrorMessage="*" Text="*" ControlToValidate="txtPaisClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Estado
                                                </td>
                                                <td>
                                                       <telerik:RadTextBox ID="txtEstadoClienteMac" runat="server" Width="150px">
                                                        </telerik:RadTextBox> 

                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ErrorMessage="*" Text="*" 
                                                        ControlToValidate="txtEstadoClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    RFC
                                                </td>
                                                <td>
                                                      <telerik:RadTextBox ID="txtRFCClienteMac" runat="server" Width="150px">
                                                      </telerik:RadTextBox> 

                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ErrorMessage="*" Text="*" 
                                                        ControlToValidate="txtRFCClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>
                                                </td>
                                              </tr>
                                              <tr>
                                              <td>Colonia</td>
                                                <td colspan="3">
                                                      <telerik:RadTextBox ID="txtColoniaClienteMac" runat="server" Width="300px">
                                                      </telerik:RadTextBox> 

                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ErrorMessage="*" Text="*" 
                                                        ControlToValidate="txtColoniaClienteMac"
                                                        ValidationGroup="Guardar" 
                                                        ></asp:RequiredFieldValidator>

                                                </td>

<%--                                            <td>Tipo</td>
                                                <td>
                                                   <telerik:RadComboBox ID="cmbStrTipoClienteMac" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Cd_Nombre" DataValueField="Id_Cd" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Cd").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Cd").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cd_Nombre") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                 </td>--%>
                                              </tr>
                                              <tr>
                                                    <td>Territorio</td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtTerritorioClienteMac" runat="server" Width="150px" Value="99" ReadOnly="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox> 
                                                     </td>
                                              </tr>
                                              <tr>
                                                    <td>Vendedor</td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtVendedorClienteMac" runat="server" Width="150px" Value="100" ReadOnly="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox> 
                                                    </td>
                                                    <td>
                                                        Tarifa de Impuesto
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtTarifaImpClienteMac" runat="server" Width="150px" Value="11" ReadOnly="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox> 
                                                    </td>
                                                    <td>
                                                        Medio Envio: 
                                                    </td>
                                                    <td>

                                                      <telerik:RadTextBox ID="txtMedioEnvioClienteMac" runat="server" Width="150px" Text="001" ReadOnly="true">
                                                      </telerik:RadTextBox> 
                                                    </td>
                                              </tr>
                                              <tr>
                                                <td>
                                                Convenios Precios Especiales:</td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbConvenioId" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="PC_Nombre" DataValueField="Id_PC" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_PC").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_PC").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_Nombre") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                </td>
                                              </tr>
                                               <tr>
                                                 <td></td>
                                              </tr> 
                                              <tr>
                                                <td colspan="2"></td>
                                                <td> 
                                                     <asp:CheckBox ID="chkGravableClienteMac" runat="server" Text="Gravable" />
                                                </td>
                                              
                                              </tr>
                                               <tr>
                                                <td colspan="2"></td>
                                                <td> 
                                                     <asp:CheckBox ID="chkPagoComisionClienteMac" runat="server" Text="Pago Comisión" />
                                                </td>
                                         
                                              </tr>
                                              <tr>
                                                <td colspan="2"></td>
                                                <td> 
                                                     
                                                </td>
                                              
                                              </tr>
                                                
                                        </table>




                                </telerik:RadPane>
                            </telerik:RadSplitter>
                          </telerik:RadPageView>


                              <telerik:RadPageView ID="RadDatFiscales" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="99%" Height="415px"
                                    ResizeMode="AdjacentPane" BorderSize="0" BorderStyle="Solid" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane2" runat="server" Width="99%" Height="410px" OnClientResized="onResize"
                                        BorderColor="Red">

                                        <table width="100%">
                                               <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > 2.- Datos Fiscales Adicionales al cliente</th>                                                         
                                                </tr>                                                        
                                              </thead>       
                                        </table>
                                        <br /><br />

                                              <telerik:RadGrid ID="rgDirFiscales" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        BorderStyle="None" ShowFooter="true"
                                                        OnNeedDataSource="rgDirFiscales_NeedDataSource" OnItemCommand="rgDirFiscales_ItemCommand"
                                                         >
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id">
                                                        <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                         <Columns>
                                                              <telerik:GridTemplateColumn DataField="ClienteDirFis" HeaderText="Cliente." UniqueName="ClienteDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblCliente" runat="server" Text='<%# Bind("ClienteDirFis") %>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtClienteDirFis" runat="server" Text='<%# Bind("ClienteDirFis") %>'
	                                                                   MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox>
                                                                    
                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>

                                                             <telerik:GridTemplateColumn DataField="DireccionDirFis" HeaderText="Dirección." UniqueName="DireccionDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("DireccionDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtDireccionDirFis" runat="server" Text='<%# Bind("DireccionDirFis") %>'
	                                                                     MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                             <telerik:GridTemplateColumn DataField="EstadoDirFis" HeaderText="Estado." UniqueName="EstadoDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblEstadoDirFis" runat="server" Text='<%# Bind("EstadoDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtEstadoDirFis" runat="server" Text='<%# Bind("EstadoDirFis") %>'
	                                                                     MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                            <telerik:GridTemplateColumn DataField="ColoniaDirFis" HeaderText="Colonia." UniqueName="ColoniaDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblColoniaDirFis" runat="server" Text='<%# Bind("ColoniaDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtColoniaDirFis" runat="server" Text='<%# Bind("ColoniaDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn DataField="CPDirFis" HeaderText="CP." UniqueName="CPDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblCPDirFis" runat="server" Text='<%# Bind("CPDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtCPDirFis" runat="server" Text='<%# Bind("CPDirFis") %>'
	                                                                     MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                          <telerik:GridTemplateColumn DataField="MunicipioDirFis" HeaderText="Municipio" UniqueName="MunicipioDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblMunicipioDirFis" runat="server" Text='<%# Bind("MunicipioDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtMunicipioDirFis" runat="server" Text='<%# Bind("MunicipioDirFis") %>'
	                                                                     MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>



                                                            <telerik:GridTemplateColumn DataField="RFCDirFis" HeaderText="RFC" UniqueName="RFCDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblRFCDirFis" runat="server" Text='<%# Bind("RFCDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtRFCDirFis" runat="server" Text='<%# Bind("RFCDirFis") %>'
	                                                                     MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn DataField="EmailFacturasDirFis" HeaderText="Email Facturas" UniqueName="EmailFacturasDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblEmailFacturasDirFis" runat="server" Text='<%# Bind("EmailFacturasDirFis") %>'></asp:Label> 
                                                                     </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtEmailFacturasDirFis" runat="server" Text='<%# Bind("EmailFacturasDirFis") %>'
	                                                                     MaxLength="100" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                             <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>

                                                             <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                             </telerik:GridButtonColumn>


                                                         </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>

                            



                                </telerik:RadPane>
                            </telerik:RadSplitter>
                          </telerik:RadPageView>

                           <telerik:RadPageView ID="RadIntranet" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter3" runat="server" Width="99%" Height="615px"
                                    ResizeMode="AdjacentPane" BorderSize="0" BorderStyle="Solid" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane3" runat="server" Width="99%" Height="610px" OnClientResized="onResize"
                                        BorderColor="Red">



                                    <table width="100%" >       
                                              <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > 3.- Intranet Franquicias</th>                                                         
                                                </tr>                                                        
                                              </thead>                                                                                                                
                                           <tr>
                                                <td></td>
                                           </tr> 
                                           <tr>
                                                <td></td>
                                           </tr> 
                                           <tr>
                                                <td></td>
                                           </tr> 
                                           <tr>
                                                <td></td>
                                           </tr> 

                                           <tr>
                                                <td>Tipo de Moneda: </td>
                                                <td> 
                                                     <telerik:RadComboBox ID="cmbTipoMoneda" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Mon_Descripcion" DataValueField="Id_Mon" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Mon").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Mon").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mon_Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                </td>
                                           </tr>
                                           <tr>
                                                <td><asp:label runat="server" ID="Label1" Text="Productos Permitidos:"></asp:label> </td>
                                                <td>
                                                      <telerik:RadComboBox ID="cmbProdPermitidos" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                </td>
                                                
                                                <td><asp:label runat="server" ID="lblCatEspecial" Text="Catalogo Especial:"></asp:label> </td>
                                                <td>
                                                      <telerik:RadComboBox ID="cmbCatEspecial" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                </td>

                                           </tr>
                                           <tr>
                                                <td colspan="4"><asp:CheckBox ID="chkFactura" runat="server" Text="Factura" />  
                                               <asp:CheckBox ID="chkFacturaExt" runat="server" Text="Factura Extemporánea" />  
                                                <asp:CheckBox ID="chkNotaCredito" runat="server" Text="Nota de Crédito" />  
                                                <asp:CheckBox ID="chkNotaCargo" runat="server" Text="Nota de Cargo" />  
                                                <asp:CheckBox ID="chkCancelacion" runat="server" Text="Cancelación" />  </td>
                                           </tr>
                                           <tr>
                                                <td colspan=6><asp:CheckBox ID="chkFechaRevision" runat="server" Text="Fecha Revisión" />
                                                <asp:label runat="server" ID="Label2" Text="Días de Crédito:"></asp:label>
                                                        <telerik:RadNumericTextBox ID="txtDiasCredito" runat="server" Width="150px">
                                                        </telerik:RadNumericTextBox> 
                                                
                                                <asp:CheckBox ID="chkCargaAcuse" runat="server" Text="Carga de Acuse" />
                                                <asp:CheckBox ID="chkAddenda" runat="server" Text="Addenda" />
                                               
                                                      <telerik:RadComboBox ID="cmbAddendaTipo" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                        </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                    <asp:CheckBox ID="chkDescuentos" runat="server" Text="Descuentos" /></td>
                                           </tr>
                                           <tr>
                                                <td><asp:label runat="server" ID="Label3" Text="Tipo de nota de crédito:"></asp:label></td>
                                                <td>
                                                     <telerik:RadComboBox ID="cmbTipoNotaCred" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                </td>
                                                <td><asp:label runat="server" ID="Label4" Text="Porcentaje Requerido:"></asp:label></td>
                                                <td>
                                                        <telerik:RadNumericTextBox ID="dblPorcRequerido" runat="server" Width="150px">
                                                        </telerik:RadNumericTextBox> 
                                                </td>
                                           </tr>
                                           <tr>
                                                <td><asp:label runat="server" ID="Label5" Text="Metodo de Pago:"></asp:label></td>
                                                <td>
                                                       <telerik:RadComboBox ID="cmbMetodoPago" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                
                                                </td>
                                                <td><asp:label runat="server" ID="Label7" Text="Num. Cta./Trjta (ult. 4 Digitos):"></asp:label>
                                                </td>
                                                <td>
                                                     <telerik:RadTextBox ID="txtNumTarjeta" runat="server" Text=''
	                                                                     MaxLength="9" 
																		 Width="100px">
                                                    </telerik:RadTextBox>
                                                </td>
                                           </tr>

                                           <tr>
                                                <td><asp:label runat="server" ID="Label6" Text="Soportes a solicitar:"></asp:label></td>
                                                <td>
                                                       <telerik:RadComboBox ID="cmbSoportes" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="370px" ReadOnly="True"
                                                            MaxHeight="250px" CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID"   runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>
                                                
                                                </td>
                                           </tr>
                                    </table> 
                                    <br />
                                    <br />
                                    
                                               <telerik:RadGrid ID="rgFranquicias" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        BorderStyle="None" ShowFooter="true"
                                                        OnNeedDataSource="rgFranquicias_NeedDataSource" OnItemCommand="rgFranquicias_ItemCommand"
                                                        OnItemDataBound="rgFranquicias_ItemDataBound" Width="90%"
                                                        >

                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id">
                                                        <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                         <Columns>

                                                              <telerik:GridTemplateColumn DataField="FranqConsecionada" HeaderText="Franq Consecionada." UniqueName="FranqConsecionada">
                                                                   <HeaderStyle Width="100px" />
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblFranqConsecionada" runat="server" Text='<%# Bind("FranqConsecionada") %>'></asp:Label> 
                                                                    </ItemTemplate>

                                                                    <EditItemTemplate>

                                                                    <telerik:RadNumericTextBox ID="txtFranqConsecionada" runat="server" Width="100px" DbValue='<%# Bind("FranqConsecionada" ) %>' >
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>


                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                               
                                                              </telerik:GridTemplateColumn>

                                                              <telerik:GridTemplateColumn DataField="ClienteMac" HeaderText="Cliente Maccola" UniqueName="ClienteMac" >
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblClienteMac" runat="server" Text='<%# Bind("ClienteMac") %>'></asp:Label> 
                                                                    </ItemTemplate>

                                                                    <EditItemTemplate>

                                                                   <telerik:RadNumericTextBox ID="txtClienteMac" runat="server" Width="100px" DbValue='<%# Bind("ClienteMac" ) %>' >
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="100px" />

                                                              </telerik:GridTemplateColumn>


                                                            <telerik:GridTemplateColumn DataField="UsuarioIntranet" HeaderText="Usuario Intranet" UniqueName="UsuarioIntranet">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblUsuarioIntranet" runat="server" Text='<%# Bind("NombreUsuario") %>'></asp:Label> 
                                                                    </ItemTemplate> 

                                                                    <EditItemTemplate>
                                                                        
                                                                       <asp:Label ID="lblUsuarioIntranetId" runat="server" Text='<%# Bind("UsuarioIntranet") %>' Visible='false'></asp:Label>
                                                                       <telerik:RadComboBox ID="cmbUsuarioIntranet" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                                            DataTextField="Usu_Completo" DataValueField="Usu_IdUsuario" EmptyMessage="Seleccione..."
                                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                            MarkFirstMatch="true" 
                                                                             Width="300px" 
                                                                            MaxHeight="250px" OnDataBound="cmbUsuarioIntranet_DataBound">
                                                                            
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Usu_IdUsuario").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Usu_IdUsuario").ToString() %>'
                                                                                                        Width="50px" />
                                                                                                </td>
                                                                                                <td style="text-align: left">
                                                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# string.Concat(Eval("Usu_Nombre"), " ", Eval("Usu_ApPaterno")," ", Eval("Usu_ApMaterno")) %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                          </table>
	                                                                                </ItemTemplate>
                                                                               </telerik:RadComboBox>

                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="200px" />
                                                    </telerik:GridTemplateColumn>


                                                    <telerik:GridTemplateColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblMonedaNombre" runat="server" Text='<%# Bind("NombreMoneda") %>'></asp:Label> 
                                                                    </ItemTemplate>

                                                                    <EditItemTemplate>

                                                                      <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("Moneda") %>' Visible='false'></asp:Label>
                                                                     <telerik:RadComboBox ID="cmbMoneda" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                                            DataTextField="Mon_Descripcion" DataValueField="Id_Mon" EmptyMessage="Seleccione..."
                                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                            MarkFirstMatch="true" 
                                                                             Width="300px" 
                                                                            MaxHeight="250px" OnDataBound="cmbMoneda_DataBound" >
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Mon").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Mon").ToString() %>'
                                                                                                        Width="50px" />
                                                                                                </td>
                                                                                                <td style="text-align: left">
                                                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mon_Descripcion") %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                          </table>
	                                                                                </ItemTemplate>
                                                                               </telerik:RadComboBox>

                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="300px" />
                                                         </telerik:GridTemplateColumn>

                                                         
                                                         <telerik:GridTemplateColumn DataField="Productos" HeaderText="Productos" UniqueName="Productos">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblProductosNombre" runat="server" Text='<%# Bind("NombreProdPermitidos") %>'></asp:Label> 
                                                                    </ItemTemplate>

                                                                    <EditItemTemplate>
                                                                       <asp:Label ID="lblProductos" runat="server" Text='<%# Bind("Productos") %>' Visible='false'></asp:Label>

                                                                       <telerik:RadComboBox ID="cmbProductos" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                            MarkFirstMatch="true" 
                                                                             Width="300px" ReadOnly="True"
                                                                            MaxHeight="250px" OnDataBound="cmbProductos_DataBound" >
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                                        Width="50px" />
                                                                                                </td>
                                                                                                <td style="text-align: left">
                                                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                          </table>
	                                                                                </ItemTemplate>
                                                                               </telerik:RadComboBox>

                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="300px" />
                                                         </telerik:GridTemplateColumn>

                                                         <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>

                                                             <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                             </telerik:GridButtonColumn>




<%--                                                    <telerik:GridTemplateColumn DataField="SistemaPV" HeaderText="Sistema" UniqueName="SistemaPV">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblSistemaPV" runat="server" Text='<%# Bind("SistemaPV") %>'></asp:Label> 
                                                                    </ItemTemplate>

                                                                    <EditItemTemplate>

                                                                       <telerik:RadComboBox ID="cmbSistemaPV" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                            MarkFirstMatch="true" 
                                                                             Width="370px" ReadOnly="True"
                                                                            MaxHeight="250px" OnDataBinding="cmbSistemaPV_DataBinding" >
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                                        Width="50px" />
                                                                                                </td>
                                                                                                <td style="text-align: left">
                                                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                          </table>
	                                                                                </ItemTemplate>
                                                                               </telerik:RadComboBox>

                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                         </telerik:GridTemplateColumn>
--%>
                                                         </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>

                                </telerik:RadPane>
                            </telerik:RadSplitter>
                          </telerik:RadPageView>



            </telerik:radmultipage>
                  </div>


      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function onResize(sender, eventArgs) {

            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
            }

            function CloseWindow() {
                GetRadWindow().Close();
            }

            function CloseAlert(mensaje) {
                //                var cerrarWindow = radalert(mensaje, 330, 150);
                //                cerrarWindow.add_close(
                //                    function () {

                alert(mensaje);
                CloseWindow();
                //                    });
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
     </telerik:radcodeblock>
     

   
     
</asp:Content>
