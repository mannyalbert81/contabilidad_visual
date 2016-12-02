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
using Presentacion.Php.Clases;

namespace Presentacion.Php.Contendor
{



    public partial class conReporteComprobantes : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
           
            parametros.tipo_comprobantes = Request.QueryString["id_tipo_comprobantes"];
            parametros.fecha_desde = Request.QueryString["fecha_desde"];
            parametros.Fecha_hasta = Request.QueryString["fecha_hasta"];
            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.numero_comprobantes = Request.QueryString["numero_ccomprobantes"];
            parametros.referencia_doc_comprobantes = Request.QueryString["referencia_doc_ccomprobantes"];

            try {
                parametros.id_usuarios = Convert.ToInt32(Request.QueryString["id_usuarios"]);
            }
            catch (Exception) { parametros.id_usuarios = 0; }

            ReportDocument crystalReport = new ReportDocument();
            var dsComprobantes = new Datas.dsReporteComprobantes();
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

            //para cambiar el where

            String where_to = "";
            //
            if (parametros.id_usuarios>0)
            {

                where_to += " AND usuarios.id_usuarios=" + parametros.id_usuarios + "";
            }

            if (!String.IsNullOrEmpty(parametros.tipo_comprobantes) && Convert.ToInt32(parametros.tipo_comprobantes)!=0)
            {

                where_to += " AND tipo_comprobantes.id_tipo_comprobantes='" + parametros.tipo_comprobantes + "'";
            }

            if (!String.IsNullOrEmpty(parametros.fecha_desde) && !String.IsNullOrEmpty(parametros.Fecha_hasta))
            {

                where_to += " AND  ccomprobantes.fecha_ccomprobantes BETWEEN '" + parametros.fecha_desde + "' AND '" + parametros.Fecha_hasta + "'";
            }

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            if (!String.IsNullOrEmpty(parametros.numero_comprobantes))
            {

                where_to += " AND ccomprobantes.numero_ccomprobantes='"+parametros.numero_comprobantes+"' ";
            }

            if (!String.IsNullOrEmpty(parametros.referencia_doc_comprobantes))
            {

                where_to += " AND ccomprobantes.referencia_doc_ccomprobantes ='"+parametros.referencia_doc_comprobantes+"'";
            }
                       
            where = where + where_to;
            
            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);

           
            dsComprobantes.Tables.Add(dt_Reporte1);
            
            
            string cadena = Server.MapPath("~/Php/Reporte/crReporteComprobantes.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsComprobantes.Tables[1]);

            //paso de parametros

            //en caso de estar vacios las fechas
            if (String.IsNullOrEmpty(parametros.fecha_desde) || String.IsNullOrEmpty(parametros.Fecha_hasta))
            {
                if (dt_Reporte1.Rows.Count > 0)
                {
                    parametros.fecha_desde = dt_Reporte1.Rows[0]["fecha_ccomprobantes"].ToString();
                    parametros.Fecha_hasta = dt_Reporte1.Rows[dt_Reporte1.Rows.Count - 1]["fecha_ccomprobantes"].ToString();
                }
                else
                {
                    parametros.fecha_desde = DateTime.Today.ToString("dd-MM-yyyy");
                    parametros.Fecha_hasta = DateTime.Today.ToString("dd-MM-yyyy");
                }

            }
            parametros.total_registros = 0;
            if(dt_Reporte1.Rows.Count>0) { parametros.total_registros = dt_Reporte1.Rows.Count; }

            crystalReport.SetParameterValue("total_registros", parametros.total_registros);
            crystalReport.SetParameterValue("fecha_desde", parametros.fecha_desde);
            crystalReport.SetParameterValue("fecha_hasta", parametros.Fecha_hasta);

            CrystalReportViewer1.ReportSource = crystalReport;
            
        }
        
    }
}