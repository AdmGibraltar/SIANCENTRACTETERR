<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rep_ClienteAdeudo.aspx.cs" Inherits="SIANWEB.Rep_ClienteAdeudo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
<script type="text/javascript" id="telerikClientEvents1">
     function RcGrafica_SeriesClicked(sender, eventArgs) {
        var tipo = eventArgs.get_category();
        var seleccion = tipo.substr(0, 7);
        //alert("You clicked on a series item with value '" + eventArgs.get_value() + "' from category '" + eventArgs.get_category() + "'.");
        if (seleccion == "Cobrado")
            window.location.href = "Rep_ClienteCuentasPagadas.aspx";
        else
            window.location.href = "Rep_ClientesCuentasPorPagar.aspx";
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <telerik:RadScriptManager Runat="server"></telerik:RadScriptManager>    
         <telerik:RadHtmlChart ID="RcGrafica" runat="server"
         Legend-Appearance-Visible ="true" Legend-Appearance-Position ="Bottom" 
            Width="950px" Height="650px" 
            Skin="BlackMetroTouch"  
            ChartTitle-Appearance-Align = "Center" OnClientSeriesClicked="RcGrafica_SeriesClicked">
            <ChartTitle>
            <Appearance>
            <TextStyle FontSize="16px"></TextStyle>
            </Appearance>
            </ChartTitle>
            <Legend>
            <Appearance Position="Right"></Appearance>
            </Legend>
         
         </telerik:RadHtmlChart>
    </div>
    </form>
</body>
</html>
