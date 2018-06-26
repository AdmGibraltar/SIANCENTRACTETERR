using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace CapaDatos
{

    public class CD_CatCNac_ACYS
    {
         SIANCENTRAL_CCEntities1 model;
        public CD_CatCNac_ACYS(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }


        public List<CatCNac_ACYS> ConsultarACYS(int idMatriz)
        {
            var res = model.CatCNac_ACYS.Where(x => x.Id_Matriz == idMatriz).ToList();
            return res;
        }


        public List<spComboNiveles_Result> ComboNiveles(int idMatriz)
        {
            var res = model.spComboNiveles(idMatriz).ToList();
            return res;
        }

        public List<CatCNac_TipoCuenta> ComboTipoCuenta()
        {
            var res = model.CatCNac_TipoCuenta.ToList();
            return res;
        }

        public CatCNac_ACYS ConsultarACYS_Item(int id)
        {
            var res = model.CatCNac_ACYS.Where(x => x.Id == id).FirstOrDefault();
            return res;
        }

        public List<CatCNac_ACYS> ConsultarACYS_Item(int id_Matriz, string Nombre)
        {
            var res = model.CatCNac_ACYS.Where(x => x.Nombre.Contains(Nombre) && x.Id_Matriz == id_Matriz).ToList();
            return res;
        }


        public Boolean Editar(CatCNac_ACYS acys)
        {
            var original = model.CatCNac_ACYS.Find(acys.Id);

            if (original != null)
            {
                model.Entry(original).CurrentValues.SetValues(acys);
                model.SaveChanges();
            }
            return true;
        }

        public Boolean Nuevo(CatCNac_ACYS acys)
        {
            model.CatCNac_ACYS.Add(acys);
            model.SaveChanges();

            return true;
        }


        public Boolean Deshabilitar(int id)
        {
            var original = model.CatCNac_ACYS.Find(id);

            if (original != null)
            {
                original.Activo = false;
                model.SaveChanges();
            }
            return true;
        }


        public Boolean Eliminar(int id)
        {
            try
            {
                var original = model.CatCNac_ACYS.Find(id);

                if (original != null)
                {
                    model.CatCNac_ACYS.Remove(original);
                    model.SaveChanges();

                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }


        public Boolean DuplicarACYS(int id)
        {
            model.spCatCN_DuplicarACYS(id);
            return true;
        }




    }
}
