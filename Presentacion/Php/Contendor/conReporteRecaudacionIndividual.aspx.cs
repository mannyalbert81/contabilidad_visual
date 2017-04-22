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
    public partial class conReporteRecaudacionIndividual : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            parametros.id_recaudacion = Request.QueryString["id_recaudacion"];

            ReportDocument crystalReport = new ReportDocument();
            var dsReporteRecaudacion = new Datas.dsReporteRecaudacion();
            DataTable dt_Reporte1 = new DataTable();

            string columnas = "recaudacion.id_recaudacion," +
                                  "fc_clientes.ruc_clientes," +
                                  "fc_clientes.razon_social_clientes," +
                                  "fc_clientes.numero_operacion," +
                                  "entidades.ruc_entidades," +
                                  "entidades.nombre_entidades," +
                                  "entidades.telefono_entidades," +
                                  "entidades.direccion_entidades," +
                                  "entidades.ciudad_entidades," +
                                  "entidades.logo_entidades," +
                                  "amortizacion_cabeza.numero_credito_amortizacion_cabeza," +
                                  "amortizacion_cabeza.numero_pagare_amortizacion_cabeza," +
                                  "recaudacion.capital_pagado_recaudacion," +
                                  "recaudacion.numero_cuota_recaudacion," +
                                  "recaudacion.fecha_pago_recaudacion," +
                                  "amortizacion_detalle.numero_cuota_amortizacion_detalle," +
                                  "amortizacion_detalle.saldo_inicial_amortizacion_detalle," +
                                  "amortizacion_detalle.interes_amortizacion_detalle," +
                                  "amortizacion_detalle.amortizacion_amortizacion_detalle," +
                                  "amortizacion_detalle.pagos_amortizacion_detalle," +
                                  "amortizacion_detalle.fecha_pagos_amortizacion_detalle," +
                                  "amortizacion_detalle.interes_dias_amortizacion_detalle," +
                                  "recaudacion.nombre_entidad_financiera_recaudacion," +
                                  "recaudacion.numero_papeleta_recaudacion," +
                                  "recaudacion.concepto_pago_amortizacion";

            string tablas = "public.recaudacion, public.amortizacion_detalle, public.fc_clientes, public.entidades, public.amortizacion_cabeza";

            string where = "amortizacion_detalle.id_amortizacion_detalle = recaudacion.id_amortizacion_detalle AND fc_clientes.id_clientes = recaudacion.id_clientes AND entidades.id_entidades = recaudacion.id_entidades AND amortizacion_cabeza.id_amortizacion_cabeza = recaudacion.id_amortizacion_cabeza";

            String where_to = "";
            
            if (!String.IsNullOrEmpty(parametros.id_recaudacion))
            {

                where_to += " AND recaudacion.id_recaudacion = " + parametros.id_recaudacion;
            }

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsReporteRecaudacion.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crReporteRecaudacion.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsReporteRecaudacion.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}