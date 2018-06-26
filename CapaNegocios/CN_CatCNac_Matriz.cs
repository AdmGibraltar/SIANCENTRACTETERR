using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.Entity;

namespace CapaNegocios
{
    public class CN_CatCNac_Matriz
    {

        SIANCENTRAL_CCEntities1 model;


       public CN_CatCNac_Matriz(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }


       public List<CatCNac_Matriz> ConsultarTodos()
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz(model);
            return CMatriz.ConsultarTodos();
        }


       public Boolean Nuevo(CatCNac_Matriz cliente)
        {
            CD_CatCNac_Matriz CNCliente = new CD_CatCNac_Matriz(model);
            return CNCliente.Nuevo(cliente);

        }


       public int ConsultarMax()
       {
           CD_CatCNac_Matriz CNCliente = new CD_CatCNac_Matriz(model);
           return CNCliente.ConsultarMax();
       }

       public CatCNac_Matriz ConsultarItem(int id)
        {
            CD_CatCNac_Matriz CNCliente = new CD_CatCNac_Matriz(model);
            return CNCliente.ConsultarItem(id);
        }

       public List<CatCNac_Matriz> ConsultarItem(string Nombre)
       {
           CD_CatCNac_Matriz CNCliente = new CD_CatCNac_Matriz(model);
           return CNCliente.ConsultarItem(Nombre);
       }

       public Boolean Editar(CatCNac_Matriz cliente)
        {
            CD_CatCNac_Matriz CNCliente = new CD_CatCNac_Matriz(model);
            return CNCliente.Editar(cliente);


        }

       public List<CatCNac_IntranetUsuarios> ComboIntranetUsuarios()
       {
           CD_CatCNac_Matriz CNCliente = new CD_CatCNac_Matriz(model);
           return CNCliente.ComboIntranetUsuarios();
       }



    }
}
