using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.Entity;

namespace CapaNegocios
{
    public class CN_CatCNac_ACYS
    {

        SIANCENTRAL_CCEntities1 model;

        public CN_CatCNac_ACYS(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }


        public List<CatCNac_ACYS> ConsultarACYS(int idMatriz)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.ConsultarACYS(idMatriz);
        }

        public List<spComboNiveles_Result> ComboNiveles(int idMatriz)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.ComboNiveles(idMatriz);
        }

        public List<CatCNac_TipoCuenta> ComboTipoCuenta()
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.ComboTipoCuenta();
        }


        public CatCNac_ACYS ConsultarACYS_Item(int id)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.ConsultarACYS_Item(id);
        }

        public List<CatCNac_ACYS> ConsultarACYS_Item(int id_Matriz, string Nombre)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.ConsultarACYS_Item(id_Matriz, Nombre);
        }


        public Boolean Editar(CatCNac_ACYS acys)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.Editar(acys);

        }

        public Boolean Nuevo(CatCNac_ACYS acys)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.Nuevo(acys);

        }

        public Boolean Deshabilitar(int id)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.Deshabilitar(id);
        }

        public Boolean DuplicarACYS(int id)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.DuplicarACYS(id);
        }


        public Boolean Eliminar(int id)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS(model);
            return CACYS.Eliminar(id);
        }

    }
}
