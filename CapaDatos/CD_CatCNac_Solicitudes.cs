using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace CapaDatos
{
    public class CD_CatCNac_Solicitudes
    {


        SIANCENTRAL_CCEntities1 model;
        public CD_CatCNac_Solicitudes(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }

        public List<CatCNac_Solicitudes> ConsultarTodos(string NombreMatriz, int idEstatus)
        {
            var res = model.CatCNac_Solicitudes.Where(x => x.Estatus == idEstatus || idEstatus == 0).
                       Where(x=>x.CatCNac_Matriz.Nombre.Contains(NombreMatriz)).OrderByDescending(x=>x.Fecha).ToList();
            return res;
        }

        public CatCNac_Solicitudes ConsultarItem(int id,int sucursal)
        {
            var res = model.CatCNac_Solicitudes.Where(x => x.Id == id && x.Id_Cd == sucursal).FirstOrDefault();
            return res;
        }

        public List<CatCNac_Usuario> ComboAsesores(int IdMatriz)
        {
            var res = model.CatCNac_Usuario.Where(x => x.IdMatriz == IdMatriz).ToList();
            return res;
        }


        public Boolean ActualizaSolicitud(int id,int sucursal, int Estatus, string comentariosAdm)
        {
            var sol = model.CatCNac_Solicitudes.Where(x => x.Id == id && x.Sucursal == sucursal).FirstOrDefault();
            var est = model.CatCNac_Estructura.Where(x => x.Id == sol.Id_Estructura).FirstOrDefault();

            if (sol.Estatus == 5 && Estatus == 2)
                sol.Estatus = 6;
            else
                sol.Estatus = Estatus;

            sol.ComentariosAdministrador = comentariosAdm;
            sol.Accion = 2;


            if (sol.Estatus == 2)
            {
                est.id_Cte = sol.ClienteSIAN;
                est.id_Cd = sol.Id_Cd;
                est.id_Emp = sol.Id_Emp;
                est.Id_Ter = sol.Territorio;

                est.NombreCliente = sol.RazonSocial;
            }

            if (sol.Estatus == 6)
            {
                est.id_Cte = null;
                est.id_Cd = null;
                est.id_Emp = null;
                est.Id_Ter = null;
                est.NombreCliente = null;
            }

            model.Entry<CatCNac_Solicitudes>(sol).State = EntityState.Modified;
            model.Entry<CatCNac_Estructura>(est).State = EntityState.Modified;
            model.SaveChanges();

            //var res = model.CatCNac_Usuario.Where(x => x.IdMatriz == IdMatriz).ToList();
            return true;
        }


        public List<CatCNac_EstatusSolicitudes> ConsultarEstatus()
        {
            return model.CatCNac_EstatusSolicitudes.ToList();
        }
    }
}
