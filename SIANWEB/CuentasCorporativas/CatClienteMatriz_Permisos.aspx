<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatClienteMatriz_Permisos.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.CatClienteMatriz_Permisos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
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
    </script>


    <style type="text/css">
        .html, body, form
        {
            margin: 0px;
            padding: 0px;
            overflow: hidden;
            height: auto;
        }
        .menuPanes
        {
            overflow: visible !important;
        }
        .formulario
        {
            font-family: Arial;
            font-size: 12px;
        }
        .dvstyle
        {
            position: fixed;
            top: 0;
            bottom: 0;
            width: 100%;
            height: 100%;
        }
        .hideMe
        {
            display: none !important;
        }
        
        div.RadGrid .rgRefresh, div.RadGrid .rgRefresh + a
        {
            display: none;
        }
    </style>
      <link href="../Styles/Toolbar.css" rel="stylesheet" type="text/css" />
      <link href="../Styles/Menu.Sian.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: verdana; font-size: 8pt">
    
            <br />
    <h2>Permisos</h2>
    <br />
    <p># Matriz: <asp:Label ID="lblId" runat="server"></asp:Label>  </p>
    <p>Cliente Matriz: <asp:Label ID="lblNombre" runat="server"></asp:Label>   </p>


    <table>
        <tr style="background-color:#b3c6ff">
            <td>Dias Crédito</td>
            <td>Acuerdo Económico Productos</td>
            <td>Datos Fiscales</td>
            <td>Asignado a RIK</td>
            <td>MOV 80</td>
        </tr>
         <tr>
            <td><asp:CheckBox id="chkDiasCredito" runat="server"/></td>
            <td><asp:CheckBox id="chkAcuEconomico" runat="server"/></td>
            <td><asp:CheckBox id="chkDatosFiscales" runat="server"/></td>
            <td><asp:CheckBox id="chkAsignadoRIK" runat="server"/></td>
            <td><asp:CheckBox id="chkMOV80" runat="server"/></td>
        </tr>
    </table>

<br />
<asp:Button ID="btnGuardar" runat="server" Text="Guardar" onclick="btnGuardar_Click" />


    </div>
    </form>
</body>
</html>
