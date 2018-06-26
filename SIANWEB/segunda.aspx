<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="segunda.aspx.cs" Inherits="SIANWEB.segunda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadHtmlChart ID="RadHtmlChart" runat="server"
        Legend-Appearance-Visible ="true" Legend-Appearance-Position ="Bottom" 
            Width="950px" Height="650px" 
            Skin="BlackMetroTouch" 
            ChartTitle-Appearance-Align = "Center">
<ChartTitle>
<Appearance>
<TextStyle FontSize="16px"></TextStyle>
</Appearance>
</ChartTitle>


        </telerik:RadHtmlChart>
    </div>
    </form>
</body>
</html>
