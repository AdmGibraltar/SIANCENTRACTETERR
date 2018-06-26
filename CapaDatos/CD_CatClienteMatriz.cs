using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;


namespace CapaDatos
{
    public class CD_CatClienteMatriz
    {

        SIANCENTRAL_CCEntities1 model;


        public CD_CatClienteMatriz(SIANCENTRAL_CCEntities1 modelo)
        {
            model = modelo;
        }

        public CD_CatClienteMatriz()
        {
            model = new SIANCENTRAL_CCEntities1();
        }


        public List<CatCNac_Matriz> ConsultarTodos()
        {
            var res = model.CatCNac_Matriz.ToList();
            return res;
        }

        public CatCNac_Matriz ConsultarMatriz(int id_matriz)
        {

            var res = model.CatCNac_Matriz.Where(x => x.Id == id_matriz).FirstOrDefault();
            return res;
        }

        public CatCNac_Matriz ConsultarMatrizItem(int id_matriz)
        {

            var res = model.CatCNac_Matriz.Where(x => x.Id == id_matriz).FirstOrDefault();
            return res;
        }


        public Boolean Guardar_Permisos(CatClienteMatriz_Permisos catPermisos)
        {


            if (!model.CatClienteMatriz_Permisos.Any(o => o.Id == catPermisos.Id))
            {
                model.CatClienteMatriz_Permisos.Add(catPermisos);

            }
            else
            {
                var original = model.CatClienteMatriz_Permisos.Find(catPermisos.Id);

                if (original != null)
                {
                    model.Entry(original).CurrentValues.SetValues(catPermisos);
                    model.SaveChanges();
                }
            }

            model.SaveChanges();

            return true;
        }


        public List<CatClienteMatriz_Permisos> ConsultarPermisos(int id)
        {
            return model.CatClienteMatriz_Permisos.Where(x => x.Id == id).ToList();
        }


        public List<spCC_ConsultarAfiliaciones_Result> ConsultarAfiliaciones(int Id_ClienteMatriz)
        {
            return model.spCC_ConsultarAfiliaciones(Id_ClienteMatriz).ToList();
        }


        public Boolean GuardarACYS(CatCNac_ACYS entAcys, Boolean nuevo)
        {

            // Borrar
    
            var listProd = model.CatAcys_Productos.Where(x=>x.Id_ACYS==entAcys.Id).ToList();
            foreach (CatAcys_Productos prod in listProd) if (!entAcys.CatAcys_Productos.Contains(prod)) model.CatAcys_Productos.Remove(prod);

            var listGar = model.CatACYS_Productos_DatosGar.Where(x => x.Id_ACYS == entAcys.Id).ToList();
            foreach (CatACYS_Productos_DatosGar gar in listGar) if (!entAcys.CatACYS_Productos_DatosGar.Contains(gar)) model.CatACYS_Productos_DatosGar.Remove(gar);

            var listGarFec = model.CatACYS_Productos_DatosGar_Fechas.Where(x => x.Id_ACYS == entAcys.Id).ToList();
            foreach (CatACYS_Productos_DatosGar_Fechas garFec in listGarFec) if (!entAcys.CatACYS_Productos_DatosGar_Fechas.Contains(garFec)) model.CatACYS_Productos_DatosGar_Fechas.Remove(garFec);


            //Agregar
            foreach (CatAcys_Productos prod in entAcys.CatAcys_Productos) if (!listProd.Contains(prod)) model.CatAcys_Productos.Add(prod);
            foreach (CatACYS_Productos_DatosGar gar in entAcys.CatACYS_Productos_DatosGar) if (!listGar.Contains(gar)) model.CatACYS_Productos_DatosGar.Add(gar);

            foreach (CatACYS_Productos_DatosGar_Fechas garFec in entAcys.CatACYS_Productos_DatosGar_Fechas) if (!listGarFec.Contains(garFec)) model.CatACYS_Productos_DatosGar_Fechas.Add(garFec);


            if (!nuevo)
            {
                var acys = model.CatCNac_ACYS.Where(x => x.Id == entAcys.Id).FirstOrDefault();

                acys.Fecha = entAcys.Fecha;
                acys.FechaInicio = entAcys.FechaInicio;
                acys.FechaFin = entAcys.FechaFin;

              //  model.Entry(entAcys).State = EntityState.Modified;

                model.Entry(acys).State = EntityState.Modified;

                model.Entry(acys.CatACYS_Cliente).CurrentValues.SetValues(entAcys.CatACYS_Cliente);
                model.Entry(acys.CatACYS_RecPedido).CurrentValues.SetValues(entAcys.CatACYS_RecPedido);
                model.Entry(acys.CatACYS_CondPago).CurrentValues.SetValues(entAcys.CatACYS_CondPago);
                model.Entry(acys.CatACYS_ServValor).CurrentValues.SetValues(entAcys.CatACYS_ServValor);
                model.Entry(acys.CatACYS_OtrosApoyos).CurrentValues.SetValues(entAcys.CatACYS_OtrosApoyos);

            }
            else
            {
               

                var acys = model.CatCNac_ACYS.Where(x => x.Id == entAcys.Id).FirstOrDefault();

                acys.Fecha = entAcys.Fecha;
                acys.FechaInicio = entAcys.FechaInicio;
                acys.FechaFin = entAcys.FechaFin;

               // model.CatCNac_ACYS.Add(acys);
                model.CatACYS_Cliente.Add(entAcys.CatACYS_Cliente);
                model.CatACYS_RecPedido.Add(entAcys.CatACYS_RecPedido);
                model.CatACYS_CondPago.Add(entAcys.CatACYS_CondPago);
                model.CatACYS_ServValor.Add(entAcys.CatACYS_ServValor);
                model.CatACYS_OtrosApoyos.Add(entAcys.CatACYS_OtrosApoyos);
             }



            model.SaveChanges();

            return true;

        }



