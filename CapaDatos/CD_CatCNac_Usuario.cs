using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace CapaDatos
{
    public class CD_CatCNac_Usuario
    {
        SIANCENTRAL_CCEntities1 model;
        public CD_CatCNac_Usuario(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }



        public List<CatCNac_Usuario> ConsultarTodos(int id_Matriz)
        {
            var res = model.CatCNac_Usuario.Where(x=>x.IdMatriz==id_Matriz).ToList();
            return res;
        }


        public CatCNac_Usuario ConsultarItem(int id)
        {
            var res = model.CatCNac_Usuario.Where(x => x.Id == id).FirstOrDefault();
            return res;
        }

        public List<CatCNac_UsuarioPermisos> ConsultarPermisos(int idMatriz)
        {
            var res = model.CatCNac_UsuarioPermisos.Where(x => x.Id_Usuario==idMatriz).ToList();
            return res;
        }

        public Boolean GuardarPermisos(CatCNac_UsuarioPermisos usuPermisos)
        {
            var usu = model.CatCNac_UsuarioPermisos.Where(x => x.Id_Estructura == usuPermisos.Id_Estructura && x.Id_Usuario == usuPermisos.Id_Usuario).FirstOrDefault();

            if (usu == null)
            {
                model.CatCNac_UsuarioPermisos.Add(usuPermisos);
                model.SaveChanges();
            }
            return true;
        }

        public Boolean BorrarPermisos(CatCNac_UsuarioPermisos usuPermisos)
        {
            var usu = model.CatCNac_UsuarioPermisos.Where(x => x.Id_Estructura == usuPermisos.Id_Estructura && x.Id_Usuario == usuPermisos.Id_Usuario).FirstOrDefault();

            if(usu != null) model.CatCNac_UsuarioPermisos.Remove(usu);
            model.SaveChanges();
            return true;
        }


        public int Nuevo(CatCNac_Usuario usuario)
        {
            model.CatCNac_Usuario.Add(usuario);
            model.SaveChanges();
            

            return usuario.Id;
        }


        public Boolean Editar(CatCNac_Usuario usuario)
        {
            var original = model.CatCNac_Usuario.Find(usuario.Id);

            if (original != null)
            {
                model.Entry(original).CurrentValues.SetValues(usuario);
                model.SaveChanges();
            }
            return true;
        }



        public Boolean Eliminar(int id)
        {
            var original = model.CatCNac_Usuario.Find(id);
            var permisosUsu = model.CatCNac_UsuarioPermisos.Where(x => x.Id == id).ToList();


            try
            {
                if (original != null)
                {
                    model.CatCNac_UsuarioPermisos.RemoveRange(permisosUsu);
                    model.CatCNac_Usuario.Remove(original);
                    model.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }





        public List<CatCNac_RolAuditorias> ComboAuditorias()
        {
            var res = model.CatCNac_RolAuditorias.ToList();
            return res;
        }

        public List<CatCNac_RolECommerce> ComboECommerce()
        {
            var res = model.CatCNac_RolECommerce.ToList();
            return res;
        }


        //public Int32 Alta(CatCNac_Estructura estr)
        //{
        //    model.CatCNac_Estructura.Add(estr);
        //    model.SaveChanges();
        //    int id = estr.Id;

        //    return id;
        //}

        //public Int32 Editar(CatCNac_Estructura estr)
        //{


        //    var original = model.CatCNac_Estructura.Find(estr.Id);

        //    if (original != null)
        //    {

        //        model.Entry(original).CurrentValues.SetValues(estr);
        //        model.SaveChanges();
        //    }

        //    return estr.Id;
        //}

        //public Boolean Borrar(CatCNac_Estructura estr)
        //{
        //    var original = model.CatCNac_Estructura.Find(estr.Id);

        //    if (original != null)
        //    {

        //        model.CatCNac_Estructura.Remove(original);
        //        model.SaveChanges();
        //    }
        //    return true;
        //}

        //public Boolean Borrar(int idMatriz)
        //{
        //    var original = model.CatCNac_Estructura.Where(x=>x.Id_Matriz==idMatriz);

        //    if (original != null)
        //    {
        //        model.CatCNac_Estructura.RemoveRange(original);
        //        model.SaveChanges();
        //    }
        //    return true;
        //}


    }
}
