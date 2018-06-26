using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace CapaDatos
{
    public class CD_CatCNac_Matriz
    {
        SIANCENTRAL_CCEntities1 model;
        public CD_CatCNac_Matriz(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }


        public List<CatCNac_Matriz> ConsultarTodos()
        {
            var res = model.CatCNac_Matriz.ToList();
            return res;
        }


        public CatCNac_Matriz ConsultarItem(int id)
        {
            var res = model.CatCNac_Matriz.Where(x => x.Id == id).FirstOrDefault();
            return res;
        }

        public List<CatCNac_Matriz> ConsultarItem(string Nombre)
        {
            var res = model.CatCNac_Matriz.Where(x => x.Nombre.Contains(Nombre)).ToList();
            return res;
        }

        public int ConsultarMax()
        {

            if (model.CatCNac_Matriz.Count() > 0)
            {
                var res = model.CatCNac_Matriz.Max(x => x.Id);
                return res;
            }
            else
                return 0;
        }

        public Boolean Nuevo(CatCNac_Matriz cliente)
        {
            model.CatCNac_Matriz.Add(cliente);
            model.SaveChanges();

            return true;
        }


        public Boolean Editar(CatCNac_Matriz cliente)
        {
            var original = model.CatCNac_Matriz.Find(cliente.Id);

            if (original != null)
            {
                if (cliente.Logo.Count() == 0) cliente.Logo = original.Logo;

                model.Entry(original).CurrentValues.SetValues(cliente);
                model.SaveChanges();
            }

            return true;
        }


        public List<CatCNac_IntranetUsuarios> ComboIntranetUsuarios()
        {
            return model.CatCNac_IntranetUsuarios.ToList();
        }






    }
}