        public Boolean GuardarDatosGeneralesMat(CatCNac_Matriz entAcys, Boolean Nuevo)
        {

            var listOrDir = model.CatACYS_DirFiscales.ToList();
            foreach (CatACYS_DirFiscales dir in listOrDir) if (!entAcys.CatACYS_DirFiscales.Contains(dir)) model.CatACYS_DirFiscales.Remove(dir);

            var listOrFran = model.CatCNac_IntranetListaFranq.ToList();
            foreach (CatCNac_IntranetListaFranq fran in listOrFran) if (!entAcys.CatCNac_IntranetListaFranq.Contains(fran))
                {
                    fran.CatCNac_IntranetUsuarios = null;
                    fran.CatCNac_ProductosPermitidos = null;
                    fran.CatTMoneda = null;


                    model.CatCNac_IntranetListaFranq.Remove(fran);
                }


            if (entAcys != null)
            {

                if (!Nuevo)
                {

                    model.Entry(entAcys.CatACYS_SIANCENTRAL).State = EntityState.Modified;
                    model.Entry(entAcys.CatCNac_IntranetFran).State = EntityState.Modified;
                    


                }
                else
                {
                    model.CatACYS_SIANCENTRAL.Add(entAcys.CatACYS_SIANCENTRAL);
                    model.CatCNac_IntranetFran.Add(entAcys.CatCNac_IntranetFran);
                   

                }

                foreach (CatACYS_DirFiscales dir in entAcys.CatACYS_DirFiscales) if (dir.Id == 0) model.CatACYS_DirFiscales.Add(dir);

                foreach (CatCNac_IntranetListaFranq fran in entAcys.CatCNac_IntranetListaFranq)
                    if (fran.Id == 0)
                    {
                        fran.CatCNac_IntranetUsuarios = null;
                        fran.CatCNac_ProductosPermitidos = null;
                        fran.CatTMoneda = null;

                        model.CatCNac_IntranetListaFranq.Add(fran); 
                    }

                try
                {
                    model.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
            return true;

        }


        public List<CatACYS_DirFiscales> ConsutarDirFiscales()
        {
            return model.CatACYS_DirFiscales.ToList();
        }

        public List<CatACYS_DirFiscales> ConsutarDirFiscales(int idMatriz)
        {
            return model.CatACYS_DirFiscales.Where(x=>x.Id_ClienteMatriz==idMatriz).ToList();
        }



        public List<CatAcys_Productos> ConsultarProductos(int id_TG, int id_Acys)
        {
            return model.CatAcys_Productos.Where(x => x.Id_TG == id_TG && x.Id_ACYS==id_Acys).ToList();
        }


        public CatProducto ConsultaProductoInfo(int id_Prd)
        {
            return model.CatProducto.Where(x => x.Id_Prd == id_Prd).FirstOrDefault();
        }

        public List<CatTMoneda> ComboMoneda()
        {
            return model.CatTMoneda.ToList();
        }

        public List<CatCNac_ProductosPermitidos> ComboProdPermitidos()
        {
            return model.CatCNac_ProductosPermitidos.ToList();
        }

        public List<CatCNac_CatalogoEspecial> ComboCatEspecial()
        {
            return model.CatCNac_CatalogoEspecial.ToList();
        }

        public List<CatCNac_Addenda> ComboAddenda()
        {
            return model.CatCNac_Addenda.ToList();
        }

        public List<CatCNac_TipoNotaCred> ComboTipoNotaCred()
        {
            return model.CatCNac_TipoNotaCred.ToList();
        }

        public List<CatCNac_MetPago> ComboMetPago()
        {
            return model.CatCNac_MetPago.ToList();
        }

        public List<CatCNac_Soportes> ComboSoportes()
        {
            return model.CatCNac_Soportes.ToList();
        }

        public List<CatCNac_IntranetListaFranq> ListaFranquicias(int idMatriz)
        {
            return model.CatCNac_IntranetListaFranq.Where(x => x.Id_Matriz == idMatriz).ToList();
        }


        public List<CatCNac_PermisosCamposACYS> ConsultaPermisosCampos()
        {
            return model.CatCNac_PermisosCamposACYS.ToList();
        }


        public List<CatCNac_Condiciones> ConsultaCondiciones()
        {
            return model.CatCNac_Condiciones.ToList();
        }


        public List<CatCDI> ConsultaTipos()
        {
            return model.CatCDI.ToList();
        }


        public List<spCNacDireccionesFiscalesACYS_Result> ComboDireccionesFiscales(int id_ACYS)
        {
            return model.spCNacDireccionesFiscalesACYS(id_ACYS).ToList();

        }


        public List<CatUsuario> ComboUsuario()
        {
            return model.CatUsuario.ToList();
        }



    }
}
