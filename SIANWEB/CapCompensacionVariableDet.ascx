<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CapCompensacionVariableDet.ascx.cs" Inherits="SIANWEB.CapCompensacionVariableDet" %>
<%--<%@ Control Language="c#" Inherits="Telerik.GridExamplesCSharp.DataEditing.UserControlEditForm.CapCompensacionVariableDet"
    CodeFile="CapCompensacionVariableDet.ascx.cs" %>--%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 
    <div runat="server" id="divPrincipaldetalle">
 

 <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_FormulaPaso" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenId" runat="server" />
                    <asp:HiddenField ID="EsAlta" runat="server" Value = '<%# (Container is GridEditFormInsertItem) ? true : false %>'  />
                    <asp:HiddenField ID="HF_ID" runat="server" />

                       
                </td>
            </tr>
        </table>


         <table>
                        <%-- Encabezados--%>
                        <tr>
                            <td style="width: 10%;">
                                <asp:Label ID="lblConcepto" runat="server" Text="Concepto" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 8%;">
                                <asp:Label ID="lblOperador" runat="server" Text="Operador" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 26%;">
                                <asp:Label ID="lblVariable" runat="server" Text="Variable" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 5%;">
                                <asp:Label ID="Label3" runat="server" Text="Valor" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:Button ID="imgBoton" runat="server" Text="Agregar" ToolTip="Actualizar" MaxHeight="50px"
                                    OnClick="btnAgregar_Click" /><br />
                                <asp:Button ID="btnEditar" runat="server" Text="Aceptar" ToolTip="Actualiza los cambios" MaxHeight="50px"
                                    OnClick="btnEditar_Click"  visible="false" /><br />
                                     <asp:Button ID="btnCancelarEditar" runat="server" Text="Ignorar" ToolTip="Cancelar Edición" MaxHeight="50px"
                                    OnClick="btnCancelarEditar_Click" visible="false"/>
                            </td>
                            <td style="width: 12%;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                <telerik:RadTextBox ID="txtConcepto" runat="server" Width="100px" MaxLength="30"  readonly ='<%# (Container is GridEditFormInsertItem) ? false : true %>'   Text='<%# DataBinder.Eval(Container, "DataItem.sVariable_Local") %>'  >
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>

                               
                            </td>

                         

                            <td style="width: 20%;">
                                <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="200px" MaxLength="100" readonly ='<%# (Container is GridEditFormInsertItem) ? false : true %>'  Text='<%# DataBinder.Eval(Container, "DataItem.sVariable_Descripcion") %>' >
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>

                            <td style="width: 8%;">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="cmbOperador" runat="server" Width="50"
                                    Label="">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="+" Value="1" />
                                        <telerik:RadComboBoxItem Text="-" Value="2" />
                                        <telerik:RadComboBoxItem Text="*" Value="3" />
                                        <telerik:RadComboBoxItem Text="/" Value="4" />
                                        <telerik:RadComboBoxItem Text="%" Value="5" />
                                        <telerik:RadComboBoxItem Text="(" Value="6" />
                                        <telerik:RadComboBoxItem Text=")" Value="7" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 26%;">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="cmbVariables" runat="server" Width="300"
                                    Label="">
                                    <Items>
                                        <telerik:RadComboBoxItem Text=" " Value="0" />
                                        <telerik:RadComboBoxItem Text="(IVC) Importe de venta cobrada" Value="1" />
                                        <telerik:RadComboBoxItem Text="(UP) Utilidad prima" Value="2" />
                                        <telerik:RadComboBoxItem Text="(ASP) Amortización de sistemas propietarios" Value="3" />
                                        <telerik:RadComboBoxItem Text="(GTS) Gastos de tecnico de servicio" Value="4" />
                                        <telerik:RadComboBoxItem Text="(AAER) Amortización anticipada de sistemas propietarios"
                                            Value="5" />
                                        <telerik:RadComboBoxItem Text="(MO) Mano de obra en proyectos" Value="6" />
                                        <telerik:RadComboBoxItem Text="(AEA) Amortización de equipos arrendados" Value="7" />
                                        <telerik:RadComboBoxItem Text="(FC) Factor por cobranza" Value="8" />
                                        <telerik:RadComboBoxItem Text="(UBC) Utilidad bruta del cliente ajustada por cobranza"
                                            Value="9" />
                                        <telerik:RadComboBoxItem Text="(FPPP) Factor por porcentaje de participacion ponderado"
                                            Value="10" />
                                        <%--<telerik:RadComboBoxItem Text="(CP) Comisión preliminar" Value="11" />--%>
                                        <telerik:RadComboBoxItem Text="(MVI) Multiplicador por venta incremental" Value="11" />
                                        
                                        <telerik:RadComboBoxItem Text="(CND) Comisiónes no devengadas (Factor 0)" Value="12" />
                                        <telerik:RadComboBoxItem Text="(SF) Sueldo fijo" Value="13" />
                                        <telerik:RadComboBoxItem Text="(TSF) Tope del sueldo fijo ( 40%)" Value="14" />
                                        <%--<telerik:RadComboBoxItem Text="(MVI) Multiplicador por venta incremental" Value="15" />--%>
                                        <telerik:RadComboBoxItem Text="(GA) Gasto administrativo" Value="16" />
                                        <telerik:RadComboBoxItem Text="(CB) Comision bruta (pago por nomina)" Value="17" />
                                        <telerik:RadComboBoxItem Text="Constante de Usuario" Value="15" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 5%;">
                                <telerik:RadTextBox ID="txtValor" runat="server" Width="50px" MaxLength="5">
                                </telerik:RadTextBox>
                            </td>
                            <td style="width: 15%;">
                                <asp:Button ID="btnTerminar" runat="server" Text="Terminar" ToolTip="Terminar" MaxHeight="50px"
                                    OnClick="btnTerminar_Click" visible="false"/>
  
                            </td>
                            <td style="width: 12%;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                           
                     
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id_VariableLocal") %>' Visible="false">
                        </asp:TextBox>
                  
                            </td>
                            <td style="width: 20%;">
                            </td>
                            <td style="width: 8%;">
                            </td>
                            <td style="width: 26%;">
                            </td>
                            <td style="width: 5%;">
                            </td>
                            <td style="width: 15%;">
                            </td>
                            <td style="width: 12%;">
                            </td>
                        </tr>
                    </table>
                    <br />

