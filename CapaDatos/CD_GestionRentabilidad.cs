﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_GestionRentabilidad
    {
        #region Variables

        string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                      };
        private string PermisoImprimir;
        


        #endregion

        public void ConsultaGestionRentabilidad_Buscar(GestionRentabilidad gestionRentabilidad, string Conexion, ref List<GestionRentabilidad> List	
            ,int Id_Emp
            ,int Id_Cd
            ,string Id_Cte
            ,string Id_Ter
            ,int Id_Rik
             , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , int UBPorCliente
            , int Categorias
            , int UBPorQuimicos
            , int UBPorPapelTradicional
            , int UBPorSistemaDiferenciado
            , int UBPorJarcieria
            , int UBPorAccesorios
            , int UBPorBolsaBasura            
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_TerStr", "@Id_CteStr", "@Cte_Nombre", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Salida", "@Id_U", "@PorUbMinima", "@Categorias", "@UBPorQuimicos", "@UBPorPapelTradicional", "@UBPorSistemaDiferenciado", "@UBPorJarcieria", "@UBPorAccesorios", "@UBPorBolsaBasura" };


                object[] Valores = { Id_Emp, Id_Cd, Id_Ter, Id_Cte, NombreCliente, MesInicial, AnioInicial, MesFinal, AnioFinal, 2, Id_U, UBPorCliente, Categorias, UBPorQuimicos, UBPorPapelTradicional, UBPorSistemaDiferenciado, UBPorJarcieria, UBPorAccesorios, UBPorBolsaBasura };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacion", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    GestionRentabilidad DgestionRentabilidad = new GestionRentabilidad();
                    DgestionRentabilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    DgestionRentabilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    DgestionRentabilidad.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    DgestionRentabilidad.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    DgestionRentabilidad.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    DgestionRentabilidad.venta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("venta")));
                    DgestionRentabilidad.Costo = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Costo")));
                    DgestionRentabilidad.UtilidadBruta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
                    DgestionRentabilidad.InversionSP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("InversionSP")));
                    DgestionRentabilidad.InversionCT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("InversionCT")));
                    DgestionRentabilidad.PorcUBReal = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBReal")));
                    DgestionRentabilidad.PorcURem = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcURem")));
                    if (Categorias==1) {
                        DgestionRentabilidad.VentaQuimicos = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaQuimicos")));
                        DgestionRentabilidad.UBQuimicos = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorUBQuimicos")));
                        DgestionRentabilidad.VentaPT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaPapelTradicional")));
                        DgestionRentabilidad.UBPT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorUBPapelTradicional")));
                        DgestionRentabilidad.VentaSD = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaSistemaDiferenciado")));
                        DgestionRentabilidad.UBSD = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorUBSistemaDiferenciado")));
                        DgestionRentabilidad.VentaJarceria = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaJarcieria")));
                        DgestionRentabilidad.UBJarceria = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorUBJarcieria")));
                        DgestionRentabilidad.VentaAccesorios = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaAccesorios")));
                        DgestionRentabilidad.UBAccesorios = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorAccesorio")));
                        DgestionRentabilidad.VentaBB = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaBolsaBasura")));
                        DgestionRentabilidad.UBBB = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorBolsaBasura")));

                    }
                    DgestionRentabilidad.CrearProyecto = "<a href='CapGestionRentabilidadSimulador.aspx?Id_Emp=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Emp"))) 
                                                                                                    + "&Id_Cd=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cd"))) 
                                                                                                    + "&Id_Ter=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter"))) 
                                                                                                    + "&Id_Cte=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cte"))) 
                                                                                                    + "&TxtNombreCliente=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) 
                                                                                                    + "&txtMesInicial=" + Convert.ToString(MesInicial) 
                                                                                                    + "&txtMesFinal=" + Convert.ToString(MesFinal)
                                                                                                    + "&TxtAnioInicial=" + Convert.ToString(AnioInicial)
                                                                                                    + "&TxtAnioFinal=" + Convert.ToString(AnioFinal) 
                                                                                                    + "&txtDondeViene=0"
                                                                                                    + "&StxtTerritorio=" + Convert.ToString(Id_Ter)
                                                                                                    + "&StxtRepresentante=" + Convert.ToString(Id_Rik)
                                                                                                    + "&STxtNumeroCliente=" + Convert.ToString(Id_Cte)
                                                                                                    + "&STxtPorCliente=" + Convert.ToString(UBPorCliente)
                                                                                                    + "&STxtPorQuimicos=" + Convert.ToString(UBPorQuimicos)
                                                                                                    + "&STxtPorPapelTradicional=" + Convert.ToString(UBPorPapelTradicional)
                                                                                                    + "&STxtPorSistemaDiferenciado=" + Convert.ToString(UBPorSistemaDiferenciado)
                                                                                                    + "&StxtPorJarcieria=" + Convert.ToString(UBPorJarcieria)
                                                                                                    + "&StxtPorAccesorios=" + Convert.ToString(UBPorAccesorios)
                                                                                                    + "&StxtPorBolsaBasura=" + Convert.ToString(UBPorBolsaBasura)
                                                                                                    + "&StxtCategorias=" + Convert.ToString(Categorias) 


                                                        + "'><img src='Imagenes/SerValor20.png' border='0'></a>";
                    List.Add(DgestionRentabilidad);
                    

                }

                dr.Close();
		        

                
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaGestionRentabilidadMonitoreo_Buscar(GestionRentabilidad gestionRentabilidad, string Conexion, ref List<GestionRentabilidad> List
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , int Id_Rik
             , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_TerStr", "@Id_CteStr", "@Cte_Nombre", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Salida", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd, Id_Ter, Id_Cte, NombreCliente, MesInicial, AnioInicial, MesFinal, AnioFinal, 2, Id_U};

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacionMonitoreo", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    GestionRentabilidad DgestionRentabilidad = new GestionRentabilidad();
                    DgestionRentabilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    DgestionRentabilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    DgestionRentabilidad.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    DgestionRentabilidad.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    DgestionRentabilidad.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    DgestionRentabilidad.venta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("venta")));
                    DgestionRentabilidad.Costo = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Costo")));
                    DgestionRentabilidad.UtilidadBruta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
                    DgestionRentabilidad.InversionSP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("InversionSP")));
                    DgestionRentabilidad.InversionCT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("InversionCT")));
                    DgestionRentabilidad.PorcURem = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcURem")));

                    DgestionRentabilidad.CostoP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoP")));
                    DgestionRentabilidad.UtilidadBrutaP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")));
                    DgestionRentabilidad.InversionSPP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("InversionSPP")));
                    DgestionRentabilidad.InversionCTP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("InversionCTP")));
                    DgestionRentabilidad.PorcURemP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcURemP")));


                    DgestionRentabilidad.CrearProyecto = "<a href='CapGestionRentabilidadSimulador.aspx?Id_Emp=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Emp")))
                                                        + "&Id_Cd=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cd")))
                                                        + "&Id_Ter=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter")))
                                                        + "&Id_Cte=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cte")))
                                                        + "&TxtNombreCliente=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")))
                                                        + "&txtMesInicial=" + Convert.ToString(MesInicial)
                                                        + "&txtMesFinal=" + Convert.ToString(MesFinal)
                                                        + "&TxtAnioInicial=" + Convert.ToString(AnioInicial)
                                                        + "&TxtAnioFinal=" + Convert.ToString(AnioFinal)
                                                        + "'><img src='Imagenes/SerValor20.png' border='0'></a>";
                    List.Add(DgestionRentabilidad);


                }

                dr.Close();



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MonitoreoUB_Central(int TipoCD, int MesIni, int AnioIni, int MesFin, int AnioFin, ref List<SegGestionRentabilidad> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {"@TipoCD",
                                       "@MesInicial",
                                       "@AnioInicial",
                                       "@MesFinal",
                                       "@AnioFinal"
                                    };
                object[] Valores = {TipoCD, 
                                   MesIni, 
                                   AnioIni,
                                   MesFin,
                                   AnioFin};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spMonitoreoUB_Central_Sp", ref dr, Parametros, Valores);

                SegGestionRentabilidad g;
                while(dr.Read())
                {
                    g = new SegGestionRentabilidad();
                    g.Region = dr["Region"].ToString().Trim();
                    g.CDI = dr["CDI"].ToString().Trim();
                    g.Representante = dr["Representante"].ToString().Trim();
                    g.Cliente = dr["Cliente"].ToString().Trim();
                    g.MetaUBImporte = Convert.ToDouble(dr["MetaUBImporte"]);
                    g.MetaUBPorc = Convert.ToDouble(dr["MetaUBPorc"]);
                    g.ProyUBImporte = Convert.ToDouble(dr["ProyUBImporte"]);
                    g.ProyUBPorc = Convert.ToDouble(dr["ProyUBPorc"]);
                    g.RealUBImporte = Convert.ToDouble(dr["RealUBImporte"]);
                    g.RealUBPorc = Convert.ToDouble(dr["RealUBPorc"]);
                    List.Add(g);

                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }



    }
}
