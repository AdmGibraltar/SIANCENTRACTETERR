using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace CapaDatos
{
    public class CD_CatCNac_Estructura
    {
        SIANCENTRAL_CCEntities1 model;
        public CD_CatCNac_Estructura(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }



        public List<CatCNac_Estructura> ConsultarTodos(int id_Matriz)
        {
            var res = model.CatCNac_Estructura.Where(x=>x.Id_Matriz==id_Matriz).ToList();
            return res;
        }




        public Int32 Alta(CatCNac_Estructura estr)
        {
            model.CatCNac_Estructura.Add(estr);
            model.SaveChanges();
            int id = estr.Id;

            return id;
        }

        public Int32 Editar(CatCNac_Estructura estr)
        {


            var original = model.CatCNac_Estructura.Find(estr.Id);

            if (original != null)
            {

                model.Entry(original).CurrentValues.SetValues(estr);
                model.SaveChanges();
            }

            return estr.Id;
        }

        public Boolean Borrar(CatCNac_Estructura estr)
        {
            var original = model.CatCNac_Estructura.Find(estr.Id);

            if (original != null)
            {

                model.CatCNac_Estructura.Remove(original);
                model.SaveChanges();
            }
            return true;
        }

        public Boolean Borrar(int idMatriz)
        {
            var original = model.CatCNac_Estructura.Where(x=>x.Id_Matriz==idMatriz);

            if (original != null)
            {
                model.CatCNac_Estructura.RemoveRange(original);
                model.SaveChanges();
            }
            return true;
        }



        public Int32 CambiarNivel(CatCNac_Estructura estr)
        {

            var original = model.CatCNac_Estructura.Find(estr.Id);

            if (original != null)
            {
                original.Nivel_ACYS = estr.Nivel_ACYS;
                original.id_Acys = estr.id_Acys;
                model.SaveChanges();
            }
            return estr.Id;
           
        }

        public Int32 CambiarSucursal(CatCNac_Estructura estr)
        {

            var original = model.CatCNac_Estructura.Find(estr.Id);

            if (original != null)
            {
                original.Sucursal = estr.Sucursal;
                original.NombreSucursal = estr.NombreSucursal;
                model.SaveChanges();
            }
            return estr.Id;

        }



    }
}
