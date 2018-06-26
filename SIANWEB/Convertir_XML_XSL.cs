using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using CapaEntidad;
using System.Xml.Linq;


namespace SIANWEB
{
    public class Convertir_XML_XSL
    {
        private String filename;
        private String stylesheet;
        private string impEdoConsolidado;
        private string destinohtml;
        private string clientesXML;
        private int tipoReporte;


        public Convertir_XML_XSL(String filename, String stylesheet, String impEdoConsolidadoin, string pclientesXML, string destino, int tiporeporte )
        {
            this.filename = filename;
            this.stylesheet = stylesheet;
            this.impEdoConsolidado = impEdoConsolidadoin;
            this.destinohtml = destino;
            this.clientesXML = pclientesXML;
            this.tipoReporte = tiporeporte;
        }

     

      
        public void convertir_html()
        {

            try
            {

                
                // si es 1 es  venta incremental y no entraría a esta parte 
                if (tipoReporte == 0)
                {
                    ArmarXML(filename, clientesXML);
                }


                //Cargamos la hoja de estilo que utilizaremos
                XslTransform xslt = new XslTransform();
                
                xslt.Load(stylesheet);

                //Carga el archivo que deseamos transformar.
                
                XPathDocument doc = new XPathDocument(filename);

                //Instanciamos XmlTextWriter con una consola de salida.
                XmlTextWriter writer = new XmlTextWriter(Console.Out);

                xslt.Transform(doc, null, writer, null);
                writer.Close();
                //Declarar y crear un nuevo objeto XslCompiledTransform
                XslCompiledTransform transform = new XslCompiledTransform();
                //Cargamos el Xls que utilizaremos
               
                transform.Load(stylesheet);
                //Generamos nuestro Html
                //transform.Transform(filename, "c:\\Resultado.html");




                // tratar de leer el archivo xls y editarlo para grabar en el 
                XslTransform xslTransform = new XslTransform();
               
                xslTransform.Load(stylesheet);
               StreamReader sr = new StreamReader(stylesheet);
                string xslBlock = sr.ReadToEnd();
                sr.Close();

              

                string resultado = "";
               

                

                resultado = impEdoConsolidado;
               
                xslBlock = xslBlock.Replace("{0}", resultado);   //"Employee = \'yes\'");



                // need to convert the xsl string to a memory stream
                // in order to put it in the xml reader.
                MemoryStream ms = new MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(xslBlock));
                XmlReader reader = new XmlTextReader(ms);

                // load the tranform with the XmlReader containing
                // the xsl transform string
                xslTransform.Load(reader);

 

                XPathDocument doc2 = new XPathDocument(filename);

                //Instanciamos XmlTextWriter con una consola de salida.
                XmlTextWriter writer2 = new XmlTextWriter(Console.Out);

                xslTransform.Transform(doc2, null, writer2, null);
 
                xslTransform.Transform(filename, destinohtml);


                writer2.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void ArmarXML(String filename, string pclientesXML)
        {

    
            string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            xmlString = xmlString + "<Data> ";
            xmlString = xmlString + pclientesXML;
            xmlString = xmlString + @"</Data>";



            XElement cmlElem = XElement.Parse(xmlString);

            //cmlElem.Save(@"C:\Proyectos\SIANWEBCENTRALv1.0\_Fuentes\SIANWEB\Reportes\InflatedPriceBooks.xml");
            cmlElem.Save(filename);
  
        }

      
    }

}
