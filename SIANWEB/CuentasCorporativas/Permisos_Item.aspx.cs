using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using Telerik.Web.UI;
using CapaNegocios;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class Permisos_Item : System.Web.UI.Page
    {

        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();
        int IdUsuario=0;

        private List<CatCNac_Estructura> listEstructura
        {
            get
            {
                return (List<CatCNac_Estructura>)Session["listEstructura"];
            }
            set
            {
                Session["listEstructura"] = value;
            }
        }

        private List<CatCNac_UsuarioPermisos> listPermisos
        {
            get
            {
                return (List<CatCNac_UsuarioPermisos>)Session["listPermisos"];
            }
            set
            {
                Session["listPermisos"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {

                //permisos.ValidarPermisos(this.rtb1);

                List<CatCNac_Estructura> hijosPrimerNodo = null;
                RadTreeNode nodoOrigen = null;
                int id = Int32.Parse(Request.QueryString["Id"]);
                int id_ClienteMat = Int32.Parse(Request.QueryString["IdMatriz"]);
                string NombreCliente = Request.QueryString["Nombre"];

                CN_CatCNac_Estructura cm_Estr = new CN_CatCNac_Estructura(model);

                listEstructura = cm_Estr.ConsultarTodos(id_ClienteMat);



                CN_CatCNac_Usuario cm_Usuario = new CN_CatCNac_Usuario(model);
                Session["cm_Usuario"] = cm_Usuario;

                cmbRol_Auditorias.DataSource = cm_Usuario.ComboAuditorias();
                cmbRol_Auditorias.DataBind();

                cmbRol_Ecommerce.DataSource = cm_Usuario.ComboECommerce();
                cmbRol_Ecommerce.DataBind();

                if (id > 0)
                {
                    var usuario = cm_Usuario.ConsultarItem(id);
                    object objusuario = usuario;
                    AsignacionCampos.AsignaCamposForma(ref objusuario, "", this);

                    //Permisos
                     listPermisos = cm_Usuario.ConsultarPermisos(id);
                }

                nodoOrigen = new RadTreeNode(NombreCliente, "0");
                nodoOrigen.Font.Bold = true;
                nodoOrigen.Font.Size = 11;

                hijosPrimerNodo = listEstructura.Where(x => x.Nivel == 1).ToList();
                ConstruirEstructura(ref nodoOrigen, hijosPrimerNodo);
                treeEstructura.Nodes.Add(nodoOrigen);
                treeEstructura.ExpandAllNodes();

            }

        }

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                CN_CatCNac_Usuario cm_Usuario = (CN_CatCNac_Usuario)Session["cm_Usuario"];

                int id = Int32.Parse(Request.QueryString["Id"]);
                int id_ClienteMat = Int32.Parse(Request.QueryString["IdMatriz"]);
                
                if (btn.CommandName == "save")
                {

                    CatCNac_Usuario usu = new CatCNac_Usuario();
                    usu.Id = id;
                    usu.IdMatriz = id_ClienteMat;

                    //LLena campos a partir del formulario

                    object objMatriz_usuario = usu;
                    AsignacionCampos.AsignaCamposEntidad(ref objMatriz_usuario, "", this);

                    if (id > 0)
                    {
                        cm_Usuario.Editar(usu);
                        IdUsuario = id;
                    }
                    else
                        IdUsuario = cm_Usuario.Nuevo(usu);

                  

                    RadTreeNode nodo1 = treeEstructura.Nodes[0];
                    GuardarPermisos(ref nodo1);
                    RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");
                }
            

        }



        protected void ConstruirEstructura(ref RadTreeNode nodoArbol, List<CatCNac_Estructura> nodosHijos)
        {
            int id = Int32.Parse(Request.QueryString["Id"]);

            foreach (CatCNac_Estructura nodo in nodosHijos)
            {

                if (nodosHijos.Count > 0)
                {
                    var nodoAgregar = new RadTreeNode(nodo.NombreNodo);
                    nodoAgregar.Value = nodo.Id.ToString();

                    if(id>0)
                    if (listPermisos.Exists(c => c.Id_Usuario == id && c.Id_Estructura == nodo.Id))
                        nodoAgregar.Checked = true;


                    nodoArbol.Nodes.Add(nodoAgregar);
                   
                    var hijosNodoActual = listEstructura.Where(x => x.NodoPadre == nodo.Id).ToList();
                    ConstruirEstructura(ref nodoAgregar, hijosNodoActual);
                }

            }
        }


        protected void GuardarPermisos(ref RadTreeNode nodoArbol)
        {
            CN_CatCNac_Usuario cm_Usuario = (CN_CatCNac_Usuario)Session["cm_Usuario"];

             int id = IdUsuario;
            foreach (RadTreeNode nodo in nodoArbol.Nodes)
            {

                CatCNac_UsuarioPermisos usuPer = new CatCNac_UsuarioPermisos();

                usuPer.Id_Usuario = id;
                usuPer.Id_Estructura = Int32.Parse(nodo.Value);

                if (nodo.Checked)
                {
                    cm_Usuario.GuardarPermisos(usuPer);
                }
                else
                {
                    usuPer.Id = Int32.Parse(nodo.Value);
                    cm_Usuario.BorrarPermisos(usuPer);
                }

                var nodo1 = nodo;
                GuardarPermisos(ref nodo1);
            }

        }

    }
}