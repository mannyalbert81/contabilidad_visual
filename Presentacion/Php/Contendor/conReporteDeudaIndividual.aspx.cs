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
    public partial class conReporteDeudaIndividual : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            parametros.id_amortizacion_cabeza = Request.QueryString["id_amortizacion_cabeza"];

            ReportDocument crystalReport = new ReportDocument();
            var dsReporteDeuda = new Datas.dsReporteDeuda();
            DataTable dt_Reporte1 = new DataTable();

            string columnas = "amortizacion_cabeza.id_amortizacion_cabeza,"+ 
                                    "amortizacion_cabeza.numero_credito_amortizacion_cabeza,"+ 
									  "amortizacion_cabeza.numero_pagare_amortizacion_cabeza,"+ 
										"fc_clientes.id_clientes,"+
									  "fc_clientes.ruc_clientes,"+ 
									  "fc_clientes.razon_social_clientes,"+ 
									  "fc_clientes.direccion_clientes,"+ 
									  "fc_clientes.telefono_clientes,"+ 
									  "fc_clientes.celular_clientes,"+ 
									  "fc_clientes.email_clientes,"+
                                      "fc_clientes.numero_operacion," +
                                      "tipo_creditos.nombre_tipo_creditos," + 
									  "amortizacion_cabeza.capital_prestado_amortizacion_cabeza,"+ 
									  "entidades.ruc_entidades,"+ 
									  "entidades.nombre_entidades,"+ 
									  "entidades.telefono_entidades,"+ 
									  "entidades.direccion_entidades,"+ 
									  "entidades.ciudad_entidades,"+ 
									  "entidades.logo_entidades,"+ 
									  "amortizacion_cabeza.total_deuda";

            string tablas = "public.fc_clientes, public.amortizacion_cabeza, public.tipo_creditos, public.entidades";

            string where = "amortizacion_cabeza.id_fc_clientes = fc_clientes.id_clientes AND tipo_creditos.id_tipo_creditos = amortizacion_cabeza.id_tipo_creditos AND entidades.id_entidades = amortizacion_cabeza.id_entidades";

            String where_to = "";

            if (!String.IsNullOrEmpty(parametros.id_recaudacion))
            {

                where_to += " AND amortizacion_cabeza.id_amortizacion_cabeza = " + parametros.id_amortizacion_cabeza;
            }

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsReporteDeuda.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crReporteDeuda.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsReporteDeuda.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}