<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rep_ClienteCuentasPagadas.aspx.cs" 
Inherits="SIANWEB.Rep_ClienteCuentasPagadas" MasterPageFile="~/MasterPage/MasterPage01.Master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
      <script type="text/javascript" id="telerikClientEvents1">
             function RcGrafica_SeriesClicked(sender, eventArgs) {
              var series = eventArgs.get_category();
              $find("<%= RAM1.ClientID %>").ajaxRequest(series);
          }

          //--------------------------------------------------------------------------------------------------
          //Funciones para cerrar la ventana radWindow actual
          //--------------------------------------------------------------------------------------------------
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
              //debugger;
              GetRadWindow().Close();
          }

          //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
          function CloseAndRebind() {
              //debugger;
              GetRadWindow().Close();
              //GetRadWindow().BrowserWindow.refreshGrid();
          }

          function TabSelected(sender, args) {

          }

          //Hace un refresh completo de la ventana padre = F5
          function RefreshParentPage() {
              GetRadWindow().BrowserWindow.location.reload();
          }

          function AbrirReportePadre() {
              GetRadWindow().BrowserWindow.AbrirReporte();
          }

          function refreshGrid() {

          }
      </script>
   </telerik:RadCodeBlock>
    
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest ="RAM1_AjaxRequest">
    </telerik:RadAjaxManager>
        <telerik:RadHtmlChart ID="RcGrafica" runat="server"
        Legend-Appearance-Visible ="true" Legend-Appearance-Position ="Bottom" 
            Width="1024px" Height="650px" 
            Skin="BlackMetroTouch"  
            ChartTitle-Appearance-Align = "Center" OnClientSeriesClicked="RcGrafica_SeriesClicked" Transitions = "True" >
            <ChartTitle>
            <Appearance>
            <TextStyle FontSize="16px"></TextStyle>
            </Appearance>
            </ChartTitle>
            <Legend>
            <Appearance Position="Right"></Appearance>
            </Legend>
        </telerik:RadHtmlChart> 
        
        <asp:HiddenField ID="HF_Cve" runat="server" />
      
</asp:Content>

