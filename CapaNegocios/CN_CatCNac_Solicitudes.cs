using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.Entity;

namespace CapaNegocios
{
    public class CN_CatCNac_Solicitudes
    {

        SIANCENTRAL_CCEntities1 model;


        public CN_CatCNac_Solicitudes(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }

        public List<CatCNac_Solicitudes> ConsultarTodos(string NombreMatriz, int idEstatus)
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes(model);
            return CEst.ConsultarTodos(NombreMatriz,idEstatus);
        }


        public CatCNac_Solicitudes ConsultarItem(int id, int sucursal)
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes(model);
            return CEst.ConsultarItem(id, sucursal);
        }

        public List<CatCNac_Usuario> ComboAsesores(int IdMatriz)
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes(model);
            return CEst.ComboAsesores(IdMatriz);
        }

        public Boolean ActualizaSolicitud(int id, int sucursal, int Estatus, string comentariosAdm)
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes(model);
            return CEst.ActualizaSolicitud(id, sucursal, Estatus, comentariosAdm);
        }

        public List<CatCNac_EstatusSolicitudes> ConsultarEstatus()
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes(model);
            return CEst.ConsultarEstatus();
        }
    }
}
