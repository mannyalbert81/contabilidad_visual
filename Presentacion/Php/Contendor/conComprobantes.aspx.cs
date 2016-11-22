using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

using System.IO;
using System.Drawing;

namespace Presentacion.Php.Contendor
{



    public partial class conComprobantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsComprobantes = new Datas.dsComprobantes();
            DataTable dt_Reporte1 = new DataTable();
           

            string columnas = " entidades.nombre_entidades, " +
                                "entidades.ruc_entidades, entidades.telefono_entidades, entidades.direccion_entidades, " +
                                "entidades.ciudad_entidades, entidades.logo_entidades, ccomprobantes.id_ccomprobantes, " +                                 
                                "tipo_comprobantes.nombre_tipo_comprobantes, " + 
								  "ccomprobantes.concepto_ccomprobantes, " +
								  "usuarios.nombre_usuarios, " + 
								  "ccomprobantes.valor_letras, "+
								  "ccomprobantes.fecha_ccomprobantes, "+
								  "ccomprobantes.numero_ccomprobantes, " + 
								  "ccomprobantes.ruc_ccomprobantes, " +
                                  "ccomprobantes.nombres_ccomprobantes, " +
                                  "ccomprobantes.retencion_ccomprobantes, " +
                                  "ccomprobantes.valor_ccomprobantes, " +
                                  "ccomprobantes.referencia_doc_ccomprobantes, " +
                                  "ccomprobantes.numero_cuenta_banco_ccomprobantes, " +
                                  "ccomprobantes.numero_cheque_ccomprobantes, " +
                                  "ccomprobantes.observaciones_ccomprobantes, " +
                                  "forma_pago.nombre_forma_pago";



            

            string tablas = "public.ccomprobantes,  public.entidades,  public.usuarios,  public.tipo_comprobantes,  public.forma_pago";

            string where = "ccomprobantes.id_forma_pago = forma_pago.id_forma_pago AND entidades.id_entidades = usuarios.id_entidades AND usuarios.id_usuarios = ccomprobantes.id_usuarios AND tipo_comprobantes.id_tipo_comprobantes = ccomprobantes.id_tipo_comprobantes";

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);

            //dsCuentas.Cuentas= dt_Reporte;

            dsComprobantes.Tables.Add(dt_Reporte1);
            
            
            string cadena = Server.MapPath("~/Php/Reporte/crComprobantes.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsComprobantes.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
            
        }


       

    }
}