<table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
    style="border-collapse: collapse;">
  <%--  <tr class="EditFormHeader">
         
    </tr>--%>
    <tr>
        <td style="vertical-align: top">
            <table id="Table1" cellspacing="1" cellpadding="1" width="250" border="0" class="module">
                <tr>
                    <td>
                        Comentarios:
                    </td>
                    <td style="vertical-align: top">
                        Formulas:
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtComentarios" Text='<%# DataBinder.Eval(Container, "DataItem.sVariable_Comentarios") %>'
                            runat="server" TextMode="MultiLine" Rows="3" Columns="40" TabIndex="5">
                        </asp:TextBox>
                    </td>
                    
                    <td style="vertical-align: top">
                        <%--  <asp:TextBox ID="txtArea" Text='<%# DataBinder.Eval(Container, "DataItem.sVariable_Formula") %>' runat="server" TextMode="MultiLine"
                            Rows="2" Columns="40" TabIndex="6">
                        </asp:TextBox>--%>
                        <telerik:RadTextBox ID="txtArea" runat="server" Width="350px" MaxLength="350" ReadOnly="true">
                        </telerik:RadTextBox>
                        <asp:TextBox ID="txtFormula" Text='<%# DataBinder.Eval(Container, "DataItem.sVariable_Formula") %>'
                            runat="server" TextMode="MultiLine" Rows="2" Columns="40" TabIndex="6" Visible="false">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
          
        </td>
    </tr>
