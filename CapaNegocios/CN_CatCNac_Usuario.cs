using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.Entity;

namespace CapaNegocios
{
    public class CN_CatCNac_Usuario
    {

        SIANCENTRAL_CCEntities1 model;


        public CN_CatCNac_Usuario(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }

        public List<CatCNac_Usuario> ConsultarTodos(int id_Matriz)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.ConsultarTodos(id_Matriz);
        }

        public CatCNac_Usuario ConsultarItem(int id)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.ConsultarItem(id);
        }

        public List<CatCNac_UsuarioPermisos> ConsultarPermisos(int idMatriz)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.ConsultarPermisos(idMatriz);
        }


        public Boolean GuardarPermisos(CatCNac_UsuarioPermisos usuPermisos)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.GuardarPermisos(usuPermisos);
        }

        public Boolean BorrarPermisos(CatCNac_UsuarioPermisos usuPermisos)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.BorrarPermisos(usuPermisos);
        }

        public Int32 Nuevo(CatCNac_Usuario usuario)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.Nuevo(usuario);
        }

        public Boolean Editar(CatCNac_Usuario usuario)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.Editar(usuario);
        }


        public Boolean Eliminar(int id)
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.Eliminar(id);
        }


        public List<CatCNac_RolAuditorias> ComboAuditorias()
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.ComboAuditorias();

        }

        public List<CatCNac_RolECommerce> ComboECommerce()
        {
            CD_CatCNac_Usuario CEst = new CD_CatCNac_Usuario(model);
            return CEst.ComboECommerce();

        }

    }
}
