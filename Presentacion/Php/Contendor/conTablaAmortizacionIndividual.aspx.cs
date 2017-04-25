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
    public partial class conTablaAmortizacionIndividual : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.id_amortizacion_cabeza = Request.QueryString["id_amortizacion_cabeza"];
           
            ReportDocument crystalReport = new ReportDocument();
            var dsTablaAmortizacionIndividual = new Datas.dsTablaAmortizacionIndividual();
            DataTable dt_Reporte1 = new DataTable();

            string columnas = "amortizacion_cabeza.id_amortizacion_cabeza," +
                              "amortizacion_cabeza.numero_credito_amortizacion_cabeza," +
                              "amortizacion_cabeza.numero_pagare_amortizacion_cabeza," +
                              "fc_clientes.ruc_clientes," +
                              "fc_clientes.razon_social_clientes," +
                              "fc_clientes.numero_operacion," +
                              "tipo_creditos.nombre_tipo_creditos," +
                              "amortizacion_cabeza.capital_prestado_amortizacion_cabeza," +
                              "amortizacion_cabeza.tasa_interes_amortizacion_cabeza," +
                              "amortizacion_cabeza.plazo_meses_amortizacion_cabeza," +
                              "amortizacion_cabeza.plazo_dias_amortizacion_cabeza," +
                              "intereses.valor_intereses," +
                              "amortizacion_cabeza.cantidad_cuotas_amortizacion_cabeza," +
                              "amortizacion_cabeza.interes_normal_mensual_amortizacion_cabeza," +
                              "amortizacion_cabeza.interes_mora_mensual_amortizacion_cabeza," +
                              "amortizacion_cabeza.fecha_amortizacion_cabeza," +
                              "amortizacion_cabeza.total_deuda," +
                              "amortizacion_detalle.numero_cuota_amortizacion_detalle," +
                              "amortizacion_detalle.saldo_inicial_amortizacion_detalle," +
                              "amortizacion_detalle.interes_amortizacion_detalle," +
                              "amortizacion_detalle.interes_dias_amortizacion_detalle," +
                              "amortizacion_detalle.amortizacion_amortizacion_detalle," +
                              "amortizacion_detalle.pagos_amortizacion_detalle," +
                              "amortizacion_detalle.fecha_pagos_amortizacion_detalle," +
                              "entidades.ruc_entidades," +
                              "entidades.nombre_entidades," +
                              "entidades.telefono_entidades," +
                              "entidades.direccion_entidades," +
                              "entidades.ciudad_entidades," +
                              "amortizacion_detalle.estado_cancelado_amortizacion_detalle," +
                              "entidades.logo_entidades";

            string tablas = " public.amortizacion_cabeza, public.fc_clientes, public.tipo_creditos, public.intereses, public.amortizacion_detalle, public.entidades";

            string where = "   fc_clientes.id_clientes = amortizacion_cabeza.id_fc_clientes AND tipo_creditos.id_tipo_creditos = amortizacion_cabeza.id_tipo_creditos AND intereses.id_intereses = amortizacion_cabeza.id_intereses AND amortizacion_detalle.id_amortizacion_cabeza = amortizacion_cabeza.id_amortizacion_cabeza AND entidades.id_entidades = fc_clientes.id_entidades AND estado_final = 'FALSE'";

            string order = "amortizacion_detalle.numero_cuota_amortizacion_detalle";

            String where_to = "";

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }
            if (!String.IsNullOrEmpty(parametros.id_amortizacion_cabeza))
            {

                where_to += " AND amortizacion_cabeza.id_amortizacion_cabeza = " + parametros.id_amortizacion_cabeza;
            }

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where, order);


            dsTablaAmortizacionIndividual.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crTablaAmortizacion.rpt");
            crystalReport.PrintToPrinter(1, false, 0, 0);
            
            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsTablaAmortizacionIndividual.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
            
        }
    }
}