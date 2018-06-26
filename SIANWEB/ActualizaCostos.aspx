<%@ Page Title="Actualizar costos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ActualizaCostos.aspx.cs" Inherits="SIANWEB.ActualizaCostos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
   <script type="text/javascript">


   </script>
           <style type="text/css"> 
       .button {
           border-top: 1px solid #96d1f8;
           background: #65a9d7;
           background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
           background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
           background: -moz-linear-gradient(top, #3e779d, #65a9d7);
           background: -ms-linear-gradient(top, #3e779d, #65a9d7);
           background: -o-linear-gradient(top, #3e779d, #65a9d7);
           padding: 10.5px 21px;
           cursor:pointer;
           -webkit-border-radius: 10px;
           -moz-border-radius: 10px;
           border-radius: 10px;
           -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
           -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
           box-shadow: rgba(0,0,0,1) 0 1px 0;
           text-shadow: rgba(0,0,0,.4) 0 1px 0;
           color: #f0f0f0;
           font-size: 14px;
           font-family: 'Lucida Grande', Helvetica, Arial, Sans-Serif;
           text-decoration: none;
           vertical-align: middle;
           }
        .button:hover {
           border-top-color: #305f7d;
           background: #305f7d;
           cursor:pointer;
           color: #ccc;
           }
        .button:active {
           border-top-color: #68abba;
           background: #68abba;
             cursor:pointer;
           }
        </style> 
     
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
            <telerik:AjaxSetting AjaxControlID="BtnActualizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
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
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" >
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
         
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
                                    <asp:Button ID="BtnActualizar" runat="server" Text="Actualizar costos" 
                                        CssClass="button" onclick="BtnActualizar_Click"  />
                                </td>
                                <td width="100">
                                    &nbsp;</td>
                                <td width="10">
                                
                                </td>
                                <td width="80">
                                    &nbsp;</td>
                                <td width="100">
                                    &nbsp;</td>
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
                                    &nbsp;</td>
                                <td >
                                   
                                    &nbsp;</td>
                                   <td>
                                   </td>
                                   <td colspan = "9">
                                       &nbsp;</td>

                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>  
                                <td>
                                </td>
                                <td colspan ="9">
                                    &nbsp;</td>
                                              
                            </tr>
                       
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                 <td>
                                </td>
                                 <td>
                                     &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
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
