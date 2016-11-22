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
    public partial class conMayorDetallado : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            parametros.tipo_comprobantes = Request.QueryString["tipo_comprobantes"];
            parametros.fecha_desde = Request.QueryString["fecha_desde"];
            parametros.Fecha_hasta = Request.QueryString["fecha_hasta"];
            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.reporte = Request.QueryString["reporte"];

            ReportDocument crystalReport = new ReportDocument();
            var dsMayor = new Datas.dsMayor();
            DataTable dt_Reporte = new DataTable();


            string columnas = "mayor.id_mayor, ccomprobantes.id_ccomprobantes,usuarios.nombre_usuarios, " +
                               "tipo_comprobantes.nombre_tipo_comprobantes, entidades.nombre_entidades, " +
                                "entidades.ruc_entidades, entidades.telefono_entidades, entidades.direccion_entidades, " +
                                "entidades.ciudad_entidades, ccomprobantes.concepto_ccomprobantes, ccomprobantes.numero_ccomprobantes," +
                                "ccomprobantes.ruc_ccomprobantes, ccomprobantes.nombres_ccomprobantes, ccomprobantes.retencion_ccomprobantes, " +
                                "ccomprobantes.valor_ccomprobantes,  ccomprobantes.valor_letras, ccomprobantes.fecha_ccomprobantes, " +
                                "ccomprobantes.referencia_doc_ccomprobantes,  ccomprobantes.numero_cuenta_banco_ccomprobantes, " +
                                "ccomprobantes.numero_cheque_ccomprobantes, ccomprobantes.observaciones_ccomprobantes,  " +
                                "plan_cuentas.id_plan_cuentas, plan_cuentas.codigo_plan_cuentas,  plan_cuentas.nombre_plan_cuentas, " +
                                "plan_cuentas.saldo_fin_plan_cuentas, plan_cuentas.n_plan_cuentas, mayor.fecha_mayor, " +
                                "mayor.debe_mayor,  mayor.haber_mayor,  mayor.saldo_mayor, mayor.saldo_ini_mayor,  mayor.creado, entidades.logo_entidades";

            string tablas = "public.ccomprobantes, public.mayor, public.plan_cuentas,  public.tipo_comprobantes,  public.usuarios,   public.entidades";

            string where = "ccomprobantes.id_usuarios = usuarios.id_usuarios AND " +
                              "mayor.id_ccomprobantes = ccomprobantes.id_ccomprobantes AND " +
                              "plan_cuentas.id_plan_cuentas = mayor.id_plan_cuentas AND " +
                              "tipo_comprobantes.id_tipo_comprobantes = ccomprobantes.id_tipo_comprobantes AND " +
                              "entidades.id_entidades = ccomprobantes.id_entidades ";

            string order = " mayor.creado";

            //para cambiar el where

            String where_to = "";
            //
            if (!String.IsNullOrEmpty(parametros.tipo_comprobantes))
            {

                where_to += " AND tipo_comprobantes.id_tipo_comprobantes='" + parametros.id_entidades+"'";
            }
            
            if (!String.IsNullOrEmpty(parametros.fecha_desde) && !String.IsNullOrEmpty(parametros.Fecha_hasta))
            {

                where_to += " AND  ccomprobantes.fecha_ccomprobantes BETWEEN '"+parametros.fecha_desde+"' AND '"+parametros.Fecha_hasta+"'";
            }

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            where = where + where_to;

            dt_Reporte = AccesoLogica.Select(columnas, tablas, where,order);
            

            dsMayor.Tables.Add(dt_Reporte);

            string cadena = "";

            if (!String.IsNullOrEmpty(parametros.reporte))
            {
                if(parametros.reporte=="simplificado")
                {
                    cadena = Server.MapPath("~/Php/Reporte/crMayorSimplificado.rpt");
                }else if(parametros.reporte=="detallado")
                {
                    cadena = Server.MapPath("~/Php/Reporte/crMayorDetallado.rpt");
                }
            }

            try
            {
                crystalReport.Load(cadena);

                crystalReport.SetDataSource(dsMayor.Tables[1]);

                //paso de parametros

                //en caso de estar vacios las fechas
                if (String.IsNullOrEmpty(parametros.fecha_desde) || String.IsNullOrEmpty(parametros.Fecha_hasta))
                {
                    if (dt_Reporte.Rows.Count > 0)
                    {
                        parametros.fecha_desde = dt_Reporte.Rows[0]["fecha_ccomprobantes"].ToString();
                        parametros.Fecha_hasta = dt_Reporte.Rows[dt_Reporte.Rows.Count - 1]["fecha_ccomprobantes"].ToString();
                    }

                }

                crystalReport.SetParameterValue("fecha_desde", parametros.fecha_desde);
                crystalReport.SetParameterValue("fecha_hasta", parametros.Fecha_hasta);

                CrystalReportViewer1.ReportSource = crystalReport;

            }catch(EngineException)
            {
                cadena = Server.MapPath("~/Php/Reporte/empty.rpt");
                crystalReport.Load(cadena);
                    
                CrystalReportViewer1.ReportSource = crystalReport;

            }


        }
        

    }
}