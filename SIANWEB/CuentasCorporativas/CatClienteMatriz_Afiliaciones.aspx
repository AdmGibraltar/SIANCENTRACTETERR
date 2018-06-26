<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatClienteMatriz_Afiliaciones.aspx.cs" 
Inherits="SIANWEB.CuentasCorporativas.CatClienteMatriz_Afiliaciones" MasterPageFile="~/MasterPage/MasterPage02.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
        <div>
                <h2>Pantalla de consultar afiliaciones</h2>
        </div>

        <table>
            <tr>
                <td>Num Matriz: </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>Cliente Matriz</td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox2" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>Condición</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="47px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Razón Social: </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox4" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>RFC:</td>
                <td  colspan="3">
                    <asp:TextBox ID="TextBox5" runat="server" Width="299px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan=4>Dirección de Facturación. </td>
            </tr>
            <tr>
                <td>Calle:</td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox6" runat="server" Width="440px"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Colonia"></asp:Label>
                </td>
                <td colspan=3>
                    <asp:TextBox ID="TextBox7" runat="server" Width="438px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Ciudad"></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Estado"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>
                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Municipio/Delegación"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Num:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label6" runat="server" Text="Zona Postal"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Width="80px"></asp:TextBox></td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Pais"></asp:Label></td>
                <td>
                    &nbsp;</td>
              </tr>
              <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Email:"></asp:Label></td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox10" runat="server" Width="300px"></asp:TextBox></td>
                      
              
              </tr>

              <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Convenio"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="DropDownList5" runat="server">
                    </asp:DropDownList>
                </td>
              </tr>
        </table>

        <br />
        <br />

        <asp:GridView ID="dgAfiliaciones" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Estatus" SortExpression="Estatus" HeaderStyle-Width="70px">
                     <ItemTemplate><%#Boolean.Parse(Eval("Estatus").ToString()) ? "Activo" : "Inactivo"%></ItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField HeaderText="Fecha de Alta" DataField="Fecha_Alta" HeaderStyle-Width="100px" DataFormatString="{0:d}"/>
                <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" HeaderStyle-Width="150px" />
                <asp:BoundField HeaderText="#Cliente SIAN" DataField="Id_Cte" HeaderStyle-Width="50px" />
                <asp:BoundField HeaderText="CDI" DataField="Db_CdNombre" HeaderStyle-Width="50px" />
                <asp:BoundField HeaderText="Terr" DataField="Id_Ter" HeaderStyle-Width="75px" />
                <asp:BoundField HeaderText="Usuario" DataField="Usuario" HeaderStyle-Width="125px" />
                <asp:BoundField HeaderText="Convenio" DataField="Convenio" HeaderStyle-Width="75px" />
                <asp:BoundField HeaderText="dom. fiscal de la factura" DataField="DirFiscal" HeaderStyle-Width="300px" />
                <asp:TemplateField HeaderText="Eliminar" HeaderStyle-Width="50px">
                <ItemTemplate>
                       <img src="../Img/x.gif" id="imgEliminar" style="cursor:pointer" onclick="" />
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
         </asp:GridView>

</asp:Content>