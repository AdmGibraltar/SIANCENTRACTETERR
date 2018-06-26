using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_CatClienteMatriz
    {
       SIANCENTRAL_CCEntities1 model;


       public CN_CatClienteMatriz(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }

       public CN_CatClienteMatriz()
       {
       }

       public List<CatCNac_Matriz> ConsultarTodos()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarTodos();
        }

        public Boolean Guardar_Permisos(CatClienteMatriz_Permisos catPermisos)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.Guardar_Permisos(catPermisos);
        }

        public List<CatClienteMatriz_Permisos> ConsultarPermisos(int id)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarPermisos(id);
        }

        public List<spCC_ConsultarAfiliaciones_Result> ConsultarAfiliaciones(int Id_ClienteMatriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarAfiliaciones(Id_ClienteMatriz);
        }

        public Boolean GuardarACYS(CatCNac_ACYS entAcys, Boolean nuevo)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);
            return CMatriz.GuardarACYS(entAcys, nuevo);
        }


        public Boolean GuardarDatosGeneralesMat(CatCNac_Matriz entAcys, Boolean Nuevo)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);
            return CMatriz.GuardarDatosGeneralesMat(entAcys, Nuevo);
        }


        public CatCNac_Matriz ConsultarMatriz(int id_matriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarMatriz(id_matriz);
        }


        public List<CatACYS_DirFiscales> ConsutarDirFiscales()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsutarDirFiscales();
        }

        public List<CatACYS_DirFiscales> ConsutarDirFiscales(int idMatriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsutarDirFiscales(idMatriz);
        }


        public List<CatAcys_Productos> ConsultarProductos(int id_TG, int id_Acys)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsultarProductos(id_TG, id_Acys);
        }


        public CatProducto ConsultaProductoInfo(int id_Prd)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsultaProductoInfo(id_Prd);
        }


        public List<CatTMoneda> ComboMoneda()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboMoneda();
        }


        public List<CatCNac_ProductosPermitidos> ComboProdPermitidos()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboProdPermitidos();
        }

        public List<CatCNac_CatalogoEspecial> ComboCatEspecial()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboCatEspecial();
        }

        public List<CatCNac_Addenda> ComboAddenda()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboAddenda();
        }

        public List<CatCNac_TipoNotaCred> ComboTipoNotaCred()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboTipoNotaCred();
        }

        public List<CatCNac_MetPago> ComboMetPago()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboMetPago();
        }

        public List<CatCNac_Soportes> ComboSoportes()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ComboSoportes();
        }


        public List<CatCNac_IntranetListaFranq> ListaFranquicias(int idMatriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ListaFranquicias(idMatriz);
        }

        public List<CatCNac_PermisosCamposACYS> ConsultaPermisosCampos()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsultaPermisosCampos();
        }

        public List<CatCNac_Condiciones> ConsultaCondiciones()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsultaCondiciones();
        }

        public List<CatCDI> ConsultaTipos()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);

            return CMatriz.ConsultaTipos();
        }


        public List<spCNacDireccionesFiscalesACYS_Result> ComboDireccionesFiscales(int id_ACYS)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);
            return CMatriz.ComboDireccionesFiscales(id_ACYS);
        }


        public List<CatUsuario> ComboUsuario()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);
            return CMatriz.ComboUsuario();
        }


        public CatCNac_Matriz ConsultarMatrizItem(int id_matriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz(model);
            return CMatriz.ConsultarMatrizItem(id_matriz);
        }


    }
}
