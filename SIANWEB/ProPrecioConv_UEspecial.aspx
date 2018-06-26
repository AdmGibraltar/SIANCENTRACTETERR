<%@ Page Title="Clientes desbloqueo" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProPrecioConv_UEspecial.aspx.cs" Inherits="SIANWEB.ProPrecioConv_UEspecial" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">


   </script>
     
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server"  OnAjaxRequest="RAM1_AjaxRequest"
         EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1" >
                <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                 <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTipoMovimiento" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMovimientos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtTotalFac" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtTotalEst" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
                   <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtTotVariacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
   
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            
                <telerik:RadToolBarButton CommandName="Guardar" Value="Guardar" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png"  />
        </Items>
    </telerik:RadToolBar>
    <div>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
            
                </td>
                <td width="150px" style="font-weight: bold">
                <asp:HiddenField id= "HF_ClvPag" runat ="server"/>
             
                </td>
            </tr>
            <tr>
            <td>
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            </td>
            </tr>
        </table>
        <div id="divPrincipal" runat="server">
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;" cellspacing="5" >
       
                            <tr>
                                <td class="style1" >
                                </td>
                                <td >
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                            
                            </tr>
                            <tr>
                                <td class="style1"  >
                                   
                          
                                   <asp:Label ID="Lbl1" runat="server" Text="Administrador 1" Font-Bold ="True"></asp:Label>
                                   
                          
                                   </td>
                                <td >
                                   
                          
                                     <telerik:RadComboBox ID="CmbId_UAdmin1" MaxHeight="400px" runat="server" 
                        Width="250px" >
                    </telerik:RadComboBox></td>
                                   <td>
                                   </td>
                                   <td>
                               
                                   </td>

                            </tr>
                           <tr>
                                <td class="style1" >
                                </td>
                                <td >
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                            
                            </tr>
                         
                                 
                                 <tr>
                                <td class="style1"  >

                                   <asp:Label ID="Label1" runat="server" Text="Administrador 2" Font-Bold ="True"></asp:Label>

                                     </td>
                                <td >

                                    <telerik:RadComboBox ID="CmbId_UAdmin2" MaxHeight="400px" runat="server" 
                        Width="250px" >
                    </telerik:RadComboBox></td>  
                                <td >
                                </td>
                                <td >
                               
                                     </td>
                                              
                            </tr>
                         <tr>
                                <td class="style1" >
                                </td>
                                <td >
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                            
                            </tr>
                       
                            <tr>
                                <td class="style1" >
                                    <asp:Label ID="LblId3" runat="server" Text="Agregar clientes intranet" Font-Bold="True"></asp:Label>
                                </td>
                                <td >
                                    <telerik:RadComboBox ID="CmbId_UCteIntranet" MaxHeight="400px" runat="server" 
                        Width="250px" >
                    </telerik:RadComboBox></td>
                                       <td >

                                    &nbsp;</td>
                                  
                                    <td >
                               
                                     </td>
                                
                            </tr>
                             <tr>
                                <td class="style1" >
                                </td>
                                <td >
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                            
                            </tr>
                                   <tr>
                                <td class="style1"  >
                                    <asp:Label ID="Label2" runat="server" Text="Agregar clientes Macola" Font-Bold="True"></asp:Label>
                                       </td>
                                <td >
                                     <telerik:RadComboBox ID="CmbId_UCteMacola" MaxHeight="400px" runat="server" 
                        Width="250px" >
                    </telerik:RadComboBox></td>
                                       <td >

                                    &nbsp;</td>
                                  
                                    <td >
                               
                                     </td>
                                
                            </tr>
                             <tr>
                                <td class="style1" >
                                </td>
                                <td >
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                            
                            </tr>
                              <tr>
                                <td class="style1"  >
                                    <asp:Label ID="Label3" runat="server" Text="Admón. equipos en comodato" Font-Bold="True"></asp:Label>
                                  </td>
                                <td >
                                    <telerik:RadComboBox ID="CmbId_UComodato" MaxHeight="400px" runat="server" 
                        Width="250px" >
                    </telerik:RadComboBox></td>
                                       <td >

                                    &nbsp;</td>
                                  
                                    <td >
                               
                                     </td>
                                
                            </tr>
                             <tr>
                                <td class="style1" >
                                </td>
                                <td >
                                </td>
                                <td >
                                </td>
                                <td>
                                </td>
                            
                            </tr>
                                 <tr>
                                <td class="style1"  >
                                    <asp:Label ID="Label4" runat="server" Text="Ver todos los convenios" Font-Bold="True"></asp:Label>
                                     </td>
                                <td >
                                     <telerik:RadComboBox ID="CmbId_UTodo" MaxHeight="400px" runat="server" 
                        Width="250px" >
                    </telerik:RadComboBox></td>
                                       <td >

                                    &nbsp;</td>
                                  
                                    <td >
                               
                                     </td>
                                
                            </tr>
                               <tr>
                                <td class="style1"  >
                                    &nbsp;</td>
                                <td >
                                  
                                   </td>
                                  
                                   <td>
                                       &nbsp;</td>
    <asp:HiddenField ID="HF_Cve" runat="server" />
                            </tr>
            </table>
            <br />
          
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">

    <style type="text/css">
        .style1
        {
            width: 250px;
        }
    </style>

    </asp:Content>

