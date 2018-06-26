<%@ Page Title="Objetivos de rotación" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProObjetivoRotacion.aspx.cs" Inherits="SIANWEB.ProObjetivoRotacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">

   </script>
     
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" 
         EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbId_Alm" LoadingPanelID="RadAjaxLoadingPanel1"
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
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbId_CC">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgMovimientos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMmovimientos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl"  OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
               <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar"
                    ImageUrl="~/Imagenes/blank.png" />
         
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
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td width="110">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td width="80">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                </td>
                                <td width="10">
                                </td>
                                <td width="45">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <asp:Label ID="LblId_CC" runat="server" Text="Centro de distribución"></asp:Label></td>
                                <td >
                                    <telerik:RadComboBox ID="CmbId_Cd" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px"  OnSelectedIndexChanged="cmbId_Cd_SelectedIndexChanged"
                                                        Width="300px">
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
                                   <td>
                                   </td>
                                   <td colspan = "9">
                                       &nbsp;</td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblObDias" runat="server" Text="Objetivo días"></asp:Label></td>
                                <td>
                                   <telerik:RadNumericTextbox ID="TxtObDias" runat="server" MaxLength="9" MinValue="0"
                                                       Width="100px" style="text-align:right" EnabledStyle-HorizontalAlign="Right">
                                <NumberFormat AllowRounding="false" DecimalDigits="0"  GroupSeparator="," GroupSizes="3"   />
                                </telerik:RadNumericTextbox ></td>  
                                <td>
                                </td>
                                <td colspan ="9">
                                    &nbsp;</td>
                                              
                            </tr>
                       
                              <tr>
                                <td>
                                    <asp:Label ID="LblObPesos" runat="server" Text="Objetivo pesos"></asp:Label></td>
                                <td>
                                   <telerik:RadNumericTextbox ID="TxtObPesos" runat="server" MaxLength="9" MinValue="0"
                                                       Width="100px" style="text-align:right" EnabledStyle-HorizontalAlign="Right">
                                 <NumberFormat AllowRounding="false" DecimalDigits="2" DecimalSeparator="." GroupSeparator="," GroupSizes="3"   />
                                </telerik:RadNumericTextbox ></td>  
                                <td>
                                </td>
                                <td colspan ="9">
                                    &nbsp;</td>
                                              
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

             
               
            </table>
            <table width="900px" cellspacing="3"   style="font-family: Verdana; font-size: 10pt;">
              <tr>
               <td>
             
               </td>
               <td align ="right" width= "80%">
                   &nbsp;</td>
               <td align ="right">
                   &nbsp;</td>
               <td>
               </td>
               </tr>
                  <tr>
               <td>
             
               </td>
               <td align ="right" width= "80%">
                   &nbsp;</td>
               <td align ="right">
                   &nbsp;</td>
               <td>
               </td>
               </tr>
                 <tr>
               <td>
             
               </td>
               <td align ="right" width= "80%">
                   &nbsp;</td>
               <td align ="right">
                   &nbsp;</td>
               <td>
               </td>
               </tr>
            </table>
        </div>
    </div>
</asp:Content>
