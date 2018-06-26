using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.Entity;

namespace CapaNegocios
{
    public class CN_CatCNac_Estructura
    {

        SIANCENTRAL_CCEntities1 model;


        public CN_CatCNac_Estructura(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }

        public List<CatCNac_Estructura> ConsultarTodos(int id_Matriz)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.ConsultarTodos(id_Matriz);

        }


        public Int32 Alta(CatCNac_Estructura estr)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.Alta(estr);
        }

        public Int32 Editar(CatCNac_Estructura estr)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.Editar(estr);
        }

        public Int32 CambiarNivel(CatCNac_Estructura estr)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.CambiarNivel(estr);
        }

        public Int32 CambiarSucursal(CatCNac_Estructura estr)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.CambiarSucursal(estr);
        }


        public Boolean Borrar(CatCNac_Estructura estr)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.Borrar(estr);
        }

        public Boolean Borrar(Int32 id)
        {
            CD_CatCNac_Estructura CEst = new CD_CatCNac_Estructura(model);
            return CEst.Borrar(id);
        }

    }
}
