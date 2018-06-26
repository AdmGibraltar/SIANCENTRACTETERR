﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SIANWEB.CatConfiguraciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
        Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton ToolTip="Correo" CommandName="mail" CssClass="mail" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Imprimir" CommandName="print" CssClass="print"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Eliminar" CommandName="delete" CssClass="delete"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Regresar" CommandName="undo" CssClass="undo" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                ValidationGroup="guardar" />
            <telerik:RadToolBarButton ToolTip="Nuevo" CommandName="new" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <asp:Panel ID="Panel1" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    Centro de distribucion
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultipage1"
                                    TabIndex="-1" ValidationGroup="guardar" SelectedIndex="0">
                                    <Tabs>
                                        <telerik:RadTab AccessKey="Z" PageViewID="RadPageView1" Selected="True" Text="&lt;u&gt;Z&lt;/u&gt;ona horaria">
                                        </telerik:RadTab>
                                        <telerik:RadTab AccessKey="C" PageViewID="RadPageView2" Text="&lt;u&gt;C&lt;/u&gt;orreo">
                                        </telerik:RadTab>
                                        <telerik:RadTab AccessKey="A" PageViewID="RadPageView3" Text="&lt;u&gt;A&lt;/u&gt;cceso">
                                        </telerik:RadTab>
                                        <telerik:RadTab AccessKey="N" PageViewID="RadPageView4" Text="Co&lt;u&gt;n&lt;/u&gt;traseñas">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" AccessKey="R" PageViewID="RadPageView5" 
                                            Text="Co&lt;u&gt;r&lt;/u&gt;reos de autorización">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                                    BorderWidth="1px">
                                    <telerik:RadPageView ID="RadPageView1" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    Zona horaria
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="CmbHoraZona" runat="server" Style="cursor: hand" 
                                                        TabIndex="1" Width="320px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkHoraVerano" runat="server" Style="cursor: hand" 
                                                        TabIndex="2" Text="Activar horario de verano" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView2" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    Servidor de correo
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailServidor" runat="server" MaxLength="35" 
                                                        onpaste="return false" TabIndex="1" Width="210px">
                                                        <clientevents onkeypress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Puerto
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtMailPuerto" runat="server" MaxLength="6" 
                                                        MinValue="0" TabIndex="2" Width="35px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Usuario
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailUsuario" runat="server" MaxLength="35" 
                                                        onpaste="return false" TabIndex="3" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Contraseña
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailContraseña" runat="server" MaxLength="35" 
                                                        onpaste="return false" TabIndex="4" Width="210px">
                                                        <clientevents onkeypress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Remitente
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailRemitente" runat="server" MaxLength="35" 
                                                        onpaste="return false" TabIndex="5" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView3" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    Intentos antes de bloquear
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtLoginIntentos" runat="server" MaxLength="2" 
                                                        MinValue="0" TabIndex="1" Width="15px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Tiempo de bloqueo (hrs)
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtLoginTiempoBloqueo" runat="server" 
                                                        MaxLength="2" MinValue="0" TabIndex="2" Width="15px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView4" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    Tiempo de vida(días)
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtContTVida" runat="server" MaxLength="2" 
                                                        MinValue="0" TabIndex="1" Width="15px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Longitud mínima
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtContLong" runat="server" MaxLength="2" 
                                                        MinValue="0" TabIndex="2" Width="15px">
                                                        <numberformat decimaldigits="0" groupseparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView5" runat="server">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Compras locales"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailCompLocal" runat="server" MaxLength="256" 
                                                        onpaste="return false" TabIndex="3" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                        <clientevents onkeypress="EmailMultiple" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Precios especiales"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailPrecioEsp" runat="server" MaxLength="256" 
                                                        onpaste="return false" TabIndex="3" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                        <clientevents onkeypress="EmailMultiple" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Ajuste de base instalada"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailBi" runat="server" MaxLength="256" 
                                                        onpaste="return false" TabIndex="3" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                        <clientevents onkeypress="EmailMultiple" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Acuerdos comerciales"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailAcys" runat="server" MaxLength="256" 
                                                        onpaste="return false" TabIndex="4" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                        <clientevents onkeypress="EmailMultiple" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Valuación de proyectos"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="TxtMailValuacion" runat="server" MaxLength="256" 
                                                        onpaste="return false" TabIndex="5" Width="210px">
                                                        <clientevents onkeypress="Email" />
                                                        <clientevents onkeypress="EmailMultiple" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" 
                                                        Text="Minimo para solicitar valuación de proyecto autorizada"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="TxtMinValuacion" Runat="server" MaxLength="9" 
                                                        MinValue="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="200">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadInputManager ID="RadInputManager1" runat="server">
        <telerik:TextBoxSetting BehaviorID="Texto" Validation-ValidationGroup="guardar" ErrorMessage="Requerido"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailContraseña" />
            </TargetControls>
        </telerik:TextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="Numeric6" Validation-ValidationGroup="guardar"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit" MinValue="1"
            MaxValue="999999" DecimalDigits="0" GroupSizes="6">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailPuerto" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="Numeric2" Validation-ValidationGroup="guardar"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit" MinValue="1"
            MaxValue="99" DecimalDigits="0">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtLoginIntentos" />
                <telerik:TargetInput ControlID="TxtLoginTiempoBloqueo" />
                <telerik:TargetInput ControlID="TxtContTVida" />
                <telerik:TargetInput ControlID="TxtContLong" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
        <telerik:NumericTextBoxSetting BehaviorID="Numeric3" Validation-ValidationGroup="guardar"
            Validation-IsRequired="True" Validation-ValidateOnEvent="Submit" MinValue="1"
            MaxValue="999" DecimalDigits="0">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtOtrosInfo" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
        <telerik:RegExpTextBoxSetting BehaviorID="Correos" Validation-ValidationGroup="guardar"
            ErrorMessage="Correo incorrecto" Validation-IsRequired="True" Validation-ValidateOnEvent="Submit"
            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailUsuario" />
                <telerik:TargetInput ControlID="TxtMailRemitente" />
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
        <telerik:RegExpTextBoxSetting BehaviorID="SMTP" Validation-ValidationGroup="guardar"
            ErrorMessage="SMTP incorrecto" Validation-IsRequired="True" Validation-ValidateOnEvent="Submit"
            ValidationExpression="^(\w+\.)+(\w+)$">
            <TargetControls>
                <telerik:TargetInput ControlID="TxtMailServidor" />
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    </telerik:RadInputManager>
</asp:Content>