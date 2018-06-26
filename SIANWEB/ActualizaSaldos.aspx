<%@ Page Title="Generar saldos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ActualizaSaldos.aspx.cs" Inherits="SIANWEB.ActualizaSaldos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind(param) {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.FisicoTerminado(param);
            }

        </script>
        <style type="text/css">
            .ruBrowse
            {
                background-position: 0 -23px !important;
                width: 80px !important;
            }
        
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="buttonSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="formulario">
         <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl"  >
            <Items>
                 <telerik:RadToolBarButton Width="20px" Enabled="False" />
            </Items>
        </telerik:RadToolBar>
          <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
            
                </td>
                <td width="150px" style="font-weight: bold">
                <asp:HiddenField id= "HF_Cve" runat ="server"/>
             
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

        <div runat="server" id="divPrincipal" style="margin-left: 10px; margin-right: 10px;
            margin-top: 10px;">
           
         <br />
            <table>
             <tr>
                            <td>
                                <asp:Label ID="Label3" Text="Año" runat="server"  >
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbanio" runat="server" Width="150px"   >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                               <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Mes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbmes" runat="server" Width="150px"   >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                                <tr>
                            <td>
                            
                            </td>
                            <td>
                                  <asp:Button ID="BtnGenerar" runat="server" Text="Generar saldos" 
                                        CssClass="button" onclick="BtnGenerar_Click" />

                            </td>
                            <td>
                            </td>
                        </tr>
            </table>

            

        </div>
    </div>
</asp:Content>
