<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatAlmacen.aspx.cs" Inherits="SIANWEB.CatAlmacen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript">
       function SoloNumeros(evt) {
           var charCode = (evt.which) ? evt.which : event.keyCode
           if (charCode > 31 && (charCode < 48 || charCode > 57))
               return false;

           return true;
       }

  
       //-->

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings> 
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkMultiOficina">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListBox1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTipoUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
            Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton ToolTip="Imprimir" CommandName="print" CssClass="print"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Ver detalles" CommandName="details" CssClass="edit" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
                <telerik:RadToolBarButton ToolTip="Nuevo" CommandName="new" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
    
            </Items>
        </telerik:RadToolBar>
     

 <table width ="100%" style="font-family: Verdana; font-size: 8pt">
 <tr>
 <td colspan ="5">
 &nbsp; <asp:Label ID="lblMensaje" runat="server"></asp:Label>
 <asp:HiddenField id= "HdId_Alm" runat = "server"/>
 <asp:HiddenField ID="HF_ClvPag" runat ="server" />
 </td>
 </tr>
 <tr>
 <td>
 &nbsp;
 </td>
 <td >
    
 Clave
 </td>
 <td >
  <telerik:radnumerictextbox ID="TxtAlm_Clave" Width="100px" runat="server" 
         MaxLength="20">
 <NumberFormat DecimalDigits="0" GroupSeparator="" />
  </telerik:radnumerictextbox>
  &nbsp;&nbsp;
    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                    ToolTip="Buscar" ValidationGroup="buscar" 
         onclick="imgBuscar_Click" />

 </td>
 <td  >

     <asp:RequiredFieldValidator ID="RfvTxtAlm_Clave" runat="server" 
         ErrorMessage="*Requerido" ControlToValidate="TxtAlm_Clave" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>

   </td>
 <td >

</td>

 </tr>
  <tr>
 <td>
  &nbsp;
 </td>
 <td >
    
Nombre
 </td>
 <td >
  <telerik:radtextbox ID="TxtAlm_Nombre" width= "300px" runat="server" 
         MaxLength="200"></telerik:radtextbox>
 </td>
 <td  >
 
     <asp:RequiredFieldValidator ID="RfvTxt_Nombre" runat="server" 
         ErrorMessage="*Requerido" ControlToValidate="TxtAlm_Nombre" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
   </td>
 <td width="100%">
</td>

 </tr>
   <tr>
 <td>
  &nbsp;
 </td>
 <td >
    
Cuenta
 </td>
 <td >
  <telerik:radtextbox ID="TxtAlm_Cuenta" width="150px" runat="server" onkeypress="return SoloNumeros(event)"
         MaxLength="5">
  </telerik:radtextbox>
 </td>
 <td  >

   </td>
 <td width="100%">
</td>

 </tr>
   <tr>
 <td>
  &nbsp;
 </td>
 <td >
    
Subcuenta
 </td>
 <td >
  <telerik:radtextbox ID="TxtAlm_SubCuenta" width="150px" runat="server" onkeypress="return SoloNumeros(event)"
         MaxLength="2">
  </telerik:radtextbox>
 </td>
 <td  >

   </td>
 <td width="100%">
</td>

 </tr>
     <tr>
 <td>
  &nbsp;
 </td>
 <td  colspan = "2" style="font-weight: bold">
&nbsp;
 </td>

 <td >

   </td>
 <td width="100%">
</td>

 </tr>

    <tr>
 <td>
  &nbsp;
 </td>
 <td  colspan = "2" style="font-weight: bold">
    
Centro de costos
 </td>

 <td >

   </td>
 <td width="100%">
</td>

 </tr>
    <tr>
 <td>
  &nbsp;
 </td>
 <td >
    
Cuenta
 </td>
 <td >
  <telerik:radtextbox ID="TxtAlm_CtaCenCosto" width="150px" runat="server" onkeypress="return SoloNumeros(event)"
         MaxLength="5">
  </telerik:radtextbox>
 </td>
 <td  >

   </td>
 <td width="100%">
</td>

 </tr>
    <tr>
 <td>
  &nbsp;
 </td>
 <td >
    
Subcuenta
 </td>
 <td >
  <telerik:radtextbox ID="TxtAlm_SubCtaCenCosto" width="150px" runat="server" onkeypress="return SoloNumeros(event)"
         MaxLength="5">
  </telerik:radtextbox>
 </td>
 <td  >

   </td>
 <td width="100%">
</td>

 </tr>

 <tr>
 <td>
  &nbsp;
 </td>
 <td>
 </td>
 <td>
  <asp:CheckBox ID="ChkAlm_Activo" runat="server" Text="Activo" />
 </td>
 <td>
 </td>
 <td width="100%">
 </td>
 </tr>
 </table>
 <br />
 <table width="600px" id="TblDetalles" runat = "server" visible="False">
 <tr>
 <td>
  &nbsp;
 </td>
 <td>
  <telerik:RadGrid ID="GrdAlmacen" runat="server" AutoGenerateColumns="False" 
         AllowPaging="True" Width ="500px"
                                    PageSize="15"  
         MasterTableView-NoMasterRecordsText="No se encontraron registros" 
         onneeddatasource="GrdAlmacen_NeedDataSource" 
         onpageindexchanged="GrdAlmacen_PageIndexChanged">
                                    <MasterTableView  TableLayout="Auto" AllowMultiColumnSorting="False"
                                        AllowNaturalSort="true" AllowSorting="true">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Alm_Clave" Visible="True" UniqueName="Alm_Clave"
                                                HeaderText="Clave" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="150px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Alm_Nombre" HeaderText="Nombre"
                                                UniqueName="Alm_Nombre" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"
                                                AllowFiltering="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="200px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Alm_CuentaStr" Visible="True" UniqueName="Alm_Cuenta"
                                                HeaderText="Cuenta" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                AllowFiltering="false">
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Alm_SubcuentaStr" Visible="True" UniqueName="Alm_Subcuenta"
                                                HeaderText="Subcuenta" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                AllowFiltering="false" ItemStyle-Width="150px" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Alm" UniqueName="Id_u" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Alm_Activo" Visible="False" UniqueName="U_FNac">
                                               </telerik:GridBoundColumn>
                                        
                                        </Columns>
                                    </MasterTableView>
             
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
 
 </td>
 <td width="100%">
 </td>
 </tr>
 </table>
        </div>
</asp:Content>