</table>

            <table>
                    <tr>
                        <td>

                        <%--quitar el editmode par ala edición en los campos y no en el grid  EditMode="InPlace"   EditMode="InPlace"   --%>
                            <telerik:RadGrid ID="rgGrid" runat="server" CommandItemDisplay="Top" GridLines="None"
                                AutoGenerateColumns="False" OnNeedDataSource="rgGrid_NeedDataSource" OnInsertCommand="rgGrid_InsertCommand"
                                 OnItemCommand="rgGrid_ItemCommand" OnItemDataBound="rgGrid_ItemDataBound"
                                OnUpdateCommand="rgGrid_UpdateCommand" PageSize="100" AllowPaging="True" DataMember="listaOrdCompraDet">
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_ConceptoVariable,Concepto_Descripcion,Concepto_Operador,Concepto_TipoVariable"
                                    DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                    NoMasterRecordsText="No se encontraron registros.">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"
                                        RefreshText="Actualizar" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Renglón" DataField="Id_ConceptoVariable"
                                            UniqueName="Id_ConceptoVariable" ReadOnly="true" Display="false">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblId_GVComprobante" runat="server" Text='<%# Eval("Id_ConceptoVariable").ToString() %>'
                                                    ReadOnly="true" Display="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Concepto" DataField="Concepto_Descripcion"
                                            UniqueName="Concepto_Descripcion" Display="true">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_Descripcion" runat="server" Text='<%# Eval("Concepto_Descripcion").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Concepto_Observaciones"
                                            UniqueName="Concepto_Observaciones" Display="true">
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_Observaciones" runat="server" Text='<%# Eval("Concepto_Observaciones").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Operador" DataField="Concepto_Operador" UniqueName="Concepto_Operador">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_Operador" runat="server" Text='<%# Eval("Concepto_Operador").ToString() %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtConcepto_Operador" runat="server" Width="65px" MaxLength="100"
                                                    AutoPostBack="true" Text='<%# Eval("Concepto_Operador") %>'>
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtConcepto_Operador" runat="server" ForeColor="#FF0000" Text='<%# Eval("Concepto_Operador").ToString() %>'
                                                    Visible="false"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Concepto_TipoVariable" DataField="Concepto_TipoVariable"
                                            UniqueName="Concepto_TipoVariable" Display="false">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_TipoVariable" runat="server" Text='<%# Eval("Concepto_TipoVariable").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Concepto_IdVariable" DataField="Concepto_IdVariable"
                                            UniqueName="Concepto_IdVariable" Display="false">
                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_IdVariable" runat="server" Text='<%# Eval("Concepto_IdVariable").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Variable" DataField="Concepto_VariableDescripcion"
                                            UniqueName="Concepto_VariableDescripcion">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto_VariableDescripcion" runat="server" Text='<%# Eval("Concepto_VariableDescripcion").ToString() %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtConcepto_VariableDescripcion" runat="server" Width="65px"
                                                    MaxLength="100" AutoPostBack="true" Text='<%# Eval("Concepto_VariableDescripcion") %>'>
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtConcepto_VariableDescripcion" runat="server" ForeColor="#FF0000"
                                                    Text='<%# Eval("Concepto_VariableDescripcion").ToString() %>' Visible="false"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                       <%-- <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar"
                                            HeaderText="Editar">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>--%>
                                          <%--  agregar boton de edicion--%>
                                         <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                    CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Eliminar" CommandName="Delete"
                                            ConfirmDialogType="RadWindow" ConfirmText="¿Desea eliminar el elemento?" Text="Cancelar"
                                            UniqueName="DeleteColumn" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                        </telerik:GridButtonColumn>
                                    

                                    </Columns>
                                </MasterTableView>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, reg. <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="15" />
                            </telerik:RadGrid>
                        </td>
                        <td style="vertical-align: top" >
                        
                          <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insertar" : "Actualizar" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancelar" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>

                        </td>
                    </tr>
                </table>
 
 </div>
